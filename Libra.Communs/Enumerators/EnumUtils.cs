using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Communs.Enumerators
{
    /// <summary>
    /// Classe que retorna 'enum' como inteiro ou string
    /// </summary>
    public static class EnumUtils
    {

        /// <summary>
        /// Retorna o valor informado no atributo StringValue do enum
        /// </summary>
        public static Int16 GetValueInt(this Enum enumm)
        {
            Int16 output = 0;
            Type type = enumm.GetType();

            FieldInfo fi = type.GetField(enumm.ToString());

            if (fi != null)
            {
                IntValue[] attrs = fi.GetCustomAttributes(typeof(IntValue), false) as IntValue[];


                if (attrs.Length > 0)
                {
                    output = attrs[0].Value;
                }
            }

            return output;
        }

        /// <summary>
        /// Retorna o valor informado no atributo StringValue do enum
        /// </summary>
        public static string GetValue(this Enum enumm)
        {
            string output = null;
            Type type = enumm.GetType();

            FieldInfo fi = type.GetField(enumm.ToString());
            StringValue[] attrs = fi.GetCustomAttributes(typeof(StringValue), false) as StringValue[];

            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }

            return output;
        }

        /// <summary>
        /// Retorna o valor informado no atributo Description do enum
        /// </summary>
        public static string GetDescription(this Enum ennum)
        {
            string output = null;
            Type type = ennum.GetType();

            FieldInfo fi = type.GetField(ennum.ToString());
            DescriptionAttribute[] attrs = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attrs.Length > 0)
            {
                output = attrs[0].Description;
            }

            return output;
        }

        public static T ParseEnum<T>(string value)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(StringValue)) as StringValue;
                if (attribute != null)
                {
                    if (attribute.Value == value)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == value)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", "description");
            //return default(T);
        }

        public static T ParseEnum<T>(Int16 value)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(IntValue)) as IntValue;
                if (attribute != null)
                {
                    if (attribute.Value == value)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == value.ToString())
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", "description");
            // or return default(T);
        }

        public static string stringValueOf(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static object enumValueOf(string value, Type enumType)
        {
            string[] names = Enum.GetNames(enumType);

            foreach (string name in names)
            {
                if (stringValueOf((Enum)Enum.Parse(enumType, name)).Equals(value))
                    return Enum.Parse(enumType, name);
            }

            throw new ArgumentException("A string não foi identificada como atributo de enumeração.");
        }
    }

    public static class Enum<T>
    {
        public static string Description(T value)
        {
            DescriptionAttribute[] da = (DescriptionAttribute[])(typeof(T).GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false));
            return da.Length > 0 ? da[0].Description : value.ToString();
        }
    }
}
