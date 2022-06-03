using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using WebAppDELICIAS.App_Start;
using WebAppDELICIAS.Models;

namespace WebAppDELICIAS.Account
{
    public partial class Register : Page
    {
        consultas datitos = new consultas();
        conexion cn = new conexion();

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            DataTable tbUsr = new DataTable();
            tbUsr.Rows.Clear();
            tbUsr = datitos.extrae("VerificarEmail", "@email", Email.Text);
            int cantUsuarios = tbUsr.Rows.Count; //Contiene: IdRol
            if (cantUsuarios == 1)
            {
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
                var user = new ApplicationUser() { UserName = Email.Text, Email = Email.Text };
                IdentityResult result = manager.Create(user, Password.Text);
                if (result.Succeeded)
                {
                    #region Elaborando Rol de Usuario 
                    DataTable tbID = new DataTable();
                    tbID.Rows.Clear();
                    tbID = datitos.extrae("consulta_IdAleatorio_Usuario", "@email", Email.Text);

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn.cadena;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ActualizarRolUsuario";
                    cmd.Parameters.Add("@idrol", SqlDbType.NVarChar).Value = tbUsr.Rows[0][0].ToString();
                    cmd.Parameters.Add("@iduser", SqlDbType.NVarChar).Value = tbID.Rows[0][0].ToString();
                    cn.conectar();
                    cmd.ExecuteNonQuery();
                    cn.cerrar();
                    #endregion
                    // Para obtener más información sobre cómo habilitar la confirmación de cuentas y el restablecimiento de contraseña, visite https://go.microsoft.com/fwlink/?LinkID=320771
                    //string code = manager.GenerateEmailConfirmationToken(user.Id);
                    //string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                    //manager.SendEmail(user.Id, "Confirmar cuenta", "Para confirmar la cuenta, haga clic <a href=\"" + callbackUrl + "\">aquí</a>.");

                    signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                }
                else
                {
                    ErrorMessage.Text = result.Errors.FirstOrDefault();
                }
            }
            else 
            {
                if (cantUsuarios > 1)
                    lblinfo.Text = "Existen un serio problema de Identidad, el Sistema registra 2 o más usuarios con este Correo Electrónico.";
                else
                    lblinfo.Text = "El Correo Electrónico insertado no se encuentra autorizado para operar en la Aplicación Web.";
            }
        }
    }
}