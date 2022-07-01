using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Prevision
    {
        private int id_Prev;
        private string nom_Prev;

        public int Id_Prev { get => id_Prev; set => id_Prev = value; }
        public string Nom_Prev { get => nom_Prev; set => nom_Prev = value; }
    }
}
