using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Knihovna_SAN.App_Code;
using System.Web.Security;

namespace Knihovna_SAN.Account
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Prihlaseni klienta do systemu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                string username = TxtBoxLogin.Text;
                string password = Cryptography.CalculateHashMD5(TxtBoxPassword.Text);

                DatabaseLibrary.ClientTable cTable = new DatabaseLibrary.ClientTable();
                DatabaseLibrary.Client loggedClient = cTable.LoginClient(username, password);



                if (loggedClient != null)
                {
                    //List<string> roles = new List<string>();
                    //// Jeslize je jmeno admin, pak dame roli admin, jinak obycejny klient.
                    //if (loggedClient.client_login.CompareTo("admin") == 0)
                    //{
                    //    roles.Add("Admin");
                    //}
                    //else
                    //{
                    //    roles.Add("User");
                    //}
                  
                    // Jeste provede kontrolu, zda ma aktivni ucet. Pokud ne, zobrazime hlasku
                    if (loggedClient.client_is_active == false)
                        Master.FindControl("LabelMemberInfo").Visible = true;
                    else
                        Master.FindControl("LabelMemberInfo").Visible = false;


                    FormsAuthentication.RedirectFromLoginPage(loggedClient.client_name, true);
                    //Context.User = new System.Security.Principal.GenericPrincipal(Context.User.Identity, roles.ToArray());
                    Session["clientId"] = loggedClient.client_id;
                }
                else
                {
                    /* Zobrazeni chybove hlasky. */
                    LabelLoginInfo.ForeColor = System.Drawing.Color.Red;
                    LabelLoginInfo.Text = "Uzivatelske jmeno nebo heslo neni spravne.";                    
                }
            }
        }
    }
}