using Datos;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conexion
{
    public class PersonaController
    {
        public static int InsertPersona(Persona persona)
        {
            int insert;
            using SqliteConnection db = new(Conexion.GetConn());
            try
            {
                SqliteCommand cmd = new("INSERT INTO PERSONA VALUES(@RUT,@PNOMBRE,@SNOMBRE,@APATERNO,@AMATERNO,@FECHA,@FONO,@DIREC,@PREVISION)", db);
                cmd.Parameters.AddWithValue("@RUT", persona.Rut);
                cmd.Parameters.AddWithValue("@PNOMBRE", persona.PNombre);
                cmd.Parameters.AddWithValue("@SNOMBRE", persona.SNombre);
                cmd.Parameters.AddWithValue("@APATERNO", persona.APaterno);
                cmd.Parameters.AddWithValue("@AMATERNO", persona.AMaterno);
                cmd.Parameters.AddWithValue("@FECHA", persona.FechaNac);
                cmd.Parameters.AddWithValue("@FONO", persona.Telofono);
                cmd.Parameters.AddWithValue("@DIREC", persona.Direccion);
                cmd.Parameters.AddWithValue("@PREVISION", persona.Prevision);
                db.Open();
                insert = cmd.ExecuteNonQuery();
                db.Close();
            }
            catch (Exception)
            {
                return 0;
            }

            return insert;
        }
        public static void DeletePersona(string rut)
        {
            using SqliteConnection db = new(Conexion.GetConn());
            try
            {
                SqliteCommand cmd = new("DELETE FROM PERSONA WHERE RUT = @Rut", db);
                cmd.Parameters.AddWithValue("@RUT", rut);
                db.Open();
                cmd.ExecuteNonQuery();
                db.Close();
            }
            catch (Exception)
            {
            }
        }
        public static bool Existe(string rut)
        {
            bool Existe;
            using SqliteConnection db = new(Conexion.GetConn());
            try
            {
                SqliteCommand cmd = new("SELECT * FROM PERSONA WHERE RUT = @Rut", db);
                cmd.Parameters.AddWithValue("@RUT", rut);
                db.Open();
                Existe = cmd.ExecuteReader().HasRows;
                db.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return Existe;
        }
        public static int ActualizarPersona(Persona persona, string rutEnUso)
        {
            int insert;
            using SqliteConnection db = new(Conexion.GetConn());
            try
            {
                SqliteCommand cmd = new("UPDATE PERSONA SET RUT = @RUT, PNOMBRE = @PNOMBRE, SNOMBRE = @SNOMBRE, APATERNO = @APATERNO, AMATERNO = @AMATERNO, FECHA_NAC = @FECHA, TELEFONO=@FONO, DIRECCION = @DIREC, PREVISION_ID = @PREVISION WHERE RUT = @RUTA", db);
                cmd.Parameters.AddWithValue("@RUT", persona.Rut);
                cmd.Parameters.AddWithValue("@PNOMBRE", persona.PNombre);
                cmd.Parameters.AddWithValue("@SNOMBRE", persona.SNombre);
                cmd.Parameters.AddWithValue("@APATERNO", persona.APaterno);
                cmd.Parameters.AddWithValue("@AMATERNO", persona.AMaterno);
                cmd.Parameters.AddWithValue("@FECHA", persona.FechaNac);
                cmd.Parameters.AddWithValue("@FONO", persona.Telofono);
                cmd.Parameters.AddWithValue("@DIREC", persona.Direccion);
                cmd.Parameters.AddWithValue("@PREVISION", persona.Prevision);
                cmd.Parameters.AddWithValue("@RUTA", rutEnUso);
                db.Open();
                insert = cmd.ExecuteNonQuery();
                db.Close();
            }
            catch (Exception)
            {
                return 0;
            }

            return insert;
        }
        public static List<FichaMedica> SelectFicha(string pNombre, string sNombre, string aPaterno, string aMaterno)
        {

            List<FichaMedica> fichaMedicas = new();

            using SqliteConnection db = new(Conexion.GetConn());
            try
            {
                SqliteCommand cmd = new("SELECT * FROM FICHA FI JOIN PERSONA PE ON(FI.RUT=PE.RUT) JOIN PATOLOGIAS_MED PM ON(PM.NFICHA = FI.NFICHA) JOIN PATOLOGIAS_QUI PQ ON(PQ.NFICHA = FI.NFICHA) WHERE PE.PNOMBRE LIKE @PNombre AND PE.SNOMBRE LIKE @SNombre AND PE.APATERNO LIKE @APaterno AND PE.AMATERNO LIKE @AMaterno", db);
                cmd.Parameters.AddWithValue("@PNombre", pNombre + "%");
                cmd.Parameters.AddWithValue("@SNombre", sNombre + "%");
                cmd.Parameters.AddWithValue("@APaterno", aPaterno + "%");
                cmd.Parameters.AddWithValue("@AMaterno", aMaterno + "%");
                SqliteDataReader reader;
                db.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Persona persona = new()
                    {
                        Rut = reader.GetString(13),
                        PNombre = reader.GetString(14),
                        SNombre = reader.GetString(15),
                        APaterno = reader.GetString(16),
                        AMaterno = reader.GetString(17),
                        Telofono = reader.GetInt32(19),
                        FechaNac = reader.GetString(18),
                        Direccion = reader.GetString(20),
                        Prevision = reader.GetInt32(21),
                    };
                    PatologiasMed patologiasMed = new()
                    {
                        Dm = reader.GetInt32(23),
                        Dismenorrea = reader.GetInt32(24),
                        Epilepsia = reader.GetInt32(25),
                        Hiper = reader.GetInt32(26),
                        Hta = reader.GetInt32(27),
                        Itu = reader.GetInt32(28),
                        Nie = reader.GetInt32(29),
                        Jaqueca = reader.GetInt32(30),
                        Mfqb = reader.GetInt32(31),
                        Sop = reader.GetInt32(32),
                        Vaginitis = reader.GetInt32(33),
                        Alergia = reader.GetInt32(34),
                        Rh = reader.GetInt32(35),
                        No = reader.GetInt32(36)
                    };
                    PatoloagiaQui patoloagiaQui = new()
                    {
                        Apendictomia = reader.GetInt32(38),
                        Ca = reader.GetInt32(39),
                        Cesarea = reader.GetInt32(40),
                        Cant_cesarea = reader.GetInt32(41),
                        Colcistectomia = reader.GetInt32(42),
                        Conizacion = reader.GetInt32(43),
                        Esterilizacion = reader.GetInt32(44),
                        Salpingectomia = reader.GetInt32(45),
                        Crioterapia = reader.GetInt32(46),
                        Hat = reader.GetInt32(47),
                        HerInguinal = reader.GetInt32(48),
                        HerUmb = reader.GetInt32(49),
                        Ooforectomia = reader.GetInt32(50),
                        Quistectomia = reader.GetInt32(51),
                        No = reader.GetInt32(52)
                    };
                    MAC metodo = new()
                    {
                        Id_Mac = reader.GetInt32(12)
                    };
                    FichaMedica fichaMedica = new()
                    {
                        NFicha = reader.GetInt32(0),
                        Gesta = reader.GetInt32(1),
                        Pv = reader.GetInt32(2),
                        Rs = reader.GetInt32(3),
                        Thr = reader.GetInt32(4),
                        Metodo = metodo,
                        Observacion = reader.GetString(5),
                        FecUpdate = reader.GetDateTime(6),
                        PartosT = reader.GetInt32(7),
                        PartosP = reader.GetInt32(8),
                        PartosM = reader.GetInt32(9),
                        PartosV = reader.GetInt32(10),
                        Persona = persona,
                        PatoloagiaQui = patoloagiaQui,
                        PatologiasMed = patologiasMed
                    };
                    fichaMedicas.Add(fichaMedica);
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                db.Close();
            }
            return fichaMedicas;
        }

    }
}
