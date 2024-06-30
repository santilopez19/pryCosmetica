using Guna.UI2.WinForms;
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
    public partial class frmCargarReporte : Form
    {
        public frmCargarReporte()
        {
            InitializeComponent();
            OcultarGroupBox();
        }

        public String tipo = "";
        public clsProcesosBD conector = new clsProcesosBD();
        private void OcultarGroupBox()
        {
            mrcInasistencia.Visible = false;
            mrcAmonestación.Visible = false;
            mrcSuspensión.Visible = false;
            mrcEvaluaciónDesempeño.Visible = false;
            mrcDespido.Visible = false;
        }

        private void btnInasistencia_Click(object sender, EventArgs e)
        {
            OcultarGroupBox();
            mrcInasistencia.Visible = true;
            tipo = "I";
        }

        private void btnAmonestaciones_Click(object sender, EventArgs e)
        {
            OcultarGroupBox();
            mrcAmonestación.Visible = true;
            tipo = "A";
            //MessageBox.Show(conector.estadoConexion);
        }

        private void btnSuspensiones_Click(object sender, EventArgs e)
        {
            OcultarGroupBox();
            mrcSuspensión.Visible = true;
            tipo = "S";
        }

        private void btnEvaluaciónDesempeño_Click(object sender, EventArgs e)
        {
            OcultarGroupBox();
            mrcEvaluaciónDesempeño.Visible = true;
            tipo = "E";
        }

        private void btnDespido_Click(object sender, EventArgs e)
        {
            OcultarGroupBox();
            mrcDespido.Visible = true;
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

        private void txtDocumentoEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada no es un número y no es una tecla de control (como backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Si no es un número, cancela el evento
                e.Handled = true;
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

        private void txtMotivoInasistencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada no es una letra y no es una tecla de control (como backspace)
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Si no es una letra, cancela el evento
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (mrcInasistencia.Visible)
            {
                txtNombreInasistencia.Clear();
                txtMotivoInasistencia.Clear();
                lstTipoInasistencia.SelectedIndex = -1;
            }

            if (mrcAmonestación.Visible)
            {
                txtNombreAmonestacion.Clear();
                lstMotivoAmonestación.SelectedIndex = -1;
            }

            if (mrcSuspensión.Visible)
            {
                txtNombreSuspensión.Clear();
                txtObservación.Clear();
                lstMotivoSuspensión.SelectedIndex = -1;
            }

            if (mrcEvaluaciónDesempeño.Visible)
            {
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

       /* private void btnCargar_Click(object sender, EventArgs e)
        {
            switch (tipo)
            {
                case "I":
                    //Bloque  inasistencias
                    conector.cargarInasistencia(txtNombreInasistencia.Text, dgvFechaInasistencia.Value, Convert.ToInt32(txtMotivoInasistencia.Text),optJustificada.Checked, lstTipoInasistencia.SelectedIndex + 1);
                    MessageBox.Show(conector.estadoConexion);
                    break;
                case "A":
                    //Bloque  Amonestaciones
                    conector.cargarAmonestacion(Convert.ToInt32(lstMotivoAmonestación.Text);
                    MessageBox.Show(conector.estadoConexion);
                    break;
                case "S":
                    //Bloque  Suspenciones
                    conector.cargarSuspencion(gbMotivoSuspensión.SelectedIndex + 1, txtNombreSuspensión.Text, gptFechaInicio.Value, gptFechaFinalización.Value, txtObservación.Text);
                    MessageBox.Show(conector.estadoConexion);
                    break;
                case "E":
                    //Bloque  Evaluacion
                    conector.cargarEvaluacion(txtNombreEvaluaciónDesempeño.Text, dtpFechaEvaluacion.Value, Convert.ToInt32(txtCalificación.Text), cbÁreaEmpleado.SelectedIndex + 1, txtObservaciónes.Text, txtNombreEvaluador.Text);
                    MessageBox.Show(conector.estadoConexion);
                    break;
                default:
                    MessageBox.Show("No se a seleccionado un tipo de reporte");
                    break;
            }
        */}
    }

