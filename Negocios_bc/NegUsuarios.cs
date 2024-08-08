using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Datos_bc;
using Entidades_bc;

namespace Negocio_bc
{
    public class NegUsuarios
    {
        private Datos_bc.DatosUsuarios objDatosUsuarios = new Datos_bc.DatosUsuarios(); // Asegúrate de tener la referencia a Datos_

        public int AltaUsuario(Usuarios usuario)
        {
            DatosUsuarios objDatosUsuarios = new DatosUsuarios();
            return objDatosUsuarios.AltaUsuario(usuario);
        }

        public Usuarios ObtenerUsuarioPorId(int id)
        {
            return objDatosUsuarios.ObtenerUsuarioPorId(id);
        }

        // Otros métodos según sea necesario

        public DataTable ObtenerTodosLosUsuarios()
        {
            // Aquí podrías implementar lógica adicional si fuera necesaria
            return objDatosUsuarios.ObtenerTodosLosUsuarios();
        }
        public Usuarios ObtenerUsuarioPorNombre(string nombreUsuario)
        {
            DatosUsuarios objDatosUsuarios = new DatosUsuarios();
            return objDatosUsuarios.ObtenerUsuarioPorNombre(nombreUsuario);
        }
    }
}

