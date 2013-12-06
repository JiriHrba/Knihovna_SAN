using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Knihovna_SAN.Action
{
    public partial class ActionReservation : System.Web.UI.Page
    {

        private DatabaseLibrary.ActionReservationTable actResTable;

        protected void Page_Load(object sender, EventArgs e)
        {
            actResTable = new DatabaseLibrary.ActionReservationTable();

            if (Session["actionId"] == null)
            {
                Response.Redirect("Default.aspx");
            }

            // Jestli je jiz klient registrovan, zobrazime info a schovame tlacitko pro rezervaci a zobrazime tlacitko pro zruseni rezervace.
            int actionId = (int)Session["actionId"];
            int clientId = (int)Session["clientId"];
            bool isReg = actResTable.IsUserRegisteredToAction(actionId, clientId);
            if (isReg)
            {
                LabelInfo.Text = "Tuto akci jste jiz rezervoval.";
                BtnActionReservation.Visible = false;
                BtnCancelReservation.Visible = true;
            }
            else
            {
                BtnCancelReservation.Visible = false;
            }


        }

        /// <summary>
        /// Rezervace na akci.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnActionReservation_Click(object sender, EventArgs e)
        {
            // Ziskani ID akce ze session
            int actionId = (int)Session["actionId"];

            // Nacteni akce z databaze (asi to neni nutne, lze predat skrze Session, ale nechce se  mi to delat, tohle je rychlejsi :))
            DatabaseLibrary.Action action = new DatabaseLibrary.ActionTable().SelectOne(actionId);

            //DatabaseLibrary.ActionReservationTable actResTable = new DatabaseLibrary.ActionReservationTable();

            // Overeni, zda je jeste volne misto    
            int numOfRegClients = actResTable.SelectCountOfRegUser(actionId);

            // Jestlize pocet registrovanych uzivatelu je mensi nez kapacita akce, muzeme provest registraci pokudd jiz klient neni registrovan
            if (numOfRegClients < action.action_capacity)
            {
                int clientId = (int)Session["clientId"];

                // Zjisteni, zda jiz neni registrovan
                bool isReg = actResTable.IsUserRegisteredToAction(actionId, clientId);

                if (!isReg)
                {

                    DatabaseLibrary.ActionReservation actRes = new DatabaseLibrary.ActionReservation(actionId, clientId);

                    // Provedeme rezervaci
                    actResTable.InsertActionReservation(actRes);

                    // Zobrazime hlasku, ze rezervace probehla v poradku                
                    LabelInfo.Text = "Rezervace probehla uspesne.";
                    BtnActionReservation.Visible = false;
                }
                else
                {
                    LabelInfo.Text = "Tuto akci jste jiz rezervoval.";
                }
            }
            else
            {
                // Zobrazime chybovou hlasku
                LabelInfo.Text = "Akce je jiz zaplnena.";
            }

        }

        /// <summary>
        /// Zruseni rezervace na akci.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnCancelReservation_Click(object sender, EventArgs e)
        {
            // Ziskani ID akce ze session
            int actionId = (int)Session["actionId"];

            // Ziskani ID clienta
            int clientId = (int)Session["clientId"];

            // Zruseni rezervace
            int res = actResTable.CancelClientReservation(actionId, clientId);

            if (res == 1)
            {
                LabelInfo.Text = "Zruseni rezervace probehlo v poradku.";
                BtnCancelReservation.Visible = false;
                BtnActionReservation.Visible = true;
            }
            else
            {
                LabelInfo.Text = "Zruseni rezervace se nezdarilo.";
            }


        }

    }
}
