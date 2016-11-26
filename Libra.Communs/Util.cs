using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Libra.Communs
{
    public static class Util
    {
        /// <summary>
        /// Ordena uma coleção que implementa a interface IList.
        /// </summary>
        /// <param name="T1">Tipo dos itens contidos na coleção.</param>
        /// <param name="T2">Tipo do retorno da função "sortFunction".</param>
        /// <param name="collection">Coleção a ser ordenada.</param>
        /// <param name="sortFunction">Função que define a ordenação.</param>
        /// <param name="ascending">Boolean que define se a ordenação deve ser crescente ou descrescente.</param>
        /// <returns>void. O resultado é verificado na própria coleção "collection".</returns>
        public static void Sort<T1, T2>(IList collection, Func<T1, T2> sortFunction, bool ascending = true)
        {
            var list = new List<T1>();

            list.AddRange(collection.Cast<T1>().ToList());
            collection.Clear();

            var arr = ascending
              ? list.OrderBy(x => { return sortFunction(x); })
              : list.OrderByDescending(x => { return sortFunction(x); });

            foreach (var item in arr)
                collection.Add(item);
        }

        /// <summary>
        /// Remove determinados caracteres de um texto.
        /// </summary>
        /// <param name="texto">Texto a ser modificado.</param>
        /// <param name="caracteres">Cada um dos caracteres que deve ser individualmente removido.</param>
        /// <returns></returns>
        public static string RemoverCaracteres(string texto, string caracteres)
        {
            for (int i = 0; i < caracteres.Length; i++)
            {
                texto = texto.Replace(caracteres.Substring(i, 1), "");
            }

            return texto;
        }


        /// <summary>
        /// Determina, a partir da extensão de um arquivo informado, se este é um arquivo de imagem.
        /// </summary>
        /// <param name="caminho"></param>
        /// <returns></returns>
        public static bool ArquivoDeImagem(string caminho)
        {
            string extensao = Path.GetExtension(caminho).ToLower();
            String[] extensoesPermitidas = { ".gif", ".jpeg", ".jpg", ".png" };

            for (int i = 0; i < extensoesPermitidas.Length; i++)
            {
                if (extensao == extensoesPermitidas[i])
                    return true;
            }

            return false;
        }


        /// <summary>
        /// Mescla células do GridView, evitando repetições verticais de determinada coluna, por um determinado DataKey (int)
        /// </summary>
        /// <param name="gridView">GridView </param>
        /// <param name="indiceColuna">Índice da coluna cujas células serão mescladas</param>
        /// <param name="DataKeyName">Nome do DataKey utilizado para criar o agrupamento</param>
        public static void MesclarCelulasGridView(ref System.Web.UI.WebControls.GridView gridView,
            int indiceColuna, string DataKeyName)
        {
            // Percorre, na ordem inversa, do último ao segundo elemento
            for (int i = gridView.Rows.Count - 1; i >= 1; i--)
            {
                // Verifica se o DataKey do registro atual é igual do anterior
                if ((int)gridView.DataKeys[i][DataKeyName] == (int)gridView.DataKeys[i - 1][DataKeyName])
                {
                    // Obtém o rowSpan do registro atual
                    int rowSpan = gridView.Rows[i].Cells[indiceColuna].RowSpan;
                    // O padrão do RowSpan é 0 (ocupando 1 célula). Ocupando 2 células, o RowSpan é 2
                    // Portanto, foi necessário transformar o "neutro" em 1 para acumular corretamente
                    if (rowSpan == 0) rowSpan = 1;

                    // Acumula o RowSpan na célula anterior
                    gridView.Rows[i - 1].Cells[indiceColuna].RowSpan = rowSpan + 1;
                    // Oculta a célula atual
                    gridView.Rows[i].Cells[indiceColuna].Visible = false;
                }
            }
        }


        #region "Escrever valor por extenso - código encontrado na internet"

        /// <summary>
        /// Função para escrever por extenso os valores em Real (em C# - suporta até R$ 9.999.999.999,99)     
        /// Rotina Criada para ler um número e transformá-lo em extenso                                       
        /// Limite máximo de 9 bilhões (9.999.999.999,99).
        /// Não aceita números negativos. 
        /// LinhadeCodigo.com.br Autor: José F. Mar / milton P. Jr
        /// Corrigido por Adriano Santos - 2007
        /// </summary> 
        /// <param name="pdbl_Valor">Valor para converter em extenso. Limite máximo de 9 bilhões (9.999.999.999,99).</param>
        /// <returns>String do valor por Extenso</returns> 
        public static string Extenso_Valor(decimal? pdbl_Valor)
        {
            string strValorExtenso = ""; //Variável que irá armazenar o valor por extenso do número informado
            string strNumero = "";       //Irá armazenar o número para exibir por extenso 
            string strCentena = "";
            string strDezena = "";
            string strDezCentavo = "";

            decimal dblcentavos = 0;
            decimal dblValorInteiro = 0;
            int intContador = 0;
            bool bln_Bilhao = false;
            bool bln_milhao = false;
            bool bln_mil = false;
            bool bln_Unidade = false;

            //Verificar se foi informado um dado indevido 
            if ((pdbl_Valor == 0 || pdbl_Valor <= 0) || (pdbl_Valor > (decimal)9999999999.99) || (pdbl_Valor == null))
            {
                //throw new Exception("Valor não suportado pela Função. Verificar se há valor negativo ou nada foi informado");
            }
            else //Entrada padrão do método
            {
                //Gerar Extenso centavos 
                pdbl_Valor = (Decimal.Round(Convert.ToDecimal(pdbl_Valor), 2));
                dblcentavos = Convert.ToDecimal(pdbl_Valor) - (Int64)Convert.ToDecimal(pdbl_Valor);

                //Gerar Extenso parte Inteira
                dblValorInteiro = (Int64)pdbl_Valor;
                if (dblValorInteiro > 0)
                {
                    if (dblValorInteiro > 999)
                    {
                        bln_mil = true;
                    }
                    if (dblValorInteiro > 999999)
                    {
                        bln_milhao = true;
                        bln_mil = false;
                    }
                    if (dblValorInteiro > 999999999)
                    {
                        bln_mil = false;
                        bln_milhao = false;
                        bln_Bilhao = true;
                    }

                    for (int i = (dblValorInteiro.ToString().Trim().Length) - 1; i >= 0; i--)
                    {
                        // strNumero = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) + 1, 1);
                        strNumero = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) - 1, 1);
                        switch (i)
                        {            /*******/
                            case 9:  /*bilhão*
                                         /*******/
                                {
                                    strValorExtenso = fcn_Numero_Unidade(strNumero) + ((int.Parse(strNumero) > 1) ? " bilhões e" : " bilhão e");
                                    bln_Bilhao = true;
                                    break;
                                }
                            case 8: /********/
                            case 5: //Centena*
                            case 2: /********/
                                {
                                    if (int.Parse(strNumero) > 0)
                                    {
                                        strCentena = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) - 1, 3);

                                        if (int.Parse(strCentena) > 100 && int.Parse(strCentena) < 200)
                                        {
                                            strValorExtenso = strValorExtenso + " cento e ";
                                        }
                                        else
                                        {
                                            strValorExtenso = strValorExtenso + " " + fcn_Numero_Centena(strNumero);
                                        }
                                        if (intContador == 8)
                                        {
                                            bln_milhao = true;
                                        }
                                        else if (intContador == 5)
                                        {
                                            bln_mil = true;
                                        }
                                    }
                                    break;
                                }
                            case 7: /*****************/
                            case 4: //Dezena de milhão*
                            case 1: /*****************/
                                {
                                    if (int.Parse(strNumero) > 0)
                                    {
                                        strDezena = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) - 1, 2);//

                                        if (int.Parse(strDezena) > 10 && int.Parse(strDezena) < 20)
                                        {
                                            strValorExtenso = strValorExtenso + (Right(strValorExtenso, 5).Trim() == "entos" ? " e " : " ")
                                            + fcn_Numero_Dezena0(Right(strDezena, 1));//corrigido

                                            bln_Unidade = true;
                                        }
                                        else
                                        {
                                            strValorExtenso = strValorExtenso + (Right(strValorExtenso, 5).Trim() == "entos" ? " e " : " ")
                                            + fcn_Numero_Dezena1(Left(strDezena, 1));//corrigido 

                                            bln_Unidade = false;
                                        }
                                        if (intContador == 7)
                                        {
                                            bln_milhao = true;
                                        }
                                        else if (intContador == 4)
                                        {
                                            bln_mil = true;
                                        }
                                    }
                                    break;
                                }
                            case 6: /******************/
                            case 3: //Unidade de milhão* 
                            case 0: /******************/
                                {
                                    if (int.Parse(strNumero) > 0 && !bln_Unidade)
                                    {
                                        if ((Right(strValorExtenso, 5).Trim()) == "entos"
                                        || (Right(strValorExtenso, 3).Trim()) == "nte"
                                        || (Right(strValorExtenso, 3).Trim()) == "nta")
                                        {
                                            strValorExtenso = strValorExtenso + " e ";
                                        }
                                        else
                                        {
                                            strValorExtenso = strValorExtenso + " ";
                                        }
                                        strValorExtenso = strValorExtenso + fcn_Numero_Unidade(strNumero);
                                    }
                                    if (i == 6)
                                    {
                                        if (bln_milhao || int.Parse(strNumero) > 0)
                                        {
                                            strValorExtenso = strValorExtenso + ((int.Parse(strNumero) == 1) && !bln_Unidade ? " milhão" : " milhões");
                                            strValorExtenso = strValorExtenso + ((int.Parse(strNumero) > 1000000) ? " " : " e");
                                            bln_milhao = true;
                                        }
                                    }
                                    if (i == 3)
                                    {
                                        if (bln_mil || int.Parse(strNumero) > 0)
                                        {
                                            strValorExtenso = strValorExtenso + " mil";
                                            strValorExtenso = strValorExtenso + ((int.Parse(strNumero) > 1000) ? " " : " e");
                                            bln_mil = true;
                                        }
                                    }
                                    if (i == 0)
                                    {
                                        if ((bln_Bilhao && !bln_milhao && !bln_mil
                                        && Right((dblValorInteiro.ToString().Trim()), 3) == "0")
                                        || (!bln_Bilhao && bln_milhao && !bln_mil
                                        && Right((dblValorInteiro.ToString().Trim()), 3) == "0"))
                                        {
                                            strValorExtenso = strValorExtenso + " e ";
                                        }
                                        strValorExtenso = strValorExtenso + ((Int64.Parse(dblValorInteiro.ToString())) > 1 ? " reais" : " real");
                                    }
                                    bln_Unidade = false;
                                    break;
                                }
                        }
                    }//
                }
                if (dblcentavos > 0)
                {

                    if (dblcentavos > 0 && dblcentavos < 0.1M)
                    {
                        strNumero = Right((Decimal.Round(dblcentavos, 2)).ToString().Trim(), 1);
                        strValorExtenso = strValorExtenso + ((dblcentavos > 0) ? " e " : " ")
                        + fcn_Numero_Unidade(strNumero) + ((dblcentavos > 0.01M) ? " centavos" : " Centavo");
                    }
                    else if (dblcentavos > 0.1M && dblcentavos < 0.2M)
                    {
                        strNumero = Right(((Decimal.Round(dblcentavos, 2) - (decimal)0.1).ToString().Trim()), 1);
                        strValorExtenso = strValorExtenso + ((dblcentavos > 0) ? " " : " e ")
                        + fcn_Numero_Dezena0(strNumero) + " centavos ";
                    }
                    else
                    {
                        strNumero = Right(dblcentavos.ToString().Trim(), 2);
                        strDezCentavo = Mid(dblcentavos.ToString().Trim(), 2, 1);

                        strValorExtenso = strValorExtenso + ((int.Parse(strNumero) > 0) ? " e " : " ");
                        strValorExtenso = strValorExtenso + fcn_Numero_Dezena1(Left(strDezCentavo, 1));

                        if ((dblcentavos.ToString().Trim().Length) > 2)
                        {
                            strNumero = Right((Decimal.Round(dblcentavos, 2)).ToString().Trim(), 1);
                            if (int.Parse(strNumero) > 0)
                            {
                                if (dblValorInteiro <= 0)
                                {
                                    if (Mid(strValorExtenso.Trim(), strValorExtenso.Trim().Length - 2, 1) == "e")
                                    {
                                        strValorExtenso = strValorExtenso + " e " + fcn_Numero_Unidade(strNumero);
                                    }
                                    else
                                    {
                                        strValorExtenso = strValorExtenso + " e " + fcn_Numero_Unidade(strNumero);
                                    }
                                }
                                else
                                {
                                    strValorExtenso = strValorExtenso + " e " + fcn_Numero_Unidade(strNumero);
                                }
                            }
                        }
                        strValorExtenso = strValorExtenso + " centavos ";
                    }
                }
                if (dblValorInteiro < 1) strValorExtenso = Mid(strValorExtenso.Trim(), 2, strValorExtenso.Trim().Length - 2);
            }

            return strValorExtenso.Trim();
        }
        private static string fcn_Numero_Dezena0(string pstrDezena0)
        {
            //Vetor que irá conter o número por extenso 
            ArrayList array_Dezena0 = new ArrayList();

            array_Dezena0.Add("onze");
            array_Dezena0.Add("doze");
            array_Dezena0.Add("treze");
            array_Dezena0.Add("quatorze");
            array_Dezena0.Add("quinze");
            array_Dezena0.Add("dezesseis");
            array_Dezena0.Add("dezessete");
            array_Dezena0.Add("dezoito");
            array_Dezena0.Add("dezenove");

            return array_Dezena0[((int.Parse(pstrDezena0)) - 1)].ToString();
        }
        private static string fcn_Numero_Dezena1(string pstrDezena1)
        {
            //Vetor que irá conter o número por extenso
            ArrayList array_Dezena1 = new ArrayList();

            array_Dezena1.Add("dez");
            array_Dezena1.Add("vinte");
            array_Dezena1.Add("trinta");
            array_Dezena1.Add("quarenta");
            array_Dezena1.Add("cinquenta");
            array_Dezena1.Add("sessenta");
            array_Dezena1.Add("setenta");
            array_Dezena1.Add("oitenta");
            array_Dezena1.Add("noventa");

            return array_Dezena1[Int16.Parse(pstrDezena1) - 1].ToString();
        }
        private static string fcn_Numero_Centena(string pstrCentena)
        {
            //Vetor que irá conter o número por extenso
            ArrayList array_Centena = new ArrayList();

            array_Centena.Add("cem");
            array_Centena.Add("duzentos");
            array_Centena.Add("trezentos");
            array_Centena.Add("quatrocentos");
            array_Centena.Add("quinhentos");
            array_Centena.Add("seiscentos");
            array_Centena.Add("setecentos");
            array_Centena.Add("oitocentos");
            array_Centena.Add("novecentos");

            return array_Centena[((int.Parse(pstrCentena)) - 1)].ToString();
        }
        private static string fcn_Numero_Unidade(string pstrUnidade)
        {
            //Vetor que irá conter o número por extenso
            ArrayList array_Unidade = new ArrayList();

            array_Unidade.Add("um");
            array_Unidade.Add("dois");
            array_Unidade.Add("três");
            array_Unidade.Add("quatro");
            array_Unidade.Add("cinco");
            array_Unidade.Add("seis");
            array_Unidade.Add("sete");
            array_Unidade.Add("oito");
            array_Unidade.Add("nove");

            return array_Unidade[(int.Parse(pstrUnidade) - 1)].ToString();
        }
        //Começa aqui os Métodos de Compatibilazação com VB 6 .........Left() Right() Mid()
        public static string Left(string param, int length)
        {
            //we start at 0 since we want to get the characters starting from the 
            //left and with the specified lenght and assign it to a variable
            if (param == "")
                return "";
            string result = param.Substring(0, length);
            //return the result of the operation 
            return result;
        }
        public static string Right(string param, int length)
        {
            //start at the index based on the lenght of the sting minus
            //the specified lenght and assign it a variable 
            if (param == "")
                return "";
            string result = param.Substring(param.Length - length, length);
            //return the result of the operation
            return result;
        }
        public static string Mid(string param, int startIndex, int length)
        {
            //start at the specified index in the string ang get N number of
            //characters depending on the lenght and assign it to a variable 
            string result = param.Substring(startIndex, length);
            //return the result of the operation
            return result;
        }
        public static string Mid(string param, int startIndex)
        {
            //start at the specified index and return all characters after it
            //and assign it to a variable
            string result = param.Substring(startIndex);
            //return the result of the operation 
            return result;
        }
        ////Acaba aqui os Métodos de Compatibilazação com VB 6 .........
        #endregion

        #region Define Tipagem para anônimos
        public static T MakeAnonymous<T>(object obj, T type)
        {
            return (T)obj;
        }

        public static List<T> MakeListAnonymous<T>(System.Collections.IEnumerable collection, T type)
        {
            List<T> lst = new List<T>();
            foreach (T o in collection)
            {
                lst.Add(o);
            }
            return lst;
        }
        #endregion

        public static string MakeTimeString(double num)
        {
            double hours = Math.Floor(num); //take integral part
            double minutes = (num - hours) * 60.0; //multiply fractional part with 60
            //double seconds = (minutes - Math.Floor(minutes)) * 60.0; //add if you want seconds
            int H = (int)Math.Floor(hours);
            int M = (int)Math.Floor(minutes);
            //int S = (int)Math.Floor(seconds); //add if you want seconds
            return H.ToString() + ":" + M.ToString().PadRight(2, '0'); //+":" + S.ToString(); //add if you want seconds
        }

        public static string RetornaUfByNomeEstado(string nomeEstado)
        {
            string nome = string.Empty;
            switch (nomeEstado)
            {
                case "Acre":
                    nome = "AC";
                    break;
                case "Alagoas":
                    nome = "AL";
                    break;
                case "Amapá;":
                    nome = "AP";
                    break;
                case "Amazonas":
                    nome = "AM";
                    break;
                case "Bahia":
                    nome = "BA";
                    break;
                case "Ceará;":
                    nome = "CE";
                    break;
                case "Distrito Federal":
                    nome = "DF";
                    break;
                case "Espírito Santo":
                    nome = "ES";
                    break;
                case "Goiás":
                    nome = "GO";
                    break;
                case "Maranhão":
                    nome = "MA";
                    break;
                case "Mato Grosso":
                    nome = "MT";
                    break;
                case "Mato Grosso do Sul":
                    nome = "MS";
                    break;
                case "Minas Gerais":
                    nome = "MG";
                    break;
                case "Pará":
                    nome = "PA";
                    break;
                case "Paraíba":
                    nome = "PB";
                    break;
                case "Paraná":
                    nome = "PR";
                    break;
                case "Pernambuco":
                    nome = "PE";
                    break;
                case "Piauí;":
                    nome = "PI";
                    break;
                case "Rio de Janeiro":
                    nome = "RJ";
                    break;
                case "Rio Grande do Norte":
                    nome = "RN";
                    break;
                case "Rio Grande do Sul":
                    nome = "RS";
                    break;
                case "Rondônia":
                    nome = "RO";
                    break;
                case "Roraima":
                    nome = "RR";
                    break;
                case "Santa Catarina":
                    nome = "SC";
                    break;
                case "São Paulo":
                    nome = "SP";
                    break;
                case "Sergipe":
                    nome = "SE";
                    break;
                case "Tocantins":
                    nome = "TO";
                    break;
            }
            return nome;
        }

        public static void AppendCssStyle(Page page, string css)
        {
            page.Header.Controls.Add(new LiteralControl(String.Format("<style tyle=\"text/css\">{0}</style>", css)));
        }

        public static void AppendCssStyleFile(Page page, string filePath)
        {
            page.Header.Controls.Add(
              new LiteralControl(String.Format("<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\">", filePath)));
        }

        public static void AppendJavaScript(Page page, string script, bool body = true)
        {
            var ctrl = new LiteralControl(String.Format("<script type=\"text/javascript\">{0}</script>", script));

            if (body)
                page.Controls.Add(ctrl);
            else
                page.Header.Controls.Add(ctrl);
        }

        public static void AppendJavaScriptFile(Page page, string filePath, bool body = true)
        {
            var ctrl = new LiteralControl(String.Format("<script type=\"text/javascript\" src=\"{0}\"></script>", filePath));

            if (body)
                page.Controls.Add(ctrl);
            else
                page.Header.Controls.Add(ctrl);
        }

        public static T GetProperty<T>(object obj, string propertyName)
        {
            return (T)obj.GetType().GetProperty(propertyName, typeof(T)).GetValue(obj, null);
        }

        public static string GetOrdinalExtenso(string numero)
        {
            string numeroExtenso = string.Empty;

            string[] unidades = { "", "Primeiro", "Segundo", "Terceiro", "Quarto", "Quinto", "Sexto", "Sétimo", "Oitavo", "Nono" };
            string[] dezenas = { "", "Décimo ", "Vigésimo ", "Trigésimo ", "Quadragésimo ", "Quinquagésimo ", "Sexagésimo ", "Setuagésimo ", "Octogésimo ", "Novagésimo " };
            string[] centenas = { "", "Centésimo ", "Ducentésimo ", "Tricentésimo ", "Quadringentésimo ", "Quingentésimo ", "Sexcentésimo ", "Septingentésimo ", "Octingentésimo ", "Noningentésimo " };

            // tira os possiveis espacos em branco.
            numero = Convert.ToString(numero).Trim();

            try
            {
                for (int i = 1; i <= numero.Length; i++)
                {
                    if (numero.Length - i == 0)
                        numeroExtenso = numeroExtenso + unidades[Convert.ToInt32(numero.Substring(Convert.ToInt32(numero.Length - 1), 1))];

                    if (numero.Length - i == 1)
                        numeroExtenso = numeroExtenso + dezenas[Convert.ToInt32(numero.Substring(Convert.ToInt32(numero.Length - 2), 1))];

                    if (numero.Length - i == 2)
                        numeroExtenso = numeroExtenso + centenas[Convert.ToInt32(numero.Substring(Convert.ToInt32(numero.Length - 3), 1))];

                    if (numero.Length - i > 2)
                    {
                        numeroExtenso = "";
                        break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return numeroExtenso;
        }
    }
}

