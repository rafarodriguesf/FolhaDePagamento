using FolhaDePagamento.Models;

namespace FolhaDePagamento.Repository
{
	public interface IHorasTrabalhadasRepositorio
	{
		Task<List<HorasTrabalhadasModel>> BuscarTodos();
		Task<HorasTrabalhadasModel> ListarPorId(long id);

		Task<List<HorasTrabalhadasModel>> ListarHorasPorFuncionario(int funcionarioId);

		Task<List<HorasTrabalhadasModel>> ListarHorasPorFuncionarioNoMes(int funcionarioId, int mes, int ano);

		Task<List<HorasTrabalhadasModel>> ListarHorasPorMesAno(int funcionarioId, int mes, int ano);
	}	
}
