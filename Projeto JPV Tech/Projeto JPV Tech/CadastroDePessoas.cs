using System;
using Microsoft.Data.SqlClient;

namespace CadastroPessoa
{
    class CadastroDePessoas
    {
        static string connectionString = "Server=KAIQUESANNI;Database=CadastroPessoa;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";



        static void Main(string[] args)
        {
            // Testando a conexão com o banco de dados
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Conexão bem-sucedida!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao conectar ao banco de dados: " + ex.Message);
            }

            // Exemplo de uso dos métodos CRUD
            Console.WriteLine("Inserindo pessoa...");
            InserirPessoa("Joao da Silva", new DateTime(1990, 5, 15), 3500.00m, "22345678901");

            Console.WriteLine("Atualizando pessoa...");
            AtualizarPessoa(1, "Joao da Silva Atualizado", new DateTime(1990, 5, 15), 4500.00m, "22345678901");

            Console.WriteLine("Selecionando pessoas...");
            SelecionarPessoas();

            // Supondo que deseje excluir a pessoa com Id = 2
            Console.WriteLine("Excluindo pessoa...");
            ExcluirPessoa(2);
        }

        static void InserirPessoa(string nome, DateTime dataNascimento, decimal renda, string cpf)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Pessoa (NomeCompleto, DataNascimento, ValorRenda, CPF) VALUES (@NomeCompleto, @DataNascimento, @ValorRenda, @CPF)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NomeCompleto", nome);
                command.Parameters.AddWithValue("@DataNascimento", dataNascimento);
                command.Parameters.AddWithValue("@ValorRenda", renda);
                command.Parameters.AddWithValue("@CPF", cpf);

                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Pessoa inserida com sucesso!");
            }
        }

        static void AtualizarPessoa(int id, string nome, DateTime dataNascimento, decimal renda, string cpf)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Pessoa SET NomeCompleto = @NomeCompleto, DataNascimento = @DataNascimento, ValorRenda = @ValorRenda, CPF = @CPF WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@NomeCompleto", nome);
                command.Parameters.AddWithValue("@DataNascimento", dataNascimento);
                command.Parameters.AddWithValue("@ValorRenda", renda);
                command.Parameters.AddWithValue("@CPF", cpf);

                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Pessoa atualizada com sucesso!");
            }
        }

        static void ExcluirPessoa(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Pessoa WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Pessoa excluída com sucesso!");
            }
        }

        static void SelecionarPessoas()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, NomeCompleto, DataNascimento, ValorRenda, CPF FROM Pessoa";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Id: {reader["Id"]}, Nome: {reader["NomeCompleto"]}, Data de Nascimento: {reader["DataNascimento"]}, Renda: {reader["ValorRenda"]}, CPF: {reader["CPF"]}");
                    }
                }
            }
        }
    }
}
