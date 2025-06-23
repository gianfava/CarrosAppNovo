using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace CarrosAppNovo.Models
{
    public class Conecta
    {
        public string? conexao_status { get; set; }
        public MySqlDataReader? Dr;
        public MySqlCommand? Cmd;
        public MySqlConnection? Conn;
        public string StrQuery = ""; //  CORREÇÃO: Variável para a query declarada

        public Conecta()
        {
        }

        public bool Conexao()
        {
            // 🔥 Ajuste os dados conforme sua configuração:
            var StrCon = new MySqlConnectionStringBuilder
            {
                Server = "gian.eastus2.cloudapp.azure.com",
                Port = 3306,
                UserID = "gianfava",
                Password = "Fatec123@",
                Database = "Carros",
                SslMode = MySqlSslMode.None
            };

            Conn = new MySqlConnection(StrCon.ToString());
            bool ret = false;

            try
            {
                Conn.Open();
                conexao_status = " Conexão realizada com sucesso!";
                ret = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Erro de conexão: " + ex.Message);
                conexao_status = "❌ " + ex.Message;
                ret = false;
            }

            return ret;
        }
    }
}