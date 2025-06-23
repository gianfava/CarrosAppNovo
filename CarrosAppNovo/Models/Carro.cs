using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySqlConnector;
using System.Collections.Generic;

namespace CarrosAppNovo.Models
{
    public class Carro : Conecta
    {
        public int Id { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int Ano { get; set; }
        public string Cor { get; set; } = string.Empty;
        public string Placa { get; set; } = string.Empty;

        public List<Carro> listaCarros = new List<Carro>();

        public Carro() { }

        public Carro(int id, string marca, string modelo, int ano, string cor, string placa)
        {
            this.Id = id;
            this.Marca = marca;
            this.Modelo = modelo;
            this.Ano = ano;
            this.Cor = cor;
            this.Placa = placa;
        }

        public bool Salvar(bool pIncluir = false)
        {
            bool ret = false;
            if (!Conexao() || Conn is null) // Adicionado check de nulo para Conn
            {
                ret = false;
                return ret;
            }

            if (pIncluir)
            {
                // inclusao de dados
                StrQuery = "INSERT INTO carro(marca, modelo, ano, cor, placa) VALUES " +
                           "(@marca, @modelo, @ano, @cor, @placa);";
            }
            else
            {
                // alteração de dados
                StrQuery = "UPDATE carro SET marca=@marca, modelo=@modelo, ano=@ano, cor=@cor, placa=@placa WHERE id=" + Id.ToString();
            }

            // ✨ CORREÇÃO: O uso de "using" já fecha o Cmd. Conn é fechado manualmente.
            Cmd = new MySqlCommand(StrQuery, Conn);
            Cmd.Parameters.AddWithValue("@marca", Marca);
            Cmd.Parameters.AddWithValue("@modelo", Modelo);
            Cmd.Parameters.AddWithValue("@ano", Ano);
            Cmd.Parameters.AddWithValue("@cor", Cor);
            Cmd.Parameters.AddWithValue("@placa", Placa);
            if (!pIncluir)
            {
                Cmd.Parameters.AddWithValue("@id", Id);
            }

            try
            {
                int i = Cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    ret = true;
                    conexao_status = "OK";
                }
            }
            catch (System.Exception ex)
            {
                conexao_status = "Erro:" + ex.Message.ToString();
            }

            Conn.Close();
            return ret;
        }

        public bool Excluir()
        {
            bool Ret = false;
            if (!Conexao() || Conn is null) // Adicionado check de nulo para Conn
            {
                return Ret;
            }

            StrQuery = "DELETE FROM carro WHERE id=" + Id.ToString();

            Cmd = new MySqlCommand(StrQuery, Conn);
            try
            {
                int i = Cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    Ret = true;
                    conexao_status = "OK";
                }
            }
            catch (System.Exception ex)
            {
                conexao_status = "Erro:" + ex.Message.ToString();
                Ret = false;
            }

            Conn.Close();
            return true;
        }

        public bool ListarCarros(string xBusca = "")
        {
            if (!Conexao() || Conn is null) // Adicionado check de nulo para Conn
            {
                return false;
            }

            if (string.IsNullOrEmpty(xBusca))
            {
                StrQuery = "SELECT * FROM `carro` ORDER BY `modelo`";
            }
            else
            {
                StrQuery = "SELECT * FROM `carro` WHERE `modelo` LIKE '%" + xBusca + "%' OR `marca` LIKE '%" + xBusca + "%' ORDER BY `modelo`";
            }

            Cmd = new MySqlCommand(StrQuery, Conn);
            Dr = Cmd.ExecuteReader();
            listaCarros.Clear();

            if (Dr is not null)
            {
                while (Dr.Read())
                {
                    listaCarros.Add(
                        new Carro
                        {
                            // ✨ CORREÇÃO: Adicionado ToString() para garantir que não seja nulo.
                            Id = int.Parse(Dr["id"]?.ToString() ?? "0"),
                            Marca = Dr["marca"]?.ToString() ?? "",
                            Modelo = Dr["modelo"]?.ToString() ?? "",
                            Ano = int.Parse(Dr["ano"]?.ToString() ?? "0"),
                            Cor = Dr["cor"]?.ToString() ?? "",
                            Placa = Dr["placa"]?.ToString() ?? ""
                        }
                    );
                }
                Dr.Close();
            }

            Conn.Close();
            return true;
        }
    }
}