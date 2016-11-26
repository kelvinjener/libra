using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Libra.Entity
{
    public interface ILibraBusinessLogic
    {
        void Selecting(object sender, LinqDataSourceSelectEventArgs e);
        void Selected(object sender, LinqDataSourceStatusEventArgs e);
        void Inserting(object sender, LinqDataSourceInsertEventArgs e);
        void Inserted(object sender, LinqDataSourceStatusEventArgs e);
        void Deleting(object sender, LinqDataSourceDeleteEventArgs e);
        void Deleted(object sender, LinqDataSourceStatusEventArgs e);
        void Updating(object sender, LinqDataSourceUpdateEventArgs e);
        void Updated(object sender, LinqDataSourceStatusEventArgs e);
    }

    public interface ILibraBusinessLogicSave<TEntity>
    {
        int Atualizar(TEntity entity);
        int Inserir(TEntity entity);
        int Deletar(TEntity entity);
    }

    public class LibraBusinessLogic<TEntity> : ILibraBusinessLogic, ILibraBusinessLogicSave<TEntity> where TEntity : class, new()
    {
        public LibraBusinessLogic()
        {
        }

        #region Listar
        public virtual Table<TEntity> List()
        {
            LibraDataContext contexto = new LibraDataContext();
            return contexto.GetTable<TEntity>();
        }

        public virtual IQueryable<TEntity> List(Expression<Func<TEntity, bool>> filtro)
        {
            LibraDataContext contexto = new LibraDataContext();
            return contexto.GetTable<TEntity>().Where(filtro);
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> key)
        {
            LibraDataContext contexto = new LibraDataContext();
            return contexto.GetTable<TEntity>().FirstOrDefault(key);
        }

        public virtual TEntity FindDefault(Expression<Func<TEntity, bool>> key)
        {
            TEntity entity = null;
            try
            {
                entity = this.Find(key);
                if (entity == null)
                    entity = new TEntity();
            }
            catch
            {
            }
            return entity;
        }

        public virtual Boolean Exists(Expression<Func<TEntity, bool>> key)
        {
            LibraDataContext contexto = new LibraDataContext();
            return contexto.GetTable<TEntity>().Count(key) > 0;
        }

        #endregion

        #region ILibraBusinessLogic Members

        public virtual void Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
        }

        public virtual void Selected(object sender, LinqDataSourceStatusEventArgs e)
        {
        }

        public virtual void Inserting(object sender, LinqDataSourceInsertEventArgs e)
        {
        }

        public virtual void Inserted(object sender, LinqDataSourceStatusEventArgs e)
        {
        }

        public virtual void Deleting(object sender, LinqDataSourceDeleteEventArgs e)
        {
        }

        public virtual void Deleted(object sender, LinqDataSourceStatusEventArgs e)
        {
        }

        public virtual void Updating(object sender, LinqDataSourceUpdateEventArgs e)
        {
        }

        public virtual void Updated(object sender, LinqDataSourceStatusEventArgs e)
        {
        }

        #endregion

        #region ILibraBusinessLogicSave Members [Inserir, Atualizar, Deletar]

        public virtual int Atualizar(TEntity entity)
        {
            ChangeSet changeSet;
            TEntity originalEntity = GetEntityByPK.Get<TEntity>(entity);
            using (LibraDataContext contexto = new LibraDataContext())
            {
                BaseDataContext.Detach<TEntity>(entity);
                BaseDataContext.Detach<TEntity>(originalEntity);

                contexto.GetTable<TEntity>().Attach(entity, originalEntity);
                changeSet = contexto.GetChangeSet();
                contexto.SubmitChanges();
            }
            return changeSet.Updates.Count;
        }

        public virtual int Inserir(TEntity entity)
        {
            ChangeSet changeSet;
            using (LibraDataContext contexto = new LibraDataContext())
            {
                contexto.GetTable<TEntity>().InsertOnSubmit(entity);
                changeSet = contexto.GetChangeSet();
                contexto.SubmitChanges();
            }
            return changeSet.Inserts.Count;
        }

        public virtual int Deletar(TEntity entity)
        {
            ChangeSet changeSet;
            TEntity originalEntity = GetEntityByPK.Get<TEntity>(entity);
            using (LibraDataContext contexto = new LibraDataContext())
            {
                BaseDataContext.Detach<TEntity>(entity);
                BaseDataContext.Detach<TEntity>(originalEntity);

                contexto.GetTable<TEntity>().Attach(entity, originalEntity);
                contexto.GetTable<TEntity>().DeleteOnSubmit(entity);
                changeSet = contexto.GetChangeSet();
                contexto.SubmitChanges();
            }
            return changeSet.Deletes.Count;
        }

        #endregion
    }
}