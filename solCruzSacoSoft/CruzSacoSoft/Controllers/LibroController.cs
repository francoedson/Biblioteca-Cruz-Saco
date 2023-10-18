using CruzSacoSoft.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CruzSacoSoft.Controllers
{
    public class LibroController : ApiController
    {

        // Define la cadena de conexión usando la configuración.
        string con = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        // GET: api/Editorial
        [HttpGet]
        [Route("api/Libro/listar")]
        public IEnumerable<Libro> Get()
        {
            List<Libro> libro = new List<Libro>();

            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("sp_ListarLibro", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Libro l = new Libro
                            {
                                codLibro = reader["codigo"].ToString(),
                                titulo = reader["titulo"].ToString(),
                                autor = reader["autor"].ToString(),
                                categoria = reader["categoria"].ToString(),
                                editorial = reader["editorial"].ToString(),
                                ubicacion = reader["Ubicacion"].ToString(),
                                ejemplares = (int)reader["cantidad"],
                                vchEstado = reader["estado"].ToString(),
                                fechaCreacion = reader["Fecha"].ToString(),
                            };
                            libro.Add(l);
                        }
                    }
                }
            }
            return libro;
        }


    }
}
