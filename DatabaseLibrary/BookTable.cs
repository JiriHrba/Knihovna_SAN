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
    public class BookTable
    {
        
        private const string INSERT_BOOK = @"INSERT INTO book (book_name, book_isbn, book_annotation, author_id) values (@book_name, @book_isbn, @book_annotation, @author_id)";
        private const string SELECT_ALL = "SELECT * FROM book";
        private const string UPDATE_BOOK = "UPDATE book SET book_name = @book_name, book_isbn = @book_isbn, book_annotation = @book_annotation, author_id = @author_id WHERE book_id = @book_id";
        private const string SELECT_ONE = "SELECT * FROM book WHERE book_id = @book_id";
        private const string DELETE = "DELETE FROM book WHERE book_id = @book_id";
        //private const String SELECT_ALL_BY_BOOKID;
        private string connString = null;

        public BookTable()
        {
            // Zkousel jsem nacist connection string ze souboru, ale pada to, nevim proc. Ve web.config ten string mam pod nazvem constr, ale proste to nejde.
            //connString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            connString = "server=localhost;User Id=root;database=san";            
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void InsertBook(Book book)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(INSERT_BOOK, conn);

                /* Add parameters into the command */
                command.Parameters.AddWithValue("@book_name", book.book_name);
                command.Parameters.AddWithValue("@book_isbn", book.book_isbn);
                command.Parameters.AddWithValue("@book_annotation", book.book_annotation);
                command.Parameters.AddWithValue("@author_id", book.author_id);

                /* Executes the command */
                command.ExecuteNonQuery();
            }
        }

        //SELECT ALL
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<Book> SelectAll()
        {
            List<Book> bookList = new List<Book>();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(SELECT_ALL, conn);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Book book = new Book();
                    book.book_id = reader.GetInt32(0);
                    book.book_name = reader.GetString(1);
                    book.book_isbn = reader.GetString(2);
                    book.book_annotation = reader.GetString(3);
                    book.author_id = reader.GetInt32(4);

                    bookList.Add(book);
                }
            }
            return bookList;
        }
        /// <summary>
        /// Provede update knihy.
        /// 
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void Update(Book book)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(UPDATE_BOOK, conn);

                command.Parameters.AddWithValue("@book_name", book.book_name);
                command.Parameters.AddWithValue("@book_isbn", book.book_isbn);
                command.Parameters.AddWithValue("@book_annotation", book.book_annotation);
                command.Parameters.AddWithValue("@author_id", book.author_id);

                /* Executes the command */
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Vraci jednu knihu ze systemu podle ID.
        /// 
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Book SelectOne(int bookId)
        {
            Book book = new Book();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(SELECT_ONE, conn);
                command.Parameters.AddWithValue("@book_id", bookId);

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    book.book_id = reader.GetInt32(0);
                    book.book_name = reader.GetString(1);
                    book.book_isbn = reader.GetString(2);
                    book.book_annotation = reader.GetString(3);
                    book.author_id = reader.GetInt32(4);
                }
                reader.Close();
            }
            return book;
        }

       

        /// <summary>
        /// Smaze knihu ze systemu. Vraci pocet radku, ktere byly smazany.
        /// 
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public int Delete(int bookId)
        {
            int rowsAffected = 0;

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(DELETE, conn);
                command.Parameters.AddWithValue("@book_id", bookId);
                rowsAffected = command.ExecuteNonQuery();
            }
            return rowsAffected;
        }
    }
}
