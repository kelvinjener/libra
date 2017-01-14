using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Libra.Models;
using Libra.Control;
using Libra.Controllers.Core;

namespace Libra.Controllers.Negocio
{
    public class FornecedoresController : FornecedoresBll
    {
        public bool ValidarFornecedor(FornecedoresModel fornecedorModel)
        {
            if (string.IsNullOrEmpty(fornecedorModel.CNPJ))
            {
                return false;
            }

            return true;
        }

        public List<FornecedoresModel> RetornaFornecedores()
        {
            var listEntity = new List<FornecedoresModel>();
            var r = base.RetornaTodos();

            foreach (var i in r)
            {
                listEntity.Add(new FornecedoresModel()
                {
                    Id = i.FORNECEDORID,
                    TipoFornecedorId = i.TIPOFORNECEDORID,
                    OrigemFornecedorId = i.ORIGEMFORNECEDORID,
                    RazaoSocial = i.RAZAOSOCIAL,
                    NomeFantasia = i.NOMEFANTASIA,
                    CNPJ = i.CNPJ,
                    InscricaoEstadual = i.INSCRICAOESTADUAL,
                    InscricaoMunicipal = i.INSCRICAOMUNICIPAL,
                    Responsavel = i.RESPONSAVEL,
                    IndicadorFabricante = i.INDICADORFABRICANTE,
                    IndicadorReceberEmail = i.INDICADORRECEBEREMAIL,
                    RamoAtividade = i.RAMOATIVIDADE,
                    InfoAdicional = i.INFOADICIONAL,
                });
            }

            return listEntity;
        }

        public FornecedoresModel RetornaFornecedor(int id)
        {
            var r = base.RetornarPorId(id);

            return new FornecedoresModel()
            {
                Id = r.FORNECEDORID,
                TipoFornecedorId = r.TIPOFORNECEDORID,
                OrigemFornecedorId = r.ORIGEMFORNECEDORID,
                RazaoSocial = r.RAZAOSOCIAL,
                NomeFantasia = r.NOMEFANTASIA,
                CNPJ = r.CNPJ,
                InscricaoEstadual = r.INSCRICAOESTADUAL,
                InscricaoMunicipal = r.INSCRICAOMUNICIPAL,
                Responsavel = r.RESPONSAVEL,
                IndicadorFabricante = r.INDICADORFABRICANTE,
                IndicadorReceberEmail = r.INDICADORRECEBEREMAIL,
                RamoAtividade = r.RAMOATIVIDADE,
                InfoAdicional = r.INFOADICIONAL,
            };
        }

        public Resultado ExcluirFornecedor(int id)
        {
            try
            {
                var erros = new List<string>();
                var fornecedor = RetornarPorId(id);

                if (fornecedor == null)
                {
                    erros.Add("O fornecedor informado não existe.");
                }

                if (erros.Count == 0)
                {
                    this.MarcarComoExcluido(fornecedor);
                }

                return erros.Count == 0 ? Resultado.RetornaSuccesso(string.Format("O fornecedor {0} foi excluído com sucesso.", fornecedor.NOMEFANTASIA)) : Resultado.RetornaErro(erros);
            }
            catch (Exception ex)
            {
                return Resultado.RetornaErro("Ocorreu um erro enquanto um fornecedor era excluído.", ex);
            }
        }
    }
}