using Microsoft.AspNet.Identity;
using System;
using System.Data;
using System.Web.UI;
using WebAppDELICIAS.App_Start;

namespace WebAppDELICIAS
{
    public partial class _Default : Page
    {
        consultas datitos = new consultas();
        conexion cn = new conexion();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                DataTable dt = new DataTable();
                dt = datitos.extrae("IdentificarUsuario", "@email", Context.User.Identity.GetUserName());
                if(dt.Rows.Count > 0)
                {
                    int idusuario = int.Parse(dt.Rows[0][0].ToString());
                    string rolusuario = dt.Rows[0][1].ToString();
                    if(rolusuario == "adm")
                    {
                        PanelData.Visible = true;
                        DataTable dtPanel = new DataTable();
                        dtPanel = datitos.extrae("ObtenerDataPanel");
                        lblClientes.Text = dtPanel.Rows[0][0].ToString() + " Clientes Registrados";
                        if (dtPanel.Rows[1][0].ToString() != "0")
                        {
                            lblPedidos.Text = "Hoy: <span class='badge' style='background-color:#5cb85c; '>" + dtPanel.Rows[1][0].ToString() + "</span> <br/>" +
                            "Próximos: " + dtPanel.Rows[2][0].ToString() + "<br/>" +
                            "No Entregados: " + dtPanel.Rows[3][0].ToString();
                        }
                        else
                        {
                            lblPedidos.Text = "Hoy: " + dtPanel.Rows[1][0].ToString() + "<br/>" +
                            "Próximos: " + dtPanel.Rows[2][0].ToString() + "<br/>" +
                            "No Entregados: " + dtPanel.Rows[3][0].ToString();
                        }
                        
                        lblMovimientos.Text = dtPanel.Rows[4][0].ToString() + " mov. sin validar";
                        lblVentas.Text =
                            "A. Central: " + dtPanel.Rows[5][0].ToString() + "<br/>" +
                            "Móvil 01: " + dtPanel.Rows[6][0].ToString() + "<br/>" +
                            "Móvil 02: " + dtPanel.Rows[7][0].ToString() + "<br/>" +
                            "Móvil 03: " + dtPanel.Rows[8][0].ToString();
                        if (dtPanel.Rows[10][0].ToString() != "0")
                        {
                            lblProductos.Text = dtPanel.Rows[9][0].ToString() + " Productos Registrados" + "<br/>" +
                            "<span class='badge' style='background-color:red; color:#ffcc66'>" + dtPanel.Rows[10][0].ToString() + "</span> Productos debajo del Stock Mínimo";
                        }
                        else
                        {
                            lblProductos.Text = dtPanel.Rows[9][0].ToString() + " Productos Registrados" + "<br/>" +
                            dtPanel.Rows[10][0].ToString() + " Productos debajo del Stock Mínimo";
                        }
                        lblDeudas.Text = "Deuda Total: S/. " + dtPanel.Rows[12][0].ToString() + "." + dtPanel.Rows[12][0].ToString() + "<br/>" +
                            "Deudores: " + dtPanel.Rows[11][0].ToString();
                    }
                    else
                        PanelData.Visible = false;
                }
                else
                {
                    PanelData.Visible = false;
                }
                
                
            }
            
        }
        //PARA VER CONEXION A INTERNET
        private bool HayInternet()
        {
            try
            {
                System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry("www.google.com");

                return true;
            }
            catch
            {
                return false;
            }
        }

        protected void btnFlujos_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/USU/Flujos.aspx");
        }

        protected void btnVentas_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/USU/Ventas.aspx");
        }

        protected void btnPedidos_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/USU/Pedidos.aspx");
        }

        protected void btnClientes_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/USU/Clientes.aspx");
        }

        protected void btnTransacciones_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/USU/Transacciones.aspx");
        }

        protected void btnInventario_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/USU/Inventario.aspx");
        }

        protected void btnProductos_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ADM/Productos.aspx");
        }

        protected void btnEmpleados_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ADM/Empleados.aspx");
        }

        protected void btnDeudas_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ADM/Deudas.aspx");
        }

        protected void btnAlmacenes_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ADM/Almacenes.aspx");
        }




        protected void linkMovimientos_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/USU/Flujos.aspx");
        }

        protected void linkVentas_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/USU/Ventas.aspx");
        }

        protected void linkPedidos_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/USU/Pedidos.aspx");
        }

        protected void linkReportes_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/USU/Transacciones.aspx");
        }

        protected void linkClientes_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/USU/Clientes.aspx");
        }

        protected void linkInventario_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/USU/Inventario.aspx");
        }

        protected void linkDeudas_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ADM/Deudas.aspx");
        }

        protected void linkProductos_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ADM/Productos.aspx");
        }

        protected void linkEmpleados_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ADM/Empleados.aspx");
        }

        protected void linkAlmacenes_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ADM/Almacenes.aspx");
        }

        protected void linkCierreTurno_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ADM/CierreTurno.aspx");
        }
    }
}
