using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos_bc;

namespace Negocio_bc
{
    public class NegLibros
    {
        DatosLibros objDatosLibros = new DatosLibros();

        public DataSet ObtenerTodosLosLibros()
        {
            return objDatosLibros.ObtenerTodosLosLibros();
        }

        public int ModificarStockLibro(int idLibro, int nuevoStock)
        {
            return objDatosLibros.ModificarStockLibro(idLibro, nuevoStock);
        }
    }
}
