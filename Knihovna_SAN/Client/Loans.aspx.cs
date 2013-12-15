using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Knihovna_SAN.Client
{
    public partial class Loans : System.Web.UI.Page
    {


        private bool reserved = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();

            
            

            if (!IsPostBack)
            {
                DateTime now = DateTime.Now;
                DateTime monthAhead = now.AddMonths(1);
                String borrowing_from = now.Day + "/" + now.Month + "/" + now.Year;
                String borrowing_to = monthAhead.Day + "/" + monthAhead.Month + "/" + monthAhead.Year;
                TextBox_borrowing_from.Text = borrowing_from;
                TextBox_borrowing_to.Text = borrowing_to;

            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            System.Diagnostics.Debug.WriteLine(dropDownList_book_id.SelectedValue);
            if (IsPostBack)
            {
                //reserved = IsAllReserved();
                if (reserved)
                {
                    Button2.Enabled = true;
                }

            }
            


        }
        
        protected void Button1_Click(object sender, EventArgs e)
        {
            //prida do Borrowing + update copy
            
            try
            {
                DatabaseLibrary.Borrowing bor = new DatabaseLibrary.Borrowing();



                string borrowing_from = TextBox_borrowing_from.Text;
                string[] items = borrowing_from.Split('/');
                DateTime borrowing_from_toDB = new DateTime(Int32.Parse(items[2]), Int32.Parse(items[1]), Int32.Parse(items[0]));

                string borrowing_to = TextBox_borrowing_to.Text;
                string[] items2 = borrowing_to.Split('/');
                DateTime borrowing_to_toDB = new DateTime(Int32.Parse(items2[2]), Int32.Parse(items2[1]), Int32.Parse(items2[0]));

                bor.borrowing_from = borrowing_from_toDB;
                bor.borrowing_to = borrowing_to_toDB;
                bor.borrowing_is_returned = false;
                bor.client_id = Convert.ToInt32(DropDownList_borrowing_client_id.SelectedValue);
                bor.copy_id = Convert.ToInt32(DropDownList_borrowing_copy_id.SelectedValue);

                new DatabaseLibrary.BorrowingTable().Insert(bor);

                DatabaseLibrary.CopyTable copyTable = new DatabaseLibrary.CopyTable();
                DatabaseLibrary.Copy copy = copyTable.SelectOne(bor.copy_id);
                copy.copy_is_present = 0;
                copyTable.Update(copy);



                //z neznameho duvodu nejde
                Label1.Text = "Výpůjčka byla zapsána";
                Console.WriteLine("oukej");
                
               



            }
            catch (Exception ex)
            {
                Label1.Text = "chybka";
                Console.WriteLine("chybka");
            }




            Response.Redirect(Request.RawUrl);
            
        }
        
        //podle vyberu knizky se v dalsim dropdownlistu zobrazi id vytisku
        protected void BookSelected(object sender, EventArgs e)
        {
            DropDownList_borrowing_copy_id.DataBind();
            
            

        }


        //zobrazi vypujcky podle zvolenych kriterii
        protected void Button_find_by_clientID_Click(object sender, EventArgs e)
        {
            
            GridView1.DataBind();
            GridView1.Visible = true;

        }
        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        protected bool IsAllReserved()
        {
            List<DatabaseLibrary.Copy> copies = new DatabaseLibrary.CopyTable().SelectAllPresentByBook((Convert.ToInt32(dropDownList_book_id.SelectedValue)));
            int count = copies.Count();
            if (count == 0)
            {
                return true;
            }
            else return false;
        }
}
}