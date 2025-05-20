using BibliotecaDigital.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class UsuariosRepository
{
    private readonly string _connectionString = "User Id=RM550366;Password=280105;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)));";

    public void Inserir(Usuarios usuario)
    {
        using (OracleConnection connection = new OracleConnection(_connectionString))
        {
            string query = @"INSERT INTO usuarios_FIAP (id_usuario, nomeusuario, emailusuario, dtnasc_usuario, senha)
                             VALUES (:id, :nome, :email, :dtnasc, :senha)";

            OracleCommand cmd = new OracleCommand(query, connection);
            cmd.Parameters.Add(new OracleParameter("id", usuario.Id_Usuario));
            cmd.Parameters.Add(new OracleParameter("nome", usuario.Nome_Usuario));
            cmd.Parameters.Add(new OracleParameter("email", usuario.Email_Usuario));
            cmd.Parameters.Add(new OracleParameter("dtnasc", usuario.DtNasc_Usuario.ToDateTime(TimeOnly.MinValue)));
            cmd.Parameters.Add(new OracleParameter("senha", usuario.senha));

            connection.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public Usuarios BuscarPorId(int id)
    {
        using (OracleConnection connection = new OracleConnection(_connectionString))
        {
            string query = "SELECT * FROM usuarios_FIAP WHERE id_usuario = :id";

            OracleCommand cmd = new OracleCommand(query, connection);
            cmd.Parameters.Add(new OracleParameter("id", id));

            connection.Open();
            OracleDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Usuarios
                {
                    Id_Usuario = Convert.ToInt32(reader["id_usuario"]),
                    Nome_Usuario = reader["nomeusuario"].ToString(),
                    Email_Usuario = reader["emailusuario"].ToString(),
                    DtNasc_Usuario = DateOnly.FromDateTime(Convert.ToDateTime(reader["dtnasc_usuario"])),
                    senha = reader["senha"].ToString()
                };
            }
        }

        return null;
    }

    public List<Usuarios> ListarTodos()
    {
        List<Usuarios> lista = new List<Usuarios>();

        using (OracleConnection connection = new OracleConnection(_connectionString))
        {
            string query = "SELECT * FROM usuarios_FIAP";

            OracleCommand cmd = new OracleCommand(query, connection);
            connection.Open();
            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Usuarios
                {
                    Id_Usuario = Convert.ToInt32(reader["id_usuario"]),
                    Nome_Usuario = reader["nomeusuario"].ToString(),
                    Email_Usuario = reader["emailusuario"].ToString(),
                    DtNasc_Usuario = DateOnly.FromDateTime(Convert.ToDateTime(reader["dtnasc_usuario"]))
                });
            }
        }

        return lista;
    }

    public void Deletar(int id)
    {
        using (OracleConnection connection = new OracleConnection(_connectionString))
        {
            string query = "DELETE FROM usuarios_FIAP WHERE id_usuario = :id";

            OracleCommand cmd = new OracleCommand(query, connection);
            cmd.Parameters.Add(new OracleParameter("id", id));

            connection.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public Usuarios BuscarPorEmail(string email)
    {
        using (OracleConnection connection = new OracleConnection(_connectionString))
        {
            string query = "SELECT * FROM usuarios_FIAP WHERE emailusuario = :email";

            OracleCommand cmd = new OracleCommand(query, connection);
            cmd.Parameters.Add(new OracleParameter("email", email));

            connection.Open();
            OracleDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Usuarios
                {
                    Id_Usuario = Convert.ToInt32(reader["id_usuario"]),
                    Nome_Usuario = reader["nomeusuario"].ToString(),
                    Email_Usuario = reader["emailusuario"].ToString(),
                    DtNasc_Usuario = DateOnly.FromDateTime(Convert.ToDateTime(reader["dtnasc_usuario"])),
                    senha = reader["senha"]?.ToString()
                };
            }
        }

        return null;
    }
}