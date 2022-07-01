using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Bitacora
    {
        private int id;
        private int nFicha;
        private DateTime fechaBitacora;
        private string nom;
        private string descripcion;

        public int NFicha { get => nFicha; set => nFicha = value; }
        public DateTime FechaBitacora { get => fechaBitacora; set => fechaBitacora = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
    }
}
