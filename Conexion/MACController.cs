using Datos;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conexion
{
    public class MACController
    {
        public static List<MAC> SelectMetodos()
        {
            List<MAC> macs = new();
            using SqliteConnection db = new(Conexion.GetConn());
            SqliteCommand cmd = new("SELECT * FROM METODO_ANTICONCEPTIVO  ORDER BY CASE WHEN NOMBRE_MAC = 'NINGUNO' THEN 1 ELSE 0 END, NOMBRE_MAC ASC", db);
            SqliteDataReader reader;
            db.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                MAC mac = new()
                {
                    Id_Mac = reader.GetInt32(0),
                    Nombre_Mac = reader.GetString(1)
                };
                macs.Add(mac);
            }

            db.Close();
            return macs;
        }
    }
}
