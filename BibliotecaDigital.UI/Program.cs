using BibliotecaDigital.Controller;
using BibliotecaDigital.Model;
using BibliotecaDigital.UI;
using System;

class Program
{
    static void Main(string[] args)
    {
        UsuariosController controller = new UsuariosController();

        //TesteUsuarios.Executar();
        Console.WriteLine("\n---- Teste de Login ----");
        Console.Write("Digite seu e-mail: ");
        string email = Console.ReadLine();

        Console.Write("Digite sua senha: ");
        string senha = Console.ReadLine();

        var usuarioLogado = controller.Login(email, senha);
        if (usuarioLogado != null)
        {
            Console.WriteLine($"Bem-vindo, {usuarioLogado.Nome_Usuario}!");
        }
        else
        {
            Console.WriteLine("Login inválido.");
        }

    }
}