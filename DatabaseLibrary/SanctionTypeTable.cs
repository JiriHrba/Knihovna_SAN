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
    public class SanctionTypeTable
    {
        private const String INSERT_SANCTIONTYPE = @"INSERT INTO sanctiontype (stype_name, stype_ammount) values (@stype_name, @stype_ammount)";
        private const string UPDATE_SANCTIONTYPE = "UPDATE sanctiontype SET stype_name = @stype_name, stype_ammount = @stype_ammount WHERE stype_id = @stype_id";
        private const string SELECT_ONE = "SELECT * FROM sanctiontype WHERE stype_id = @stype_id";
        private const string SELECT_ALL = "SELECT * FROM sanctiontype";
        private const string DELETE = "DELETE FROM sanctiontype WHERE stype_id = @stype_id";
        private string connString = null;

        public SanctionTypeTable()
        {
            // Zkousel jsem nacist connection string ze souboru, ale pada to, nevim proc. Ve web.config ten string mam pod nazvem constr, ale proste to nejde.
            //connString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            connString = "server=localhost;User Id=root;database=san";            
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void InsertSanctionType(SanctionType stype)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(INSERT_SANCTIONTYPE, conn);

                /* Add parameters into the command */
                command.Parameters.AddWithValue("@stype_name", stype.stype_name);
                command.Parameters.AddWithValue("@stype_ammount", stype.stype_ammount);

                /* Executes the command */
                command.ExecuteNonQuery();
            }
        }


        //UPDATE
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void Update(SanctionType stype)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(UPDATE_SANCTIONTYPE, conn);

                command.Parameters.AddWithValue("@stype_id", stype.stype_id);
                command.Parameters.AddWithValue("@stype_name", stype.stype_name);
                command.Parameters.AddWithValue("@stype_ammount", stype.stype_ammount);

                /* Executes the command */
                command.ExecuteNonQuery();
            }
        }

        /// 
        /// Vraci jeden typ sankce ze systemu podle ID.
        ///
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public SanctionType SelectOne(int stypeId)
        {
            SanctionType stype = new SanctionType();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(SELECT_ONE, conn);
                command.Parameters.AddWithValue("@stype_id", stypeId);

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    stype.stype_id = reader.GetInt32(0);
                    stype.stype_name = reader.GetString(1);
                    stype.stype_ammount = reader.GetInt32(2);
                }
                reader.Close();
            }
            return stype;
        }

        /// <summary>
        /// Vraci vsechny typy sankci ze systemu.
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<SanctionType> SelectAll()
        {
            List<SanctionType> stypeList = new List<SanctionType>();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(SELECT_ALL, conn);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    SanctionType stype = new SanctionType();
                    stype.stype_id = reader.GetInt32(0);
                    stype.stype_name = reader.GetString(1);
                    stype.stype_ammount = reader.GetInt32(2);

                    stypeList.Add(stype);
                }
            }
            return stypeList;
        }

        /// 
        /// Smaze typ sankce ze systemu. Vraci pocet radku, ktere byly smazany.
        /// 
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public int Delete(int stypeId)
        {
            int rowsAffected = 0;

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(DELETE, conn);
                command.Parameters.AddWithValue("@stype_id", stypeId);
                rowsAffected = command.ExecuteNonQuery();
            }
            return rowsAffected;
        }
    }
}
