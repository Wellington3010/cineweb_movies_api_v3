﻿using AutoMapper;
using cineweb_movies_api.DTO;
using cineweb_movies_api.Entities;
using cineweb_movies_api.Filters;
using cineweb_movies_api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cineweb_movies_api.Controllers
{
    [ApiController]
    [Route("movies")]
    public class FilmeController : Controller
    {
        private readonly FilmeBaseRepository<Filme, int> _moviesRepository;
        private readonly IMapper _mapper;
        
        public FilmeController(FilmeBaseRepository<Filme, int> repo, IMapper mapper)
        {
            _moviesRepository = repo;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("home")]
        public IActionResult GetHomeMovies()
        {
            List<UserMovieDTO> userMovies = new List<UserMovieDTO>();

            var currentMovies = _moviesRepository.ListItems().Where(x => x.HomeMovie == true && x.Active == false).ToList();

            currentMovies.ForEach((item) =>
            {
                userMovies.Add(_mapper.Map<UserMovieDTO>(item));
            });

            return Json(userMovies);
        }

        [HttpGet]
        [Route("current")]
        public IActionResult GetCurrentMovies()
        {
            List<UserMovieDTO> userMovies = new List<UserMovieDTO>();

            var currentMovies = _moviesRepository.ListItems().Where(x => x.Active == true).ToList();

            currentMovies.ForEach((item) =>
            {
                userMovies.Add(_mapper.Map<UserMovieDTO>(item));
            });

            return Json(userMovies);
        }

        [HttpGet]
        [Route("current/by-date")]
        public IActionResult GetCurrentMoviesByDate(DateTime date)
        {
            List<UserMovieDTO> userMovies = new List<UserMovieDTO>();

            var currentMovies = _moviesRepository.ListItems().Where(x => x.Active == true && x.Data <= date).ToList();

            currentMovies.ForEach((item) =>
            {
                userMovies.Add(_mapper.Map<UserMovieDTO>(item));
            });

            return Json(userMovies);
        }

        [HttpGet]
        [Route("coming-soon")]
        public IActionResult GetComingSoonMovies()
        {
            List<UserMovieDTO> userMovies = new List<UserMovieDTO>();

            var comingSoonMovies = _moviesRepository.ListItems().Where(x => x.Active == false).ToList();

            comingSoonMovies.ForEach((item) =>
            {
                userMovies.Add(_mapper.Map<UserMovieDTO>(item));
            });

            return Json(userMovies);
        }

        [HttpGet]
        [Route("coming-soon/by-date")]
        public IActionResult GetComingSoonMoviesByDate(DateTime date)
        {
            List<UserMovieDTO> userMovies = new List<UserMovieDTO>();

            var comingSoonMovies = _moviesRepository.ListItems().Where(x => x.Active == false && x.Data <= date).ToList();

            comingSoonMovies.ForEach((item) =>
            {
                userMovies.Add(_mapper.Map<UserMovieDTO>(item));
            });

            return Json(userMovies);
        }

        [HttpGet]
        [Route("by-parameter")]
        public IActionResult GetMoviesByParameter(string parameter, string parameterType)
        {
            List<UserMovieDTO> userMovies = new List<UserMovieDTO>();
            var dictionaryMovies = new Dictionary<string, List<Filme>>();
            dictionaryMovies.Add("Titulo", _moviesRepository.ListItems().Where(x => x.Titulo == parameter).ToList());
            dictionaryMovies.Add("Genero", _moviesRepository.ListItems().Where(x => x.Genero == parameter).ToList());

            dictionaryMovies[parameterType].ForEach((item) =>
            {
                userMovies.Add(_mapper.Map<UserMovieDTO>(item));
            });

            return Json(userMovies);
        }

        [HttpPost]
        [Route("admin/save-movie")]
        [Autorizacao]
        public IActionResult SaveMovie([FromBody] CreateMovieDTO movie)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieEntity = _mapper.Map<Filme>(movie);
            var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(movie.Poster, "");
            movieEntity.Poster = data;
            _moviesRepository.AddItem(movieEntity);
            return Ok(true);
        }

        [HttpPost]
        [Route("admin/update-movie")]
        [Autorizacao]
        public async Task<IActionResult> UpdateMovie([FromBody] UpdateMovieDTO movie)
        {
            var movieEntity = _mapper.Map<Filme>(movie);
            var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(movie.Poster, "");
            movieEntity.Poster = data;
            var movieResult = await _moviesRepository.FindByTitle(movie.TituloAntigo);
            await _moviesRepository.RemoveById(movieResult.Id);
            movieEntity.Id = movieResult.Id;
            await _moviesRepository.AddItem(movieEntity);
            return Json(true);
        }

        [HttpPost]
        [Route("admin/delete-movie")]
        [Autorizacao]
        public async Task<IActionResult> DeleteMovieById([FromBody] DeleteMovieDTO movie)
        {
            var movieResult = await _moviesRepository.FindByTitle(movie.TituloAntigo);
            await _moviesRepository.RemoveById(movieResult.Id);
            return Json(true);
        }

        [HttpGet]
        [Route("admin/find-by-movie-Genero")]
        public async Task<IActionResult> FindByMovieGenero(string Genero)
        {
            var adminMovies = new List<MovieDTO>();
            var moviesByGenero = await _moviesRepository.FindByGenre(Genero);

            moviesByGenero.ForEach((item) =>
            {
                adminMovies.Add(_mapper.Map<MovieDTO>(item));
            });
            return Json(adminMovies);
        }

        [HttpGet]
        [Route("admin/all-movies")]
        public async Task<IActionResult> AllMovies()
        {
            var adminMovies = new List<MovieDTO>();
            var allMovies = await _moviesRepository.FindAll();

            allMovies.ForEach((item) =>
            {
                adminMovies.Add(_mapper.Map<MovieDTO>(item));
            });
            return Json(adminMovies);
        }

        [HttpGet]
        [Route("admin/find-by-movie-Titulo")]
        public IActionResult FindByMovieTitulo(string Titulo)
        {
            return Json(_mapper.Map<MovieDTO>(_moviesRepository.FindByTitle(Titulo)));
        }
    }
}
