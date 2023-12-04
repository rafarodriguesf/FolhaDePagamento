using FolhaDePagamento.Models;
using FolhaDePagamento.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace FolhaDePagamento.Controllers
{
	public class FeriasController : Controller
	{
		public const string SessionKeyUser = "_Usuario";
		public const string SessionKeyId = "_FuncionarioId";

		private readonly IFeriasRepositorio _FeriasRepositorio;
		private readonly IFuncionarioRepositorio _FuncionariosRepositorio;
		private readonly ICargoRepositorio _CargoRepositorio;

		public FeriasController(IFeriasRepositorio FeriasRepositorio, IFuncionarioRepositorio FuncionarioRepositorio, ICargoRepositorio CargoRepositorio)
		{
			_FeriasRepositorio = FeriasRepositorio;
			_FuncionariosRepositorio = FuncionarioRepositorio;
			_CargoRepositorio = CargoRepositorio;
		}

		public async Task<IActionResult> Index()
		{
			var funcionarioId = HttpContext.Session.GetInt32(SessionKeyId);
			
			if (funcionarioId != null)
			{
				List<FeriasModel> ferias = await _FeriasRepositorio.BuscarFeriasPorFuncionario((int)funcionarioId);
				return View(ferias);
			}
			else
			{
				return RedirectToAction("Index", "Login");
			}
		}
        public async Task<IActionResult> Criar()
        {
            return await Task.FromResult(View());
        }

        [HttpPost]
        public async Task<IActionResult> Criar(FeriasModel ferias, DateTime datainicio, DateTime datafim)
        {
            FeriasModel model = ferias;

            List<ValidationResult> results = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(model, null, null);

            bool isValid = Validator.TryValidateObject(model, context, results, true);
			var funcionarioId = HttpContext.Session.GetInt32(LoginController.SessionKeyId);

			if (!isValid)
            {
                foreach (var validationResult in results)
                {
                    return View(model);
                }
            }

			var funcionario = await _FuncionariosRepositorio.ObterPorId((int)funcionarioId);
			var feriasPendentes = await _FeriasRepositorio.BuscarFeriasPendentes((int)funcionarioId);
			if (feriasPendentes.Any())
			{
				ModelState.AddModelError("", "Você já possui uma solicitação de férias pendente.");
				return View(ferias); // Retorna a view com o erro
			}

            DateTime hoje = DateTime.Today;
            if (hoje > datainicio)
            {
                ModelState.AddModelError("", "A data de início das férias deve ser posterior à data de hoje.");
                return View(ferias); // Retorna a view com o erro
            }

            var dataAdmissao = funcionario.DataAdmissao.Value.Date;
			var mesesDesdeAdmissaoAteFerias = ((datainicio.Year - dataAdmissao.Year) * 12) + datainicio.Month - dataAdmissao.Month;
			if (mesesDesdeAdmissaoAteFerias < 12)
			{
				ModelState.AddModelError("", "Você ainda não completou 12 meses desde a admissão para solicitar 30 dias de férias.");
				return View(ferias); // Retorna a view com o erro
			}

			if (datainicio > datafim)
			{
				ModelState.AddModelError("DataInicio", "A data de início das férias deve ser anterior à data de fim.");
				return View(ferias); // Retorna a view com o erro
			}

			var totalDiasFerias = (datafim - datainicio).Days;
			if (totalDiasFerias != 30)
			{
				ModelState.AddModelError("", $"As férias devem ser exatamente de 30 dias. Sugestão: {datainicio.ToShortDateString()} a {datainicio.AddDays(30).ToShortDateString()}");
				return View(ferias); // Retorna a view com o erro
			}

			
			int anos = hoje.Year - dataAdmissao.Year;
			if (hoje.Month < dataAdmissao.Month || (hoje.Month == dataAdmissao.Month && hoje.Day < dataAdmissao.Day))
			{
				anos--;
			}

			anos = Math.Max(0, anos);
			var feriasAprovadas = await _FeriasRepositorio.ObterFeriasAprovadasPorFuncionario((int)funcionarioId);
			var feriasDisponiveis = anos - feriasAprovadas;

			if (feriasDisponiveis > 0)
			{
				var cargoid = funcionario.CargoId;
				var cargo = await _CargoRepositorio.ObterPorId(cargoid);

				if (cargo != null)
				{
					model.ValorFerias = cargo.SalarioBase;
				}

				model.FuncionarioId = Convert.ToInt32(funcionarioId);
				model.DataSolicitacao = DateTime.Now;
				model.Status = "Pendente";
				model.DataInicio = datainicio;
				model.DataFim = datafim;

				model = await _FeriasRepositorio.Adicionar(model);

				return await Task.FromResult(RedirectToAction("Index"));
			}
			else
			{
				
				ModelState.AddModelError("", "Você não possui férias disponíveis para solicitar.");
				return View(ferias);
			}
        }
    }
}
