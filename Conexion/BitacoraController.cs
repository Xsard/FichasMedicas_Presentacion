using Datos;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conexion
{
    public class BitacoraController
    {
        public static int InsertBitacora(Bitacora bitacora)
        {
            int insert;
            using SqliteConnection db = new(Conexion.GetConn());
            try
            {
                SqliteCommand cmd = new("INSERT INTO BITACORA(FECHA, NFICHA, NOMB, DESCRIPCION) VALUES(@FECHA,@NFICHA, @NOM,@DESC)", db);
                cmd.Parameters.AddWithValue("@FECHA", bitacora.FechaBitacora);
                cmd.Parameters.AddWithValue("@NFICHA", bitacora.NFicha);
                cmd.Parameters.AddWithValue("@NOM", bitacora.Nom);
                cmd.Parameters.AddWithValue("@DESC", bitacora.Descripcion);
                db.Open();
                insert = cmd.ExecuteNonQuery();
                db.Close();
            }
            catch (Exception)
            {
                return -1;
            }

            return insert;
        }
        public static List<Bitacora> Bitacoras(int NFicha) 
        {
            List<Bitacora> bitacoras = new();
            using SqliteConnection db = new(Conexion.GetConn());
            try
            {
                SqliteCommand cmd = new("SELECT * FROM BITACORA WHERE NFICHA = @NFICHA ORDER BY FECHA DESC", db);
                cmd.Parameters.AddWithValue("@NFICHA", NFicha);
                SqliteDataReader reader;
                db.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Bitacora bitacora = new()
                    {
                        Id = reader.GetInt32(0),
                        FechaBitacora = DateTime.Parse(reader.GetString(1)),
                        Nom = reader.GetString(2),
                        NFicha = reader.GetInt32(3),
                        Descripcion = reader.GetString(4)
                    };
                    bitacoras.Add(bitacora);
                }

            }
            catch (Exception)
            {
            }
            finally
            {
                db.Close();
            }
            return bitacoras.OrderByDescending(x => x.FechaBitacora).ToList();
        }
        public static int EliminarBitacoras(int id)
        {
            int delete;
            using SqliteConnection db = new(Conexion.GetConn());
            try
            {
                SqliteCommand cmd = new("DELETE FROM BITACORA WHERE ID_BITACORA = @ID", db);
                cmd.Parameters.AddWithValue("@ID", id);
                db.Open();
                delete = cmd.ExecuteNonQuery();
                db.Close();
            }
            catch (Exception)
            {
                return -1;
            }

            return delete;
        }
        public static int UpdateBitacora(Bitacora bitacora)
        {
            int delete;
            using SqliteConnection db = new(Conexion.GetConn());
            try
            {
                SqliteCommand cmd = new("UPDATE BITACORA SET FECHA = @FECHA, NOMB = @NOM, DESCRIPCION = @DESC WHERE ID_BITACORA = @ID", db);
                cmd.Parameters.AddWithValue("@FECHA", bitacora.FechaBitacora);
                cmd.Parameters.AddWithValue("@NOM", bitacora.Nom);
                cmd.Parameters.AddWithValue("@DESC", bitacora.Descripcion);
                cmd.Parameters.AddWithValue("@ID", bitacora.Id);
                db.Open();
                delete = cmd.ExecuteNonQuery();
                db.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return delete;
        }
    }
}
