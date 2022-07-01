using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Persona
    {
        private string rut;
        private string pNombre;
        private string sNombre;
        private string aPaterno;
        private string aMaterno;
        private string fechaNac;
        private int telofono;
        private string direccion;
        private int prevision;

        public string Rut { get => rut; set => rut = value; }
        public string FechaNac { get => fechaNac; set => fechaNac = value; }
        public int Telofono { get => telofono; set => telofono = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public int Prevision { get => prevision; set => prevision = value; }
        public string PNombre { get => pNombre; set => pNombre = value; }
        public string SNombre { get => sNombre; set => sNombre = value; }
        public string APaterno { get => aPaterno; set => aPaterno = value; }
        public string AMaterno { get => aMaterno; set => aMaterno = value; }
    }
}
