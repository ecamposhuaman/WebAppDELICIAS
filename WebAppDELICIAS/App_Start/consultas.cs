using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace WebAppDELICIAS.App_Start
{
    public class consultas
    {
        conexion cn = new conexion();

        public DataTable extrae(string nombresp)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.Connection = cn.cadena;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = nombresp;
            cn.conectar();
            da.SelectCommand = cmd;
            da.Fill(dt);
            cn.cerrar();
            return dt;
        }

        public DataTable extrae(string nombresp, string nomparam, string valorparam)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.Connection = cn.cadena;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(nomparam, SqlDbType.VarChar).Value = valorparam;
            cmd.CommandText = nombresp;
            cn.conectar();
            da.SelectCommand = cmd;
            da.Fill(dt);
            cn.cerrar();
            return dt;
        }

        public DataTable extrae(string nombresp, string nomparam, DateTime valorparam)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.Connection = cn.cadena;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(nomparam, SqlDbType.VarChar).Value = valorparam;
            cmd.CommandText = nombresp;
            cn.conectar();
            da.SelectCommand = cmd;
            da.Fill(dt);
            cn.cerrar();
            return dt;
        }

        public DataTable extrae(string nombresp, string nomparam, int valorparam)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.Connection = cn.cadena;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(nomparam, SqlDbType.Int).Value = valorparam;
            cmd.CommandText = nombresp;
            cn.conectar();
            da.SelectCommand = cmd;
            da.Fill(dt);
            cn.cerrar();
            return dt;
        }

        public DataTable extrae(string nombresp, string nomparam, string valorparam, string fechahoy)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.Connection = cn.cadena;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(nomparam, SqlDbType.Int).Value = valorparam;
            cmd.Parameters.Add(fechahoy, SqlDbType.DateTime).Value = DateTime.Now;
            cmd.CommandText = nombresp;
            cn.conectar();
            da.SelectCommand = cmd;
            da.Fill(dt);
            cn.cerrar();
            return dt;
        }

        public DataTable extrae(string nombresp, string nomparam, int valorparam, string fechahoy)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.Connection = cn.cadena;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(nomparam, SqlDbType.Int).Value = valorparam;
            cmd.Parameters.Add(fechahoy, SqlDbType.DateTime).Value = DateTime.Now;
            cmd.CommandText = nombresp;
            cn.conectar();
            da.SelectCommand = cmd;
            da.Fill(dt);
            cn.cerrar();
            return dt;
        }

        public DataTable extrae(string nombresp, string nomparam, int valorparam, string nomparam2, DateTime fecha)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.Connection = cn.cadena;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(nomparam, SqlDbType.Int).Value = valorparam;
            cmd.Parameters.Add(nomparam2, SqlDbType.DateTime).Value = fecha;
            cmd.CommandText = nombresp;
            cn.conectar();
            da.SelectCommand = cmd;
            da.Fill(dt);
            cn.cerrar();
            return dt;
        }

        public DataTable extrae(string nombresp, string nomparam1, string valorparam1,
                                   string nomparam2, string valorparam2)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.Connection = cn.cadena;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(nomparam1, SqlDbType.VarChar).Value = valorparam1;
            cmd.Parameters.Add(nomparam2, SqlDbType.VarChar).Value = valorparam2;
            cmd.CommandText = nombresp;
            cn.conectar();
            da.SelectCommand = cmd;
            da.Fill(dt);
            cn.cerrar();
            return dt;
        }

        public DataTable extrae(string nombresp, string nomparam1, string valorparam1,
                                   string nomparam2, string valorparam2, string fechahoy)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.Connection = cn.cadena;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(nomparam1, SqlDbType.VarChar).Value = valorparam1;
            cmd.Parameters.Add(nomparam2, SqlDbType.VarChar).Value = valorparam2;
            cmd.Parameters.Add(fechahoy, SqlDbType.DateTime).Value = DateTime.Now;
            cmd.CommandText = nombresp;
            cn.conectar();
            da.SelectCommand = cmd;
            da.Fill(dt);
            cn.cerrar();
            return dt;
        }


        public void rellenacombo(DropDownList cmb, string campover, string campovalor, string nombresp)
        {
            cmb.DataSource = extrae(nombresp);
            cmb.DataValueField = campovalor;
            cmb.DataTextField = campover;
            cmb.DataBind();
        }

        public void rellenacomboXparam(DropDownList cmb, string campover, string campovalor, string nombresp, string nombreparam, int valorparam)
        {
            cmb.DataSource = extrae(nombresp, nombreparam, valorparam);
            cmb.DataValueField = campovalor;
            cmb.DataTextField = campover;
            cmb.DataBind();
        }
       

        public void cargarempleados(DropDownList cmb, string campover, string campovalor, string nombresp)
        {
            cmb.DataSource = extrae(nombresp);
            cmb.DataValueField = campovalor;
            cmb.DataTextField = campover;
            cmb.DataBind();
        }
    }
}