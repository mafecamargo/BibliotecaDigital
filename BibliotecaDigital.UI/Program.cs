using BibliotecaDigital.Controller;
using BibliotecaDigital.Model;
using System;

class Program
{
    static void Main(string[] args)
    {
        UsuariosController controller = new UsuariosController();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Biblioteca Digital ===");
            Console.WriteLine("1 - Fazer Login");
            Console.WriteLine("2 - Cadastrar Novo Usuário");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("--- Login ---");
                    Console.Write("Digite seu e-mail: ");
                    string emailLogin = Console.ReadLine();

                    Console.Write("Digite sua senha: ");
                    string senhaLogin = Console.ReadLine();

                    var usuarioLogado = controller.Login(emailLogin, senhaLogin);
                    if (usuarioLogado != null)
                    {
                        Console.Clear();
                        Console.WriteLine($"Bem-vindo, {usuarioLogado.Nome_Usuario}!");
                        MenuUsuarioLogado(usuarioLogado);
                    }
                    else
                    {
                        Console.WriteLine("Login inválido.");
                        Console.ReadKey();
                    }
                    break;

                case "2":
                    Console.Clear();
                    Console.WriteLine("--- Cadastro ---");
                    Console.Write("Nome: ");
                    string nome = Console.ReadLine();

                    Console.Write("E-mail: ");
                    string email = Console.ReadLine();

                    Console.Write("Data de Nascimento (dd/mm/aaaa): ");
                    DateTime dtNasc;
                    while (!DateTime.TryParse(Console.ReadLine(), out dtNasc))
                    {
                        Console.Write("Data inválida. Digite novamente (dd/mm/aaaa): ");
                    }

                    Console.Write("Senha: ");
                    string senha = Console.ReadLine();

                    var novoUsuario = new Usuarios
                    {
                        Nome_Usuario = nome,
                        Email_Usuario = email,
                        DtNasc_Usuario = DateOnly.FromDateTime(dtNasc),
                        senha = senha
                    };

                    controller.CadastrarUsuario(novoUsuario);
                    Console.WriteLine("Usuário cadastrado com sucesso!");
                    Console.ReadKey();
                    break;

                case "0":
                    Console.WriteLine("Saindo...");
                    return;

                default:
                    Console.WriteLine("Opção inválida.");
                    Console.ReadKey();
                    break;
            }
        }

        static void MenuUsuarioLogado(Usuarios usuario)
        {
            LivrosController livrosController = new LivrosController();
            EmprestimosController emprestimosController = new EmprestimosController();
            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.WriteLine($"=== Menu do Usuário: {usuario.Nome_Usuario} ===");
                Console.WriteLine("1 - Listar Livros");
                Console.WriteLine("2 - Cadastrar Novo Livro");
                Console.WriteLine("3 - Realizar Empréstimo");
                Console.WriteLine("4 - Recomendações");
                Console.WriteLine("5 - Meus Empréstimos");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("--- Lista de Livros ---");

                        var livros = livrosController.ObterTodosLivros();

                        if (livros.Count == 0)
                        {
                            Console.WriteLine("Nenhum livro encontrado.");
                        }
                        else
                        {
                            foreach (var livro in livros)
                            {
                                Console.WriteLine($"Nome: {livro.Nome_Livro} | Autor: {livro.Autor_Livro} | Classificação: {livro.IdadeClass_Livro}+ | Estoque: {livro.Qtd_Livro}");
                            }
                        }

                        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("=== Cadastro de Novo Livro ===\n");

                        Livros novoLivro = new Livros();

                        Console.Write("Nome do Livro: ");
                        novoLivro.Nome_Livro = Console.ReadLine();

                        Console.Write("Autor do Livro: ");
                        novoLivro.Autor_Livro = Console.ReadLine();

                        int idadeClass;
                        Console.Write("Classificação Indicativa (idade mínima): ");
                        while (!int.TryParse(Console.ReadLine(), out idadeClass) || idadeClass < 0)
                        {
                            Console.Write("Informe um número válido para a classificação indicativa: ");
                        }
                        novoLivro.IdadeClass_Livro = idadeClass;

                        int quantidade;
                        Console.Write("Quantidade em estoque: ");
                        while (!int.TryParse(Console.ReadLine(), out quantidade) || quantidade < 0)
                        {
                            Console.Write("Informe um número válido para a quantidade: ");
                        }
                        novoLivro.Qtd_Livro = quantidade;

                        try
                        {
                            livrosController.CadastrarLivro(novoLivro);
                            Console.WriteLine("\nLivro cadastrado com sucesso!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"\nErro ao cadastrar livro: {ex.Message}");
                        }

                        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                        Console.ReadKey();
                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("--- Realizar Empréstimo ---");

                        var livrosDisponiveis = livrosController.ObterTodosLivros();

                        Console.WriteLine("Livros disponíveis:");
                        foreach (var livro in livrosDisponiveis)
                        {
                            Console.WriteLine($"ID: {livro.Id_Livro} | Título: {livro.Nome_Livro} | Autor: {livro.Autor_Livro}");
                        }

                        Console.Write("\nDigite o ID do livro que deseja emprestar: ");
                        if (!int.TryParse(Console.ReadLine(), out int idLivro))
                        {
                            Console.WriteLine("ID inválido!");
                            Console.ReadKey();
                            break;
                        }

                        var hoje = DateOnly.FromDateTime(DateTime.Today);
                        var devolucao = DateOnly.FromDateTime(DateTime.Today.AddDays(7));

                        var emprestimo = new Emprestimos
                        {
                            Dt_Emprestimo = hoje,
                            Dt_Devolucao = devolucao,
                            Id_Usuario = usuario.Id_Usuario,
                            Id_Livro = idLivro
                        };

                        emprestimosController.CadastrarEmprestimo(emprestimo);

                        Console.WriteLine($"\nEmpréstimo realizado com sucesso!");
                        Console.WriteLine($"Devolução prevista para: {devolucao:dd/MM/yyyy}");
                        Console.ReadKey();
                        break;

                    case "4":
                        Console.Clear();
                        Console.WriteLine("--- Recomendações de Livros ---");

                        // 1. Calcular idade do usuário
                        var hoje1 = DateTime.Today;
                        var nascimento = usuario.DtNasc_Usuario.ToDateTime(TimeOnly.MinValue);
                        int idadeUsuario = hoje1.Year - nascimento.Year;
                        if (nascimento > hoje1.AddYears(-idadeUsuario)) idadeUsuario--;

                        // 2. Buscar todos os livros pelo controller
                        var todosLivros = livrosController.ObterTodosLivros();

                        // 3. Filtrar por classificação indicativa
                        var livrosRecomendados = todosLivros
                            .Where(l => l.IdadeClass_Livro <= idadeUsuario)
                            .ToList();

                        if (livrosRecomendados.Count == 0)
                        {
                            Console.WriteLine("Nenhum livro recomendado para sua faixa etária.");
                        }
                        else
                        {
                            Console.WriteLine($"Recomendações para {usuario.Nome_Usuario} (Idade: {idadeUsuario} anos):\n");
                            foreach (var livro in livrosRecomendados)
                            {
                                Console.WriteLine($"Título: {livro.Nome_Livro} | Autor: {livro.Autor_Livro} | Classificação: {livro.IdadeClass_Livro}+");
                            }
                        }

                        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                        Console.ReadKey();
                        break;

                    case "5":
                        Console.Clear();
                        Console.WriteLine("--- Meus Empréstimos ---");

                        var meusEmprestimos = emprestimosController.ListarEmprestimos()
                            .FindAll(e => e.Id_Usuario == usuario.Id_Usuario);

                        if (meusEmprestimos.Count == 0)
                        {
                            Console.WriteLine("Você ainda não possui empréstimos.");
                        }
                        else
                        {
                            var livrosDoSistema = livrosController.ObterTodosLivros();
                            foreach (var emp in meusEmprestimos)
                            {
                                var livro = livrosDoSistema.Find(l => l.Id_Livro == emp.Id_Livro);
                                Console.WriteLine($"Livro: {livro?.Nome_Livro ?? "Desconhecido"}");
                                Console.WriteLine($"Empréstimo: {emp.Dt_Emprestimo:dd/MM/yyyy}");
                                Console.WriteLine($"Devolução: {emp.Dt_Devolucao:dd/MM/yyyy}");
                                Console.WriteLine(new string('-', 30));
                            }
                        }

                        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                        Console.ReadKey();
                        break;

                    case "0":
                        continuar = false;
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
