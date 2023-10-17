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
    public class CategoriaController : ApiController
    {
        // Define la cadena de conexión usando la configuración.
        string con = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        // GET: api/Categoria
        [HttpGet]
        [Route("api/Categoria/listar")]
        public IEnumerable<Categoria> Get()
        {
            List<Categoria> categorias = new List<Categoria>();

            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("sp_ListarCategoria", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Categoria c = new Categoria
                            {
                                codCategoria = reader["codigo"].ToString(),
                                descripcion = reader["descripcion"].ToString(),
                                vchEstado = reader["Estado"].ToString(),
                                fechaRegistro = reader["Fecha"].ToString(),
                            };
                            categorias.Add(c);
                        }
                    }
                }
            }
            return categorias ;
        }


        [HttpPost]
        [Route("api/Categoria/registrar")]
        public void Registrar([FromBody] Categoria c)
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("sp_RegistrarCategoria", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Descripcion", c.descripcion);
                    cmd.ExecuteNonQuery();
                }
            }

        }

        [HttpPost]
        [Route("api/Categoria/editar")]
        public void Editar([FromBody] Categoria c)
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("sp_ModificarCategoria", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codCat", c.codCategoria);
                    cmd.Parameters.AddWithValue("@Descripcion", c.descripcion);
                    cmd.Parameters.AddWithValue("@Estado", c.intEstado);
                    cmd.ExecuteNonQuery();
                }
            }

        }
    }
}
