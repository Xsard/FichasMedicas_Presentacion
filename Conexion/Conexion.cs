using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.IO;

namespace Conexion
{
    public class Conexion
    {
        public static string GetConn()
        {            
            string dbpath = "Data Source=FichasDB.db";
            return dbpath;
        }
        public static SqliteConnection Connection()
        {
            using SqliteConnection db = new(GetConn());
            return db;
        }
    }
}
