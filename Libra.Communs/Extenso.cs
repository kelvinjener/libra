using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Communs
{
    public static class Extenso
    {   /*******************************************************************************
        * Função para escrever por extenso os valores em Real (em C# - suporta até R$ 9.999.999.999,99)     *
        * Rotina Criada para ler um número e transformá-lo em extenso                                       *
        * Limite máximo de 9 Bilhões (9.999.999.999,99)                                                     *
        * Não aceita números negativos......                                                                *
        ************************************************************************************/

        public static string Extenso_Mes(int mes)
        {
            switch (mes)
            {
                case 1: return "Janeiro";
                case 2: return "Fevereiro";
                case 3: return "Março";
                case 4: return "Abril";
                case 5: return "Maio";
                case 6: return "Junho";
                case 7: return "Julho";
                case 8: return "Agosto";
                case 9: return "Setembro";
                case 10: return "Outubro";
                case 11: return "Novembro";
                case 12: return "Dezembro";
                default: return "";
            }
        }

        public static string Extenso_Valor(decimal pdbl_Valor)
        {
            string strValorExtenso = ""; //Variável que irá armazenar o valor por extenso do número informado
            string strNumero = "";       //Irá armazenar o número para exibir por extenso
            string strCentena = "";
            string strDezena = "";
            //string strUnidade = "";

            decimal dblCentavos = 0;
            decimal dblValorInteiro = 0;
            int intContador = 0;
            bool bln_Bilhao = false;
            bool bln_Milhao = false;
            bool bln_Mil = false;
            //bool bln_Real = false;
            bool bln_Unidade = false;

            //Verificar se foi informado um dado indevido
            if (pdbl_Valor == 0 || pdbl_Valor <= 0)
            {
                strValorExtenso = "Verificar se há valor negativo ou nada foi informado";
            }
            if (pdbl_Valor > (decimal)9999999999.99)
            {
                strValorExtenso = "Valor não suportado pela Função";
            }
            else //Entrada padrão do método
            {
                //Gerar Extenso Centavos
                dblCentavos = pdbl_Valor - (int)pdbl_Valor;
                //Gerar Extenso parte Inteira
                dblValorInteiro = (int)pdbl_Valor;
                if (dblValorInteiro > 0)
                {
                    if (dblValorInteiro > 999)
                    {
                        bln_Mil = true;
                    }
                    if (dblValorInteiro > 999999)
                    {
                        bln_Milhao = true;
                        bln_Mil = false;
                    }
                    if (dblValorInteiro > 999999999)
                    {
                        bln_Mil = false;
                        bln_Milhao = false;
                        bln_Bilhao = true;
                    }

                    for (int i = (dblValorInteiro.ToString().Trim().Length) - 1; i >= 0; i--)
                    {
                        // strNumero = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) + 1, 1);
                        strNumero = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) - 1, 1);
                        switch (i)
                        {            /*******/
                            case 9:  /*Bilhão*
                                     /*******/
                                {
                                    strValorExtenso = fcn_Numero_Unidade(strNumero) + ((Convert.ToInt16(strNumero) > 1) ? " Bilhões de" : " Bilhão de");
                                    bln_Bilhao = true;
                                    break;
                                }
                            case 8: /********/
                            case 5: //Centena*
                            case 2: /********/
                                {
                                    if (Convert.ToInt16(strNumero) > 0)
                                    {
                                        strCentena = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) - 1, 3);

                                        if (Convert.ToInt16(strCentena) > 100 && Convert.ToInt16(strCentena) < 200)
                                        {
                                            strValorExtenso = strValorExtenso + " Cento e ";
                                        }
                                        else
                                        {
                                            strValorExtenso = strValorExtenso + " " + fcn_Numero_Centena(strNumero);
                                        }
                                        if (intContador == 8)
                                        {
                                            bln_Milhao = true;
                                        }
                                        else if (intContador == 5)
                                        {
                                            bln_Mil = true;
                                        }
                                    }
                                    break;
                                }
                            case 7: /*****************/
                            case 4: //Dezena de Milhão*
                            case 1: /*****************/
                                {
                                    if (Convert.ToInt16(strNumero) > 0)
                                    {
                                        strDezena = Mid(dblValorInteiro.ToString().Trim(), (dblValorInteiro.ToString().Trim().Length - i) - 1, 2);//

                                        if (Convert.ToInt16(strDezena) > 10 && Convert.ToInt16(strDezena) < 20)
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
                                            bln_Milhao = true;
                                        }
                                        else if (intContador == 4)
                                        {
                                            bln_Mil = true;
                                        }
                                    }
                                    break;
                                }
                            case 6: /******************/
                            case 3: //Unidade de Milhão*
                            case 0: /******************/
                                {
                                    if (Convert.ToInt16(strNumero) > 0 && !bln_Unidade)
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
                                        if (bln_Milhao || Convert.ToInt16(strNumero) > 0)
                                        {
                                            strValorExtenso = strValorExtenso + ((Convert.ToInt16(strNumero) == 1) && !bln_Unidade ? " Milhão de" : " Milhões de");
                                            bln_Milhao = true;
                                        }
                                    }
                                    if (i == 3)
                                    {
                                        if (bln_Mil || Convert.ToInt16(strNumero) > 0)
                                        {
                                            strValorExtenso = strValorExtenso + " Mil";
                                            bln_Mil = true;
                                        }
                                    }
                                    if (i == 0)
                                    {
                                        if ((bln_Bilhao && !bln_Milhao && !bln_Mil
                                        && Right((dblValorInteiro.ToString().Trim()), 3) == "0")
                                        || (!bln_Bilhao && bln_Milhao && !bln_Mil
                                        && Right((dblValorInteiro.ToString().Trim()), 3) == "0"))
                                        {
                                            strValorExtenso = strValorExtenso + " de ";
                                        }
                                        strValorExtenso = strValorExtenso + ((Convert.ToInt16(dblValorInteiro.ToString())) > 1 ? " Reais " : " Real ");
                                    }
                                    bln_Unidade = false;
                                    break;
                                }
                        }
                    }//
                }
                if (dblCentavos > 0)
                {
                    if (Convert.ToInt64(dblCentavos.ToString().Replace(",", "")) > 0 && dblCentavos < (decimal)0.1)
                    {
                        strNumero = Right((Decimal.Round(dblCentavos, 2)).ToString().Trim(), 1);
                        strValorExtenso = strValorExtenso + ((Convert.ToInt16(dblCentavos.ToString()) > 0) ? " e " : " ")
                        + fcn_Numero_Unidade(strNumero) + ((Convert.ToInt16(strNumero) > 1) ? " Centavos " : "Centavo ");
                    }
                    else if (dblCentavos > (decimal)0.1 && dblCentavos < (decimal)0.2)
                    {
                        strNumero = Right(((Decimal.Round(dblCentavos, 2) - (decimal)0.1).ToString().Trim()), 1);
                        strValorExtenso = strValorExtenso + ((Convert.ToInt16(dblCentavos.ToString()) > 0) ? " " : " e ")
                        + fcn_Numero_Dezena0(strNumero) + " Centavos ";
                    }
                    else
                    {
                        if (dblCentavos > 0) //0#
                        {
                            strNumero = Right(dblCentavos.ToString().Trim(), 2);//Mid(dblCentavos.ToString().Trim(), 0, 1);//
                            strValorExtenso = strValorExtenso + ((Convert.ToInt16(strNumero) > 0) ? " e " : " ")//((Convert.ToInt16(dblCentavos.ToString()) > 0) ? " e " : " ")
                            + fcn_Numero_Dezena1(Left(strNumero, 1));
                            if ((dblCentavos.ToString().Trim().Length) > 2)
                            {
                                strNumero = Right((Decimal.Round(dblCentavos, 2)).ToString().Trim(), 1);
                                if (Convert.ToInt16(strNumero) > 0)
                                {
                                    if (Mid(strValorExtenso.Trim(), strValorExtenso.Trim().Length - 2, 1) == "e")
                                    {
                                        strValorExtenso = strValorExtenso + " " + fcn_Numero_Unidade(strNumero);
                                    }
                                    else
                                    {
                                        strValorExtenso = strValorExtenso + " e " + fcn_Numero_Unidade(strNumero);
                                    }
                                }
                            }
                            strValorExtenso = strValorExtenso + " Centavos ";
                        }
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

            array_Dezena0.Add("Onze");
            array_Dezena0.Add("Doze");
            array_Dezena0.Add("Treze");
            array_Dezena0.Add("Quatorze");
            array_Dezena0.Add("Quinze");
            array_Dezena0.Add("Dezesseis");
            array_Dezena0.Add("Dezessete");
            array_Dezena0.Add("Dezoito");
            array_Dezena0.Add("Dezenove");

            return array_Dezena0[((Convert.ToInt16(pstrDezena0)) - 1)].ToString();
        }
        private static string fcn_Numero_Dezena1(string pstrDezena1)
        {
            if (pstrDezena1.Equals("0"))
                return "";

            //Vetor que irá conter o número por extenso
            ArrayList array_Dezena1 = new ArrayList();

            array_Dezena1.Add("Dez");
            array_Dezena1.Add("Vinte");
            array_Dezena1.Add("Trinta");
            array_Dezena1.Add("Quarenta");
            array_Dezena1.Add("Cinquenta");
            array_Dezena1.Add("Sessenta");
            array_Dezena1.Add("Setenta");
            array_Dezena1.Add("Oitenta");
            array_Dezena1.Add("Noventa");

            return array_Dezena1[((Convert.ToInt16(pstrDezena1)) - 1)].ToString();
        }

        private static string fcn_Numero_Centena(string pstrCentena)
        {
            if (pstrCentena.Equals("0"))
                return "";

            //Vetor que irá conter o número por extenso
            ArrayList array_Centena = new ArrayList();

            array_Centena.Add("Cem");
            array_Centena.Add("Duzentos");
            array_Centena.Add("Trezentos");
            array_Centena.Add("Quatrocentos");
            array_Centena.Add("Quinhentos");
            array_Centena.Add("Seiscentos");
            array_Centena.Add("Setecentos");
            array_Centena.Add("Oitocentos");
            array_Centena.Add("Novecentos");

            return array_Centena[((Convert.ToInt16(pstrCentena)) - 1)].ToString();
        }
        private static string fcn_Numero_Unidade(string pstrUnidade)
        {
            if (pstrUnidade.Equals("0"))
                return "";

            //Vetor que irá conter o número por extenso
            ArrayList array_Unidade = new ArrayList();

            array_Unidade.Add("Um");
            array_Unidade.Add("Dois");
            array_Unidade.Add("Três");
            array_Unidade.Add("Quatro");
            array_Unidade.Add("Cinco");
            array_Unidade.Add("Seis");
            array_Unidade.Add("Sete");
            array_Unidade.Add("Oito");
            array_Unidade.Add("Nove");

            return array_Unidade[((Convert.ToInt16(pstrUnidade)) - 1)].ToString();
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

        public enum TipoValorExtenso
        {
            Monetario,
            Porcentagem,
            Decimal
        }

        public static string toExtenso(double Valor, TipoValorExtenso tipoValorExtenso)
        {
            decimal valorEscrever = new decimal(Valor);

            return toExtenso(valorEscrever, tipoValorExtenso);
        }

        // O método toExtenso recebe um valor do tipo decimal
        public static string toExtenso(decimal valor, TipoValorExtenso tipoValorExtenso)
        {
            if (valor <= 0 | valor >= 1000000000000000)
                throw new ArgumentOutOfRangeException("Valor não suportado pelo sistema. Valor: " + valor);

            string strValor = String.Empty;
            strValor = valor.ToString("000000000000000.00#");
            //strValor = valor.ToString("{0:0.00#}");
            string valor_por_extenso = string.Empty;
            int qtdCasasDecimais =
                strValor.Substring(strValor.IndexOf(',') + 1, strValor.Length - (strValor.IndexOf(',') + 1)).Length;
            bool existemValoresAposDecimal = Convert.ToInt32(strValor.Substring(16, qtdCasasDecimais)) > 0;

            for (int i = 0; i <= 15; i += 3)
            {
                var parte = strValor.Substring(i, 3);
                // se parte contém vírgula, pega a substring com base na quantidade de casas decimais.
                if (parte.IndexOf(',') > 0)
                {
                    parte = strValor.Substring(i + 1, qtdCasasDecimais);
                }
                valor_por_extenso += escreva_parte(Convert.ToDecimal(parte));
                if (i == 0 & valor_por_extenso != string.Empty)
                {
                    if (Convert.ToInt32(strValor.Substring(0, 3)) == 1)
                        valor_por_extenso += " TRILHÃO" +
                                             ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0)
                                                  ? " E "
                                                  : string.Empty);
                    else if (Convert.ToInt32(strValor.Substring(0, 3)) > 1)
                        valor_por_extenso += " TRILHÕES" +
                                             ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0)
                                                  ? " E "
                                                  : string.Empty);
                }
                else if (i == 3 & valor_por_extenso != string.Empty)
                {
                    if (Convert.ToInt32(strValor.Substring(3, 3)) == 1)
                        valor_por_extenso += " BILHÃO" +
                                             ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0)
                                                  ? " E "
                                                  : string.Empty);
                    else if (Convert.ToInt32(strValor.Substring(3, 3)) > 1)
                        valor_por_extenso += " BILHÕES" +
                                             ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0)
                                                  ? " E "
                                                  : string.Empty);
                }
                else if (i == 6 & valor_por_extenso != string.Empty)
                {
                    if (Convert.ToInt32(strValor.Substring(6, 3)) == 1)
                        valor_por_extenso += " MILHÃO" +
                                             ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0)
                                                  ? " E "
                                                  : string.Empty);
                    else if (Convert.ToInt32(strValor.Substring(6, 3)) > 1)
                        valor_por_extenso += " MILHÕES" +
                                             ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0)
                                                  ? " E "
                                                  : string.Empty);
                }
                else if (i == 9 & valor_por_extenso != string.Empty)
                    if (Convert.ToInt32(strValor.Substring(9, 3)) > 0)
                        valor_por_extenso += " MIL" +
                                             ((Convert.ToDecimal(strValor.Substring(12, 3)) > 0)
                                                  ? " E "
                                                  : string.Empty);

                if (i == 12)
                {
                    if (valor_por_extenso.Length > 8)
                        if (valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "BILHÃO" |
                            valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "MILHÃO")
                            valor_por_extenso += " DE";
                        else if (valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "BILHÕES" |
                                 valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "MILHÕES" |
                                 valor_por_extenso.Substring(valor_por_extenso.Length - 8, 7) == "TRILHÕES")
                            valor_por_extenso += " DE";
                        else if (valor_por_extenso.Substring(valor_por_extenso.Length - 8, 8) == "TRILHÕES")
                            valor_por_extenso += " DE";

                    if (Convert.ToInt64(strValor.Substring(0, 15)) == 1)
                    {
                        switch (tipoValorExtenso)
                        {
                            case TipoValorExtenso.Monetario:
                                valor_por_extenso += " REAL";
                                break;
                            case TipoValorExtenso.Porcentagem:
                                if (existemValoresAposDecimal == false)
                                    valor_por_extenso += " PORCENTO";
                                break;
                            case TipoValorExtenso.Decimal:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException("tipoValorExtenso");
                        }
                    }

                    else if (Convert.ToInt64(strValor.Substring(0, 15)) > 1)
                    {
                        switch (tipoValorExtenso)
                        {
                            case TipoValorExtenso.Monetario:
                                valor_por_extenso += " REAIS";
                                break;
                            case TipoValorExtenso.Porcentagem:
                                if (existemValoresAposDecimal == false)
                                    valor_por_extenso += " PORCENTO";
                                break;
                            case TipoValorExtenso.Decimal:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException("tipoValorExtenso");
                        }
                    }

                    if (Convert.ToInt32(strValor.Substring(16, 2)) > 0 && valor_por_extenso != string.Empty)
                    {
                        switch (tipoValorExtenso)
                        {
                            case TipoValorExtenso.Monetario:
                                valor_por_extenso += " E ";
                                break;
                            case TipoValorExtenso.Porcentagem:
                                valor_por_extenso += " VÍRGULA ";
                                break;
                            case TipoValorExtenso.Decimal:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException("tipoValorExtenso");
                        }
                    }
                }

                if (i == 15)
                    if (Convert.ToInt32(strValor.Substring(16, qtdCasasDecimais)) == 1)
                    {
                        switch (tipoValorExtenso)
                        {
                            case TipoValorExtenso.Monetario:
                                valor_por_extenso += " CENTAVO";
                                break;
                            case TipoValorExtenso.Porcentagem:
                                valor_por_extenso += " CENTAVO";
                                break;
                            case TipoValorExtenso.Decimal:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException("tipoValorExtenso");
                        }
                    }

                    else if (Convert.ToInt32(strValor.Substring(16, qtdCasasDecimais)) > 1)
                    {
                        switch (tipoValorExtenso)
                        {
                            case TipoValorExtenso.Monetario:
                                valor_por_extenso += " CENTAVOS";
                                break;
                            case TipoValorExtenso.Porcentagem:
                                valor_por_extenso += " PORCENTO";
                                break;
                            case TipoValorExtenso.Decimal:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException("tipoValorExtenso");
                        }
                    }
            }
            return valor_por_extenso;
        }

        private static string escreva_parte(decimal valor)
        {
            if (valor <= 0)
                return string.Empty;
            else
            {
                string montagem = string.Empty;
                if (valor > 0 & valor < 1)
                {
                    valor *= 100;
                }
                string strValor = valor.ToString("000");
                int a = Convert.ToInt32(strValor.Substring(0, 1));
                int b = Convert.ToInt32(strValor.Substring(1, 1));
                int c = Convert.ToInt32(strValor.Substring(2, 1));

                if (a == 1) montagem += (b + c == 0) ? "CEM" : "CENTO";
                else if (a == 2) montagem += "DUZENTOS";
                else if (a == 3) montagem += "TREZENTOS";
                else if (a == 4) montagem += "QUATROCENTOS";
                else if (a == 5) montagem += "QUINHENTOS";
                else if (a == 6) montagem += "SEISCENTOS";
                else if (a == 7) montagem += "SETECENTOS";
                else if (a == 8) montagem += "OITOCENTOS";
                else if (a == 9) montagem += "NOVECENTOS";

                if (b == 1)
                {
                    if (c == 0) montagem += ((a > 0) ? " E " : string.Empty) + "DEZ";
                    else if (c == 1) montagem += ((a > 0) ? " E " : string.Empty) + "ONZE";
                    else if (c == 2) montagem += ((a > 0) ? " E " : string.Empty) + "DOZE";
                    else if (c == 3) montagem += ((a > 0) ? " E " : string.Empty) + "TREZE";
                    else if (c == 4) montagem += ((a > 0) ? " E " : string.Empty) + "QUATORZE";
                    else if (c == 5) montagem += ((a > 0) ? " E " : string.Empty) + "QUINZE";
                    else if (c == 6) montagem += ((a > 0) ? " E " : string.Empty) + "DEZESSEIS";
                    else if (c == 7) montagem += ((a > 0) ? " E " : string.Empty) + "DEZESSETE";
                    else if (c == 8) montagem += ((a > 0) ? " E " : string.Empty) + "DEZOITO";
                    else if (c == 9) montagem += ((a > 0) ? " E " : string.Empty) + "DEZENOVE";
                }
                else if (b == 2) montagem += ((a > 0) ? " E " : string.Empty) + "VINTE";
                else if (b == 3) montagem += ((a > 0) ? " E " : string.Empty) + "TRINTA";
                else if (b == 4) montagem += ((a > 0) ? " E " : string.Empty) + "QUARENTA";
                else if (b == 5) montagem += ((a > 0) ? " E " : string.Empty) + "CINQUENTA";
                else if (b == 6) montagem += ((a > 0) ? " E " : string.Empty) + "SESSENTA";
                else if (b == 7) montagem += ((a > 0) ? " E " : string.Empty) + "SETENTA";
                else if (b == 8) montagem += ((a > 0) ? " E " : string.Empty) + "OITENTA";
                else if (b == 9) montagem += ((a > 0) ? " E " : string.Empty) + "NOVENTA";

                if (strValor.Substring(1, 1) != "1" & c != 0 & montagem != string.Empty) montagem += " E ";

                if (strValor.Substring(1, 1) != "1")
                    if (c == 1) montagem += "UM";
                    else if (c == 2) montagem += "DOIS";
                    else if (c == 3) montagem += "TRÊS";
                    else if (c == 4) montagem += "QUATRO";
                    else if (c == 5) montagem += "CINCO";
                    else if (c == 6) montagem += "SEIS";
                    else if (c == 7) montagem += "SETE";
                    else if (c == 8) montagem += "OITO";
                    else if (c == 9) montagem += "NOVE";

                return montagem;
            }
        }
    }
}

