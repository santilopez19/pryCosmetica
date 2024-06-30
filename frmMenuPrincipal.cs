using Guna.Charts.WinForms;
using LiveCharts.Wpf;
using LiveCharts;
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
using System.Windows.Media;

namespace pryCosmetica
{
    public partial class frmMenuPrincipal : Form
    {
        public frmMenuPrincipal()
        {
            InitializeComponent();
            lblTitulo.Text = "Inicio";
            clsProcesosBD objbd = new clsProcesosBD();
            objbd.ContarEmpleados();
            lblNumEmpleados.Text =objbd.totalConsulta.ToString();
            objbd.ContarPostulantes();
            lblNumPostulantes.Text = objbd.totalConsulta.ToString();
            objbd.ContarReportes();
            lblNumReport.Text = objbd.totalConsulta.ToString();
            CreateChart();
            objbd.CumpleEmpleados(dgvCumple);

            // Hacer la grilla no interactiva
            dgvCumple.Enabled = false;
        }

        // Método para obtener la conexión a tu base de datos
        public OleDbConnection GetConnection()
        {
            // Cambia esta cadena de conexión según sea necesario
            return new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Cosmetica.accdb");
        }

        // Método para obtener los datos de la base de datos
        public DataTable GetEvaluations()
        {
            using (OleDbConnection conn = GetConnection())
            {
                conn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(
                    "SELECT Cuil, Fecha, IdCalificacion FROM EVALUACIONDESEMPEÑO", conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        // Procesa los datos para calcular las mejores calificaciones mensuales
        public Dictionary<string, double[]> ProcessData(DataTable dt)
        {
            var result = new Dictionary<string, double[]>();

            var groupedData = dt.AsEnumerable()
                .GroupBy(row => new
                {
                    Year = ((DateTime)row["Fecha"]).Year,
                    Month = ((DateTime)row["Fecha"]).Month
                })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    MaxCalificacion = g.Max(row => Convert.ToInt32(row["IdCalificacion"]))
                });

            foreach (var data in groupedData)
            {
                string key = $"{data.Year}-{data.Month:00}";
                if (!result.ContainsKey(key))
                    result[key] = new double[2]; // Solo necesitamos 2 años (2023 y 2024)

                result[key][data.Year % 2023] = data.MaxCalificacion;
            }

            return result;
        }

        private void CreateChart()
        {
            DataTable dt = GetEvaluations();
            var data = ProcessData(dt);

            // Configurar el eje X
            chartCar.AxisX.Clear();
            chartCar.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Meses",
                Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0)),
                Labels = new[]
                {
                "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
                "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"
                }

            });

            // Configurar el eje Y
            chartCar.AxisY.Clear();
            chartCar.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Calificación",
                Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0)),
                MinValue = 1,
                MaxValue = 5,
                Labels = new[] { "", "Mediocre", "Mal", "Regular", "Bien", "Sobresaliente" }

            });

            //chartCar.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(200,86, 52, 64)); // Cambia este color al que desees

            // Crear series para los años 2023 y 2024

            var series2024 = new LineSeries
            {
                Title = "2024",
                Values = new ChartValues<double>(),

                Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(65, 0, 65)), // Color para la línea de 2023
                Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(60, 65, 0, 65))
            };

            var series2023 = new LineSeries
            {
                Title = "2023",
                Values = new ChartValues<double>(),
                PointGeometry = DefaultGeometries.Circle,
                PointGeometrySize = 10,
                Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(207, 207, 220)), // Color para la línea de 2023
                Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(50, 207, 207, 220))
            };


            // Crear los nombres de los meses
            for (int i = 0; i < 12; i++)
            {
                string key2024 = $"2024-{i + 1:00}";
                string key2023 = $"2023-{i + 1:00}";

                double max2024 = data.ContainsKey(key2024) ? data[key2024][1] : 0;
                double max2023 = data.ContainsKey(key2023) ? data[key2023][0] : 0;

                series2024.Values.Add(max2024);
                series2023.Values.Add(max2023);
            }

            // Limpiar series previas
            chartCar.Series.Clear();

            // Agregar las series al gráfico
            chartCar.Series.Add(series2024);
            chartCar.Series.Add(series2023);

        }
        #region Formularios-SubMenus

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
            pnlPrincipal.Controls.Add(formHijo);
            pnlPrincipal.Tag = formHijo;
            formHijo.BringToFront();           
            formHijo.Show();
        }

        // ABRIR Y OCULTAR SUBMENUS   

        private void ocultarSubMenu()
        {
            if (pnlSubMenuEmpleados.Visible == true)
            {
                pnlSubMenuEmpleados.Visible = false;
            }
            if (pnlSubMenuPostulante.Visible == true)
            {
                pnlSubMenuPostulante.Visible=false;
            }
            if(pnlSubMenuReportes.Visible == true)
            {
                pnlSubMenuReportes.Visible=false;
            }
        }
        private void mostrarSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                ocultarSubMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            mostrarSubMenu(pnlSubMenuEmpleados);
        }

        private void btnPostulante_Click(object sender, EventArgs e)
        {
            mostrarSubMenu(pnlSubMenuPostulante);
        }
        private void btnReportes_Click(object sender, EventArgs e)
        {
            mostrarSubMenu(pnlSubMenuReportes);           
        }

        private void btnCargarEmpleado_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = "Cargar Empleado";
            abrirFormHijo(new frmCargarEmpleado());
            // codigo
            frmCargarEmpleado frm = new frmCargarEmpleado();
            ocultarSubMenu();
        }

        private void btnBuscarEmpleado_Click(object sender, EventArgs e)
        { 
            lblTitulo.Text = "Buscar Empleados";
            abrirFormHijo(new frmBuscarEmpleado());
            ocultarSubMenu();
        }

        private void btnCargarPostulante_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = "Cargar Postulante";
            abrirFormHijo(new frmCargarPostulante());
            ocultarSubMenu();
        }

        private void btnBuscarPostulante_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = "Buscar Postulantes";
            abrirFormHijo(new frmBuscarPostulante());
            ocultarSubMenu();
        }
        private void btnCargarReporte_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = "Cargar Reporte";
            abrirFormHijo(new frmCargarReporte());
            ocultarSubMenu();
        }

        private void btnBuscarReporte_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = "Buscar Reportes";
            abrirFormHijo(new frmBuscarReporte());
            ocultarSubMenu();
        }

        private void pctLogo_Click(object sender, EventArgs e)
        {
            if (formActivo != null)
                formActivo.Close();
            lblTitulo.Text = "Inicio";
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

    }
}
