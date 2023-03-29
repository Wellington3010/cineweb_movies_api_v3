using AutoMapper;
using cineweb_movies_api.Controllers;
using cineweb_movies_api.DTO;
using cineweb_movies_api.Entities;
using cineweb_movies_api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cineweb_movies_api_test.Pedidos
{
    [TestFixture]
    [Category("UnitTests")]
    public class PedidosControllerTest
    {
        private PedidosController _controller;
        private PedidoDTO _pedido;
        private Pedido _pedidoEntity;
        private Mock<IngressoBaseRepository<Ingresso, int, int>> _ingressoRepository;
        private Mock<FilmeBaseRepository<Filme, int>> _filmeBaseRepository;
        private Mock<PedidoBaseRepository<Pedido, int>> _pedidoBaseRepository;
        private Mock<ClienteBaseRepository<Cliente, int, string>> _clienteBaseRepository;
        private Mock<IMapper> _mapper;
        private Ingresso _ingressoQuantidadeZerada;
        private Ingresso _ingressoQuantidadeMaiorQueZero;
        private Filme _filme;
        private Cliente _cliente;

        [SetUp]
        public void SetUp()
        {
            _mapper = new Mock<IMapper>();
            _ingressoRepository = new Mock<IngressoBaseRepository<Ingresso, int, int>>();
            _filmeBaseRepository = new Mock<FilmeBaseRepository<Filme, int>>();
            _pedidoBaseRepository = new Mock<PedidoBaseRepository<Pedido, int>>();
            _clienteBaseRepository = new Mock<ClienteBaseRepository<Cliente, int, string>>();
            _ingressoQuantidadeZerada = new Ingresso { FilmeId = 1, IdIngresso = 5, Preco = 75, Quantidade = 0 };
            _ingressoQuantidadeMaiorQueZero = new Ingresso { FilmeId = 1, IdIngresso = 5, Preco = 75, Quantidade = 50 };
            _filme = new Filme { Id = 1 };
            _ingressoQuantidadeZerada.Filme = _filme;
            _ingressoQuantidadeMaiorQueZero.Filme = _filme;
            _pedido = new PedidoDTO { CPF = "233.642.340-51", NomeCliente = "Jaber", Titulos = new List<string>(), ValorTotal = 125 };
            _cliente = new Cliente { CPF = "233.642.340-51", IdCliente = 5, NomeCliente = "Jaber" };
            _pedidoEntity = new Pedido();
            _pedido.Titulos.Add(string.Empty);
            _controller = new PedidosController(_clienteBaseRepository.Object, _filmeBaseRepository.Object, _ingressoRepository.Object, _pedidoBaseRepository.Object, _mapper.Object);
        }


        [Test(Description = "RealizarPedidoParaFilmeSemIngressosCadastrados")]
        public void RealizarPedidoParaFilmeSemIngressosCadastrados()
        {
            _mapper.Setup(x => x.Map<Pedido>(_pedido)).Returns(_pedidoEntity);
            _filmeBaseRepository.Setup(x => x.FindByTitle(string.Empty).Result).Returns(_filme);
            _ingressoRepository.Setup(x => x.ListarIngressosPorFilme(_filme.Id).Result).Returns(_ingressoQuantidadeZerada);
            _clienteBaseRepository.Setup(x => x.FindByCPF(_pedido.CPF).Result).Returns(_cliente);
            var retorno = _controller.CadastrarPedido(_pedido).Result;

            Assert.IsTrue(retorno.GetType() == typeof(BadRequestResult));
        }

        [Test(Description = "RealizarPedidoParaFilmeSemIngressosCadastrados")]
        public void RealizarPedidoParaFilmeComIngressosCadastrados()
        {
            _mapper.Setup(x => x.Map<Pedido>(_pedido)).Returns(_pedidoEntity);
            _filmeBaseRepository.Setup(x => x.FindByTitle(string.Empty).Result).Returns(_filme);
            _ingressoRepository.Setup(x => x.ListarIngressosPorFilme(_filme.Id).Result).Returns(_ingressoQuantidadeMaiorQueZero);
            _ingressoRepository.Setup(x => x.Update(_ingressoQuantidadeMaiorQueZero)).Returns(Task.FromResult(new OkResult()));
            _pedidoBaseRepository.Setup(x => x.AddItem(new Pedido())).Returns(Task.FromResult(new OkResult()));
            _clienteBaseRepository.Setup(x => x.FindByCPF(_pedido.CPF).Result).Returns(_cliente);
            var retorno = _controller.CadastrarPedido(_pedido).Result;

            Assert.IsTrue(retorno.GetType() == typeof(OkResult));
        }
    }
}
