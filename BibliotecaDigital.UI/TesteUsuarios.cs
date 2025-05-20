using BibliotecaDigital.Controller;
using BibliotecaDigital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaDigital.UI
{
    public class TesteUsuarios
    {
        public static void Executar()
        {
            var controller = new UsuariosController();

            // Criando um novo usuário
            var novoUsuario = new Usuarios
            {
                Id_Usuario = 1, // ajuste conforme necessário
                Nome_Usuario = "Pedro Lucas",
                Email_Usuario = "pedro@example.com",
                DtNasc_Usuario = new DateOnly(2005, 1, 28),
                senha = "senhaSegura123"
            };

            Console.WriteLine("Cadastrando novo usuário...");
            controller.CadastrarUsuario(novoUsuario);
            Console.WriteLine("Usuário cadastrado com sucesso!\n");

            // Listando todos os usuários
            Console.WriteLine("Lista de todos os usuários:");
            var usuarios = controller.ObterTodosUsuarios();
            foreach (var usuario in usuarios)
            {
                Console.WriteLine($"ID: {usuario.Id_Usuario}, Nome: {usuario.Nome_Usuario}, Email: {usuario.Email_Usuario}, Nascimento: {usuario.DtNasc_Usuario}, Senha: {usuario.senha}");
            }

            // Buscando um usuário por ID
            Console.WriteLine("\nBuscando usuário com ID 1...");
            var usuarioBuscado = controller.ObterUsuarioPorId(1);
            if (usuarioBuscado != null)
            {
                Console.WriteLine($"Encontrado: {usuarioBuscado.Nome_Usuario} ({usuarioBuscado.Email_Usuario})");
            }
            else
            {
                Console.WriteLine("Usuário não encontrado.");
            }

            // Deletando usuário (opcional)
            Console.WriteLine("\nDeseja deletar o usuário inserido? (s/n)");
            var opcao = Console.ReadLine();
            if (opcao?.ToLower() == "s")
            {
                controller.RemoverUsuario(1);
                Console.WriteLine("Usuário removido com sucesso!");
            }
        }
    }
}
