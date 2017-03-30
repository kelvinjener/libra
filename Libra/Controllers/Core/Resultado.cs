using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libra.Controllers.Core
{
    public class Resultado
    {
        public Resultado()
        {
            this.Errors = new List<string>();
        }

        public string CorrelationId { get; set; }
        public bool IsError { get; set; }
        public List<string> Errors { get; set; }
        public string ErrorMessage { get; set; }
        public string Message { get; set; }
        public string ReturnCode { get; set; }
        public object ReturnObject { get; set; }

        public static Resultado SUCCESSO
        {
            get
            {
                return Resultado.RetornaSuccessoComCodigo("Operação realizada com sucesso!", "1001");
            }
        }

        public static Resultado ERRO
        {
            get
            {
                return Resultado.RetornaErro("Falha ao executar o método remoto!", "1002");
            }
        }

        public static Resultado RetornaSuccesso(string message, string code, object returnObject)
        {
            return new Resultado()
            {
                IsError = false,
                Message = message,
                ReturnCode = code,
                ReturnObject = returnObject
            };
        }

        public static Resultado RetornaSuccessoComCodigo(string message, string code)
        {
            return Resultado.RetornaSuccesso(message, code, null);
        }

        public static Resultado RetornaSuccesso(object returnObject)
        {
            return Resultado.RetornaSuccesso(SUCCESSO.Message, SUCCESSO.ReturnCode, returnObject);
        }

        public static Resultado RetornaSuccesso(string message)
        {
            return Resultado.RetornaSuccesso(message, SUCCESSO.ReturnCode, null);
        }

        public static Resultado RetornaSuccesso(string message, object returnObject)
        {
            return Resultado.RetornaSuccesso(message, SUCCESSO.ReturnCode, returnObject);
        }

        public static Resultado RetornaErro(string message, string code, List<string> errors = null, Exception ex = null)
        {
            return new Resultado()
            {
                IsError = true,
                Message = message,
                ReturnCode = code,
                Errors = errors,
                ErrorMessage = ex != null ? ex.Message : "",
            };
        }

        public static Resultado RetornaErro(string message, List<string> errors)
        {
            return RetornaErro(message, ERRO.ReturnCode, errors);
        }

        public static Resultado RetornaErro(List<string> errors)
        {
            return RetornaErro(errors.FirstOrDefault(), ERRO.ReturnCode, errors);
        }

        public static Resultado RetornaErro(string message)
        {
            return RetornaErro(message, ERRO.ReturnCode);
        }

        public static Resultado RetornaErro(string message, Exception ex)
        {
            return RetornaErro(message, ERRO.ReturnCode, null, ex);
        }

        public static Resultado RetornaErro(string message, string code, Exception ex)
        {
            return RetornaErro(message, code, null, ex);
        }
    }
}