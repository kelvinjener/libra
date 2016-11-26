using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Communs
{
    /// <summary>
    /// Classe que retorna 'enum' como inteiro ou string
    /// </summary>
    public class EnumUtils
    {
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

    [TypeConverter(typeof(LocalizedEnumConverter))]
    public enum UnidadeFederativa : short
    {
        [Description("Acre")]
        AC = 1,
        [Description("Alagoas")]
        AL = 2,
        [Description("Amapá")]
        AP = 3,
        [Description("Amazonas")]
        AM = 4,
        [Description("Bahia")]
        BA = 5,
        [Description("Ceará")]
        CE = 6,
        [Description("Distrito Federal")]
        DF = 7,
        [Description("Espírito Santo")]
        ES = 8,
        [Description("Roraima")]
        RR = 9,
        [Description("Goiás")]
        GO = 10,
        [Description("Maranhão")]
        MA = 11,
        [Description("Mato Grosso")]
        MT = 12,
        [Description("Mato Grosso do Sul")]
        MS = 13,
        [Description("Minas Gerais")]
        MG = 14,
        [Description("Pará")]
        PA = 15,
        [Description("Paraíba")]
        PB = 16,
        [Description("Paraná")]
        PR = 17,
        [Description("Pernambuco")]
        PE = 18,
        [Description("Piauí")]
        PI = 19,
        [Description("Rio de Janeiro")]
        RJ = 20,
        [Description("Rio Grande do Norte")]
        RN = 21,
        [Description("Rio Grande do Sul")]
        RS = 22,
        [Description("Rondônia")]
        RO = 23,
        [Description("Tocantins")]
        TO = 24,
        [Description("Santa Catarina")]
        SC = 25,
        [Description("São Paulo")]
        SP = 26,
        [Description("Sergipe")]
        SE = 27
    }

    [TypeConverter(typeof(LocalizedEnumConverter))]
    public enum DiaSemana : short
    {
        [Description("Domingo")]
        Domingo = 0,
        [Description("Segunda")]
        Segunda = 1,
        [Description("Terça")]
        Terça = 2,
        [Description("Quarta")]
        Quarta = 3,
        [Description("Quinta")]
        Quinta = 4,
        [Description("Sexta")]
        Sexta = 5,
        [Description("Sábado")]
        Sabado = 6
    }

    [TypeConverter(typeof(LocalizedEnumConverter))]
    public enum EstadoCivil : short
    {
        [Description("Solteiro")]
        Solteiro = 0,
        [Description("Casado")]
        Casado = 1,
        [Description("Desquitado")]
        Desquitado = 2,
        [Description("Divorciado")]
        Divorciado = 3,
        [Description("Viúvo")]
        Viuvo = 4
    }

    [TypeConverter(typeof(LocalizedEnumConverter))]
    public enum Sexo : short
    {
        [Description("Feminino")]
        Feminino = 0,
        [Description("Masculino")]
        Masculino = 1
    }

    [TypeConverter(typeof(LocalizedEnumConverter))]
    public enum TipoLog : short
    {
        [Description("Consulta")]
        Select = 0,
        [Description("Inclusão")]
        Insert = 1,
        [Description("Alteração")]
        Update = 2,
        [Description("Exclusão")]
        Delete = 3
    }

    [TypeConverter(typeof(LocalizedEnumConverter))]
    public enum Situacao : short
    {
        [Description("Inativo")]
        Inativo = 0,
        [Description("Ativo")]
        Ativo = 1
    }

    [TypeConverter(typeof(LocalizedEnumConverter))]
    public enum TipoUnidade : short
    {
        [Description("Matriz")]
        Matriz = 0,
        [Description("Depósito")]
        Deposito = 1,
        [Description("Loja")]
        Loja = 2
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
