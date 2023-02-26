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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cineweb_movies_api_test.Filmes
{
    [TestFixture]
    [Category("UnitTests")]
    public class FilmesControllerTest
    {
        private FilmeController _controller;
        private Filme _filme;
        private Mock<FilmeBaseRepository<Filme, Guid>> _filmeBaseRepository;
        private Mock<IMapper> _mapper;
        private CreateMovieDTO _filmeComAtributoPosterNulo;
        private CreateMovieDTO _filmeComAtributoPosterInvalido;
        private CreateMovieDTO _filmeComAtributoPosterPreenchido;

        [SetUp]
        public void SetUp()
        {
            _mapper = new Mock<IMapper>();
            _filmeBaseRepository = new Mock<FilmeBaseRepository<Filme, Guid>>();
            _filmeComAtributoPosterInvalido = new CreateMovieDTO { Titulo = "FlasPoint", Active = true, Data = DateTime.Now, Genero = "Ação", HomeMovie = true, Poster = "ImagemForaDoFormatoWebp", Sinopse = "Sinopse" };
            _filmeComAtributoPosterNulo = new CreateMovieDTO { Titulo = "FlasPoint", Active = true, Data = DateTime.Now, Genero = "Ação", HomeMovie = true, Poster = null, Sinopse = "Sinopse" };
            _filmeComAtributoPosterPreenchido = new CreateMovieDTO { Titulo = "FlasPoint", Active = true, Data = DateTime.Now, Genero = "Ação", HomeMovie = true, Poster = "data:image/png;base64,iVBORw0KGg1kRbG0VZ2GoEX=", Sinopse = "Sinopse" };
            _filme = new Filme { Id = Guid.NewGuid() };
            _controller = new FilmeController(_filmeBaseRepository.Object, _mapper.Object);
        }


        [Test(Description = "CadastroDeFilmeSemPoster")]
        public void CadastroDeFilmeSemPoster()
        {
            _controller.ModelState.AddModelError("Poster", "É obrigatório o cadastro do poster");
            var retorno = _controller.SaveMovie(_filmeComAtributoPosterNulo);

            Assert.IsTrue(retorno.GetType() == typeof(BadRequestResult));
        }

        [Test(Description = "CadastroDeFilmeComPosterValido")]
        public void CadastroDeFilmeComPosterValido()
        {
            _mapper.Setup(x => x.Map<Filme>(_filmeComAtributoPosterPreenchido)).Returns(_filme);
            _filmeBaseRepository.Setup(x => x.AddItem(_filme)).Returns(Task.FromResult(new OkResult()));
            var retorno = _controller.SaveMovie(_filmeComAtributoPosterPreenchido);

            Assert.IsTrue(retorno.GetType() == typeof(OkObjectResult));
        }

        [Test(Description = "CadastroDeFilmeComPosterInvalido")]
        public void CadastroDeFilmeComPosterInvalido()
        {
            _controller.ModelState.AddModelError("Poster", "A imagem do poster deve estar no formato webp do tipo base64");
            var retorno = _controller.SaveMovie(_filmeComAtributoPosterInvalido);

            Assert.IsTrue(retorno.GetType() == typeof(BadRequestResult));
        }
    }
}
