using System.Configuration;
using System.Data.SqlClient;

namespace Proyecto_Ventas.Data
{
    public class Conexion
    {
        public SqlConnection ObtenerConexion()
        {
            string cadena = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

            return new SqlConnection(cadena);
        }
    }
}