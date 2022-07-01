using Datos;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conexion
{
    public class PrevisionController
    {

        public static List<Prevision> SelectPrevision()
        {
            List<Prevision> previsiones = new();
            using SqliteConnection db = new(Conexion.GetConn());
            SqliteCommand cmd = new("SELECT * FROM PREVISION ORDER BY NOMBRE_PREVISION", db);
            SqliteDataReader reader;
            db.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Prevision prevision = new() 
                {
                    Id_Prev = reader.GetInt32(0),
                    Nom_Prev = reader.GetString(1)
                };
                previsiones.Add(prevision);
            }
                
            db.Close();
            return previsiones;
        }
    }
}
