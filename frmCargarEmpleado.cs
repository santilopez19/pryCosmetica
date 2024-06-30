using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryCosmetica
{
    public partial class frmCargarEmpleado : Form
    {
        public frmCargarEmpleado()
        {
            InitializeComponent();
        }

        public static string nombreEmpleado;
        public static string apellidoEmpleado;
        public static string cuil;
        public static string tipoDocumento;
        public static string numeroDocumentoEmpleado;
        public static string telefonoEmpleado;
        public static string estadoCivilEmpleado;
        public static string mailEmpleado;
        public static DateTime fechaNacimientoEmpleado;
        public static DateTime fechaIngresoEmpleado;
        public static string areaDeTrabajo;
        public static string tipoDeContrato;
        public static string categoria;
        public static string calle;
        public static string ciudad;
        public static string numeroCalle;
        public static string codPostal;
        public static string barrio;
        public static string filePath;

        private void btnCargarEmpleado_Click(object sender, EventArgs e)
        {
            nombreEmpleado = txtNombreEmpleado.Text;
            apellidoEmpleado = txtApellidoEmpleado.Text;
            cuil = txtILegajo.Text;
            estadoCivilEmpleado = lstEstadoCivil.SelectedItem.ToString();
            tipoDocumento = lstTipoDocumento.SelectedItem.ToString();
            numeroDocumentoEmpleado = txtNúmeroDocumento.Text;
            telefonoEmpleado = txtTeléfono.Text;
            mailEmpleado = txtMail.Text;
            fechaNacimientoEmpleado = dtpFechaNacimiento.Value;
            fechaIngresoEmpleado = dtpFechaIngreso.Value;
            areaDeTrabajo = lstAreaEmpleado.SelectedItem.ToString();
            tipoDeContrato = lstTipoContrato.SelectedItem.ToString();
            categoria = lstCategoría.SelectedItem.ToString();
            calle = lstCalle.SelectedItem.ToString();
            numeroCalle = txtNúmeroCalle.Text;
            ciudad = lstCiudad.SelectedItem.ToString();
            codPostal = txtCodPostal.Text;
            barrio = lstBarrio.SelectedItem.ToString();

            clsProcesosBD objProcesos = new clsProcesosBD();

            objProcesos.CargarEmpleado();

            objProcesos.CargarAreaEmpleado();
        }

        private void frmCargarEmpleado_Load(object sender, EventArgs e)
        {
            clsProcesosBD objProcesos = new clsProcesosBD();

            objProcesos.CargarTipoDocumento(lstTipoDocumento);
            objProcesos.CargarEstadoCivil(lstEstadoCivil);
            objProcesos.CargarCategoria(lstCategoría);
            objProcesos.CargarAreaDeTrabajo(lstAreaEmpleado);
            objProcesos.CargarTipoDeContrato(lstTipoContrato);
            objProcesos.CargarCalles(lstCalle);
            objProcesos.CargarCiudad(lstCiudad);
            objProcesos.CargarBarrio(lstBarrio);

            btnCargarEmpleado.Enabled = false;
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

        private void txtApellidoEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada no es una letra y no es una tecla de control (como backspace)
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Si no es una letra, cancela el evento
                e.Handled = true;
            }
        }

        private void txtILegajo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada no es un número y no es una tecla de control (como backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Si no es un número, cancela el evento
                e.Handled = true;
            }
        }

        private void txtNúmeroDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada no es un número y no es una tecla de control (como backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Si no es un número, cancela el evento
                e.Handled = true;
            }
        }

        private void txtTeléfono_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada no es un número y no es una tecla de control (como backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Si no es un número, cancela el evento
                e.Handled = true;
            }
        }

        private void txtNúmeroCalle_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada no es un número y no es una tecla de control (como backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Si no es un número, cancela el evento
                e.Handled = true;
            }
        }

        private void txtCodPostal_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada no es un número y no es una tecla de control (como backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Si no es un número, cancela el evento
                e.Handled = true;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombreEmpleado.Clear();
            txtApellidoEmpleado.Clear();
            txtILegajo.Clear();
            txtNúmeroDocumento.Clear();
            txtTeléfono.Clear();
            txtMail.Clear();
            txtNúmeroCalle.Clear();
            txtCodPostal.Clear();

            lstTipoDocumento.SelectedIndex = -1;
            lstEstadoCivil.SelectedIndex = -1;
            lstCategoría.SelectedIndex = -1;
            lstAreaEmpleado.SelectedIndex = -1;
            lstTipoContrato.SelectedIndex = -1;
            lstCalle.SelectedIndex = -1;
            lstCiudad.SelectedIndex = -1;
            lstBarrio.SelectedIndex = -1;

        }

        private void txtNombreEmpleado_TextChanged(object sender, EventArgs e)
        {
            if (txtNombreEmpleado.Text != "" & txtApellidoEmpleado.Text != "" & txtILegajo.Text != "" & txtNúmeroDocumento.Text != "" &
                txtTeléfono.Text != "" & txtMail.Text != "" & txtNúmeroCalle.Text != "" & txtCodPostal.Text != "" &
                lstTipoDocumento.SelectedIndex != -1 & lstEstadoCivil.SelectedIndex != -1 & lstCategoría.SelectedIndex != -1
                & lstAreaEmpleado.SelectedIndex != -1 & lstTipoContrato.SelectedIndex != -1 & lstCalle.SelectedIndex != -1
                & lstCiudad.SelectedIndex != -1 & lstBarrio.SelectedIndex != -1)
            {
                btnCargarEmpleado.Enabled = true;
            }
            else
            {
                btnCargarEmpleado.Enabled = false;
            }
        }

        private void txtApellidoEmpleado_TextChanged(object sender, EventArgs e)
        {
            if (txtNombreEmpleado.Text != "" & txtApellidoEmpleado.Text != "" & txtILegajo.Text != "" & txtNúmeroDocumento.Text != "" &
                txtTeléfono.Text != "" & txtMail.Text != "" & txtNúmeroCalle.Text != "" & txtCodPostal.Text != "" &
                lstTipoDocumento.SelectedIndex != -1 & lstEstadoCivil.SelectedIndex != -1 & lstCategoría.SelectedIndex != -1
                & lstAreaEmpleado.SelectedIndex != -1 & lstTipoContrato.SelectedIndex != -1 & lstCalle.SelectedIndex != -1
                & lstCiudad.SelectedIndex != -1 & lstBarrio.SelectedIndex != -1)
            {
                btnCargarEmpleado.Enabled = true;
            }
            else
            {
                btnCargarEmpleado.Enabled = false;
            }
        }

        private void txtILegajo_TextChanged(object sender, EventArgs e)
        {
            if (txtNombreEmpleado.Text != "" & txtApellidoEmpleado.Text != "" & txtILegajo.Text != "" & txtNúmeroDocumento.Text != "" &
                txtTeléfono.Text != "" & txtMail.Text != "" & txtNúmeroCalle.Text != "" & txtCodPostal.Text != "" &
                lstTipoDocumento.SelectedIndex != -1 & lstEstadoCivil.SelectedIndex != -1 & lstCategoría.SelectedIndex != -1
                & lstAreaEmpleado.SelectedIndex != -1 & lstTipoContrato.SelectedIndex != -1 & lstCalle.SelectedIndex != -1
                & lstCiudad.SelectedIndex != -1 & lstBarrio.SelectedIndex != -1)
            {
                btnCargarEmpleado.Enabled = true;
            }
            else
            {
                btnCargarEmpleado.Enabled = false;
            }
        }

        private void lstTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtNombreEmpleado.Text != "" & txtApellidoEmpleado.Text != "" & txtILegajo.Text != "" & txtNúmeroDocumento.Text != "" &
                txtTeléfono.Text != "" & txtMail.Text != "" & txtNúmeroCalle.Text != "" & txtCodPostal.Text != "" &
                lstTipoDocumento.SelectedIndex != -1 & lstEstadoCivil.SelectedIndex != -1 & lstCategoría.SelectedIndex != -1
                & lstAreaEmpleado.SelectedIndex != -1 & lstTipoContrato.SelectedIndex != -1 & lstCalle.SelectedIndex != -1
                & lstCiudad.SelectedIndex != -1 & lstBarrio.SelectedIndex != -1)
            {
                btnCargarEmpleado.Enabled = true;
            }
            else
            {
                btnCargarEmpleado.Enabled = false;
            }
        }

        private void txtNúmeroDocumento_TextChanged(object sender, EventArgs e)
        {
            if (txtNombreEmpleado.Text != "" & txtApellidoEmpleado.Text != "" & txtILegajo.Text != "" & txtNúmeroDocumento.Text != "" &
                txtTeléfono.Text != "" & txtMail.Text != "" & txtNúmeroCalle.Text != "" & txtCodPostal.Text != "" &
                lstTipoDocumento.SelectedIndex != -1 & lstEstadoCivil.SelectedIndex != -1 & lstCategoría.SelectedIndex != -1
                & lstAreaEmpleado.SelectedIndex != -1 & lstTipoContrato.SelectedIndex != -1 & lstCalle.SelectedIndex != -1
                & lstCiudad.SelectedIndex != -1 & lstBarrio.SelectedIndex != -1)
            {
                btnCargarEmpleado.Enabled = true;
            }
            else
            {
                btnCargarEmpleado.Enabled = false;
            }
        }

        private void txtTeléfono_TextChanged(object sender, EventArgs e)
        {
            if (txtNombreEmpleado.Text != "" & txtApellidoEmpleado.Text != "" & txtILegajo.Text != "" & txtNúmeroDocumento.Text != "" &
                txtTeléfono.Text != "" & txtMail.Text != "" & txtNúmeroCalle.Text != "" & txtCodPostal.Text != "" &
                lstTipoDocumento.SelectedIndex != -1 & lstEstadoCivil.SelectedIndex != -1 & lstCategoría.SelectedIndex != -1
                & lstAreaEmpleado.SelectedIndex != -1 & lstTipoContrato.SelectedIndex != -1 & lstCalle.SelectedIndex != -1
                & lstCiudad.SelectedIndex != -1 & lstBarrio.SelectedIndex != -1)
            {
                btnCargarEmpleado.Enabled = true;
            }
            else
            {
                btnCargarEmpleado.Enabled = false;
            }
        }

        private void lstEstadoCivil_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtNombreEmpleado.Text != "" & txtApellidoEmpleado.Text != "" & txtILegajo.Text != "" & txtNúmeroDocumento.Text != "" &
                txtTeléfono.Text != "" & txtMail.Text != "" & txtNúmeroCalle.Text != "" & txtCodPostal.Text != "" &
                lstTipoDocumento.SelectedIndex != -1 & lstEstadoCivil.SelectedIndex != -1 & lstCategoría.SelectedIndex != -1
                & lstAreaEmpleado.SelectedIndex != -1 & lstTipoContrato.SelectedIndex != -1 & lstCalle.SelectedIndex != -1
                & lstCiudad.SelectedIndex != -1 & lstBarrio.SelectedIndex != -1)
            {
                btnCargarEmpleado.Enabled = true;
            }
            else
            {
                btnCargarEmpleado.Enabled = false;
            }
        }

        private void txtMail_TextChanged(object sender, EventArgs e)
        {
            if (txtNombreEmpleado.Text != "" & txtApellidoEmpleado.Text != "" & txtILegajo.Text != "" & txtNúmeroDocumento.Text != "" &
                txtTeléfono.Text != "" & txtMail.Text != "" & txtNúmeroCalle.Text != "" & txtCodPostal.Text != "" &
                lstTipoDocumento.SelectedIndex != -1 & lstEstadoCivil.SelectedIndex != -1 & lstCategoría.SelectedIndex != -1
                & lstAreaEmpleado.SelectedIndex != -1 & lstTipoContrato.SelectedIndex != -1 & lstCalle.SelectedIndex != -1
                & lstCiudad.SelectedIndex != -1 & lstBarrio.SelectedIndex != -1)
            {
                btnCargarEmpleado.Enabled = true;
            }
            else
            {
                btnCargarEmpleado.Enabled = false;
            }
        }

        private void lstCategoría_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtNombreEmpleado.Text != "" & txtApellidoEmpleado.Text != "" & txtILegajo.Text != "" & txtNúmeroDocumento.Text != "" &
                txtTeléfono.Text != "" & txtMail.Text != "" & txtNúmeroCalle.Text != "" & txtCodPostal.Text != "" &
                lstTipoDocumento.SelectedIndex != -1 & lstEstadoCivil.SelectedIndex != -1 & lstCategoría.SelectedIndex != -1
                & lstAreaEmpleado.SelectedIndex != -1 & lstTipoContrato.SelectedIndex != -1 & lstCalle.SelectedIndex != -1
                & lstCiudad.SelectedIndex != -1 & lstBarrio.SelectedIndex != -1)
            {
                btnCargarEmpleado.Enabled = true;
            }
            else
            {
                btnCargarEmpleado.Enabled = false;
            }
        }

        private void lstAreaEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtNombreEmpleado.Text != "" & txtApellidoEmpleado.Text != "" & txtILegajo.Text != "" & txtNúmeroDocumento.Text != "" &
                txtTeléfono.Text != "" & txtMail.Text != "" & txtNúmeroCalle.Text != "" & txtCodPostal.Text != "" &
                lstTipoDocumento.SelectedIndex != -1 & lstEstadoCivil.SelectedIndex != -1 & lstCategoría.SelectedIndex != -1
                & lstAreaEmpleado.SelectedIndex != -1 & lstTipoContrato.SelectedIndex != -1 & lstCalle.SelectedIndex != -1
                & lstCiudad.SelectedIndex != -1 & lstBarrio.SelectedIndex != -1)
            {
                btnCargarEmpleado.Enabled = true;
            }
            else
            {
                btnCargarEmpleado.Enabled = false;
            }
        }

        private void lstTipoContrato_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtNombreEmpleado.Text != "" & txtApellidoEmpleado.Text != "" & txtILegajo.Text != "" & txtNúmeroDocumento.Text != "" &
                txtTeléfono.Text != "" & txtMail.Text != "" & txtNúmeroCalle.Text != "" & txtCodPostal.Text != "" &
                lstTipoDocumento.SelectedIndex != -1 & lstEstadoCivil.SelectedIndex != -1 & lstCategoría.SelectedIndex != -1
                & lstAreaEmpleado.SelectedIndex != -1 & lstTipoContrato.SelectedIndex != -1 & lstCalle.SelectedIndex != -1
                & lstCiudad.SelectedIndex != -1 & lstBarrio.SelectedIndex != -1)
            {
                btnCargarEmpleado.Enabled = true;
            }
            else
            {
                btnCargarEmpleado.Enabled = false;
            }
        }

        private void lstCalle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtNombreEmpleado.Text != "" & txtApellidoEmpleado.Text != "" & txtILegajo.Text != "" & txtNúmeroDocumento.Text != "" &
                txtTeléfono.Text != "" & txtMail.Text != "" & txtNúmeroCalle.Text != "" & txtCodPostal.Text != "" &
                lstTipoDocumento.SelectedIndex != -1 & lstEstadoCivil.SelectedIndex != -1 & lstCategoría.SelectedIndex != -1
                & lstAreaEmpleado.SelectedIndex != -1 & lstTipoContrato.SelectedIndex != -1 & lstCalle.SelectedIndex != -1
                & lstCiudad.SelectedIndex != -1 & lstBarrio.SelectedIndex != -1)
            {
                btnCargarEmpleado.Enabled = true;
            }
            else
            {
                btnCargarEmpleado.Enabled = false;
            }
        }

        private void lstCiudad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtNombreEmpleado.Text != "" & txtApellidoEmpleado.Text != "" & txtILegajo.Text != "" & txtNúmeroDocumento.Text != "" &
                txtTeléfono.Text != "" & txtMail.Text != "" & txtNúmeroCalle.Text != "" & txtCodPostal.Text != "" &
                lstTipoDocumento.SelectedIndex != -1 & lstEstadoCivil.SelectedIndex != -1 & lstCategoría.SelectedIndex != -1
                & lstAreaEmpleado.SelectedIndex != -1 & lstTipoContrato.SelectedIndex != -1 & lstCalle.SelectedIndex != -1
                & lstCiudad.SelectedIndex != -1 & lstBarrio.SelectedIndex != -1)
            {
                btnCargarEmpleado.Enabled = true;
            }
            else
            {
                btnCargarEmpleado.Enabled = false;
            }
        }

        private void lstBarrio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtNombreEmpleado.Text != "" & txtApellidoEmpleado.Text != "" & txtILegajo.Text != "" & txtNúmeroDocumento.Text != "" &
                txtTeléfono.Text != "" & txtMail.Text != "" & txtNúmeroCalle.Text != "" & txtCodPostal.Text != "" &
                lstTipoDocumento.SelectedIndex != -1 & lstEstadoCivil.SelectedIndex != -1 & lstCategoría.SelectedIndex != -1
                & lstAreaEmpleado.SelectedIndex != -1 & lstTipoContrato.SelectedIndex != -1 & lstCalle.SelectedIndex != -1
                & lstCiudad.SelectedIndex != -1 & lstBarrio.SelectedIndex != -1)
            {
                btnCargarEmpleado.Enabled = true;
            }
            else
            {
                btnCargarEmpleado.Enabled = false;
            }
        }

        private void txtNúmeroCalle_TextChanged(object sender, EventArgs e)
        {
            if (txtNombreEmpleado.Text != "" & txtApellidoEmpleado.Text != "" & txtILegajo.Text != "" & txtNúmeroDocumento.Text != "" &
                txtTeléfono.Text != "" & txtMail.Text != "" & txtNúmeroCalle.Text != "" & txtCodPostal.Text != "" &
                lstTipoDocumento.SelectedIndex != -1 & lstEstadoCivil.SelectedIndex != -1 & lstCategoría.SelectedIndex != -1
                & lstAreaEmpleado.SelectedIndex != -1 & lstTipoContrato.SelectedIndex != -1 & lstCalle.SelectedIndex != -1
                & lstCiudad.SelectedIndex != -1 & lstBarrio.SelectedIndex != -1)
            {
                btnCargarEmpleado.Enabled = true;
            }
            else
            {
                btnCargarEmpleado.Enabled = false;
            }
        }

        private void txtCodPostal_TextChanged(object sender, EventArgs e)
        {
            if (txtNombreEmpleado.Text != "" & txtApellidoEmpleado.Text != "" & txtILegajo.Text != "" & txtNúmeroDocumento.Text != "" &
                txtTeléfono.Text != "" & txtMail.Text != "" & txtNúmeroCalle.Text != "" & txtCodPostal.Text != "" &
                lstTipoDocumento.SelectedIndex != -1 & lstEstadoCivil.SelectedIndex != -1 & lstCategoría.SelectedIndex != -1
                & lstAreaEmpleado.SelectedIndex != -1 & lstTipoContrato.SelectedIndex != -1 & lstCalle.SelectedIndex != -1
                & lstCiudad.SelectedIndex != -1 & lstBarrio.SelectedIndex != -1)
            {
                btnCargarEmpleado.Enabled = true;
            }
            else
            {
                btnCargarEmpleado.Enabled = false;
            }
        }
    }
}
