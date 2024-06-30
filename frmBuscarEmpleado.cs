using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace pryCosmetica
{
    public partial class frmBuscarEmpleado : Form
    {
        public frmBuscarEmpleado()
        {
            InitializeComponent();
        }
        private Guna2TextBox txtGuna;
        private Guna2ComboBox cmbGuna;
        private Guna2DateTimePicker dtpGuna;
        string Campo;
        string Tabla;
        string VarTexto;
        DateTime Fecha;
        string ComandoSQL;
        string Seleccionado;
        clsProcesosBD BD = new clsProcesosBD();
        private void cmbFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFiltro.SelectedItem != null)
            {
                string selectedValue = cmbFiltro.SelectedItem.ToString();
                Seleccionado = cmbFiltro.SelectedItem.ToString();

                limpiarControles();

                if (selectedValue == "Nombre")
                {
                    txtGuna = new Guna.UI2.WinForms.Guna2TextBox();
                    txtGuna.Font = new Font("Bahnschrift", 11.25f, FontStyle.Regular);
                    txtGuna.ForeColor = Color.Black;
                    txtGuna.BorderRadius = 10;
                    txtGuna.BorderColor = Color.FromArgb(51, 0, 51);
                    txtGuna.Visible = true;
                    this.Controls.Add(txtGuna);
                    this.ResumeLayout(false);
                    this.PerformLayout();
                    txtGuna.BringToFront();
                    txtGuna.Refresh();

                    // Ajustar ubicación y tamaño después de agregar el control
                    txtGuna.Location = new Point(297, 29);
                    txtGuna.Size = new Size(200, 36);
                    Campo = "Nombre";
                    Tabla = "EMPLEADO";
                    // Agregar el manejador del evento KeyPress
                    txtGuna.KeyPress += new KeyPressEventHandler(txtNombre_KeyPress);
                }
                else if (selectedValue == "CUIL")
                {
                    txtGuna = new Guna.UI2.WinForms.Guna2TextBox();
                    txtGuna.Font = new Font("Bahnschrift", 11.25f, FontStyle.Regular);
                    txtGuna.ForeColor = Color.Black;
                    txtGuna.BorderRadius = 10;
                    txtGuna.BorderColor = Color.FromArgb(51, 0, 51);
                    txtGuna.Visible = true;
                    this.Controls.Add(txtGuna);
                    this.ResumeLayout(false);
                    this.PerformLayout();
                    txtGuna.BringToFront();
                    txtGuna.Refresh();

                    // Ajustar ubicación y tamaño después de agregar el control
                    txtGuna.Location = new Point(297, 29);
                    txtGuna.Size = new Size(200, 36);

                    // Agregar el manejador del evento KeyPress
                    txtGuna.KeyPress += new KeyPressEventHandler(txtDocumento_KeyPress);
                    Campo = "Cuil";
                    Tabla = "EMPLEADO";
                }
                else if (selectedValue == "Estado Civil")
                {
                    cmbGuna = new Guna.UI2.WinForms.Guna2ComboBox();
                    cmbGuna.Location = new Point(297, 29);
                    cmbGuna.Size = new Size(200, 36);
                    cmbGuna.Font = new Font("Bahnschrift", 11.25f, FontStyle.Regular);
                    cmbGuna.ForeColor = Color.Black;
                    cmbGuna.BorderRadius = 10;
                    cmbGuna.BorderColor = Color.FromArgb(51, 0, 51);
                    cmbGuna.Visible = true;
                    this.Controls.Add(cmbGuna);
                    cmbGuna.BringToFront();
                    cmbGuna.Refresh();
                    Campo = "Estado";
                    Tabla = "ESTADOCIVIL";
                    BD.CargarEstadoCivil(cmbGuna);
                }
                else if (selectedValue == "Contrato")
                {
                    cmbGuna = new Guna.UI2.WinForms.Guna2ComboBox();
                    cmbGuna.Location = new Point(297, 29);
                    cmbGuna.Size = new Size(200, 36);
                    cmbGuna.Font = new Font("Bahnschrift", 11.25f, FontStyle.Regular);
                    cmbGuna.ForeColor = Color.Black;
                    cmbGuna.BorderRadius = 10;
                    cmbGuna.BorderColor = Color.FromArgb(51, 0, 51);
                    cmbGuna.Visible = true;
                    this.Controls.Add(cmbGuna);
                    cmbGuna.BringToFront();
                    cmbGuna.Refresh();
                    Campo = "Tipo";
                    Tabla = "TIPOCONTRATO";
                    BD.CargarTipoDeContrato(cmbGuna);
                }
                else if (selectedValue == "Categoria")
                {
                    cmbGuna = new Guna.UI2.WinForms.Guna2ComboBox();
                    cmbGuna.Location = new Point(297, 29);
                    cmbGuna.Size = new Size(200, 36);
                    cmbGuna.Font = new Font("Bahnschrift", 11.25f, FontStyle.Regular);
                    cmbGuna.ForeColor = Color.Black;
                    cmbGuna.BorderRadius = 10;
                    cmbGuna.BorderColor = Color.FromArgb(51, 0, 51);
                    cmbGuna.Visible = true;
                    this.Controls.Add(cmbGuna);
                    cmbGuna.BringToFront();
                    cmbGuna.Refresh();
                    Campo = "Categoria";
                    Tabla = "CATEGORIA";
                    BD.CargarCategoria(cmbGuna);
                }
                else if (selectedValue == "Fecha de Ingreso")
                {
                    dtpGuna = new Guna.UI2.WinForms.Guna2DateTimePicker();
                    dtpGuna.Location = new Point(297, 29);
                    dtpGuna.Size = new Size(224, 36);
                    dtpGuna.Font = new Font("Bahnschrift", 9f);
                    dtpGuna.BorderRadius = 10;
                    dtpGuna.FillColor = Color.FromArgb(51, 0, 51);
                    dtpGuna.ForeColor = Color.FromArgb(255, 255, 255);
                    dtpGuna.Visible = true;
                    dtpGuna.Format = DateTimePickerFormat.Short;
                    this.Controls.Add(dtpGuna);
                    dtpGuna.BringToFront();
                    dtpGuna.Refresh();
                    Campo = "FechaIngreso";
                    Tabla = "EMPLEADO";
                }
            }
        }

        void limpiarControles()
        {
            var controlsToRemove = new List<Control>();
            foreach (Control control in this.Controls)
            {
                if ((control is Guna.UI2.WinForms.Guna2TextBox ||
                     control is Guna.UI2.WinForms.Guna2ComboBox ||
                     control is Guna.UI2.WinForms.Guna2DateTimePicker) &&
                     control.Name != "cmbFiltro") // Asegurarse de no eliminar el cmbFiltro
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

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo letras y la tecla de retroceso (Backspace)
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo dígitos y la tecla de retroceso (Backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            switch (Seleccionado)
            {
                case "CUIL":
                    VarTexto = txtGuna.Text;

                    ComandoSQL =
                    "SELECT EMPLEADO.Cuil, EMPLEADO.Nombre, EMPLEADO.Apellido, TIPODOCUMENTO.Tipo, EMPLEADO.NumeroDoc, ESTADOCIVIL.Estado, BARRIO.Nombre, EMPLEADO.NumeroCalle, EMPLEADO.FechaNacimiento, TIPOCONTRATO.Tipo, CATEGORIA.Categoria, EMPLEADO.Mail, EMPLEADO.CV, EMPLEADO.FechaIngreso, EMPLEADO.FechaBaja" +
                    " FROM CATEGORIA INNER JOIN(TIPODOCUMENTO INNER JOIN (TIPOCONTRATO INNER JOIN (ESTADOCIVIL INNER JOIN (BARRIO INNER JOIN EMPLEADO ON BARRIO.[IdBarrio] = EMPLEADO.[idBarrio]) ON ESTADOCIVIL.[IdEstadoCivil] = EMPLEADO.[IdEstadoCivil]) ON TIPOCONTRATO.[IdTipoContrato] = EMPLEADO.[IdTipoContrato]) ON TIPODOCUMENTO.[IdTipoDocumento] = EMPLEADO.[IdTipoDocumento]) ON CATEGORIA.IdCategoria = EMPLEADO.[IdCategoria]";

                    BD.BuscadorEmpleados(dgvGrilla, ComandoSQL, VarTexto, 0);
                    break;

                case "Nombre":
                    VarTexto = "'" + txtGuna.Text + "'";
                    ComandoSQL =
                    "SELECT EMPLEADO.Cuil, EMPLEADO.Nombre, EMPLEADO.Apellido, TIPODOCUMENTO.Tipo, EMPLEADO.NumeroDoc, ESTADOCIVIL.Estado, BARRIO.Nombre, EMPLEADO.NumeroCalle, EMPLEADO.FechaNacimiento, TIPOCONTRATO.Tipo, CATEGORIA.Categoria, EMPLEADO.Mail, EMPLEADO.CV, EMPLEADO.FechaIngreso, EMPLEADO.FechaBaja" +
                    " FROM CATEGORIA INNER JOIN(TIPODOCUMENTO INNER JOIN (TIPOCONTRATO INNER JOIN (ESTADOCIVIL INNER JOIN (BARRIO INNER JOIN EMPLEADO ON BARRIO.[IdBarrio] = EMPLEADO.[idBarrio]) ON ESTADOCIVIL.[IdEstadoCivil] = EMPLEADO.[IdEstadoCivil]) ON TIPOCONTRATO.[IdTipoContrato] = EMPLEADO.[IdTipoContrato]) ON TIPODOCUMENTO.[IdTipoDocumento] = EMPLEADO.[IdTipoDocumento]) ON CATEGORIA.IdCategoria = EMPLEADO.[IdCategoria]" +
                    " WHERE EMPLEADO.Nombre" + " = " + VarTexto;
                    BD.BuscadorEmpleados(dgvGrilla, ComandoSQL);
                    break;
                case "Estado Civil":
                    VarTexto = cmbGuna.SelectedItem.ToString();
                    ComandoSQL =
                    "SELECT EMPLEADO.Cuil, EMPLEADO.Nombre, EMPLEADO.Apellido, TIPODOCUMENTO.Tipo, EMPLEADO.NumeroDoc, ESTADOCIVIL.Estado, BARRIO.Nombre, EMPLEADO.NumeroCalle, EMPLEADO.FechaNacimiento, TIPOCONTRATO.Tipo, CATEGORIA.Categoria, EMPLEADO.Mail, EMPLEADO.CV, EMPLEADO.FechaIngreso, EMPLEADO.FechaBaja" +
                    " FROM CATEGORIA INNER JOIN(TIPODOCUMENTO INNER JOIN (TIPOCONTRATO INNER JOIN (ESTADOCIVIL INNER JOIN (BARRIO INNER JOIN EMPLEADO ON BARRIO.[IdBarrio] = EMPLEADO.[idBarrio]) ON ESTADOCIVIL.[IdEstadoCivil] = EMPLEADO.[IdEstadoCivil]) ON TIPOCONTRATO.[IdTipoContrato] = EMPLEADO.[IdTipoContrato]) ON TIPODOCUMENTO.[IdTipoDocumento] = EMPLEADO.[IdTipoDocumento]) ON CATEGORIA.IdCategoria = EMPLEADO.[IdCategoria]";
                    BD.BuscadorEmpleados(dgvGrilla, ComandoSQL, VarTexto, 5);
                    break;

                case "Contrato":
                    VarTexto = cmbGuna.SelectedItem.ToString();
                    ComandoSQL =
                    "SELECT EMPLEADO.Cuil, EMPLEADO.Nombre, EMPLEADO.Apellido, TIPODOCUMENTO.Tipo, EMPLEADO.NumeroDoc, ESTADOCIVIL.Estado, BARRIO.Nombre, EMPLEADO.NumeroCalle, EMPLEADO.FechaNacimiento, TIPOCONTRATO.Tipo, CATEGORIA.Categoria, EMPLEADO.Mail, EMPLEADO.CV, EMPLEADO.FechaIngreso, EMPLEADO.FechaBaja" +
                    " FROM CATEGORIA INNER JOIN(TIPODOCUMENTO INNER JOIN (TIPOCONTRATO INNER JOIN (ESTADOCIVIL INNER JOIN (BARRIO INNER JOIN EMPLEADO ON BARRIO.[IdBarrio] = EMPLEADO.[idBarrio]) ON ESTADOCIVIL.[IdEstadoCivil] = EMPLEADO.[IdEstadoCivil]) ON TIPOCONTRATO.[IdTipoContrato] = EMPLEADO.[IdTipoContrato]) ON TIPODOCUMENTO.[IdTipoDocumento] = EMPLEADO.[IdTipoDocumento]) ON CATEGORIA.IdCategoria = EMPLEADO.[IdCategoria]";
                    BD.BuscadorEmpleados(dgvGrilla, ComandoSQL, VarTexto, 9);

                    break;

                case "Categoria":
                    VarTexto = cmbGuna.SelectedItem.ToString();
                    ComandoSQL =
                    "SELECT EMPLEADO.Cuil, EMPLEADO.Nombre, EMPLEADO.Apellido, TIPODOCUMENTO.Tipo, EMPLEADO.NumeroDoc, ESTADOCIVIL.Estado, BARRIO.Nombre, EMPLEADO.NumeroCalle, EMPLEADO.FechaNacimiento, TIPOCONTRATO.Tipo, CATEGORIA.Categoria, EMPLEADO.Mail, EMPLEADO.CV, EMPLEADO.FechaIngreso, EMPLEADO.FechaBaja" +
                    " FROM CATEGORIA INNER JOIN(TIPODOCUMENTO INNER JOIN (TIPOCONTRATO INNER JOIN (ESTADOCIVIL INNER JOIN (BARRIO INNER JOIN EMPLEADO ON BARRIO.[IdBarrio] = EMPLEADO.[idBarrio]) ON ESTADOCIVIL.[IdEstadoCivil] = EMPLEADO.[IdEstadoCivil]) ON TIPOCONTRATO.[IdTipoContrato] = EMPLEADO.[IdTipoContrato]) ON TIPODOCUMENTO.[IdTipoDocumento] = EMPLEADO.[IdTipoDocumento]) ON CATEGORIA.IdCategoria = EMPLEADO.[IdCategoria]";
                    BD.BuscadorEmpleados(dgvGrilla, ComandoSQL, VarTexto, 10);

                    break;

                case "Fecha de Ingreso":
                    string fechaCorta = dtpGuna.Value.ToString("yyyy-MM-dd");
                    ComandoSQL =
                    "SELECT EMPLEADO.Cuil, EMPLEADO.Nombre, EMPLEADO.Apellido, TIPODOCUMENTO.Tipo, EMPLEADO.NumeroDoc, ESTADOCIVIL.Estado, BARRIO.Nombre, EMPLEADO.NumeroCalle, EMPLEADO.FechaNacimiento, TIPOCONTRATO.Tipo, CATEGORIA.Categoria, EMPLEADO.Mail, EMPLEADO.CV, EMPLEADO.FechaIngreso, EMPLEADO.FechaBaja" +
                    " FROM CATEGORIA INNER JOIN(TIPODOCUMENTO INNER JOIN (TIPOCONTRATO INNER JOIN (ESTADOCIVIL INNER JOIN (BARRIO INNER JOIN EMPLEADO ON BARRIO.[IdBarrio] = EMPLEADO.[idBarrio]) ON ESTADOCIVIL.[IdEstadoCivil] = EMPLEADO.[IdEstadoCivil]) ON TIPOCONTRATO.[IdTipoContrato] = EMPLEADO.[IdTipoContrato]) ON TIPODOCUMENTO.[IdTipoDocumento] = EMPLEADO.[IdTipoDocumento]) ON CATEGORIA.IdCategoria = EMPLEADO.[IdCategoria]" +
                    " WHERE EMPLEADO.FechaIngreso = #" + fechaCorta + "#";
                    BD.BuscadorEmpleados(dgvGrilla, ComandoSQL);
                    break;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            dgvGrilla.ReadOnly = false;
            dgvGrilla.Columns[0].ReadOnly = true;
            dgvGrilla.Columns[3].ReadOnly = true;
            dgvGrilla.Columns[5].ReadOnly = true;
            dgvGrilla.Columns[9].ReadOnly = true;
            dgvGrilla.Columns[10].ReadOnly = true;
            dgvGrilla.Columns[12].ReadOnly = true;
            dgvGrilla.Columns[13].ReadOnly = true;
        }

        private void dgvGrilla_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int columnIndex = e.ColumnIndex;

            if (rowIndex >= 0 && columnIndex >= 0)
            {
                string NombreColumna = dgvGrilla.Columns[columnIndex].Name;
                object NuevoValor = dgvGrilla.Rows[rowIndex].Cells[columnIndex].Value;
                var ClavePrincipal = dgvGrilla.Rows[rowIndex].Cells["CUIL"].Value;

                // Validar que el nombre de la columna sea seguro y permitido
                var columnasPermitidas = new List<string> { "CUIL", "Nombre", "Apellido", "NumeroCalle", "NumeroDoc", "Mail", "FechaIngreso", "FechaBaja" }; // Lista de columnas permitidas
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
                string query = $"UPDATE EMPLEADO SET [{NombreColumna}] = ? WHERE CUIL = ?";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    // Agregar los parámetros en el orden correcto y con los tipos de datos adecuados
                    cmd.Parameters.AddWithValue("@NuevoValor", NuevoValor);
                    cmd.Parameters.AddWithValue("@ClavePrincipal", ClavePrincipal);

                    // Ejecutar la consulta
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            dgvGrilla.ReadOnly = true;
        }
    }
}