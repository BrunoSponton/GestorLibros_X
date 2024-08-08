using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entidades_bc;

namespace Datos_bc
{
    public class DatosCarrito : DatosConexionBD
    {
        public DatosCarrito() : base() { }

        public int AbmCarrito(string accion, Carrito carrito)
        {
            int resultado = -1;
            string orden = string.Empty;

            if (accion == "Alta")
                orden = "INSERT INTO CARRITO (USUARIO_ID, LIBRO_ID, CANTIDAD) VALUES (@USUARIO_ID, @LIBRO_ID, @CANTIDAD);";
            else if (accion == "Modificar")
                orden = "UPDATE CARRITO SET CANTIDAD = @CANTIDAD WHERE USUARIO_ID = @USUARIO_ID AND LIBRO_ID = @LIBRO_ID;";
            else if (accion == "Borrar")
                orden = "DELETE FROM CARRITO WHERE USUARIO_ID = @USUARIO_ID AND LIBRO_ID = @LIBRO_ID;";

            SqlCommand cmd = new SqlCommand(orden, conexion);
            cmd.Parameters.AddWithValue("@USUARIO_ID", carrito.UsuarioId);
            cmd.Parameters.AddWithValue("@LIBRO_ID", carrito.LibroId);
            cmd.Parameters.AddWithValue("@CANTIDAD", carrito.Cantidad);

            try
            {
                Abrirconexion();
                SqlTransaction transaction = conexion.BeginTransaction();
                cmd.Transaction = transaction;

                bool stockActualizado = true;
                if (accion == "Alta")
                {
                    // Verificar stock disponible antes de insertar en carrito
                    if (!HayStockSuficiente(carrito.LibroId, carrito.Cantidad))
                    {
                        throw new Exception("Stock insuficiente para realizar la operación.");
                    }
                    stockActualizado = ActualizarStockLibro(carrito.LibroId, -carrito.Cantidad, transaction);
                }
                else if (accion == "Borrar")
                {
                    // Recuperar la cantidad del carrito antes de eliminarlo
                    Carrito carritoExistente = ObtenerCarritoPorUsuarioYLibro(carrito.UsuarioId, carrito.LibroId);
                    if (carritoExistente != null)
                    {
                        stockActualizado = ActualizarStockLibro(carrito.LibroId, carritoExistente.Cantidad, transaction);
                    }
                }

                if (!stockActualizado)
                {
                    transaction.Rollback();
                    throw new Exception("Error al actualizar el stock.");
                }

                resultado = cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception e)
            {
                Cerrarconexion();
                throw new Exception("Error al tratar de guardar, borrar o modificar en CARRITO", e);
            }
            finally
            {
                Cerrarconexion();
                cmd.Dispose();
            }

            return resultado;
        }

        private bool ActualizarStockLibro(int libroId, int cantidadCambio, SqlTransaction transaction)
        {
            string query = "UPDATE LIBROS SET STOCK = STOCK + @CANTIDAD_CAMBIO WHERE ID = @LIBRO_ID AND (STOCK + @CANTIDAD_CAMBIO) >= 0;";
            SqlCommand cmd = new SqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@CANTIDAD_CAMBIO", cantidadCambio);
            cmd.Parameters.AddWithValue("@LIBRO_ID", libroId);
            cmd.Transaction = transaction;

            try
            {
                int filasAfectadas = cmd.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
            catch
            {
                return false;
            }
        }

        private bool HayStockSuficiente(int libroId, int cantidadRequerida)
        {
            string query = "SELECT STOCK FROM LIBROS WHERE ID = @LIBRO_ID";
            SqlCommand cmd = new SqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@LIBRO_ID", libroId);

            try
            {
                Abrirconexion();
                int stockActual = (int)cmd.ExecuteScalar();
                return stockActual >= cantidadRequerida;
            }
            catch
            {
                return false;
            }
            finally
            {
                Cerrarconexion();
                cmd.Dispose();
            }
        }

        public int AgregarAlCarrito(Carrito carrito)
        {
            return AbmCarrito("Alta", carrito);
        }

        public int EliminarDelCarrito(int usuarioId, int libroId)
        {
            Carrito carrito = ObtenerCarritoPorUsuarioYLibro(usuarioId, libroId);
            if (carrito == null)
            {
                throw new Exception("No se encontró el item en el carrito para eliminar.");
            }
            return AbmCarrito("Borrar", carrito);
        }

        public Carrito ObtenerCarritoPorUsuarioYLibro(int usuarioId, int libroId)
        {
            Carrito carrito = null;
            string query = "SELECT * FROM CARRITO WHERE USUARIO_ID = @USUARIO_ID AND LIBRO_ID = @LIBRO_ID";

            SqlCommand cmd = new SqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@USUARIO_ID", usuarioId);
            cmd.Parameters.AddWithValue("@LIBRO_ID", libroId);

            try
            {
                Abrirconexion();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    carrito = new Carrito
                    {
                        UsuarioId = (int)reader["USUARIO_ID"],
                        LibroId = (int)reader["LIBRO_ID"],
                        Cantidad = (int)reader["CANTIDAD"]
                    };
                }
                reader.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener el carrito por Usuario y Libro", e);
            }
            finally
            {
                Cerrarconexion();
                cmd.Dispose();
            }

            return carrito;
        }

        public DataTable ObtenerCarritoPorUsuario(int usuarioId)
        {
            DataTable dt = new DataTable();
            string query = "SELECT CARRITO.USUARIO_ID, CARRITO.LIBRO_ID, CARRITO.CANTIDAD, LIBROS.TITULO " +
                           "FROM CARRITO " +
                           "INNER JOIN LIBROS ON CARRITO.LIBRO_ID = LIBROS.ID " +
                           "WHERE CARRITO.USUARIO_ID = @USUARIO_ID";

            SqlCommand cmd = new SqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@USUARIO_ID", usuarioId);

            try
            {
                Abrirconexion();
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener el carrito por Usuario", e);
            }
            finally
            {
                Cerrarconexion();
                cmd.Dispose();
            }

            return dt;

        }
    }
}

