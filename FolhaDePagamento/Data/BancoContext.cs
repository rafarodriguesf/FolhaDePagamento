using FolhaDePagamento.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Transactions;

namespace FolhaDePagamento.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<FuncionarioModel>()
				.HasOne(x => x.Cargo)
				.WithMany(y => y.Funcionarios)
				.HasForeignKey(x => x.CargoId);

			modelBuilder.Entity<FeriasModel>()
				.HasOne(x => x.funcionario)
				.WithMany(y => y.Ferias)
				.HasForeignKey(f => f.FuncionarioId);

		}

		public DbSet<LoginModel> Login { get; set; }
        public DbSet<FuncionarioModel> Funcionario { get; set; }
        public DbSet<CargoModel> Cargo { get; set; }
        public DbSet<HorasTrabalhadasModel> HorasTrabalhadas { get; set; }
        public DbSet<FeriasModel> Ferias { get; set; }
        public DbSet<PagamentoModel> Pagamento { get; set; }
        
	}
}
