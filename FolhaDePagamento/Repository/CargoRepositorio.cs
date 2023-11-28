using FolhaDePagamento.Data;
using FolhaDePagamento.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FolhaDePagamento.Repository
{
	public class CargoRepositorio : ICargoRepositorio
	{
		private readonly BancoContext _bancoContext;

		public CargoRepositorio(BancoContext bancoContext)
		{
			_bancoContext = bancoContext;
		}

		public async Task<List<CargoModel>> BuscarTodos()
		{
			return await _bancoContext.Cargo.ToListAsync();
		}

		public async Task<CargoModel> ListarPorId(long id)
		{
			Task<CargoModel> cargoDB;

			try
			{
				cargoDB = _bancoContext.Cargo.FirstOrDefaultAsync(x => x.Id == id);
			}
			catch (System.Exception e)
			{
				throw new System.Exception($"{e.Message}Houve um erro na busca do funcionario");
			}

			return await cargoDB;
		}
		public async Task<CargoModel> ObterPorId(int cargoId)
		{
			var cargo = await _bancoContext.Cargo
				.FirstOrDefaultAsync(f => f.Id == cargoId);

			return cargo;
		}

	}
}
