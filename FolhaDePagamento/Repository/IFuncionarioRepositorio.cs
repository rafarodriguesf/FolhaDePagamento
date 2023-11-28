using FolhaDePagamento.Models;

namespace FolhaDePagamento.Repository
{
	public interface IFuncionarioRepositorio
	{
		Task<List<FuncionarioModel>> BuscarTodos();
		Task<FuncionarioModel> ListarPorId(long id);

		Task<FuncionarioModel> ObterPorId(int funcionarioId);
	}
}
