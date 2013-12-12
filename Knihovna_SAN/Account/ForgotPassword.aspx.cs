using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Knihovna_SAN.Account
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnSendNewPass_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                string email = TxtBoxRegEmail.Text;

                try
                {
                    BusinessLogic.BusinessClient.NewPasswordForClient(email);
                    // Pokud to proslo, zobrazime info, ze OK
                    LabelInfo.Text = "Nove heslo Vam bylo zaslano na email.";
                }
                catch (BusinessLogic.ForgotPasswordException ex)
                {
                    // Zmena hesla se nezdarila.
                    LabelInfo.Text = ex.Message;
                }
            }
        }
    }
}