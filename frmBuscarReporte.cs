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
    public partial class frmBuscarReporte : Form
    {
        public frmBuscarReporte()
        {
            InitializeComponent();
        }
        #region ABM
        //  ABRIR FORMULARIO DENTRO DEL PRINCIPAL
        private Form formActivo = null;
        private void abrirFormHijo(Form formHijo)
        {
            if (formActivo != null)
                formActivo.Close();
            formActivo = formHijo;
            formHijo.TopLevel = false;
            formHijo.FormBorderStyle = FormBorderStyle.None;
            formHijo.Dock = DockStyle.Fill;
            this.Controls.Add(formHijo);
            this.Tag = formHijo;
            formHijo.BringToFront();
            formHijo.Show();
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            string tipoReporte = "";

            if (dgvInasistencia.Visible)
            {
                tipoReporte = "Inasistencias";
            }
            else if (dgvAmonestacion.Visible)
            {
                tipoReporte = "Amonestaciones";
            }
            else if (dgvSuspension.Visible)
            {
                tipoReporte = "Suspensiones";
            }
            else if (dgvEvaluacion.Visible)
            {
                tipoReporte = "Evaluacion de Desempeño";
            }
            else if (dgvDespido.Visible)
            {
                tipoReporte = "Despidos";
            }

            abrirFormHijo(new frmAbmReporte(tipoReporte));
        }
        #endregion


        private Guna.UI2.WinForms.Guna2TextBox txtCUIL;
        private Guna.UI2.WinForms.Guna2TextBox txtNombre;
        string selectedValue;
        private void cmbCondicion_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedValue = cmbCondicion.SelectedItem.ToString();

            limpiarControles();

            if (selectedValue == "CUIL")
            {
                txtCUIL = new Guna.UI2.WinForms.Guna2TextBox();
                txtCUIL.ForeColor = Color.Black;
                txtCUIL.Font = new Font("Bahnschrift", 11.25f, FontStyle.Regular);
                txtCUIL.BorderRadius = 10;
                txtCUIL.BorderColor = Color.Black;
                txtCUIL.Visible = true;
                this.Controls.Add(txtCUIL);
                this.ResumeLayout(false);
                this.PerformLayout();
                txtCUIL.BringToFront();
                txtCUIL.Refresh();

                txtCUIL.Location = new Point(446, 51);
                txtCUIL.Size = new Size(200, 36);

                // Agregar el manejador del evento KeyPress
                txtCUIL.KeyPress += new KeyPressEventHandler(txtCUIL_KeyPress);
            }
            else if (selectedValue == "Nombre")
            {
                txtNombre = new Guna.UI2.WinForms.Guna2TextBox();
                txtNombre.ForeColor = Color.Black;
                txtNombre.Font = new Font("Bahnschrift", 11.25f, FontStyle.Regular);
                txtNombre.BorderRadius = 10;
                txtNombre.BorderColor = Color.Black;
                txtNombre.Visible = true;
                this.Controls.Add(txtNombre);
                this.ResumeLayout(false);
                this.PerformLayout();
                txtNombre.BringToFront();
                txtNombre.Refresh();

                txtNombre.Location = new Point(446, 51);
                txtNombre.Size = new Size(200, 36);

                // Agregar el manejador del evento KeyPress
                txtNombre.KeyPress += new KeyPressEventHandler(txtNombre_KeyPress);
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
                     control.Name != "cmbFiltro" && control.Name != "cmbCondicion") // Asegurarse de no eliminar el cmbFiltro
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

        private void txtCUIL_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo dígitos y la tecla de retroceso (Backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cmbFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFiltro.SelectedItem != null)
            {
                string selectedValue = cmbFiltro.SelectedItem.ToString();

                limpiarControles();

                if (selectedValue == "Inasistencias")
                {
                    dgvEvaluacion.Visible = false;
                    dgvSuspension.Visible = false;
                    dgvAmonestacion.Visible = false;
                    dgvInasistencia.Visible = true;
                    dgvDespido.Visible = false;
                }
                else if (selectedValue == "Amonestaciones")
                {
                    dgvEvaluacion.Visible = false;
                    dgvSuspension.Visible = false;
                    dgvAmonestacion.Visible = true;
                    dgvInasistencia.Visible = false;
                    dgvDespido.Visible = false;
                }
                else if (selectedValue == "Suspensiones")
                {
                    dgvEvaluacion.Visible = false;
                    dgvSuspension.Visible = true;
                    dgvAmonestacion.Visible = false;
                    dgvInasistencia.Visible = false;
                    dgvDespido.Visible = false;
                }
                else if (selectedValue == "Evaluacion de Desempeño")
                {
                    dgvEvaluacion.Visible = true;
                    dgvSuspension.Visible = false;
                    dgvAmonestacion.Visible = false;
                    dgvInasistencia.Visible = false;
                    dgvDespido.Visible = false;
                }
                else if (selectedValue == "Despidos")
                {
                    dgvEvaluacion.Visible = false;
                    dgvSuspension.Visible = false;
                    dgvAmonestacion.Visible = false;
                    dgvInasistencia.Visible = false;
                    dgvDespido.Visible = true;
                }
            }
        }
        clsProcesosBD BD = new clsProcesosBD();
        OleDbDataReader lectorBD;

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            String selecFiltro = cmbFiltro.SelectedItem.ToString();
            try
            {
                BD.conexion.ConnectionString = BD.varCadenaConexion;
                BD.conexion.Open();
                BD.comando = new OleDbCommand();

                BD.comando.Connection = BD.conexion;
                BD.comando.CommandType = System.Data.CommandType.Text;
                switch (selecFiltro)
                {
                    case "Inasistencias":

                        BD.comando.CommandText = @"
                                    SELECT 
                                        INASISTENCIAS.IdInasistencias, 
                                        INASISTENCIAS.Cuil, 
                                        EMPLEADO.Nombre, 
                                        INASISTENCIAS.Fecha, 
                                        MOTIVOINASISTENCIA.Motivo, 
                                        INASISTENCIAS.Justificado, 
                                        TIPOINASISTENCIA.[Tipo Inasistencia]
                                    FROM 
                                        EMPLEADO 
                                    INNER JOIN 
                                        (TIPOINASISTENCIA 
                                    INNER JOIN 
                                        (MOTIVOINASISTENCIA 
                                    INNER JOIN 
                                        INASISTENCIAS ON MOTIVOINASISTENCIA.[IdMotivo] = INASISTENCIAS.[IdMotivo]) ON TIPOINASISTENCIA.[IdTipoInasistencia] = INASISTENCIAS.[IdTipoInasistencia]) ON EMPLEADO.[Cuil] = INASISTENCIAS.[Cuil];
                                    ";

                        lectorBD = BD.comando.ExecuteReader();
                        dgvInasistencia.Rows.Clear();

                        while (lectorBD.Read())
                        {
                            dgvInasistencia.Rows.Add(
                            lectorBD["IdInasistencias"].ToString(),
                            lectorBD["Cuil"].ToString(),
                            lectorBD["Nombre"].ToString(),
                            lectorBD["Fecha"].ToString(),
                            lectorBD["Motivo"].ToString(),
                            lectorBD["Justificado"].ToString(),
                            lectorBD["Tipo Inasistencia"].ToString());

                        }
                        if (cmbCondicion.SelectedItem != null)
                        {
                            String selecCondicion = cmbCondicion.SelectedItem.ToString();
                            if (selecCondicion == "CUIL")
                            {
                                BD.comando.CommandText = @"
                                    SELECT 
                                        INASISTENCIAS.IdInasistencias, 
                                        INASISTENCIAS.Cuil, 
                                        EMPLEADO.Nombre, 
                                        INASISTENCIAS.Fecha, 
                                        MOTIVOINASISTENCIA.Motivo, 
                                        INASISTENCIAS.Justificado, 
                                        TIPOINASISTENCIA.[Tipo Inasistencia]
                                    FROM 
                                        EMPLEADO 
                                    INNER JOIN 
                                        (TIPOINASISTENCIA 
                                    INNER JOIN 
                                        (MOTIVOINASISTENCIA 
                                    INNER JOIN 
                                        INASISTENCIAS ON MOTIVOINASISTENCIA.[IdMotivo] = INASISTENCIAS.[IdMotivo]) ON TIPOINASISTENCIA.[IdTipoInasistencia] = INASISTENCIAS.[IdTipoInasistencia]) ON EMPLEADO.[Cuil] = INASISTENCIAS.[Cuil]
                                   WHERE INASISTENCIAS.[Cuil] = @Cuil";

                                BD.comando.Parameters.Clear();
                                BD.comando.Parameters.AddWithValue("@Cuil", txtCUIL.Text);

                                lectorBD.Close();
                                lectorBD = BD.comando.ExecuteReader();
                                dgvInasistencia.Rows.Clear();
                                while (lectorBD.Read())
                                {
                                    if (lectorBD["Cuil"].ToString() == txtCUIL.Text)
                                    {
                                        dgvInasistencia.Rows.Add(
                                        lectorBD["IdInasistencias"].ToString(),
                                        lectorBD["Cuil"].ToString(),
                                        lectorBD["Nombre"].ToString(),
                                        lectorBD["Fecha"].ToString(),
                                        lectorBD["Motivo"].ToString(),
                                        lectorBD["Justificado"].ToString(),
                                        lectorBD["Tipo Inasistencia"].ToString());
                                    }
                                }
                            }
                            else if (selecCondicion == "Nombre")
                            {
                                BD.comando.CommandText = @"
                                    SELECT 
                                        INASISTENCIAS.IdInasistencias, 
                                        INASISTENCIAS.Cuil, 
                                        EMPLEADO.Nombre, 
                                        INASISTENCIAS.Fecha, 
                                        MOTIVOINASISTENCIA.Motivo, 
                                        INASISTENCIAS.Justificado, 
                                        TIPOINASISTENCIA.[Tipo Inasistencia]
                                    FROM 
                                        EMPLEADO 
                                    INNER JOIN 
                                        (TIPOINASISTENCIA 
                                    INNER JOIN 
                                        (MOTIVOINASISTENCIA 
                                    INNER JOIN 
                                        INASISTENCIAS ON MOTIVOINASISTENCIA.[IdMotivo] = INASISTENCIAS.[IdMotivo]) ON TIPOINASISTENCIA.[IdTipoInasistencia] = INASISTENCIAS.[IdTipoInasistencia]) ON EMPLEADO.[Cuil] = INASISTENCIAS.[Cuil];
                                    ";

                                lectorBD.Close();
                                lectorBD = BD.comando.ExecuteReader();
                                dgvInasistencia.Rows.Clear();
                                while (lectorBD.Read())
                                {
                                    if (lectorBD["Nombre"].ToString() == txtNombre.Text)
                                    {
                                        dgvInasistencia.Rows.Add(
                                        lectorBD["IdInasistencias"].ToString(),
                                        lectorBD["Cuil"].ToString(),
                                        lectorBD["Nombre"].ToString(),
                                        lectorBD["Fecha"].ToString(),
                                        lectorBD["Motivo"].ToString(),
                                        lectorBD["Justificado"].ToString(),
                                        lectorBD["Tipo Inasistencia"].ToString());
                                    }
                                }
                            }
                        }
                        break;

                    case "Amonestaciones":

                        BD.comando.CommandText = @"
                                    SELECT 
                                        AMONESTACIONES.IdAmonestaciones, 
                                        AMONESTACIONES.Cuil, 
                                        EMPLEADO.Nombre, 
                                        AMONESTACIONES.Fecha, 
                                        MOTIVOAMONESTACION.Motivo
                                    FROM 
                                        EMPLEADO 
                                    INNER JOIN 
                                        (MOTIVOAMONESTACION 
                                    INNER JOIN 
                                        AMONESTACIONES ON MOTIVOAMONESTACION.[IdMotivoAmonestacion] = AMONESTACIONES.[IdMotivoAmonestacion]) ON EMPLEADO.[Cuil] = AMONESTACIONES.[Cuil]"
                                    ;

                        lectorBD = BD.comando.ExecuteReader();
                        dgvAmonestacion.Rows.Clear();
                        while (lectorBD.Read())
                        {
                            dgvAmonestacion.Rows.Add(
                            lectorBD["IdAmonestaciones"].ToString(),
                            lectorBD["Cuil"].ToString(),
                            lectorBD["Nombre"].ToString(),
                            lectorBD["Fecha"].ToString(),
                            lectorBD["Motivo"].ToString());

                        }

                        if (cmbCondicion.SelectedItem != null)
                        {
                            String selecCondicion = cmbCondicion.SelectedItem.ToString();
                            if (selecCondicion == "CUIL")
                            {
                                BD.comando.CommandText = @"
                                    SELECT 
                                        AMONESTACIONES.IdAmonestaciones, 
                                        AMONESTACIONES.Cuil, 
                                        EMPLEADO.Nombre, 
                                        AMONESTACIONES.Fecha, 
                                        MOTIVOAMONESTACION.Motivo
                                    FROM 
                                        EMPLEADO 
                                    INNER JOIN 
                                        (MOTIVOAMONESTACION 
                                    INNER JOIN 
                                        AMONESTACIONES ON MOTIVOAMONESTACION.[IdMotivoAmonestacion] = AMONESTACIONES.[IdMotivoAmonestacion]) ON EMPLEADO.[Cuil] = AMONESTACIONES.[Cuil]
                                    WHERE AMONESTACIONES.[Cuil] = @Cuil";

                                BD.comando.Parameters.Clear();
                                BD.comando.Parameters.AddWithValue("@Cuil", txtCUIL.Text);

                                lectorBD.Close();
                                lectorBD = BD.comando.ExecuteReader();
                                dgvAmonestacion.Rows.Clear();
                                while (lectorBD.Read())
                                {
                                    if (lectorBD["Cuil"].ToString() == txtCUIL.Text)
                                    {
                                        dgvAmonestacion.Rows.Add(
                                        lectorBD["IdAmonestaciones"].ToString(),
                                        lectorBD["Cuil"].ToString(),
                                        lectorBD["Nombre"].ToString(),
                                        lectorBD["Fecha"].ToString(),
                                        lectorBD["Motivo"].ToString());
                                    }
                                }
                            }
                            else if (selecCondicion == "Nombre")
                            {
                                BD.comando.CommandText = @"
                                    SELECT 
                                        AMONESTACIONES.IdAmonestaciones, 
                                        AMONESTACIONES.Cuil, 
                                        EMPLEADO.Nombre, 
                                        AMONESTACIONES.Fecha, 
                                        MOTIVOAMONESTACION.Motivo
                                    FROM 
                                        EMPLEADO 
                                    INNER JOIN 
                                        (MOTIVOAMONESTACION 
                                    INNER JOIN 
                                        AMONESTACIONES ON MOTIVOAMONESTACION.[IdMotivoAmonestacion] = AMONESTACIONES.[IdMotivoAmonestacion]) ON EMPLEADO.[Cuil] = AMONESTACIONES.[Cuil]
                                    ";

                                lectorBD.Close();
                                lectorBD = BD.comando.ExecuteReader();
                                dgvAmonestacion.Rows.Clear();
                                while (lectorBD.Read())
                                {
                                    if (lectorBD["Nombre"].ToString() == txtNombre.Text)
                                    {
                                        dgvAmonestacion.Rows.Add(
                                        lectorBD["IdAmonestaciones"].ToString(),
                                        lectorBD["Cuil"].ToString(),
                                        lectorBD["Nombre"].ToString(),
                                        lectorBD["Fecha"].ToString(),
                                        lectorBD["Motivo"].ToString());
                                    }
                                }
                            }
                        }
                        break;
                    case "Suspensiones":

                        BD.comando.CommandText = @"
                                    SELECT 
                                        SUSPENSIONES.IdSuspension, 
                                        SUSPENSIONES.Cuil, 
                                        EMPLEADO.Nombre, 
                                        SUSPENSIONES.Desde, 
                                        SUSPENSIONES.Hasta, 
                                        SUSPENSIONES.Observaciones, 
                                        MOTIVOSUSPENSION.Motivo
                                    FROM 
                                        EMPLEADO 
                                    INNER JOIN 
                                        (MOTIVOSUSPENSION 
                                    INNER JOIN 
                                        SUSPENSIONES ON MOTIVOSUSPENSION.[IdMotivoSuspension] = SUSPENSIONES.[IdMotivo]) ON EMPLEADO.[Cuil] = SUSPENSIONES.[Cuil]
                                    ";

                        lectorBD = BD.comando.ExecuteReader();
                        dgvSuspension.Rows.Clear();
                        while (lectorBD.Read())
                        {
                            dgvSuspension.Rows.Add(
                            lectorBD["IdSuspension"].ToString(),
                            lectorBD["Cuil"].ToString(),
                            lectorBD["Nombre"].ToString(),
                            lectorBD["Desde"].ToString(),
                            lectorBD["Hasta"].ToString(),
                            lectorBD["Observaciones"].ToString(),
                            lectorBD["Motivo"].ToString());

                        }

                        if (cmbCondicion.SelectedItem != null)
                        {
                            String selecCondicion = cmbCondicion.SelectedItem.ToString();
                            if (selecCondicion == "CUIL")
                            {
                                BD.comando.CommandText = @"
                                    SELECT 
                                        SUSPENSIONES.IdSuspension, 
                                        SUSPENSIONES.Cuil, 
                                        EMPLEADO.Nombre, 
                                        SUSPENSIONES.Desde, 
                                        SUSPENSIONES.Hasta, 
                                        SUSPENSIONES.Observaciones, 
                                        MOTIVOSUSPENSION.Motivo
                                    FROM 
                                        EMPLEADO 
                                    INNER JOIN 
                                        (MOTIVOSUSPENSION 
                                    INNER JOIN 
                                        SUSPENSIONES ON MOTIVOSUSPENSION.[IdMotivoSuspension] = SUSPENSIONES.[IdMotivo]) ON EMPLEADO.[Cuil] = SUSPENSIONES.[Cuil]                                    
                                    WHERE SUSPENSIONES.[Cuil] = @Cuil";

                                BD.comando.Parameters.Clear();
                                BD.comando.Parameters.AddWithValue("@Cuil", txtCUIL.Text);

                                lectorBD.Close();
                                lectorBD = BD.comando.ExecuteReader();
                                dgvSuspension.Rows.Clear();
                                while (lectorBD.Read())
                                {
                                    if (lectorBD["Cuil"].ToString() == txtCUIL.Text)
                                    {
                                        dgvSuspension.Rows.Add(
                                        lectorBD["IdSuspension"].ToString(),
                                        lectorBD["Cuil"].ToString(),
                                        lectorBD["Nombre"].ToString(),
                                        lectorBD["Desde"].ToString(),
                                        lectorBD["Hasta"].ToString(),
                                        lectorBD["Observaciones"].ToString(),
                                        lectorBD["Motivo"].ToString());
                                    }
                                }
                            }
                            else if (selecCondicion == "Nombre")
                            {
                                BD.comando.CommandText = @"
                                    SELECT 
                                        SUSPENSIONES.IdSuspension, 
                                        SUSPENSIONES.Cuil, 
                                        EMPLEADO.Nombre, 
                                        SUSPENSIONES.Desde, 
                                        SUSPENSIONES.Hasta, 
                                        SUSPENSIONES.Observaciones, 
                                        MOTIVOSUSPENSION.Motivo
                                    FROM 
                                        EMPLEADO 
                                    INNER JOIN 
                                        (MOTIVOSUSPENSION 
                                    INNER JOIN 
                                        SUSPENSIONES ON MOTIVOSUSPENSION.[IdMotivoSuspension] = SUSPENSIONES.[IdMotivo]) ON EMPLEADO.[Cuil] = SUSPENSIONES.[Cuil]                                    
                                    ";


                                lectorBD.Close();
                                lectorBD = BD.comando.ExecuteReader();
                                dgvSuspension.Rows.Clear();
                                while (lectorBD.Read())
                                {
                                    if (lectorBD["Nombre"].ToString() == txtNombre.Text)
                                    {
                                        dgvSuspension.Rows.Add(
                                        lectorBD["IdSuspension"].ToString(),
                                        lectorBD["Cuil"].ToString(),
                                        lectorBD["Nombre"].ToString(),
                                        lectorBD["Desde"].ToString(),
                                        lectorBD["Hasta"].ToString(),
                                        lectorBD["Observaciones"].ToString(),
                                        lectorBD["Motivo"].ToString());
                                    }
                                }
                            }
                        }
                        break;

                    case "Evaluacion de Desempeño":

                        BD.comando.CommandText = @"
                                    SELECT 
                                        EVALUACIONDESEMPEÑO.IdEvaluacionDesempeño, 
                                        EVALUACIONDESEMPEÑO.Cuil, EMPLEADO.Nombre, 
                                        EVALUACIONDESEMPEÑO.Fecha, 
                                        CALIFICACION.Calificacion, 
                                        AREA.NombreArea, 
                                        EVALUACIONDESEMPEÑO.Observacion, 
                                        EVALUADORES.NombreEvaluador
                                    FROM 
                                        EVALUADORES 
                                    INNER JOIN 
                                        (EMPLEADO 
                                    INNER JOIN 
                                        (AREA 
                                    INNER JOIN 
                                        (CALIFICACION 
                                    INNER JOIN EVALUACIONDESEMPEÑO ON CALIFICACION.[IdCalificacion] = EVALUACIONDESEMPEÑO.[IdCalificacion]) ON AREA.[IdArea] = EVALUACIONDESEMPEÑO.[IdArea]) ON EMPLEADO.[Cuil] = EVALUACIONDESEMPEÑO.[Cuil]) ON EVALUADORES.[idEvaluador] = EVALUACIONDESEMPEÑO.[IdEvaluador]
                                    ";

                        lectorBD = BD.comando.ExecuteReader();
                        dgvEvaluacion.Rows.Clear();
                        while (lectorBD.Read())
                        {
                            dgvEvaluacion.Rows.Add(
                            lectorBD["IdEvaluacionDesempeño"].ToString(),
                            lectorBD["Cuil"].ToString(),
                            lectorBD["Nombre"].ToString(),
                            lectorBD["Fecha"].ToString(),
                            lectorBD["Calificacion"].ToString(),
                            lectorBD["NombreArea"].ToString(),
                            lectorBD["Observacion"].ToString(),
                            lectorBD["NombreEvaluador"].ToString());

                        }

                        if (cmbCondicion.SelectedItem != null)
                        {
                            String selecCondicion = cmbCondicion.SelectedItem.ToString();
                            if (selecCondicion == "CUIL")
                            {
                                BD.comando.CommandText = @"
                                    SELECT 
                                        EVALUACIONDESEMPEÑO.IdEvaluacionDesempeño, 
                                        EVALUACIONDESEMPEÑO.Cuil, EMPLEADO.Nombre, 
                                        EVALUACIONDESEMPEÑO.Fecha, 
                                        CALIFICACION.Calificacion, 
                                        AREA.NombreArea, 
                                        EVALUACIONDESEMPEÑO.Observacion, 
                                        EVALUADORES.NombreEvaluador
                                    FROM 
                                        EVALUADORES 
                                    INNER JOIN 
                                        (EMPLEADO 
                                    INNER JOIN 
                                        (AREA 
                                    INNER JOIN 
                                        (CALIFICACION 
                                    INNER JOIN EVALUACIONDESEMPEÑO ON CALIFICACION.[IdCalificacion] = EVALUACIONDESEMPEÑO.[IdCalificacion]) ON AREA.[IdArea] = EVALUACIONDESEMPEÑO.[IdArea]) ON EMPLEADO.[Cuil] = EVALUACIONDESEMPEÑO.[Cuil]) ON EVALUADORES.[idEvaluador] = EVALUACIONDESEMPEÑO.[IdEvaluador]
                                    WHERE EVALUACIONDESEMPEÑO.[Cuil] = @Cuil";

                                BD.comando.Parameters.Clear();
                                BD.comando.Parameters.AddWithValue("@Cuil", txtCUIL.Text);

                                lectorBD.Close();
                                lectorBD = BD.comando.ExecuteReader();
                                dgvEvaluacion.Rows.Clear();
                                while (lectorBD.Read())
                                {
                                    if (lectorBD["Cuil"].ToString() == txtCUIL.Text)
                                    {
                                        dgvEvaluacion.Rows.Add(
                                        lectorBD["IdEvaluacionDesempeño"].ToString(),
                                        lectorBD["Cuil"].ToString(),
                                        lectorBD["Nombre"].ToString(),
                                        lectorBD["Fecha"].ToString(),
                                        lectorBD["Calificacion"].ToString(),
                                        lectorBD["NombreArea"].ToString(),
                                        lectorBD["Observacion"].ToString(),
                                        lectorBD["NombreEvaluador"].ToString());
                                    }
                                }
                            }
                            else if (selecCondicion == "Nombre")
                            {
                                BD.comando.CommandText = @"
                                   SELECT 
                                        EVALUACIONDESEMPEÑO.IdEvaluacionDesempeño, 
                                        EVALUACIONDESEMPEÑO.Cuil, EMPLEADO.Nombre, 
                                        EVALUACIONDESEMPEÑO.Fecha, 
                                        CALIFICACION.Calificacion, 
                                        AREA.NombreArea, 
                                        EVALUACIONDESEMPEÑO.Observacion, 
                                        EVALUADORES.NombreEvaluador
                                    FROM 
                                        EVALUADORES 
                                    INNER JOIN 
                                        (EMPLEADO 
                                    INNER JOIN 
                                        (AREA 
                                    INNER JOIN 
                                        (CALIFICACION 
                                    INNER JOIN EVALUACIONDESEMPEÑO ON CALIFICACION.[IdCalificacion] = EVALUACIONDESEMPEÑO.[IdCalificacion]) ON AREA.[IdArea] = EVALUACIONDESEMPEÑO.[IdArea]) ON EMPLEADO.[Cuil] = EVALUACIONDESEMPEÑO.[Cuil]) ON EVALUADORES.[idEvaluador] = EVALUACIONDESEMPEÑO.[IdEvaluador]
                                    ";


                                lectorBD.Close();
                                lectorBD = BD.comando.ExecuteReader();
                                dgvEvaluacion.Rows.Clear();
                                while (lectorBD.Read())
                                {
                                    if (lectorBD["Nombre"].ToString() == txtNombre.Text)
                                    {
                                        dgvEvaluacion.Rows.Add(
                                        lectorBD["IdEvaluacionDesempeño"].ToString(),
                                        lectorBD["Cuil"].ToString(),
                                        lectorBD["Nombre"].ToString(),
                                        lectorBD["Fecha"].ToString(),
                                        lectorBD["Calificacion"].ToString(),
                                        lectorBD["NombreArea"].ToString(),
                                        lectorBD["Observacion"].ToString(),
                                        lectorBD["NombreEvaluador"].ToString());
                                    }
                                }
                            }
                        }
                        break;

                    case "Despidos":

                        BD.comando.CommandText = @"
                                    SELECT 
                                        EMPLEADO.[Cuil], 
                                        EMPLEADO.[Nombre], 
                                        EMPLEADO.[FechaBaja]
                                    FROM 
                                        EMPLEADO                                                                      
                                            ";

                        lectorBD = BD.comando.ExecuteReader();
                        dgvDespido.Rows.Clear();
                        while (lectorBD.Read())
                        {
                            if (!lectorBD.IsDBNull(lectorBD.GetOrdinal("FechaBaja")))
                            {
                                dgvDespido.Rows.Add(
                                lectorBD["Cuil"].ToString(),
                                lectorBD["Nombre"].ToString(),
                                lectorBD["FechaBaja"].ToString());
                            }

                        }

                        if (cmbCondicion.SelectedItem != null)
                        {
                            String selecCondicion = cmbCondicion.SelectedItem.ToString();
                            if (selecCondicion == "CUIL")
                            {
                                BD.comando.CommandText = @"
                                    SELECT 
                                        EMPLEADO.[Cuil], 
                                        EMPLEADO.[Nombre], 
                                        EMPLEADO.[FechaBaja]
                                    FROM 
                                        EMPLEADO
                                    WHERE EMPLEADO.[Cuil] = @Cuil";

                                BD.comando.Parameters.Clear();
                                BD.comando.Parameters.AddWithValue("@Cuil", txtCUIL.Text);

                                lectorBD.Close();
                                lectorBD = BD.comando.ExecuteReader();
                                dgvDespido.Rows.Clear();
                                while (lectorBD.Read())
                                {
                                    if (lectorBD["Cuil"].ToString() == txtCUIL.Text)
                                    {
                                        if (!lectorBD.IsDBNull(lectorBD.GetOrdinal("FechaBaja")))
                                        {
                                            dgvDespido.Rows.Add(
                                            lectorBD["Cuil"].ToString(),
                                            lectorBD["Nombre"].ToString(),
                                            lectorBD["FechaBaja"].ToString());
                                        }
                                    }
                                }
                            }
                            else if (selecCondicion == "Nombre")
                            {
                                BD.comando.CommandText = @"
                                    SELECT 
                                        EMPLEADO.[Cuil], 
                                        EMPLEADO.[Nombre], 
                                        EMPLEADO.[FechaBaja]
                                    FROM 
                                        EMPLEADO
                                        ";

                                lectorBD.Close();
                                lectorBD = BD.comando.ExecuteReader();
                                dgvDespido.Rows.Clear();
                                while (lectorBD.Read())
                                {
                                    if (lectorBD["Nombre"].ToString() == txtNombre.Text)
                                    {
                                        if (!lectorBD.IsDBNull(lectorBD.GetOrdinal("FechaBaja")))
                                        {
                                            dgvDespido.Rows.Add(
                                            lectorBD["Cuil"].ToString(),
                                            lectorBD["Nombre"].ToString(),
                                            lectorBD["FechaBaja"].ToString());
                                        }
                                    }
                                }
                            }
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
    }
}
