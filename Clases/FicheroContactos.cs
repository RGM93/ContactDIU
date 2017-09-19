using System;
using System.Data;
using System.IO;
using System.Windows.Media;
using System.Xml;

namespace ContactDIU.Clases
{
    public class FicheroContactos
    {
        public FicheroContactos()
        {
        }

        // Guardar Contactos en un Fichero XML

        public static void guardarContactos(Contactos c)
        {
            DataTable dt = new DataTable();
            dt.TableName = "Contactos";
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Apellidos");
            dt.Columns.Add("Telefono");
            dt.Columns.Add("Telefono2");
            dt.Columns.Add("FechaNacimiento");
            dt.Columns.Add("Direccion");
            dt.Columns.Add("Email");
            dt.Columns.Add("Color");
            dt.Columns.Add("Foto");

            dt.Rows.Add();

            foreach (var item in c)
            {
                dt.Rows.Add();
                dt.Rows[dt.Rows.Count - 1]["Nombre"] = item.Nombre;
                dt.Rows[dt.Rows.Count - 1]["Apellidos"] = item.Apellido;
                dt.Rows[dt.Rows.Count - 1]["Telefono"] = item.Telefono1;
                dt.Rows[dt.Rows.Count - 1]["Telefono2"] = item.Telefono2;
                dt.Rows[dt.Rows.Count - 1]["FechaNacimiento"] = item.FechaNacimiento;
                dt.Rows[dt.Rows.Count - 1]["Direccion"] = item.Direccion;
                dt.Rows[dt.Rows.Count - 1]["Email"] = item.Email;
                dt.Rows[dt.Rows.Count - 1]["Color"] = item.Color;
                dt.Rows[dt.Rows.Count - 1]["Foto"] = item.Foto;
            }

            dt.WriteXml("Contactos.Xml");
        }

        // Leer Contactos desde un Fichero XML

        public static Contactos leerContactos()
        {
            XmlTextReader reader = new XmlTextReader("Contactos.xml");
            reader.WhitespaceHandling = WhitespaceHandling.None;
            XmlNodeType nt;
            Contactos c = new Contactos();
            Usuario u = new Usuario();

            if (File.Exists("Contactos.xml"))
            {
                while (reader.Read())
                {
                    nt = reader.NodeType;

                    if (nt == XmlNodeType.Element)
                    {
                        if (reader.Name == "Nombre")
                        {
                            u = new Usuario();
                            reader.Read();
                            u.Nombre = reader.Value;
                        }
                        if (reader.Name == "Apellidos")
                        {
                            reader.Read();
                            u.Apellido = reader.Value;
                        }
                        if (reader.Name == "Telefono")
                        {
                            reader.Read();
                            u.Telefono1 = reader.Value;
                        }
                        if (reader.Name == "Telefono2")
                        {
                            reader.Read();
                            u.Telefono2 = reader.Value;
                        }
                        if (reader.Name == "FechaNacimiento")
                        {
                            reader.Read();
                            u.FechaNacimiento = reader.Value;
                        }
                        if (reader.Name == "Direccion")
                        {
                            reader.Read();
                            u.Direccion = reader.Value;
                        }
                        if (reader.Name == "Email")
                        {
                            reader.Read();
                            u.Email = reader.Value;
                        }
                        if (reader.Name == "Color")
                        {
                            reader.Read();
                            u.Color = (SolidColorBrush)(new BrushConverter().ConvertFrom(reader.Value));
                        }
                        if (reader.Name == "Foto")
                        {
                            reader.Read();
                            if(!reader.Value.Equals("pack://application:,,,/imagenes/camara.png") && !reader.Value.Equals("pack://application:,,,/imagenes/camaranight.png"))
                            {
                                string appStartPath = System.IO.Directory.GetCurrentDirectory() + "\\FotosContactos\\" + reader.Value;
                                if (File.Exists(appStartPath))
                                {
                                    u.foto = reader.Value;
                                }
                            }
                            else
                            {
                                u.foto = reader.Value;
                            }

                            c.Add(u);
                        }
                    }
                }
                reader.Close();
            }

            return c;
        }

        // Guardar Configuración de la Aplicación desde un fichero TXT

        public static void guardarConfiguracion(String theme, String style, String size)
        {
            
            StreamWriter fichero = new StreamWriter("Configuracion.txt");

            if (fichero != null)
            {
                fichero.WriteLine(theme.ToString());
                fichero.WriteLine(style.ToString());
                fichero.WriteLine(size.ToString());
            }

            fichero.Close();
        }


        //Leer Configuración de la Aplicación desde un Fichero TXT

        public static string cargarConfiguracion()
        {
            SolidColorBrush theme = new SolidColorBrush();
            string cargarF = "";
            if (File.Exists("Configuracion.txt"))
            {
                StreamReader fichero = new StreamReader("Configuracion.txt");
                
                if (fichero != null)
                {
                    // Leemos los colores predeterminados de la interfaz 
                    string backg = fichero.ReadLine();
                    string style = fichero.ReadLine();
                    string tamLetra = fichero.ReadLine();
                    
                    cargarF = cargarF + backg + style + tamLetra;
                }
                fichero.Close();
            }

            return cargarF;
        }
    }
}