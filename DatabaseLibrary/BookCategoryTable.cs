using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Configuration;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web;

namespace DatabaseLibrary
{
    public class BookCategoryTable
    {
        private const string INSERT_BOOKCATEGORY = @"insert into bookcategory (category_id, book_id) values (@category_id, @book_id)";
        private const string SELECT_ALL = "SELECT * FROM bookcategory";
        private const string SELECT_ALL_BY_BOOK_ID = "SELECT * FROM bookcategory WHERE book_id = @book_id";
        //private const string UPDATE_BOOKCATEGORY = "UPDATE bookcategory SET category_id = @category_id, book_id = @book_id WHERE copy_id = @copy_id";
        //private const string SELECT_ONE = "SELECT * FROM bookcategory WHERE copy_id = @copy_id";
        //private const string DELETE = "DELETE FROM bookcategory WHERE copy_id = @copy_id";
        
        private string connString = null;

        public BookCategoryTable()
        {
            // Zkousel jsem nacist connection string ze souboru, ale pada to, nevim proc. Ve web.config ten string mam pod nazvem constr, ale proste to nejde.
            //connString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            connString = "server=localhost;User Id=root;database=san";            
        }
        //INSERT
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void InsertBookCategory(BookCategory bookcat)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(INSERT_BOOKCATEGORY, conn);

                /* Add parameters into the command */
                command.Parameters.AddWithValue("@category_id", bookcat.category_id);
                command.Parameters.AddWithValue("@book_id", bookcat.book_id);

                /* Executes the command */
                command.ExecuteNonQuery();
            }
        }

        //SELECT ALL
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<BookCategory> SelectAll()
        {
            List<BookCategory> bookcatList = new List<BookCategory>();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(SELECT_ALL, conn);
                
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    BookCategory bookcat = new BookCategory();
                    bookcat.category_id = reader.GetInt32(0);
                    bookcat.book_id = reader.GetInt32(1);

                    bookcatList.Add(bookcat);
                }
            }
            return bookcatList;
        }

        //SELECT ALL BY BOOK ID
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<BookCategory> SelectAllByBook(int bookId)
        {
            List<BookCategory> bookcatList = new List<BookCategory>();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(SELECT_ALL_BY_BOOK_ID, conn);
                command.Parameters.AddWithValue("@book_id", bookId);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    BookCategory bookcat = new BookCategory();
                    bookcat.category_id = reader.GetInt32(0);
                    bookcat.book_id = reader.GetInt32(1);

                    bookcatList.Add(bookcat);
                }
            }
            return bookcatList;
        }
        
        
    }
}
