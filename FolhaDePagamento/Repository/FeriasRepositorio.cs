using FolhaDePagamento.Data;
using FolhaDePagamento.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FolhaDePagamento.Repository
{
	public class FeriasRepositorio : IFeriasRepositorio
	{
		private readonly BancoContext _bancoContext;

		public FeriasRepositorio(BancoContext bancoContext)
		{
			_bancoContext = bancoContext;
		}

		public async Task<List<FeriasModel>> BuscarTodos()
		{
			return await _bancoContext.Ferias.ToListAsync();
		}

		public async Task<FeriasModel> ListarPorId(long id)
		{
			Task<FeriasModel> feriasBD;

			try
			{
				feriasBD = _bancoContext.Ferias.FirstOrDefaultAsync(x => x.Id == id);
			}
			catch (System.Exception e)
			{
				throw new System.Exception($"{e.Message}Houve um erro na busca das Horas");
			}

			return await feriasBD;
		}

		public async Task<List<FeriasModel>> BuscarFeriasPorFuncionario(int funcionarioId)
		{

			var feriasdofunc = _bancoContext.Ferias
				.Where(f => f.FuncionarioId == funcionarioId)
				.ToList();

			return feriasdofunc;
		}
        public async Task<FeriasModel> Adicionar(FeriasModel ferias)	
        {
            await _bancoContext.Ferias.AddAsync(ferias);
            await _bancoContext.SaveChangesAsync();

            return ferias;
        }

		public async Task<List<FeriasModel>> BuscarFeriasPendentes(int funcionarioId)
		{
			var feriasPendentes = await _bancoContext.Ferias
				.Where(f => f.FuncionarioId == funcionarioId && f.Status == "Pendente")
				.ToListAsync();

			return feriasPendentes;
		}

		public async Task<int> ObterFeriasAprovadasPorFuncionario(int funcionarioId)
		{
			// Consulta as férias do funcionário com status "Aprovado"
			var feriasAprovadas = await _bancoContext.Ferias
				.Where(f => f.FuncionarioId == funcionarioId && f.Status == "Aprovado")
				.CountAsync();

			return feriasAprovadas;
		}

	}
}
