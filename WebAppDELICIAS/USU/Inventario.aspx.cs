using System;
using WebAppDELICIAS.App_Start;

namespace WebAppDELICIAS.ADM
{
    public partial class Inventario : System.Web.UI.Page
    {
        consultas datitos = new consultas();
        conexion cn = new conexion();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                RellenaDatosCMB();
            }
        }

        public void RellenaDatosCMB()
        {
            datitos.rellenacombo(cmbAlmacen, "NombreAlm", "IdAlmacen", "ListarAlmacenesCMB_Inventariado");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            buscarData();
        }

        public void buscarData()
        {
            grdResultados.DataSource = null;
            grdResultados.DataBind();
            int idalm = int.Parse(cmbAlmacen.SelectedValue);
            grdResultados.DataSource = datitos.extrae("InventariarPorAlmacen", "@idalmacen", idalm);
            grdResultados.DataBind();
            grdResultados.Caption = "INVENTARIO GENERAL DE PRODUCTOS";
            if(cmbAlmacen.SelectedValue != "100")
            {
                decimal SumaImporte = 0;
                for (int X = 0; X < grdResultados.Rows.Count; X++)
                {
                    SumaImporte = SumaImporte + decimal.Parse(grdResultados.Rows[X].Cells[4].Text);
                    grdResultados.Caption = "INVENTARIO EN: " + cmbAlmacen.SelectedItem + "<br/>" +
                        "Valor en Productos Inventariados: S/. " + SumaImporte;
                }
            }
        }

        protected void grdResultados_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}