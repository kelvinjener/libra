using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Libra.Models;
using Libra.Control;

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
                CNPJ = r.CNPJ,
                NomeFantasia = r.NOMEFANTASIA
            };
        }
    }
}