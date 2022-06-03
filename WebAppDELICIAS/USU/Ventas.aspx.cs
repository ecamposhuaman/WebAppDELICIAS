using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppDELICIAS.App_Start;

namespace WebAppDELICIAS
{
    public partial class Ventas : System.Web.UI.Page
    {
        consultas datitos = new consultas();
        conexion cn = new conexion();
        public Color dorado = ColorTranslator.FromHtml("#F79824");
        public Color verde = ColorTranslator.FromHtml("#5cb85c");
        public List<string> lista = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "popup2", "ejecutaCliente();", true);
            //btnRegistrarVenta.OnClientClick = string.Format("if(!ConfirmacionV({0},{1},{2})) return false;", 1, 2, 3);
            if (IsPostBack == false)
            {
                BindDataV();

                ScriptManager.RegisterStartupScript(this, typeof(Page), "popup", "hideShowDIV();", true);
                
                DataTable Tabla = new DataTable();
                Tabla.Columns.Add("Producto");
                Tabla.Columns.Add("Concepto");
                Tabla.Columns.Add("Cant.");
                Tabla.Columns.Add("P.Unit.");
                Tabla.Columns.Add("Importe");
                Tabla.Columns.Add("Bonif.");
                Tabla.Columns.Add("IdProducto");
                grdProductos.DataSource = Tabla;
                grdProductos.DataBind();
                Session["DatosVnt"] = Tabla;

                Session["idpedido"] = 0;
                
                txtDscto.Attributes.Add("placeholder", "0.00");
                txtDescripDscto.Attributes.Add("placeholder", "Descripción de descuento...");
                txtEfectivo.Attributes.Add("placeholder", "0.00");

                RellenaDatosCMBProductos();
                RellenarComboClientes();
                lblDeudaVuelto.Visible = false;
            }
        }

        public void RellenaDatosCMBProductos()
        {
            DataTable dtusu = datitos.extrae("IdentificarUsuario", "@email", Context.User.Identity.GetUserName());
            int idusuario = int.Parse(dtusu.Rows[0][0].ToString());
            datitos.rellenacomboXparam(cmbPuntoVenta, "NombreAlm", "IdAlmacen", "ListarAlmacen_xIDvendedor", "@idvendedor", idusuario);
            Session["Usuario"] = dtusu;
        }

        public void RellenarComboClientes()
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "popup", "noabrirDscto();", true);
            if (chkAutomaticClient.Checked)
            {
                DataTable dtCli = datitos.extrae("ListarClientesCMB_Optimizado"); //IdPersona, ApelNombres, IdCategCli
                cmbCliente.DataSource = dtCli;
                cmbCliente.DataValueField = "IdPersona";
                cmbCliente.DataTextField = "ApelNombres";
                cmbCliente.DataBind();
                Session["Clientes"] = dtCli;
                lista = dtCli.Rows.OfType<DataRow>().Select(dr => dr.Field<string>("ApelNombres")).ToList();
            }
            else
            {
                DataTable dtCli = datitos.extrae("ListarClientesCMB"); //IdPersona, ApelNombres, IdCategCli
                cmbCliente.DataSource = dtCli;
                cmbCliente.DataValueField = "IdPersona";
                cmbCliente.DataTextField = "ApelNombres";
                cmbCliente.DataBind();
                Session["Clientes"] = dtCli;
                lista = dtCli.Rows.OfType<DataRow>().Select(dr => dr.Field<string>("ApelNombres")).ToList();
            }
            VerPedido();
        }

        protected void txtDscto_TextChanged(object sender, EventArgs e)
        {
            string mnt = CalcMonto().ToString();
            string mntDsctd = calcMontoDescontado().ToString();
            lblMonto.Text = mnt;
            lblMontoDsctado.Text = mntDsctd;
            if (txtEfectivo.Text == "")
            {
                lblLabelDeudaVuelto.Text = "";
                lblDeudaVuelto.Text = "";
                establecerOnClientClick(mntDsctd, "0");
            }
            else
            {
                calcularDeudaVuelto();
                if(cmbContdCred.SelectedIndex == 0)
                {
                    lblLabelDeudaVuelto.ForeColor = dorado;
                    lblLabelDeudaVuelto.Text = "Vuelto: ";
                }
                else
                {
                    lblLabelDeudaVuelto.ForeColor = verde;
                    lblLabelDeudaVuelto.Text = "Deuda a generarse: ";
                }
            }
        }

        protected void cmbContdCred_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "popup", "noabrirDscto();", true);
            if (cmbContdCred.SelectedIndex == 0) //Si elige AL CONTADO
            {
                lblLabelEfectivo.ForeColor = dorado;
                lblLabelEfectivo.Text = "Efectivo:";
                txtEfectivo.ForeColor = dorado;
                if (txtEfectivo.Text == "")
                {
                    //calcularDeudaVuelto();
                    lblLabelDeudaVuelto.Text = "";
                    lblDeudaVuelto.Text = "";
                }
                else
                {
                    calcularDeudaVuelto();
                    lblLabelDeudaVuelto.ForeColor = dorado;
                    lblLabelDeudaVuelto.Text = "Vuelto: ";
                }
            }
            else
            {
                lblLabelEfectivo.ForeColor = verde;
                lblLabelEfectivo.Text = "Dinero a Cuenta:";
                txtEfectivo.ForeColor = verde;

                calcularDeudaVuelto();
                lblLabelDeudaVuelto.ForeColor = verde;
                lblLabelDeudaVuelto.Text = "Deuda a generarse: ";
                
            }
           
        }

        protected void txtEfectivo_TextChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "popup", "noabrirDscto();", true);
            if (txtEfectivo.Text == "")
            {
                calcularDeudaVuelto();
                
            }
            else
            {
                calcularDeudaVuelto();
                if (cmbContdCred.SelectedIndex == 0)
                {
                    lblLabelDeudaVuelto.ForeColor = dorado;
                    lblLabelDeudaVuelto.Text = "Vuelto: ";
                }
                else
                {
                    lblLabelDeudaVuelto.ForeColor = verde;
                    lblLabelDeudaVuelto.Text = "Deuda a generarse: ";
                }
            }
            
        }

        public decimal CalcMonto()
        {
            lblInfoRegistrar.Text = "";
            decimal SumaImporte = 0;
            int tam = grdProductos.Rows.Count;
            for (int X = 0; X < tam; X++)
            {
                SumaImporte = SumaImporte + decimal.Parse(grdProductos.Rows[X].Cells[6].Text);
            }
            return SumaImporte;
        }

        public decimal calcMontoDescontado()
        {
            decimal montoDscontado = CalcMonto();
            if (txtDscto.Text != "")
            {
                decimal dsct = decimal.Parse(txtDscto.Text, new NumberFormatInfo() { NumberDecimalSeparator = "." });
                montoDscontado = montoDscontado - dsct;
                //(decimal)myNum.ToString("0.##")
            }
            return montoDscontado;

        }

        public void calcularDeudaVuelto()
        {
            decimal mntDesctd = calcMontoDescontado();
            decimal dineroefect = 0;
            if (txtEfectivo.Text != "")
            {
                dineroefect = decimal.Parse(txtEfectivo.Text, new NumberFormatInfo() { NumberDecimalSeparator = "." });
            }

            decimal vuelto = dineroefect - mntDesctd;
            decimal deuda = mntDesctd - dineroefect;
            if (cmbContdCred.SelectedIndex == 0) //Si elige AL CONTADO
            {
                lblDeudaVuelto.Text = "S/." + vuelto.ToString();
                lblDeudaVuelto.ForeColor = dorado; //dorado
                lblLabelDeudaVuelto.Text = "Vuelto: ";
                if (vuelto < 0)
                    lblDeudaVuelto.ForeColor = Color.Red;
                establecerOnClientClick(mntDesctd.ToString(), "0");
            }
            else
            {
                lblDeudaVuelto.Text = "S/." + deuda.ToString();
                lblDeudaVuelto.ForeColor = verde; //verde
                lblLabelDeudaVuelto.Text = "Deuda a generarse: ";
                if (deuda < 0)
                    lblDeudaVuelto.ForeColor = Color.Red;
                establecerOnClientClick(mntDesctd.ToString(), deuda.ToString());
            }

        }

        public decimal SumBonif()
        {
            decimal Bonificaciones = 0;
            int tam = grdProductos.Rows.Count;
            for (int X = 0; X < tam; X++)
            {
                Bonificaciones = Bonificaciones + decimal.Parse(grdProductos.Rows[X].Cells[7].Text);
            }
            return Bonificaciones;
        }

        protected void grdProductos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "popup", "noabrirDscto();", true);
            DataTable dt = (DataTable)Session["DatosVnt"];
            dt.Rows.RemoveAt(e.RowIndex);
            //Guardar nuevos valores
            Session["DatosVnt"] = dt;
            grdProductos.DataSource = dt;
            grdProductos.DataBind();
            string monto = CalcMonto().ToString();
            string montoDsctd = calcMontoDescontado().ToString();
            lblMonto.Text = monto;
            lblMontoDsctado.Text = montoDsctd;
            grdProductos.Caption = "Detalle de Productos Seleccionados - SUB TOTAL: S/. " + monto + " (BONIF: S/." + SumBonif() + ")";

            establecerOnClientClick(montoDsctd, "0");
            alterandoDet_Venta();
            if(grdProductos.Rows.Count == 0)
            {
                btnRegistrarVenta.Enabled = false;
                Session["idpedido"] = 0;
                lblCantProd.Text = null;
                btnLimpiar.Visible = false;
            }
            else
                btnLimpiar.Visible = true;
            
        }

        public void alterandoDet_Venta()
        {
            lblLabelDeudaVuelto.Text = "";  // Vuelve a estar en blanco para atender un nuevo monto A CUENTA
            lblDeudaVuelto.Text = "";
            txtEfectivo.Text = "";
            lblLabelEfectivo.Text = "Efectivo";
            lblLabelEfectivo.ForeColor = dorado;
            cmbContdCred.SelectedIndex = 0;
        }

        protected void btnRegistrarVenta_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "popup", "noabrirDscto();", true);
            if (VerifConexion("https://www.google.com"))
            {
                lblInfoRegistrar.Text = "";
                EnviarData_BD();
            }
            else
            {
                lblInfoRegistrar.Text = "Este dispositivo no se encuentra conectado a Internet. No es posible registrar esta Venta por el momento.";
            }

            RellenarComboClientes();
        }
        
        public void EnviarData_BD()
        {
            #region Preparando Datos de Venta
            decimal montoVenta = CalcMonto();
            decimal descuento = 0;
            if (txtDscto.Text != "")
            {
                descuento = decimal.Parse(txtDscto.Text, new NumberFormatInfo() { NumberDecimalSeparator = "." });
            }
            decimal montoDsctado = calcMontoDescontado();
            decimal deuda = 0;
            if (cmbContdCred.SelectedValue == "1")
            {
                deuda = montoDsctado;
                if (txtEfectivo.Text != "")
                {
                    deuda = montoDsctado - decimal.Parse(txtEfectivo.Text, new NumberFormatInfo() { NumberDecimalSeparator = "." });
                }
            }
            decimal Bonificaciones = SumBonif();
            
            #endregion

            // Obteniendo FechaActual y Usuario de Sesión
            DataTable dtusu = (DataTable)Session["Usuario"];
            int idusuario = int.Parse(dtusu.Rows[0][0].ToString());
            DateTime ahora = DateTime.UtcNow;

            // Registrando ENCABEZADO DE FLUJO
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn.cadena;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RegistrarVenta";
            cmd.Parameters.Add("@idalm", SqlDbType.Int).Value = cmbPuntoVenta.SelectedValue;
            cmd.Parameters.Add("@idcli", SqlDbType.Int).Value = cmbCliente.SelectedValue;
            cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = ahora;
            cmd.Parameters.Add("@idusu", SqlDbType.Int).Value = idusuario;
            cmd.Parameters.Add("@mnto", SqlDbType.Decimal).Value = montoVenta;
            cmd.Parameters.Add("@dscto", SqlDbType.Decimal).Value = descuento;
            cmd.Parameters.Add("@descrDscto", SqlDbType.VarChar).Value = txtDescripDscto.Text;
            cmd.Parameters.Add("@mntoDsctdo", SqlDbType.Decimal).Value = montoDsctado;
            cmd.Parameters.Add("@cred", SqlDbType.Int).Value = cmbContdCred.SelectedValue;
            cmd.Parameters.Add("@deuda", SqlDbType.Decimal).Value = deuda;
            cmd.Parameters.Add("@bonif", SqlDbType.Decimal).Value = Bonificaciones;
            cn.conectar();
            DataTable dtIdVenta = new DataTable();   
            SqlDataAdapter da1 = new SqlDataAdapter();
            da1.SelectCommand = cmd;
            da1.Fill(dtIdVenta);
            cn.cerrar();
            int idVenta = int.Parse(dtIdVenta.Rows[0][0].ToString());

            // Enviando Datos desde GRIDVIEW
            SqlCommand cmdV = new SqlCommand();
            cmdV.Parameters.Clear();
            cmdV.Connection = cn.cadena;
            cmdV.CommandType = CommandType.StoredProcedure;
            cmdV.CommandText = "RegistrarDetVenta";
            foreach (GridViewRow row in grdProductos.Rows)
            {
                cmdV.Parameters.Clear();
                cmdV.Parameters.Add("@idventa", SqlDbType.Int).Value = idVenta;
                cmdV.Parameters.Add("@idPVnta", SqlDbType.Int).Value = cmbPuntoVenta.SelectedValue;     // Dato Adicional para registro en KARDEX
                //cmdV.Parameters.Add("@idAlmDest", SqlDbType.Int).Value = cmbPuntoVenta.SelectedValue;     // Dato Adicional para registro en KARDEX
                cmdV.Parameters.Add("@cantidad", SqlDbType.Int).Value = int.Parse(row.Cells[4].Text);
                cmdV.Parameters.Add("@idLote", SqlDbType.Int).Value = 1;    //Lote único para esta versión
                cmdV.Parameters.Add("@idprod", SqlDbType.Int).Value = int.Parse(row.Cells[8].Text);
                cmdV.Parameters.Add("@codconc", SqlDbType.VarChar).Value = row.Cells[3].Text;
                cmdV.Parameters.Add("@punit", SqlDbType.Decimal).Value = decimal.Parse(row.Cells[5].Text);
                cmdV.Parameters.Add("@importe", SqlDbType.Decimal).Value = decimal.Parse(row.Cells[6].Text);

                cn.conectar();
                cmdV.ExecuteNonQuery();
                cn.cerrar();
            }
            #region ELIMINAR PEDIDO
            int idpd = (int)Session["idpedido"];
            if (idpd > 0)
            {
                SqlCommand cmde = new SqlCommand();
                cmde.Connection = cn.cadena;
                cmde.CommandType = CommandType.StoredProcedure;
                cmde.CommandText = "EliminarPedido";
                cmde.Parameters.Add("@idPedido", SqlDbType.Int).Value = idpd;
                cn.conectar();
                cmde.ExecuteNonQuery();
                cn.cerrar();
                cmbPedidos.Items.FindByValue(cmbPedidos.SelectedValue).Enabled = false;
            }
            #endregion

            ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessAlert", "alert ('✓ Venta Registrada.\\n\\n ID Venta: " + idVenta + "');", true);
            Limpiar();
        }

        public void establecerOnClientClick(string mntoDsctd, string deu)
        {
            string dsct = "0";
            if (txtDscto.Text != "")
                dsct = txtDscto.Text;

            DataTable tblCli = (DataTable)Session["Clientes"];
            string categCliente = tblCli.Rows[cmbCliente.SelectedIndex][2].ToString();
            if (categCliente == "5")
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn.cadena;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ObtenerDeudaEmpleado";
                cmd.Parameters.Add("@idCliente", SqlDbType.Int).Value = cmbCliente.SelectedValue;
                cmd.Parameters.Add("@mnto", SqlDbType.Decimal).Value = deu;
                cn.conectar();
                DataTable dtdeuda = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter();
                da1.SelectCommand = cmd;
                da1.Fill(dtdeuda);
                cn.cerrar();
                string rpta = dtdeuda.Rows[0][0].ToString();
                string deudatotal = dtdeuda.Rows[0][1].ToString();

                btnRegistrarVenta.Attributes["onclick"] = string.Format("if(!NotificarVentaEmpleado({0},{1},{2},{3},{4})) return false;", mntoDsctd.Replace(",", "."), dsct.Replace(",", "."), deu.Replace(",", "."), deudatotal.Replace(",", "."), rpta);

            }
            else
            {
                btnRegistrarVenta.Attributes["onclick"] = string.Format("if(!ConfirmacionV({0},{1},{2})) return false;", mntoDsctd.Replace(",", "."), dsct.Replace(",", "."), deu.Replace(",", "."));
            }
        }

        // Ocultando la Columna IdProducto de GRIDVIEW
        protected void grdProductos_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //e.Row.Cells[5].Visible = false;
            e.Row.Cells[8].Visible = false;
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        public void Limpiar()
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "popup", "noabrirDscto();", true);
            //Session["Datos"] = null;
            grdProductos.DataSource = null;
            grdProductos.DataBind();
            DataTable tbls = new DataTable();
            tbls = (DataTable)Session["DatosVnt"];
            tbls.Clear();
            Session["DatosVnt"] = tbls;
            lblInfoRegistrar.Text = null;
            lblMonto.Text = "0";
            lblMontoDsctado.Text = "0";
            Session["idpedido"] = 0;
            
            txtDscto.Text = "";
            txtDescripDscto.Text = "";
            btnLimpiar.Visible = false;
            btnRegistrarVenta.Enabled = false;
            alterandoDet_Venta();
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

        protected void btnAplDscto_Click(object sender, EventArgs e)
        {
            lblDescuento.Visible = true;
            txtDescripDscto.Visible = true;
            txtDescripDscto.Visible = true;
        }

        protected void cmbPedidos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "popup", "noabrirDscto();", true);
            Limpiar();

            if (cmbPedidos.SelectedIndex != 0)
            {
                int idPedido = int.Parse(cmbPedidos.SelectedValue);
                DataTable pedidos = datitos.extrae("BuscarDetallePedido", "@idPedido", idPedido);
                // PEDIDOS ==  D.Cantidad, D.IdProducto, P.NombreProd, D.CodConc, D.PUnit
                DataTable Tabla = (DataTable)Session["DatosVnt"];
                // TABLA -- Producto, Concepto, Cant, precio, Importe, Bonif, IdProducto
                DataTable tblPrecios = datitos.extrae("ListarProdPrecio");
                // tblPrecios -- Id, Precio
                foreach (DataRow row in pedidos.Rows)
                {
                    decimal precio = tblPrecios.Rows[int.Parse(row[1].ToString()) - 1].Field<decimal>("PrecioNormal");
                    decimal importe = int.Parse(row[0].ToString()) * precio;
                    Tabla.Rows.Add(row[2], row[3], row[0], precio, importe, 0, row[1]); //Traslado
                }
                
                grdProductos.DataSource = Tabla;
                grdProductos.DataBind();
                Session["DatosVnt"] = Tabla;
                string monto = CalcMonto().ToString();
                string montoDsctd = calcMontoDescontado().ToString();
                lblMonto.Text = monto;
                lblMontoDsctado.Text = montoDsctd;
                grdProductos.Caption = "Detalle de Productos (de Pedido) - SUB TOTAL: S/. " + monto + " (BONIF: S/." + SumBonif() + ")";
                establecerOnClientClick(montoDsctd, "0");
                alterandoDet_Venta();
                btnRegistrarVenta.Enabled = true;

                Session["idpedido"] = idPedido;
            }
            else
            {
                Session["idpedido"] = 0;
            }

        }

        protected void cmbCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            VerPedido();
        }

        public void VerPedido()
        {
            //Limpiar();
            calcularDeudaVuelto();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "popup", "noabrirDscto();", true);
            //Limpiar();
            Session["idpedido"] = 0;
            cmbPedidos.Items.Clear();
            cmbPedidos.Items.Insert(0, "Pedido:");
            datitos.rellenacomboXparam(cmbPedidos, "VistaPedido", "IdPedido", "ListarPedidoCMB", "@idCliente", int.Parse(cmbCliente.SelectedValue));
            if (cmbPedidos.Items.Count > 1)
                cmbPedidos.Visible = true;
            else
                cmbPedidos.Visible = false;

        }

        protected void chkAutomaticClient_CheckedChanged(object sender, EventArgs e)
        {
            RellenarComboClientes();
        }

        protected void btnModal_Click(object sender, EventArgs e)
        {
            string script = @"<script type='text/javascript'>AbrirModal();</script>";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "AbrirModal", script, false);

            //if (IsSessionTimedOut()) // String.IsNullOrEmpty(sss)
            //{
            //    Response.Redirect("~/Account/Login.aspx");
            //}
            //else
            //{
            //    string script = @"<script type='text/javascript'>AbrirModal();</script>";
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), "AbrirModal", script, false);
            //}

        }

        private void BindDataV()
        {
            DataTable dta = new DataTable();
            dta = datitos.extrae("ListarProductosPreVenta");
            if (dta.Rows.Count > 0)
            {
                grdContact.DataSource = dta;
                grdContact.DataBind();
            }
            else
            {
                dta.Rows.Add(dta.NewRow());
                grdContact.DataSource = dta;
                grdContact.DataBind();

                int TotalColumns = grdContact.Rows[0].Cells.Count;
                grdContact.Rows[0].Cells.Clear();
                grdContact.Rows[0].Cells.Add(new TableCell());
                grdContact.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                grdContact.Rows[0].Cells[0].Text = "Stock en cero... ;)";
            }
        }

        protected void grdContact_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    int ID = int.Parse(DataBinder.Eval(e.Row.DataItem, "IdProducto").ToString());

            //    if (ID >= 10)
            //        e.Row.BackColor = System.Drawing.Color.Red;
            //    else
            //        e.Row.BackColor = System.Drawing.Color.Green;
            //}



            //DataTable contactType = new DataTable();
            //contactType = datitos.extrae("ListaTipos_CMB_PRUEBAS");

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Label lblType = (Label)e.Row.FindControl("lblType");
            //    if (lblType != null)
            //    {
            //        int typeId = Convert.ToInt32(lblType.Text);
            //        DataTable dato = datitos.extrae("SeleccionarTipo_PRUEBAS","@Id", typeId);
            //        lblType.Text = dato.Rows[0][0].ToString();
            //    }
            //    DropDownList cmbType = (DropDownList)e.Row.FindControl("cmbType");
            //    if (cmbType != null)
            //    {
            //        cmbType.DataSource = contactType; ;
            //        cmbType.DataTextField = "NombreTipo";
            //        cmbType.DataValueField = "IdTipo";
            //        cmbType.DataBind();
            //        cmbType.SelectedValue = grdContact.DataKeys[e.Row.RowIndex].Values[1].ToString();
            //    }
            //}
            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    DropDownList cmbNewType = (DropDownList)e.Row.FindControl("cmbNewType");
            //    cmbNewType.DataSource = contactType;
            //    cmbNewType.DataBind();
            //}
        }
        protected void grdContact_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdContact.EditIndex = -1;
            BindDataV();
        }
        protected void grdContact_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int flag = 0;
            Label lblId = (Label)grdContact.Rows[e.RowIndex].FindControl("lblId");
            TextBox txtName = (TextBox)grdContact.Rows[e.RowIndex].FindControl("txtName");
            CheckBox chkActive = (CheckBox)grdContact.Rows[e.RowIndex].FindControl("chkActive");
            DropDownList cmbType = (DropDownList)grdContact.Rows[e.RowIndex].FindControl("cmbType");
            DropDownList ddlSex = (DropDownList)grdContact.Rows[e.RowIndex].FindControl("ddlSex");
            if (chkActive.Checked) flag = 1; else flag = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn.cadena;
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "UpdateContactos_PRUEBAS";
            cmd.Parameters.Add("@Original_Id", SqlDbType.Int).Value = Convert.ToInt32(lblId.Text);
            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = txtName.Text;
            cmd.Parameters.Add("@Sexo", SqlDbType.VarChar).Value = ddlSex.SelectedValue;
            cmd.Parameters.Add("@Tipo", SqlDbType.Int).Value = cmbType.SelectedValue;
            cmd.Parameters.Add("@EsActivo", SqlDbType.Int).Value = flag;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            //contact.Update(txtName.Text, ddlSex.SelectedValue, cmbType.SelectedValue, flag, Convert.ToInt32(lblId.Text));
            grdContact.EditIndex = -1;
            BindDataV();
        }
        protected void grdContact_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            int id = Convert.ToInt32(grdContact.DataKeys[e.RowIndex].Values[0].ToString());

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn.cadena;
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteContactos_PRUEBAS";
            cmd.Parameters.Add("@Original_Id", SqlDbType.Int).Value = id;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();

            BindDataV();
        }
        protected void grdContact_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int flag = 0;
            if (e.CommandName.Equals("Insert"))
            {
                TextBox txtNewName = (TextBox)grdContact.FooterRow.FindControl("txtNewName");
                CheckBox chkNewActive = (CheckBox)grdContact.FooterRow.FindControl("chkNewActive");
                DropDownList cmbNewType = (DropDownList)grdContact.FooterRow.FindControl("cmbNewType");
                DropDownList ddlNewSex = (DropDownList)grdContact.FooterRow.FindControl("ddlNewSex");
                if (chkNewActive.Checked) flag = 1; else flag = 0;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn.cadena;
                SqlDataAdapter da = new SqlDataAdapter();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "InsertContactos_PRUEBAS";
                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = txtNewName.Text;
                cmd.Parameters.Add("@Sexo", SqlDbType.VarChar).Value = ddlNewSex.SelectedValue;
                cmd.Parameters.Add("@Tipo", SqlDbType.Int).Value = cmbNewType.SelectedValue;
                cmd.Parameters.Add("@EsActivo", SqlDbType.Int).Value = flag;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                //contact.Insert(txtNewName.Text, ddlNewSex.SelectedValue, cmbNewType.SelectedValue, flag);
                BindDataV();
            }
        }
        protected void grdContact_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdContact.EditIndex = e.NewEditIndex;
            BindDataV();
        }

        protected void BtnAniadirProductos_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "popup", "noabrirDscto();", true);
            
            DataTable Tabla = new DataTable();
            Tabla = (DataTable)Session["DatosVnt"];

            foreach (GridViewRow row in grdContact.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    //Preparando Datos desde GridView Editado en tiempo de ejecución
                    int IdProd = Convert.ToInt32(grdContact.DataKeys[row.RowIndex].Value);
                    string NombreProd = ((Label)row.Cells[1].FindControl("lblName")).Text;
                    decimal Precio = decimal.Parse(((Label)row.Cells[2].FindControl("lblPrecio")).Text);
                    int CantVN = 0;
                    int CantBnf = 0;
                    int CantRcV = 0;
                    int CantRcM = 0;
                    int.TryParse((((TextBox)row.Cells[3].FindControl("txtCantVN")).Text), out CantVN);
                    int.TryParse((((TextBox)row.Cells[4].FindControl("txtCantBnf")).Text), out CantBnf);
                    int.TryParse((((TextBox)row.Cells[5].FindControl("txtCantRcV")).Text), out CantRcV);
                    int.TryParse((((TextBox)row.Cells[6].FindControl("txtCantRcM")).Text), out CantRcM);
                    

                    #region -- Código para guardar GridView en Variable de Sesión --
                    if (CantVN > 0)
                        Tabla.Rows.Add(NombreProd, "VN", CantVN , Precio , CantVN * Precio, 0, IdProd);
                    if (CantBnf > 0)                               
                        Tabla.Rows.Add(NombreProd, "Bnf", CantBnf, Precio, 0, CantBnf * Precio, IdProd);
                    if (CantRcV > 0)                               
                        Tabla.Rows.Add(NombreProd, "RcV", CantRcV, Precio, 0, 0, IdProd);
                    if (CantRcM > 0)                              
                        Tabla.Rows.Add(NombreProd, "RcM", CantRcM, Precio, 0, 0, IdProd);
                    #endregion
                }
            }
            
            #region --Filtrar eliminando Duplicados--
            DataTable TablaNDupl = new DataTable();
            TablaNDupl.Columns.Add("Producto");
            TablaNDupl.Columns.Add("Concepto");
            TablaNDupl.Columns.Add("Cant.");
            TablaNDupl.Columns.Add("P.Unit.");
            TablaNDupl.Columns.Add("Importe");
            TablaNDupl.Columns.Add("Bonif.");
            TablaNDupl.Columns.Add("IdProducto");
            if (Tabla.Rows.Count > 0)
            {
                TablaNDupl = Tabla.AsEnumerable()
                    .GroupBy(r => new
                    {
                        Version = r.Field<String>("IdProducto"),
                        Col1 = r.Field<String>("Concepto")
                    })
                    .Select(g =>
                    {
                        var row = g.First();
                        row.SetField("Cant.", g.Sum(r => long.Parse(r.Field<string>("Cant."))));
                        row.SetField("Importe", g.Sum(r => decimal.Parse(r.Field<string>("Importe"))));
                        row.SetField("Bonif.", g.Sum(r => decimal.Parse(r.Field<string>("Bonif."))));
                        return row;
                    }).CopyToDataTable();
            }


            #region Para Filtrar y eliminar duplicados con una sola columna
            //DataTable TablaNDupl2 = new DataTable();
            //TablaNDupl2 = dt.AsEnumerable()
            //   .GroupBy(r => r.Field<string>("Id"))
            //   .Select(g =>
            //   {
            //       var row = dt.NewRow();
            //       row.ItemArray = new object[]
            //           {
            //                   g.Key,
            //                   g.Sum(r => r.Field<int>("Valor")) 
            //                   //o... g.Sum(r => long.Parse(r.Field<string>("Valor")))
            //            };
            //       return row;
            //   }).CopyToDataTable();
            #endregion
            #endregion

            grdProductos.DataSource = TablaNDupl;
            grdProductos.DataBind();
            Session["Datos"] = TablaNDupl;
            
            lblInfoRegistrar.Text = "";
            string monto = CalcMonto().ToString();
            string montoDsctd = calcMontoDescontado().ToString();
            lblMonto.Text = monto;
            lblMontoDsctado.Text = montoDsctd;
            grdProductos.Caption = "Detalle de Productos Seleccionados - SUB TOTAL: S/. " + monto + " (BONIF: S/." + SumBonif() + ")";
            establecerOnClientClick(montoDsctd, "0");
            alterandoDet_Venta();

            if (grdProductos.Rows.Count > 0)
            {
                btnLimpiar.Visible = true;
                btnRegistrarVenta.Enabled = true;
            }
            else
                lblCantProd.Text = null;

            //Tener Listo el nuevo Formato GridView para el Modal
            BindDataV();

        }
    }
}