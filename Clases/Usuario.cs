using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace ContactDIU.Clases
{
    [Serializable]
    public class Usuario 
    {
        public String nombre, apellido, direccion, email, fechaNacimiento;
        public String telefono1, telefono2;
        public SolidColorBrush color = new SolidColorBrush(Colors.Black);
        public String foto;

        public Usuario()
        {
        }

        public Usuario(String nombre, String apellido, String telefono1, String telefono2, String direccion, String email, String fechaNacimiento, String foto, SolidColorBrush color){
            if (apellido.Equals("")) { apellido = null; };
            if (telefono2.Equals("")) { telefono2 = null; };
            if (direccion.Equals("")) { direccion = null; };
            if (email.Equals("")) { email = null; };
            if (fechaNacimiento.Equals("")) { fechaNacimiento = null; };

            this.nombre = nombre; this.apellido = apellido; this.telefono1 = telefono1; this.telefono2 = telefono2;
            this.fechaNacimiento = fechaNacimiento; this.direccion = direccion; this.email = email;
            this.foto = foto; this.color = color;
        }

        public String Nombre
        {
            get {return nombre;}
            set { nombre = value;}
        }

        public String Apellido
        {
            get {return apellido;}
            set { apellido = value;}
        }

        public String Direccion
        {
            get {return direccion;}
            set { direccion = value;}
        }

        public String Email
        {
            get {return email;}
            set { email = value;}
        }

        public String Telefono1
        {
            get {return telefono1;}
            set { telefono1 = value;}
        }

        public String Telefono2
        {
            get {return telefono2;}
            set { telefono2 = value;}
        }

        public String FechaNacimiento
        {
            get {return fechaNacimiento;}
            set { fechaNacimiento = value;}
        }

        public SolidColorBrush Color
        {
            get { return color; }
            set { color = value; }
        }

        public String Foto
        {
            get{return foto;}
            set{foto = value;}
        }

        public String NombreCompleto
        {
            get { return "    " + this.Nombre + " " + this.Apellido; }
        }

        public override string ToString()
        {
            return "    " + this.Nombre + " " + this.Apellido ;
        }
    }
}
