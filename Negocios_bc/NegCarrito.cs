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
    public class NegCarrito
    {
        DatosCarrito objDatosCarrito = new DatosCarrito();

        public bool AgregarProductoAlCarrito(int usuarioId, int libroId, int cantidad)
        {
            DatosCarrito datosCarrito = new DatosCarrito();
            return datosCarrito.AgregarAlCarrito(usuarioId, libroId, cantidad);
        }

        public int AbmCarrito(string accion, Carrito carrito)
        {
            return objDatosCarrito.AbmCarrito(accion, carrito);
        }

        public Carrito ObtenerCarritoPorUsuarioYLibro(int usuarioId, int libroId)
        {
            return objDatosCarrito.ObtenerCarritoPorUsuarioYLibro(usuarioId, libroId);
        }

        public DataTable ObtenerCarritoPorUsuario(int usuarioId)
        {
            return objDatosCarrito.ObtenerCarritoPorUsuario(usuarioId);
        }

        public int EliminarDelCarrito(int usuarioId, int libroId)
        {
            return objDatosCarrito.EliminarDelCarrito(usuarioId, libroId);
        }
    }
}
