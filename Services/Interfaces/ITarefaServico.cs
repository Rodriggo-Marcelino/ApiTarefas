using ApiTarefas.Database;
using ApiTarefas.Dto;
using ApiTarefas.Models;
using ApiTarefas.Models.Erros;

namespace ApiTarefas.Services.Interfaces
{
    public interface ITarefaServico
    {
        List<Tarefa> GetTarefas(int page);
        Tarefa CreateTarefa(TarefaDto tarefaDto);
        Tarefa UpdateTarefa(int id, TarefaDto tarefaDto);
        void DeleteTarefa(int id);
        Tarefa GetTarefa(int id);
    }
}