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
    /// Interaction logic for Miembros.xaml
    /// </summary>
    public partial class Miembros : Window
    {
        public Miembros()
        {
            InitializeComponent();
            ClassMiembro miembros = new ClassMiembro();
            miembros.MostrarMiembros(dataGridMiembros);
            textId.IsEnabled = false;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            ClassMiembro miembros = new ClassMiembro();
            miembros.GuardarMiembro(textNombre, textDireccion, textTelefono, textEmail, textFechaRegistro);
            miembros.MostrarMiembros(dataGridMiembros);
            miembros.LimpiarText(textId, textNombre, textDireccion, textTelefono, textEmail, textFechaRegistro);
        }

        private void dataGridMiembros_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ClassMiembro miembros = new ClassMiembro();
            miembros.SeleccionarMiembro(dataGridMiembros, textId, textNombre, textDireccion,textTelefono, textEmail, textFechaRegistro);
        }

        private void dataGridMiembros_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "FechaRegistro" && e.Column is DataGridTextColumn column)
            {
                column.Binding = new Binding("FechaRegistro")
                {
                    StringFormat = "yyyy/MM/dd"
                };
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            ClassMiembro miembros = new ClassMiembro();
            miembros.ModificarMiembro(textId, textNombre, textDireccion, textTelefono, textEmail, textFechaRegistro);
            miembros.MostrarMiembros(dataGridMiembros);
            miembros.LimpiarText(textId, textNombre, textDireccion, textTelefono, textEmail, textFechaRegistro);
        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            ClassMiembro miembros = new ClassMiembro();
            miembros.EliminarMiembro(textId);
            miembros.MostrarMiembros(dataGridMiembros);
            miembros.LimpiarText(textId, textNombre, textDireccion, textTelefono, textEmail, textFechaRegistro);
        }

        private void dataGridMiembros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Prestamos prestamos = new Prestamos();
            this.Close();
            prestamos.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Libros libros = new Libros();
            this.Close();
            libros.Show();
        }
    }
}
