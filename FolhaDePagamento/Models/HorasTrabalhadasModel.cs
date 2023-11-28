using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FolhaDePagamento.Models
{
	public class HorasTrabalhadasModel
	{
		public int Id { get; set; }
		public FuncionarioModel funcionario { get; set; }
        public int FuncionarioId { get; set; }
		public DateTime DataHorasTrabalhadas { get; set; }

		[Required]
		public TimeSpan HoraEntrada { get; set; }
		
		[Required]
		public TimeSpan HoraSaida { get; set; }
		


	}
}
