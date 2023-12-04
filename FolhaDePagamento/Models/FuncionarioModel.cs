using System.ComponentModel.DataAnnotations;

namespace FolhaDePagamento.Models
{
	public class FuncionarioModel
	{
		public int Id { get; set; }

		public int CargoId { get; set; }
		public CargoModel Cargo { get; set; }

		[Required]
		[MaxLength(50)]
		public string Nome { get; set; }

		[Required]
		[MaxLength(15)]
		public string CPF { get; set; }

		[Required]
		public DateTime DataNasc { get; set; }

		[MaxLength(20)]
		public string Telefone { get; set; }

		[MaxLength(50)]
		public string Email { get; set; }

		public DateTime? DataAdmissao { get; set; }

		[Required]
		[MaxLength(50)]
		public string Instituicao { get; set; }

		[Required]
		public int Agencia { get; set; }

		[Required]
		[MaxLength(50)]
		public string ContaCorrente { get; set; }


		public ICollection<LoginModel> Logins { get; set; } = new List<LoginModel>();
		public ICollection<FeriasModel> Ferias { get; set; } = new List<FeriasModel>();
		public ICollection<PagamentoModel> Pagamentos { get; set; } = new List<PagamentoModel>();
	}
}
