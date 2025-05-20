using BibliotecaDigital.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaDigital.Repository
{
    public class EmprestimosRepository
    {
        private readonly string _connectionString = "User Id=RM550366;Password=280105;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)));";

        public void Inserir(Emprestimos emprestimo)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                string query = @"
        INSERT INTO emprestimos_FIAP 
        (dt_emprestimo, dt_devolucao, id_usuario, id_livro)
        VALUES (:dtEmp, :dtDev, :idUsuario, :idLivro)";

                OracleCommand cmd = new OracleCommand(query, connection);
                cmd.Parameters.Add(new OracleParameter("dtEmp", emprestimo.Dt_Emprestimo.ToDateTime(TimeOnly.MinValue)));
                cmd.Parameters.Add(new OracleParameter("dtDev", emprestimo.Dt_Devolucao.ToDateTime(TimeOnly.MinValue)));
                cmd.Parameters.Add(new OracleParameter("idUsuario", emprestimo.Id_Usuario));
                cmd.Parameters.Add(new OracleParameter("idLivro", emprestimo.Id_Livro));

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Emprestimos> ListarTodos()
        {
            var lista = new List<Emprestimos>();

            using (var connection = new OracleConnection(_connectionString))
            {
                string query = "SELECT * FROM emprestimos_FIAP";

                OracleCommand cmd = new OracleCommand(query, connection);
                connection.Open();
                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Emprestimos
                    {
                        Id_Emprestimo = Convert.ToInt32(reader["id_emprestimo"]),
                        Dt_Emprestimo = DateOnly.FromDateTime(Convert.ToDateTime(reader["dt_emprestimo"])),
                        Dt_Devolucao = DateOnly.FromDateTime(Convert.ToDateTime(reader["dt_devolucao"])),
                        Id_Usuario = Convert.ToInt32(reader["id_usuario"]),
                        Id_Livro = Convert.ToInt32(reader["id_livro"])
                    });
                }
            }

            return lista;
        }

        public void Deletar(int id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                string query = "DELETE FROM emprestimos_FIAP WHERE id_emprestimo = :id";

                OracleCommand cmd = new OracleCommand(query, connection);
                cmd.Parameters.Add(new OracleParameter("id", id));

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Emprestimos> ListarPorUsuario(int idUsuario)
        {
            var lista = new List<Emprestimos>();

            using (var connection = new OracleConnection(_connectionString))
            {
                string query = "SELECT * FROM emprestimos_FIAP WHERE id_usuario = :idUsuario";

                OracleCommand cmd = new OracleCommand(query, connection);
                cmd.Parameters.Add(new OracleParameter("idUsuario", idUsuario));

                connection.Open();
                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Emprestimos
                    {
                        Id_Emprestimo = Convert.ToInt32(reader["id_emprestimo"]),
                        Dt_Emprestimo = DateOnly.FromDateTime(Convert.ToDateTime(reader["dt_emprestimo"])),
                        Dt_Devolucao = DateOnly.FromDateTime(Convert.ToDateTime(reader["dt_devolucao"])),
                        Id_Usuario = Convert.ToInt32(reader["id_usuario"]),
                        Id_Livro = Convert.ToInt32(reader["id_livro"])
                    });
                }
            }

            return lista;
        }


    }


}
