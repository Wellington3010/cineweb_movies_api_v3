using AutoMapper;
using cineweb_movies_api.DTO;
using cineweb_movies_api.Entities;
using cineweb_movies_api.Filters;
using cineweb_movies_api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cineweb_movies_api.Controllers
{
    [ApiController]
    [Route("ingressos")]
    public class IngressoController : Controller
    {
        private readonly IMapper _mapper;
        private readonly FilmeBaseRepository<Filme, Guid> _moviesRepository;
        private readonly IngressoBaseRepository<Ingresso, int, Guid> _ingressoBaseRepository;

        public IngressoController(IMapper mapper, IngressoBaseRepository<Ingresso, int, Guid> ingressoBaseRepository, FilmeBaseRepository<Filme, Guid> moviesRepository)
        {
            _mapper = mapper;
            _moviesRepository = moviesRepository;
            _ingressoBaseRepository = ingressoBaseRepository;
        }

        [HttpPost]
        [Route("cadastrar")]
        [Autorizacao]
        public async Task<IActionResult> CadastrarIngressos(IngressoDTO ingressoDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var filme = await _moviesRepository.FindByTitle(ingressoDTO.Titulo);
            var ingresso = _mapper.Map<Ingresso>(ingressoDTO);
            ingresso.Filme = filme;
            ingresso.FilmeId = filme.Id;
            await _ingressoBaseRepository.AddItem(ingresso);
            return Ok();
        }

        [HttpPost]
        [Route("deletar")]
        [Autorizacao]
        public async Task<IActionResult> DeletarIngressos(IngressoDTO ingressoDTO)
        {
            var filme = await _moviesRepository.FindByTitle(ingressoDTO.Titulo);
          
            await _ingressoBaseRepository.RemoveById(filme.Ingresso.IdIngresso);
            return Ok();
        }

        [HttpPost]
        [Route("atualizar")]
        [Autorizacao]
        public async Task<IActionResult> AtualizarIngressos(IngressoDTO ingressoDTO)
        {
            var filme = await _moviesRepository.FindByTitle(ingressoDTO.Titulo);
            var ingresso = _mapper.Map<Ingresso>(ingressoDTO);
            filme.Ingresso.Quantidade = ingresso.Quantidade;
            filme.Ingresso.Preco = ingresso.Preco;
            await _moviesRepository.Update(filme);
            return Ok();
        }

        [HttpGet]
        [Route("listar")]
        [Autorizacao]
        public IActionResult ListarIngressos()
        {
            return View();
        }
    }
}
