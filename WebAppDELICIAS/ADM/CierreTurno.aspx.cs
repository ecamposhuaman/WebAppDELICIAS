using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppDELICIAS.App_Start;

namespace WebAppDELICIAS
{
    public partial class CierreTurno : System.Web.UI.Page
    {
        consultas datitos = new consultas();
        conexion cn = new conexion();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                DataTable Tabla = new DataTable();
                //Tabla.Columns.Add("Lote");
                Tabla.Columns.Add("Descripción");
                Tabla.Columns.Add("Monto");
                grdGastos.DataSource = Tabla;
                grdGastos.DataBind();
                Session["Gastos"] = Tabla;

                DataTable dt = new DataTable();
                dt = datitos.extrae("IdentificarUsuario", "@email", Context.User.Identity.GetUserName());
                Session["Usuario"] = dt;
                int idusuario = int.Parse(dt.Rows[0][0].ToString());
                datitos.rellenacomboXparam(cmbAlmacen, "NombreAlm", "IdAlmacen", "ListarAlmacen_xIDvendedor", "@idvendedor", idusuario);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            LimpiarDATA();
            int idalmacen = int.Parse(cmbAlmacen.SelectedValue);
            DateTime Fecha = DateTime.Parse(txtFecha.Text);
            DataTable dtCT = new DataTable();
            dtCT = datitos.extrae("ObtenerCierreTurno", "@idalmacen", idalmacen, "@fecha", Fecha);
            Session["CierreTurno"] = dtCT;
            
            decimal VTENT = decimal.Parse(dtCT.Rows[0][1].ToString());
            decimal VTSAL = decimal.Parse(dtCT.Rows[0][2].ToString());
            decimal STKACT = decimal.Parse(dtCT.Rows[0][3].ToString());
            decimal VDeudas = decimal.Parse(dtCT.Rows[0][4].ToString());
            decimal VDescuentos = decimal.Parse(dtCT.Rows[0][5].ToString());
            decimal VBonif = decimal.Parse(dtCT.Rows[0][6].ToString());
            lblVTENT.Text = "S/. " + VTENT.ToString();
            lblVTSAL.Text = "S/. " + VTSAL.ToString();
            lblSTKACT.Text = "S/. " + STKACT.ToString();
            lblVDeudas.Text = "S/. " + VDeudas.ToString();
            lblVDescuentos.Text = "S/. " + VDescuentos.ToString();
            lblVBonif.Text = "S/. " + VBonif.ToString();
            
            if (dtCT.Rows[0][0].ToString() == "Existente")
            {
                lblInforme.Text = "Valores obtenidos de un Cierre de Turno [" + dtCT.Rows[0][7].ToString() + "] Guardado con anterioridad por " + dtCT.Rows[0][8].ToString() + " de Almacén " + dtCT.Rows[0][9].ToString() + " con Fecha " + DateTime.Parse(dtCT.Rows[0][10].ToString()).ToShortDateString();
                
                lblTotalGastos.Text = "S/. " + dtCT.Rows[0][11].ToString();
                lblTotalEfectivo.Text = "S/. " + dtCT.Rows[0][12].ToString();
                
                txt200.Text = dtCT.Rows[0][13].ToString();
                txt100.Text = dtCT.Rows[0][14].ToString();
                txt50.Text = dtCT.Rows[0][15].ToString();
                txt20.Text = dtCT.Rows[0][16].ToString();
                txt10.Text = dtCT.Rows[0][17].ToString();
                txt5.Text = dtCT.Rows[0][18].ToString();
                txt2.Text = dtCT.Rows[0][19].ToString();
                txt1.Text = dtCT.Rows[0][20].ToString();
                txt05.Text = dtCT.Rows[0][21].ToString();
                txt02.Text = dtCT.Rows[0][22].ToString();
                txt01.Text = dtCT.Rows[0][23].ToString();

                int idCierreTrn = int.Parse(dtCT.Rows[0][7].ToString());
                DataTable dtgastos = new DataTable();
                dtgastos = datitos.extrae("ObtenerGastos", "@idCierreTrn", idCierreTrn);
                Session["Gastos"] = dtgastos;
                grdGastos.DataSource = dtgastos;
                grdGastos.DataBind();
            }
            else
            {
                lblInforme.Text = "Nuevo Cierre de Turno. Valores Obtenidos de las operaciones realizadas durante la fecha seleccionada";
            }
            decimal MovProd = VTENT - (VTSAL + STKACT);
            decimal Venta = VDeudas + VDescuentos + VBonif + SumaGastos() + TotalEfectivo();
            lblMOVPROD.Text = "S/. " + MovProd.ToString();
            lblVENTAS.Text = "S/. " + Venta.ToString();
            lblSaldo.Text = "SALDO: S/. " + (MovProd - Venta).ToString();
            ColorearLabel(STKACT, (MovProd - Venta));
            btnGuardar.Enabled = true;
            btnAgregar.Enabled = true;
            btnVerEntradas.Enabled = true;
            btnVerSalidas.Enabled = true;
            //btnGuardar.Attributes["onclick"] = string.Format("if(!Confirmacion({0},{1})) return false;", STKACT.ToString().Replace(",", "."), (VDeudas + VDescuentos + VBonif + SumaGastos() + TotalEfectivo()).ToString().Replace(",", "."));
        }
        
        public void ColorearLabel(decimal STK, decimal saldo)
        {
            if (STK != 0)
                lblSTKACT.ForeColor = Color.Red;
            else
                lblSTKACT.ForeColor = Color.LightGreen;

            if (saldo != 0) {
                if (saldo > 0)
                    lblSaldo.ForeColor = Color.Yellow;
                else
                    lblSaldo.ForeColor = Color.Red;
            }
            else
                lblSaldo.ForeColor = Color.LightGreen;
        }

        public void LimpiarDATA()
        {
            txt200.Text = "0";
            txt100.Text = "0";
            txt50.Text = "0";
            txt20.Text = "0";
            txt10.Text = "0";
            txt5.Text = "0";
            txt2.Text = "0";
            txt1.Text = "0";
            txt05.Text = "0";
            txt02.Text = "0";
            txt01.Text = "0";
            txtNombreGasto.Text = "";
            txtMontoGasto.Text = "";
            lblTotalGastos.Text = "S/. 0.00";
            lblTotalEfectivo.Text = "S/. 0.00";
            grdGastos.DataSource = null;
            grdGastos.DataBind();
            lblSaldo.Text = "";
            grdMovimientos.DataSource = null;
            grdMovimientos.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            DataTable dtCierre = (DataTable)Session["CierreTurno"];
            DataTable dttus = (DataTable)Session["Usuario"];
            int idusuario = int.Parse(dttus.Rows[0][0].ToString());

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn.cadena;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RegistrarCierreTurno";
            cmd.Parameters.Add("@idpersona", SqlDbType.Int).Value = idusuario; 
            cmd.Parameters.Add("@idalmacen", SqlDbType.Int).Value = int.Parse(cmbAlmacen.SelectedValue);
            cmd.Parameters.Add("@fechareg", SqlDbType.Date).Value = DateTime.Parse(txtFecha.Text);
            cmd.Parameters.Add("@valent", SqlDbType.Decimal).Value = decimal.Parse(dtCierre.Rows[0][1].ToString());
            cmd.Parameters.Add("@valsal", SqlDbType.Decimal).Value = decimal.Parse(dtCierre.Rows[0][2].ToString());
            cmd.Parameters.Add("@valstk", SqlDbType.Decimal).Value = decimal.Parse(dtCierre.Rows[0][3].ToString());
            cmd.Parameters.Add("@valdeu", SqlDbType.Decimal).Value = decimal.Parse(dtCierre.Rows[0][4].ToString());
            cmd.Parameters.Add("@valdsc", SqlDbType.Decimal).Value = decimal.Parse(dtCierre.Rows[0][5].ToString());
            cmd.Parameters.Add("@valbon", SqlDbType.Decimal).Value = decimal.Parse(dtCierre.Rows[0][6].ToString());
            cmd.Parameters.Add("@totgas", SqlDbType.Decimal).Value = SumaGastos();
            cmd.Parameters.Add("@totefe", SqlDbType.Decimal).Value = TotalEfectivo();
            cmd.Parameters.Add("@b200", SqlDbType.Int).Value = int.Parse(txt200.Text);
            cmd.Parameters.Add("@b100", SqlDbType.Int).Value = int.Parse(txt100.Text);
            cmd.Parameters.Add("@b50", SqlDbType.Int).Value = int.Parse(txt50.Text);
            cmd.Parameters.Add("@b20", SqlDbType.Int).Value = int.Parse(txt20.Text);
            cmd.Parameters.Add("@b10", SqlDbType.Int).Value = int.Parse(txt10.Text);
            cmd.Parameters.Add("@m5", SqlDbType.Int).Value = int.Parse(txt5.Text);
            cmd.Parameters.Add("@m2", SqlDbType.Int).Value = int.Parse(txt2.Text);
            cmd.Parameters.Add("@m1", SqlDbType.Int).Value = int.Parse(txt1.Text);
            cmd.Parameters.Add("@m05", SqlDbType.Int).Value = int.Parse(txt05.Text);
            cmd.Parameters.Add("@m02", SqlDbType.Int).Value = int.Parse(txt02.Text);
            cmd.Parameters.Add("@m01", SqlDbType.Int).Value = int.Parse(txt01.Text);
            //cn.conectar();
            //cmd.ExecuteNonQuery();
            //cn.cerrar();
            cn.conectar();
            DataTable dtIdCT = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter();
            da1.SelectCommand = cmd;
            da1.Fill(dtIdCT);
            cn.cerrar();
            string msg = dtIdCT.Rows[0][0].ToString();
            int idCierreT = int.Parse(dtIdCT.Rows[0][1].ToString());

            DataTable dtG = (DataTable)Session["Gastos"];
            // Enviando Datos desde GRIDVIEWGASTOS
            SqlCommand cmdg = new SqlCommand();
            cmdg.Connection = cn.cadena;
            cmdg.CommandType = CommandType.StoredProcedure;
            cmdg.CommandText = "RegistrarGastos";
            foreach (GridViewRow row in grdGastos.Rows)
            {
                cmdg.Parameters.Clear();
                cmdg.Parameters.Add("@idct", SqlDbType.Int).Value = idCierreT;    
                cmdg.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = row.Cells[2].Text;
                cmdg.Parameters.Add("@val", SqlDbType.Decimal).Value = decimal.Parse(row.Cells[3].Text);
                cn.conectar();
                cmdg.ExecuteNonQuery();
                cn.cerrar();
            }
            if(msg == "NCT")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessAlert", "alert ('✓ Se Guardó Nuevo Cierre de Turno de \\n\\n " + cmbAlmacen.SelectedItem + " con fecha " + txtFecha.Text + ".');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessAlert", "alert ('✓ Se Actualizó Cierre de Turno Existente de \\n\\n " + cmbAlmacen.SelectedItem + " con fecha " + txtFecha.Text + ".');", true);
            txtNombreGasto.Text = "";
            txtMontoGasto.Text = "";
            lblInforme.Text = "";
        }

        protected void txt200_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt200.Text))
                txt200.Text = "0";
            sumarEfectivo();
        }
        protected void txt100_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt100.Text))
                txt100.Text = "0";
            sumarEfectivo();
        }
        protected void txt50_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt50.Text))
                txt50.Text = "0";
            sumarEfectivo();
        }
        protected void txt20_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt20.Text))
                txt20.Text = "0";
            sumarEfectivo();
        }
        protected void txt10_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt10.Text))
                txt10.Text = "0";
            sumarEfectivo();
        }
        protected void txt5_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt5.Text))
                txt5.Text = "0";
            sumarEfectivo();
        }
        protected void txt2_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt2.Text))
                txt2.Text = "0";
            sumarEfectivo();
        }
        protected void txt1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt1.Text))
                txt1.Text = "0";
            sumarEfectivo();
        }
        protected void txt05_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt05.Text))
                txt05.Text = "0";
            sumarEfectivo();
        }
        protected void txt02_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt02.Text))
                txt02.Text = "0";
            sumarEfectivo();
        }
        protected void txt01_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt01.Text))
                txt01.Text = "0";
            sumarEfectivo();
        }

        public void sumarEfectivo()
        {
            lblTotalEfectivo.Text = "S/. " + string.Format("{0:0.00}", TotalEfectivo().ToString());
            sumarEfectivoGasto();
        }

        public decimal TotalEfectivo()
        {
            decimal num200 = int.Parse(txt200.Text) * 200;
            decimal num100 = int.Parse(txt100.Text) * 100;
            decimal num50 = int.Parse(txt50.Text) * 50;
            decimal num20 = int.Parse(txt20.Text) * 20;
            decimal num10 = int.Parse(txt10.Text) * 10;
            decimal num5 = int.Parse(txt5.Text) * 5;
            decimal num2 = int.Parse(txt2.Text) * 2;
            decimal num1 = int.Parse(txt1.Text) * 1;
            decimal num05 = decimal.Parse(string.Format("{0:0.00}", decimal.Parse(txt05.Text, new NumberFormatInfo() { NumberDecimalSeparator = "." }) / 2));
            decimal num02 = decimal.Parse(string.Format("{0:0.00}", decimal.Parse(txt02.Text, new NumberFormatInfo() { NumberDecimalSeparator = "." }) / 5));
            decimal num01 = decimal.Parse(string.Format("{0:0.00}", decimal.Parse(txt01.Text, new NumberFormatInfo() { NumberDecimalSeparator = "." }) / 10));


            decimal TotalEfectivo1 =
                num200 +
                num100 +
                num50 +
                num20 +
                num10 +
                num5 +
                num2 +
                num1 +
                num05 +
                num02 +
                num01
                ;
            return TotalEfectivo1;
        }

        public void sumarEfectivoGasto()
        {
            DataTable tbl1 = new DataTable();
            tbl1 = (DataTable)Session["cierreturno"];
            decimal VTENT = decimal.Parse(tbl1.Rows[0][1].ToString());
            decimal VTSAL = decimal.Parse(tbl1.Rows[0][2].ToString());
            decimal STKACT = decimal.Parse(tbl1.Rows[0][3].ToString());
            decimal VDeudas = decimal.Parse(tbl1.Rows[0][4].ToString());
            decimal VDescuentos = decimal.Parse(tbl1.Rows[0][5].ToString());
            decimal VBonif = decimal.Parse(tbl1.Rows[0][6].ToString());
            
            decimal MovProd = VTENT - (VTSAL + STKACT);
            decimal Venta = VDeudas + VDescuentos + VBonif + SumaGastos() + TotalEfectivo();
            lblMOVPROD.Text = "S/. " + MovProd.ToString();
            lblVENTAS.Text = "S/. " + Venta.ToString();
            lblSaldo.Text = "SALDO: S/. " + (MovProd - Venta).ToString();
            ColorearLabel(STKACT, (MovProd - Venta));
        }
        
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            DataTable Tabla = new DataTable();
            Tabla = (DataTable)Session["Gastos"];
            Tabla.Rows.Add(txtNombreGasto.Text, string.Format("{0:0.00}", decimal.Parse(txtMontoGasto.Text, new NumberFormatInfo() { NumberDecimalSeparator = "." })));
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessAlert", "alert ('" + string.Format("{0:0.00}", 2.3) + "');", true);
            grdGastos.DataSource = Tabla;
            grdGastos.DataBind();
            Session["Gastos"] = Tabla;

            lblTotalGastos.Text = "S/. " + SumaGastos();

            sumarEfectivoGasto();
        }

        public decimal SumaGastos()
        {
            decimal Suma = 0;
            for (int X = 0; X < grdGastos.Rows.Count; X++)
            {
                Suma = Suma + decimal.Parse(grdGastos.Rows[X].Cells[3].Text);
            }
            return Suma;
        }

        protected void grdGastos_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grdGastos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)Session["Gastos"];
            dt.Rows.RemoveAt(e.RowIndex);
            //Guardar nuevos valores
            Session["Gastos"] = dt;
            grdGastos.DataSource = dt;
            grdGastos.DataBind();
            decimal dato = SumaGastos();
            if (dato == 0)
                lblTotalGastos.Text = "S/. " + "0,00";
            else
                lblTotalGastos.Text = "S/. " + dato;

            sumarEfectivoGasto();
        }

        protected void cmbAlmacen_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnGuardar.Enabled = false;
            btnAgregar.Enabled = false;
            btnVerEntradas.Enabled = false;
            btnVerSalidas.Enabled = false;
            grdMovimientos.DataSource = null;
            grdMovimientos.DataBind();
        }

        protected void txtFecha_TextChanged(object sender, EventArgs e)
        {
            btnGuardar.Enabled = false;
            btnAgregar.Enabled = false;
            btnVerEntradas.Enabled = false;
            btnVerSalidas.Enabled = false;
            grdMovimientos.DataSource = null;
            grdMovimientos.DataBind();
        }

        protected void btnVerEntradas_Click(object sender, EventArgs e)
        {
            grdMovimientos.DataSource = null;
            grdMovimientos.DataBind();
            grdMovimientos.DataSource = datitos.extrae("VerEntradas", "@idalmacen", cmbAlmacen.SelectedValue, "@fecha", txtFecha.Text);
            grdMovimientos.DataBind();
        }

        protected void btnVerSalidas_Click(object sender, EventArgs e)
        {
            grdMovimientos.DataSource = null;
            grdMovimientos.DataBind();
            grdMovimientos.DataSource = datitos.extrae("VerSalidas", "@idalmacen", cmbAlmacen.SelectedValue, "@fecha", txtFecha.Text);
            grdMovimientos.DataBind();
        }
    }
}