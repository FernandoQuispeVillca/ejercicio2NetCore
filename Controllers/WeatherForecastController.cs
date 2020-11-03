using ejercicio2.DataAcces;
using ejercicio2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ejercicio2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public List<Usuario> ObtenerUsuarios()
        {
            ConnectionManager ConnectionString = new ConnectionManager();
            List<Usuario> users = new List<Usuario>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                sqlConnection.Open();
                var sqlCmd = new SqlCommand("ListarUsuario", sqlConnection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                var resultado = sqlCmd.ExecuteReader();
                while (resultado.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.id = (int)resultado["Id"];
                    usuario.nombre = (string)resultado["Nombre"];
                    usuario.apellido = (string)resultado["Apellido"];
                    users.Add(usuario);
                   
                }
                sqlConnection.Close();
            }
            return users;
        }

        public List<Usuario> ObtenerUsuario(int id)
        {
            ConnectionManager ConnectionString = new ConnectionManager();
            List<Usuario> users = new List<Usuario>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                sqlConnection.Open();
                var sqlCmd = new SqlCommand("BuscarUsuario", sqlConnection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@id",id);
                var resultado = sqlCmd.ExecuteReader();
                while (resultado.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.id = (int)resultado["Id"];
                    usuario.nombre = (string)resultado["Nombre"];
                    usuario.apellido = (string)resultado["Apellido"];
                    users.Add(usuario);

                }
                sqlConnection.Close();
            }
            return users;
        }

        public void InsertarUsuario(Usuario u)
        {
            ConnectionManager ConnectionString = new ConnectionManager();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                sqlConnection.Open();
                var sqlCmd = new SqlCommand("InsertarUsuario", sqlConnection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@nombre", u.nombre);
                sqlCmd.Parameters.AddWithValue("@apellido", u.apellido);
                sqlCmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        public void ModificarUsuario(Usuario u)
        {
            ConnectionManager ConnectionString = new ConnectionManager();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                sqlConnection.Open();
                var sqlCmd = new SqlCommand("ModificarUsuario", sqlConnection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@id", u.id);
                sqlCmd.Parameters.AddWithValue("@nombre", u.nombre);
                sqlCmd.Parameters.AddWithValue("@apellido", u.apellido);
                sqlCmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        public void BorrarUsuario(int id)
        {
            ConnectionManager ConnectionString = new ConnectionManager();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString.GetConnectionString()))
            {
                sqlConnection.Open();
                var sqlCmd = new SqlCommand("EliminarUsuario", sqlConnection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@id", id);
                sqlCmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }


        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Usuario> Get()
        {

            var users = ObtenerUsuarios().ToList();
            return users;
        }

        // GET: /id
        [HttpGet("{id}")]
        public List<Usuario> GetUsuario(int id)
        {
            var users = ObtenerUsuario(id).ToList();
            return users;
        }

        // POST: post
        [HttpPost]
        public void PostUsuario(Usuario u)
        {
                 InsertarUsuario(u);
        }

        // PUT: put
        [HttpPut("{id}")]
        public void put(Usuario u)
        {
            ModificarUsuario(u);
        }

        // DELETE: delete
        [HttpDelete("{id}")]
        public void DeleteUsuario( int id)
        {
            BorrarUsuario(id);
        }
    }
}
