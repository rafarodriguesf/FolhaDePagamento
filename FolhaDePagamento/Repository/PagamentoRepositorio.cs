using FolhaDePagamento.Data;
using FolhaDePagamento.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FolhaDePagamento.Repository
{
	public class PagamentoRepositorio : IPagamentoRepositorio
	{
		private readonly BancoContext _bancoContext;

		public PagamentoRepositorio(BancoContext bancoContext)
		{
			_bancoContext = bancoContext;
		}

		public async Task<List<PagamentoModel>> BuscarTodos()
		{
			return await _bancoContext.Pagamento.ToListAsync();
		}

		public async Task<PagamentoModel> ListarPorId(long id)
		{
			Task<PagamentoModel> pagamentoDB;

			try
			{
				pagamentoDB = _bancoContext.Pagamento.FirstOrDefaultAsync(x => x.Id == id);
			}
			catch (System.Exception e)
			{
				throw new System.Exception($"{e.Message}Houve um erro na busca das Horas");
			}

			return await pagamentoDB;
		}

		public async Task<List<PagamentoModel>> BuscarPagamentoPorFuncionario(int funcionarioId)
		{

			var pagamentodofunc = _bancoContext.Pagamento
				.Where(f => f.FuncionarioId == funcionarioId)
				.ToList();

			return pagamentodofunc;
		}
		

	}
}
