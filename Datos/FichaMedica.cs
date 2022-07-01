using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class FichaMedica
    {
        private int nFicha;
        private int gesta;
        private int partosT;
        private int partosP;
        private int partosM;
        private int partosV;
        private int pv;
        private int rs;
        private int thr;
        private string observacion;
        private Persona persona;
        private PatoloagiaQui patoloagiaQui;
        private PatologiasMed patologiasMed;
        private MAC metodo;
        private DateTime fecUpdate;
        public int NFicha { get => nFicha; set => nFicha = value; }
        public int Gesta { get => gesta; set => gesta = value; }
        public int Pv { get => pv; set => pv = value; }
        public int Rs { get => rs; set => rs = value; }
        public int Thr { get => thr; set => thr = value; }
        public string Observacion { get => observacion; set => observacion = value; }
        public Persona Persona { get => persona; set => persona = value; }
        public PatoloagiaQui PatoloagiaQui { get => patoloagiaQui; set => patoloagiaQui = value; }
        public PatologiasMed PatologiasMed { get => patologiasMed; set => patologiasMed = value; }
        public MAC Metodo { get => metodo; set => metodo = value; }
        public DateTime FecUpdate { get => fecUpdate; set => fecUpdate = value; }
        public int PartosT { get => partosT; set => partosT = value; }
        public int PartosP { get => partosP; set => partosP = value; }
        public int PartosM { get => partosM; set => partosM = value; }
        public int PartosV { get => partosV; set => partosV = value; }
    }
}
