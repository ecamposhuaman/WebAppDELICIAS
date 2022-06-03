using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppDELICIAS.App_Start;

namespace WebAppDELICIAS.ADM
{
    public partial class Productos : System.Web.UI.Page
    {
        consultas datitos = new consultas();
        conexion cn = new conexion();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["idprod"] = 0;
                lblProducto.Text = "";
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
            grdProductos.DataSource = datitos.extrae("ListarProductos_GRD", "@nombre", txtbuscar.Text.ToString());
            grdProductos.DataBind();
            grdProductos.Visible = true;
            btnguardar.Enabled = false;
            deshabilitar(false);
            btnmodificar.Enabled = false;
            //btnRestabCuenta.Enabled = false;
        }

        public void mostrar_datos_desdeGrid()
        {
            GridViewRow row = grdProductos.SelectedRow;
            int fila = Convert.ToInt32(grdProductos.DataKeys[row.RowIndex].Value);
            Session["idprod"] = fila;
            DataTable tabla = new DataTable();
            tabla = datitos.extrae("BuscarProductos_xId", "@idproducto", fila);
            lblProducto.Text = tabla.Rows[0][0].ToString();
            txtNombreProd.Text = tabla.Rows[0][1].ToString();
            txtDescripcion.Text = tabla.Rows[0][2].ToString();
            cmbCategProd.SelectedValue = tabla.Rows[0][3].ToString();
            txtStockMinimo.Text = tabla.Rows[0][4].ToString();
            txtPrecio.Text = tabla.Rows[0][5].ToString().Replace(",", ".");
        }

        public void deshabilitar(Boolean estado)
        {
            txtNombreProd.Enabled = estado;
            txtDescripcion.Enabled = estado;
            cmbCategProd.Enabled = estado;
            txtStockMinimo.Enabled = estado;
            txtPrecio.Enabled = estado;
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

        public void modificarCliente(int idpr)
        {
            //Boolean estado;
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Actualizar_Producto";
            cmd.Connection = cn.cadena;

            cmd.Parameters.Add("@idproducto", SqlDbType.Int).Value = idpr;
            cmd.Parameters.Add("@nomProd", SqlDbType.VarChar).Value = txtNombreProd.Text;
            cmd.Parameters.Add("@descrProd", SqlDbType.VarChar).Value = txtDescripcion.Text;
            cmd.Parameters.Add("@idCat", SqlDbType.Int).Value = int.Parse(cmbCategProd.SelectedValue);
            cmd.Parameters.Add("@stockMin", SqlDbType.Int).Value = int.Parse(txtStockMinimo.Text);
            cmd.Parameters.Add("@precio", SqlDbType.Decimal).Value = decimal.Parse(txtPrecio.Text, new NumberFormatInfo() { NumberDecimalSeparator = "." });
            
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        protected void btnguardar_Click(object sender, EventArgs e)
        {
            int idp = (int)Session["idprod"];
            if (lbloperacion.Text == "m")
            {
                modificarCliente(idp);
                lblinfo1.Text = "✓ Producto Modificado.";
                //Response.Write("<script>alert('Usuario Modificado');</script>");
                deshabilitar(false);
            }
            else
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn.cadena;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "InsertarProducto";
                cmd.Parameters.Add("@nomProd", SqlDbType.VarChar).Value = txtNombreProd.Text;
                cmd.Parameters.Add("@descrProd", SqlDbType.VarChar).Value = txtDescripcion.Text;
                cmd.Parameters.Add("@idCat", SqlDbType.Int).Value = int.Parse(cmbCategProd.SelectedValue);
                cmd.Parameters.Add("@stockMin", SqlDbType.Int).Value = int.Parse(txtStockMinimo.Text);
                cmd.Parameters.Add("@precio", SqlDbType.Decimal).Value = decimal.Parse(txtPrecio.Text, new NumberFormatInfo() { NumberDecimalSeparator = "." });

                cn.conectar();
                cmd.ExecuteNonQuery();
                cn.cerrar();
                lblinfo1.Text = "✓ Nuevo Producto Registrado.";
                //Response.Write("<script>alert('Usuario Nuevo Registrado');</script>");
            }
            //btnRestabCuenta.Enabled = true;
        }

        protected void btnnuevo_Click(object sender, EventArgs e)
        {
            lblinfo1.Text = "";
            clean_campos();
            lblProducto.Text = "";
            btnguardar.Enabled = true;
            lbloperacion.Text = "n";
            deshabilitar(true);
            grdProductos.Visible = false;
            btnmodificar.Enabled = false;
            //btnRestabCuenta.Enabled = false;
        }

        public void clean_campos()
        {
            lblProducto.Text = "";
            txtNombreProd.Text = "";
            txtDescripcion.Text = "";
            txtStockMinimo.Text = "";
            txtPrecio.Text = "";
            cmbCategProd.SelectedIndex = 0;
            
        }

        protected override void Render(HtmlTextWriter writer)
        {
            foreach (GridViewRow r in grdProductos.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    r.Attributes["onmouseover"] = "this.style.cursor='Pointer';"; //this.style.border = '3px solid white'; this.style.background = '#A9BCF5'; this.style.fontSize = '14px'; this.style.textDecoration ='underline';this.style.border = '2px solid black';
                    //r.Attributes["onmouseout"] = "this.style.backgroundColor='';";
                    r.ToolTip = "Click para mostrar detalles";
                    r.Attributes["onclick"] = this.Page.ClientScript.GetPostBackClientHyperlink(this.grdProductos, "Select$" + r.RowIndex, true);
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