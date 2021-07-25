using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loja_rec.Models
{
    public class Produto
    {
        private const string connectionBD = "Server=Localhost;Database=loja_rec;Userid=root; Password=1234";
        string msg = null;

        private string descricao, sku;
        private double preco;
        private int id, quantidade;

        public string Descricao
        {
            get => descricao;
            set => descricao = value;
        }
        public string Sku
        {
            get => sku;
            set => sku = value;
        }
        public double Preco
        {
            get => preco;
            set => preco = value;
        }
        public int Id
        {
            get => id;
            set => id = value;
        }
        public int Quantidade
        {
            get => quantidade;
            set => quantidade = value;
        }
        public string inserir()
        {
            MySqlConnection con = new MySqlConnection(connectionBD);
            try
            {
                con.Open();
                MySqlCommand query = new MySqlCommand("INSERT INTO produtos(descricao, preco, quantidade, sku)" +
                    " VALUES (@descricao, @preco, @quantidade, @sku)", con);
                query.Parameters.AddWithValue("@descricao", Descricao);
                query.Parameters.AddWithValue("@preco", Preco);
                query.Parameters.AddWithValue("@quantidade", Quantidade);
                query.Parameters.AddWithValue("@sku", Sku);
                query.ExecuteNonQuery();
                msg = "Produto cadastrado com sucesso";

            }
            catch (Exception e1)
            {
                msg = "Falha ao cadastrar produto" + e1.Message;
            }
            finally
            {
                con.Close();
            }
            return msg;
        }   
        public static List<Produto> ListarProduto()
        {
            MySqlConnection con = new MySqlConnection(connectionBD);
            List<Produto> lista = new List<Produto>();
            try
            {
                con.Open();
                MySqlCommand query = new MySqlCommand("SELECT * from produtos", con);
                MySqlDataReader leitor = query.ExecuteReader();
                while (leitor.Read())
                {
                    Produto item = new Produto();
                    item.Id = int.Parse(leitor["id"].ToString());
                    item.Descricao = leitor["descricao"].ToString();
                    item.Preco = double.Parse(leitor["preco"].ToString());
                    item.Quantidade = int.Parse(leitor["quantidade"].ToString());
                    item.Sku = leitor["sku"].ToString();
                    lista.Add(item);
                }
            }
            catch (Exception e2)
            {
                lista = null;
            }
            finally
            {
                con.Close();
            }
            return lista;
        }

        public string Excluir()
        {
            MySqlConnection con = new MySqlConnection(connectionBD);
            try
            {
                con.Open();
                MySqlCommand query = new MySqlCommand("DELETE FROM produtos WHERE id = @id", con);
                query.Parameters.AddWithValue("@id", Id);
                query.ExecuteNonQuery();
                msg = "Produto deletado com sucesso";
            }
            catch (Exception w1)
            {
                msg = "Erro ao excluir o produto";
            }
            finally
            {
                con.Close();
            }
            return msg;
        }

        public string Editar()
        {
            MySqlConnection con = new MySqlConnection(connectionBD);
            try
            {
                con.Open();
                MySqlCommand query = new MySqlCommand("UPDATE produtos SET descricao = @descricao, preco " +
                    "= @preco, quantidade = @quantidade, sku = @sku WHERE id = @id", con);
                query.Parameters.AddWithValue("@id", Id);
                query.Parameters.AddWithValue("@descricao", Descricao);
                query.Parameters.AddWithValue("@preco", Preco);
                query.Parameters.AddWithValue("@quantidade", Quantidade);
                query.Parameters.AddWithValue("@sku", Sku);
                query.ExecuteNonQuery();
                msg = "Quantidade do produto alterada com sucesso";

            }
            catch (Exception r1)
            {
                msg = "Falha ao editar a quantidade do produto";
            }
            finally
            {
                con.Close();
            }
            return msg;
        }

        public Produto getById()
        {
            MySqlConnection con = new MySqlConnection(connectionBD);
            Produto prod = new Produto();
            try
            {
                con.Open();
                var query = con.CreateCommand();
                query = new MySqlCommand("SELECT * FROM produtos WHERE id = @id;", con);
                query.Parameters.AddWithValue("@id", Id);
                var data = query.ExecuteReader();

                while (data.Read())
                {
                    prod.Id = int.Parse(data["id"].ToString());
                    prod.Descricao = data["descricao"].ToString();
                    prod.Preco = double.Parse(data["preco"].ToString());
                    prod.Quantidade = int.Parse(data["quantidade"].ToString());
                    prod.Sku = data["sku"].ToString();
                }
            }
            catch (Exception e)
            {
                prod = null;
            }
            finally
            {
                con.Close();
            }

            return prod;
        }
    }
    



}