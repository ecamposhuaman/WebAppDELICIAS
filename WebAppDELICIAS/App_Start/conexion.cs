using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebAppDELICIAS.App_Start
{
    public class conexion
    {
        private static readonly string ConnString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public SqlConnection cadena = new SqlConnection(ConnString);
        //public SqlConnection cadena = new SqlConnection("Persist Security Info=False;Data Source=SONY;Initial Catalog=BDRACIRI;User ID = User01;Password = User0107;");
        public void conectar()
        {
            cadena.Open();
        }
        public void cerrar()
        {
            cadena.Close();
        }
    }
}