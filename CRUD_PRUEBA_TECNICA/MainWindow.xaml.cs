﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRUD_PRUEBA_TECNICA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Libros libros = new Libros();
            this.Close();
            libros.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Miembros miembros = new Miembros();
            this.Close();
            miembros.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Prestamos prestamos = new Prestamos();
            this.Close();
            prestamos.Show();
        }
    }
}