using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FolhaDePagamento.Models
{
	public class PagamentoModel
	{
		public int Id { get; set; }
		public FuncionarioModel funcionario { get; set; }
        public int FuncionarioId { get; set; }

		public DateTime DataPagamento { get; set; }

		public decimal ValorPago { get; set; }


	}
}
