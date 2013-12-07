using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseLibrary;
using System.Security.Cryptography;
namespace BusinessLogic
{
    public class BusinessClient
    {
        /// <summary>
        /// Provede registraci klienta podle UC1: Registrace klienta. 
        /// Oproti UC1 jsou zde dve zmeny:
        /// 1) Neposila se aktivacni email - pouze se posle email s vygenerovanym heslem
        /// 2) Login se negeneruje, uzivatel si ho zada sam. Uz vidim, jak nekdo bude chtit mit login treba CKFdjfnr154 :)
        /// </summary>
        /// <param name="client"></param>
        static public void RegisterClient(Client client)
        {
            // Doplnime udaje, ktere nebyly zadany v registracnim formulari
            
            // Heslo zatim nebudeme generovat, nechodi zatim email, nastavime proste na ABC123
            //string password = GeneratePassword(6);
            string password = "ABC123";            
            client.client_pass_hash = CalculateHashMD5(password);
            client.client_member_from = DateTime.Now;
            client.client_member_to = DateTime.Now.AddDays(1);            
            client.client_isEmp = false;

            ClientTable cTab = new ClientTable();

            // Muze vyhodit vyjimku - catch blok v aspx strance
            cTab.Insert(client);

            // Odeslani emailu [zatim nebude odesilat nic]
            //SendEmailToClient(client.client_email, password);
        }

        /// <summary>
        /// Vygeneruje nahodne heslo z pismen a cisel.
        /// </summary>
        /// <param name="passwordLength"></param>
        /// <returns></returns>
        static private string GeneratePassword(int passwordLength)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            char[] chars = new char[passwordLength];
            Random rd = new Random();

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

        /// <summary>
        /// Odesle email.
        /// [Zatim nefunguje, je potreba pridat prihlasovaci udaje.]
        /// </summary>
        /// <param name="address"></param>
        /// <param name="password"></param>
        static private void SendEmailToClient(string address, string password)
        {
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            message.To.Add(address);
            message.Subject = "Knihova SAN - Registrace";
            message.From = new System.Net.Mail.MailAddress("knihovna@san.cz");
            message.Body = String.Format("Vase heslo pro prihlaseni: {0}", password);
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.google.cz");
            smtp.Send(message);
        }

        /// <summary>
        /// Vraci MD5 hash.
        /// </summary>
        /// <param name="clearText"></param>
        /// <returns></returns>
        static public string CalculateHashMD5(string clearText)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] tmpSource;
            byte[] tmpHash;

            tmpSource = System.Text.Encoding.UTF8.GetBytes(clearText);
            tmpHash = md5.ComputeHash(tmpSource);

            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in tmpHash)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            string hash = s.ToString();
            return hash;
        }

    }
}
