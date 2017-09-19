using ContactDIU.Clases;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace ContactDIU.Paginas
{
    public partial class PaginaPrincipal : Page
    {
        public Contactos c = null;
        public Application ap = Application.Current;
        private SolidColorBrush night = new SolidColorBrush(Colors.Black);
        private SolidColorBrush light = new SolidColorBrush(Colors.White);
        private String cargarF = "";

        public PaginaPrincipal()
        {
            InitializeComponent();
            c = new Contactos();
            this.Background = new SolidColorBrush(Colors.White);
            cargarF = FicheroContactos.cargarConfiguracion();
            c = FicheroContactos.leerContactos();

            string theme = "";
            if(cargarF != "")
            {
                theme = cargarF.Substring(0, 9);
                string style = cargarF.Substring(9, 6);
                string tamLetra = cargarF.Substring(15);
            }

            if (theme.ToString() == new SolidColorBrush(Colors.Black).ToString())
            {
                aparienciaNight();
            }
            else if (theme.ToString() == new SolidColorBrush(Colors.White).ToString())
            {
                aparienciaLight();
            }
            
            /*if (style.Equals("Normal"))
            {
                ap.MainWindow.FontStyle = FontStyles.Normal;
            }
            else if (style.Equals("Italic"))
            {
                ap.MainWindow.FontStyle = FontStyles.Italic;
            }
            
            ap.MainWindow.FontSize = Convert.ToInt32(tamLetra);*/
        }

        public PaginaPrincipal(Contactos c)
        {
            InitializeComponent();
            this.c = c;
        }

        // Navegación a la página ListaContacto

        private void botonListaContactos(object sender, RoutedEventArgs e)
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

            this.Content = NavigationService.Navigate(lc);
        }

        // Navegación a la página Configuracion

        private void botonConfiguracion(object sender, RoutedEventArgs e)
        {

            Configuracion conf = new Configuracion(c);

            if (this.Background.ToString().Equals(night.ToString()))
            {
                conf = aparienciaNight(conf);
            }
            if (this.Background.ToString().Equals(light.ToString()))
            {
                conf = aparienciaLight(conf);
            }

            this.Content = NavigationService.Navigate(conf);
        }

        // Sale de la aplicación guardando los contactos y configuraciones

        private void botonSalir(object sender, RoutedEventArgs e)
        {
            string style = Application.Current.MainWindow.FontStyle.ToString();
            string size = Application.Current.MainWindow.FontSize.ToString();
            FicheroContactos.guardarConfiguracion(this.Background.ToString(), style, size);
            FicheroContactos.guardarContactos(c);
            Application.Current.Shutdown();
        }

        // Apariencia de la interfaz

        private void aparienciaNight()
        {
            this.Background = night;
            this.Foreground = light;
            this.Lista.BorderBrush = light;
            this.Lista.Foreground = light;
            this.Configura.BorderBrush = light;
            this.Configura.Foreground = light;
            this.Exit.BorderBrush = light;
            this.Exit.Foreground = light;

            string converted = String.Format("pack://application:,,,/imagenes/logonight.png");
            this.Logo.Source = new BitmapImage(new Uri(converted, UriKind.Absolute));
            string converted2 = String.Format("pack://application:,,,/imagenes/agendanight.png");
            this.Agenda.Source = new BitmapImage(new Uri(converted2, UriKind.Absolute));
        }

        private void aparienciaLight()
        {
            this.Foreground = night;
            this.Lista.BorderBrush = night;
            this.Lista.Foreground = night;
            this.Configura.BorderBrush = night;
            this.Configura.Foreground = night;
            this.Exit.BorderBrush = night;
            this.Exit.Foreground = night;

            string converted = String.Format("pack://application:,,,/imagenes/logo.png");
            this.Logo.Source = new BitmapImage(new Uri(converted, UriKind.Absolute));
            string converted2 = String.Format("pack://application:,,,/imagenes/agenda.png");
            this.Agenda.Source = new BitmapImage(new Uri(converted2, UriKind.Absolute));
        }

        private ListaContactos aparienciaNight(ListaContactos lc)
        {
            lc.Background = night;
            lc.Foreground = light;
            string converted = String.Format("pack://application:,,,/imagenes/atrasnight.png");
            lc.BotonAtras.Source = new BitmapImage(new Uri(converted, UriKind.Absolute));
            string converted2 = String.Format("pack://application:,,,/imagenes/addnight.png");
            lc.BotonAdd.Source = new BitmapImage(new Uri(converted2, UriKind.Absolute));
            string converted3 = String.Format("pack://application:,,,/imagenes/editarnight.png");
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
            lc.BotonAtras.Source = new BitmapImage(new Uri(converted, UriKind.Absolute));
            string converted2 = String.Format("pack://application:,,,/imagenes/add.png");
            lc.BotonAdd.Source = new BitmapImage(new Uri(converted2, UriKind.Absolute));
            string converted3 = String.Format("pack://application:,,,/imagenes/editar.png");
            lc.BotonEditar.Source = new BitmapImage(new Uri(converted3, UriKind.Absolute));
            lc.Lista.Background = light;
            lc.Lista.Foreground = night;
            lc.Contactos.Foreground = night;

            return lc;
        }

        private Configuracion aparienciaNight(Configuracion conf)
        {
            conf.Background = night;
            conf.Foreground = light;
            conf.Fuente.BorderBrush = light;
            conf.Fuente.Foreground = light;
            conf.Resetear.BorderBrush = light;
            conf.Resetear.Foreground = light;
            conf.Apariencia.BorderBrush = light;
            conf.Apariencia.Foreground = light;
            string converted = String.Format("pack://application:,,,/imagenes/atrasnight.png");
            BitmapImage image = new BitmapImage(new Uri(converted, UriKind.Absolute));
            conf.AtrasConf.Source = image;
            conf.Apariencia.Content = "Light";

            return conf;
        }

        private Configuracion aparienciaLight(Configuracion conf)
        {
            conf.Background = light;
            conf.Foreground = night;
            conf.Fuente.BorderBrush = night;
            conf.Fuente.Foreground = night;
            conf.Resetear.BorderBrush = night;
            conf.Resetear.Foreground = night;
            conf.Apariencia.BorderBrush = night;
            conf.Apariencia.Foreground = night;
            string converted = String.Format("pack://application:,,,/imagenes/atras.png");
            BitmapImage image = new BitmapImage(new Uri(converted, UriKind.Absolute));
            conf.AtrasConf.Source = image;
            conf.Apariencia.Content = "Night";

            return conf;
        }
    }
}
