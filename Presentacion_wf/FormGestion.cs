using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio_bc;
using Entidades_bc;

namespace Presentacion_wf
{
    public partial class FormGestion : Form
    {
        NegLibros negLibros = new NegLibros();
        NegCarrito negCarrito = new NegCarrito();

        public FormGestion()
        {
            InitializeComponent();
            CargarDatosLibros();
            CargarDatosCarrito();
        }

        private void CargarDatosLibros()
        {
            try
            {
                DataSet dsLibros = negLibros.ObtenerTodosLosLibros();
                dgvLibros.DataSource = dsLibros.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los libros: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDatosCarrito()
        {
            try
            {
                DataTable dtCarrito = negCarrito.ObtenerCarritoPorUsuario(1); // Suponiendo que el usuario actual es el ID 1
                dgvCarrito.DataSource = dtCarrito;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el carrito: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregarAlCarrito_Click(object sender, EventArgs e)
        {
            try
            {
                int libroId = Convert.ToInt32(dgvLibros.CurrentRow.Cells["ID"].Value);
                int cantidad = (int)nudCantidad.Value;

                Carrito carrito = new Carrito
                {
                    UsuarioId = 1, // Suponiendo que el usuario actual es el ID 1
                    LibroId = libroId,
                    Cantidad = cantidad
                };

                negCarrito.AgregarAlCarrito(carrito);
                MessageBox.Show("Libro agregado al carrito correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatosCarrito();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar al carrito: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnComprar_Click(object sender, EventArgs e)
        {
            try
            {
                // Lógica para realizar la compra
                // Aquí se eliminaría el libro del carrito y se mostraría un mensaje
                int libroId = Convert.ToInt32(dgvCarrito.CurrentRow.Cells["LIBRO_ID"].Value);
                int cantidad = (int)dgvCarrito.CurrentRow.Cells["CANTIDAD"].Value;

                negCarrito.EliminarDelCarrito(1, libroId); // Suponiendo que el usuario actual es el ID 1
                negLibros.ModificarStockLibro(libroId, cantidad); // Devolver el stock al libro

                MessageBox.Show("Compra realizada. El envío llegará dentro de 5 días hábiles.", "Compra Realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarDatosLibros();
                CargarDatosCarrito();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al realizar la compra: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarDelCarrito_Click(object sender, EventArgs e)
        {
            try
            {
                int libroId = Convert.ToInt32(dgvCarrito.CurrentRow.Cells["LIBRO_ID"].Value);

                negCarrito.EliminarDelCarrito(1, libroId); // Suponiendo que el usuario actual es el ID 1

                MessageBox.Show("Libro eliminado del carrito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarDatosCarrito();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar del carrito: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
