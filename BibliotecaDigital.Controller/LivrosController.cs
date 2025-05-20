using BibliotecaDigital.Model;
using BibliotecaDigital.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaDigital.Controller
{
    public class LivrosController
    {
        private readonly LivrosRepository _repository;

        public LivrosController()
        {
            _repository = new LivrosRepository();
        }

        public void CadastrarLivro(Livros livro)
        {
            _repository.Inserir(livro);
        }

        public Livros ObterLivroPorId(int id)
        {
            return _repository.BuscarPorId(id);
        }

        public List<Livros> ObterTodosLivros()
        {
            return _repository.ListarTodos();
        }

        public void RemoverLivro(int id)
        {
            _repository.Deletar(id);
        }
    }
}
