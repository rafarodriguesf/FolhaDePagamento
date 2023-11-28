using FolhaDePagamento.Data;
using FolhaDePagamento.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FolhaDePagamento.Repository
{
	public class LoginRepositorio : ILoginRepositorio
	{
		private readonly BancoContext _bancoContext;

		public LoginRepositorio(BancoContext bancoContext)
		{
			_bancoContext = bancoContext;
		}

		public async Task<List<LoginModel>> BuscarTodos()
		{
			return await _bancoContext.Login.ToListAsync();
		}

		public async Task<LoginModel> Adicionar(LoginModel login)
		{
			await _bancoContext.Login.AddAsync(login);
			await _bancoContext.SaveChangesAsync();
			return await Task.FromResult(login);
		}
		
		public async Task<LoginModel> ListarPorId(long id)
		{
			return await _bancoContext.Login.FirstOrDefaultAsync(l =>
			l.Id == id);
		}

		public async Task<LoginModel> ListarPorUsuario(string usuario)
		{
			return await _bancoContext.Login.FirstOrDefaultAsync(l => l.Usuario == usuario);
		}

		public async Task<LoginModel> ListarPorUsuarioSenha(string usuario, string senha)
		{
			return await _bancoContext.Login.FirstOrDefaultAsync(l => l.Usuario == usuario && l.Senha == senha);
		}

		public async Task<string> ObterNomeFuncionarioPorLoginId(int loginId)
		{
			var login = await _bancoContext.Login.FindAsync(loginId);

			if (login != null)
			{
				var funcionario = await _bancoContext.Funcionario.FindAsync(login.FuncionarioId);
				return funcionario?.Nome;
			}

			return null;
		}




		public async Task<LoginModel> Atualizar(LoginModel login)
		{
			LoginModel loginDB = await _bancoContext.Login.FirstOrDefaultAsync(x => x.Id == login.Id);

			if (loginDB == null) throw new System.Exception("Houve um erro na atualização do login");

			loginDB.Usuario = login.Usuario;
			loginDB.Senha = login.Senha;
			loginDB.Ativo = login.Ativo;
			loginDB.FuncionarioId = login.FuncionarioId;

			_bancoContext.Login.Update(loginDB);
			await _bancoContext.SaveChangesAsync();

			return await Task.FromResult(loginDB);
		}

		public async Task<LoginModel> AtualizarUsuario(LoginModel login)
		{
			LoginModel loginDB = await ListarPorId(login.Id);

			if (loginDB == null) throw new System.Exception("Houve um erro na atualização do login");

			loginDB.Usuario = login.Usuario;
			loginDB.Senha = login.Senha;
			loginDB.Ativo = login.Ativo;
			loginDB.FuncionarioId = login.FuncionarioId;
			_bancoContext.Login.Update(loginDB);
			await _bancoContext.SaveChangesAsync();

			return await Task.FromResult(loginDB);
		}

	}
}
