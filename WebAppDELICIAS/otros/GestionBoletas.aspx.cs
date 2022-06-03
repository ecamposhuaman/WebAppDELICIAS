using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using WebAppDELICIAS.App_Start;

namespace WebAppDELICIAS
{
    public partial class GestionBoletas : System.Web.UI.Page
    {
        consultas datitos = new consultas();
        conexion cn = new conexion();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            DataTable dtusu = new DataTable();
            dtusu = datitos.extrae("ObtenerIdPersona", "@email", Context.User.Identity.GetUserName());
            string idusuario = dtusu.Rows[0][0].ToString();

            grdBoletas.DataSource = datitos.extrae("ListarBoletas", "@idpersona", Convert.ToInt32(idusuario));
            grdBoletas.DataBind();
            grdBoletas.Visible = true;

        }
        

        protected void grdBoletas_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grdBoletas.SelectedRow;
            int fila = Convert.ToInt32(grdBoletas.DataKeys[row.RowIndex].Value);
            string aaa = row.Cells[1].Text;

            Response.ContentType = "application / pdf";
            Response.AppendHeader("Content-Disposition", "adjunto; filename = " + aaa + ".pdf");
            Response.TransmitFile(Server.MapPath("~/Boletas/" + aaa + ".pdf"));
            Response.End();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            foreach (GridViewRow r in grdBoletas.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    r.Attributes["onmouseover"] = "this.style.cursor='Pointer';"; //this.style.border = '3px solid white'; this.style.background = '#A9BCF5'; this.style.fontSize = '14px'; this.style.textDecoration ='underline';this.style.border = '2px solid black';
                    //r.Attributes["onmouseout"] = "this.style.backgroundColor='';";
                    r.ToolTip = "Click para Descargar Boleta";
                    r.Attributes["onclick"] = this.Page.ClientScript.GetPostBackClientHyperlink(this.grdBoletas, "Select$" + r.RowIndex, true);
                    //Page.ClientScript.RegisterForEventValidation(r.UniqueID, "Select$" + r.RowIndex);
                }
            }
            base.Render(writer);
        }

        protected void btnCargarBoletas_Click(object sender, EventArgs e)
        {
            Response.Redirect("CargarBoletas.aspx");
        }
    }
}