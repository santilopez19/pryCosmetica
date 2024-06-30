using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryCosmetica
{
    public class clsProcesosBD
    {
        public OleDbConnection conexion = new OleDbConnection();
        public OleDbCommand comando = new OleDbCommand();
        public OleDbDataAdapter adaptador = new OleDbDataAdapter();

        public string varCadenaConexion = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Cosmetica.accdb";

        string rutaArchivo;
        public string estadoConexion;
        public void CargarEmpleado()
        {
            try
            {
                using (OleDbConnection conexion = new OleDbConnection(varCadenaConexion))
                {
                    conexion.Open();

                    using (OleDbCommand comando = new OleDbCommand())
                    {
                        comando.Connection = conexion;
                        comando.CommandType = CommandType.TableDirect;
                        comando.CommandText = "EMPLEADO";

                        OleDbDataAdapter adaptador = new OleDbDataAdapter(comando);
                        DataSet DS = new DataSet();
                        adaptador.Fill(DS, "EMPLEADO");

                        DataTable objTabla = DS.Tables["EMPLEADO"];


                        //Verifico duplicados
                        string filtro = $"Cuil = '{frmCargarEmpleado.cuil}'";
                        DataRow[] filasExistentes = objTabla.Select(filtro);

                        if (filasExistentes.Length > 0)
                        {
                            MessageBox.Show("Ya existe un empleado con el mismo CUIL.");
                            return;
                        }

                        //Verifico si el código postal existe
                        int codigoPostalBarrio = ObtenerCodigoPostalBarrio(conexion, frmCargarEmpleado.codPostal);
                        if (codigoPostalBarrio == -1)
                        {
                            MessageBox.Show("El código postal no existe.");
                            return;
                        }

                        DataRow nuevoRegistro = objTabla.NewRow();

                        nuevoRegistro["Nombre"] = frmCargarEmpleado.nombreEmpleado;
                        nuevoRegistro["Apellido"] = frmCargarEmpleado.apellidoEmpleado;
                        nuevoRegistro["Cuil"] = frmCargarEmpleado.cuil;
                        nuevoRegistro["NumeroDoc"] = frmCargarEmpleado.numeroDocumentoEmpleado;
                        nuevoRegistro["NumeroCalle"] = frmCargarEmpleado.numeroCalle;
                        nuevoRegistro["FechaNacimiento"] = frmCargarEmpleado.fechaNacimientoEmpleado;
                        nuevoRegistro["Mail"] = frmCargarEmpleado.mailEmpleado;
                        nuevoRegistro["Telefono"] = frmCargarEmpleado.telefonoEmpleado;
                        nuevoRegistro["FechaIngreso"] = frmCargarEmpleado.fechaIngresoEmpleado;

                        //Obtengo el IdTipoDocumento correspondiente
                        int idTipoDocumento = ObtenerIdTipoDocumento(conexion, frmCargarEmpleado.tipoDocumento);
                        nuevoRegistro["IdTipoDocumento"] = idTipoDocumento;

                        //Obtengo el IdEstadoCivil correspondiente
                        int idEstadoCivil = ObtenerIdEstadoCivil(conexion, frmCargarEmpleado.estadoCivilEmpleado);
                        nuevoRegistro["IdEstadoCivil"] = idEstadoCivil;

                        //Obtengo el IdCalle correspondiente
                        int idCalle = ObtenerIdCalle(conexion, frmCargarEmpleado.calle);
                        nuevoRegistro["IdCalle"] = idCalle;

                        //Obtengo el IdTipoContrato correspondiente
                        int idTipoContrato = ObtenerIdTipoContrato(conexion, frmCargarEmpleado.tipoDeContrato);
                        nuevoRegistro["IdTipoContrato"] = idTipoContrato;

                        //Obtengo el IdCategoria correspondiente
                        int idCategoria = ObtenerIdCategoria(conexion, frmCargarEmpleado.categoria);
                        nuevoRegistro["IdCategoria"] = idCategoria;

                        //Obtengo el CodigoPostalBarrio correspondiente
                        nuevoRegistro["CodigoPostal"] = codigoPostalBarrio;

                        //Obtengo el CodigoPostalBarrio correspondiente
                        int idCiudad = ObtenerIdCiudad(conexion, frmCargarEmpleado.ciudad);
                        nuevoRegistro["IdCiudad"] = idCiudad;

                        //Obtengo el CodigoPostalBarrio correspondiente
                        int idBarrio = ObtenerIdBarrio(conexion, frmCargarEmpleado.barrio);
                        nuevoRegistro["IdBarrio"] = idBarrio;

                        objTabla.Rows.Add(nuevoRegistro);

                        OleDbCommandBuilder constructor = new OleDbCommandBuilder(adaptador);
                        adaptador.Update(DS, "EMPLEADO");

                        MessageBox.Show("Los datos del empleado se cargaron correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        MessageBox.Show("A continuacón debe cargar el curriculum de dicho empleado.");

                        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                        {
                            //Configuro propiedades del SaveFileDialog
                            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                            saveFileDialog.FilterIndex = 1;
                            saveFileDialog.RestoreDirectory = true;

                            //Obtengo el directorio del ejecutable de la aplicación
                            string executablePath = AppDomain.CurrentDomain.BaseDirectory;

                            //Construyo la ruta relativa ../../CV
                            string relativePath = Path.Combine(executablePath, @"..\..\CVempleado");
                            string initialDirectory = Path.GetFullPath(relativePath);

                            //Verifico si la ruta existe, si no, crearla
                            if (!Directory.Exists(initialDirectory))
                            {
                                Directory.CreateDirectory(initialDirectory);
                                MessageBox.Show("La carpeta CV no existía y ha sido creada en: " + initialDirectory);
                            }

                            //Establezco la ruta inicial
                            saveFileDialog.InitialDirectory = initialDirectory;

                            //Obtengo el DNI del empleado (asegúrate de obtenerlo desde tu formulario o contexto)
                            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                            string fileName = $"{frmCargarEmpleado.numeroDocumentoEmpleado}_{timestamp}.pdf";
                            saveFileDialog.FileName = fileName; // Establecer el nombre del archivo por defecto


                            //Muestro el SaveFileDialog
                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                //Manejo la ruta seleccionada
                                string filePath = saveFileDialog.FileName;

                                //Guardo la ruta en la base de datos
                                GuardarRutaCVEnBaseDeDatos(frmCargarEmpleado.numeroDocumentoEmpleado, filePath);
                            }
                        }
                    }
                }
            }
            catch (OleDbException ex)
            {
                if (ex.ErrorCode == -2147467259)
                {
                    MessageBox.Show("Error de clave duplicada: " + ex.Message);
                }
                else
                {
                    MessageBox.Show("Error de base de datos: " + ex.Message);
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        public void CargarAreaEmpleado()
        {
            try
            {
                using (OleDbConnection conexion = new OleDbConnection(varCadenaConexion))
                {
                    conexion.Open();

                    //Obtengo el IdArea correspondiente
                    int idArea = ObtenerIdArea(conexion, frmCargarEmpleado.areaDeTrabajo);

                    //Verifico si ya existe el empleado que se quiere cargar en el área seleccionada
                    using (OleDbCommand verificarComando = new OleDbCommand())
                    {
                        verificarComando.Connection = conexion;
                        verificarComando.CommandType = CommandType.Text;
                        verificarComando.CommandText = "SELECT COUNT(*) FROM [AREA-EMPLEADO] WHERE Cuil = ? AND IdArea = ?";
                        verificarComando.Parameters.AddWithValue("@Cuil", frmCargarEmpleado.cuil);
                        verificarComando.Parameters.AddWithValue("@IdArea", idArea);

                        int count = (int)verificarComando.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("Dicho empleado ya está registrado en el área especificada.");
                            return;
                        }
                    }

                    //Verifico si el código postal existe para que no entre al catch directamente y tire un error que no entendería el usuario
                    int codigoPostalExistente = ObtenerCodigoPostalBarrio(conexion, frmCargarEmpleado.codPostal);
                    if (codigoPostalExistente == -1)
                    {
                        MessageBox.Show("El código postal especificado no existe. No se puede cargar el área del empleado.");
                        return;
                    }

                    using (OleDbCommand comando = new OleDbCommand())
                    {
                        comando.Connection = conexion;
                        comando.CommandType = CommandType.Text;
                        comando.CommandText = "INSERT INTO [AREA-EMPLEADO] (Cuil, Desde, Estado, IdArea) VALUES (@Cuil, @Desde, @Estado, @IdArea)";

                        //Especifico los tipos de datos correctos
                        comando.Parameters.Add("@Cuil", OleDbType.VarChar).Value = frmCargarEmpleado.cuil;
                        comando.Parameters.Add("@Desde", OleDbType.Date).Value = frmCargarEmpleado.fechaIngresoEmpleado;
                        comando.Parameters.Add("@Estado", OleDbType.Boolean).Value = true;
                        comando.Parameters.Add("@IdArea", OleDbType.Integer).Value = idArea;

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        private int ObtenerIdTipoDocumento(OleDbConnection conexion, string tipoDocumento)
        {
            using (OleDbCommand comando = new OleDbCommand("SELECT IdTipoDocumento FROM TIPODOCUMENTO WHERE Tipo = ?", conexion))
            {
                comando.Parameters.AddWithValue("?", tipoDocumento);
                using (OleDbDataReader lectorBD = comando.ExecuteReader())
                {
                    if (lectorBD.Read())
                    {
                        return lectorBD.GetInt32(0);
                    }
                }
            }
            return -1;
        }

        private int ObtenerIdEstadoCivil(OleDbConnection conexion, string estadoCivil)
        {
            using (OleDbCommand comando = new OleDbCommand("SELECT IdEstadoCivil FROM ESTADOCIVIL WHERE Estado = ?", conexion))
            {
                comando.Parameters.AddWithValue("?", estadoCivil);
                using (OleDbDataReader lectorBD = comando.ExecuteReader())
                {
                    if (lectorBD.Read())
                    {
                        return lectorBD.GetInt32(0);
                    }
                }
            }
            return -1;
        }

        private int ObtenerIdCalle(OleDbConnection conexion, string nombreCalle)
        {
            using (OleDbCommand comando = new OleDbCommand("SELECT IdCalle FROM CALLE WHERE NombreCalle = ?", conexion))
            {
                comando.Parameters.AddWithValue("?", nombreCalle);
                using (OleDbDataReader lectorBD = comando.ExecuteReader())
                {
                    if (lectorBD.Read())
                    {
                        return lectorBD.GetInt32(0);
                    }
                }
            }
            return -1;
        }

        private int ObtenerIdTipoContrato(OleDbConnection conexion, string tipoContrato)
        {
            using (OleDbCommand comando = new OleDbCommand("SELECT IdTipoContrato FROM TIPOCONTRATO WHERE Tipo = ?", conexion))
            {
                comando.Parameters.AddWithValue("?", tipoContrato);
                using (OleDbDataReader lectorBD = comando.ExecuteReader())
                {
                    if (lectorBD.Read())
                    {
                        return lectorBD.GetInt32(0);
                    }
                }
            }
            return -1;
        }

        private int ObtenerIdCategoria(OleDbConnection conexion, string idCategoria)
        {
            using (OleDbCommand comando = new OleDbCommand("SELECT IdCategoria FROM CATEGORIA WHERE Categoria = ?", conexion))
            {
                comando.Parameters.AddWithValue("?", idCategoria);
                using (OleDbDataReader lectorBD = comando.ExecuteReader())
                {
                    if (lectorBD.Read())
                    {
                        return lectorBD.GetInt32(0);
                    }
                }
            }
            return -1;
        }

        private int ObtenerIdArea(OleDbConnection conexion, string idArea)
        {
            using (OleDbCommand comando = new OleDbCommand("SELECT IdArea FROM AREA WHERE NombreArea = ?", conexion))
            {
                comando.Parameters.AddWithValue("?", idArea);
                using (OleDbDataReader lectorBD = comando.ExecuteReader())
                {
                    if (lectorBD.Read())
                    {
                        return lectorBD.GetInt32(0);
                    }
                }
            }
            return -1;
        }

        private int ObtenerCodigoPostalBarrio(OleDbConnection conexion, string codigoPostal)
        {
            using (OleDbCommand comando = new OleDbCommand("SELECT IdCodigoPostal FROM CODIGOPOSTAL WHERE Numero = ?", conexion))
            {
                comando.Parameters.AddWithValue("?", codigoPostal);
                using (OleDbDataReader lectorBD = comando.ExecuteReader())
                {
                    if (lectorBD.Read())
                    {
                        return lectorBD.GetInt32(0);
                    }
                }
            }
            return -1;
        }

        private int ObtenerIdCiudad(OleDbConnection conexion, string idCiudad)
        {
            using (OleDbCommand comando = new OleDbCommand("SELECT IdCiudad FROM CIUDAD WHERE Nombre = ?", conexion))
            {
                comando.Parameters.AddWithValue("?", idCiudad);
                using (OleDbDataReader lectorBD = comando.ExecuteReader())
                {
                    if (lectorBD.Read())
                    {
                        return lectorBD.GetInt32(0);
                    }
                }
            }
            return -1;
        }

        private int ObtenerIdBarrio(OleDbConnection conexion, string idBarrio)
        {
            using (OleDbCommand comando = new OleDbCommand("SELECT IdBarrio FROM BARRIO WHERE Nombre = ?", conexion))
            {
                comando.Parameters.AddWithValue("?", idBarrio);
                using (OleDbDataReader lectorBD = comando.ExecuteReader())
                {
                    if (lectorBD.Read())
                    {
                        return lectorBD.GetInt32(0);
                    }
                }
            }
            return -1;
        }

        public void CargarTipoDocumento(ComboBox cargarDocumento)
        {
            string connectionString = varCadenaConexion;
            string query = "SELECT Tipo FROM TIPODOCUMENTO";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand(query, connection);
                    OleDbDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string documento = reader["Tipo"].ToString();
                        cargarDocumento.Items.Add(documento);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        public void CargarEstadoCivil(ComboBox cargarEstadoCivil)
        {
            string connectionString = varCadenaConexion;
            string query = "SELECT Estado FROM ESTADOCIVIL";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand(query, connection);
                    OleDbDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string Estado = reader["Estado"].ToString();
                        cargarEstadoCivil.Items.Add(Estado);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        public void CargarCategoria(ComboBox cargarCategoria)
        {
            string connectionString = varCadenaConexion;
            string query = "SELECT Categoria FROM CATEGORIA";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand(query, connection);
                    OleDbDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string Categoria = reader["Categoria"].ToString();
                        cargarCategoria.Items.Add(Categoria);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        public void CargarAreaDeTrabajo(ComboBox cargarAreaDeTrabajo)
        {
            string connectionString = varCadenaConexion;
            string query = "SELECT NombreArea FROM AREA";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand(query, connection);
                    OleDbDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string Area = reader["NombreArea"].ToString();
                        cargarAreaDeTrabajo.Items.Add(Area);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        public void CargarTipoDeContrato(ComboBox cargarTipoDeContrato)
        {
            string connectionString = varCadenaConexion;
            string query = "SELECT Tipo FROM TIPOCONTRATO";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand(query, connection);
                    OleDbDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string Area = reader["Tipo"].ToString();
                        cargarTipoDeContrato.Items.Add(Area);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        public void CargarCalles(ComboBox cargarCalle)
        {
            string connectionString = varCadenaConexion;
            string query = "SELECT NombreCalle FROM CALLE";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand(query, connection);
                    OleDbDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string calle = reader["NombreCalle"].ToString();
                        cargarCalle.Items.Add(calle);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        public void CargarCiudad(ComboBox cargarCiudad)
        {
            string connectionString = varCadenaConexion;
            string query = "SELECT Nombre FROM CIUDAD";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand(query, connection);
                    OleDbDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string ciudad = reader["Nombre"].ToString();
                        cargarCiudad.Items.Add(ciudad);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        public void CargarBarrio(ComboBox cargarBarrio)
        {
            string connectionString = varCadenaConexion;
            string query = "SELECT Nombre FROM BARRIO";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand(query, connection);
                    OleDbDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string barrio = reader["Nombre"].ToString();
                        cargarBarrio.Items.Add(barrio);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        public void GuardarRutaCVEnBaseDeDatos(string dni, string filePath)
        {
            try
            {
                using (OleDbConnection conexion = new OleDbConnection(varCadenaConexion))
                {
                    conexion.Open();
                    using (OleDbCommand comando = new OleDbCommand())
                    {
                        comando.Connection = conexion;
                        comando.CommandType = CommandType.Text;
                        comando.CommandText = "UPDATE EMPLEADO SET CV = @RutaCV WHERE NumeroDoc = @DNI";

                        // Especificar los tipos de datos correctos
                        comando.Parameters.Add("@RutaCV", OleDbType.VarChar).Value = filePath;
                        comando.Parameters.Add("@DNI", OleDbType.VarChar).Value = dni;

                        // Ejecutar el comando
                        comando.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Ruta del CV guardada exitosamente en la base de datos.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la ruta del CV en la base de datos: " + ex.Message);
            }
        }
        public int totalConsulta;
        public void ContarEmpleados()
        {

            try
            {
                using (OleDbConnection conexion = new OleDbConnection(varCadenaConexion))
                {
                    conexion.Open();

                    using (OleDbCommand comando = new OleDbCommand())
                    {
                        comando.Connection = conexion;
                        comando.CommandType = CommandType.Text;
                        comando.CommandText = "SELECT COUNT(*) FROM EMPLEADO";


                        totalConsulta = (int)comando.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ContarPostulantes()
        {

            try
            {
                using (OleDbConnection conexion = new OleDbConnection(varCadenaConexion))
                {
                    conexion.Open();

                    using (OleDbCommand comando = new OleDbCommand())
                    {
                        comando.Connection = conexion;
                        comando.CommandType = CommandType.Text;
                        comando.CommandText = "SELECT COUNT(*) FROM POSTULANTES";


                        totalConsulta = (int)comando.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ContarReportes()
        {
            try
            {
                using (OleDbConnection conexion = new OleDbConnection(varCadenaConexion))
                {
                    conexion.Open();

                    using (OleDbCommand comando = new OleDbCommand())
                    {
                        comando.Connection = conexion;
                        comando.CommandType = CommandType.Text;
                        comando.CommandText = "SELECT COUNT(*) FROM INASISTENCIAS";


                        // Consulta para contar inasistencias
                        comando.CommandText = "SELECT COUNT(*) FROM INASISTENCIAS";
                        totalConsulta += (int)comando.ExecuteScalar();

                        // Consulta para contar amonestaciones
                        comando.CommandText = "SELECT COUNT(*) FROM AMONESTACIONES";
                        totalConsulta += (int)comando.ExecuteScalar();

                        // Consulta para contar evaluaciones de desempeño
                        comando.CommandText = "SELECT COUNT(*) FROM EVALUACIONDESEMPEÑO";
                        totalConsulta += (int)comando.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void CumpleEmpleados(Guna2DataGridView grilla)
        {
            try
            {
                using (OleDbConnection conexion = new OleDbConnection(varCadenaConexion))
                {
                    conexion.Open();

                    using (OleDbCommand comando = new OleDbCommand())
                    {
                        comando.Connection = conexion;
                        comando.CommandType = CommandType.Text;

                        // Consulta SQL ajustada para incluir Apellido y limitar a los primeros 6 resultados
                        comando.CommandText = @"
        SELECT TOP 5 Nombre, Apellido, FechaNacimiento
        FROM EMPLEADO
        ORDER BY 
            IIF(
                DATEPART('y', FechaNacimiento) >= DATEPART('y', DATE()),
                DATEPART('y', FechaNacimiento) - DATEPART('y', DATE()),
                365 + DATEPART('y', FechaNacimiento) - DATEPART('y', DATE())
            )
        ";

                        OleDbDataAdapter adaptador = new OleDbDataAdapter(comando);
                        DataTable tablaEmpleados = new DataTable();
                        adaptador.Fill(tablaEmpleados);

                        grilla.DataSource = tablaEmpleados;

                        // Configurar columnas del DataGridView
                        grilla.Columns[0].HeaderText = "Nombre";
                        grilla.Columns[1].HeaderText = "Apellido";
                        grilla.Columns[2].HeaderText = "Fecha de Nacimiento";
                        grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void BuscadorEmpleados(DataGridView Grilla, string ConsultaSQL, string Variable, int comparar)
        {
            conexion = new OleDbConnection(varCadenaConexion);
            comando = new OleDbCommand(ConsultaSQL, conexion);
            adaptador = new OleDbDataAdapter(comando);
            conexion.Open();
            OleDbDataReader lector;

            try
            {
                lector = comando.ExecuteReader();
                Grilla.Rows.Clear();
                while (lector.Read())
                {
                    if (lector[comparar].ToString() == Variable)
                    {
                        Grilla.Rows.Add(
                            lector[0].ToString(), //ID
                            lector[1].ToString(), //NOMBRE
                            lector[2].ToString(), //APELLIDO
                            lector[3].ToString(), //TIPO DOCUMENTO
                            lector[4].ToString(), //DOCUMENTO
                            lector[5].ToString(), //ESTADO CIVIL
                            lector[6].ToString(), //DIRECCION
                            lector[7].ToString(), //NUMERO DE CALLE
                            lector[8].ToString(), //FECHA NACIMIENTO
                            lector[9].ToString(), //CONTRATO
                            lector[10].ToString(),//CATEGORIA
                            lector[11].ToString(),//MAIL
                            lector[12].ToString(),//CV
                            lector[13].ToString(),//FECHA INGRESO
                            lector[14].ToString());//FECHA BAJA
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex);
            }
            finally
            {
                conexion.Close();
            }
        }
        public void BuscadorEmpleados(DataGridView Grilla, string ConsultaSQL)
        {
            conexion = new OleDbConnection(varCadenaConexion);
            comando = new OleDbCommand(ConsultaSQL, conexion);
            adaptador = new OleDbDataAdapter(comando);
            conexion.Open();
            OleDbDataReader lector;
            try
            {
                lector = comando.ExecuteReader();
                Grilla.Rows.Clear();
                while (lector.Read())
                {

                    Grilla.Rows.Add(
                        lector[0].ToString(), //ID
                        lector[1].ToString(), //NOMBRE
                        lector[2].ToString(), //APELLIDO
                        lector[3].ToString(), //TIPO DOCUMENTO
                        lector[4].ToString(), //DOCUMENTO
                        lector[5].ToString(), //ESTADO CIVIL
                        lector[6].ToString(), //DIRECCION
                        lector[7].ToString(), //NUMERO DE CALLE
                        lector[8].ToString(), //FECHA NACIMIENTO
                        lector[9].ToString(), //CONTRATO
                        lector[10].ToString(),//CATEGORIA   
                        lector[11].ToString(),//MAIL
                        lector[12].ToString(),//CV
                        lector[13].ToString(),//FECHA INGRESO
                        lector[14].ToString());//FECHA BAJA

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex);
            }
            finally
            {
                conexion.Close();
            }
        }

        /////Cargar Suspension
        public clsProcesosBD()
        {
            try
            {
                DataSet objDS;
                rutaArchivo = "..\\..\\Resources\\Cosmetica.accdb";

                conexion = new OleDbConnection();
                conexion.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + rutaArchivo;
                conexion.Open();

                objDS = new DataSet();

                estadoConexion = "Conectado";
            }
            catch (Exception error)
            {
                estadoConexion = error.Message;
            }
        }

        public void cargarAmonestacion(Int32 Motivo, String empleado, DateTime fecha)
        {
            int id = this.buscarIDEmpleado(empleado);
            fecha.ToShortDateString();
            MessageBox.Show(id.ToString());
            if (id == -1) { MessageBox.Show("El Empleado ingresado no esta registrado"); return; }
            try
            {
                DataSet objDS = new DataSet();
                comando = new OleDbCommand();

                comando.Connection = conexion;
                comando.CommandType = System.Data.CommandType.TableDirect;
                comando.CommandText = "AMONESTACIONES";

                adaptador = new OleDbDataAdapter(comando);

                adaptador.Fill(objDS, "AMONESTACIONES");

                DataTable objTabla = objDS.Tables["AMONESTACIONES"];
                DataRow nuevoRegistro = objTabla.NewRow();

                nuevoRegistro["Cuil"] = id;
                nuevoRegistro["Fecha"] = fecha;
                nuevoRegistro["IdMotivoAmonestacion"] = Motivo;

                objTabla.Rows.Add(nuevoRegistro);

                OleDbCommandBuilder constructor = new OleDbCommandBuilder(adaptador);
                adaptador.Update(objDS, "AMONESTACIONES");

                estadoConexion = "Registro exitoso de amonestacion";
            }
            catch (Exception error)
            {
                estadoConexion = error.Message;
            }

        }

        public int buscarIDEmpleado(String nombre)
        {
            try
            {
                int id = -1;

                DataSet objDS = new DataSet();
                comando = new OleDbCommand();

                comando.Connection = conexion;
                comando.CommandType = System.Data.CommandType.TableDirect;
                comando.CommandText = "EMPLEADO";

                adaptador = new OleDbDataAdapter(comando);

                adaptador.Fill(objDS, "EMPLEADO");

                DataTable objTabla = objDS.Tables["EMPLEADO"];
                DataRow[] datareader = objTabla.Select("Nombre like '" + nombre + "'");

                foreach (DataRow row in datareader)
                {
                    id = row.Field<int>("Cuil");
                }


                return id;

            }
            catch (Exception error)
            {
                estadoConexion = error.Message;
                MessageBox.Show(estadoConexion);
                return -2;
            }
        }

        public void cargarSuspencion(Int32 Motivo, String empleado, DateTime fecha, DateTime fecha2, String Detalle)
        {
            DataSet objDS = new DataSet();
            int id = this.buscarIDEmpleado(empleado);
            MessageBox.Show(id.ToString());
            if (id == -1) { MessageBox.Show("El Empleado ingresado no esta registrado"); return; }
            try
            {
                comando = new OleDbCommand();

                comando.Connection = conexion;
                comando.CommandType = System.Data.CommandType.TableDirect;
                comando.CommandText = "SUSPENSIONES";

                adaptador = new OleDbDataAdapter(comando);

                adaptador.Fill(objDS, "SUSPENSIONES");

                DataTable objTabla = objDS.Tables["SUSPENSIONES"];
                DataRow nuevoRegistro = objTabla.NewRow();

                nuevoRegistro["Cuil"] = id;
                nuevoRegistro["Desde"] = fecha;
                nuevoRegistro["Hasta"] = fecha2;
                nuevoRegistro["IdMotivo"] = Motivo;
                nuevoRegistro["Observaciones"] = Detalle;

                objTabla.Rows.Add(nuevoRegistro);

                OleDbCommandBuilder constructor = new OleDbCommandBuilder(adaptador);
                adaptador.Update(objDS, "SUSPENSIONES");

                estadoConexion = "Registro exitoso de suspencion";
            }
            catch (Exception error)
            {
                estadoConexion = error.Message;
            }

        }

        public void cargarEvaluacion(String empleado, DateTime fecha, Int32 calificacion, Int32 area, String detalle, String evaluador)
        {
            DataSet objDS = new DataSet();
            int id = this.buscarIDEmpleado(empleado);
            int idEvaluador = this.buscarIDEmpleado(evaluador);
            MessageBox.Show(id.ToString());
            if (id == -1) { MessageBox.Show("El Empleado ingresado no esta registrado"); return; }
            try
            {
                comando = new OleDbCommand();

                comando.Connection = conexion;
                comando.CommandType = System.Data.CommandType.TableDirect;
                comando.CommandText = "EVALUACIONDESEMPEÑO";

                adaptador = new OleDbDataAdapter(comando);

                adaptador.Fill(objDS, "EVALUACIONDESEMPEÑO");

                DataTable objTabla = objDS.Tables["EVALUACIONDESEMPEÑO"];
                DataRow nuevoRegistro = objTabla.NewRow();

                nuevoRegistro["Cuil"] = id;
                nuevoRegistro["Fecha"] = fecha;
                nuevoRegistro["IdCalificacion"] = calificacion;
                nuevoRegistro["IdArea"] = area;
                nuevoRegistro["Observacion"] = detalle;
                nuevoRegistro["IdEvaluador"] = idEvaluador;

                objTabla.Rows.Add(nuevoRegistro);

                OleDbCommandBuilder constructor = new OleDbCommandBuilder(adaptador);
                adaptador.Update(objDS, "EVALUACIONDESEMPEÑO");

                estadoConexion = "Registro exitoso de Evaluacion de Desempeño";
            }
            catch (Exception error)
            {
                estadoConexion = error.Message;
            }

        }

        public void cargarInasistencia(String empleado, DateTime fecha, Int32 motivo, Boolean justificado, Int32 tipoInasitencia)
        {
            DataSet objDS = new DataSet();
            int id = this.buscarIDEmpleado(empleado);
            MessageBox.Show(id.ToString());
            if (id == -1) { MessageBox.Show("El Empleado ingresado no esta registrado"); return; }
            try
            {
                comando = new OleDbCommand();

                comando.Connection = conexion;
                comando.CommandType = System.Data.CommandType.TableDirect;
                comando.CommandText = "INASISTENCIAS";

                adaptador = new OleDbDataAdapter(comando);

                adaptador.Fill(objDS, "INASISTENCIAS");

                DataTable objTabla = objDS.Tables["INASISTENCIAS"];
                DataRow nuevoRegistro = objTabla.NewRow();

                nuevoRegistro["Cuil"] = id;
                nuevoRegistro["Fecha"] = fecha;
                nuevoRegistro["IdMotivo"] = motivo;
                nuevoRegistro["Justificado"] = justificado;
                nuevoRegistro["IdTipoInasistencia"] = tipoInasitencia;

                objTabla.Rows.Add(nuevoRegistro);

                OleDbCommandBuilder constructor = new OleDbCommandBuilder(adaptador);
                adaptador.Update(objDS, "INASISTENCIAS");

                estadoConexion = "Registro exitoso de Evaluacion de Desempeño";
            }
            catch (Exception error)
            {
                estadoConexion = error.Message;
            }

        }
    }
}
