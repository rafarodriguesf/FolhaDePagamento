using FolhaDePagamento.Models;
using FolhaDePagamento.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace FolhaDePagamento.Controllers
{
	public class HorasTrabalhadasController : Controller
	{
		public const string SessionKeyUser = "_Usuario";
		public const string SessionKeyId = "_FuncionarioId";

		private readonly IHorasTrabalhadasRepositorio _HorasTrabalhadasRepositorio;

		public HorasTrabalhadasController(IHorasTrabalhadasRepositorio HorasTrabalhadasRepositorio)
		{
			_HorasTrabalhadasRepositorio = HorasTrabalhadasRepositorio;
		}
		public async Task<IActionResult> ListarHorasPorMes(int mes, int ano)
		{
			var funcionarioId = HttpContext.Session.GetInt32(SessionKeyId);

			if (funcionarioId != null)
			{
				var horasDoMes = await _HorasTrabalhadasRepositorio.ListarHorasPorFuncionarioNoMes((int)funcionarioId, mes, ano);
				return View(horasDoMes);
			}

			return RedirectToAction("Index", "Home");
		}
		public async Task<IActionResult> Index(int? mes, int? ano)
		{
			var funcionarioId = HttpContext.Session.GetInt32(SessionKeyId);

			var dataReferencia = new DateTime(ano ?? DateTime.Today.Year, mes ?? DateTime.Today.Month, 1);

			var mesAtual = dataReferencia.Month;
			var anoAtual = dataReferencia.Year;

			var mesAnterior = dataReferencia.AddMonths(-1).Month;
			var anoAnterior = dataReferencia.AddMonths(-1).Year;

			if (mesAtual == 1)
			{
				mesAnterior = 12;
				anoAnterior = dataReferencia.Year - 1;
			}

			var proximoMes = dataReferencia.AddMonths(1).Month;
			var proximoAno = dataReferencia.AddMonths(1).Year;

			if (mesAtual == 12)
			{
				proximoMes = 1;
				proximoAno = dataReferencia.Year + 1;
			}

			var horasFuncionario = await _HorasTrabalhadasRepositorio.ListarHorasPorMesAno((int)funcionarioId, mesAtual, anoAtual);

			ViewBag.MesAnterior = mesAnterior;
			ViewBag.AnoAnterior = anoAnterior;
			ViewBag.MesSeguinte = proximoMes;
			ViewBag.AnoSeguinte = proximoAno;

			return View(horasFuncionario);
		}
		public IActionResult ListarHorasFuncionario()
		{
			var funcionarioId = HttpContext.Session.GetInt32(LoginController.SessionKeyId);
			if (funcionarioId != null)
			{
				var horasDoFuncionario = _HorasTrabalhadasRepositorio.ListarHorasPorFuncionario((int)funcionarioId);	
				return View(horasDoFuncionario);
			}
			else
			{
				return RedirectToAction("Usuario nao encontrado", "Erro");
			}
		}

		


	}
}
