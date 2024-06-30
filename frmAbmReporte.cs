using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryCosmetica
{
    public partial class frmAbmReporte : Form
    {

        string cuilViejo;
        public frmAbmReporte(string tipoReporte)
        {
            InitializeComponent();
            MostrarGroupBox(tipoReporte);
        }

        private void MostrarGroupBox(string tipoReporte)
        {
            // Ocultar todos los GroupBox
            mrcInasistencia.Visible = false;
            mrcSuspensión.Visible = false;
            mrcEvaluaciónDesempeño.Visible = false;
            mrcAmonestación.Visible = false;

            // Mostrar el GroupBox correspondiente
            switch (tipoReporte)
            {
                case "Inasistencias":
                    mrcInasistencia.Visible = true;
                    break;
                case "Amonestaciones":
                    mrcAmonestación.Visible = true;
                    break;
                case "Suspensiones":
                    mrcSuspensión.Visible = true;
                    break;
                case "Evaluacion de Desempeño":
                    mrcEvaluaciónDesempeño.Visible = true;
                    break;
                case "Despidos":
                    mrcDespido.Visible = true;
                    break;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (mrcInasistencia.Visible)
            {
                txtCuilInasistencia.Clear();
                txtNombreInasistencia.Clear();
                txtMotivoInasistencia.Clear();
                lstTipoInasistencia.SelectedIndex = -1;
            }

            if (mrcAmonestación.Visible)
            {
                txtCuilAmonestacion.Clear();
                txtNombreAmonestacion.Clear();
                lstMotivoAmonestación.SelectedIndex = -1;
            }

            if (mrcSuspensión.Visible)
            {
                txtCuilSuspencion.Clear();
                txtNombreSuspensión.Clear();
                txtObservación.Clear();
                lstMotivoSuspensión.SelectedIndex = -1;
            }

            if (mrcEvaluaciónDesempeño.Visible)
            {
                txtCuilEvaluacion.Clear();
                txtNombreEmpleado.Clear();
                txtNombreEvaluador.Clear();
                txtCalificación.Clear();
                txtObservaciónEvaluación.Clear();
                lstArea.SelectedIndex = -1;
            }
            if (mrcDespido.Visible)
            {
                txtDocumentoEmpleado.Clear();
            }
        }

        private void txtNombreInasistencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada no es una letra y no es una tecla de control (como backspace)
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Si no es una letra, cancela el evento
                e.Handled = true;
            }
        }

        private void txtCuilInasistencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada no es un número y no es una tecla de control (como backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Si no es un número, cancela el evento
                e.Handled = true;
            }
        }

        private void txtMotivoInasistencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada no es una letra y no es una tecla de control (como backspace)
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Si no es una letra, cancela el evento
                e.Handled = true;
            }
        }

        private void txtCuilSuspencion_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada no es un número y no es una tecla de control (como backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Si no es un número, cancela el evento
                e.Handled = true;
            }
        }

        private void txtNombreSuspensión_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada no es una letra y no es una tecla de control (como backspace)
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Si no es una letra, cancela el evento
                e.Handled = true;
            }
        }

        private void txtObservación_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada no es una letra y no es una tecla de control (como backspace)
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Si no es una letra, cancela el evento
                e.Handled = true;
            }
        }

        private void txtCuilEvaluacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada no es un número y no es una tecla de control (como backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Si no es un número, cancela el evento
                e.Handled = true;
            }
        }

        private void txtNombreEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada no es una letra y no es una tecla de control (como backspace)
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Si no es una letra, cancela el evento
                e.Handled = true;
            }
        }

        private void txtNombreEvaluador_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada no es una letra y no es una tecla de control (como backspace)
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Si no es una letra, cancela el evento
                e.Handled = true;
            }
        }

        private void txtCalificación_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada no es un número y no es una tecla de control (como backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Si no es un número, cancela el evento
                e.Handled = true;
            }
        }

        private void txtObservaciónEvaluación_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada no es una letra y no es una tecla de control (como backspace)
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Si no es una letra, cancela el evento
                e.Handled = true;
            }
        }

        private void txtCuilAmonestacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada no es un número y no es una tecla de control (como backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Si no es un número, cancela el evento
                e.Handled = true;
            }
        }

        private void txtNombreAmonestacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada no es una letra y no es una tecla de control (como backspace)
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Si no es una letra, cancela el evento
                e.Handled = true;
            }
        }

        private void txtDocumentoEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada no es un número y no es una tecla de control (como backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Si no es un número, cancela el evento
                e.Handled = true;
            }
        }
        public void SetValues(string cuil, string nombre, string motivo, string justificado, string tipoInasistencia, string fecha)
        {
            txtDocumentoEmpleado.Text = cuil;


        }
        clsProcesosBD BD = new clsProcesosBD();

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }
    }
}
