using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiTarefas.Database;
using ApiTarefas.Models;
using ApiTarefas.ModelViews;
using ApiTarefas.Dto;
using ApiTarefas.Services;
using ApiTarefas.Models.Erros;
using ApiTarefas.Services.Interfaces;

namespace ApiTarefas.Controllers
{
    [ApiController]
    [Route("/tarefas/")]
    public class TarefasController : ControllerBase
    {
        public TarefasController(ITarefaServico servico)
        {
            _servico = servico;
        }

        private readonly ITarefaServico _servico;

        [HttpGet]
        public IActionResult Get(int page = 1)
        {
            var tarefas = _servico.GetTarefas(page);
            return StatusCode(200, tarefas);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TarefaDto tarefaDto)
        {
            try
            {
                var tarefa = _servico.CreateTarefa(tarefaDto);
                return StatusCode(201, tarefa);
            }
            catch (TarefaError e)
            {
                return StatusCode(400, new ErrorView { Mensagem = e.Message });
            }

        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] TarefaDto tarefaDto)
        {
            try
            {
                var tarefaDb = _servico.UpdateTarefa(id, tarefaDto);
                return StatusCode(200, tarefaDb);
            }
            catch (TarefaError e)
            {
                return StatusCode(400, new ErrorView { Mensagem = e.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            try
            {
                var tarefa = _servico.GetTarefa(id);
                return StatusCode(200, tarefa);
            }
            catch (TarefaError e)
            {
                return StatusCode(404, new ErrorView { Mensagem = e.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _servico.DeleteTarefa(id);
                return StatusCode(204);
            }
            catch (TarefaError e)
            {
                return StatusCode(404, new ErrorView { Mensagem = e.Message });
            }
            
        }
    }
}