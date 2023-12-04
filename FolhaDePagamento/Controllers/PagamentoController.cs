using FolhaDePagamento.Models;
using FolhaDePagamento.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace FolhaDePagamento.Controllers
{
	public class PagamentoController : Controller
	{
		public const string SessionKeyUser = "_Usuario";
		public const string SessionKeyId = "_FuncionarioId";

		private readonly IPagamentoRepositorio _PagamentoRepositorio;

		public PagamentoController(IPagamentoRepositorio PagamentoRepositorio)
		{
            _PagamentoRepositorio = PagamentoRepositorio;
		}

		public async Task<IActionResult> Index()
		{
			var funcionarioId = HttpContext.Session.GetInt32(SessionKeyId);
			
			if (funcionarioId != null)
			{
				List<PagamentoModel> pagamentos = await _PagamentoRepositorio.BuscarPagamentoPorFuncionario((int)funcionarioId);
				return View(pagamentos);
			}
			else
			{
				return RedirectToAction("Index", "Login");
			}
		}
       
    }
}
