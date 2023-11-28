using FolhaDePagamento.Data;
using FolhaDePagamento.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FolhaDePagamento.Repository
{
	public class HorasTrabalhadasRepositorio : IHorasTrabalhadasRepositorio
	{
		private readonly BancoContext _bancoContext;

		public HorasTrabalhadasRepositorio(BancoContext bancoContext)
		{
			_bancoContext = bancoContext;
		}

		public async Task<List<HorasTrabalhadasModel>> BuscarTodos()
		{
			return await _bancoContext.HorasTrabalhadas.ToListAsync();
		}

		public async Task<HorasTrabalhadasModel> ListarPorId(long id)
		{
			Task<HorasTrabalhadasModel> horasDB;

			try
			{
				horasDB = _bancoContext.HorasTrabalhadas.FirstOrDefaultAsync(x => x.Id == id);
			}
			catch (System.Exception e)
			{
				throw new System.Exception($"{e.Message}Houve um erro na busca das Horas");
			}

			return await horasDB;
		}

		public async Task<List<HorasTrabalhadasModel>> ListarHorasPorFuncionario(int funcionarioId)
		{

			// Filtra as horas com base no ID do funcionário
			var horasDoFuncionario = _bancoContext.HorasTrabalhadas
				.Where(h => h.FuncionarioId == funcionarioId)
				.ToList();

			return horasDoFuncionario;
		}
		public async Task<List<HorasTrabalhadasModel>> ListarHorasPorFuncionarioNoMes(int funcionarioId, int mes, int ano)
		{
			var horasDoFuncionarioNoMes = _bancoContext.HorasTrabalhadas
				.Where(h => h.FuncionarioId == funcionarioId &&
							h.DataHorasTrabalhadas.Month == mes &&
							h.DataHorasTrabalhadas.Year == ano)
				.ToList();

			return horasDoFuncionarioNoMes;
		}

		public async Task<List<HorasTrabalhadasModel>> ListarHorasPorMesAno(int funcionarioId, int mes, int ano)
		{
			// Aqui você pode filtrar as horas do funcionário com base no mês e ano fornecidos
			var horasDoFuncionario = await _bancoContext.HorasTrabalhadas
				.Where(h => h.FuncionarioId == funcionarioId && h.DataHorasTrabalhadas.Month == mes && h.DataHorasTrabalhadas.Year == ano)
				.ToListAsync();

			return horasDoFuncionario;
		}



	}
}
