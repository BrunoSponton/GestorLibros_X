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
    public class DatosUsuarios : DatosConexionBD
    {
        public DatosUsuarios() : base()
        {
        }

        public int AltaUsuario(Usuarios usuario)
        {
            int resultado = -1;

            if (usuario != null)
            {
                string orden = $"INSERT INTO USUARIOS (ID, NOMBRE, CORREO, DIRECCION, CONTRASENA) " +
                                $"VALUES ({usuario.Id}, '{usuario.Nombre}', '{usuario.Correo}', " +
                                $"'{usuario.Direccion}', '{usuario.Contraseña}');";

                SqlCommand cmd = new SqlCommand(orden, conexion);

                try
                {
                    Abrirconexion();
                    resultado = cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new Exception("Error al intentar agregar un usuario", e);
                }
                finally
                {
                    Cerrarconexion();
                    cmd.Dispose();
                }
            }
            else
            {
                throw new ArgumentNullException("El usuario no puede ser nulo");
            }

            return resultado;
        }

        public Usuarios ObtenerUsuarioPorId(int id)
        {
            Usuarios usuario = null;
            string query = "SELECT * FROM USUARIOS WHERE ID = @ID";

            SqlCommand cmd = new SqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@ID", id);

            try
            {
                Abrirconexion();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    usuario = new Usuarios
                    {
                        Id = (int)reader["ID"],
                        Nombre = reader["NOMBRE"].ToString(),
                        Correo = reader["CORREO"].ToString(),
                        Direccion = reader["DIRECCION"].ToString(),
                        Contraseña = reader["CONTRASENA"].ToString()
                    };
                }
                reader.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Error al intentar obtener un usuario por ID", e);
            }
            finally
            {
                Cerrarconexion();
                cmd.Dispose();
            }

            return usuario;
        }

        public DataTable ObtenerTodosLosUsuarios()
        {
            DataTable dt = new DataTable();

            string query = "SELECT * FROM USUARIOS";

            SqlCommand cmd = new SqlCommand(query, conexion);

            try
            {
                Abrirconexion();
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener todos los usuarios", e);
            }
            finally
            {
                Cerrarconexion();
                cmd.Dispose();
            }

            return dt;
        }

        public Usuarios ObtenerUsuarioPorNombre(string nombreUsuario)
        {
            Usuarios usuario = null;
            string orden = "SELECT * FROM USUARIOS WHERE NOMBRE = @Nombre";
            SqlCommand cmd = new SqlCommand(orden, conexion);
            cmd.Parameters.AddWithValue("@Nombre", nombreUsuario);

            try
            {
                Abrirconexion();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    usuario = new Usuarios
                    {
                        Id = Convert.ToInt32(reader["ID"]),
                        Nombre = reader["NOMBRE"].ToString(),
                        Correo = reader["CORREO"].ToString(),
                        Direccion = reader["DIRECCION"].ToString(),
                        Contraseña = reader["CONTRASENA"].ToString()
                    };
                }
                reader.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Error al intentar obtener el usuario", e);
            }
            finally
            {
                Cerrarconexion();
                cmd.Dispose();
            }

            return usuario;
        }
    }
}

