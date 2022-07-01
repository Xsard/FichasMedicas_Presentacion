using Datos;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conexion
{
    public class UsuarioController
    {
        public static int CrearUsuario(string user, string psw)
        {
            int insert;
            using SqliteConnection db = new(Conexion.GetConn());
            try
            {
                byte[] pass = GenericClass1.EncryptStringToBytes(psw, key, IV);
                SqliteCommand cmd = new("INSERT INTO USUARIO VALUES(@P,@U)", db);
                cmd.Parameters.AddWithValue("@P", user);
                cmd.Parameters.AddWithValue("@U", pass);
                db.Open();
                insert = cmd.ExecuteNonQuery();
                db.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Close();
            }
            return insert;
        }
        public static int ValidarUsusario(string user, string psw)
        {
            int validado = 0;
            using SqliteConnection db = new(Conexion.GetConn());
            try
            {
                byte[] pass = GenericClass1.EncryptStringToBytes(psw, key, IV);
                SqliteCommand cmd = new("SELECT * FROM USUARIO WHERE USER = @PSW AND PSSW = @P", db);
                SqliteDataReader reader;
                cmd.Parameters.AddWithValue("@PSW", user);
                cmd.Parameters.AddWithValue("@P", pass);
                db.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    validado = 1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                db.Close();
            }
            return validado;
        }
        public static int ModificarUsusario(string user, string psw, string userN, string pswN)
        {
            int modificado = 0;
            using SqliteConnection db = new(Conexion.GetConn());
            try
            {
                byte[] pass = GenericClass1.EncryptStringToBytes(psw, key, IV);
                byte[] passN = GenericClass1.EncryptStringToBytes(pswN, key, IV);
                SqliteCommand cmd = new("UPDATE USUARIO SET USER = @USRN, PSSW = @PASS WHERE USER = @PSW AND PSSW = @FD", db);
                cmd.Parameters.AddWithValue("@USRN", userN);
                cmd.Parameters.AddWithValue("@PASS", passN);
                cmd.Parameters.AddWithValue("@PSW", user);
                cmd.Parameters.AddWithValue("@FD", pass);
                db.Open();
                modificado = cmd.ExecuteNonQuery();
                db.Close();

            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                db.Close();
            }
            return modificado;
        }
    }

}
