using FolhaDePagamento.Models;

namespace FolhaDePagamento.Repository
{
	public interface ILoginRepositorio
	{
		Task<List<LoginModel>> BuscarTodos();
		Task<LoginModel> Adicionar(LoginModel login);
		Task<LoginModel> ListarPorId(long id);
		Task<LoginModel> ListarPorUsuario(string usuario);
		Task<LoginModel> ListarPorUsuarioSenha(string usuario, string senha);
		Task<string> ObterNomeFuncionarioPorLoginId(int loginId);
		Task<LoginModel> Atualizar(LoginModel login);
		Task<LoginModel> AtualizarUsuario(LoginModel login);
	}
}
