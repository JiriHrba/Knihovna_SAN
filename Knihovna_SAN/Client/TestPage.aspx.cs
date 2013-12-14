using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DatabaseLibrary;
using System.Web.Security;
using Knihovna_SAN.App_Code;

namespace Knihovna_SAN.Client
{
    public partial class TestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session.Clear();
            
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
            //Vlozeni nove akce, bez validatoru

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
            //prida zaznam do kopie
            DatabaseLibrary.Copy copy = new DatabaseLibrary.Copy();

            copy.copy_is_present = Convert.ToInt32(TextBox_copy_isPresent.Text);
            copy.book_id = Convert.ToInt32(ddl_copy_bookID.SelectedValue);

            new DatabaseLibrary.CopyTable().InsertCopy(copy);

            Response.Redirect(Request.RawUrl);
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            
            DateTime reservation_appeal = Convert.ToDateTime(null);
            //prida zaznam do rezervace knihy
            DatabaseLibrary.Reservation res = new DatabaseLibrary.Reservation();
            
            res.client_id = Convert.ToInt32(ddl_reser_client.SelectedValue);
            res.book_id = Convert.ToInt32(ddl_reser_copy.SelectedValue);

            long copy_count = new DatabaseLibrary.CopyTable().SelectCount(res.book_id);
            long reser_count = new DatabaseLibrary.ReservationTable().SelectCount(res.book_id);

            if (copy_count > reser_count)
            {
                reservation_appeal = DateTime.Now;
            }
            
            res.reservation_date = DateTime.Now;

            res.reservation_appeal = reservation_appeal;
            
            new DatabaseLibrary.ReservationTable().InsertReservation(res);
            
            Response.Redirect(Request.RawUrl);
        }

        //prida rezervaci knihy z pohledu klienta
        protected void Button12_Click(object sender, EventArgs e)
        {
            
            int clientId = (int)Session["clientId"];
            DateTime reservation_appeal = Convert.ToDateTime(null);
            //prida zaznam do rezervace knihy
            DatabaseLibrary.Reservation res = new DatabaseLibrary.Reservation();

            res.client_id = clientId;
            res.book_id = Convert.ToInt32(ddl_reser_copy.SelectedValue);

            long copy_count = new DatabaseLibrary.CopyTable().SelectCount(res.book_id);
            long reser_count = new DatabaseLibrary.ReservationTable().SelectCount(res.book_id);

            if (copy_count > reser_count)
            {
                reservation_appeal = DateTime.Now;
            }
           
            res.reservation_date = DateTime.Now;
            res.reservation_appeal = reservation_appeal;

            new DatabaseLibrary.ReservationTable().InsertReservation(res);

            Response.Redirect(Request.RawUrl);

        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            //prida typ sankce
            DatabaseLibrary.SanctionType stype = new DatabaseLibrary.SanctionType();

            stype.stype_ammount = Convert.ToInt32(TextBox_stype_ammount.Text);
            stype.stype_name = TextBox_stype_name.Text;

            new DatabaseLibrary.SanctionTypeTable().InsertSanctionType(stype);

            Response.Redirect(Request.RawUrl);
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            //prida zaznam do sanction history
            DatabaseLibrary.SanctionsHistory sanHis = new DatabaseLibrary.SanctionsHistory();

            string sanction_grant = TextBox_sanction_grant.Text;
            string[] items = sanction_grant.Split('/');
            DateTime sanction_grant_toDB = new DateTime(Int32.Parse(items[2]), Int32.Parse(items[1]), Int32.Parse(items[0]));

            string sanction_paid = TextBox_sanction_paid.Text;
            string[] items2 = sanction_paid.Split('/');
            DateTime sanction_paid_toDB = new DateTime(Int32.Parse(items2[2]), Int32.Parse(items2[1]), Int32.Parse(items2[0]));


            sanHis.client_id = Convert.ToInt32(DropDownList_sanction_client_id.SelectedValue);
            sanHis.sanction_desc = TextBox_sanction_desc.Text;
            sanHis.sanction_grant = sanction_grant_toDB;
            sanHis.sanction_paid = sanction_paid_toDB;
            sanHis.stype_id = Convert.ToInt32(DropDownList_sanction_stype_id.SelectedValue); ;

            new DatabaseLibrary.SanctionsHistoryTable().InsertSanctionsHistory(sanHis);

            Response.Redirect(Request.RawUrl);
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            //prida do author
            DatabaseLibrary.Author aut = new DatabaseLibrary.Author();

            aut.author_name = TextBox_author_name.Text;
            aut.author_surname = TextBox_author_surname.Text;
            aut.author_middle_name = TextBox_author_middle_name.Text;

            string author_birthDate = TextBox_author_birth_date.Text;
            string[] items = author_birthDate.Split('/');
            DateTime birthDate_author = new DateTime(Int32.Parse(items[2]), Int32.Parse(items[1]), Int32.Parse(items[0]));

            aut.author_birth_date = birthDate_author;

            new DatabaseLibrary.AuthorTable().InsertAuthor(aut);

            Response.Redirect(Request.RawUrl);
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            //prida do Book
            DatabaseLibrary.Book book = new DatabaseLibrary.Book();

            book.book_name = TextBox_book_name.Text;
            book.book_isbn = TextBox_book_isbn.Text;
            book.book_annotation = TextBox_book_annotation.Text;
            book.author_id = Convert.ToInt32(DropDownList_book_author_id.SelectedValue);

            new DatabaseLibrary.BookTable().InsertBook(book);

            Response.Redirect(Request.RawUrl);
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            //prida do Borrowing
            DatabaseLibrary.Borrowing bor = new DatabaseLibrary.Borrowing();

            string borrowing_from = TextBox_borrowing_from.Text;
            string[] items = borrowing_from.Split('/');
            DateTime borrowing_from_toDB = new DateTime(Int32.Parse(items[2]), Int32.Parse(items[1]), Int32.Parse(items[0]));

            string borrowing_to = TextBox_borrowing_to.Text;
            string[] items2 = borrowing_to.Split('/');
            DateTime borrowing_to_toDB = new DateTime(Int32.Parse(items2[2]), Int32.Parse(items2[1]), Int32.Parse(items2[0]));

            bor.borrowing_from = borrowing_from_toDB;
            bor.borrowing_to = borrowing_to_toDB;
            bor.borrowing_is_returned = Convert.ToBoolean(TextBox_borrowing_is_returned.Text);
            bor.client_id = Convert.ToInt32(DropDownList_borrowing_client_id.SelectedValue);
            bor.copy_id = Convert.ToInt32(DropDownList_borrowing_copy_id.SelectedValue);

            new DatabaseLibrary.BorrowingTable().Insert(bor);

            Response.Redirect(Request.RawUrl);
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            //prida do BookCategory
            DatabaseLibrary.BookCategory bookCat = new DatabaseLibrary.BookCategory();

            bookCat.book_id = Convert.ToInt32(DropDownList_bookCatogery_book_id.SelectedValue);
            bookCat.category_id = Convert.ToInt32(DropDownList_bookCatogery_category_id.SelectedValue);

            new DatabaseLibrary.BookCategoryTable().InsertBookCategory(bookCat);

            Response.Redirect(Request.RawUrl);
        }

        protected void Button11_Click(object sender, EventArgs e)
        {
            //prida do ActionCategory
            DatabaseLibrary.ActionCategory actCat = new DatabaseLibrary.ActionCategory();

            actCat.action_id = Convert.ToInt32(DropDownList_actionCategory_action_id.SelectedValue);
            actCat.category_id = Convert.ToInt32(DropDownList_actionCategory_category_id.SelectedValue);

            new DatabaseLibrary.ActionCategoryTable().InsertActionCategory(actCat);

            Response.Redirect(Request.RawUrl);
        }
        
}
}