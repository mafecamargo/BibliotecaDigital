using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDigital.Model;
using BibliotecaDigital.Repository;

namespace BibliotecaDigital.Controller
{
    public class UsuariosController
    {
        private readonly UsuariosRepository _repository;

        public UsuariosController()
        {
            _repository = new UsuariosRepository();
        }

        public void CadastrarUsuario(Usuarios usuario)
        {
            _repository.Inserir(usuario);
        }

        public Usuarios ObterUsuarioPorId(int id)
        {
            return _repository.BuscarPorId(id);
        }

        public List<Usuarios> ObterTodosUsuarios()
        {
            return _repository.ListarTodos();
        }

        public void RemoverUsuario(int id)
        {
            _repository.Deletar(id);
        }

        public Usuarios Login(string email, string senha)
        {
            var usuario = _repository.BuscarPorEmail(email);
            if (usuario != null && usuario.senha == senha)
            {
                return usuario;
            }

            return null; 
        }


    }
}
