using Libra.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Control
{
    public class CaracteristicasDiversasProdutoBll : LibraBusinessLogic<CARACTERISTICASDIVERSASPRODUTO>
    {
        private LibraDataContext dc = new LibraDataContext();

        public bool VerificaProdutoCaracteristicasAssociados(int idCaracteristicas)
        {
            bool associado = false;

            List<PRODUTOCARACTERISTICADIVERSA> produtoCaracteristicas = (from pcd in dc.PRODUTOCARACTERISTICADIVERSAs
                                                                         where pcd.CARACTERISTICASDIVERSASPRODUTOID == idCaracteristicas
                                                                         select pcd).ToList();

            if (produtoCaracteristicas.Count > 0)
                associado = true;

            return associado;
        }

        public CARACTERISTICASDIVERSASPRODUTO GetCaracteristicasDiversasProdutoById(int idCaracteristicasDiversasProduto)
        {
            return dc.CARACTERISTICASDIVERSASPRODUTOs.Where(u => u.CARACTERISTICASDIVERSASPRODUTOID == idCaracteristicasDiversasProduto).FirstOrDefault();
        }

        public List<CARACTERISTICASDIVERSASPRODUTO> GetAllCaracteristicasDiversasProdutos()
        {
            return dc.CARACTERISTICASDIVERSASPRODUTOs.OrderBy(u => u.NOME).ToList();
        }

        public List<CaracteristicasDiversasProdutoModelGrid> GetAllCaracteristicasDiversasGrid()
        {
            var result = (from CaracteristicasDiversasProduto in dc.CARACTERISTICASDIVERSASPRODUTOs
                          orderby CaracteristicasDiversasProduto.NOME
                          select new CaracteristicasDiversasProdutoModelGrid
                          {
                              CaracteristicasDiversasProdutoId = CaracteristicasDiversasProduto.CARACTERISTICASDIVERSASPRODUTOID,
                              Nome = CaracteristicasDiversasProduto.NOME,
                              Ativo = CaracteristicasDiversasProduto.ATIVO
                          });

            return result.Distinct().OrderBy(p => p.Nome).ToList();
        }

        public List<CaracteristicasDiversasProdutoModelGrid> GetAllCaracteristicasDiversasGridFiltro(List<bool> listAtivo, string nomeCaracteristicasDiversasProduto)
        {
            var result = (from CaracteristicasDiversasProduto in dc.CARACTERISTICASDIVERSASPRODUTOs
                          orderby CaracteristicasDiversasProduto.NOME
                          select new
                          {
                              CaracteristicasDiversasProduto
                          });

            if (listAtivo.Count > 0)
            {
                result = result.Where(a => listAtivo.Contains(a.CaracteristicasDiversasProduto.ATIVO));
            }

            if (!nomeCaracteristicasDiversasProduto.Equals(string.Empty))
                result = result.Where(a => a.CaracteristicasDiversasProduto.NOME.ToUpper().Contains(nomeCaracteristicasDiversasProduto.ToUpper()));

            var x = from p in result
                    select new CaracteristicasDiversasProdutoModelGrid
                    {
                        CaracteristicasDiversasProdutoId = p.CaracteristicasDiversasProduto.CARACTERISTICASDIVERSASPRODUTOID,
                        Nome = p.CaracteristicasDiversasProduto.NOME,
                        Ativo = p.CaracteristicasDiversasProduto.ATIVO
                    };

            return x.Distinct().OrderBy(p => p.Nome).ToList();
        }

        public List<CARACTERISTICASDIVERSASPRODUTO> GetAllAtivos()
        {
            return dc.CARACTERISTICASDIVERSASPRODUTOs.Where(u => u.ATIVO).OrderBy(u => u.NOME).ToList();
        }


        public void Salvar(CARACTERISTICASDIVERSASPRODUTO CaracteristicasDiversasProduto)
        {
            if (CaracteristicasDiversasProduto.CARACTERISTICASDIVERSASPRODUTOID > 0)
                Atualizar(CaracteristicasDiversasProduto);
            else
                Inserir(CaracteristicasDiversasProduto);
        }
    }

    public class CaracteristicasDiversasProdutoModelGrid
    {
        public int CaracteristicasDiversasProdutoId { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }

    }
}
