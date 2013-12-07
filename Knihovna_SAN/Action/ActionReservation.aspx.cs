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
            // Ziskani ID akce ze session a ID uzivatele ze session
            int actionId = (int)Session["actionId"];
            int clientId = (int)Session["clientId"];

            try
            {
                BusinessLogic.BusinessAction.ReservationToAction(actionId, clientId);
                LabelInfo.Text = "Rezervace probehla v poradku.";
                BtnActionReservation.Visible = false;
                BtnCancelReservation.Visible = true;
            }
            catch (BusinessLogic.ReservationException ex)
            {
                LabelInfo.Text = ex.Message;
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

            try
            {
                BusinessLogic.BusinessAction.CancelReservation(actionId, clientId);
                LabelInfo.Text = "Zruseni rezervace probehlo v poradku.";
                BtnActionReservation.Visible = true;
                BtnCancelReservation.Visible = false;
            }
            catch (BusinessLogic.ReservationException ex)
            {
                LabelInfo.Text = ex.Message;
            }

            
        }
    }
}
