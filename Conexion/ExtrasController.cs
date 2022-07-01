using Datos;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conexion
{
    public class ExtrasController
    {
        public static List<Extras> SelectTRH()
        {
            List<Extras> TRH = new();
            using SqliteConnection db = new(Conexion.GetConn());
            SqliteCommand cmd = new("SELECT * FROM TRH", db);
            SqliteDataReader reader;
            db.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Extras aux = new()
                {
                    Id = reader.GetInt32(0),
                    Estado = reader.GetString(1)
                };
                TRH.Add(aux);
            }

            db.Close();
            return TRH;
        }
        public static List<Extras> SelectRS()
        {
            List<Extras> RS = new();
            using SqliteConnection db = new(Conexion.GetConn());
            SqliteCommand cmd = new("SELECT * FROM RS", db);
            SqliteDataReader reader;
            db.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Extras aux = new()
                {
                    Id = reader.GetInt32(0),
                    Estado = reader.GetString(1)
                };
                RS.Add(aux);
            }

            db.Close();
            return RS;
        }
    }
}
