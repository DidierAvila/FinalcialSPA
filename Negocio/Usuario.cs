using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;

namespace Negocio
{
    public class Usuario
    {
        private string Username;
        private string Password;

        public string _Password
        {
            get { return Password; }
            set { Password = value; }
        }
        
        public string _Username
        {
            get { return Username; }
            set { Username = value; }
        }


        public bool IniciarSesion()
        {
            return true;
        }
    }
}
