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
    public class CopyTable
    {
        
        private const String INSERT_COPY = @"insert into copy (copy_is_present, book_id) values (@copy_is_present, @book_id)";
        private const string SELECT_ALL = "SELECT * FROM Copy";
        private const string SELECT_ALL_BY_BOOK_ID = "SELECT * FROM copy WHERE book_id = @book_id";
        private const string SELECT_ALLPRESENT_BY_BOOK_ID = "SELECT * FROM copy WHERE book_id = @book_id AND copy_is_present=1";
        //private const string UPDATE_COPY = "UPDATE copy SET copy_is_present = @copy_is_present, book_id = @book_id WHERE copy_id = @copy_id";
        private const string UPDATE_COPY = "UPDATE copy SET copy_is_present = @copy_is_present WHERE copy_id = @copy_id";
        private const string SELECT_ONE = "SELECT * FROM copy WHERE copy_id = @copy_id";
        private const string DELETE = "DELETE FROM copy WHERE copy_id = @copy_id";
        private const string SELECT_COUNT_BY_BOOKID = "SELECT COUNT(*) FROM copy WHERE book_id = @book_id";
        private string connString = null;

        public CopyTable()
        {
            // Zkousel jsem nacist connection string ze souboru, ale pada to, nevim proc. Ve web.config ten string mam pod nazvem constr, ale proste to nejde.
            //connString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            connString = "server=localhost;User Id=root;database=san";            
        }
        //INSERT
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void InsertCopy(Copy copy)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(INSERT_COPY, conn);

                /* Add parameters into the command */
                command.Parameters.AddWithValue("@copy_is_present", copy.copy_is_present);
                command.Parameters.AddWithValue("@book_id", copy.book_id);

                /* Executes the command */
                command.ExecuteNonQuery();
            }
        }

        //SELECT ALL
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<Copy> SelectAll()
        {
            List<Copy> copyList = new List<Copy>();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(SELECT_ALL, conn);
                
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Copy copy = new Copy();
                    copy.copy_id = reader.GetInt32(0);
                    copy.copy_is_present = reader.GetInt32(1);
                    copy.book_id = reader.GetInt32(2);

                    copyList.Add(copy);
                }
            }
            return copyList;
        }

        //SELECT ALL BY BOOK ID
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<Copy> SelectAllByBook(int bookId)
        {
            List<Copy> copyList = new List<Copy>();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(SELECT_ALL_BY_BOOK_ID, conn);
                command.Parameters.AddWithValue("@book_id", bookId);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Copy copy = new Copy();
                    copy.copy_id = reader.GetInt32(0);
                    copy.copy_is_present = reader.GetInt32(1);
                    copy.book_id = reader.GetInt32(2);

                    copyList.Add(copy);
                }
            }
            return copyList;
        }

        //SELECT ALL PRESENT BY BOOK ID
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<Copy> SelectAllPresentByBook(int bookId)
        {
            List<Copy> copyList = new List<Copy>();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(SELECT_ALLPRESENT_BY_BOOK_ID, conn);
                command.Parameters.AddWithValue("@book_id", bookId);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Copy copy = new Copy();
                    copy.copy_id = reader.GetInt32(0);
                    copy.copy_is_present = reader.GetInt32(1);
                    copy.book_id = reader.GetInt32(2);

                    copyList.Add(copy);
                }
            }
            return copyList;
        }

        //UPDATE
        /// <summary>
        /// Provede update jednoho vytisku.
        /// 
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void Update(Copy copy)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(UPDATE_COPY, conn);

                command.Parameters.AddWithValue("@copy_is_present", copy.copy_is_present);
                //command.Parameters.AddWithValue("@book_id", copy.book_id);
                command.Parameters.AddWithValue("@copy_id", copy.copy_id);

                /* Executes the command */
                command.ExecuteNonQuery();
            }
        }
        //SELECT ONE
        /// <summary>
        /// Vraci jeden vytisk ze systemu podle ID.
        ///
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Copy SelectOne(int copyId)
        {
            Copy copy = new Copy();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(SELECT_ONE, conn);
                command.Parameters.AddWithValue("@copy_id", copyId);

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    copy.copy_id = reader.GetInt32(0);
                    copy.book_id = reader.GetInt32(1);
                    copy.copy_is_present = reader.GetInt32(2);
                }
                reader.Close();
            }
            return copy;
        }

        /// <summary>
        /// Vraci count kopii ze systemu podle BOOK ID.
        /// 
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public long SelectCount(int bookId)
        {
            long count = 0;

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(SELECT_COUNT_BY_BOOKID, conn);
                command.Parameters.AddWithValue("@book_id", bookId);
                count = Convert.ToInt64(command.ExecuteScalar());

            }
            return count;
        }

        /// <summary>
        /// Smaze vytisk ze systemu. Vraci pocet radku, ktere byly smazany.
        /// </summary>
        /// <param name="categoryId"></param>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public int Delete(int copyId)
        {
            int rowsAffected = 0;

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(DELETE, conn);
                command.Parameters.AddWithValue("@copy_id", copyId);
                rowsAffected = command.ExecuteNonQuery();
            }
            return rowsAffected;
        }
    }
}
