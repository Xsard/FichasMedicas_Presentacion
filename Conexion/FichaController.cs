using Datos;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conexion
{
    public class FichaController
    {
        public static int InsertFicha(FichaMedica ficha, string rut)
        {
            int insert;
            using SqliteConnection db = new(Conexion.GetConn());
            SqliteCommand cmd = new("INSERT INTO FICHA VALUES(@NFICHA,@GESTA,@PV,@RS,@THR,@OBS,@FEC_ACTU,@PART,@PARP,@PARM,@PARV,@RUT,@ID_MAC)", db);
            cmd.Parameters.AddWithValue("@NFICHA", ficha.NFicha);
            cmd.Parameters.AddWithValue("@GESTA", ficha.Gesta);
            cmd.Parameters.AddWithValue("@PV", ficha.Pv);
            cmd.Parameters.AddWithValue("@RS", ficha.Rs);
            cmd.Parameters.AddWithValue("@THR", ficha.Thr);
            cmd.Parameters.AddWithValue("@ID_MAC", ficha.Metodo.Id_Mac);
            cmd.Parameters.AddWithValue("@OBS", ficha.Observacion);
            cmd.Parameters.AddWithValue("@FEC_ACTU", ficha.FecUpdate);
            cmd.Parameters.AddWithValue("@RUT", rut);
            cmd.Parameters.AddWithValue("@PART", ficha.PartosT);
            cmd.Parameters.AddWithValue("@PARP", ficha.PartosP);
            cmd.Parameters.AddWithValue("@PARM", ficha.PartosM);
            cmd.Parameters.AddWithValue("@PARV", ficha.PartosV);
            try
            {
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
        public static int InsertFichaMed(PatologiasMed patologiasMed)
        {
            int insert;
            using SqliteConnection db = new(Conexion.GetConn());
            SqliteCommand cmd = new("INSERT INTO PATOLOGIAS_MED VALUES(@NFICHA,@DM,@DIS,@EPI,@HIP,@HTA,@ITU,@NIE,@JAQ,@MF,@SOP,@VAGINITIS,@ALERGIA,@RH,@NO,@MENO,@CMENO)", db);
            cmd.Parameters.AddWithValue("@NFICHA", patologiasMed.NFicha);
            cmd.Parameters.AddWithValue("@DM", patologiasMed.Dm);
            cmd.Parameters.AddWithValue("@DIS", patologiasMed.Dismenorrea);
            cmd.Parameters.AddWithValue("@EPI", patologiasMed.Epilepsia);
            cmd.Parameters.AddWithValue("@HIP", patologiasMed.Hiper);
            cmd.Parameters.AddWithValue("@HTA", patologiasMed.Hta);
            cmd.Parameters.AddWithValue("@ITU", patologiasMed.Itu);
            cmd.Parameters.AddWithValue("@NIE", patologiasMed.Nie);
            cmd.Parameters.AddWithValue("@JAQ", patologiasMed.Jaqueca);
            cmd.Parameters.AddWithValue("@MF", patologiasMed.Mfqb);
            cmd.Parameters.AddWithValue("@SOP", patologiasMed.Sop);
            cmd.Parameters.AddWithValue("@VAGINITIS", patologiasMed.Vaginitis);
            cmd.Parameters.AddWithValue("@ALERGIA", patologiasMed.Alergia);
            cmd.Parameters.AddWithValue("@RH", patologiasMed.Rh);
            cmd.Parameters.AddWithValue("@NO", patologiasMed.No);
            cmd.Parameters.AddWithValue("@MENO", patologiasMed.Menopausia);
            cmd.Parameters.AddWithValue("@CMENO", patologiasMed.CantMeno);
            try
            {
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
        public static int InsertFichaQui(PatoloagiaQui patoloagiaQui)
        {
            int insert;
            using SqliteConnection db = new(Conexion.GetConn());
            SqliteCommand cmd = new("INSERT INTO PATOLOGIAS_QUI VALUES(@NFICHA,@AP,@CA,@CE,@CA_CE,@COL,@CON,@ESTE,@SALP,@CRIO,@HAT,@HERI,@HERU,@OO,@QUI,@NO)", db);
            cmd.Parameters.AddWithValue("@NFICHA", patoloagiaQui.NFicha);
            cmd.Parameters.AddWithValue("@AP", patoloagiaQui.Apendictomia);
            cmd.Parameters.AddWithValue("@CA", patoloagiaQui.Ca);
            cmd.Parameters.AddWithValue("@CE", patoloagiaQui.Cesarea);
            cmd.Parameters.AddWithValue("@CA_CE", patoloagiaQui.Cant_cesarea);
            cmd.Parameters.AddWithValue("@COL", patoloagiaQui.Colcistectomia);
            cmd.Parameters.AddWithValue("@CON", patoloagiaQui.Conizacion);
            cmd.Parameters.AddWithValue("@ESTE", patoloagiaQui.Esterilizacion);
            cmd.Parameters.AddWithValue("@SALP", patoloagiaQui.Salpingectomia);
            cmd.Parameters.AddWithValue("@CRIO", patoloagiaQui.Crioterapia);
            cmd.Parameters.AddWithValue("@HAT", patoloagiaQui.Hat);
            cmd.Parameters.AddWithValue("@HERI", patoloagiaQui.HerInguinal);
            cmd.Parameters.AddWithValue("@HERU", patoloagiaQui.HerUmb);
            cmd.Parameters.AddWithValue("@OO", patoloagiaQui.Ooforectomia);
            cmd.Parameters.AddWithValue("@QUI", patoloagiaQui.Quistectomia);
            cmd.Parameters.AddWithValue("@NO", patoloagiaQui.No);
            try
            {
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
        public static FichaMedica SelectFicha(int numeroFicha)
        {
            FichaMedica fichaMedica = null;

            using SqliteConnection db = new(Conexion.GetConn());
            try
            {
                SqliteCommand cmd = new("SELECT * FROM FICHA FI JOIN PERSONA PE ON(FI.RUT=PE.RUT) JOIN PATOLOGIAS_MED PM ON(PM.NFICHA = FI.NFICHA) JOIN PATOLOGIAS_QUI PQ ON(PQ.NFICHA = FI.NFICHA) WHERE FI.NFICHA=@NFIC", db);
                cmd.Parameters.AddWithValue("@NFIC", numeroFicha);
                SqliteDataReader reader;
                db.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
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
                    fichaMedica = new()
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
                    
                }
                else
                {
                    return null;
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
            return fichaMedica;
        }
        public static FichaMedica SelectFicha(string rut)
        {
            FichaMedica fichaMedica = null;

            using SqliteConnection db = new(Conexion.GetConn());
            try
            {
                SqliteCommand cmd = new("SELECT * FROM FICHA FI JOIN PERSONA PE ON(FI.RUT=PE.RUT) JOIN PATOLOGIAS_MED PM ON(PM.NFICHA = FI.NFICHA) JOIN PATOLOGIAS_QUI PQ ON(PQ.NFICHA = FI.NFICHA) WHERE FI.RUT=@RUT", db);
                cmd.Parameters.AddWithValue("@RUT", rut);
                SqliteDataReader reader;
                db.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
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
                        No = reader.GetInt32(36),
                        Menopausia = reader.GetInt32(37),
                        CantMeno = reader.GetInt32(38)
                    };
                    PatoloagiaQui patoloagiaQui = new()
                    {
                        Apendictomia = reader.GetInt32(40),
                        Ca = reader.GetInt32(41),
                        Cesarea = reader.GetInt32(42),
                        Cant_cesarea = reader.GetInt32(43),
                        Colcistectomia = reader.GetInt32(44),
                        Conizacion = reader.GetInt32(45),
                        Esterilizacion = reader.GetInt32(46),
                        Salpingectomia = reader.GetInt32(47),
                        Crioterapia = reader.GetInt32(48),
                        Hat = reader.GetInt32(49),
                        HerInguinal = reader.GetInt32(50),
                        HerUmb = reader.GetInt32(51),
                        Ooforectomia = reader.GetInt32(52),
                        Quistectomia = reader.GetInt32(53),
                        No = reader.GetInt32(53)
                    };
                    MAC metodo = new()
                    {
                        Id_Mac = reader.GetInt32(12)
                    };
                    fichaMedica = new()
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

                }
                else
                {
                    return null;
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
            return fichaMedica;
        }
        public static int ActualizarFicha(FichaMedica fichaMedica, int nFichaEnUso)
        {
            int actualizar;
            using SqliteConnection db = new(Conexion.GetConn());
            SqliteCommand cmd = new("UPDATE FICHA SET NFICHA = @NFICHA, GESTA = @GESTA, PV = @PV, ID_RS = @RS, ID_TRH = @THR, ID_MAC = @ID_MAC, OBSERVACION = @OBS, FECHA_ACTUALIZACION = @FEC_ACTU, PartosTotales = @PART, PartosPrematuros = @PARP, Abortos = @PARM, PartosVivos = @PARV WHERE NFICHA = @NFICHAU", db);
            cmd.Parameters.AddWithValue("@NFICHA", fichaMedica.NFicha);
            cmd.Parameters.AddWithValue("@GESTA", fichaMedica.Gesta);
            cmd.Parameters.AddWithValue("@PV", fichaMedica.Pv);
            cmd.Parameters.AddWithValue("@RS", fichaMedica.Rs);
            cmd.Parameters.AddWithValue("@THR", fichaMedica.Thr);
            cmd.Parameters.AddWithValue("@ID_MAC", fichaMedica.Metodo.Id_Mac);
            cmd.Parameters.AddWithValue("@OBS", fichaMedica.Observacion);
            cmd.Parameters.AddWithValue("@FEC_ACTU", fichaMedica.FecUpdate);
            cmd.Parameters.AddWithValue("@PART", fichaMedica.PartosT);
            cmd.Parameters.AddWithValue("@PARP", fichaMedica.PartosP);
            cmd.Parameters.AddWithValue("@PARM", fichaMedica.PartosM);
            cmd.Parameters.AddWithValue("@PARV", fichaMedica.PartosV);
            cmd.Parameters.AddWithValue("@NFICHAU", nFichaEnUso);
            try
            {
                db.Open();
                actualizar = cmd.ExecuteNonQuery();
                db.Close();
            }
            catch (Exception)
            {
                return -1;
            }
            return actualizar;
        }
        public static int ActualizarFichaMed(PatologiasMed patologiasMed)
        {
            int actualizar;
            using SqliteConnection db = new(Conexion.GetConn());
            SqliteCommand cmd = new("UPDATE PATOLOGIAS_MED SET DM = @DM, DISMENORREA = @DIS, EPILEPSIA = @EPI, HIPER = @HIP, HTA = @HTA, ITU = @ITU, NIE = @NIE, JAQUECA = @JAQ, MFQB = @MF, SOP = @SOP, VAGINITIS = @VAGINITIS, ALERGIA = @ALERGIA, RH = @RH, NO = @NO, MENOPAUSIA = @MENO, MENO_CANT = @CMENO WHERE NFICHA = @NFICHA", db);
            cmd.Parameters.AddWithValue("@NFICHA", patologiasMed.NFicha);
            cmd.Parameters.AddWithValue("@DM", patologiasMed.Dm);
            cmd.Parameters.AddWithValue("@DIS", patologiasMed.Dismenorrea);
            cmd.Parameters.AddWithValue("@EPI", patologiasMed.Epilepsia);
            cmd.Parameters.AddWithValue("@HIP", patologiasMed.Hiper);
            cmd.Parameters.AddWithValue("@HTA", patologiasMed.Hta);
            cmd.Parameters.AddWithValue("@ITU", patologiasMed.Itu);
            cmd.Parameters.AddWithValue("@NIE", patologiasMed.Nie);
            cmd.Parameters.AddWithValue("@JAQ", patologiasMed.Jaqueca);
            cmd.Parameters.AddWithValue("@MF", patologiasMed.Mfqb);
            cmd.Parameters.AddWithValue("@SOP", patologiasMed.Sop);
            cmd.Parameters.AddWithValue("@VAGINITIS", patologiasMed.Vaginitis);
            cmd.Parameters.AddWithValue("@ALERGIA", patologiasMed.Alergia);
            cmd.Parameters.AddWithValue("@RH", patologiasMed.Rh);
            cmd.Parameters.AddWithValue("@NO", patologiasMed.No);
            cmd.Parameters.AddWithValue("@MENO", patologiasMed.Menopausia);
            cmd.Parameters.AddWithValue("@CMENO", patologiasMed.CantMeno);
            try
            {
                db.Open();
                actualizar = cmd.ExecuteNonQuery();
                db.Close();
            }
            catch (Exception)
            {
                return 0;
            }
            return actualizar;
        }
        public static int ActualizarFichaQui(PatoloagiaQui patoloagiaQui)
        {
            int actualizar;
            using SqliteConnection db = new(Conexion.GetConn());
            SqliteCommand cmd = new("UPDATE PATOLOGIAS_QUI SET APENDICTOMIA = @AP, CA = @CA, CESAREA = @CE, CANT_CESAREA = @CA_CE, COLCISTECTOMIA = @COL, CONIZACION = @CON, ESTERILIZACION = @ESTE, SALPINGECTOMIA = @SALP, CRIOTERAPIA = @CRIO, HAT= @HAT, HER_INGUINAL = @HERI, HER_UMB = @HERU, OOFORECTOMIA = @OO, QUISTECTOMIA = @QUI, NO = @NO WHERE NFICHA = @NFICHA", db);
            cmd.Parameters.AddWithValue("@NFICHA", patoloagiaQui.NFicha);
            cmd.Parameters.AddWithValue("@AP", patoloagiaQui.Apendictomia);
            cmd.Parameters.AddWithValue("@CA", patoloagiaQui.Ca);
            cmd.Parameters.AddWithValue("@CE", patoloagiaQui.Cesarea);
            cmd.Parameters.AddWithValue("@CA_CE", patoloagiaQui.Cant_cesarea);
            cmd.Parameters.AddWithValue("@COL", patoloagiaQui.Colcistectomia);
            cmd.Parameters.AddWithValue("@CON", patoloagiaQui.Conizacion);
            cmd.Parameters.AddWithValue("@ESTE", patoloagiaQui.Esterilizacion);
            cmd.Parameters.AddWithValue("@SALP", patoloagiaQui.Salpingectomia);
            cmd.Parameters.AddWithValue("@CRIO", patoloagiaQui.Crioterapia);
            cmd.Parameters.AddWithValue("@HAT", patoloagiaQui.Hat);
            cmd.Parameters.AddWithValue("@HERI", patoloagiaQui.HerInguinal);
            cmd.Parameters.AddWithValue("@HERU", patoloagiaQui.HerUmb);
            cmd.Parameters.AddWithValue("@OO", patoloagiaQui.Ooforectomia);
            cmd.Parameters.AddWithValue("@QUI", patoloagiaQui.Quistectomia);
            cmd.Parameters.AddWithValue("@NO", patoloagiaQui.No);

            try
            {
                db.Open();
                actualizar = cmd.ExecuteNonQuery();
                db.Close();
            }
            catch (Exception)
            {
                return 0;
            }
            return actualizar;
        }
        public static int Existe(int nFicha)
        {
            int existe = 0;
            using SqliteConnection db = new(Conexion.GetConn());
            try
            {
                SqliteCommand cmd = new("SELECT * FROM FICHA WHERE NFICHA=@NFICHA", db);
                cmd.Parameters.AddWithValue("@NFICHA", nFicha);
                db.Open();
                SqliteDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    existe = 1;
                }
                db.Close();
            }
            catch (Exception)
            {
                return 0;
            }
            return existe;
        }
        public static int PatMExtras(int nFicha, string nombre, int estado, int id)
        {
            int insert;
            using SqliteConnection db = new(Conexion.GetConn());
            SqliteCommand cmd = new("INSERT INTO PM_EXTRAS VALUES(@ID,@NFICHA,@NOM,@EST)", db);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@NFICHA", nFicha);
            cmd.Parameters.AddWithValue("@NOM", nombre);
            cmd.Parameters.AddWithValue("@EST", estado);
            try
            {
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
        public static int UpdateMExtras(int nFicha, string nuevoNombre, int estado, int id)
        {
            int update;
            using SqliteConnection db = new(Conexion.GetConn());
            SqliteCommand cmd = new("UPDATE PM_EXTRAS SET NOMBRE = @NOM, ESTADO = @EST WHERE ID_EXTRA =@N AND NFICHA = @NF", db);
            cmd.Parameters.AddWithValue("@NF", nFicha);
            cmd.Parameters.AddWithValue("@NOM", nuevoNombre);
            cmd.Parameters.AddWithValue("@N", id);
            cmd.Parameters.AddWithValue("@EST", estado);
            try
            {
                db.Open();
                update = cmd.ExecuteNonQuery();
                db.Close();
            }
            catch (Exception)
            {
                return 0;
            }
            return update;
        }
        public static int PatQExtras(int nFicha, string nombre, int estado, int id)
        {
            int insert;
            using SqliteConnection db = new(Conexion.GetConn());
            SqliteCommand cmd = new("INSERT INTO PQ_EXTRAS VALUES(@ID,@NFICHA,@NOM,@EST)", db);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@NFICHA", nFicha);
            cmd.Parameters.AddWithValue("@NOM", nombre);
            cmd.Parameters.AddWithValue("@EST", estado);
            try
            {
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
        public static int DeletePatMExtra(int nFicha, int id)
        {
            int delete;
            using SqliteConnection db = new(Conexion.GetConn());
            SqliteCommand cmd = new("DELETE FROM PM_EXTRAS WHERE ID_EXTRA = @ID AND NFICHA = @NFICHA ", db);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@NFICHA", nFicha);
            try
            {
                db.Open();
                delete = cmd.ExecuteNonQuery();
                db.Close();
            }
            catch (Exception)
            {
                return 0;
            }
            return delete;
        }
        public static int UpdateQExtras(int nFicha, string nuevoNombre, int estado, int id)
        {
            int update;
            using SqliteConnection db = new(Conexion.GetConn());
            SqliteCommand cmd = new("UPDATE PQ_EXTRAS SET NOMBRE = @NOM, ESTADO = @EST WHERE ID_EXTRA =@N AND NFICHA = @NF", db);
            cmd.Parameters.AddWithValue("@NF", nFicha);
            cmd.Parameters.AddWithValue("@NOM", nuevoNombre);
            cmd.Parameters.AddWithValue("@N", id);
            cmd.Parameters.AddWithValue("@EST", estado);
            try
            {
                db.Open();
                update = cmd.ExecuteNonQuery();
                db.Close();
            }
            catch (Exception)
            {
                return 0;
            }
            return update;
        }
        public static int DeletePatQExtra(int nFicha, int id)
        {
            int delete;
            using SqliteConnection db = new(Conexion.GetConn());
            SqliteCommand cmd = new("DELETE FROM PQ_EXTRAS WHERE ID_EXTRA = @ID AND NFICHA = @NFICHA ", db);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@NFICHA", nFicha);
            try
            {
                db.Open();
                delete = cmd.ExecuteNonQuery();
                db.Close();
            }
            catch (Exception)
            {
                return 0;
            }
            return delete;
        }
        public static List<PatExtras> SelectPatMExtras(int nFicha)
        {
            List<PatExtras> patExtras = new();
            using SqliteConnection db = new(Conexion.GetConn());
            try
            {
                SqliteCommand cmd = new("SELECT NOMBRE, ESTADO FROM PM_EXTRAS WHERE NFICHA = @NFICHA", db);
                cmd.Parameters.AddWithValue("@NFICHA", nFicha);
                SqliteDataReader reader;
                db.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PatExtras pat = new()
                    {
                        Nombre = reader.GetString(0),
                        Estado = reader.GetInt32(1)
                    };
                    patExtras.Add(pat);
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
            return patExtras;
        }
        public static List<PatExtras> SelectPatQExtras(int nFicha)
        {
            List<PatExtras> patExtras = new();
            using SqliteConnection db = new(Conexion.GetConn());
            try
            {
                SqliteCommand cmd = new("SELECT NOMBRE, ESTADO FROM PQ_EXTRAS WHERE NFICHA = @NFICHA", db);
                cmd.Parameters.AddWithValue("@NFICHA", nFicha);
                SqliteDataReader reader;
                db.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PatExtras pat = new()
                    {
                        Nombre = reader.GetString(0),
                        Estado = reader.GetInt32(1)
                    };
                    patExtras.Add(pat);
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
            return patExtras;
        }
    }
}
