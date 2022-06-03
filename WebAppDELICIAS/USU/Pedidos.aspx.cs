using Microsoft.AspNet.Identity;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppDELICIAS.App_Start;

namespace WebAppDELICIAS.ADM
{
    public partial class Pedidos : System.Web.UI.Page
    {
        consultas datitos = new consultas();
        conexion cn = new conexion();

        protected void Page_Load(object sender, EventArgs e)
        {

            //btnRegistrarVenta.OnClientClick = string.Format("if(!ConfirmacionV({0},{1},{2})) return false;", 1, 2, 3);
            if (IsPostBack == false)
            {
                RellenaDatosCMBProductos();

                DataTable Tabla = new DataTable();
                //Tabla.Columns.Add("Lote");
                Tabla.Columns.Add("Cant.");
                Tabla.Columns.Add("Producto");
                Tabla.Columns.Add("Concpt");
                Tabla.Columns.Add("IdProd");
                Tabla.Columns.Add("P. Unit.");
                Tabla.Columns.Add("Importe");
                grdProductos.DataSource = Tabla;
                grdProductos.DataBind();
                Session["DatosPed"] = Tabla;

                //Obtener Usuario
                DataTable dt = new DataTable();
                dt = datitos.extrae("IdentificarUsuario", "@email", Context.User.Identity.GetUserName());
                Session["Usuario"] = dt;

            }
        }

        public void RellenaDatosCMBProductos()
        {
            datitos.rellenacombo(cmbCliente, "ApelNombres", "IdPersona", "ListarClientesCMB");

            DataTable dtProdPrec = datitos.extrae("ListarProdPrecioCMB");
            cmbProducto.DataSource = dtProdPrec;
            cmbProducto.DataValueField = "IdProducto";
            cmbProducto.DataTextField = "NombreProd";
            cmbProducto.DataBind();
            Session["Precios"] = dtProdPrec;
        }

        public int SumarCantProd()
        {
            int CantProductos = 0;
            for (int X = 0; X < grdProductos.Rows.Count; X++)
            {
                CantProductos = CantProductos + int.Parse(grdProductos.Rows[X].Cells[2].Text);
            }
            return CantProductos;
        }

        protected void btnAgregarP_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtCantidad.Text) > 0)
            {
                DataTable Tabla = (DataTable)Session["DatosPed"];
                DataTable tblPrecios = (DataTable)Session["Precios"];
                decimal precio = tblPrecios.Rows[cmbProducto.SelectedIndex].Field<decimal>("PrecioNormal");

                decimal importe = int.Parse(txtCantidad.Text) * precio;
                Tabla.Rows.Add(txtCantidad.Text, cmbProducto.SelectedItem, "VN", cmbProducto.SelectedValue, precio, importe);
                grdProductos.DataSource = Tabla;
                grdProductos.DataBind();
                Session["DatosPed"] = Tabla;

                lblInfoRegistrar.Text = "";
                string monto = CalcMonto().ToString();
                string cantidad = SumarCantProd().ToString();
                lblMonto.Text = monto;
                grdProductos.Caption = "Detalle de Pedido - Monto: S/. " + monto + " | Cantidad: " + cantidad + " productos.";
                estableceOnClientC(monto, cantidad);
                btnRegistrarPedido.Enabled = true;

            }
            txtCantidad.Text = "";
        }
        

        public void estableceOnClientC(string monto, string cant)
        {
            btnRegistrarPedido.Attributes["onclick"] = string.Format("if(!ConfirmacionP({0},{1})) return false;", monto.Replace(",", "."), cant);
        }

        public decimal CalcMonto()
        {
            decimal SumaImporte = 0;
            for (int X = 0; X < grdProductos.Rows.Count; X++)
            {
                SumaImporte = SumaImporte + decimal.Parse(grdProductos.Rows[X].Cells[7].Text);
            }
            return SumaImporte;
        }
        
        protected void grdProductos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)Session["DatosPed"];
            dt.Rows.RemoveAt(e.RowIndex);
            //Guardar nuevos valores
            Session["DatosPed"] = dt;
            grdProductos.DataSource = dt;
            grdProductos.DataBind();

            lblInfoRegistrar.Text = "";
            string monto = CalcMonto().ToString();
            string cantidad = SumarCantProd().ToString();
            lblMonto.Text = monto;
            grdProductos.Caption = "Detalle de Pedido - Monto: S/. " + monto + " | Cantidad: " + cantidad + " productos.";
            estableceOnClientC(monto, cantidad);

            if (grdProductos.Rows.Count == 0)
                btnRegistrarPedido.Enabled = false;
        }


        protected void btnRegistrarPedido_Click(object sender, EventArgs e)
        {
            if (VerifConexion("http://www.google.com"))
            {
                lblInfoRegistrar.Text = "";
                EnviarData_BD();
            }
            else
            {
                lblInfoRegistrar.Text = "Este dispositivo no se encuentra conectado a Internet. No es posible registrar este pedido por el momento.";
            }
        }

        public void EnviarData_BD()
        {
            #region Preparando Datos de Venta
            decimal montopedido = CalcMonto();
            DateTime fecha = DateTime.Now;
            if (txtFechaEntrega.Text != "")
            {
                fecha = DateTime.Parse(txtFechaEntrega.Text);
            }
            #endregion

            // Obteniendo FechaActual y Usuario de Sesión
            DataTable dtusu = (DataTable)Session["Usuario"];
            int idusuario = int.Parse(dtusu.Rows[0][0].ToString());
            DateTime ahora = DateTime.Now;

            // Registrando ENCABEZADO DE FLUJO
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn.cadena;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RegistrarPedido";

            cmd.Parameters.Add("@idcli", SqlDbType.Int).Value = cmbCliente.SelectedValue;
            cmd.Parameters.Add("@totRef", SqlDbType.Decimal).Value = montopedido;
            cmd.Parameters.Add("@fechaEnt", SqlDbType.DateTime).Value = fecha;
            //cn.conectar();
            //cmd.ExecuteNonQuery();
            //cn.cerrar();
            cn.conectar();
            DataTable dtIdPedido = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter();
            da1.SelectCommand = cmd;
            da1.Fill(dtIdPedido);
            cn.cerrar();
            int idpedido = int.Parse(dtIdPedido.Rows[0][0].ToString());

            // Enviando Datos desde GRIDVIEW
            SqlCommand cmdV = new SqlCommand();
            cmdV.Connection = cn.cadena;
            cmdV.CommandType = CommandType.StoredProcedure;
            cmdV.CommandText = "RegistrarDetPedido";
            foreach (GridViewRow row in grdProductos.Rows)
            {
                cmdV.Parameters.Clear();
                cmdV.Parameters.Add("@idpedido", SqlDbType.Int).Value = idpedido;
                cmdV.Parameters.Add("@cant", SqlDbType.Int).Value = int.Parse(row.Cells[2].Text);
                cmdV.Parameters.Add("@idlote", SqlDbType.Int).Value = 1;
                cmdV.Parameters.Add("@idprod", SqlDbType.Int).Value = int.Parse(row.Cells[5].Text);
                cmdV.Parameters.Add("@codConc", SqlDbType.VarChar).Value = row.Cells[4].Text.ToString();
                cmdV.Parameters.Add("@precUnit", SqlDbType.Decimal).Value = decimal.Parse(row.Cells[6].Text);
                cmdV.Parameters.Add("@importe", SqlDbType.Decimal).Value = decimal.Parse(row.Cells[7].Text);

                cn.conectar();
                cmdV.ExecuteNonQuery();
                cn.cerrar();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessAlert", "alert ('✓ Pedido Registrado Exitosamente.\\n\\n ID Pedido: " + idpedido + "');", true);
            grdProductos.DataSource = null;
            grdProductos.DataBind();
            btnRegistrarPedido.Enabled = false;

            DataTable tbls = new DataTable();
            tbls = (DataTable)Session["DatosPed"];
            tbls.Clear();
            Session["DatosPed"] = tbls;
            lblInfoRegistrar.Text = null;
            lblMonto.Text = "0";
            lblInfoRegistrar.Text = "";
            txtCantidad.Text = "";
            txtFechaEntrega.Text = "";
            cmbProducto.SelectedIndex = 0;
        }

        // Ocultando Columnas de GRIDVIEW
        protected void grdProductos_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[4].Visible = false;
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            grdProductos.DataSource = null;
            grdProductos.DataBind();
            DataTable tbls = new DataTable();
            tbls = (DataTable)Session["DatosPed"];
            tbls.Clear();
            Session["DatosPed"] = tbls;
            lblInfoRegistrar.Text = null;
            lblMonto.Text = "0";

            lblInfoRegistrar.Text = "";
            txtCantidad.Text = "";
            txtFechaEntrega.Text = "";

            cmbProducto.SelectedIndex = 0;
            btnRegistrarPedido.Enabled = false;
        }

        public bool VerifConexion(string mURL)
        {
            WebRequest Peticion = default(WebRequest);
            WebResponse Respuesta = default(WebResponse);
            try
            {
                Peticion = WebRequest.Create(mURL);
                Respuesta = Peticion.GetResponse();
                return true;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.NameResolutionFailure)
                {
                    return false;
                }
                return false;
            }
        }

    }

}