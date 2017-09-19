using ContactDIU.Clases;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace ContactDIU.Paginas
{
    public partial class Configuracion : Page
    {
        private Contactos c = new Contactos();
        private SolidColorBrush night = new SolidColorBrush(Colors.Black);
        private SolidColorBrush light = new SolidColorBrush(Colors.White);

        public Configuracion(Contactos c)
        {
            InitializeComponent();
            this.c = c;
            if(Application.Current.MainWindow.FontStyle.Equals(FontStyles.Italic)){
                Fuente.Content = "Normal";
            }
        }

        private void botonAtras(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            if (b != null)
            {
                PaginaPrincipal pp = new PaginaPrincipal(c);

                if (this.Background.ToString().Equals(night.ToString()))
                {
                    pp = aparienciaNight(pp);
                }
                if (this.Background.ToString().Equals(light.ToString()))
                {
                    pp = aparienciaLight(pp);
                }

                this.Content = NavigationService.Navigate(pp);
            }
        }

        // Cambia el tema de la aplicación a Light/Night

        private void botonApariencia(object sender, RoutedEventArgs e)
        {
            SolidColorBrush w = new SolidColorBrush(Colors.White);
            SolidColorBrush b = new SolidColorBrush(Colors.Black);

            if (Apariencia.Content.Equals("Light"))
            {
                aparienciaLight();
            }

            else if (Apariencia.Content.Equals("Night"))
            {
                aparienciaNight();
            }

            string style = Application.Current.MainWindow.FontStyle.ToString();
            string size = Application.Current.MainWindow.FontSize.ToString();
            FicheroContactos.guardarConfiguracion(this.Background.ToString(), style, size);
        }

        // Cambia el estilo de la fuente de la aplicación a Normal/Italic

        private void cambiarFuente(object sender, RoutedEventArgs e)
        {
            if (Fuente.Content.Equals("Italica"))
            {
                Application.Current.MainWindow.FontStyle = FontStyles.Italic;
                Fuente.Content = "Normal";
            }

            else if (Fuente.Content.Equals("Normal"))
            {
                Application.Current.MainWindow.FontStyle = FontStyles.Normal;
                Fuente.Content = "Italica";
            }

            string style = Application.Current.MainWindow.FontStyle.ToString();
            string size = Application.Current.MainWindow.FontSize.ToString();
            FicheroContactos.guardarConfiguracion(this.Background.ToString(), style, size);
        }

        // Cambia el tamaño de la Fuente dependiendo de la posición del Slider

        private void TamFuente(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Application.Current.MainWindow.FontSize = (int)Sl.Value;
            this.Theme.FontSize = Sl.Value;
            this.EstiloFuente.FontSize = Sl.Value;
            this.Cache.FontSize = Sl.Value;
            this.FuenteTam.FontSize = Sl.Value;

            string style = Application.Current.MainWindow.FontStyle.ToString();
            string size = Application.Current.MainWindow.FontSize.ToString();
            FicheroContactos.guardarConfiguracion(this.Background.ToString(), style, size);
        }

        // Elimina los datos de todos los contactos

        private void botonReset(object sender, RoutedEventArgs e)
        {
            int resultado = (int)MessageBox.Show("¿Desea borrar todos los contactos de su agenda?", "Eliminar Datos Agenda", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (resultado)
            {
                case (int)MessageBoxResult.Yes:

                    while (c.Count != 0)
                    {
                        c.RemoveAt(0);
                    }
                    
                    FicheroContactos.guardarContactos(c);
                    this.Content = NavigationService.Navigate(new PaginaPrincipal(c));
                    break;
            }
        }

        // Apariencia de la Interfaz

        private PaginaPrincipal aparienciaNight(PaginaPrincipal pp)
        {
            pp.Background = night;
            pp.Foreground = light;
            pp.Lista.BorderBrush = light;
            pp.Lista.Foreground = light;
            pp.Configura.BorderBrush = light;
            pp.Configura.Foreground = light;
            pp.Exit.BorderBrush = light;
            pp.Exit.Foreground = light;

            string converted = String.Format("pack://application:,,,/imagenes/logonight.png");
            string converted2 = String.Format("pack://application:,,,/imagenes/agendanight.png");
            pp.Logo.Source = new BitmapImage(new Uri(converted, UriKind.Absolute));
            pp.Agenda.Source = new BitmapImage(new Uri(converted2, UriKind.Absolute));

            return pp;
        }

        private PaginaPrincipal aparienciaLight(PaginaPrincipal pp)
        {
            pp.Background = light;
            pp.Foreground = night;
            pp.Lista.BorderBrush = night;
            pp.Lista.Foreground = night;
            pp.Configura.BorderBrush = night;
            pp.Configura.Foreground = night;
            pp.Exit.BorderBrush = night;
            pp.Exit.Foreground = night;

            string converted = String.Format("pack://application:,,,/imagenes/logo.png");
            string converted2 = String.Format("pack://application:,,,/imagenes/agenda.png");
            pp.Logo.Source = new BitmapImage(new Uri(converted, UriKind.Absolute));
            pp.Agenda.Source = new BitmapImage(new Uri(converted2, UriKind.Absolute));

            return pp;
        }

        private void aparienciaNight()
        {
            this.Background = night;
            this.Foreground = light;
            this.Fuente.BorderBrush = light;
            this.Fuente.Foreground = light;
            this.Resetear.BorderBrush = light;
            this.Resetear.Foreground = light;
            this.Apariencia.BorderBrush = light;
            this.Apariencia.Foreground = light;
            string converted = String.Format("pack://application:,,,/imagenes/atrasnight.png");
            this.AtrasConf.Source = new BitmapImage(new Uri(converted, UriKind.Absolute));
            this.Apariencia.Content = "Light";
        }

        private void aparienciaLight()
        {
            this.Background = light;
            this.Foreground = night;
            this.Fuente.BorderBrush = night;
            this.Fuente.Foreground = night;
            this.Resetear.BorderBrush = night;
            this.Resetear.Foreground = night;
            this.Apariencia.BorderBrush = night;
            this.Apariencia.Foreground = night;
            string converted = String.Format("pack://application:,,,/imagenes/atras.png");
            this.AtrasConf.Source = new BitmapImage(new Uri(converted, UriKind.Absolute));
            this.Apariencia.Content = "Night";
        }
    }
}