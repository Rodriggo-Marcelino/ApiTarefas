namespace ApiTarefas.Models.Erros;

public class TarefaError(string message) : Exception(message)
{
}
