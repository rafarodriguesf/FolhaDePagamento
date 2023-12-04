using FolhaDePagamento.Models;

namespace FolhaDePagamento.Repository
{
	public interface IPagamentoRepositorio
    {
		Task<List<PagamentoModel>> BuscarTodos();
		Task<List<PagamentoModel>> BuscarPagamentoPorFuncionario(int funcionarioId);

        Task<PagamentoModel> ListarPorId(long id);
	}
}
