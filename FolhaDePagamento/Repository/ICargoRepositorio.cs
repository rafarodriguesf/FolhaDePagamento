using FolhaDePagamento.Models;

namespace FolhaDePagamento.Repository
{
	public interface ICargoRepositorio
	{
		Task<List<CargoModel>> BuscarTodos();
		Task<CargoModel> ListarPorId(long id);

		Task<CargoModel> ObterPorId(int cargoId);
	}
}
