using ContactDIU.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace ContactDIU.Paginas
{
    public partial class ListaContactos : System.Windows.Controls.Page
    {
        private Contactos c = new Contactos();
        private SolidColorBrush night = new SolidColorBrush(Colors.Black);
        private SolidColorBrush light = new SolidColorBrush(Colors.White);
        private DataTable tablaContactos;
        private List<Usuario> l;

        // Constructor donde se inicializa la Lista de Contactos

        public ListaContactos(Contactos c)
        {
            InitializeComponent();
            this.c = c;
            tablaContactos = new DataTable();
            l = new List<Usuario>();

            for (int i = 0; i < c.Count; i++)
            {
                l.Add(c.ElementAt(i));
            }

            List<Usuario> ls = l.OrderBy(o => o.Nombre).ToList();
            Lista.ItemsSource = ls;
            CollectionView vista = null;
            if (Lista != null)
            {
                vista = (CollectionView)CollectionViewSource.GetDefaultView(Lista.ItemsSource);
                if (vista != null)
                {
                    vista.Filter = FiltrarUsuario;
                }
            }
            
        }

        // Filtra los contactos por su Nombre

        private void filtrarContactos(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Lista.ItemsSource).Refresh();
        }

        private bool FiltrarUsuario(object item)
        {
            if (String.IsNullOrEmpty(Busqueda.Text))
                return true;
            else
                return ((item as Usuario).Nombre.IndexOf(Busqueda.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        // Botón Atrás para navegar a la página PaginaPrincipal

        private void botonAtras(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button b = (System.Windows.Controls.Button)sender;
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

        // Navegación a la página CrearContacto

        private void botonCrear(object sender, RoutedEventArgs e)
        {
            Button b = (Button) sender;
            if (b != null)
            {
                CrearContacto cc = new CrearContacto(c);

                if (this.Background.ToString().Equals(night.ToString()))
                {
                    cc = aparienciaNight(cc);
                }
                if (this.Background.ToString().Equals(light.ToString()))
                {
                    cc = aparienciaLight(cc);
                }

                this.Content = NavigationService.Navigate(cc); 
            }
        }

        // Navegación a la página EditarContacto

        private void botonEditar(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            if (b != null)
            {
                Usuario u = new Usuario();
                u = (Usuario)Lista.SelectedItem;
                EditarContacto cc = new EditarContacto(c, u);

                if (u != null)
                {
                    if (this.Background.ToString().Equals(night.ToString()))
                    {
                        cc = aparienciaNight(cc, u);
                    }
                    if (this.Background.ToString().Equals(light.ToString()))
                    {
                        cc = aparienciaLight(cc, u);
                    }

                    this.Content = NavigationService.Navigate(cc); 
                }
            }
        }

        // GotFocus y LostFocus para el WaterMaker del TextBox "Busqueda"

        private void Busqueda_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Busqueda.Text))
            {
                Busqueda.Visibility = System.Windows.Visibility.Collapsed;
                BusquedaWM.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void BusquedaWM_GotFocus(object sender, RoutedEventArgs e)
        {
            BusquedaWM.Visibility = System.Windows.Visibility.Collapsed;
            Busqueda.Visibility = System.Windows.Visibility.Visible;
            Busqueda.Focus();
        }

        // Apariencia de la interfaz

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

        private CrearContacto aparienciaNight(CrearContacto cc)
        {
            cc.Background = night;
            cc.Foreground = light;
            cc.CreaContacto.Foreground = light;
            cc.Nombre.Background = night;
            cc.Nombre.BorderBrush = light;
            cc.Nombre.Foreground = light;
            cc.NombreWM.Background = night;
            cc.NombreWM.BorderBrush = light;
            cc.NombreWM.Foreground = light;
            cc.PuntoRojoNombre.Background = night;
            cc.Apellido.Background = night;
            cc.Apellido.BorderBrush = light;
            cc.Apellido.Foreground = light;
            cc.ApellidoWM.Background = night;
            cc.ApellidoWM.BorderBrush = light;
            cc.ApellidoWM.Foreground = light;
            cc.Telefono1.Background = night;
            cc.Telefono1.BorderBrush = light;
            cc.Telefono1.Foreground = light;
            cc.Telefono1WM.Background = night;
            cc.Telefono1WM.BorderBrush = light;
            cc.Telefono1WM.Foreground = light;
            cc.PuntoRojoTelefono.Background = night;
            cc.Telefono2.Background = night;
            cc.Telefono2.BorderBrush = light;
            cc.Telefono2.Foreground = light;
            cc.Telefono2WM.Background = night;
            cc.Telefono2WM.BorderBrush = light;
            cc.Telefono2WM.Foreground = light;
            cc.Direccion.Background = night;
            cc.Direccion.BorderBrush = light;
            cc.Direccion.Foreground = light;
            cc.DireccionWM.Background = night;
            cc.DireccionWM.BorderBrush = light;
            cc.DireccionWM.Foreground = light;
            cc.email.Background = night;
            cc.email.BorderBrush = light;
            cc.email.Foreground = light;
            cc.emailWM.Background = night;
            cc.emailWM.BorderBrush = light;
            cc.emailWM.Foreground = light;
            cc.FechaNacimiento.Background = night;
            cc.FechaNacimiento.BorderBrush = light;
            cc.FechaNacimiento.Foreground = night;
            cc.Aceptar.Background = night;
            cc.Aceptar.BorderBrush = light;
            cc.Aceptar.Foreground = light;
            cc.Cancelar.Background = night;
            cc.Cancelar.BorderBrush = light;
            cc.Cancelar.Foreground = light;
            cc.CirculoColor.Stroke = light;
            cc.Azul.BorderBrush = light;
            cc.Verde.BorderBrush = light;
            cc.Rojo.BorderBrush = light;
            cc.Amarillo.BorderBrush = light;
            cc.Rosa.BorderBrush = light;
            cc.Lila.BorderBrush = light;
            cc.Gris.BorderBrush = light;
            cc.Marrón.BorderBrush = light;
            cc.Negro.BorderBrush = light;

            string converted = String.Format("pack://application:,,,/imagenes/camaranight.png");
            string converted2 = String.Format("pack://application:,,,/imagenes/telefononight.png");
            string converted3 = String.Format("pack://application:,,,/imagenes/calendarionight.png");
            string converted4 = String.Format("pack://application:,,,/imagenes/casanight.png");
            string converted5 = String.Format("pack://application:,,,/imagenes/cerrarnight.png");
            cc.Imagen.Source = new BitmapImage(new Uri(converted, UriKind.Absolute));
            cc.Ring.Source = new BitmapImage(new Uri(converted2, UriKind.Absolute));
            cc.Calendario.Source = new BitmapImage(new Uri(converted3, UriKind.Absolute));
            cc.Casa.Source = new BitmapImage(new Uri(converted4, UriKind.Absolute));
            cc.FechaBasura.Source = new BitmapImage(new Uri(converted5, UriKind.Absolute));

            cc.simbol.Foreground = light;

            return cc;
        }

        private CrearContacto aparienciaLight(CrearContacto cc)
        {
            cc.Background = light;
            cc.Foreground = night;
            cc.CreaContacto.Foreground = night;
            cc.Nombre.Background = light;
            cc.Nombre.BorderBrush = night;
            cc.Nombre.Foreground = night;
            cc.NombreWM.Background = light;
            cc.NombreWM.BorderBrush = night;
            cc.NombreWM.Foreground = night;
            cc.PuntoRojoNombre.Background = light;
            cc.Apellido.Background = light;
            cc.Apellido.BorderBrush = night;
            cc.Apellido.Foreground = night;
            cc.ApellidoWM.Background = light;
            cc.ApellidoWM.BorderBrush = night;
            cc.ApellidoWM.Foreground = night;
            cc.Telefono1.Background = light;
            cc.Telefono1.BorderBrush = night;
            cc.Telefono1.Foreground = night;
            cc.Telefono1WM.Background = light;
            cc.Telefono1WM.BorderBrush = night;
            cc.Telefono1WM.Foreground = night;
            cc.PuntoRojoTelefono.Background = light;
            cc.Telefono2.Background = light;
            cc.Telefono2.BorderBrush = night;
            cc.Telefono2.Foreground = night;
            cc.Telefono2WM.Background = light;
            cc.Telefono2WM.BorderBrush = night;
            cc.Telefono2WM.Foreground = night;
            cc.Direccion.Background = light;
            cc.Direccion.BorderBrush = night;
            cc.Direccion.Foreground = night;
            cc.DireccionWM.Background = light;
            cc.DireccionWM.BorderBrush = night;
            cc.DireccionWM.Foreground = night;
            cc.email.Background = light;
            cc.email.BorderBrush = night;
            cc.email.Foreground = night;
            cc.emailWM.Background = light;
            cc.emailWM.BorderBrush = night;
            cc.emailWM.Foreground = night;
            cc.FechaNacimiento.Background = light;
            cc.FechaNacimiento.BorderBrush = night;
            cc.FechaNacimiento.Foreground = night;
            cc.Aceptar.Background = light;
            cc.Aceptar.BorderBrush = night;
            cc.Aceptar.Foreground = night;
            cc.Cancelar.Background = light;
            cc.Cancelar.BorderBrush = night;
            cc.Cancelar.Foreground = night;
            cc.CirculoColor.Stroke = night;
            cc.Azul.BorderBrush = night;
            cc.Verde.BorderBrush = night;
            cc.Rojo.BorderBrush = night;
            cc.Amarillo.BorderBrush = night;
            cc.Rosa.BorderBrush = night;
            cc.Lila.BorderBrush = night;
            cc.Gris.BorderBrush = night;
            cc.Marrón.BorderBrush = night;
            cc.Negro.BorderBrush = night;

            string converted = String.Format("pack://application:,,,/imagenes/camara.png");
            string converted2 = String.Format("pack://application:,,,/imagenes/telefono.png");
            string converted3 = String.Format("pack://application:,,,/imagenes/calendario.png");
            string converted4 = String.Format("pack://application:,,,/imagenes/casa.png");
            string converted5 = String.Format("pack://application:,,,/imagenes/cerrar.png");
            cc.Imagen.Source = new BitmapImage(new Uri(converted, UriKind.Absolute));
            cc.Ring.Source = new BitmapImage(new Uri(converted2, UriKind.Absolute));
            cc.Calendario.Source = new BitmapImage(new Uri(converted3, UriKind.Absolute));
            cc.Casa.Source = new BitmapImage(new Uri(converted4, UriKind.Absolute));
            cc.FechaBasura.Source = new BitmapImage(new Uri(converted5, UriKind.Absolute));

            cc.simbol.Foreground = night;

            return cc;
        }

        private EditarContacto aparienciaNight(EditarContacto cc, Usuario u)
        {
            cc.Background = night;
            cc.Foreground = light;
            cc.EditaContacto.Foreground = light;
            cc.Nombre.Background = night;
            cc.Nombre.BorderBrush = light;
            cc.Nombre.Foreground = light;
            cc.NombreWM.Background = night;
            cc.NombreWM.BorderBrush = light;
            cc.NombreWM.Foreground = light;
            cc.PuntoRojoNombre.Background = night;
            cc.Apellido.Background = night;
            cc.Apellido.BorderBrush = light;
            cc.Apellido.Foreground = light;
            cc.ApellidoWM.Background = night;
            cc.ApellidoWM.BorderBrush = light;
            cc.ApellidoWM.Foreground = light;
            cc.Telefono1.Background = night;
            cc.Telefono1.BorderBrush = light;
            cc.Telefono1.Foreground = light;
            cc.Telefono1WM.Background = night;
            cc.Telefono1WM.BorderBrush = light;
            cc.Telefono1WM.Foreground = light;
            cc.PuntoRojoTelefono.Background = night;
            cc.Telefono2.Background = night;
            cc.Telefono2.BorderBrush = light;
            cc.Telefono2.Foreground = light;
            cc.Telefono2WM.Background = night;
            cc.Telefono2WM.BorderBrush = light;
            cc.Telefono2WM.Foreground = light;
            cc.Direccion.Background = night;
            cc.Direccion.BorderBrush = light;
            cc.Direccion.Foreground = light;
            cc.DireccionWM.Background = night;
            cc.DireccionWM.BorderBrush = light;
            cc.DireccionWM.Foreground = light;
            cc.email.Background = night;
            cc.email.BorderBrush = light;
            cc.email.Foreground = light;
            cc.emailWM.Background = night;
            cc.emailWM.BorderBrush = light;
            cc.emailWM.Foreground = light;
            cc.FechaNacimiento.Background = night;
            cc.FechaNacimiento.BorderBrush = light;
            cc.FechaNacimiento.Foreground = night;
            cc.Aceptar.Background = night;
            cc.Aceptar.BorderBrush = light;
            cc.Aceptar.Foreground = light;
            cc.Cancelar.Background = night;
            cc.Cancelar.BorderBrush = light;
            cc.Cancelar.Foreground = light;
            cc.CirculoColor.Stroke = light;
            cc.Azul.BorderBrush = light;
            cc.Verde.BorderBrush = light;
            cc.Rojo.BorderBrush = light;
            cc.Amarillo.BorderBrush = light;
            cc.Rosa.BorderBrush = light;
            cc.Lila.BorderBrush = light;
            cc.Gris.BorderBrush = light;
            cc.Marrón.BorderBrush = light;
            cc.Negro.BorderBrush = light;

            if (u.Foto.Contains("camara.png"))
            {
                var converted = String.Format("pack://application:,,,/imagenes/camaranight.png");
                cc.Imagen.Source = new BitmapImage(new Uri(converted, UriKind.Absolute));
            }

            else if (u.Foto.Contains("camaranight.png"))
            {
                var converted = String.Format("pack://application:,,,/imagenes/camaranight.png");
                cc.Imagen.Source = new BitmapImage(new Uri(converted, UriKind.Absolute));
            }

            string converted2 = String.Format("pack://application:,,,/imagenes/telefononight.png");
            string converted3 = String.Format("pack://application:,,,/imagenes/calendarionight.png");
            string converted4 = String.Format("pack://application:,,,/imagenes/casanight.png");
            string converted5 = String.Format("pack://application:,,,/imagenes/deletenight.png");
            string converted6 = String.Format("pack://application:,,,/imagenes/cerrarnight.png");
            cc.Ring.Source = new BitmapImage(new Uri(converted2, UriKind.Absolute));
            cc.Calendario.Source = new BitmapImage(new Uri(converted3, UriKind.Absolute));
            cc.Casa.Source = new BitmapImage(new Uri(converted4, UriKind.Absolute));
            cc.Basura.Source = new BitmapImage(new Uri(converted5, UriKind.Absolute));
            cc.FechaBasura.Source = new BitmapImage(new Uri(converted6, UriKind.Absolute));

            cc.simbol.Foreground = light;

            return cc;
        }

        private EditarContacto aparienciaLight(EditarContacto cc, Usuario u)
        {
            cc.Background = light;
            cc.Foreground = night;
            cc.EditaContacto.Foreground = night;
            cc.Nombre.Background = light;
            cc.Nombre.BorderBrush = night;
            cc.Nombre.Foreground = night;
            cc.NombreWM.Background = light;
            cc.NombreWM.BorderBrush = night;
            cc.NombreWM.Foreground = night;
            cc.PuntoRojoNombre.Background = light;
            cc.Apellido.Background = light;
            cc.Apellido.BorderBrush = night;
            cc.Apellido.Foreground = night;
            cc.ApellidoWM.Background = light;
            cc.ApellidoWM.BorderBrush = night;
            cc.ApellidoWM.Foreground = night;
            cc.Telefono1.Background = light;
            cc.Telefono1.BorderBrush = night;
            cc.Telefono1.Foreground = night;
            cc.Telefono1WM.Background = light;
            cc.Telefono1WM.BorderBrush = night;
            cc.Telefono1WM.Foreground = night;
            cc.PuntoRojoTelefono.Background = light;
            cc.Telefono2.Background = light;
            cc.Telefono2.BorderBrush = night;
            cc.Telefono2.Foreground = night;
            cc.Telefono2WM.Background = light;
            cc.Telefono2WM.BorderBrush = night;
            cc.Telefono2WM.Foreground = night;
            cc.Direccion.Background = light;
            cc.Direccion.BorderBrush = night;
            cc.Direccion.Foreground = night;
            cc.DireccionWM.Background = light;
            cc.DireccionWM.BorderBrush = night;
            cc.DireccionWM.Foreground = night;
            cc.email.Background = light;
            cc.email.BorderBrush = night;
            cc.email.Foreground = night;
            cc.emailWM.Background = light;
            cc.emailWM.BorderBrush = night;
            cc.emailWM.Foreground = night;
            cc.FechaNacimiento.Background = light;
            cc.FechaNacimiento.BorderBrush = night;
            cc.FechaNacimiento.Foreground = night;
            cc.Aceptar.Background = light;
            cc.Aceptar.BorderBrush = night;
            cc.Aceptar.Foreground = night;
            cc.Cancelar.Background = light;
            cc.Cancelar.BorderBrush = night;
            cc.Cancelar.Foreground = night;
            cc.CirculoColor.Stroke = night;
            cc.Azul.BorderBrush = night;
            cc.Verde.BorderBrush = night;
            cc.Rojo.BorderBrush = night;
            cc.Amarillo.BorderBrush = night;
            cc.Rosa.BorderBrush = night;
            cc.Lila.BorderBrush = night;
            cc.Gris.BorderBrush = night;
            cc.Marrón.BorderBrush = night;
            cc.Negro.BorderBrush = night;

            if (u.Foto.Contains("camaranight.png"))
            {
                var converted = String.Format("pack://application:,,,/imagenes/camara.png");
                cc.Imagen.Source = new BitmapImage(new Uri(converted, UriKind.Absolute));
            }

            else if (u.Foto.Contains("camaranight.png"))
            {
                var converted = String.Format("pack://application:,,,/imagenes/camara.png");
                cc.Imagen.Source = new BitmapImage(new Uri(converted, UriKind.Absolute));
            }

            string converted2 = String.Format("pack://application:,,,/imagenes/telefono.png");
            string converted3 = String.Format("pack://application:,,,/imagenes/calendario.png");
            string converted4 = String.Format("pack://application:,,,/imagenes/casa.png");
            string converted5 = String.Format("pack://application:,,,/imagenes/delete.png");
            string converted6 = String.Format("pack://application:,,,/imagenes/cerrar.png");
            cc.Ring.Source = new BitmapImage(new Uri(converted2, UriKind.Absolute));
            cc.Calendario.Source = new BitmapImage(new Uri(converted3, UriKind.Absolute));
            cc.Casa.Source = new BitmapImage(new Uri(converted4, UriKind.Absolute));
            cc.Basura.Source = new BitmapImage(new Uri(converted5, UriKind.Absolute));
            cc.FechaBasura.Source = new BitmapImage(new Uri(converted6, UriKind.Absolute));

            cc.simbol.Foreground = night;

            return cc;
        }
    }
}