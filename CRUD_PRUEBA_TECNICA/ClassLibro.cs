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
    internal class ClassLibro
    {
        public void MostrarLibros(DataGrid libros)
        {
            ConexionBD conexionBD = new ConexionBD();

            try
            {
                string query = "SELECT * FROM Libros;";
                MySqlCommand command = new MySqlCommand(query, conexionBD.enableConnection());
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable datatable = new DataTable();
                adapter.Fill(datatable);
                libros.ItemsSource = datatable.DefaultView;
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

        public void GuardarLibro(TextBox titulo, TextBox autor, TextBox isbn, TextBox fechaPublicacion, TextBox genero, TextBox disponibilidad)
        {
            // Validación de campos obligatorios
            if (string.IsNullOrWhiteSpace(titulo.Text) || string.IsNullOrWhiteSpace(autor.Text) ||
                string.IsNullOrWhiteSpace(isbn.Text) || string.IsNullOrWhiteSpace(fechaPublicacion.Text) ||
                string.IsNullOrWhiteSpace(genero.Text) || string.IsNullOrWhiteSpace(disponibilidad.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return;
            }

            ConexionBD conexionBD = new ConexionBD();
            try
            {
                // Verificación de que el ISBN no esté duplicado
                string checkQuery = $"SELECT COUNT(*) FROM Libros WHERE ISBN = '{isbn.Text}'";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, conexionBD.enableConnection());
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("El ISBN ya está registrado. Por favor, use un ISBN diferente.");
                    return;
                }

                // Validación y conversión de disponibilidad a entero
                if (!int.TryParse(disponibilidad.Text, out int disponibilidadINT))
                {
                    MessageBox.Show("El campo Disponibilidad debe ser un número entero.");
                    return;
                }

                // Inserción de datos si las validaciones son exitosas
                string query = $"INSERT INTO Libros(Titulo, Autor, ISBN, FechaPublicacion, Genero, Disponibilidad)" +
                               $" VALUES('{titulo.Text}', '{autor.Text}', '{isbn.Text}', '{fechaPublicacion.Text}', '{genero.Text}', '{disponibilidadINT}');";
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

        public void SeleccionarLibro(DataGrid tablaUsuarios, TextBox id, TextBox titulo,
            TextBox autor, TextBox isbn, TextBox fechaPublicacion, TextBox genero,
            TextBox disponibilidad)
        {
            try
            {
                if (tablaUsuarios.SelectedItem != null)
                {
                    DataRowView rowSelected = (DataRowView)tablaUsuarios.SelectedItem;
                    id.Text = rowSelected.Row.ItemArray[0].ToString();
                    titulo.Text = rowSelected.Row.ItemArray[1].ToString();
                    autor.Text = rowSelected.Row.ItemArray[2].ToString();
                    isbn.Text = rowSelected.Row.ItemArray[3].ToString();
                    
                    if (DateTime.TryParse(rowSelected.Row.ItemArray[4].ToString(), out DateTime fecha))
                    {
                        fechaPublicacion.Text = fecha.ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        fechaPublicacion.Text = rowSelected.Row.ItemArray[5].ToString();
                    }

                    genero.Text = rowSelected.Row.ItemArray[5].ToString();
                    bool isDisponible = false;
                    if (bool.TryParse(rowSelected.Row.ItemArray[6].ToString(), out isDisponible))
                    {
                        disponibilidad.Text = isDisponible ? "1" : "0";
                    }
                    else
                    {
                        disponibilidad.Text = "0";
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al seleccionar. " + e.ToString());
            }
        }

        public void ModificarLibro(TextBox id, TextBox titulo, TextBox autor, TextBox isbn, TextBox fechaPublicacion, TextBox genero, TextBox disponibilidad)
        {
            // Validación de campos obligatorios
            if (string.IsNullOrWhiteSpace(id.Text) || string.IsNullOrWhiteSpace(titulo.Text) ||
                string.IsNullOrWhiteSpace(autor.Text) || string.IsNullOrWhiteSpace(isbn.Text) ||
                string.IsNullOrWhiteSpace(fechaPublicacion.Text) || string.IsNullOrWhiteSpace(genero.Text) ||
                string.IsNullOrWhiteSpace(disponibilidad.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return;
            }

            ConexionBD conexionBD = new ConexionBD();

            try
            {
                // Conversión del campo Disponibilidad a int
                int disponibilidadINT;
                if (!int.TryParse(disponibilidad.Text, out disponibilidadINT))
                {
                    MessageBox.Show("El campo Disponibilidad debe ser un número entero.");
                    return;
                }

                // Verificación de duplicados
                string checkQuery = $"SELECT COUNT(*) FROM Libros WHERE ISBN = '{isbn.Text}' AND LibroID != '{id.Text}'";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, conexionBD.enableConnection());
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("El ISBN ya está registrado. Por favor, use un ISBN diferente.");
                    return;
                }

                // Actualización de datos
                string query = $"UPDATE Libros SET Titulo ='{titulo.Text}'," +
                    $" Autor ='{autor.Text}', ISBN ='{isbn.Text}'," +
                    $" FechaPublicacion ='{fechaPublicacion.Text}', Genero ='{genero.Text}', Disponibilidad = '{disponibilidadINT}'" +
                    $" WHERE LibroID ='{id.Text}';";

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


        public void LimpiarText(TextBox id, TextBox titulo,
            TextBox autor, TextBox isbn, TextBox fechaPublicacion, TextBox genero,
            TextBox disponibilidad)
        {
            id.Text = "";
            titulo.Text = "";
            autor.Text = "";
            isbn.Text = "";
            fechaPublicacion.Text = "";
            genero.Text = "";
            disponibilidad.Text = "";
        }

        public void EliminarLibro(TextBox id)
        {
            ConexionBD conexionBD = new ConexionBD();

            try
            {
                string query = $"DELETE FROM Libros WHERE LibroID = '{id.Text}';";

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
