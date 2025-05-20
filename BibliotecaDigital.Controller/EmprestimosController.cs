using BibliotecaDigital.Model;
using BibliotecaDigital.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaDigital.Controller
{
    public class EmprestimosController
    {
        private readonly EmprestimosRepository _repository;

        public EmprestimosController()
        {
            _repository = new EmprestimosRepository();
        }

        public void CadastrarEmprestimo(Emprestimos emprestimo)
        {
            _repository.Inserir(emprestimo);
        }

        public List<Emprestimos> ListarEmprestimos()
        {
            return _repository.ListarTodos();
        }

        public void RemoverEmprestimo(int id)
        {
            _repository.Deletar(id);
        }

        public List<Emprestimos> ListarEmprestimosPorUsuario(int idUsuario)
        {
            return _repository.ListarPorUsuario(idUsuario);
        }
    }
}
