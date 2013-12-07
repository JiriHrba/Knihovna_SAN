using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DatabaseLibrary;
using System.Security.Cryptography;
using Knihovna_SAN;
using Knihovna_SAN.App_Code;
using BusinessLogic;

namespace Knihovna_SAN.Client
{
    public partial class NewClient : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnInsertClient_Click(object sender, EventArgs e)
        {           
            if (IsValid)
            {
                DatabaseLibrary.Client client = new DatabaseLibrary.Client();

                /*
                 * Precteni udaju z registracniho formulare
                 */
                client.client_name = TxtBoxName.Text;
                client.client_surname = TxtBoxSurname.Text;
                client.client_email = TxtBoxEmail.Text;
                client.client_phone = TxtBoxPhone.Text;

                // Parsovani data narozeni
                string birthDate = TxtBoxBirthDate.Text;
                string[] items = birthDate.Split('/');
                DateTime dateBirth = new DateTime(Int32.Parse(items[2]), Int32.Parse(items[1]), Int32.Parse(items[0]));
                client.client_birth_date = dateBirth;

                client.client_street = TxtBoxStreet.Text;
                client.client_city = TxtBoxCity.Text;
                client.client_zip = TxtBoxZIP.Text;
                client.client_country = TxtBoxCountry.Text;
               
                client.client_login = TxtBoxLogin.Text;

                /*
                 * Registrace klienta podle UC1: (vice info v komentari k metode BusinessClient.RegisterClient(Client client))
                 */
                try
                {
                    BusinessClient.RegisterClient(client);
                    LabelInfo.Text = "Registrace uspesna. Heslo Vam bylo poslano emailem.";
                }
                catch (InputDataException ex)
                {
                    // Email nebo login jiz v systemu existuje
                    LabelInfo.Text = ex.Message;
                }
                catch (Exception ex)
                {
                    LabelInfo.Text = "Behem registrace doslo k chybe.";
                }                                
            }
        }

       

        

    }
}