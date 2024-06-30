using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryCosmetica
{
    public partial class frmInicioSesion : Form
    {
        public frmInicioSesion()
        {
            InitializeComponent();
        }

        clsProcesosBD BD = new clsProcesosBD();
        OleDbDataReader lectorBD;

        private void timerCargaPrograma_Tick(object sender, EventArgs e)
        {
            
            timerCargaPrograma.Stop(); // Detiene el Timer
            ProgressBar progressBar1 = timerCargaPrograma.Tag as ProgressBar;
            if (progressBar1 != null)
            {
                this.Controls.Remove(progressBar1); // Elimina el ProgressBar del formulario
                progressBar1.Dispose(); // Libera los recursos del ProgressBar
            }
            

            frmMenuPrincipal menuPrincipal = new frmMenuPrincipal();
            menuPrincipal.Show();
            this.Hide(); // Oculta el formulario actual           
        }



        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {                    
            try
            {
                BD.conexion.ConnectionString = BD.varCadenaConexion;
                BD.conexion.Open();
                BD.comando = new OleDbCommand();

                BD.comando.Connection = BD.conexion;
                BD.comando.CommandType = System.Data.CommandType.TableDirect;
                BD.comando.CommandText = "INICIOSESION";

                lectorBD = BD.comando.ExecuteReader();

                if (lectorBD.HasRows)
                {
                    while (lectorBD.Read())
                    {
                        if (lectorBD[0].ToString() == txtCuil.Text && lectorBD[1].ToString() == txtContraseña.Text)
                        {
                            timerCargaPrograma.Start(); // Inicia el Timer  
                        }
                        else
                        {
                            MessageBox.Show("Usuario y/o contraseña incorrecto.");
                        }

                    }
                }

            }
            catch (Exception error)
            {

                MessageBox.Show(error.Message);
            }
            lectorBD.Close();

            BD.conexion.Close();
        }

        private void txtCuil_Click(object sender, EventArgs e)
        {
            txtCuil.Clear();
            txtCuil.ForeColor = Color.Black;
        }

        private void txtContraseña_Click(object sender, EventArgs e)
        {
            txtContraseña.Clear();
            txtContraseña.ForeColor = Color.Black;
            txtContraseña.Font = new Font("Bahnschrift", 10f, FontStyle.Regular);
            txtContraseña.PasswordChar = '●';
        }

        private void txtCuil_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada no es un número y no es una tecla de control (como backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Si no es un número, cancela el evento
                e.Handled = true;
            }
           
            // Si se presiona la tecla Enter
            if (e.KeyChar == (char)Keys.Enter)
            {
                // Evitar el sonido 'ding'
                e.Handled = true;

                // Seleccionar el siguiente control
                txtContraseña.Clear();
                txtContraseña.ForeColor = Color.Black;
                txtContraseña.Font = new Font("Bahnschrift", 10f, FontStyle.Regular);
                txtContraseña.PasswordChar = '●';
                txtContraseña.Focus();
            }
        }

        private void txtContraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Si se presiona la tecla Enter
            if (e.KeyChar == (char)Keys.Enter)
            {
                // Evitar el sonido 'ding'
                e.Handled = true;

                try
                {
                    BD.conexion.ConnectionString = BD.varCadenaConexion;
                    BD.conexion.Open();
                    BD.comando = new OleDbCommand();

                    BD.comando.Connection = BD.conexion;
                    BD.comando.CommandType = System.Data.CommandType.TableDirect;
                    BD.comando.CommandText = "INICIOSESION";

                    lectorBD = BD.comando.ExecuteReader();

                    if (lectorBD.HasRows)
                    {
                        while (lectorBD.Read())
                        {
                            if (lectorBD[0].ToString() == txtCuil.Text && lectorBD[1].ToString() == txtContraseña.Text)
                            {
                                timerCargaPrograma.Start(); // Inicia el Timer  
                            }
                            else
                            {
                                MessageBox.Show("usuario no existente, Usuario y/o contraseña incorrecto");
                            }

                        }
                    }

                }
                catch (Exception error)
                {

                    MessageBox.Show(error.Message);
                }
                lectorBD.Close();

                BD.conexion.Close();
            }
        }

        private void txtCuil_PreviewKeyDown_1(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                // Cancela la tecla Tab
                e.IsInputKey = true;
            }
        }

        private void txtContraseña_PreviewKeyDown_1(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                // Cancela la tecla Tab
                e.IsInputKey = true;
            }
        }

        private void txtCuil_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.Handled = true; // Evita el sonido
                e.SuppressKeyPress = true; // Evita el sonido
            }
        }

        private void txtContraseña_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.Handled = true; // Evita el sonido
                e.SuppressKeyPress = true; // Evita el sonido
            }
        }
    }
}
