using FolhaDePagamento.Data;
using FolhaDePagamento.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FolhaDePagamento.Repository
{
	public class FuncionarioRepositorio : IFuncionarioRepositorio
	{
		private readonly BancoContext _bancoContext;

		public FuncionarioRepositorio(BancoContext bancoContext)
		{
			_bancoContext = bancoContext;
		}

		public async Task<List<FuncionarioModel>> BuscarTodos()
		{
			return await _bancoContext.Funcionario.ToListAsync();
		}

		public async Task<FuncionarioModel> ListarPorId(long id)
		{
			Task<FuncionarioModel> funcionarioDB;

			try
			{
				funcionarioDB = _bancoContext.Funcionario.FirstOrDefaultAsync(x => x.Id == id);
			}
			catch (System.Exception e)
			{
				throw new System.Exception($"{e.Message}Houve um erro na busca do funcionario");
			}

			return await funcionarioDB;
		}
		public async Task<FuncionarioModel> ObterPorId(int funcionarioId)
		{
			var funcionario = await _bancoContext.Funcionario
				.FirstOrDefaultAsync(f => f.Id == funcionarioId);

			return funcionario;
		}





	}
}
