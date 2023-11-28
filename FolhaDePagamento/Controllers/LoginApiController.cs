using FolhaDePagamento.Models;
using FolhaDePagamento.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace FolhaDePagamento.Controllers
{
	[Route("api/Login")]
	[ApiController]
	public class LoginApiController : Controller
	{
		private readonly ILoginRepositorio _loginRepositorio;
		
		public LoginApiController(ILoginRepositorio loginRepositorio)
		{
			_loginRepositorio = loginRepositorio;
		}

		// Get: api/Login
		[HttpGet]
		async public Task<ActionResult<LoginModel>> Logar(string? usuario, string? senha)
		{
			LoginModel loginDB = await _loginRepositorio.ListarPorUsuario(usuario!);
			var sucesso = false;

			if (senha != null)
				sucesso = true;

			if (sucesso)
			{
				return await Task.FromResult(loginDB);
			}
			return await Task.FromResult(BadRequest());
		}

		public class LoginUsuarioModel
		{
			public string? usuario { get; set; }
			public string senha { get; set; }	
			
		}

		// Post: api/Login
		[EnableCors("MyPolicy")]
		[HttpPost]
		async public Task<ActionResult<LoginModel>> LogarPost(LoginUsuarioModel loginUsuarioModel)
		{
			LoginModel loginDB = await _loginRepositorio.ListarPorUsuario(loginUsuarioModel.usuario);
			var sucesso = false;

			if (loginUsuarioModel.senha != null)
				sucesso = true;

			if (sucesso)
			{
				return await Task.FromResult(loginDB);
			}
			return await Task.FromResult(BadRequest());
		}

		


	}
}
