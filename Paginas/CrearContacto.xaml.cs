﻿using ContactDIU.Clases;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace ContactDIU.Paginas
{
    public partial class CrearContacto : Page
    {
        private FicheroContactos fc = new FicheroContactos();
        private Contactos c = new Contactos();
        private SolidColorBrush night = new SolidColorBrush(Colors.Black);
        private SolidColorBrush light = new SolidColorBrush(Colors.White);
        private SolidColorBrush col;
        private String nombreFoto;

        public CrearContacto(Contactos c)
        {
            InitializeComponent();
            PuntoRojoNombre.Visibility = System.Windows.Visibility.Hidden;
            PuntoRojoTelefono.Visibility = System.Windows.Visibility.Hidden;
            this.c = c;
        }

        // Añade el contacto a crear a la agenda

        private void botonAceptar(object sender, RoutedEventArgs e)
        {
            PuntoRojoNombre.Visibility = System.Windows.Visibility.Hidden;
            PuntoRojoTelefono.Visibility = System.Windows.Visibility.Hidden;
            SolidColorBrush color = (SolidColorBrush)CirculoColor.Fill;
            var converted = String.Format("pack://application:,,,/imagenes/camara.png");
            var converted2 = String.Format("pack://application:,,,/imagenes/camaranight.png");
            BitmapImage def = new BitmapImage(new Uri(converted, UriKind.Absolute));
            BitmapImage defn = new BitmapImage(new Uri(converted2, UriKind.Absolute));

            if (!Imagen.Source.ToString().Equals(def.ToString()) && !Imagen.Source.ToString().Equals(defn.ToString()))
            {
                
            }

            else if (Background.ToString().Equals(Colors.Black.ToString()))
            {
                nombreFoto = defn.ToString();
            }

            else if (Background.ToString().Equals(Colors.White.ToString()))
            {
                nombreFoto = def.ToString();
            }

            Usuario u = new Usuario(Nombre.Text, Apellido.Text, Telefono1.Text, Telefono2.Text, Direccion.Text, email.Text, FechaNacimiento.Text, nombreFoto, col);

            if (u.Nombre != "" && u.Telefono1 != "")
            {
                c.Add(u);
                FicheroContactos.guardarContactos(c);
                
                ListaContactos lc = new ListaContactos(c);

                if (this.Background.ToString().Equals(night.ToString()))
                {
                    lc = aparienciaNight(lc);
                }
                if (this.Background.ToString().Equals(light.ToString()))
                {
                    lc = aparienciaLight(lc);
                }

                NavigationService.Navigate(lc);
            }
            else
            {
                if (u.Nombre == "")
                {
                    PuntoRojoNombre.Visibility = System.Windows.Visibility.Visible;
                }
                if (u.Telefono1 == "")
                {
                    PuntoRojoTelefono.Visibility = System.Windows.Visibility.Visible;
                }
            }

        }

        // Cancela la creación del contacto

        private void botonCancelar(object sender, RoutedEventArgs e)
        {
            ListaContactos lc = new ListaContactos(c);

            if (this.Background.ToString().Equals(night.ToString()))
            {
                lc = aparienciaNight(lc);
            }
            if (this.Background.ToString().Equals(light.ToString()))
            {
                lc = aparienciaLight(lc);
            }

            NavigationService.Navigate(lc);
        }

        // Carga la imagen del Contacto

        private void cargarFoto(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Elige una imagen";
            op.Filter = "Archivos de imagen(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";

            if (op.ShowDialog() == true)
            {
                Imagen.Source = new BitmapImage(new Uri(op.FileName));
                string appStartPath = System.IO.Directory.GetCurrentDirectory();
                string name = System.IO.Path.GetFileName(op.FileName);
                string destinationPath = String.Format(appStartPath + "\\{0}\\" + name, "FotosContactos");
                File.Copy(op.FileName, destinationPath, true);
                nombreFoto = name;
            }
        }

        // Valida si en el campo de Telefono solamente se pueda escribir números

        private void validarTelefono(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsControl(e.Text[0]) && !char.IsDigit(e.Text[0]))
            {
                e.Handled = true;
            }
        }

        // Valida si en el campo de FechaNacimiento la fecha es correcta o no

        private void validarFecha(object sender, MouseEventArgs e)
        {
            var picker = sender as DatePicker;
            DateTime? dt = picker.SelectedDate;
            if (dt != null)
            {
                int valor = Convert.ToInt32(dt.Value.Year);
                if (valor <= 1917 || valor >= 2017)
                {
                    FechaNacimiento.Text = "";
                    MessageBox.Show("¡Error en la fecha! Debe de estar comprendida entre 1917 y 2017.");
                }
            }
        }

        // Borrar la fecha añadida al DatePicker

        private void BorrarFecha(object sender, RoutedEventArgs e)
        {
            FechaNacimiento.Text = "";
        }

        // GotFocus y LostFocus para los WaterMakers de los TextBox

        private void NombreWM_GotFocus(object sender, RoutedEventArgs e)
        {
            NombreWM.Visibility = System.Windows.Visibility.Collapsed;
            Nombre.Visibility = System.Windows.Visibility.Visible;
            Nombre.Focus();
        }

        private void NombreWM_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Nombre.Text))
            {
                Nombre.Visibility = System.Windows.Visibility.Collapsed;
                NombreWM.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void ApellidoWM_GotFocus(object sender, RoutedEventArgs e)
        {
            ApellidoWM.Visibility = System.Windows.Visibility.Collapsed;
            Apellido.Visibility = System.Windows.Visibility.Visible;
            Apellido.Focus();
        }

        private void Apellido_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Apellido.Text))
            {
                Apellido.Visibility = System.Windows.Visibility.Collapsed;
                ApellidoWM.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void Telefono1WM_GotFocus(object sender, RoutedEventArgs e)
        {
            Telefono1WM.Visibility = System.Windows.Visibility.Collapsed;
            Telefono1.Visibility = System.Windows.Visibility.Visible;
            Telefono1.Focus();
        }

        private void Telefono1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Telefono1.Text))
            {
                Telefono1.Visibility = System.Windows.Visibility.Collapsed;
                Telefono1WM.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void Telefono2WM_GotFocus(object sender, RoutedEventArgs e)
        {
            Telefono2WM.Visibility = System.Windows.Visibility.Collapsed;
            Telefono2.Visibility = System.Windows.Visibility.Visible;
            Telefono2.Focus();
        }

        private void Telefono2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Telefono2.Text))
            {
                Telefono2.Visibility = System.Windows.Visibility.Collapsed;
                Telefono2WM.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void DireccionWM_GotFocus(object sender, RoutedEventArgs e)
        {
            DireccionWM.Visibility = System.Windows.Visibility.Collapsed;
            Direccion.Visibility = System.Windows.Visibility.Visible;
            Direccion.Focus();
        }

        private void Direccion_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Direccion.Text))
            {
                Direccion.Visibility = System.Windows.Visibility.Collapsed;
                DireccionWM.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void emailWM_GotFocus(object sender, RoutedEventArgs e)
        {
            emailWM.Visibility = System.Windows.Visibility.Collapsed;
            email.Visibility = System.Windows.Visibility.Visible;
            email.Focus();
        }

        private void email_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(email.Text))
            {
                email.Visibility = System.Windows.Visibility.Collapsed;
                emailWM.Visibility = System.Windows.Visibility.Visible;
            }
        }

        // Elección de colores para asociar un color con el contacto

        private void cuadroAzul(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            if (b != null)
            {
                Color c = Colors.Blue;
                CirculoColor.Fill = new SolidColorBrush(c);
                col = new SolidColorBrush(c);
            }
        }

        private void cuadroVerde(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            if (b != null)
            {
                Color c = Colors.Green;
                CirculoColor.Fill = new SolidColorBrush(c);
                col = new SolidColorBrush(c);
            }
        }

        private void cuadroRojo(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            if (b != null)
            {
                Color c = Colors.Red;
                CirculoColor.Fill = new SolidColorBrush(c);
                col = new SolidColorBrush(c);
            }
        }

        private void cuadroAmarillo(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            if (b != null)
            {
                Color c = Colors.Yellow;
                CirculoColor.Fill = new SolidColorBrush(c);
                col = new SolidColorBrush(c);
            }
        }

        private void cuadroRosa(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            if (b != null)
            {
                Color c = Colors.Violet;
                CirculoColor.Fill = new SolidColorBrush(c);
                col = new SolidColorBrush(c);
            }
        }

        private void cuadroLila(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            if (b != null)
            {
                Color c = Colors.Purple;
                CirculoColor.Fill = new SolidColorBrush(c);
                col = new SolidColorBrush(c);
            }
        }

        private void cuadroGris(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            if (b != null)
            {
                Color c = Colors.Silver;
                CirculoColor.Fill = new SolidColorBrush(c);
                col = new SolidColorBrush(c);
            }
        }

        private void cuadroMarron(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            if (b != null)
            {
                Color c = Colors.BurlyWood;
                CirculoColor.Fill = new SolidColorBrush(c);
                col = new SolidColorBrush(c);
            }
        }

        private void cuadroNegro(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            if (b != null)
            {
                Color c = Colors.Black;
                CirculoColor.Fill = new SolidColorBrush(c);
                col = new SolidColorBrush(c);
            }
        }

        // Apariencia de la Interfaz

        private ListaContactos aparienciaNight(ListaContactos lc)
        {
            lc.Background = night;
            lc.Foreground = light;
            string converted = String.Format("pack://application:,,,/imagenes/atrasnight.png");
            string converted2 = String.Format("pack://application:,,,/imagenes/addnight.png");
            string converted3 = String.Format("pack://application:,,,/imagenes/editarnight.png");
            lc.BotonAtras.Source = new BitmapImage(new Uri(converted, UriKind.Absolute));
            lc.BotonAdd.Source = new BitmapImage(new Uri(converted2, UriKind.Absolute));
            lc.BotonEditar.Source = new BitmapImage(new Uri(converted3, UriKind.Absolute));
            lc.Lista.Background = night;
            lc.Lista.Foreground = light;
            lc.Contactos.Foreground = light;

            return lc;
        }

        private ListaContactos aparienciaLight(ListaContactos lc)
        {
            lc.Background = light;
            lc.Foreground = night;
            string converted = String.Format("pack://application:,,,/imagenes/atras.png");
            string converted2 = String.Format("pack://application:,,,/imagenes/add.png");
            string converted3 = String.Format("pack://application:,,,/imagenes/editar.png");
            lc.BotonAtras.Source = new BitmapImage(new Uri(converted, UriKind.Absolute));
            lc.BotonAdd.Source = new BitmapImage(new Uri(converted2, UriKind.Absolute));
            lc.BotonEditar.Source = new BitmapImage(new Uri(converted3, UriKind.Absolute));
            lc.Lista.Background = light;
            lc.Lista.Foreground = night;
            lc.Contactos.Foreground = night;

            return lc;
        }
    }
}