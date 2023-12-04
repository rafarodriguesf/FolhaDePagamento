using FolhaDePagamento.Models;
using FolhaDePagamento.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FolhaDePagamento.Controllers
{
	public class FuncionarioController : Controller
	{
		public const string SessionKeyUser = "_Usuario";
		public const string SessionKeyId = "_FuncionarioId";
		private readonly IFuncionarioRepositorio _FuncionarioRepositorio;

		public FuncionarioController(IFuncionarioRepositorio FuncionarioRepositorio)
		{
			_FuncionarioRepositorio = FuncionarioRepositorio;
		}
		public async Task<IActionResult> Index()
		{
			var funcionarioId = HttpContext.Session.GetInt32(SessionKeyId);
			var func = await _FuncionarioRepositorio.ObterPorId((int)funcionarioId);
			var detalheFuncionario = new List<FuncionarioModel> { func };
			return View(detalheFuncionario);
		}
	}
}
