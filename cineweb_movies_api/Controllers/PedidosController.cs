using AutoMapper;
using cineweb_movies_api.DTO;
using cineweb_movies_api.Entities;
using cineweb_movies_api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cineweb_movies_api.Controllers
{
    [ApiController]
    [Route("pedidos")]
    public class PedidosController : Controller
    {
        private ClienteBaseRepository<Cliente, int, string> _clientesRepository;
        private PedidoBaseRepository<Pedido, int> _pedidosRepository;
        private readonly FilmeBaseRepository<Filme, Guid> _moviesRepository;
        private readonly IngressoBaseRepository<Ingresso, int, Guid> _ingressoBaseRepository;
        private IMapper _mapper;

        public PedidosController(
            ClienteBaseRepository<Cliente, int, string> clienteRepository,
            FilmeBaseRepository<Filme, Guid> moviesRepository,
            IngressoBaseRepository<Ingresso, int, Guid> ingressoBaseRepository,
            PedidoBaseRepository<Pedido, int> pedidoRepository, IMapper mapper)
        {
            _clientesRepository = clienteRepository;
            _pedidosRepository = pedidoRepository;
            _moviesRepository = moviesRepository;
            _ingressoBaseRepository = ingressoBaseRepository;
            _mapper = mapper;
        }

        [NonAction]
        public PedidosController Instance()
        {
            return this;
        }


        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> CadastrarPedido(PedidoDTO pedidoDTO)
        {
            bool pedidoDeFilmeSemIngressoCadastrado = false;
            List<Pedido> pedidosParaCadastro = new List<Pedido>();
            var pedido = _mapper.Map<Pedido>(pedidoDTO);
            pedido.CodigoPedido = Guid.NewGuid();
            var cliente = await _clientesRepository.FindByCPF(pedidoDTO.CPF);

            if (cliente is not null)
            {
                pedido.IdCliente = cliente.IdCliente;
            }

            if (cliente is null)
            {
                await _clientesRepository.AddItem(new Cliente { CPF = pedidoDTO.CPF, NomeCliente = pedidoDTO.NomeCliente });
                cliente = await _clientesRepository.FindByCPF(pedidoDTO.CPF);
                pedido.IdCliente = cliente.IdCliente;
            }


            pedidoDTO.Titulos.ForEach((item) =>
            {
                pedido.FilmeId = _moviesRepository.FindByTitle(item).Result.Id;
                var ingressos = _ingressoBaseRepository.ListarIngressosPorFilme(pedido.FilmeId).Result;

                if(!(ingressos.Quantidade > 0))
                {
                   pedidoDeFilmeSemIngressoCadastrado = true;
                }

                pedido.IdIngresso = ingressos.IdIngresso;
               

                var novoPedido = new Pedido
                {
                    FilmeId = pedido.FilmeId,
                    IdCliente = pedido.IdCliente,
                    IdIngresso = pedido.IdIngresso,
                    CodigoPedido = pedido.CodigoPedido,
                    ValorTotal = pedido.ValorTotal,
                };

                pedidosParaCadastro.Add(novoPedido);
            });

            if(pedidoDeFilmeSemIngressoCadastrado)
            {
                return BadRequest();
            }

            pedidosParaCadastro.ForEach((item) =>
            {
                try
                {
                    var ingressos = _ingressoBaseRepository.ListarIngressosPorFilme(item.FilmeId).Result;
                    ingressos.Quantidade -= 1;
                    _ingressoBaseRepository.Update(ingressos);
                    _pedidosRepository.AddItem(item);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            });

            return Ok();
        }

        [HttpDelete]
        [Route("deletar")]
        public IActionResult DeletarPedido()
        {
            return View();
        }

        [HttpPut]
        [Route("atualizar")]
        public IActionResult AtualizarPedido()
        {
            return View();
        }

        [HttpGet]
        [Route("listar")]
        public async Task<IActionResult> ListarPedidos(int IdUsuario)
        {

            var cliente = await _clientesRepository.FindById(IdUsuario);

            if (cliente is not null)
            {
                return Ok(_mapper.Map<PedidoDTO>(await _pedidosRepository.FindPedidosByCliente(cliente.IdCliente)));
            }

            return BadRequest();
        }
    }
}
