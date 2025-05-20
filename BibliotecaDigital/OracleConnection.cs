using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibiotecaDigital.Repository
{
    public class OracleService
    {
        private readonly string _connectionString = AppConfig.OracleConnectionString;


        public bool ValidarLogin(string username, string password)
        {

            string sql = "SELECT COUNT(*) FROM USUARIOS WHERE NOME_USUARIO = :pUsername AND SENHA = :pPassword";


            using (OracleConnection connection = new OracleConnection(_connectionString))
            {

                using (OracleCommand command = new OracleCommand(sql, connection))
                {

                    command.Parameters.Add(new OracleParameter("pUsername", OracleDbType.Varchar2)).Value = username;
                    command.Parameters.Add(new OracleParameter("pPassword", OracleDbType.Varchar2)).Value = password;

                    try
                    {

                        connection.Open();


                        int count = Convert.ToInt32(command.ExecuteScalar());

                        return count == 1;
                    }
                    catch (OracleException ex)
                    {

                        Console.WriteLine($"Erro Oracle ao validar login: {ex.Message}");
                        return false;
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine($"Erro geral ao validar login: {ex.Message}");
                        return false;
                    }

                }
            }
        }
    }
}