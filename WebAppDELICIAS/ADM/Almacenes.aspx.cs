using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppDELICIAS.App_Start;

namespace WebAppDELICIAS
{
    public partial class Almacenes : System.Web.UI.Page
    {
        consultas datitos = new consultas();
        conexion cn = new conexion();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["idalm"] = 0;
                lblIdAlmacen.Text = "";
                deshabilitar(false);
                btnmodificar.Enabled = false;
                //btnRestabCuenta.Enabled = false;
                lblinfo1.Text = "";
                datitos.rellenacombo(cmbResponsable, "ApelNombres", "IdPersona", "ListarEmpleadosCMB");
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
            grdAlmacenes.DataSource = datitos.extrae("ListarAlmacenes_GRD", "@nombre", txtbuscar.Text.ToString());
            grdAlmacenes.DataBind();
            grdAlmacenes.Visible = true;
            btnguardar.Enabled = false;
            deshabilitar(false);
            btnmodificar.Enabled = false;
            //btnRestabCuenta.Enabled = false;
        }

        public void mostrar_datos_desdeGrid()
        {
            GridViewRow row = grdAlmacenes.SelectedRow;
            int fila = Convert.ToInt32(grdAlmacenes.DataKeys[row.RowIndex].Value);
            Session["idalm"] = fila;
            DataTable tabla = new DataTable();
            tabla = datitos.extrae("buscaAlmacenes_xId", "@idalmacen", fila);
            lblIdAlmacen.Text = tabla.Rows[0][0].ToString();
            txtNombreAlm.Text = tabla.Rows[0][1].ToString();
            cmbTipo.SelectedValue = tabla.Rows[0][2].ToString();
            cmbResponsable.SelectedValue = tabla.Rows[0][3].ToString();
        }

        public void deshabilitar(Boolean estado)
        {
            txtNombreAlm.Enabled = estado;
            cmbResponsable.Enabled = estado;
            cmbTipo.Enabled = estado;
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

        public void modificarCliente(int idalm)
        {
            //Boolean estado;
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Actualizar_Almacen";
            cmd.Connection = cn.cadena;

            cmd.Parameters.Add("@idalmacen", SqlDbType.Int).Value = idalm;
            cmd.Parameters.Add("@nomAlm", SqlDbType.VarChar).Value = txtNombreAlm.Text;
            cmd.Parameters.Add("@tipo", SqlDbType.VarChar).Value = cmbTipo.SelectedValue;
            cmd.Parameters.Add("@idResponsable", SqlDbType.Int).Value = int.Parse(cmbResponsable.SelectedValue);

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        protected void btnguardar_Click(object sender, EventArgs e)
        {
            int idp = (int)Session["idalm"];
            if (lbloperacion.Text == "m")
            {
                modificarCliente(idp);
                lblinfo1.Text = "✓ Almacén Modificado.";
                //Response.Write("<script>alert('Usuario Modificado');</script>");
                deshabilitar(false);
            }
            else
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn.cadena;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "InsertarAlmacen";
                cmd.Parameters.Add("@nomAlm", SqlDbType.VarChar).Value = txtNombreAlm.Text;
                cmd.Parameters.Add("@tipo", SqlDbType.VarChar).Value = cmbTipo.SelectedValue;
                cmd.Parameters.Add("@idResponsable", SqlDbType.Int).Value = int.Parse(cmbResponsable.SelectedValue);

                cn.conectar();
                cmd.ExecuteNonQuery();
                cn.cerrar();
                lblinfo1.Text = "✓ Nuevo Almacén Registrado.";
                //Response.Write("<script>alert('Usuario Nuevo Registrado');</script>");
            }
            //btnRestabCuenta.Enabled = true;
        }

        protected void btnnuevo_Click(object sender, EventArgs e)
        {
            lblinfo1.Text = "";
            clean_campos();
            lblIdAlmacen.Text = "";
            btnguardar.Enabled = true;
            lbloperacion.Text = "n";
            deshabilitar(true);
            grdAlmacenes.Visible = false;
            btnmodificar.Enabled = false;
            //btnRestabCuenta.Enabled = false;
        }

        public void clean_campos()
        {
            lblIdAlmacen.Text = "";
            txtNombreAlm.Text = "";
            cmbTipo.SelectedIndex = 0;
            cmbResponsable.SelectedIndex = 0;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            foreach (GridViewRow r in grdAlmacenes.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    r.Attributes["onmouseover"] = "this.style.cursor='Pointer';"; //this.style.border = '3px solid white'; this.style.background = '#A9BCF5'; this.style.fontSize = '14px'; this.style.textDecoration ='underline';this.style.border = '2px solid black';
                    //r.Attributes["onmouseout"] = "this.style.backgroundColor='';";
                    r.ToolTip = "Click para mostrar detalles";
                    r.Attributes["onclick"] = this.Page.ClientScript.GetPostBackClientHyperlink(this.grdAlmacenes, "Select$" + r.RowIndex, true);
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