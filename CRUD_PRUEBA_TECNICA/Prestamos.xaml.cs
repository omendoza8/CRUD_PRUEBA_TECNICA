using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CRUD_PRUEBA_TECNICA
{
    /// <summary>
    /// Interaction logic for Prestamos.xaml
    /// </summary>
    public partial class Prestamos : Window
    {
        public Prestamos()
        {
            InitializeComponent();

            ClassPrestamo prestamos = new ClassPrestamo();
            prestamos.MostrarPrestamos(dataGridPrestamos);
            textPrestamoID.IsEnabled = false;
        }

        private void dataGridPrestamos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClassPrestamo prestamos = new ClassPrestamo();
            prestamos.GuardarPrestamo(textMiembroID, textLibroID, textFechaPrestamo, textFechaDevolucion, textDevuelto);
            prestamos.MostrarPrestamos(dataGridPrestamos);
            prestamos.LimpiarText(textPrestamoID, textMiembroID, textLibroID, textFechaPrestamo, textFechaDevolucion, textDevuelto);
        }

        private void dataGridPrestamos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ClassPrestamo prestamos = new ClassPrestamo();
            prestamos.SeleccionarPrestamo(dataGridPrestamos, textPrestamoID, textMiembroID, textLibroID,textFechaPrestamo, textFechaDevolucion, textDevuelto);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ClassPrestamo prestamos = new ClassPrestamo();
            prestamos.ModificarPrestamo(textPrestamoID, textMiembroID, textLibroID, textFechaPrestamo, textFechaDevolucion, textDevuelto);
            prestamos.MostrarPrestamos(dataGridPrestamos);
            prestamos.LimpiarText(textPrestamoID, textMiembroID, textLibroID, textFechaPrestamo, textFechaDevolucion, textDevuelto);

        }

        private void dataGridPrestamos_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((e.PropertyName == "FechaPrestamo" || e.PropertyName == "FechaDevolucion") && e.Column is DataGridTextColumn column)
            {
                column.Binding = new Binding(e.PropertyName)
                {
                    StringFormat = "yyyy/MM/dd"
                };
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ClassPrestamo prestamos = new ClassPrestamo();
            prestamos.EliminarPrestamo(textPrestamoID);
            prestamos.MostrarPrestamos(dataGridPrestamos);
            prestamos.LimpiarText(textPrestamoID, textMiembroID, textLibroID, textFechaPrestamo, textFechaDevolucion, textDevuelto);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Miembros miembros = new Miembros();
            this.Close();
            miembros.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Libros libros = new Libros();
            this.Close();
            libros.Show();
        }
    }

}
