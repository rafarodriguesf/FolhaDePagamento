using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FolhaDePagamento.Models
{
	public class FeriasModel
	{
		public int Id { get; set; }
		public FuncionarioModel funcionario { get; set; }
        public int FuncionarioId { get; set; }

		[Required(ErrorMessage ="Selecione a data de inicio das ferias")]
		public DateTime DataInicio { get; set; }

		[Required(ErrorMessage = "Selecione a data de fim das ferias")]
		public DateTime DataFim { get; set; }
		public decimal ValorFerias { get; set; }
		public DateTime DataSolicitacao { get; set; }

		public string Status { get; set; }
		


	}
}
