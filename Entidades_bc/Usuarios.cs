using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_bc
{
    public class Usuarios
    {
        #region Atributos
        private int id;
        private string nombre;
        private string correo;
        private string direccion;
        private string contraseña;
        #endregion

        #region Constructor
        public Usuarios()
        {
            id = 0;
            nombre = string.Empty;
            correo = string.Empty;
            direccion = string.Empty;
            contraseña = string.Empty;
        }
        #endregion

        #region Propiedades/Encapsulamiento
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Correo
        {
            get { return correo; }
            set { correo = value; }
        }

        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        public string Contraseña
        {
            get { return contraseña; }
            set { contraseña = value; }
        }
        #endregion
    }
}

