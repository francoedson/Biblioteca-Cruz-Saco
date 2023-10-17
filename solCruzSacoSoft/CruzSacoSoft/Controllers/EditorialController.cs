using CruzSacoSoft.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;

namespace CruzSacoSoft.Controllers
{
    public class EditorialController : ApiController
    {

        // Define la cadena de conexión usando la configuración.
        string con = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        // GET: api/Editorial
        [HttpGet]
        [Route("api/Editorial/listar")]
        public IEnumerable<Editorial> Get()
        {
            List<Editorial> editorial = new List<Editorial>();

            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("sp_ListarEditorial", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Editorial e = new Editorial
                            {
                                codEditorial = reader["codigo"].ToString(),
                                descripcion = reader["descripcion"].ToString(),
                                vchEstado = reader["estado"].ToString(),
                                fechaRegistro = reader["Fecha"].ToString(),
                            };
                            editorial.Add(e);
                        }
                    }
                }
            }
            return editorial;
        }


        [HttpPost]
        [Route("api/Editorial/registrar")]
        public void Registrar([FromBody] Editorial e)
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("sp_RegistrarEditorial", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Descripcion", e.descripcion);
                    cmd.ExecuteNonQuery();
                }
            }

        }

        [HttpPost]
        [Route("api/Editorial/editar")]
        public void Editar([FromBody] Editorial e)
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("sp_ModificarEditorial", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codEditorial", e.codEditorial);
                    cmd.Parameters.AddWithValue("@Descripcion", e.descripcion);
                    cmd.Parameters.AddWithValue("@Estado", e.intEstado);
                    cmd.ExecuteNonQuery();
                }
            }

        }
    }
}
