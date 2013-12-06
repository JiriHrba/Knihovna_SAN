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
    public class ReservationTable
    {
        //INSERT
        private const String INSERT_RESERVATION = @"INSERT INTO reservation (client_id, copy_id, reservation_appeal, reservation_date) values (@client_id, @copy_id, @reservation_appeal, @reservation_date)";
        private const string SELECT_ALL = "SELECT * FROM reservation";
        private const string UPDATE_RESERVATION = "UPDATE reservation SET client_id = @client_id, copy_id = @copy_id, reservation_appeal = @reservation_appeal, reservation_date = @reservation_date WHERE reservation_id = @reservation_id";
        private const string SELECT_ONE = "SELECT * FROM reservation WHERE reservation_id = @reservation_id";
        private const string DELETE = "DELETE FROM reservation WHERE reservation_id = @reservation_id";
        //private const String SELECT_ALL_BY_BOOKID;
        private string connString = null;

        public ReservationTable()
        {
            // Zkousel jsem nacist connection string ze souboru, ale pada to, nevim proc. Ve web.config ten string mam pod nazvem constr, ale proste to nejde.
            //connString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            connString = "server=localhost;User Id=root;database=san";            
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void InsertReservation(Reservation res)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(INSERT_RESERVATION, conn);

                /* Add parameters into the command */
                command.Parameters.AddWithValue("@copy_id", res.copy_id);
                command.Parameters.AddWithValue("@client_id", res.client_id);
                command.Parameters.AddWithValue("@reservation_appeal", res.reservation_appeal);
                command.Parameters.AddWithValue("@reservation_date", res.reservation_date);

                /* Executes the command */
                command.ExecuteNonQuery();
            }
        }

        //SELECT ALL
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<Reservation> SelectAll()
        {
            List<Reservation> stypeList = new List<Reservation>();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(SELECT_ALL, conn);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Reservation res = new Reservation();
                    res.client_id = reader.GetInt32(0);
                    res.copy_id = reader.GetInt32(1);
                    res.reservation_id = reader.GetInt32(2);
                    res.reservation_appeal = reader.GetDateTime(3);
                    res.reservation_date = reader.GetDateTime(4);

                    stypeList.Add(res);
                }
            }
            return stypeList;
        }
        /// <summary>
        /// Provede update jedne rezervace.
        /// 
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void Update(Reservation res)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(UPDATE_RESERVATION, conn);

                command.Parameters.AddWithValue("@copy_id", res.copy_id);
                command.Parameters.AddWithValue("@client_id", res.client_id);
                command.Parameters.AddWithValue("@reservation_appeal", res.reservation_appeal);
                command.Parameters.AddWithValue("@reservation_date", res.reservation_date);

                /* Executes the command */
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Vraci jednu rezervaci ze systemu podle ID.
        /// 
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Reservation SelectOne(int reservationId)
        {
            Reservation res = new Reservation();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(SELECT_ONE, conn);
                command.Parameters.AddWithValue("@reservation_id", reservationId);

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    res.client_id = reader.GetInt32(0);
                    res.copy_id = reader.GetInt32(1);
                    res.reservation_id = reader.GetInt32(2);
                    res.reservation_appeal = reader.GetDateTime(3);
                    res.reservation_date = reader.GetDateTime(4);
                }
                reader.Close();
            }
            return res;
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
