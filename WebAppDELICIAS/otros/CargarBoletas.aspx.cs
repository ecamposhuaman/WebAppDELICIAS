using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppDELICIAS.App_Start;

namespace WebAppDELICIAS
{
    public partial class CargarBoletas : System.Web.UI.Page
    {
        consultas datitos = new consultas();
        conexion cn = new conexion();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                buscar();
            }
            
                
        }
        public void buscar()
        {
            DataTable dtusu = new DataTable();
            dtusu = datitos.extrae("ObtenerIdPersona", "@email", Context.User.Identity.GetUserName());
            string idusuario = dtusu.Rows[0][0].ToString();

            grdBoletas.DataSource = datitos.extrae("ListarBoletasTodas", "@idpersona", Convert.ToInt32(idusuario));
            grdBoletas.DataBind();
            grdBoletas.Visible = true;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            foreach (GridViewRow r in grdBoletas.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    r.Attributes["onmouseover"] = "this.style.cursor='Pointer';"; //this.style.border = '3px solid white'; this.style.background = '#A9BCF5'; this.style.fontSize = '14px'; this.style.textDecoration ='underline';this.style.border = '2px solid black';
                    //r.Attributes["onmouseout"] = "this.style.backgroundColor='';";
                    r.ToolTip = "Click para mostrar detalles";
                    r.Attributes["onclick"] = this.Page.ClientScript.GetPostBackClientHyperlink(this.grdBoletas, "Select$" + r.RowIndex, true);
                    //Page.ClientScript.RegisterForEventValidation(r.UniqueID, "Select$" + r.RowIndex);
                }
            }
            base.Render(writer);
        }


        protected void grdBoletas_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdBoletas.SelectedRow.Font.Bold = true; // Color.FromName("#24cb5f");
            //mostrar_datos_desdeGrid();
            GridViewRow row = grdBoletas.SelectedRow;
            int idtrans = Convert.ToInt32(grdBoletas.DataKeys[row.RowIndex].Value);
            txtIdBoleta.Text = row.Cells[0].Text;
            cmbNombre.SelectedValue = row.Cells[3].Text;

            string script = @"<script type='text/javascript'>AbrirModalActElim();</script>";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "AbrirModalActElim", script, false);
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.Connection = cn.cadena;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Actualizar_Boletas";

            cmd.Parameters.Add("@idboleta", SqlDbType.Int).Value = Convert.ToInt32(txtIdBoleta.Text);
            cmd.Parameters.Add("@idpersona", SqlDbType.Int).Value = Convert.ToInt32(cmbNombre.SelectedValue);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();

            buscar();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.Connection = cn.cadena;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Eliminar_Boletas";

            cmd.Parameters.Add("@idboleta", SqlDbType.Int).Value = Convert.ToInt32(txtIdBoleta.Text);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();

            buscar();
        }



        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            string script = @"<script type='text/javascript'>AbrirModalNuevo();</script>";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "AbrirModalNuevo", script, false);
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            //Obtener datos de Imagen
            string nombreOrig = Path.GetFileName(fupArchivo.FileName);
            string exten = Path.GetExtension(fupArchivo.FileName);
            string ubicacion = Server.MapPath("~/Boletas/"); //Ubicacion de las Imágenes
            string Ubic_NombreArch = "";
            string URLPDF = "";
            //DataTable dtinfo = new DataTable();

            if (fupArchivo.PostedFile.ContentLength < 8388608)
            {
                try
                {
                    if (fupArchivo.HasFile)
                    {
                        try
                        {
                            if (fupArchivo.PostedFile.ContentType == "application/pdf")
                            {
                                try
                                {
                                    //Generando nombre identificador
                                    Ubic_NombreArch = ubicacion + nombreOrig + exten;
                                    URLPDF = ubicacion + nombreOrig + exten;
                                    fupArchivo.SaveAs(Ubic_NombreArch); //Guardando imagen

                                    #region Enviando a BD
                                    SqlCommand cmd = new SqlCommand();
                                    cmd.Connection = cn.cadena;
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "Registrar_Boleta";
                                    cmd.Parameters.Add("@idpersona", SqlDbType.Int).Value = Convert.ToInt32(cmbNombre.SelectedValue);
                                    cmd.Parameters.Add("@nombreboleta", SqlDbType.VarChar).Value = nombreOrig;
                                    cmd.Parameters.Add("@urlboleta", SqlDbType.VarChar).Value = URLPDF;

                                    cn.conectar();
                                    cmd.ExecuteNonQuery();
                                    cn.cerrar();
                                    #endregion



                                    Response.Write("<script>alert('✓ Boleta registrada satisfactoriamente.');</script>");
                                }
                                catch (Exception ex)
                                {
                                    StatusLabel.Text = "Ocurrió el siguiente error 03: " + ex.ToString();
                                }
                            }
                            else
                            {
                                StatusLabel.Text = "Formato no permitido";
                            }
                        }
                        catch (Exception ex)
                        {
                            StatusLabel.Text = "El archivo no puede ser cargado. Ocurrió el siguiente error 02: " + ex.Message;
                        }
                    }
                }
                catch (Exception ex)
                {
                    StatusLabel.Text = "El archivo no puede ser cargado. Ocurrió el siguiente error 01: " + ex.Message;
                }
            }
            else
            {
                StatusLabel.Text = "No está permitido cargar imágenes de más de 8 MB";
            }
        }

        protected void btnMisBoletas_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestionBoletas.aspx");
        }
    }
}