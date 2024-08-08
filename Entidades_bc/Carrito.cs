using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_bc
{
    public class Carrito
    {
        #region Atributos
        private int usuarioId;
        private int libroId;
        private int cantidad;
        #endregion

        #region Constructor
        public Carrito()
        {
            usuarioId = 0;
            libroId = 0;
            cantidad = 0;
        }
        #endregion

        #region Propiedades/Encapsulamiento
        public int UsuarioId
        {
            get { return usuarioId; }
            set { usuarioId = value; }
        }

        public int LibroId
        {
            get { return libroId; }
            set { libroId = value; }
        }

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
        #endregion
    }

}

