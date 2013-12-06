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
    public class ActionReservationTable
    {
        private const string INSERT_ActionReservation = "INSERT INTO actionreservation (act_reservation_date, act_reservation_came, action_id, client_id) VALUES (@act_reservation_date, @act_reservation_client_came, @action_id, @client_id)";
        private const string UPDATE_ActionReservation = "UPDATE actionreservation SET act_reservation_date = @act_reservation_date, act_reservation_came = @act_reservation_client_came, action_id = @action_id, client_id = @client_id WHERE act_reservation_id = @act_reservation_id";
        private const string SELECT_ONE = "SELECT * FROM actionreservation WHERE act_reservation_id = @act_reservation_id";
        private const string SELECT_ALL = "SELECT * FROM actionreservation";
        private const string DELETE = "DELETE FROM actionreservation WHERE act_reservation_id = @act_reservation_id";

        /// <summary>
        /// Pro zruseni rezervace klienta na danou akci.
        /// </summary>
        private const string DELETE_CLIENT_RESERVATION = "DELETE FROM actionreservation WHERE action_id = @actId AND client_id = @cId";

        /// <summary>
        /// Dotaz, ktery vraci pocet registrovanych uzivatelu na danou akci.
        /// </summary>
        private const string SELECT_COUNT_OF_REGISTERED_USER = "SELECT COUNT(client_id) FROM actionreservation WHERE action_id = @id";

        /// <summary>
        /// Dotaz, ktery vraci to, zda je uzivatel jiz registrovan na akci. 0 = neni registronan, > 0 = je registrovan.
        /// </summary>
        private const string SELECT_IS_CLIENT_REGISTERED = "SELECT COUNT(*) FROM actionreservation WHERE action_id = @actId AND client_id = @clientId";

        private string connString = null;

        public ActionReservationTable()
        {
            // Zkousel jsem nacist connection string ze souboru, ale pada to, nevim proc. Ve web.config ten string mam pod nazvem constr, ale proste to nejde.
            //connString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            connString = "server=localhost;User Id=root;database=san";            
        }

        /// <summary>
        /// Vlozi do systemu novou rezervaci na akci.
        /// </summary>
        /// <param name="actRes"></param>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void InsertActionReservation(ActionReservation actRes)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(INSERT_ActionReservation, conn);

                /* Add parameters into the command */
                command.Parameters.AddWithValue("@act_reservation_date", actRes.act_reservation_date);
                command.Parameters.AddWithValue("@act_reservation_client_came", actRes.act_reservation_client_came);
                command.Parameters.AddWithValue("@action_id", actRes.action_id);
                command.Parameters.AddWithValue("@client_id", actRes.client_id);

                /* Executes the command */
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Provede update jedne rezervaci na akci.
        /// </summary>
        /// <param name="actRes"></param>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void Update(ActionReservation actRes)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(UPDATE_ActionReservation, conn);

                command.Parameters.AddWithValue("@act_reservation_date", actRes.act_reservation_date);
                command.Parameters.AddWithValue("@act_reservation_client_came", actRes.act_reservation_client_came);
                command.Parameters.AddWithValue("@action_id", actRes.action_id);
                command.Parameters.AddWithValue("@client_id", actRes.client_id);

                /* Executes the command */
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Vraci jednu rezervaci na akci ze systemu podle ID.
        /// </summary>
        /// <param name="ActionReservationId"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]   
        public ActionReservation SelectOne(int ActionReservationId)
        {
            ActionReservation actRes = new ActionReservation();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(SELECT_ONE, conn);
                command.Parameters.AddWithValue("@ActionReservation_id", ActionReservationId);

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    actRes.act_reservation_id = reader.GetInt32(0);
                    actRes.act_reservation_date = reader.GetDateTime(1);
                    actRes.act_reservation_client_came = reader.GetBoolean(2);
                    actRes.action_id = reader.GetInt32(3);
                    actRes.client_id = reader.GetInt32(4);
                }
                reader.Close();
            }
            return actRes;
        }

        /// <summary>
        /// Vraci vsechny rezervace na akci ze systemu.
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<ActionReservation> SelectAll()
        {
            List<ActionReservation> actResList = new List<ActionReservation>();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(SELECT_ALL, conn);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ActionReservation actRes = new ActionReservation();
                    actRes.act_reservation_id = reader.GetInt32(0);
                    actRes.act_reservation_date = reader.GetDateTime(1);
                    actRes.act_reservation_client_came = reader.GetBoolean(2);
                    actRes.action_id = reader.GetInt32(3);
                    actRes.client_id = reader.GetInt32(4);

                    actResList.Add(actRes);
                }
            }
            return actResList;
        }

        /// <summary>
        /// Smaze rezervaci na akci ze systemu. Vraci pocet radku, ktere byly smazany.
        /// </summary>
        /// <param name="ActionReservationId"></param>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public int Delete(int ActionReservationId)
        {
            int rowsAffected = 0;

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(DELETE, conn);
                command.Parameters.AddWithValue("@ActionReservation_id", ActionReservationId);
                rowsAffected = command.ExecuteNonQuery();
            }
            return rowsAffected;
        }

        /// <summary>
        /// Vraci pocet uzivatelu, kteri jsou registrovani na akci s danym id.
        /// </summary>
        /// <param name="actionId">id akce</param>
        /// <returns>pocet registrovanych uzivatelu</returns>
        public int SelectCountOfRegUser(int actionId)
        {
            int numberOfClients = 0;

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(SELECT_COUNT_OF_REGISTERED_USER, conn);
                command.Parameters.AddWithValue("@id", actionId);
                object o = command.ExecuteScalar();
                numberOfClients = int.Parse(string.Format("{0}", o));
            }
            return numberOfClients;
        }

        
        /// <summary>
        /// Vraci true, pokud je jiz uzivatel registrovan na akci. Jinak false.
        /// </summary>
        /// <param name="actionId"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public bool IsUserRegisteredToAction(int actionId, int clientId)
        {
            int rowCount = 0;

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(SELECT_IS_CLIENT_REGISTERED, conn);
                command.Parameters.AddWithValue("@actId", actionId);
                command.Parameters.AddWithValue("@clientId", clientId);
                object o = command.ExecuteScalar();
                rowCount = int.Parse(string.Format("{0}", o));
            }
            return rowCount > 0;
        }

        /// <summary>
        /// Zrusi rezervaci klienta na danou akci.
        /// </summary>
        /// <param name="actionId"></param>
        /// <param name="clientId"></param>
        /// <returns>Pocet smazanych radku</returns>
        public int CancelClientReservation(int actionId, int clientId)
        {
            int rowCount = 0;

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(DELETE_CLIENT_RESERVATION, conn);
                command.Parameters.AddWithValue("@actId", actionId);
                command.Parameters.AddWithValue("@cId", clientId);
                rowCount = command.ExecuteNonQuery();                
            }
            return rowCount;
        }
    }
}