using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CRUD_PRUEBA_TECNICA
{
    internal class ClassMiembro
    {
        public void MostrarMiembros(DataGrid usuarios)
        {
            ConexionBD conexionBD = new ConexionBD();

            try
            {
                string query = "SELECT * FROM Miembros;";
                MySqlCommand command = new MySqlCommand(query, conexionBD.enableConnection());
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable datatable = new DataTable();
                adapter.Fill(datatable);
                usuarios.ItemsSource = datatable.DefaultView;
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

        public void GuardarMiembro(TextBox nombre, TextBox direccion, TextBox telefono, TextBox email, TextBox fechaRegistro)
        {
            // Validación de campos obligatorios
            if (string.IsNullOrWhiteSpace(nombre.Text) || string.IsNullOrWhiteSpace(email.Text) ||
                string.IsNullOrWhiteSpace(fechaRegistro.Text))
            {
                MessageBox.Show("Hay campos obligatorios por llenar.");
                return;
            }

            ConexionBD conexionBD = new ConexionBD();
            try
            {
                // Verificación de duplicados
                string checkQuery = $"SELECT COUNT(*) FROM Miembros WHERE Email = '{email.Text}'";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, conexionBD.enableConnection());
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("El Email ya está registrado. Por favor, use un Email diferente.");
                    return;
                }

                // Inserción de datos
                string query = $"INSERT INTO Miembros(Nombre, Direccion, Telefono, Email, FechaRegistro)" +
                               $" VALUES('{nombre.Text}', '{direccion.Text}', '{telefono.Text}', '{email.Text}', '{fechaRegistro.Text}');";
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

        public void SeleccionarMiembro(DataGrid tablaUsuarios, TextBox id, TextBox nombre,
            TextBox direccion, TextBox telefono, TextBox email, TextBox fechaRegistro)
        {
            try
            {
                if(tablaUsuarios.SelectedItem != null)
                {
                    DataRowView rowSelected = (DataRowView)tablaUsuarios.SelectedItem;
                    id.Text = rowSelected.Row.ItemArray[0].ToString();
                    nombre.Text = rowSelected.Row.ItemArray[1].ToString();
                    direccion.Text = rowSelected.Row.ItemArray[2].ToString();
                    telefono.Text = rowSelected.Row.ItemArray[3].ToString();
                    email.Text = rowSelected.Row.ItemArray[4].ToString();
                    if (DateTime.TryParse(rowSelected.Row.ItemArray[5].ToString(), out DateTime fecha))
                    {
                        fechaRegistro.Text = fecha.ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        fechaRegistro.Text = rowSelected.Row.ItemArray[5].ToString();
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Error al seleccionar. " + e.ToString());
            }
        }

        public void ModificarMiembro(TextBox id, TextBox nombre, TextBox direccion, TextBox telefono, TextBox email, TextBox fechaRegistro)
        {
            // Validación de campos obligatorios
            if (string.IsNullOrWhiteSpace(id.Text) || string.IsNullOrWhiteSpace(nombre.Text) ||
                string.IsNullOrWhiteSpace(email.Text) || string.IsNullOrWhiteSpace(fechaRegistro.Text))
            {
                MessageBox.Show("Hay campos obligatorios por llenar.");
                return;
            }

            // Validación de la fecha
            if (!DateTime.TryParse(fechaRegistro.Text, out DateTime fechaRegistroDT))
            {
                MessageBox.Show("La fecha de registro debe estar en un formato válido.");
                return;
            }

            ConexionBD conexionBD = new ConexionBD();
            try
            {
                // Verificación de existencia del miembro
                string checkMiembroQuery = $"SELECT COUNT(*) FROM Miembros WHERE MiembroID = '{id.Text}'";
                MySqlCommand checkMiembroCommand = new MySqlCommand(checkMiembroQuery, conexionBD.enableConnection());
                int miembroCount = Convert.ToInt32(checkMiembroCommand.ExecuteScalar());

                if (miembroCount == 0)
                {
                    MessageBox.Show("El MiembroID no existe.");
                    return;
                }


                // Modificación de datos si las validaciones son exitosas
                string query = $"UPDATE Miembros SET Nombre = '{nombre.Text}', Direccion = '{direccion.Text}'," +
                               $" Telefono = '{telefono.Text}', Email = '{email.Text}'," +
                               $" FechaRegistro = '{fechaRegistroDT:yyyy-MM-dd}' WHERE MiembroID = '{id.Text}';";

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

        public void LimpiarText(TextBox id, TextBox nombre, TextBox direccion,
            TextBox telefono, TextBox email, TextBox fechaRegistro)
        {
            id.Text = "";
            nombre.Text = "";
            direccion.Text = "";
            telefono.Text = "";
            email.Text = "";
            fechaRegistro.Text = "";
        }

        public void EliminarMiembro(TextBox id)
        {
            ConexionBD conexionBD = new ConexionBD();

            try
            {
                string query = $"DELETE FROM Miembros WHERE MiembroID = '{id.Text}';";

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
