using Microsoft.AspNet.Identity;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppDELICIAS.App_Start;
using System.Linq;

namespace WebAppDELICIAS
{
    public partial class Flujos : System.Web.UI.Page
    {
        consultas datitos = new consultas();
        conexion cn = new conexion();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                BindData();

                RellenaDatos();
                
                DataTable Tabla = new DataTable();
                Tabla.Columns.Add("NombreProd");
                Tabla.Columns.Add("Cantidad");
                Tabla.Columns.Add("Estado");
                Tabla.Columns.Add("IdProducto");
                grdProductos.DataSource = Tabla;
                grdProductos.DataBind();
                Session["Datos"] = Tabla;
                
            }
        }

        public void RellenaDatos()
        {
            //DataTable dtusu = (DataTable)Session["Usuario"];
            //int idusuario = int.Parse(dtusu.Rows[0][0].ToString());
            //datitos.rellenacomboXparam(cmbAlmacenOrig, "NombreAlm", "IdAlmacen", "ListarAlmacen_xIDvendedor", "@idvendedor", idusuario);
            datitos.rellenacombo(cmbAlmacenOrig, "NombreAlm", "IdAlmacen", "ListarAlmacenesCMB_NC");
            datitos.rellenacombo(cmbAlmacenDest, "NombreAlm", "IdAlmacen", "ListarAlmacenesCMB_NC");
            cmbAlmacenDest.Items.FindByValue(cmbAlmacenOrig.SelectedValue).Enabled = false;

            //2 datitos.rellenacombo(cmbProducto, "NombreProd", "IdProducto", "ListarProdCMB");
        }
        
        public void SumarCantProd()
        {
            int CantProductos = 0;
            for (int X = 0; X < grdProductos.Rows.Count; X++)
            {
                CantProductos = CantProductos + int.Parse(grdProductos.Rows[X].Cells[3].Text);
            }
            grdProductos.Caption = "Detalle de Productos Seleccionados. (" + CantProductos.ToString() + " uds.)";
            establecerOnClientClick(grdProductos.Rows.Count.ToString(), CantProductos.ToString());
        }

        protected void grdProductos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)Session["Datos"];
            
            dt.Rows.RemoveAt(e.RowIndex); //RowIndex
            //Ordenar y plasmar nuevos valores
            Session["Datos"] = dt;
            grdProductos.DataSource = dt;
            grdProductos.DataBind();
            
            SumarCantProd();
            if (grdProductos.Rows.Count == 0)
            {
                btnRegistrarFlujo.Enabled = false;
                lblCantProd.Text = null;
                btnLimpiar.Visible = false;
            }
            else
                btnLimpiar.Visible = true;
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

        protected void btnRegistrarFlujo_Click(object sender, EventArgs e)
        {
            if (VerifConexion("http://www.google.com"))
            {
                lblInfoRegistrar.Text = "";
                if (String.IsNullOrEmpty(Context.User.Identity.GetUserName().ToString()))
                    lblInfoRegistrar.Text = "Es necesario Iniciar Sesión para registrar movimientos";
                else
                    EnviarData_BD();
            }
            else
            {
                lblInfoRegistrar.Text = "Este dispositivo no se encuentra conectado a Internet por lo que no es posible registrar este movimiento.";
            }
        }

        public void EnviarData_BD()
        {
            // Obteniendo Usuario de Sesión y FechaActual.
            DataTable dtusu = datitos.extrae("IdentificarUsuario", "@email", Context.User.Identity.GetUserName());
            int idusuario = int.Parse(dtusu.Rows[0][0].ToString());
            DateTime ahora = DateTime.UtcNow;
            // Registrando ENCABEZADO DE FLUJO
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn.cadena;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RegistrarFlujo";
            cmd.Parameters.Add("@idalmOr", SqlDbType.Int).Value = cmbAlmacenOrig.SelectedValue;
            cmd.Parameters.Add("@idalmDs", SqlDbType.Int).Value = cmbAlmacenDest.SelectedValue;
            cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = ahora;
            cmd.Parameters.Add("@idusu", SqlDbType.Int).Value = idusuario;
            //cn.conectar();
            //cmd.ExecuteNonQuery();
            //cn.cerrar();
            cn.conectar();
            DataTable dtIdFlujo = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter();
            da2.SelectCommand = cmd;
            da2.Fill(dtIdFlujo);
            cn.cerrar();
            int idflujo = int.Parse(dtIdFlujo.Rows[0][0].ToString());

            // Enviando Datos desde GRIDVIEW
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = cn.cadena;
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.CommandText = "RegistrarDetFlujo";
            foreach (GridViewRow row in grdProductos.Rows)
            {
                cmd2.Parameters.Clear();
                cmd2.Parameters.Add("@idFlujo", SqlDbType.Int).Value = idflujo;
                cmd2.Parameters.Add("@idalmOr", SqlDbType.Int).Value = cmbAlmacenOrig.SelectedValue;     // Dato Adicional para registro en KARDEX
                cmd2.Parameters.Add("@idalmDs", SqlDbType.Int).Value = cmbAlmacenDest.SelectedValue;     // Dato Adicional para registro en KARDEX
                cmd2.Parameters.Add("@cantidad", SqlDbType.Int).Value = int.Parse(row.Cells[3].Text);
                cmd2.Parameters.Add("@idLote", SqlDbType.Int).Value = 1;    //Lote único para esta versión
                cmd2.Parameters.Add("@idprod", SqlDbType.Int).Value = int.Parse(row.Cells[5].Text);
                cmd2.Parameters.Add("@estado", SqlDbType.VarChar).Value = row.Cells[4].Text;

                cn.conectar();
                cmd2.ExecuteNonQuery();
                cn.cerrar();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessAlert", "alert ('✓ Movimiento Registrado. \\n\\n ID de Movimiento: " + idflujo + "');", true);
            grdProductos.DataSource = null;
            grdProductos.DataBind();
            DataTable tbls = new DataTable();
            tbls = (DataTable)Session["Datos"];
            tbls.Clear();
            Session["Datos"] = tbls;
            lblCantProd.Text = null;
            lblInfoRegistrar.Text = null;
            btnRegistrarFlujo.Enabled = false;
        }
        
        protected void cmbAlmacenOrig_SelectedIndexChanged(object sender, EventArgs e)
        {
            datitos.rellenacombo(cmbAlmacenDest, "NombreAlm", "IdAlmacen", "ListarAlmacenesCMB_NC");
            cmbAlmacenDest.Items.FindByValue(cmbAlmacenOrig.SelectedValue).Enabled = false;
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            grdProductos.DataSource = null;
            grdProductos.DataBind();
            DataTable tbls = (DataTable)Session["Datos"];
            tbls.Clear();
            Session["Datos"] = tbls;
            lblCantProd.Text = null;
            lblInfoRegistrar.Text = null;
            btnLimpiar.Visible = false;
            btnRegistrarFlujo.Enabled = false;
        }

        public void establecerOnClientClick(string items, string cant)
        {
            btnRegistrarFlujo.Attributes["onclick"] = string.Format("if(!ConfirmacionF({0},{1})) return false;", items, cant);
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

        private void BindData()
        {
            DataTable dta = new DataTable();
            dta = datitos.extrae("ListarProductosPreFlujo");
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
            BindData();
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
            BindData();
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
            
            BindData();
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
                BindData();
            }
        }
        protected void grdContact_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdContact.EditIndex = e.NewEditIndex;
            BindData();
        }
        

        // Ocultando la Columna 4 IdProducto de GRIDVIEW
        protected void grdProductos_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[5].Visible = false;
        }

        protected void BtnAniadirProductos_Click(object sender, EventArgs e)
        {
            // --- Conexión para Código para Guardar GRIDVIEW en Base de Datos ---
            //SqlCommand cmdV = new SqlCommand();
            //cmdV.Connection = cn.cadena;
            //cmdV.CommandType = CommandType.StoredProcedure;
            //cmdV.CommandText = "insertarEnTablaPreFlujos";

            //Conexión para Código para guardar DridView en Variable de Sesión
            DataTable Tabla = new DataTable();
            Tabla = (DataTable)Session["Datos"];

            foreach (GridViewRow row in grdContact.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    //Preparando Datos desde GridView Editado en tiempo de ejecución
                    int IdProd = Convert.ToInt32(grdContact.DataKeys[row.RowIndex].Value);
                    string NombreProd = ((Label)row.Cells[1].FindControl("lblName")).Text;
                    int CantA = 0;
                    int CantB = 0;
                    int CantC = 0;
                    int.TryParse((((TextBox)row.Cells[2].FindControl("txtCantA")).Text), out CantA);
                    int.TryParse((((TextBox)row.Cells[3].FindControl("txtCantB")).Text), out CantB);
                    int.TryParse((((TextBox)row.Cells[4].FindControl("txtCantC")).Text), out CantC);

                    #region -- Código para Guardar GRIDVIEW en Base de Datos --
                    //cmdV.Parameters.Clear();
                    //cmdV.Parameters.Add("@IdProducto", SqlDbType.Int).Value = IdProd;
                    //cmdV.Parameters.Add("@NombreProd", SqlDbType.VarChar).Value = NombreProd;
                    //if (CantA > 0 || CantB > 0 || CantC > 0)
                    //{
                    //    cmdV.Parameters.Add("@CantA", SqlDbType.Int).Value = CantA;
                    //    cmdV.Parameters.Add("@CantB", SqlDbType.Int).Value = CantB;
                    //    cmdV.Parameters.Add("@CantC", SqlDbType.Int).Value = CantC;
                    //    cn.conectar();
                    //    cmdV.ExecuteNonQuery();
                    //    cn.cerrar();
                    //}
                    #endregion

                    #region -- Código para guardar GridView en Variable de Sesión --
                    if (CantA > 0)
                        Tabla.Rows.Add(NombreProd, CantA, "A", IdProd);
                    if (CantB > 0)
                        Tabla.Rows.Add(NombreProd, CantB, "B", IdProd);
                    if (CantC > 0)
                        Tabla.Rows.Add(NombreProd, CantC, "C", IdProd);
                    #endregion
                }
            }
            #region --Filtrar eliminando Duplicados--
            DataTable TablaNDupl = new DataTable();
            TablaNDupl.Columns.Add("NombreProd");
            TablaNDupl.Columns.Add("Cantidad");
            TablaNDupl.Columns.Add("Estado");
            TablaNDupl.Columns.Add("IdProducto");
            if (Tabla.Rows.Count > 0)
            {
                TablaNDupl = Tabla.AsEnumerable()
                    .GroupBy(r => new
                    {
                        Version = r.Field<String>("IdProducto"),
                        Col1 = r.Field<String>("Estado")
                    })
                    .Select(g =>
                    {
                        var row = g.First();
                        row.SetField("Cantidad", g.Sum(r => long.Parse(r.Field<string>("Cantidad"))));
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

            //Ordenar Datatable por Columna
            //TablaNDupl.DefaultView.Sort = "Estado";
            //TablaNDupl = TablaNDupl.DefaultView.Table;

            grdProductos.DataSource = TablaNDupl;
            grdProductos.DataBind();
            Session["Datos"] = TablaNDupl;
            SumarCantProd();
            if (grdProductos.Rows.Count > 0)
            {
                btnLimpiar.Visible = true;
                btnRegistrarFlujo.Enabled = true;
            }
            else
                lblCantProd.Text = null;

            //Tener Listo el nuevo Formato GridView para el Modal
            BindData();
            
        }

        //public static bool IsSessionTimedOut()
        //{
        //    HttpContext ctx = HttpContext.Current;
        //    if (ctx == null)
        //        throw new Exception("Este método sólo se puede usar en una aplicación Web");
        //    //Comprobamos que haya sesión en primer lugar 
        //    //(por ejemplo si por ejemplo EnableSessionState=false)
        //    if (ctx.Session == null)
        //        return false;   //Si no hay sesión, no puede caducar
        //    //Se comprueba si se ha generado una nueva sesión en esta petición
        //    if (!ctx.Session.IsNewSession)
        //        return false;   //Si no es una nueva sesión es que no ha caducado

        //    HttpCookie objCookie = ctx.Request.Cookies["ASP.NET_SessionId"];
        //    //Esto en teoría es imposible que pase porque si hay una 
        //    //nueva sesión debería existir la cookie, pero lo compruebo porque
        //    //IsNewSession puede dar True sin ser cierto (más en el post)
        //    if (objCookie == null)
        //        return false;

        //    //Si hay un valor en la cookie es que hay un valor de sesión previo, pero como la sesión 
        //    //es nueva no debería estar, por lo que deducimos que la sesión anterior ha caducado
        //    if (!string.IsNullOrEmpty(objCookie.Value))
        //        return true;
        //    else
        //        return false;
        //}
    }
}