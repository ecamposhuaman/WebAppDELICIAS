using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppDELICIAS.App_Start;

namespace WebAppDELICIAS
{
    public partial class Clientes : System.Web.UI.Page
    {
        consultas datitos = new consultas();
        conexion cn = new conexion();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["idcli"] = 0;
                lblIdCliente.Text = "";
                deshabilitar(false);
                btnmodificar.Enabled = false;
                //btnRestabCuenta.Enabled = false;
                lblinfo1.Text = "";
                datitos.rellenacombo(cmbDistrito, "Lugar", "IdLugar", "ListarLugares_CMB");
            }
        }

        public void state(Boolean stt)
        {
            //cmbUsuarios.Enabled = stt;
        }

        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            clean_campos();
            deshabilitar(false);
            lblinfo1.Text = ""; 
            grdClientes.DataSource = datitos.extrae("ListarClientes_GRD", "@nombre", txtbuscar.Text.ToString());
            grdClientes.DataBind();
            grdClientes.Visible = true;
            btnguardar.Enabled = false;
            deshabilitar(false);
            btnmodificar.Enabled = false;
            //btnRestabCuenta.Enabled = false;
        }

        public void mostrar_datos_desdeGrid()
        {
            GridViewRow row = grdClientes.SelectedRow;
            int fila = Convert.ToInt32(grdClientes.DataKeys[row.RowIndex].Value);
            Session["idcli"] = fila;
            DataTable tabla = new DataTable();
            tabla = datitos.extrae("buscaClientes_xId", "@idcliente", fila);
            lblIdCliente.Text = tabla.Rows[0][0].ToString();
            txtDNI.Text = tabla.Rows[0][1].ToString();
            txtApNombres.Text = tabla.Rows[0][2].ToString();
            txtEmail.Text = tabla.Rows[0][3].ToString();
            txtTelefono.Text = tabla.Rows[0][4].ToString();
            cmbCategCli.SelectedValue = tabla.Rows[0][5].ToString();
            txtDiasVisita.Text = tabla.Rows[0][6].ToString();
            txtDireccion.Text = tabla.Rows[0][7].ToString();
            cmbDistrito.SelectedValue = tabla.Rows[0][8].ToString();
        }

        public void deshabilitar(Boolean estado)
        {
            txtDNI.Enabled = estado;
            txtApNombres.Enabled = estado;
            txtEmail.Enabled = estado;
            txtTelefono.Enabled = estado;
            //cmbCategCli.Enabled = estado;
            txtDiasVisita.Enabled = estado;
            txtDireccion.Enabled = estado;
            cmbDistrito.Enabled = estado;
        }

        protected void btnmodificar_Click(object sender, EventArgs e)
        {
            lblinfo1.Text = "";
            lbloperacion.Text = "m";
            btnguardar.Enabled = true;
            deshabilitar(true);
            btnmodificar.Enabled = false;
            //btnRestabCuenta.Enabled = false;
        }

        public void modificarCliente(int idCliente)
        {
            //Boolean estado;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn.cadena;
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Actualizar_Cliente";
            
            cmd.Parameters.Add("@idcliente", SqlDbType.Int).Value = idCliente;
            cmd.Parameters.Add("@dni", SqlDbType.VarChar).Value = txtDNI.Text;
            cmd.Parameters.Add("@apelnom", SqlDbType.VarChar).Value = txtApNombres.Text;
            cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = txtEmail.Text;
            cmd.Parameters.Add("@telef", SqlDbType.VarChar).Value = txtTelefono.Text;
            cmd.Parameters.Add("@idcateg", SqlDbType.Int).Value = cmbCategCli.SelectedValue;
            cmd.Parameters.Add("@diasvisita", SqlDbType.VarChar).Value = txtDiasVisita.Text.ToUpper();
            cmd.Parameters.Add("@direc", SqlDbType.VarChar).Value = txtDireccion.Text;
            //cmd.Parameters.Add("@idsector", SqlDbType.Int).Value = cmbSector.SelectedValue;
            cmd.Parameters.Add("@iddistri", SqlDbType.Int).Value = cmbDistrito.SelectedValue;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        protected void btnguardar_Click(object sender, EventArgs e)
        {
            int idp = (int)Session["idcli"];
            if (lbloperacion.Text == "m")
            {
                modificarCliente(idp);
                lblinfo1.Text = "✓ Cliente Modificado.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessAlert", "alert ('✓ Cliente Modificado.');", true);
                //Response.Write("<script>alert('Usuario Modificado');</script>");
                deshabilitar(false);
            }
            else
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn.cadena;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "InsertarCliente";
                cmd.Parameters.Add("@dni", SqlDbType.VarChar).Value = txtDNI.Text;
                cmd.Parameters.Add("@apelnom", SqlDbType.VarChar).Value = txtApNombres.Text;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = txtEmail.Text;
                cmd.Parameters.Add("@telef", SqlDbType.VarChar).Value = txtTelefono.Text;
                cmd.Parameters.Add("@idcateg", SqlDbType.Int).Value = cmbCategCli.SelectedValue;
                cmd.Parameters.Add("@diasvisita", SqlDbType.VarChar).Value = txtDiasVisita.Text.ToUpper();
                cmd.Parameters.Add("@direc", SqlDbType.VarChar).Value = txtDireccion.Text;
                //cmd.Parameters.Add("@idsector", SqlDbType.Int).Value = cmbSector.SelectedValue;
                cmd.Parameters.Add("@iddistri", SqlDbType.Int).Value = cmbDistrito.SelectedValue;

                cn.conectar();
                cmd.ExecuteNonQuery();
                cn.cerrar();
                lblinfo1.Text = "✓ Nuevo Cliente Registrado.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessAlert", "alert ('✓ Cliente Nuevo Registrado.');", true);
                //Response.Write("<script>alert('Usuario Nuevo Registrado');</script>");
            }
            //btnRestabCuenta.Enabled = true;
        }

        protected void btnnuevo_Click(object sender, EventArgs e)
        {
            lblinfo1.Text = "";
            clean_campos();
            lblIdCliente.Text = "";
            btnguardar.Enabled = true;
            lbloperacion.Text = "n";
            deshabilitar(true);
            grdClientes.Visible = false;
            btnmodificar.Enabled = false;
            //btnRestabCuenta.Enabled = false;
        }

        public void clean_campos()
        {
            lblIdCliente.Text = "";
            txtDNI.Text = "";
            txtApNombres.Text = "";
            txtEmail.Text = "";
            txtTelefono.Text = "";
            cmbCategCli.SelectedIndex = 0;
            txtDiasVisita.Text = "";
            txtDireccion.Text = "";
            cmbDistrito.SelectedIndex = 0;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            foreach (GridViewRow r in grdClientes.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    r.Attributes["onmouseover"] = "this.style.cursor='Pointer';"; //this.style.border = '3px solid white'; this.style.background = '#A9BCF5'; this.style.fontSize = '14px'; this.style.textDecoration ='underline';this.style.border = '2px solid black';
                    //r.Attributes["onmouseout"] = "this.style.backgroundColor='';";
                    r.ToolTip = "Click para mostrar detalles";
                    r.Attributes["onclick"] = this.Page.ClientScript.GetPostBackClientHyperlink(this.grdClientes, "Select$" + r.RowIndex, true);
                    //Page.ClientScript.RegisterForEventValidation(r.UniqueID, "Select$" + r.RowIndex);
                }
            }
            base.Render(writer);
        }

        protected void grdClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblinfo1.Text = "";
            mostrar_datos_desdeGrid();
            //btnRestabCuenta.Enabled = true;
            btnmodificar.Enabled = true;
        }
    }
}