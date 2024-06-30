using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace pryCosmetica
{
    public partial class frmBuscarPostulante : Form
    {
        public frmBuscarPostulante()
        {
            InitializeComponent();
            dgvGrilla.CellEndEdit += new DataGridViewCellEventHandler(dgvGrilla_CellEndEdit);
            btnModificar.Click += new EventHandler(btnModificar_Click);
            dgvGrilla.ReadOnly = true;
            dgvGrilla.CellClick += dgvGrilla_CellClick;
        }

        clsProcesosBD BD = new clsProcesosBD();
        OleDbDataReader lectorBD;

        private Guna.UI2.WinForms.Guna2TextBox txtDNI;
        private Guna.UI2.WinForms.Guna2TextBox txtNombre;
        private Guna.UI2.WinForms.Guna2TextBox txtTelefono;
        private Guna.UI2.WinForms.Guna2ComboBox cmbArea;

        private void cmbBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBusqueda.SelectedIndex != -1)
            {
                btnBuscar.Enabled = true;
            }
            
            clsProcesosBD objProcesos = new clsProcesosBD();
            sacarControl();
            int seleccion = cmbBusqueda.SelectedIndex;

            switch (seleccion)
            {
                case 0: //DNI
                    txtDNI = new Guna.UI2.WinForms.Guna2TextBox();
                    txtDNI.BorderRadius = 10;
                    txtDNI.Font = new Font("Bahnschrift", 11.25f, FontStyle.Regular);
                    txtDNI.ForeColor = Color.Black;
                    txtDNI.BorderColor = Color.FromArgb(51, 0, 51);
                    txtDNI.Visible = true;
                    this.Controls.Add(txtDNI);
                    this.ResumeLayout(false);
                    this.PerformLayout();
                    txtDNI.BringToFront();
                    txtDNI.Refresh();

                    txtDNI.Location = new Point(279, 50);
                    txtDNI.Size = new Size(200, 36);

                    // Agregar el manejador del evento KeyPress
                    txtDNI.KeyPress += new KeyPressEventHandler(txtDNI_KeyPress);
                    break;

                case 1: //Nombre
                    txtNombre = new Guna.UI2.WinForms.Guna2TextBox();
                    txtNombre.BorderRadius = 10;
                    txtNombre.Font = new Font("Bahnschrift", 11.25f, FontStyle.Regular);
                    txtNombre.ForeColor = Color.Black;
                    txtNombre.BorderColor = Color.FromArgb(51, 0, 51);
                    txtNombre.Visible = true;
                    this.Controls.Add(txtNombre);
                    this.ResumeLayout(false);
                    this.PerformLayout();
                    txtNombre.BringToFront();
                    txtNombre.Refresh();

                    txtNombre.Location = new Point(279, 50);
                    txtNombre.Size = new Size(200, 36);

                    // Agregar el manejador del evento KeyPress
                    txtNombre.KeyPress += new KeyPressEventHandler(txtNombre_KeyPress);
                    break;

                case 2: //Telefono
                    txtTelefono = new Guna.UI2.WinForms.Guna2TextBox();
                    txtTelefono.BorderRadius = 10;
                    txtTelefono.Font = new Font("Bahnschrift", 11.25f, FontStyle.Regular);
                    txtTelefono.ForeColor = Color.Black;
                    txtTelefono.BorderColor = Color.FromArgb(51, 0, 51);
                    txtTelefono.Visible = true;
                    this.Controls.Add(txtTelefono);
                    this.ResumeLayout(false);
                    this.PerformLayout();
                    txtTelefono.BringToFront();
                    txtTelefono.Refresh();

                    txtTelefono.Location = new Point(279, 50);
                    txtTelefono.Size = new Size(200, 36);

                    // Agregar el manejador del evento KeyPress
                    txtTelefono.KeyPress += new KeyPressEventHandler(txtTelefono_KeyPress);
                    break;

                case 3: //Area
                    cmbArea = new Guna.UI2.WinForms.Guna2ComboBox();
                    objProcesos.CargarAreaDeTrabajo(cmbArea);
                    cmbArea.BorderRadius = 10;
                    cmbArea.Font = new Font("Bahnschrift", 11.25f, FontStyle.Regular);
                    cmbArea.ForeColor = Color.Black;
                    cmbArea.BorderColor = Color.FromArgb(51, 0, 51);
                    cmbArea.Visible = true;
                    this.Controls.Add(cmbArea);
                    this.ResumeLayout(false);
                    this.PerformLayout();
                    cmbArea.BringToFront();
                    cmbArea.Refresh();

                    cmbArea.Location = new Point(279, 50);
                    cmbArea.Size = new Size(200, 36);

                    break;
            }
        }

        void sacarControl()
        {
            var controlsToRemove = new List<Control>();
            foreach (Control control in this.Controls)
            {
                if ((control is Guna.UI2.WinForms.Guna2TextBox ||
                     control is Guna.UI2.WinForms.Guna2ComboBox ||
                     control is Guna.UI2.WinForms.Guna2DateTimePicker) &&
                     control.Name != "cmbBusqueda") // Asegurarse de no eliminar el cmbFiltro
                {
                    controlsToRemove.Add(control);
                }
            }
            foreach (var control in controlsToRemove)
            {
                this.Controls.Remove(control);
                control.Dispose();
            }
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo dígitos y la tecla de retroceso (Backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo dígitos y la tecla de retroceso (Backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo letras y la tecla de retroceso (Backspace)
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            String Seleccionado = cmbBusqueda.SelectedItem.ToString();
            try
            {
                BD.conexion.ConnectionString = BD.varCadenaConexion;
                BD.conexion.Open();
                BD.comando = new OleDbCommand();

                BD.comando.Connection = BD.conexion;
                BD.comando.CommandType = System.Data.CommandType.Text;

                switch (Seleccionado)
                {
                    case "DNI":
                        BD.comando.CommandText = @"SELECT POSTULANTES.DNI, POSTULANTES.Nombre, POSTULANTES.Apellido, POSTULANTES.Correo, POSTULANTES.Telefono, POSTULANTES.CV, AREA.NombreArea
                                            FROM AREA INNER JOIN POSTULANTES ON AREA.[IdArea] = POSTULANTES.[IdArea]";

                        lectorBD = BD.comando.ExecuteReader();
                        dgvGrilla.Rows.Clear();
                        while (lectorBD.Read())
                        {
                            if (lectorBD["DNI"].ToString() == txtDNI.Text)
                            {
                                dgvGrilla.Rows.Add(
                                lectorBD["DNI"].ToString(),
                                lectorBD["Nombre"].ToString(),
                                lectorBD["Apellido"].ToString(),
                                lectorBD["Correo"].ToString(),
                                lectorBD["Telefono"].ToString(),
                                lectorBD["CV"].ToString(),
                                lectorBD["NombreArea"].ToString());
                            }

                        }

                        break;
                    case "Nombre":
                        BD.comando.CommandText = @"SELECT POSTULANTES.DNI, POSTULANTES.Nombre, POSTULANTES.Apellido, POSTULANTES.Correo, POSTULANTES.Telefono, POSTULANTES.CV, AREA.NombreArea
                                            FROM AREA INNER JOIN POSTULANTES ON AREA.[IdArea] = POSTULANTES.[IdArea]";
                        lectorBD = BD.comando.ExecuteReader();
                        dgvGrilla.Rows.Clear();
                        while (lectorBD.Read())
                        {
                            if (lectorBD["Nombre"].ToString() == txtNombre.Text)
                            {
                                dgvGrilla.Rows.Add(
                                lectorBD["DNI"].ToString(),
                                lectorBD["Nombre"].ToString(),
                                lectorBD["Apellido"].ToString(),
                                lectorBD["Correo"].ToString(),
                                lectorBD["Telefono"].ToString(),
                                lectorBD["CV"].ToString(),
                                lectorBD["NombreArea"].ToString());
                            }

                        }
                        break;
                    case "Telefono":
                        BD.comando.CommandText = @"SELECT POSTULANTES.DNI, POSTULANTES.Nombre, POSTULANTES.Apellido, POSTULANTES.Correo, POSTULANTES.Telefono, POSTULANTES.CV, AREA.NombreArea
                                            FROM AREA INNER JOIN POSTULANTES ON AREA.[IdArea] = POSTULANTES.[IdArea]";
                        lectorBD = BD.comando.ExecuteReader();
                        dgvGrilla.Rows.Clear();
                        while (lectorBD.Read())
                        {
                            if (lectorBD["Telefono"].ToString() == txtTelefono.Text)
                            {
                                dgvGrilla.Rows.Add(
                                lectorBD["DNI"].ToString(),
                                lectorBD["Nombre"].ToString(),
                                lectorBD["Apellido"].ToString(),
                                lectorBD["Correo"].ToString(),
                                lectorBD["Telefono"].ToString(),
                                lectorBD["CV"].ToString(),
                                lectorBD["NombreArea"].ToString());
                            }
                        }

                        break;
                    case "Area":

                        if (cmbArea.SelectedIndex != -1)
                        {
                            btnBuscar.Enabled = true;

                            string SeleccionadoArea = cmbArea.SelectedItem.ToString();
                            BD.comando.CommandText = @"SELECT POSTULANTES.DNI, POSTULANTES.Nombre, POSTULANTES.Apellido, POSTULANTES.Correo, POSTULANTES.Telefono, POSTULANTES.CV, AREA.NombreArea
                                            FROM AREA INNER JOIN POSTULANTES ON AREA.[IdArea] = POSTULANTES.[IdArea]";
                            lectorBD = BD.comando.ExecuteReader();
                            dgvGrilla.Rows.Clear();
                            while (lectorBD.Read())
                            {
                                if (lectorBD["NombreArea"].ToString() == SeleccionadoArea)
                                {
                                    dgvGrilla.Rows.Add(
                                    lectorBD["DNI"].ToString(),
                                    lectorBD["Nombre"].ToString(),
                                    lectorBD["Apellido"].ToString(),
                                    lectorBD["Correo"].ToString(),
                                    lectorBD["Telefono"].ToString(),
                                    lectorBD["CV"].ToString(),
                                    lectorBD["NombreArea"].ToString());
                                }

                            }
                        }
                        else
                        {
                            MessageBox.Show("No hay ningún área seleccionada.");
                        }
                        break;
                }


            }
            catch (Exception error)
            {

                MessageBox.Show(error.Message);
            }
            finally
            {
                if (lectorBD != null && !lectorBD.IsClosed)
                {
                    lectorBD.Close();
                }
                if (BD.conexion.State == System.Data.ConnectionState.Open)
                {
                    BD.conexion.Close();
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            dgvGrilla.ReadOnly = false;
            dgvGrilla.Columns[6].ReadOnly = true;
        }

        private void dgvGrilla_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int columnIndex = e.ColumnIndex;

            if (rowIndex >= 0 && columnIndex >= 0)
            {
                string NombreColumna = dgvGrilla.Columns[columnIndex].Name;
                object NuevoValor = dgvGrilla.Rows[rowIndex].Cells[columnIndex].Value;
                var ClavePrincipal = dgvGrilla.Rows[rowIndex].Cells["DNI"].Value;

                // Validar que el nombre de la columna sea seguro y permitido
                var columnasPermitidas = new List<string> { "Nombre", "Apellido", "Telefono", "Correo", "Direccion", "DNI" }; // Lista de columnas permitidas
                if (!columnasPermitidas.Contains(NombreColumna))
                {
                    MessageBox.Show("Nombre de columna no permitido.");
                    return;
                }


                DialogResult result = MessageBox.Show("¿Desea hacer esta modificación?", "Confirmar modificación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    ActualizarBase(ClavePrincipal, NombreColumna, NuevoValor);

                }
                else
                {
                    // Revertir el valor en la grilla si la modificación no es confirmada
                    dgvGrilla.CancelEdit();
                }
            }
        }

        private void ActualizarBase(object ClavePrincipal, string NombreColumna, object NuevoValor)
        {
            using (OleDbConnection conn = new OleDbConnection(BD.varCadenaConexion))
            {
                conn.Open();
                string query = $"UPDATE POSTULANTES SET [{NombreColumna}] = ? WHERE DNI = ?";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    // Agregar los parámetros en el orden correcto
                    cmd.Parameters.Add(new OleDbParameter("?", NuevoValor));
                    cmd.Parameters.Add(new OleDbParameter("?", ClavePrincipal));

                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            dgvGrilla.ReadOnly = true;
        }

        private void dgvGrilla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si se hizo clic en una celda válida
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                Int32 filaSeleccionada = Convert.ToInt32(dgvGrilla.CurrentCell.RowIndex);
                if (e.ColumnIndex == 5)
                {

                    string rutaPDF = dgvGrilla.Rows[filaSeleccionada].Cells[5].Value?.ToString();

                    if (!string.IsNullOrEmpty(rutaPDF))
                    {
                        try
                        {
                            // Intentar abrir el archivo PDF
                            Process.Start(rutaPDF);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al abrir el archivo PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void frmBuscarPostulante_Load(object sender, EventArgs e)
        {
            btnBuscar.Enabled = false;
            btnModificar.Enabled = false;
        }

        private void dgvGrilla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvGrilla != null)
            {
                btnModificar.Enabled = true;
            }
            else
            {
                btnModificar.Enabled = false;
            }
        }
    } 
}
