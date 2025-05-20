using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaDigital.Model
{
    public class Usuarios
    {
        public int Id_Usuario { get; set; }

        public string Nome_Usuario { get; set; }

        public string Email_Usuario { get; set; }

        public DateOnly DtNasc_Usuario { get; set; }

        public string senha {  get; set; }

    }
}
