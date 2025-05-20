using BibliotecaDigital.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaDigital.Repository
{
    public class LivrosRepository
    {
        private readonly string _connectionString = "User Id=RM550366;Password=280105;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)));";

        public void Inserir(Livros livro)
        {
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                string query = @"INSERT INTO livros_FIAP (id_livro, nome_livro, autor_livro, idadeclass_livro, qtd_livro)
                                 VALUES (:id, :nome, :autor, :idadeclass, :qtd)";

                OracleCommand cmd = new OracleCommand(query, connection);
                cmd.Parameters.Add(new OracleParameter("id", livro.Id_Livro));
                cmd.Parameters.Add(new OracleParameter("nome", livro.Nome_Livro));
                cmd.Parameters.Add(new OracleParameter("autor", livro.Autor_Livro));
                cmd.Parameters.Add(new OracleParameter("idadeclass", livro.IdadeClass_Livro));
                cmd.Parameters.Add(new OracleParameter("qtd", livro.Qtd_Livro));

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Livros BuscarPorId(int id)
        {
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                string query = "SELECT * FROM livros_FIAP WHERE id_livro = :id";

                OracleCommand cmd = new OracleCommand(query, connection);
                cmd.Parameters.Add(new OracleParameter("id", id));

                connection.Open();
                OracleDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Livros
                    {
                        Id_Livro = Convert.ToInt32(reader["id_livro"]),
                        Nome_Livro = reader["nome_livro"].ToString(),
                        Autor_Livro = reader["autor_livro"].ToString(),
                        IdadeClass_Livro = Convert.ToInt32(reader["idadeclass_livro"]),
                        Qtd_Livro = Convert.ToInt32(reader["qtd_livro"])
                    };
                }
            }

            return null;
        }

        public List<Livros> ListarTodos()
        {
            List<Livros> lista = new List<Livros>();

            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                string query = "SELECT * FROM livros_FIAP";

                OracleCommand cmd = new OracleCommand(query, connection);
                connection.Open();
                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Livros
                    {
                        Id_Livro = Convert.ToInt32(reader["id_livro"]),
                        Nome_Livro = reader["nome_livro"].ToString(),
                        Autor_Livro = reader["autor_livro"].ToString(),
                        IdadeClass_Livro = Convert.ToInt32(reader["idadeclass_livro"]),
                        Qtd_Livro = Convert.ToInt32(reader["qtd_livro"])
                    });
                }
            }

            return lista;
        }

        public void Deletar(int id)
        {
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                string query = "DELETE FROM livros_FIAP WHERE id_livro = :id";

                OracleCommand cmd = new OracleCommand(query, connection);
                cmd.Parameters.Add(new OracleParameter("id", id));

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
