using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Datos_bc
{
    public class DatosLibros : DatosConexionBD
    {
        public DatosLibros() : base()
        {
        }

        public DataSet ObtenerTodosLosLibros()
        {
            string orden = "SELECT ID, TITULO, AUTOR, GENERO, PRECIO, STOCK FROM LIBROS";
            SqlDataAdapter adaptador = new SqlDataAdapter(orden, conexion);
            DataSet ds = new DataSet();

            try
            {
                Abrirconexion();
                adaptador.Fill(ds, "Libros");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los libros", ex);
            }
            finally
            {
                Cerrarconexion();
                adaptador.Dispose();
            }

            return ds;
        }

        public int ModificarStockLibro(int idLibro, int nuevoStock)
        {
            int resultado = -1;
            string orden = $"UPDATE LIBROS SET STOCK = {nuevoStock} WHERE ID = {idLibro}";

            SqlCommand cmd = new SqlCommand(orden, conexion);

            try
            {
                Abrirconexion();
                resultado = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el stock del libro", ex);
            }
            finally
            {
                Cerrarconexion();
                cmd.Dispose();
            }

            return resultado;
        }
    }
}
