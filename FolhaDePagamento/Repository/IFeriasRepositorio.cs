using FolhaDePagamento.Models;

namespace FolhaDePagamento.Repository
{
	public interface IFeriasRepositorio
	{
		Task<List<FeriasModel>> BuscarTodos();
		Task<List<FeriasModel>> BuscarFeriasPorFuncionario(int funcionarioId);
		Task<List<FeriasModel>> BuscarFeriasPendentes(int funcionarioId);

		Task<FeriasModel> Adicionar(FeriasModel ferias);
        Task<FeriasModel> ListarPorId(long id);

		Task<int> ObterFeriasAprovadasPorFuncionario(int funcionarioId);
	}
}
