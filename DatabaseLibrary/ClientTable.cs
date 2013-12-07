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
    public class ClientTable
    {
        private const string INSERT = "INSERT INTO Client (client_name, client_surname, client_email, client_phone, client_birth_date, client_member_from, client_member_to, client_street, client_city, client_zip, country, client_isEmp, client_is_active, client_login, client_pass_hash, client_last_act) VALUES(@name, @surname, @email, @phone, @birthDate, @memberFrom, @memberTo, @street, @city, @zip, @country, @isEmp, @isActive, @login, @passHash, @lastAct)";
        private const string UPDATE = "UPDATE Client SET client_name = @name, client_surname = @surname, client_email = @email, client_phone = @phone, client_birth_date = @birthDate, client_member_from = @memberFrom, client_member_to = @memberTo, client_street = @street, client_city = @city, client_zip = @zip, country = @country, client_isEmp = @isEmp, client_is_active = @isActive, client_login = @login, client_pass_hash = @passHash, client_last_act = @lastAct WHERE client_id = @id";
        private const string DELETE = "DELETE FROM Client WHERE client_id = @id";
        private const string SELECT_ONE = "SELECT * FROM Client WHERE client_id = @id";
        private const string SELECT_ALL = "SELECT * FROM Client";

        /// <summary>
        /// Tento SQL dotaz slouzi k overovani klientu pri prihlasovani.
        /// </summary>
        private const string LOGIN_SELECT = "SELECT * FROM Client WHERE client_login = @login AND client_pass_hash = @hash";

        /// <summary>
        /// Dotazy pro overovani, zda email nebo login jiz v systemu existuje.
        /// </summary>
        private const string SELECT_EMAIL = "SELECT COUNT(*) FROM Client WHERE client_email = @email";
        private const string SELECT_LOGIN = "SELECT COUNT(*) FROM Client WHERE client_login = @login";

        private string connString;

        public ClientTable()
        {
            // Zkousel jsem nacist connection string ze souboru, ale pada to, nevim proc. Ve web.config ten string mam pod nazvem constr, ale proste to nejde.
            //connString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            connString = "server=localhost;User Id=root;database=san";    
        }

        /// <summary>
        /// Vlozi noveho klienta do systemu. Pokud v systemu jiz existuje klient, ktery ma stejnou emailovou adresu nebo login, klient nebude vlozen a bude vyhozena
        /// vyjimka MySqlException s popisem, proc k vyjimce doslo.
        /// </summary>
        /// <param name="client"></param>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void Insert(Client client)
        {       
            // Provedeme kontrolu vstupni dat - pokud to neprojde, vyhodi se vyjimka
            CheckClientData(client.client_email, client.client_login);

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(INSERT, conn);

                /* Add parameters into the command */
                command.Parameters.AddWithValue("@name", client.client_name);
                command.Parameters.AddWithValue("@surname", client.client_surname);
                command.Parameters.AddWithValue("@email", client.client_email);

                if (client.client_phone != null)
                    command.Parameters.AddWithValue("@phone", client.client_phone);
                else
                    command.Parameters.AddWithValue("@phone", DBNull.Value);

                command.Parameters.AddWithValue("@birthDate", client.client_birth_date);
                command.Parameters.AddWithValue("@memberFrom", client.client_member_from);

                if (client.client_member_to != null)
                    command.Parameters.AddWithValue("@memberTo", client.client_member_to);
                else
                    command.Parameters.AddWithValue("@memberTo", DBNull.Value);

                if (client.client_street != null)
                    command.Parameters.AddWithValue("@street", client.client_street);
                else
                    command.Parameters.AddWithValue("@street", DBNull.Value);

                if (client.client_city != null)
                    command.Parameters.AddWithValue("@city", client.client_city);
                else
                    command.Parameters.AddWithValue("@city", DBNull.Value);

                if (client.client_zip != null)
                    command.Parameters.AddWithValue("@zip", client.client_zip);
                else
                    command.Parameters.AddWithValue("@zip", DBNull.Value);

                if (client.client_country != null)
                    command.Parameters.AddWithValue("@country", client.client_country);
                else
                    command.Parameters.AddWithValue("@country", DBNull.Value);

                command.Parameters.AddWithValue("@isEmp", client.client_isEmp);
                command.Parameters.AddWithValue("@isActive", client.client_is_active);
                command.Parameters.AddWithValue("@login", client.client_login);
                command.Parameters.AddWithValue("@passHash", client.client_pass_hash);

                if (client.client_last_act != null)
                    command.Parameters.AddWithValue("@lastAct", client.client_last_act);
                else
                    command.Parameters.AddWithValue("@lastAct", DBNull.Value);                

                /* Executes the command */
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Upravi informace o klientovi.
        /// </summary>
        /// <param name="client"></param>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void Update(Client client)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(UPDATE, conn);

                /* Add parameters into the command */
                command.Parameters.AddWithValue("@id", client.client_id);
                command.Parameters.AddWithValue("@name", client.client_name);
                command.Parameters.AddWithValue("@surname", client.client_surname);
                command.Parameters.AddWithValue("@email", client.client_email);

                if (client.client_phone != null)
                    command.Parameters.AddWithValue("@phone", client.client_phone);
                else
                    command.Parameters.AddWithValue("@phone", DBNull.Value);

                command.Parameters.AddWithValue("@birthDate", client.client_birth_date);
                command.Parameters.AddWithValue("@memberFrom", client.client_member_from);

                if (client.client_member_to != null)
                    command.Parameters.AddWithValue("@memberTo", client.client_member_to);
                else
                    command.Parameters.AddWithValue("@memberTo", DBNull.Value);

                if (client.client_street != null)
                    command.Parameters.AddWithValue("@street", client.client_street);
                else
                    command.Parameters.AddWithValue("@street", DBNull.Value);

                if (client.client_city != null)
                    command.Parameters.AddWithValue("@city", client.client_city);
                else
                    command.Parameters.AddWithValue("@city", DBNull.Value);

                if (client.client_zip != null)
                    command.Parameters.AddWithValue("@zip", client.client_zip);
                else
                    command.Parameters.AddWithValue("@zip", DBNull.Value);

                if (client.client_country != null)
                    command.Parameters.AddWithValue("@country", client.client_country);
                else
                    command.Parameters.AddWithValue("@country", DBNull.Value);

                command.Parameters.AddWithValue("@isEmp", client.client_isEmp);
                command.Parameters.AddWithValue("@isActive", client.client_is_active);
                command.Parameters.AddWithValue("@login", client.client_login);
                command.Parameters.AddWithValue("@passHash", client.client_pass_hash);

                if (client.client_last_act != null)
                    command.Parameters.AddWithValue("@lastAct", client.client_last_act);
                else
                    command.Parameters.AddWithValue("@lastAct", DBNull.Value);

                /* Executes the command */
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Odstrani klienta ze systemu.
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public int Delete(int clientId)
        {
            int rowsAffected = 0;

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(DELETE, conn);
                command.Parameters.AddWithValue("@id", clientId);
                rowsAffected = command.ExecuteNonQuery();
            }
            return rowsAffected;
        }

        /// <summary>
        /// Vraci jednoho klienta podle predaneho ID.
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]   
        public Client SelectOne(int clientId)
        {
            Client client = new Client();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(SELECT_ONE, conn);
                command.Parameters.AddWithValue("@id", clientId);

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    client = ReadClientData(reader);
                }
                reader.Close();
            }
            return client;
        }

        /// <summary>
        /// Vraci seznam vsech klientu ze systemu.
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<Client> SelectAll()
        {
            List<Client> clientList = new List<Client>();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(SELECT_ALL, conn);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    clientList.Add(ReadClientData(reader));
                }
            }
            return clientList;
        }

        /// <summary>
        /// Slouzi pro prihlasovani uzivatelu. Metoda vraci objekt tridy Client, pokud uzivatel s loginem a heslem existuje. Pokud ne, 
        /// bude vracena null hodnota. Zaroven metoda ulozi datum posledniho prihlaseni a zkontroluje, zda ma klient aktivni ucet (tj. pokud member_to > DateTime.Now).
        /// </summary>
        /// <param name="login">uzivatelske jmeno</param>
        /// <param name="passHash">MD5 hash hesla</param>
        /// <returns>instance klienta nebo null, pokud udaje nesouhlasi</returns>
        public Client LoginClient(string login, string passHash)
        {
            Client loggedClient = null;
            
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(LOGIN_SELECT, conn);

                command.Parameters.AddWithValue("@login", login);
                command.Parameters.AddWithValue("@hash", passHash);

                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    loggedClient = ReadClientData(reader);

                    // Provedeme update sloupce lastAct - prave se prihlasil, nastavime na aktualni datum a cas
                    loggedClient.client_last_act = DateTime.Now;

                    // Zjistime, zda ma klient aktivni clenstvi, pokud ne, nastavime na false
                    if (loggedClient.client_member_to < DateTime.Now)
                        loggedClient.client_is_active = false;

                    // Update v databazi
                    Update(loggedClient);
                }                                
            }
            return loggedClient;
        }

        private Client ReadClientData(MySqlDataReader reader)
        {
            Client client = new Client();

            client.client_id = reader.GetInt32("client_id");
            client.client_name = reader.GetString("client_name");
            client.client_surname = reader.GetString("client_surname");
            client.client_email = reader.GetString("client_email");
            client.client_phone = !reader.IsDBNull(4) ? reader.GetString("client_phone") : null;
            client.client_birth_date = reader.GetDateTime("client_birth_date");
            client.client_member_from = reader.GetDateTime("client_member_from");
            client.client_member_to = !reader.IsDBNull(7) ? reader.GetDateTime("client_member_to") : DateTime.MinValue;
            client.client_street = !reader.IsDBNull(8) ? reader.GetString("client_street") : null;
            client.client_city = !reader.IsDBNull(9) ? reader.GetString("client_city") : null;
            client.client_zip = !reader.IsDBNull(10) ? reader.GetString("client_zip") : null;
            client.client_country = !reader.IsDBNull(11) ? reader.GetString("country") : null;
            client.client_isEmp = reader.GetBoolean("client_isEmp");
            client.client_is_active = reader.GetBoolean("client_is_active");
            client.client_login = reader.GetString("client_login");
            client.client_pass_hash = reader.GetString("client_pass_hash");
            client.client_last_act = !reader.IsDBNull(16) ? reader.GetDateTime("client_last_act") : DateTime.MinValue;

            return client;
        }

        /// <summary>
        /// Provadi kontrolu, zda email nebo login clienta jiz existuje v systemu. Pokud ano, vyhodi InputDataException.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="login"></param>
        private void CheckClientData(string email, string login)
        {
            int rowCount = 0;
            string errmsg = null;
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(SELECT_EMAIL, conn);
                command.Parameters.AddWithValue("@email", email);
                object o = command.ExecuteScalar();
                rowCount = int.Parse(string.Format("{0}", o));

                if (rowCount != 0)
                {
                    errmsg = "Klient s timto emailem jiz existuje.";
                }
                else
                {
                    command = new MySqlCommand(SELECT_LOGIN, conn);
                    command.Parameters.AddWithValue("@login", login);
                    o = command.ExecuteScalar();
                    rowCount = int.Parse(string.Format("{0}", o));

                    if (rowCount != 0)
                        errmsg = "Klient s timto loginem jiz existuje.";
                }                
            }
            if (errmsg != null)
                throw new InputDataException(errmsg);
        }
    }
}
