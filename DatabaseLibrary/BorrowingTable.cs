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
    public class BorrowingTable
    {
        private const string INSERT = "INSERT INTO borrowing (borrowing_from, borrowing_to, borrowing_is_returned, client_id, copy_id) VALUES (@from, @to, @isRet, @clientId, @copyId)";
        private const string UPDATE = "UPDATE borrowing SET borrowing_from = @from, borrowing_to = @to, borrowing_is_returned = @isRet, client_id = @clientId, copy_id = @copyId WHERE borrowing_id = @id";
        private const string SELECT_ONE = "SELECT * FROM borrowing WHERE borrowing_id = @id";
        private const string SELECT_ALL = "SELECT * FROM borrowing";
        private const string DELETE = "DELETE FROM borrowing WHERE borrowing_id = @id";
        private const string SELLECT_ALL_BY_CLIENT_ID = "SELECT * FROM borrowing where client_id = @id";

        private string connString;

        public BorrowingTable()
        {
            // Zkousel jsem nacist connection string ze souboru, ale pada to, nevim proc. Ve web.config ten string mam pod nazvem constr, ale proste to nejde.
            //connString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            connString = "server=localhost;User Id=root;database=san";    
        }

        /// <summary>
        /// Vytvori v systemu novou vypujcku.
        /// </summary>
        /// <param name="borrowing"></param>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void Insert(Borrowing borrowing)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(INSERT, conn);

                /* Add parameters into the command */
                command.Parameters.AddWithValue("@from", borrowing.borrowing_from);
                command.Parameters.AddWithValue("@to", borrowing.borrowing_to);
                command.Parameters.AddWithValue("@isRet", borrowing.borrowing_is_returned);
                command.Parameters.AddWithValue("@clientId", borrowing.client_id);
                command.Parameters.AddWithValue("@copyId", borrowing.copy_id);
                                
                /* Executes the command */
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Provede Update jedne vypujcky.
        /// </summary>
        /// <param name="borrowing"></param>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void Update(Borrowing borrowing)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(UPDATE, conn);

                /* Add parameters into the command */
                command.Parameters.AddWithValue("@id", borrowing.borrowing_id);
                command.Parameters.AddWithValue("@from", borrowing.borrowing_from);
                command.Parameters.AddWithValue("@to", borrowing.borrowing_to);
                command.Parameters.AddWithValue("@isRet", borrowing.borrowing_is_returned);
                command.Parameters.AddWithValue("@clientId", borrowing.client_id);
                command.Parameters.AddWithValue("@copyId", borrowing.copy_id);

                /* Executes the command */
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Vrati vypujcku podle zadaneho ID.
        /// </summary>
        /// <param name="borrowingId"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]   
        public Borrowing SelectOne(int borrowingId)
        {
            Borrowing b = new Borrowing();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(SELECT_ONE, conn);
                command.Parameters.AddWithValue("@id", borrowingId);

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    b = ReadBorrowingData(reader);
                }
                reader.Close();
            }
            return b;
        }

        /// <summary>
        /// Vrati vsechny vypujcky ze systemu.
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<Borrowing> SelectAll()
        {
            List<Borrowing> borrList = new List<Borrowing>();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(SELECT_ALL, conn);


                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    borrList.Add(ReadBorrowingData(reader));
                }
            }
            return borrList;
        }

        /// <summary>
        /// Vrati vsechny vypujcky ze systemu dle id klienta
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<Borrowing> SelectAllbyClientId(int clientId)
        {
            List<Borrowing> borrList = new List<Borrowing>();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(SELLECT_ALL_BY_CLIENT_ID, conn);
                command.Parameters.AddWithValue("@id", clientId);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    borrList.Add(ReadBorrowingData(reader));
                }
            }
            return borrList;
        }

        /// <summary>
        /// Smaze vypujcku podle predaneho ID.
        /// </summary>
        /// <param name="id">ID vypujcky</param>
        /// <returns>Pocet modifikovanych radku v tabulce</returns>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public int Delete(int id)
        {
            int rowsAffected = 0;

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(DELETE, conn);
                command.Parameters.AddWithValue("@id", id);
                rowsAffected = command.ExecuteNonQuery();
            }
            return rowsAffected;
        }

        private Borrowing ReadBorrowingData(MySqlDataReader reader)
        {
            Borrowing b = new Borrowing();
            b.borrowing_id = reader.GetInt32(0);
            b.borrowing_from = reader.GetDateTime(1);
            b.borrowing_to = reader.GetDateTime(2);
            b.borrowing_is_returned = reader.GetBoolean(3);
            b.client_id = reader.GetInt32(4);
            b.copy_id = reader.GetInt32(5);
            return b;
        }
    }
}
