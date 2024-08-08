using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_bc
{
    public class Libros
    {
        #region Atributos
        private int id;
        private string titulo;
        private string autor;
        private string genero;
        private decimal precio;
        private int stock;
        #endregion

        #region Constructor
        public Libros()
        {
            id = 0;
            titulo = string.Empty;
            autor = string.Empty;
            genero = string.Empty;
            precio = 0;
            stock = 0;
        }
        #endregion

        #region Propiedades/Encapsulamiento
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Titulo
        {
            get { return titulo; }
            set { titulo = value; }
        }

        public string Autor
        {
            get { return autor; }
            set { autor = value; }
        }

        public string Genero
        {
            get { return genero; }
            set { genero = value; }
        }

        public decimal Precio
        {
            get { return precio; }
            set { precio = value; }
        }

        public int Stock
        {
            get { return stock; }
            set { stock = value; }
        }
        #endregion
    }
}

