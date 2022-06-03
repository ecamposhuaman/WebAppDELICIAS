using Microsoft.AspNet.Identity;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppDELICIAS.App_Start;

namespace WebAppDELICIAS
{
    public partial class ManttoTransacciones : System.Web.UI.Page
    {
        consultas datitos = new consultas();
        conexion cn = new conexion();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                RellenaDatosCMB();
                Session["estado"] = "nn";
                DataTable dt = new DataTable();
                dt = datitos.extrae("IdentificarUsuario", "@email", Context.User.Identity.GetUserName());
                Session["Usuario"] = dt;
            }
        }

        public void RellenaDatosCMB()
        {
            datitos.rellenacombo(cmbAlmacen, "NombreAlm", "IdAlmacen", "ListarAlmacenesCMB_Transac");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            buscarData();
        }
        protected void cmbVentaFlujo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbVentaFlujo.SelectedValue == "P")
            {
                cmbAlmacen.Enabled = false;
                txtFecha.Enabled = false;
                btnBuscar.CausesValidation = false;
            }
            else
            {
                cmbAlmacen.Enabled = true;
                txtFecha.Enabled = true;
                btnBuscar.CausesValidation = true;
            }
        }

        public void buscarData()
        {
            lblInfoTransacciones.Text = "";
            btnAnular.Visible = false;
            btnValidarF.Visible = false;
            lblInfoDetalle.Text = "";
            grdDetalleTransac.DataSource = null;
            grdDetalleTransac.DataBind();
            grdTransacciones.DataSource = null;
            grdTransacciones.DataBind();
            string idalm = cmbAlmacen.SelectedValue;
            if (cmbVentaFlujo.SelectedValue != "P")
            {
                grdTransacciones.DataSource = datitos.extrae("BuscarTransacciones", "@transac", cmbVentaFlujo.SelectedValue + idalm, "@fecha", txtFecha.Text);
                grdTransacciones.DataBind();
                grdTransacciones.Caption = cmbVentaFlujo.SelectedItem + " encontrados.";
                if (cmbVentaFlujo.SelectedValue == "F")
                    Session["estado"] = "FF";
                else
                    Session["estado"] = "VV";
            }
            else
            {
                grdTransacciones.DataSource = datitos.extrae("BuscarPedidos");
                grdTransacciones.DataBind();
                grdTransacciones.Caption = cmbVentaFlujo.SelectedItem + " registrados (aún no entregados).";
                Session["estado"] = "PP";
            }
            MostrarResumen();
            
        }

        public void MostrarResumen()
        {
            if (grdTransacciones.Rows.Count > 0)
            {
                if (cmbVentaFlujo.SelectedValue == "V")
                {
                    decimal SumaSubTotal = 0;
                    decimal SumaDescuento = 0;
                    decimal SumaBnf = 0;
                    decimal SumaTotal = 0;
                    decimal SumaEfectivo = 0;
                    decimal SumaDeuda = 0;
                    decimal entregable = 0;
                    for (int X = 0; X < grdTransacciones.Rows.Count; X++)
                    {
                        SumaSubTotal = SumaSubTotal + decimal.Parse(grdTransacciones.Rows[X].Cells[3].Text);
                        SumaBnf = SumaBnf + decimal.Parse(grdTransacciones.Rows[X].Cells[4].Text);
                        SumaDescuento = SumaDescuento + decimal.Parse(grdTransacciones.Rows[X].Cells[5].Text);
                        SumaTotal = SumaTotal + decimal.Parse(grdTransacciones.Rows[X].Cells[6].Text);
                        SumaEfectivo = SumaEfectivo + decimal.Parse(grdTransacciones.Rows[X].Cells[7].Text);
                        SumaDeuda = SumaDeuda + decimal.Parse(grdTransacciones.Rows[X].Cells[8].Text);
                        entregable = SumaEfectivo + SumaDeuda;
                        //decimal.Parse(grdTransacciones.Rows[X].Cells[6].Text, new NumberFormatInfo() { NumberDecimalSeparator = "." });
                    }
                    lblInfoTransacciones.Text = " RESUMEN " + "<br/>" +
                    " TOTAL DE VENTA: <span style='color:gold;'> S/. " + SumaTotal + "</span><br/>" +
                    " + SubTotales: S/. " + SumaSubTotal + "<br/>" +
                    " - Descuentos: S/. " + SumaDescuento + "<hr />" +
                    " TOTAL ENTREGABLE: <span style='color:gold;'> S/. " + entregable + "</span><br />" +
                    " + En Efectivo: S/. " + SumaEfectivo + "<br/>" +
                    " + Deuda Generada: S/. " + SumaDeuda + "<hr />" +
                    " VALOR EN BONIFICACIONES: <span style='color:gold;'> S/. " + SumaBnf + "</span>";
                }
            }
            else
                lblInfoTransacciones.Text = "Ningún Resultado Encontrado";
        }

        protected void grdTransacciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdTransacciones.SelectedRow.Font.Bold = true; // Color.FromName("#24cb5f");
            mostrar_datos_desdeGrid();
            btnAnular.Visible = true;
            
        }

        public void mostrar_datos_desdeGrid()
        {
            lblInfoDetalle.Text = "";
            GridViewRow row = grdTransacciones.SelectedRow;
            int idtrans = Convert.ToInt32(grdTransacciones.DataKeys[row.RowIndex].Value);
            Session["idtrans"] = idtrans;
            DataTable tabla = new DataTable();
            tabla = datitos.extrae("BuscarDetalleTransaccion", "@idTransac", idtrans.ToString(), "@flujoventa", cmbVentaFlujo.SelectedValue.ToString());
            grdDetalleTransac.DataSource = tabla;
            grdDetalleTransac.DataBind();
            if (cmbVentaFlujo.SelectedValue == "V")
            {
                grdDetalleTransac.Caption = "Detalle de Venta";
                //lblInfoDetalle.Text = "Detalle de Venta: ID: " + idtrans.ToString() + "<br/>" +
                lblInfoDetalle.Text = "ID VENTA: " + idtrans.ToString() + "<br/>" +
                    "TOTAL: S/. " + grdTransacciones.Rows[row.RowIndex].Cells[6].Text + "<br/>" +
                    "- Descuento Aplicado: S/. " + grdTransacciones.Rows[row.RowIndex].Cells[5].Text + "<br/>" +
                    "DEUDA GENERADA: S/. " + grdTransacciones.Rows[row.RowIndex].Cells[8].Text + "<br/>" +
                    "Valor de Bonificación: S/. " + grdTransacciones.Rows[row.RowIndex].Cells[4].Text + "<br/>" +
                    "Descripción de Descuento aplicado: " + grdTransacciones.Rows[row.RowIndex].Cells[9].Text;
                btnAnular.Text = "Anular";
            }
            else
            {
                if (cmbVentaFlujo.SelectedValue == "F")
                {
                    grdDetalleTransac.Caption = "Detalle de Flujo";
                    decimal SumaImporte = 0;
                    for (int X = 0; X < grdDetalleTransac.Rows.Count; X++)
                    {
                        SumaImporte = SumaImporte + decimal.Parse(grdDetalleTransac.Rows[X].Cells[4].Text);

                    }
                    lblInfoDetalle.Text = "ID FLUJO: " + idtrans.ToString() + "<br/>" +
                    " Total de Importe: S/. " + SumaImporte;
                    if (Session["estado"].ToString() == "FF")
                    {
                        if (grdTransacciones.Rows[row.RowIndex].Cells[3].Text == "sin validar")
                            btnValidarF.Visible = true;
                        else
                            btnValidarF.Visible = false;
                    }
                    btnAnular.Text = "Anular";
                }
                else
                {
                    grdDetalleTransac.Caption = "Detalle de Pedido";
                    lblInfoDetalle.Text = "ID PEDIDO: " + idtrans.ToString() + "<br/>" +
                    " Cliente: " + grdTransacciones.Rows[row.RowIndex].Cells[1].Text + "<br/>" +
                    " Fecha de Entrega: " + grdTransacciones.Rows[row.RowIndex].Cells[2].Text;
                    btnAnular.Text = "Eliminar";
                }
               
            }
            
        }

        protected override void Render(HtmlTextWriter writer)
        {
            foreach (GridViewRow r in grdTransacciones.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    r.Attributes["onmouseover"] = "this.style.cursor='Pointer';"; //this.style.border = '3px solid white'; this.style.background = '#A9BCF5'; this.style.fontSize = '14px'; this.style.textDecoration ='underline';this.style.border = '2px solid black';
                    //r.Attributes["onmouseout"] = "this.style.backgroundColor='';";
                    r.ToolTip = "Click para mostrar detalles";
                    r.Attributes["onclick"] = this.Page.ClientScript.GetPostBackClientHyperlink(this.grdTransacciones, "Select$" + r.RowIndex, true);
                    //Page.ClientScript.RegisterForEventValidation(r.UniqueID, "Select$" + r.RowIndex);
                }
            }
            base.Render(writer);
        }

        protected void btnAnular_Click(object sender, EventArgs e)
        {
            GridViewRow rowAnul = grdTransacciones.SelectedRow;
            int idanul = Convert.ToInt32(grdTransacciones.DataKeys[rowAnul.RowIndex].Value);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn.cadena;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AnularTransaccion";
            cmd.Parameters.Add("@idtrans", SqlDbType.Int).Value = idanul;
            cmd.Parameters.Add("@tipotrans", SqlDbType.VarChar).Value = cmbVentaFlujo.SelectedValue.ToString(); 
            cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Now;

            cn.conectar();
            cmd.ExecuteNonQuery();
            cn.cerrar();
            buscarData();
        }

        protected void grdTransacciones_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //e.Row.Cells[4].Visible = false;
            //if(grdTransacciones.Columns.Count > 7)
            // e.Row.Cells[9].Visible = false;
        }

        protected void btnValidarF_Click(object sender, EventArgs e)
        {
            DataTable dtusu = (DataTable)Session["Usuario"];
            int idusuario = int.Parse(dtusu.Rows[0][0].ToString());
            GridViewRow rowAnul = grdTransacciones.SelectedRow;
            int idflujo = Convert.ToInt32(grdTransacciones.DataKeys[rowAnul.RowIndex].Value);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn.cadena;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ValidarFlujo";
            
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@idflujo", SqlDbType.Int).Value = idflujo;
            cmd.Parameters.Add("@idvalidador", SqlDbType.Int).Value = idusuario;

            cn.conectar();
            cmd.ExecuteNonQuery();
            cn.cerrar();
        }
    }
}