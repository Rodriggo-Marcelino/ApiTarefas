using ApiTarefas.Database;
using ApiTarefas.Dto;
using ApiTarefas.Models;
using ApiTarefas.Models.Erros;
using ApiTarefas.Services.Interfaces;

namespace ApiTarefas.Services;
public class TarefaServico : ITarefaServico
{
    public TarefaServico(TarefasContext db)
    {
        _db = db;
    }

    private readonly TarefasContext _db;

    public List<Tarefa> GetTarefas(int page = 1)
    {
        if (page < 1) page = 1;
        int pageSize = 10;
        int offset = (page - 1) * pageSize;
        return [.. _db.Tarefas.Skip(offset).Take(pageSize)];
    }

    public Tarefa CreateTarefa(TarefaDto tarefaDto)
    {
        if (string.IsNullOrEmpty(tarefaDto.Titulo))
            throw new TarefaError("O título da tarefa é obrigatório!");
        var tarefa = new Tarefa
        {
            Titulo = tarefaDto.Titulo,
            Descricao = tarefaDto.Descricao,
            Concluida = tarefaDto.Concluida
        };
        _db.Tarefas.Add(tarefa);
        _db.SaveChanges();
        return tarefa;
    }

    public Tarefa UpdateTarefa(int id, TarefaDto tarefaDto)
    {
        if (string.IsNullOrEmpty(tarefaDto.Titulo))
            throw new TarefaError("O título da tarefa é obrigatório!");

        var tarefaDb = _db.Tarefas.Find(id) ?? throw new TarefaError("Tarefa não encontrada!");

        tarefaDb.Titulo = tarefaDto.Titulo;
        tarefaDb.Descricao = tarefaDto.Descricao;
        tarefaDb.Concluida = tarefaDto.Concluida;

        _db.Tarefas.Update(tarefaDb);
        _db.SaveChanges();
        return tarefaDb;
    }

    public void DeleteTarefa(int id)
    {
        var tarefaDb = _db.Tarefas.Find(id);

        if (tarefaDb == null)
            throw new TarefaError("Tarefa não encontrada!");

        _db.Tarefas.Remove(tarefaDb);
        _db.SaveChanges();
    }

    public Tarefa GetTarefa(int id)
    {
        var tarefa = _db.Tarefas.Find(id) ?? throw new TarefaError("Tarefa não encontrada!");
        return tarefa;
    }

}
