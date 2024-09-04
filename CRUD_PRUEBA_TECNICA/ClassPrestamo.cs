using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace CRUD_PRUEBA_TECNICA
{
    internal class ClassPrestamo
    {
        public void MostrarPrestamos(DataGrid prestamos)
        {
            ConexionBD conexionBD = new ConexionBD();

            try
            {
                string query = "SELECT * FROM Prestamos;";
                MySqlCommand command = new MySqlCommand(query, conexionBD.enableConnection());
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable datatable = new DataTable();
                adapter.Fill(datatable);
                prestamos.ItemsSource = datatable.DefaultView;
            }
            catch (Exception e)
            {
                MessageBox.Show("Registros no obtenidos. " + e.ToString());
            }
            finally
            {
                conexionBD.closeConnection();
            }
        }

        public void GuardarPrestamo(TextBox miembroID, TextBox libroID, TextBox fechaPrestamo, TextBox fechaDevolucion, TextBox devuelto)
        {
            // Validación de campos obligatorios
            if (string.IsNullOrWhiteSpace(miembroID.Text) || string.IsNullOrWhiteSpace(libroID.Text) ||
                string.IsNullOrWhiteSpace(fechaPrestamo.Text) || string.IsNullOrWhiteSpace(devuelto.Text))
            {
                MessageBox.Show("Hay campos obligatorios por llenar.");
                return;
            }

            // Validación de fechas
            if (!DateTime.TryParse(fechaPrestamo.Text, out DateTime fechaPrestamoDT) ||
                !DateTime.TryParse(fechaDevolucion.Text, out DateTime fechaDevolucionDT))
            {
                MessageBox.Show("Las fechas deben estar en un formato válido.");
                return;
            }

            if (fechaDevolucionDT < fechaPrestamoDT)
            {
                MessageBox.Show("La fecha de devolución no puede ser anterior a la fecha de préstamo.");
                return;
            }

            ConexionBD conexionBD = new ConexionBD();
            try
            {
                // Verificación de existencia de Miembro
                string checkMiembroQuery = $"SELECT COUNT(*) FROM Miembros WHERE MiembroID = '{miembroID.Text}'";
                MySqlCommand checkMiembroCommand = new MySqlCommand(checkMiembroQuery, conexionBD.enableConnection());
                int miembroCount = Convert.ToInt32(checkMiembroCommand.ExecuteScalar());

                if (miembroCount == 0)
                {
                    MessageBox.Show("El MiembroID no existe.");
                    return;
                }

                // Verificación de existencia de Libro
                string checkLibroQuery = $"SELECT COUNT(*) FROM Libros WHERE LibroID = '{libroID.Text}'";
                MySqlCommand checkLibroCommand = new MySqlCommand(checkLibroQuery, conexionBD.enableConnection());
                int libroCount = Convert.ToInt32(checkLibroCommand.ExecuteScalar());

                if (libroCount == 0)
                {
                    MessageBox.Show("El LibroID no existe.");
                    return;
                }

                // Inserción de datos si las validaciones son exitosas
                string query = $"INSERT INTO Prestamos(MiembroID, LibroID, FechaPrestamo, FechaDevolucion, Devuelto)" +
                               $" VALUES('{miembroID.Text}', '{libroID.Text}', '{fechaPrestamoDT:yyyy-MM-dd}', " +
                               $"'{fechaDevolucionDT:yyyy-MM-dd}', '{devuelto.Text}');";
                MySqlCommand command = new MySqlCommand(query, conexionBD.enableConnection());
                command.ExecuteNonQuery();

                MessageBox.Show("Se guardaron los datos exitosamente.");
            }
            catch (Exception e)
            {
                MessageBox.Show("No se pudieron guardar los datos. " + e.ToString());
            }
            finally
            {
                conexionBD.closeConnection();
            }
        }


        public void SeleccionarPrestamo(DataGrid tablaPrestamos, TextBox prestamoID, TextBox miembroID, TextBox libroID, TextBox fechaPrestamo, TextBox fechaDevolucion, TextBox devuelto)
        {
            try
            {
                if (tablaPrestamos.SelectedItem != null)
                {
                    DataRowView rowSelected = (DataRowView)tablaPrestamos.SelectedItem;
                    prestamoID.Text = rowSelected.Row.ItemArray[0].ToString();
                    miembroID.Text = rowSelected.Row.ItemArray[1].ToString();
                    libroID.Text = rowSelected.Row.ItemArray[2].ToString();
                    if (DateTime.TryParse(rowSelected.Row.ItemArray[3].ToString(), out DateTime fecha))
                    {
                        fechaPrestamo.Text = fecha.ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        fechaPrestamo.Text = rowSelected.Row.ItemArray[3].ToString();
                    }

                    if (DateTime.TryParse(rowSelected.Row.ItemArray[4].ToString(), out fecha))
                    {
                        fechaDevolucion.Text = fecha.ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        fechaDevolucion.Text = rowSelected.Row.ItemArray[4].ToString();
                    }

                    bool isDevuelto = false;
                    if (bool.TryParse(rowSelected.Row.ItemArray[5].ToString(), out isDevuelto))
                    {
                        devuelto.Text = isDevuelto ? "1" : "0";
                    }
                    else
                    {
                        devuelto.Text = "0"; // Valor por defecto si no es un booleano
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al seleccionar. " + e.ToString());
            }
        }

        public void ModificarPrestamo(TextBox prestamoID, TextBox miembroID, TextBox libroID, TextBox fechaPrestamo, TextBox fechaDevolucion, TextBox devuelto)
        {
            // Validación de campos obligatorios
            if (string.IsNullOrWhiteSpace(prestamoID.Text) || string.IsNullOrWhiteSpace(miembroID.Text) ||
                string.IsNullOrWhiteSpace(libroID.Text) || string.IsNullOrWhiteSpace(fechaPrestamo.Text)||
                string.IsNullOrWhiteSpace(devuelto.Text))
            {
                MessageBox.Show("Hay campos obligatorios por llenar.");
                return;
            }

            // Validación de fechas
            if (!DateTime.TryParse(fechaPrestamo.Text, out DateTime fechaPrestamoDT) ||
                !DateTime.TryParse(fechaDevolucion.Text, out DateTime fechaDevolucionDT))
            {
                MessageBox.Show("Las fechas deben estar en un formato válido.");
                return;
            }

            if (fechaDevolucionDT < fechaPrestamoDT)
            {
                MessageBox.Show("La fecha de devolución no puede ser anterior a la fecha de préstamo.");
                return;
            }

            ConexionBD conexionBD = new ConexionBD();
            try
            {
                // Verificación de existencia del préstamo
                string checkPrestamoQuery = $"SELECT COUNT(*) FROM Prestamos WHERE PrestamoID = '{prestamoID.Text}'";
                MySqlCommand checkPrestamoCommand = new MySqlCommand(checkPrestamoQuery, conexionBD.enableConnection());
                int prestamoCount = Convert.ToInt32(checkPrestamoCommand.ExecuteScalar());

                if (prestamoCount == 0)
                {
                    MessageBox.Show("El PrestamoID no existe.");
                    return;
                }

                // Verificación de existencia de Miembro
                string checkMiembroQuery = $"SELECT COUNT(*) FROM Miembros WHERE MiembroID = '{miembroID.Text}'";
                MySqlCommand checkMiembroCommand = new MySqlCommand(checkMiembroQuery, conexionBD.enableConnection());
                int miembroCount = Convert.ToInt32(checkMiembroCommand.ExecuteScalar());

                if (miembroCount == 0)
                {
                    MessageBox.Show("El MiembroID no existe.");
                    return;
                }

                // Verificación de existencia de Libro
                string checkLibroQuery = $"SELECT COUNT(*) FROM Libros WHERE LibroID = '{libroID.Text}'";
                MySqlCommand checkLibroCommand = new MySqlCommand(checkLibroQuery, conexionBD.enableConnection());
                int libroCount = Convert.ToInt32(checkLibroCommand.ExecuteScalar());

                if (libroCount == 0)
                {
                    MessageBox.Show("El LibroID no existe.");
                    return;
                }

                // Actualización de datos si las validaciones son exitosas
                string query = $"UPDATE Prestamos SET MiembroID ='{miembroID.Text}'," +
                               $"LibroID ='{libroID.Text}', FechaPrestamo ='{fechaPrestamoDT:yyyy-MM-dd}', " +
                               $"FechaDevolucion = '{fechaDevolucionDT:yyyy-MM-dd}', Devuelto = '{devuelto.Text}' " +
                               $"WHERE PrestamoID ='{prestamoID.Text}';";

                MySqlCommand command = new MySqlCommand(query, conexionBD.enableConnection());
                command.ExecuteNonQuery();

                MessageBox.Show("Se modificaron los datos exitosamente.");
            }
            catch (Exception e)
            {
                MessageBox.Show("No se pudieron modificar los datos. " + e.ToString());
            }
            finally
            {
                conexionBD.closeConnection();
            }
        }

        public void LimpiarText(TextBox prestamoID, TextBox miembroID, TextBox libroID, TextBox fechaPrestamo, TextBox fechaDevolucion, TextBox devuelto)
        {
            prestamoID.Text = "";
            miembroID.Text = "";
            libroID.Text = "";
            fechaPrestamo.Text = "";
            fechaDevolucion.Text = "";
            devuelto.Text = "";
        }

        public void EliminarPrestamo(TextBox id)
        {
            ConexionBD conexionBD = new ConexionBD();

            try
            {
                string query = $"DELETE FROM Prestamos WHERE PrestamoID = '{id.Text}';";

                MySqlCommand command = new MySqlCommand(query, conexionBD.enableConnection());
                MySqlDataReader reader;
                reader = command.ExecuteReader();
                MessageBox.Show("Se eliminaron los datos.");

                while (reader.Read())
                {

                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al eliminar los datos." + e.ToString());
            }
            finally
            {
                conexionBD.closeConnection();
            }
        }
    }
}
