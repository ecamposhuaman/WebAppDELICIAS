using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppDELICIAS.App_Start;

namespace WebAppDELICIAS.ADM
{
    public partial class Empleados : System.Web.UI.Page
    {
        consultas datitos = new consultas();
        conexion cn = new conexion();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["idemp"] = 0;
                lblEmpleado.Text = "";
                deshabilitar(false);
                btnmodificar.Enabled = false;
                //btnRestabCuenta.Enabled = false;
                lblinfo1.Text = "";
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
            grdEmpleados.DataSource = datitos.extrae("ListarEmpleados_GRD", "@nombre", txtbuscar.Text.ToString());
            grdEmpleados.DataBind();
            grdEmpleados.Visible = true;
            btnguardar.Enabled = false;
            deshabilitar(false);
            btnmodificar.Enabled = false;
            //btnRestabCuenta.Enabled = false;
        }

        public void mostrar_datos_desdeGrid()
        {
            GridViewRow row = grdEmpleados.SelectedRow;
            int fila = Convert.ToInt32(grdEmpleados.DataKeys[row.RowIndex].Value);
            Session["idemp"] = fila;
            DataTable tabla = new DataTable();
            tabla = datitos.extrae("buscarEmpleados_xId", "@idemp", fila);
            lblEmpleado.Text = tabla.Rows[0][0].ToString();
            txtDNI.Text = tabla.Rows[0][1].ToString();
            txtApNombres.Text = tabla.Rows[0][2].ToString();
            txtEmail.Text = tabla.Rows[0][3].ToString();
            txtTelefono.Text = tabla.Rows[0][4].ToString();
            txtCargo.Text = tabla.Rows[0][5].ToString();
            txtLimiteVenta.Text = tabla.Rows[0][6].ToString().Replace(",", ".");
            cmbRol.SelectedValue = tabla.Rows[0][7].ToString();
        }

        public void deshabilitar(Boolean estado)
        {
            txtDNI.Enabled = estado;
            txtApNombres.Enabled = estado;
            txtEmail.Enabled = estado;
            txtTelefono.Enabled = estado;
            txtCargo.Enabled = estado;
            txtLimiteVenta.Enabled = estado;
            cmbRol.Enabled = estado;
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
            decimal limiteVenta = 0;
            if (txtLimiteVenta.Text != "")
            {
                limiteVenta = decimal.Parse(txtLimiteVenta.Text, new NumberFormatInfo() { NumberDecimalSeparator = "." });
            }
            //Boolean estado;
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Actualizar_Empleado";
            cmd.Connection = cn.cadena;

            cmd.Parameters.Add("@idemp", SqlDbType.Int).Value = idCliente;
            cmd.Parameters.Add("@dni", SqlDbType.VarChar).Value = txtDNI.Text;
            cmd.Parameters.Add("@apelnom", SqlDbType.VarChar).Value = txtApNombres.Text;
            cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = txtEmail.Text;
            cmd.Parameters.Add("@telef", SqlDbType.VarChar).Value = txtTelefono.Text;
            cmd.Parameters.Add("@cargo", SqlDbType.VarChar).Value = txtCargo.Text;
            cmd.Parameters.Add("@racion", SqlDbType.Decimal).Value = limiteVenta;
            cmd.Parameters.Add("@idrol", SqlDbType.NVarChar).Value = cmbRol.SelectedValue;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        protected void btnguardar_Click(object sender, EventArgs e)
        {
            decimal limiteVenta = 0;
            if (txtLimiteVenta.Text != "")
            {
                limiteVenta = decimal.Parse(txtLimiteVenta.Text, new NumberFormatInfo() { NumberDecimalSeparator = "." });
            }

            int idp = (int)Session["idemp"];
            if (lbloperacion.Text == "m")
            {
                modificarCliente(idp);
                lblinfo1.Text = "✓ Empledo Modificado.";
                //Response.Write("<script>alert('Usuario Modificado');</script>");
                deshabilitar(false);
            }
            else
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn.cadena;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "InsertarEmpleado";
                cmd.Parameters.Add("@dni", SqlDbType.VarChar).Value = txtDNI.Text;
                cmd.Parameters.Add("@apelnom", SqlDbType.VarChar).Value = txtApNombres.Text;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = txtEmail.Text;
                cmd.Parameters.Add("@telef", SqlDbType.VarChar).Value = txtTelefono.Text;
                cmd.Parameters.Add("@cargo", SqlDbType.VarChar).Value = txtCargo.Text;
                cmd.Parameters.Add("@racion", SqlDbType.Decimal).Value = limiteVenta;
                cmd.Parameters.Add("@idrol", SqlDbType.NVarChar).Value = cmbRol.SelectedValue;

                cn.conectar();
                cmd.ExecuteNonQuery();
                cn.cerrar();
                lblinfo1.Text = "✓ Nuevo Empleado Registrado.";
                //Response.Write("<script>alert('Usuario Nuevo Registrado');</script>");
            }
            //btnRestabCuenta.Enabled = true;
        }

        protected void btnnuevo_Click(object sender, EventArgs e)
        {
            lblinfo1.Text = "";
            clean_campos();
            lblEmpleado.Text = "";
            btnguardar.Enabled = true;
            lbloperacion.Text = "n";
            deshabilitar(true);
            grdEmpleados.Visible = false;
            btnmodificar.Enabled = false;
            //btnRestabCuenta.Enabled = false;
        }

        public void clean_campos()
        {
            lblEmpleado.Text = "";
            txtDNI.Text = "";
            txtApNombres.Text = "";
            txtEmail.Text = "";
            txtTelefono.Text = "";
            txtCargo.Text = "";
            txtLimiteVenta.Text = "";
            cmbRol.SelectedValue = "nnn";
        }

        protected override void Render(HtmlTextWriter writer)
        {
            foreach (GridViewRow r in grdEmpleados.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    r.Attributes["onmouseover"] = "this.style.cursor='Pointer';"; //this.style.border = '3px solid white'; this.style.background = '#A9BCF5'; this.style.fontSize = '14px'; this.style.textDecoration ='underline';this.style.border = '2px solid black';
                    //r.Attributes["onmouseout"] = "this.style.backgroundColor='';";
                    r.ToolTip = "Click para mostrar detalles";
                    r.Attributes["onclick"] = this.Page.ClientScript.GetPostBackClientHyperlink(this.grdEmpleados, "Select$" + r.RowIndex, true);
                    //Page.ClientScript.RegisterForEventValidation(r.UniqueID, "Select$" + r.RowIndex);
                }
            }
            base.Render(writer);
        }

        protected void grdEmpleados_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblinfo1.Text = "";
            mostrar_datos_desdeGrid();
            //btnRestabCuenta.Enabled = true;
            btnmodificar.Enabled = true;
        }
    }
}