using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryCosmetica
{
    public partial class frmCargarPostulante : Form
    {

        private clsProcesosBD procesosBD;

        public frmCargarPostulante()
        {
            InitializeComponent();

            procesosBD = new clsProcesosBD();
            procesosBD.conexion.ConnectionString = procesosBD.varCadenaConexion;
            CargarComboBoxAreas();
        }

        private void txtNombrePostulante_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada no es una letra y no es una tecla de control (como backspace)
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Si no es una letra, cancela el evento
                e.Handled = true;
            }
        }

        private void txtApellidoPostulante_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada no es una letra y no es una tecla de control (como backspace)
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Si no es una letra, cancela el evento
                e.Handled = true;
            }
        }

        private void txtDNIPostulante_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada no es un número y no es una tecla de control (como backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Si no es un número, cancela el evento
                e.Handled = true;
            }
        }

        private void txtTeléfonoPostulante_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si la tecla presionada no es un número y no es una tecla de control (como backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Si no es un número, cancela el evento
                e.Handled = true;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Obtener los valores de los campos del formulario
            string nombre = txtNombrePostulante.Text.Trim();
            string apellido = txtApellidoPostulante.Text.Trim();
            string correo = txtCorreo.Text.Trim();
            string dni = txtDNIPostulante.Text.Trim();
            string telefono = txtTeléfonoPostulante.Text.Trim();
            string areaSeleccionada = cmbArea.SelectedValue.ToString();

            // Validar que todos los campos obligatorios estén completos
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) ||
                string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(dni) ||
                string.IsNullOrEmpty(telefono) || string.IsNullOrEmpty(areaSeleccionada))
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Guardar el archivo de CV
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf|Todos los archivos (*.*)|*.*";
            openFileDialog.Title = "Seleccione el archivo del CV";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string nombreArchivo = Path.GetFileName(openFileDialog.FileName);
                string rutaDestino = Path.Combine(Application.StartupPath, "CVpostulantes", nombreArchivo);

                // Verificar si la carpeta CVpostulantes existe, si no, crearla
                string carpetaCV = Path.Combine(Application.StartupPath, "CVpostulantes");
                if (!Directory.Exists(carpetaCV))
                {
                    Directory.CreateDirectory(carpetaCV);
                }

                // Copiar el archivo seleccionado a la carpeta de destino
                File.Copy(openFileDialog.FileName, rutaDestino, true);

                // Insertar los datos en la base de datos
                string consulta = "INSERT INTO POSTULANTES (DNI, Nombre, Apellido, Correo, Telefono, CV, IdArea) " +
                                  "VALUES (@dni, @nombre, @apellido, @correo, @telefono, @cv, @idArea)";

                OleDbCommand cmd = new OleDbCommand(consulta, procesosBD.conexion);
                cmd.Parameters.AddWithValue("@dni", dni);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@apellido", apellido);
                cmd.Parameters.AddWithValue("@correo", correo);
                cmd.Parameters.AddWithValue("@telefono", telefono);
                cmd.Parameters.AddWithValue("@cv", rutaDestino);
                cmd.Parameters.AddWithValue("@idArea", areaSeleccionada);

                try
                {
                    procesosBD.conexion.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Datos guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    procesosBD.conexion.Close();
                }
            }
        }

        private void CargarComboBoxAreas()
        {
            string consulta = "SELECT IdArea, NombreArea FROM AREA";
            OleDbDataAdapter adapter = new OleDbDataAdapter(consulta, procesosBD.conexion);
            DataTable dtAreas = new DataTable();

            try
            {
                procesosBD.conexion.Open();
                adapter.Fill(dtAreas);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las áreas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                procesosBD.conexion.Close();
            }

            // Asignar datos al ComboBox
            cmbArea.DataSource = dtAreas;
            cmbArea.DisplayMember = "NombreArea";
            cmbArea.ValueMember = "IdArea";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtApellidoPostulante.Clear();
            txtNombrePostulante.Clear();
            txtDNIPostulante.Clear();
            txtCorreo.Clear();
            txtTeléfonoPostulante.Clear();
            cmbArea.SelectedIndex = -1;
        }
    }
}
