using Npgsql;
using System.Collections.Generic;
using System.Configuration;
using WpfApp1.Models;

namespace WpfApp1.DataAccess
{
    public class PersonneRepository
    {
        private readonly string connectionString;

        public PersonneRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString;
        }

        public List<Personne> GetAll()
        {
            var personnes = new List<Personne>();
            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            var cmd = new NpgsqlCommand("SELECT * FROM personne", conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                personnes.Add(new Personne
                {
                    Id = reader.GetInt32(0),
                    Nom = reader.GetString(1),
                    Prenom = reader.GetString(2),
                    Age = reader.GetInt32(3)
                });
            }
            return personnes;
        }

        public void Add(Personne p)
        {
            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            var cmd = new NpgsqlCommand("INSERT INTO personne (nom, prenom, age) VALUES (@nom, @prenom, @age)", conn);
            cmd.Parameters.AddWithValue("nom", p.Nom);
            cmd.Parameters.AddWithValue("prenom", p.Prenom);
            cmd.Parameters.AddWithValue("age", p.Age);
            cmd.ExecuteNonQuery();
        }

        public void Update(Personne p)
        {
            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            var cmd = new NpgsqlCommand("UPDATE personne SET nom = @nom, prenom = @prenom, age = @age WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("id", p.Id);
            cmd.Parameters.AddWithValue("nom", p.Nom);
            cmd.Parameters.AddWithValue("prenom", p.Prenom);
            cmd.Parameters.AddWithValue("age", p.Age);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            var cmd = new NpgsqlCommand("DELETE FROM personne WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }

        public List<Personne> Search(string nom)
        {
            var personnes = new List<Personne>();
            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            var cmd = new NpgsqlCommand("SELECT * FROM personne WHERE nom ILIKE @nom", conn);
            cmd.Parameters.AddWithValue("nom", "%" + nom + "%");
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                personnes.Add(new Personne
                {
                    Id = reader.GetInt32(0),
                    Nom = reader.GetString(1),
                    Prenom = reader.GetString(2),
                    Age = reader.GetInt32(3)
                });
            }
            return personnes;
        }
    }
}
