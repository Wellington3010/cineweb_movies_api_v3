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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace cineweb_movies_api_test.Ingressos
{
    [TestFixture]
    [Category("UnitTests")]
    public class IngressosControllerTest : ControllerBase
    {
        private IngressoController _controller;
        private Ingresso _ingresso;
        private Mock<IngressoBaseRepository<Ingresso, int, Guid>> _ingressoRepository;
        private Mock<FilmeBaseRepository<Filme, Guid>> _filmeBaseRepository;
        private Mock<IMapper> _mapper;
        private IngressoDTO _ingressoQuantidadeZeradaDTO;
        private IngressoDTO _ingressoQuantidadeMaiorQueZeroDTO;
        private Filme _filme;

        [SetUp]
        public void SetUp()
        {
            _mapper = new Mock<IMapper>();
            _ingressoRepository = new Mock<IngressoBaseRepository<Ingresso, int, Guid>>();
            _filmeBaseRepository = new Mock<FilmeBaseRepository<Filme, Guid>>();
            _ingressoQuantidadeZeradaDTO = new IngressoDTO { Preco = 75, Quantidade = 0, Titulo = "FlasPoint" };
            _ingressoQuantidadeMaiorQueZeroDTO = new IngressoDTO { Preco = 75, Quantidade = 300, Titulo = "FlasPoint" };
            _filme = new Filme { Id = Guid.NewGuid() };
            _ingresso = new Ingresso { Preco = 75, Quantidade = 300, Filme = _filme, FilmeId = _filme.Id, IdIngresso = 5 };
            _controller = new IngressoController(_mapper.Object, _ingressoRepository.Object, _filmeBaseRepository.Object);
        }


        [Test(Description = "CadastroDeIngressoComQuantidadeZerada")]
        public void RealizarPedidoParaFilmeSemIngressosCadastrados()
        {
            _controller.ModelState.AddModelError("Quantidade", "A quantidade deve ser maior que zero");
            var retorno = _controller.CadastrarIngressos(_ingressoQuantidadeZeradaDTO).Result;

            Assert.IsTrue(retorno.GetType() == typeof(BadRequestResult));
        }

        [Test(Description = "CadastroDeIngressoComQuantidadeMaiorQueZero")]
        public void RealizarPedidoParaFilmeComIngressosCadastrados()
        {
            _mapper.Setup(x => x.Map<Ingresso>(_ingressoQuantidadeMaiorQueZeroDTO)).Returns(_ingresso);
            _filmeBaseRepository.Setup(x => x.FindByTitle(_ingressoQuantidadeMaiorQueZeroDTO.Titulo).Result).Returns(_filme);
            _ingressoRepository.Setup(x => x.AddItem(_ingresso)).Returns(Task.FromResult(new OkResult()));

            var retorno = _controller.CadastrarIngressos(_ingressoQuantidadeMaiorQueZeroDTO).Result;

            Assert.IsTrue(retorno.GetType() == typeof(OkResult));
        }
    }
}
