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
    public class SanctionsHistoryTable
    {
        private const String INSERT_SANCTIONSHISTORY = @"INSERT INTO sanctionshistory (client_id, sanction_desc, sanction_grant, sanction_paid, stype_id) values (@client_id, @sanction_desc, @sanction_grant, @sanction_paid, @stype_id)";
        private const string SELECT_ALL = "SELECT * FROM sanctionshistory";
        private const string SELECT_ALL_BY_CLIENT_ID = "SELECT * FROM sanctionshistory WHERE client_id = @client_id";
        private const string UPDATE_SANCTIONSHISTORY = "UPDATE sanctionshistory SET client_id = @client_id, sanction_desc = @sanction_desc, sanction_grant = @sanction_grant, sanction_paid = @sanction_paid, stype_id = @stype_id WHERE sanctionshistory_id = @sanctionshistory_id";
        private const string SELECT_ONE = "SELECT * FROM sanctionshistory WHERE sanctionshistory_id = @sanctionshistory_id";
        private const string DELETE = "DELETE FROM sanctionshistory WHERE sanctionshistory_id = @sanctionshistory_id";
        private string connString = null;

        public SanctionsHistoryTable()
        {
            // Zkousel jsem nacist connection string ze souboru, ale pada to, nevim proc. Ve web.config ten string mam pod nazvem constr, ale proste to nejde.
            //connString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            connString = "server=localhost;User Id=root;database=san";            
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void InsertSanctionsHistory(SanctionsHistory sanHis)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(INSERT_SANCTIONSHISTORY, conn);

                /* Add parameters into the command */
                command.Parameters.AddWithValue("@client_id", sanHis.client_id);
                command.Parameters.AddWithValue("@sanction_desc", sanHis.sanction_desc);
                command.Parameters.AddWithValue("@sanction_grant", sanHis.sanction_grant);
                command.Parameters.AddWithValue("@sanction_paid", sanHis.sanction_paid);
                command.Parameters.AddWithValue("@stype_id", sanHis.stype_id);

                /* Executes the command */
                command.ExecuteNonQuery();
            }
        }

        //SELECT ALL
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<SanctionsHistory> SelectAll()
        {
            List<SanctionsHistory> sanHisList = new List<SanctionsHistory>();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(SELECT_ALL, conn);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    SanctionsHistory sanHis = new SanctionsHistory();
                    sanHis.sanction_id = reader.GetInt32(0);
                    sanHis.client_id = reader.GetInt32(1);
                    sanHis.sanction_desc = reader.GetString(2);
                    sanHis.sanction_grant = reader.GetDateTime(3);
                    sanHis.sanction_paid = reader.GetDateTime(4);
                    sanHis.stype_id = reader.GetInt32(5);

                    sanHisList.Add(sanHis);
                }
            }
            return sanHisList;
        }
        /// <summary>
        /// Provede update jedne historie sankce.
        /// 
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void Update(SanctionsHistory sanHis)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(UPDATE_SANCTIONSHISTORY, conn);

                command.Parameters.AddWithValue("@client_id", sanHis.client_id);
                command.Parameters.AddWithValue("@sanction_desc", sanHis.sanction_desc);
                command.Parameters.AddWithValue("@sanction_grant", sanHis.sanction_grant);
                command.Parameters.AddWithValue("@sanction_paid", sanHis.sanction_paid);
                command.Parameters.AddWithValue("@stype_id", sanHis.stype_id);

                /* Executes the command */
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Vraci jednu sankci ze systemu podle ID.
        /// 
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public SanctionsHistory SelectOne(int sanctionId)
        {
            SanctionsHistory sanHis = new SanctionsHistory();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(SELECT_ONE, conn);
                command.Parameters.AddWithValue("@sanction_id", sanctionId);

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    sanHis.sanction_id = reader.GetInt32(0);
                    sanHis.client_id = reader.GetInt32(1);
                    sanHis.sanction_desc = reader.GetString(2);
                    sanHis.sanction_grant = reader.GetDateTime(3);
                    sanHis.sanction_paid = reader.GetDateTime(4);
                    sanHis.stype_id = reader.GetInt32(5);
                }
                reader.Close();
            }
            return sanHis;
        }

        /// <summary>
        /// Vraci vsechny sankce ze systemu podle ID klienta.
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<SanctionsHistory> SelectAllByClient(int clientId)
        {
            List<SanctionsHistory> sanHisList = new List<SanctionsHistory>();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(SELECT_ALL_BY_CLIENT_ID, conn);
                command.Parameters.AddWithValue("@client_id", clientId);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    SanctionsHistory sanHis = new SanctionsHistory();
                    sanHis.sanction_id = reader.GetInt32(0);
                    sanHis.client_id = reader.GetInt32(1);
                    sanHis.sanction_desc = reader.GetString(2);
                    sanHis.sanction_grant = reader.GetDateTime(3);
                    sanHis.sanction_paid = reader.GetDateTime(4);
                    sanHis.stype_id = reader.GetInt32(5);

                    sanHisList.Add(sanHis);
                }
            }
            return sanHisList;
        }

        /// <summary>
        /// Smaze sankci ze systemu. Vraci pocet radku, ktere byly smazany.
        /// 
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public int Delete(int sanctionId)
        {
            int rowsAffected = 0;

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(DELETE, conn);
                command.Parameters.AddWithValue("@sanction_id", sanctionId);
                rowsAffected = command.ExecuteNonQuery();
            }
            return rowsAffected;
        }
    }
}
