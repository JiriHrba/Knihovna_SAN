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
    public class ActionTable
    {
        private const string INSERT = "INSERT INTO action (action_date, action_capacity, action_name, action_cost) VALUES (@date, @capacity, @name, @cost)";
        private const string UPDATE = "UPDATE action SET action_date = @date, action_capacity = @capacity, action_name = @name, action_cost = @cost WHERE action_id = @id";
        private const string SELECT_ONE = "SELECT * FROM action WHERE action_id = @id";
        private const string SELECT_ALL = "SELECT * FROM action";
        private const string DELETE = "DELETE FROM action WHERE action_id = @id";

        /// <summary>
        /// Dotaz, ktery vraci vsechny akce, na nez je rezerovan uzivatel s danym ID
        /// </summary>
        private const string SELECT_CLIENT_RESERVATED_ACTION = "SELECT * FROM action WHERE action_id IN (SELECT action_id FROM actionreservation WHERE client_id = @id)";

        private string connString;

        public ActionTable()
        {
            // Zkousel jsem nacist connection string ze souboru, ale pada to, nevim proc. Ve web.config ten string mam pod nazvem constr, ale proste to nejde.
            //connString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            connString = "server=localhost;User Id=root;database=san";    
        }

        /// <summary>
        /// Vytvori v systemu novou akci.
        /// </summary>
        /// <param name="Action"></param>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void Insert(Action Action)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(INSERT, conn);

                /* Add parameters into the command */
                command.Parameters.AddWithValue("@date", Action.action_date);
                command.Parameters.AddWithValue("@capacity", Action.action_capacity);
                command.Parameters.AddWithValue("@name", Action.action_name);
                command.Parameters.AddWithValue("@cost", Action.action_cost);
                
                /* Executes the command */
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Provede Update jedne akce.
        /// </summary>
        /// <param name="Action"></param>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void Update(Action Action)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(UPDATE, conn);

                /* Add parameters into the command */
                command.Parameters.AddWithValue("@id", Action.action_id);
                command.Parameters.AddWithValue("@date", Action.action_date);
                command.Parameters.AddWithValue("@capacity", Action.action_capacity);
                command.Parameters.AddWithValue("@name", Action.action_name);
                command.Parameters.AddWithValue("@cost", Action.action_cost);

                /* Executes the command */
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Vrati akci podle zadaneho ID.
        /// </summary>
        /// <param name="ActionId"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]   
        public Action SelectOne(int ActionId)
        {
            Action b = new Action();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(SELECT_ONE, conn);
                command.Parameters.AddWithValue("@id", ActionId);

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    b = ReadActionData(reader);
                }
                reader.Close();
            }
            return b;
        }

        /// <summary>
        /// Vrati vsechny akce ze systemu.
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<Action> SelectAll()
        {
            List<Action> actionsList = new List<Action>();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(SELECT_ALL, conn);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    actionsList.Add(ReadActionData(reader));
                }
            }
            return actionsList;
        }

        /// <summary>
        /// Smaze akci podle predaneho ID.
        /// </summary>
        /// <param name="id">ID akce</param>
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

        private Action ReadActionData(MySqlDataReader reader)
        {
            Action a = new Action();
            a.action_id = reader.GetInt32(0);
            a.action_date = reader.GetDateTime(1);
            a.action_capacity = reader.GetInt32(2);
            a.action_name = reader.GetString(3);
            a.action_cost = reader.GetInt32(4);
     
            return a;
        }

        /// <summary>
        /// Vrati vsechny akce ze systemu, na ktere je registrovan uzivel s predanym ID.
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<Action> SelectClientsActions(int clientId)
        {
            List<Action> actionsList = new List<Action>();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(SELECT_CLIENT_RESERVATED_ACTION, conn);
                command.Parameters.AddWithValue("@id", clientId);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    actionsList.Add(ReadActionData(reader));
                }
            }
            return actionsList;
        }
    }
}