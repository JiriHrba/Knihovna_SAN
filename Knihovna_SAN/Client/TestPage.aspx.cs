using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DatabaseLibrary;

namespace Knihovna_SAN.Client
{
    public partial class TestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            // Vlozeni nove kategorie
            // bez validatoru
            DatabaseLibrary.Category cat = new DatabaseLibrary.Category();

            cat.category_name = TextBox_categoryName.Text;
            cat.category_type = Convert.ToInt32(TextBox_categoryType.Text);

            new DatabaseLibrary.CategoryTable().InsertCategory(cat);

            Response.Redirect(Request.RawUrl);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            DatabaseLibrary.Action act = new DatabaseLibrary.Action();

            string actionDate = TextBox_actionDate.Text;
            string[] items = actionDate.Split('/');
            DateTime dateAction = new DateTime(Int32.Parse(items[2]), Int32.Parse(items[1]), Int32.Parse(items[0]));

            act.action_date = dateAction;
            act.action_capacity = Convert.ToInt32(TextBox_actionCapacity.Text);
            act.action_name = TextBox_actionName.Text;
            act.action_cost = Convert.ToInt32(TextBox_actionCost.Text);

            new DatabaseLibrary.ActionTable().Insert(act);

            Response.Redirect(Request.RawUrl);
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            DatabaseLibrary.Copy copy = new DatabaseLibrary.Copy();

            copy.copy_is_present = Convert.ToInt32(TextBox_copy_isPresent.Text);
            copy.book_id = Convert.ToInt32(ddl_copy_bookID.SelectedValue);

            new DatabaseLibrary.CopyTable().InsertCopy(copy);

            Response.Redirect(Request.RawUrl);
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            DatabaseLibrary.Reservation res = new DatabaseLibrary.Reservation();

            res.client_id = Convert.ToInt32(ddl_reser_client.SelectedValue);
            res.copy_id = Convert.ToInt32(ddl_reser_copy.SelectedValue);
            res.reservation_appeal = DateTime.Now;
            //res.reservation_date = DateTime.Now;

            new DatabaseLibrary.ReservationTable().InsertReservation(res);

            Response.Redirect(Request.RawUrl);
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            DatabaseLibrary.SanctionType stype = new DatabaseLibrary.SanctionType();

            stype.stype_name = "Typ sankce";
            stype.stype_ammount = 20;

            new DatabaseLibrary.SanctionTypeTable().InsertSanctionType(stype);

            Response.Redirect(Request.RawUrl);
        }
        protected void Button6_Click(object sender, EventArgs e)
        {
            DatabaseLibrary.SanctionsHistory sanHis = new DatabaseLibrary.SanctionsHistory();

            sanHis.client_id = 1;
            sanHis.sanction_desc = "popis sankce";
            sanHis.sanction_grant = DateTime.Now;
            sanHis.sanction_paid = DateTime.Now;
            sanHis.stype_id = 1;

            new DatabaseLibrary.SanctionsHistoryTable().InsertSanctionsHistory(sanHis);

            Response.Redirect(Request.RawUrl);
        }
}
}