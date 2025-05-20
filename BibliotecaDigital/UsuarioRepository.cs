using Oracle.ManagedDataAccess.Client;
using System;
//using BibiotecaDigital.Repository.UsuarioRepository.cs;

namespace BibiotecaDigital.Repository
{
    public class UsuarioRepository
    {
        private readonly string _connectionString = "User Id=xx;Password=xx;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)));";

        public bool ValidarLogin(string usuario, string email)
        {
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                string query = "SELECT COUNT(1) FROM BibliDig_Usuarios WHERE Nome_Usuario = :usuario AND Email_Usuario = :email";

                connection.Open();

                using (OracleCommand cmd = new OracleCommand(query, connection))
                {
                    cmd.Parameters.Add(new OracleParameter("usuario", usuario));
                    cmd.Parameters.Add(new OracleParameter("email", email));

                    object resultado = cmd.ExecuteScalar();

                    return Convert.ToInt32(resultado) > 0;
                }
            }
        }
    }
}

