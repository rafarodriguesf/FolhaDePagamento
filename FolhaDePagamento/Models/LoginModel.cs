using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FolhaDePagamento.Models
{
	public class LoginModel
	{
		public int Id { get; set; }
		public FuncionarioModel funcionario { get; set; }
        public int FuncionarioId { get; set; }
        
		[Required]
		public string Usuario { get; set; }
		
		
		[Required]
		public string Senha { get; set; }
		
		public int Ativo { get; set; }


	}
}
