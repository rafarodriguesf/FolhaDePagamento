using FolhaDePagamento.Data;
using FolhaDePagamento.Models;
using FolhaDePagamento.Repository;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace FolhaDePagamento.Controllers
{
	public class LoginController : Controller
	{
		private readonly ILoginRepositorio _loginRepositorio;
		public const string SessionKeyUser = "_Usuario"; 
		public const string SessionKeyId = "_FuncionarioId";
		

		public LoginController(ILoginRepositorio loginRepositorio)
		{
			_loginRepositorio = loginRepositorio;
		}

		async public Task<IActionResult> Index()
		{

			return await Task.FromResult(View());
		}

		async public Task<IActionResult> Editar(long id)
		{
			LoginModel login = await _loginRepositorio.ListarPorId(id);

			return await Task.FromResult(View(login));
		}

		public async Task<IActionResult> ApagarConfirmacao(long id)
		{
			LoginModel login = await _loginRepositorio.ListarPorId(id);
			
			return await Task.FromResult(View(login));
		}

		async public Task<IActionResult> Registro()
		{

			return await Task.FromResult(View());
		}


		[HttpPost]
		async public Task<IActionResult> Index(string usuario, string senha)
		{
			LoginModel loginDB = await _loginRepositorio.ListarPorUsuario(usuario);
			var sucesso = false;

			if (loginDB == null)
			{
				return RedirectToAction("UsuarioNaoEncontrado", "Erro");
			}

			if (senha != null && loginDB != null)
				sucesso = true;

			if (sucesso)
			{
				var nomeFuncionario = await _loginRepositorio.ObterNomeFuncionarioPorLoginId(loginDB.Id);
				ViewBag.NomeFuncionario = nomeFuncionario;
				HttpContext.Session.SetString(SessionKeyUser, nomeFuncionario);
				HttpContext.Session.SetInt32(SessionKeyId, (int)loginDB.FuncionarioId);

				return await Task.FromResult(RedirectToAction("Index", "Home"));
			}

			return await Task.FromResult(View());
		}

		[HttpPost]
		async public Task<IActionResult> Registro(LoginModel login, string? senha)
		{
			LoginModel loginDB = login;

			loginDB.Senha = senha;
			loginDB.Ativo = 1;

			await _loginRepositorio.Adicionar(loginDB);

			return await Task.FromResult(RedirectToAction("Index", "Login"));
		}

		[HttpPost]
		public async Task<IActionResult> Alterar(LoginModel login, string senhaAtual, string novaSenha)
		{
			LoginModel loginDB = await _loginRepositorio.ListarPorId(login.Id);

			var sucesso = true;
			if (sucesso)
			{
				loginDB.Usuario = login.Usuario;
				loginDB.Senha = novaSenha;
				await _loginRepositorio.Atualizar(loginDB);
				return await Task.FromResult(RedirectToAction("Sair", "Home"));
			}

			return await Task.FromResult(View());
		}



	}
}
