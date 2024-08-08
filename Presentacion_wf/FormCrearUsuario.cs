using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades_bc;
using Negocio_bc;

namespace Presentacion_wf
{
    public partial class FormCrearUsuario : Form
    {
        private FormInicioSesion formInicioSesion;

        public FormCrearUsuario(FormInicioSesion inicioSesion)
        {
            InitializeComponent();
            formInicioSesion = inicioSesion;
        }

        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            Usuarios nuevoUsuario = new Usuarios
            {
                Nombre = txtNuevoNombre.Text,
                Correo = txtNuevoCorreo.Text,
                Direccion = txtNuevaDireccion.Text,
                Contraseña = txtNuevaContraseña.Text
            };

            NegUsuarios negUsuarios = new NegUsuarios();
            int resultado = negUsuarios.AltaUsuario(nuevoUsuario);

            if (resultado > 0)
            {
                MessageBox.Show("Usuario creado con éxito.", "Creación de usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                formInicioSesion.Show(); // Muestra el formulario de inicio de sesión nuevamente
            }
            else
            {
                MessageBox.Show("Error al crear el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelarUsuario_Click(object sender, EventArgs e)
        {
            this.Close();
            formInicioSesion.Show(); // Muestra el formulario de inicio de sesión nuevamente
        }
    }
}
