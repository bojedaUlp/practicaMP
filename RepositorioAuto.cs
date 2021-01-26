using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace pruebaPracticas.Models
{
    public class RepositorioAuto
    {
		private readonly string connectionString;
		private readonly IConfiguration configuration;

		public RepositorioAuto(IConfiguration configuration)
		{
			this.configuration = configuration;
			connectionString = configuration["ConnectionString:DefaultConnection"];
		}

		public int Alta(Auto a)
		{
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"INSERT INTO Auto (patente,marca,modelo,año,kms,img1,img2) " +
					$"VALUES (@patente,@marca,@modelo,@año,@kms,@img1,@img2);" +
					$"SELECT SCOPE_IDENTITY();";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@patente", a.Patente);
					command.Parameters.AddWithValue("@marca", a.Marca);
					command.Parameters.AddWithValue("@modelo", a.Modelo);
					command.Parameters.AddWithValue("@año", a.Año);
					command.Parameters.AddWithValue("@kms", a.Kms);
					command.Parameters.AddWithValue("@img1", a.Img1);
					command.Parameters.AddWithValue("@img2", a.Img2);
					connection.Open();
					res = Convert.ToInt32(command.ExecuteScalar());
					a.Id_Auto = res;
					connection.Close();
				}
			}
			return res;
		}
		public int Baja(int id)
		{
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"DELETE FROM Auto WHERE id_Auto = @id";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@id", id);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}
		public int Modificacion(Auto a)
		{
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"UPDATE Auto SET patente=@patente, marca=@marca,modelo=@modelo, año=@año, kms=@kms, img1=@img1, img2=@img2 " +
					$" WHERE id_Auto = @id";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@patente", a.Patente);
					command.Parameters.AddWithValue("@marca", a.Marca);
					command.Parameters.AddWithValue("@modelo", a.Modelo);
					command.Parameters.AddWithValue("@año", a.Año);
					command.Parameters.AddWithValue("@kms", a.Kms);
					command.Parameters.AddWithValue("@img1", a.Img1);
					command.Parameters.AddWithValue("@img2", a.Img2);
					command.Parameters.AddWithValue("@id", a.Id_Auto);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}

		public IList<Auto> ObtenerTodos()
		{
			IList<Auto> res = new List<Auto>();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"SELECT id_Auto, patente, marca, modelo, año, kms, img1, img2 " +
					$" FROM Auto";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Auto a = new Auto
						{
							Id_Auto = reader.GetInt32(0),
							Patente = reader.GetString(1),
							Marca = reader.GetString(2),
							Modelo = reader.GetString(3),
							Año = reader.GetInt32(4),
							Kms = reader.GetInt32(5),
							Img1 = reader.GetString(6),
							Img2 = reader.GetString(7),

						};
						res.Add(a);
					}
					connection.Close();
				}
			}
			return res;
		}

		public Auto ObtenerPorId(int id)
		{
			Auto a = null;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"SELECT id_Auto, patente, marca, modelo, año, kms, img1, img2 " +
					$" FROM Auto" +
					$" WHERE id_Auto=@id";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.Add("@id", SqlDbType.Int).Value = id;
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						a = new Auto
						{
							Id_Auto = reader.GetInt32(0),
							Patente = reader.GetString(1),
							Marca = reader.GetString(2),
							Modelo = reader.GetString(3),
							Año = reader.GetInt32(4),
							Kms = reader.GetInt32(5),
							Img1 = reader.GetString(6),
							Img2 = reader.GetString(7),
						};
					}
					connection.Close();
				}
			}
			return a;
		}


	}
}
