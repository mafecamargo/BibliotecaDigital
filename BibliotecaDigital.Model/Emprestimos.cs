using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaDigital.Model
{
    public class Emprestimos
    {
        public int Id_Emprestimo { get; set; }

        public DateOnly Dt_Emprestimo { get; set; }

        public DateOnly Dt_Devolucao { get; set; }
        public int Id_Usuario { get; set; }

        public int Id_Livro { get; set; }

    }
}
