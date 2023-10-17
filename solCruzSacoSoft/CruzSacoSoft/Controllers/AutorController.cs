using CruzSacoSoft.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CruzSacoSoft.Controllers
{
    public class AutorController : ApiController
    {
        // Define la cadena de conexión usando la configuración.
        string con = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        // GET: api/Categoria
        [HttpGet]
        [Route("api/Autor/listar")]
        public IEnumerable<Autor> Get()
        {
            List<Autor> autor = new List<Autor>();

            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("sp_ListarAutor", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Autor a = new Autor
                            {
                                codAutor = reader["codigo"].ToString(),
                                descripcion = reader["descripcion"].ToString(),
                                vchEstado = reader["estado"].ToString(),
                                fechaRegistro = reader["Fecha"].ToString(),
                            };
                            autor.Add(a);
                        }
                    }
                }
            }
            return autor;
        }


        [HttpPost]
        [Route("api/Autor/registrar")]
        public void Registrar([FromBody] Autor a)
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("sp_RegistrarAutor", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Descripcion", a.descripcion);
                    cmd.ExecuteNonQuery();
                }
            }

        }

        [HttpPost]
        [Route("api/Autor/editar")]
        public void Editar([FromBody] Autor a)
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("sp_ModificarAutor", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codAutor", a.codAutor);
                    cmd.Parameters.AddWithValue("@Descripcion", a.descripcion);
                    cmd.Parameters.AddWithValue("@Estado", a.intEstado);
                    cmd.ExecuteNonQuery();
                }
            }

        }
    }
}
