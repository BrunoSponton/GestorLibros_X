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
    public partial class FormInicioSesion : Form
    {
        public FormInicioSesion()
        {
            InitializeComponent();
        }

        public static class UsuarioSesion
        {
            public static int UsuarioId { get; set; }
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            IniciarSesion();
        }

        private void IniciarSesion()
        {
            string nombreUsuario = txtNombreUsuario.Text;
            string contrasena = txtContraseña.Text;

            int usuarioId = ValidarUsuario(nombreUsuario, contrasena);
            if (usuarioId != -1)
            {
                UsuarioSesion.UsuarioId = usuarioId;

                FormGestion formPrincipal = new FormGestion();
                formPrincipal.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Nombre de usuario o contraseña incorrectos.", "Error de inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int ValidarUsuario(string nombreUsuario, string contrasena)
        {
            NegUsuarios negUsuarios = new NegUsuarios();
            Usuarios usuario = negUsuarios.ObtenerUsuarioPorNombre(nombreUsuario);

            if (usuario != null && usuario.Contraseña == contrasena)
            {
                return usuario.Id;
            }
            else
            {
                return -1;
            }
        }

        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            FormCrearUsuario formCrearUsuario = new FormCrearUsuario(this);
            formCrearUsuario.Show();
            this.Hide();
        }
    }
}
