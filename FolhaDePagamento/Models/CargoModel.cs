using System.ComponentModel.DataAnnotations;

namespace FolhaDePagamento.Models
{
	public class CargoModel
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string NomeCargo { get; set; }

		[Required]
		public int CargaHoraria { get; set; }

		[Required]
		[DataType(DataType.Currency)]
		[DisplayFormat(DataFormatString = "{0:C2}")]
		public decimal SalarioBase { get; set; }

		[Required]
		[Range(0, 100)]
		public decimal INSS { get; set; }

		public ICollection<FuncionarioModel> Funcionarios { get; set; } = new List<FuncionarioModel>();
	}
}
