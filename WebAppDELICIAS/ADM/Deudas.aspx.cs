using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using WebAppDELICIAS.App_Start;

namespace WebAppDELICIAS.ADM
{
    public partial class Deudas : System.Web.UI.Page
    {
        consultas datitos = new consultas();
        conexion cn = new conexion();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                RellenaDatosCMB();
            }
        }
        public void RellenaDatosCMB()
        {
            //cmbClientes.Items.Add("Seleccione un Cliente");
            DataTable dtCli = datitos.extrae("ListarClientesCMB");
            cmbClientes.DataSource = dtCli;
            cmbClientes.DataValueField = "IdPersona";
            cmbClientes.DataTextField = "ApelNombres";
            cmbClientes.DataBind();
            Session["Clientes"] = dtCli;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            lblInfoError.Text = "";
            buscarData();
            
        }

        public void buscarData()
        {
            grdDeudas.DataSource = null;
            grdDeudas.DataBind();
            int idCliente = int.Parse(cmbClientes.SelectedValue);
            grdDeudas.DataSource = datitos.extrae("BuscarDeudasXCliente", "@idcliente", idCliente);
            grdDeudas.DataBind();
            grdDeudas.Caption = "Deudas Encontradas";

        }

        protected void btnPagarDeuda_Click(object sender, EventArgs e)
        {
            if (txtMonto.Text != "")
            {
                lblInfoError.Text = "";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn.cadena;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PagarDeuda";
                cmd.Parameters.Add("@idClient", SqlDbType.Int).Value = cmbClientes.SelectedValue;
                cmd.Parameters.Add("@monto", SqlDbType.Decimal).Value = decimal.Parse(txtMonto.Text, new NumberFormatInfo() { NumberDecimalSeparator = "." });

                cn.conectar();
                DataTable dtRpta = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter();
                da1.SelectCommand = cmd;
                da1.Fill(dtRpta);
                cn.cerrar();
                if(dtRpta.Rows.Count > 0)
                    lblInfoError.Text = dtRpta.Rows[0][0].ToString();
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessAlert", "alert ('✓ La amortización fué registrada. Puede verificar las deudas del Cliente " + cmbClientes.SelectedItem + "');", true);

            }
        }

        protected void btnBuscarTotos_Click(object sender, EventArgs e)
        {
            lblInfoError.Text = "";
            cmbClientes.SelectedIndex = 0;
            btnPagarDeuda.Enabled = false;
            grdDeudas.DataSource = null;
            grdDeudas.DataBind();
            grdDeudas.DataSource = datitos.extrae("BuscarTodasLasDeudas");
            grdDeudas.DataBind();
            grdDeudas.Caption = "Todas las Deudas Existentes por Cliente";
            
        }

        protected void cmbClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbClientes.SelectedIndex == 0)
            {
                btnPagarDeuda.Enabled = false;
                grdDeudas.DataSource = null;
                grdDeudas.DataBind();
            }
                
            else{
                btnPagarDeuda.Enabled = true;
            }
               
        }
    }
}