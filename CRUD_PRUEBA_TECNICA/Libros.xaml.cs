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
    /// Interaction logic for Libros.xaml
    /// </summary>
    public partial class Libros : Window
    {
        public Libros()
        {
            InitializeComponent();
            ClassLibro libros = new ClassLibro();
            libros.MostrarLibros(dataGridLibros);
            textID.IsEnabled = false;
        }

        private void dataGridLibros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void btnGuardarLibro_Click(object sender, RoutedEventArgs e)
        {
            ClassLibro libro = new ClassLibro();
            libro.GuardarLibro(textTitulo, textAutor, textISBN, textFechaPublicacion,textGenero, textDisponibilidad);
            libro.MostrarLibros(dataGridLibros);
            libro.LimpiarText(textID, textTitulo, textAutor, textISBN, textFechaPublicacion, textGenero, textDisponibilidad);
        }

        private void dataGridLibros_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ClassLibro libro = new ClassLibro();
            libro.SeleccionarLibro(dataGridLibros, textID, textTitulo, textAutor, textISBN, textFechaPublicacion, textGenero, textDisponibilidad);
        }

        private void btnModificarLibro_Click(object sender, RoutedEventArgs e)
        {
            ClassLibro libro = new ClassLibro();
            libro.ModificarLibro(textID, textTitulo, textAutor, textISBN, textFechaPublicacion, textGenero, textDisponibilidad);
            libro.MostrarLibros(dataGridLibros);
            libro.LimpiarText(textID, textTitulo, textAutor, textISBN, textFechaPublicacion, textGenero, textDisponibilidad);
        }

        private void dataGridLibros_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "FechaPublicacion" && e.Column is DataGridTextColumn column)
            {
                column.Binding = new Binding("FechaPublicacion")
                {
                    StringFormat = "yyyy/MM/dd"
                };
            }
        }

        private void btnBorrarLibro_Click(object sender, RoutedEventArgs e)
        {
            ClassLibro libro = new ClassLibro();
            libro.EliminarLibro(textID);
            libro.MostrarLibros(dataGridLibros);
            libro.LimpiarText(textID, textTitulo, textAutor, textISBN, textFechaPublicacion, textGenero, textDisponibilidad);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Miembros miembros = new Miembros();
            this.Close();
            miembros.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Prestamos prestamos = new Prestamos();
            this.Close();
            prestamos.Show();
        }
    }
}
