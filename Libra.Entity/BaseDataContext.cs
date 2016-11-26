using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Data;

namespace Libra.Entity
{
    public static class GetEntityByPK
    {
        public static T Get<T>(T valuesPK) where T : class
        {
            LibraDataContext context = new LibraDataContext();
            var table = context.GetTable<T>();
            var mapping = context.Mapping.GetTable(typeof(T));
            var pkfield = mapping.RowType.DataMembers.Where(d => d.IsPrimaryKey);
            if (pkfield == null)
                throw new Exception(String.Format("Table {0} does not contain a Primary Key field", mapping.TableName));

            string Sql = "select * from " + mapping.TableName;
            string where = " WHERE ";
            int i = 0;
            object[] param = new object[pkfield.Count()];
            foreach (MetaDataMember item in pkfield)
            {
                where += item.Name + " = {" + i + "} AND ";
                object value = valuesPK.GetType().GetProperty(item.Name).GetValue(valuesPK, null);
                param[i] = value;
                i++;
            }

            Sql += where.Substring(0, where.Length - 4);

            IEnumerable<T> entityListasda = context.ExecuteQuery<T>(Sql, param);

            return entityListasda.SingleOrDefault();
        }
    }

    public class BaseDataContext : DataContext
    {
        private static USUARIO usuario;

        public static USUARIO Usuario
        {
            get { return BaseDataContext.usuario; }
            set { BaseDataContext.usuario = value; }
        }

        /// <summary>
        /// Método utilizado para desatachar a entidade do contexto que ela pertence.
        /// </summary>
        /// <param name="entity">Entidade</param>
        public static void Detach(Object entity)
        {
            Type t = entity.GetType();

            System.Reflection.PropertyInfo[] properties = t.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            foreach (var property in properties)
            {
                string name = property.Name;

                if (property.PropertyType.IsGenericType &&
                property.PropertyType.GetGenericTypeDefinition() == typeof(EntitySet<>))
                {
                    property.SetValue(entity, null, null);
                }

                object[] attributes = property.GetCustomAttributes(true);
                if (attributes.Length > 0)
                {
                    foreach (object obj in attributes)
                    {
                        if (obj.GetType().Name == "ColumnAttribute")
                        {
                            ColumnAttribute column = (ColumnAttribute)obj;
                            column.IsVersion = column.IsPrimaryKey;
                        }
                    }
                }
            }

            System.Reflection.FieldInfo[] fields = t.GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            foreach (var field in fields)
            {
                string name = field.Name;

                if (field.FieldType.IsGenericType &&
                field.FieldType.GetGenericTypeDefinition() == typeof(EntityRef<>))
                {
                    field.SetValue(entity, null);
                }
            }

            System.Reflection.EventInfo eventPropertyChanged = t.GetEvent("PropertyChanged");
            System.Reflection.EventInfo eventPropertyChanging = t.GetEvent("PropertyChanging");

            if (eventPropertyChanged != null)
            {
                eventPropertyChanged.RemoveEventHandler(entity, null);
            }

            if (eventPropertyChanging != null)
            {
                eventPropertyChanging.RemoveEventHandler(entity, null);
            }
        }
        /// <summary>
        /// Método utilizado para desatachar a entidade do contexto que ela pertence.
        /// </summary>
        /// <typeparam name="T">Tipo da Entidade</typeparam>
        /// <param name="entity">Entidade</param>
        public static void Detach<T>(T entity)
        {
            Type t = entity.GetType();

            System.Reflection.PropertyInfo[] properties = t.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            foreach (var property in properties)
            {
                string name = property.Name;

                if (property.PropertyType.IsGenericType &&
                property.PropertyType.GetGenericTypeDefinition() == typeof(EntitySet<>))
                {
                    property.SetValue(entity, null, null);
                }

                object[] attributes = property.GetCustomAttributes(true);
                if (attributes.Length > 0)
                {
                    foreach (object obj in attributes)
                    {
                        if (obj.GetType().Name == "ColumnAttribute")
                        {
                            ColumnAttribute column = (ColumnAttribute)obj;
                            column.IsVersion = column.IsPrimaryKey;
                        }
                    }
                }
            }

            System.Reflection.FieldInfo[] fields = t.GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            foreach (var field in fields)
            {
                string name = field.Name;

                if (field.FieldType.IsGenericType &&
                field.FieldType.GetGenericTypeDefinition() == typeof(EntityRef<>))
                {
                    field.SetValue(entity, null);
                }
            }

            System.Reflection.EventInfo eventPropertyChanged = t.GetEvent("PropertyChanged");
            System.Reflection.EventInfo eventPropertyChanging = t.GetEvent("PropertyChanging");

            if (eventPropertyChanged != null)
            {
                eventPropertyChanged.RemoveEventHandler(entity, null);
            }

            if (eventPropertyChanging != null)
            {
                eventPropertyChanging.RemoveEventHandler(entity, null);
            }
        }

        public bool GravarLog { get; set; }

        private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();

        public static int IdUsuario
        {
            get
            {
                return usuario.USUARIOID;
            }
        }

        public BaseDataContext() :
            base(global::Libra.Entity.Properties.Settings.Default.LibraDBConnectionString, mappingSource)
        {
            GravarLog = true;
        }

        public BaseDataContext(string connection) :
            base(connection, mappingSource)
        {
            GravarLog = true;
        }

        public BaseDataContext(IDbConnection connection) :
            base(connection, mappingSource)
        {
            GravarLog = true;
        }

        public BaseDataContext(string connection, MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            GravarLog = true;
        }

        public BaseDataContext(IDbConnection connection, MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            GravarLog = true;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public new System.Data.Linq.Table<T> GetTable<T>() where T : class
        {
            System.Data.Linq.Table<T> list = base.GetTable<T>();

            return list;
        }

    }
}
