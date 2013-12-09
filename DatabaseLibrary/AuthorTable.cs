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
    public class AuthorTable
    {
        //INSERT
        private const string INSERT_AUTHOR = @"INSERT INTO author (author_name, author_surname, author_middle_name, author_birth_date) values (@author_name, @author_surname, @author_middle_name, @author_birth_date)";
        private const string SELECT_ALL = "SELECT * FROM author";
        private const string UPDATE_AUTHOR = "UPDATE author SET author_name = @author_name, author_surname = @author_surname, author_middle_name = @author_middle_name, author_birth_date = @author_birth_date WHERE author_id = @author_id";
        private const string SELECT_ONE = "SELECT * FROM author WHERE author_id = @author_id";
        private const string DELETE = "DELETE FROM author WHERE author_id = @author_id";
        //private const String SELECT_ALL_BY_BOOKID;
        private string connString = null;

        public AuthorTable()
        {
            // Zkousel jsem nacist connection string ze souboru, ale pada to, nevim proc. Ve web.config ten string mam pod nazvem constr, ale proste to nejde.
            //connString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            connString = "server=localhost;User Id=root;database=san";            
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void InsertAuthor(Author author)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(INSERT_AUTHOR, conn);

                /* Add parameters into the command */
                command.Parameters.AddWithValue("@author_name", author.author_name);
                command.Parameters.AddWithValue("@author_surname", author.author_surname);
                command.Parameters.AddWithValue("@author_middle_name", author.author_middle_name);
                command.Parameters.AddWithValue("@author_birth_date", author.author_birth_date);

                /* Executes the command */
                command.ExecuteNonQuery();
            }
        }

        //SELECT ALL
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<Author> SelectAll()
        {
            List<Author> autList = new List<Author>();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(SELECT_ALL, conn);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Author author = new Author();
                    author.author_id = reader.GetInt32(0);
                    author.author_name = reader.GetString(1);
                    author.author_surname = reader.GetString(2);
                    author.author_middle_name = !reader.IsDBNull(3) ? reader.GetString(3) : null;
                    author.author_birth_date = reader.GetDateTime(4);

                    autList.Add(author);
                }
            }
            return autList;
        }
        /// <summary>
        /// Provede update jednoho autora.
        /// 
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void Update(Author author)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(UPDATE_AUTHOR, conn);

                command.Parameters.AddWithValue("@author_id", author.author_id);
                command.Parameters.AddWithValue("@author_name", author.author_name);
                command.Parameters.AddWithValue("@author_surname", author.author_surname);
                command.Parameters.AddWithValue("@author_middle_name", author.author_middle_name);
                command.Parameters.AddWithValue("@author_birth_date", author.author_birth_date);

                /* Executes the command */
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Vraci jednu rezervaci ze systemu podle ID.
        /// 
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Author SelectOne(int authorId)
        {
            Author author = new Author();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(SELECT_ONE, conn);
                command.Parameters.AddWithValue("@author_id", authorId);

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    author.author_id = reader.GetInt32(0);
                    author.author_name = reader.GetString(1);
                    author.author_surname = reader.GetString(2);
                    author.author_middle_name = reader.GetString(3);
                    author.author_birth_date = reader.GetDateTime(4);
                }
                reader.Close();
            }
            return author;
        }

       

        /// <summary>
        /// Smaze rezervaci ze systemu. Vraci pocet radku, ktere byly smazany.
        /// 
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public int Delete(int reservationId)
        {
            int rowsAffected = 0;

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(DELETE, conn);
                command.Parameters.AddWithValue("@reservation_id", reservationId);
                rowsAffected = command.ExecuteNonQuery();
            }
            return rowsAffected;
        }
    }
}
