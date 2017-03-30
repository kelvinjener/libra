using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Libra.Models;
using Libra.Control;
using Libra.Controllers.Core;
using Libra.Entity;

namespace Libra.Controllers.Negocio
{
    public class FornecedoresController : FornecedoresBll
    {
        public Resultado ValidarFornecedor(FornecedoresModel fornecedorModel)
        {
            var erros = new List<string>();

            if (string.IsNullOrEmpty(fornecedorModel.CNPJ))
            {
                erros.Add("O campo <b>CNPJ</b> é de preenchimento obrigatório!");
            }

            return erros.Count == 0 ? Resultado.RetornaSuccesso("Validação realizada com sucesso!") : Resultado.RetornaErro(erros);
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

        public Resultado InserirFornecedor(FornecedoresModel fornecedor)
        {
            var validacao = ValidarFornecedor(fornecedor);

            if (!validacao.IsError)
            {
                try
                {
                    var r = this.Inserir(ConverterParaEntidadeModelo(fornecedor));
                    return Resultado.RetornaSuccessoComCodigo("Fornecedor salvo com sucesso!", r.ToString());
                }
                catch (Exception ex)
                {
                    return Resultado.RetornaErro("Não foi possível salvar o Fornecedor!Verifique os campos informados.", ex);
                }
            }
            else
            {
                return Resultado.RetornaErro(validacao.Errors);
            }
        }

        public Resultado AtualizarFornecedor(FornecedoresModel fornecedor)
        {
            var validacao = ValidarFornecedor(fornecedor);

            if (!validacao.IsError)
            {
                try
                {
                    var r = this.Atualizar(ConverterParaEntidadeModelo(fornecedor));
                    return Resultado.RetornaSuccessoComCodigo("Fornecedor atualizado com sucesso!", r.ToString());
                }
                catch (Exception ex)
                {
                    return Resultado.RetornaErro("Ocorreu um erro enquanto o sistema tentava atualizar um fornecedor.", ex);
                }
            }
            else
            {
                return Resultado.RetornaErro(validacao.Errors);
            }
        }

        public FORNECEDORE ConverterParaEntidadeModelo(FornecedoresModel fornecedor)
        {
            var entidade = new FORNECEDORE()
            {
                TIPOFORNECEDORID = fornecedor.TipoFornecedorId,
                ORIGEMFORNECEDORID = fornecedor.OrigemFornecedorId,
                RAZAOSOCIAL = fornecedor.RazaoSocial,
                NOMEFANTASIA = fornecedor.NomeFantasia,
                CNPJ = fornecedor.CNPJ,
                INSCRICAOESTADUAL = fornecedor.InscricaoEstadual,
                INSCRICAOMUNICIPAL = fornecedor.InscricaoMunicipal,
                RESPONSAVEL = fornecedor.Responsavel,
                INDICADORFABRICANTE = fornecedor.IndicadorFabricante,
                INDICADORRECEBEREMAIL = fornecedor.IndicadorReceberEmail,
                RAMOATIVIDADE = fornecedor.RamoAtividade,
                INFOADICIONAL = fornecedor.InfoAdicional,
                CRIADOPOR = 1,
                DATACRIACAO = DateTime.Now.Date,
                ATIVO = true
            };

            if (fornecedor.Id != null)
            {
                entidade.FORNECEDORID = fornecedor.Id.Value;
            }

            return entidade;
        }
    }
}