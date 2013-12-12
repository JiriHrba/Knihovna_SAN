using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseLibrary;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net;
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
                        
            string password = GeneratePassword(6);                  
            client.client_pass_hash = CalculateHashMD5(password);
            client.client_member_from = DateTime.Now;
            client.client_member_to = DateTime.Now.AddDays(1);            
            client.client_isEmp = false;
            client.client_is_active = true;

            ClientTable cTab = new ClientTable();

            // Muze vyhodit vyjimku - catch blok v aspx strance
            cTab.Insert(client);

            // Odeslani emailu [zatim nebude odesilat nic]
            SendEmailToClient(client.client_email, password);
        }

        /// <summary>
        /// Tato metoda se vola, pokud klient zapomene sve heslo a necha si poslat nove. Nove nahodne vygenerovane 
        /// heslo bude poslano na email, ktery klient vyplnil pri registraci.
        /// </summary>
        /// <param name="email">adresa, na kterou bude zaslano nove heslo</param>
        static public void NewPasswordForClient(string email)
        {
            ClientTable ctab = new ClientTable();

            // Nejprve overime, zda email mame v databazi.
            if (ctab.CheckEmail(email))
            {
                /* Pokud existuje, muzeme vygenerovat nove heslo, provest zmeny v DB a poslat 
                   klientovi email.
                */
                string newPass = GeneratePassword(6);
                string newPassHash = CalculateHashMD5(newPass);

                // Zmena hesla v DB
                ctab.ChangePassword(email, newPassHash);

                // Poslani emailu
                string text = String.Format("U Vaseho uctu bylo zmeneno heslo. Nove heslo je nyni {0}.", newPass);
                SendEmailToClient(email, text, "Knihovna SAN - Zmena hesla");
            }
            else
            {
                // Email v systemu neexistuje. Vyhodime expcetion.
                throw new ForgotPasswordException("Tento email neni v systemu evidovan.");
            }
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
        /// Odesle email klientovi. Tato metoda se pouziva, pokud se uzivatel registruje a je mu posilano 
        /// jeho nove heslo.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="password"></param>
        static private void SendEmailToClient(string address, string password)
        {
            var fromAddress = new MailAddress("knihovna.san@gmail.com", "SAN Knihovna");
            var toAddress = new MailAddress(address);
            const string fromPassword = "san123san123";
            const string subject = "Knihovna - registrace do systemu";
            string body = "Vase heslo: " + password;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }

        /// <summary>
        /// Obecna metoda pro odeslani emailu klientovi. 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="text"></param>
        /// <param name="subject"></param>
        static public void SendEmailToClient(string email, string text, string subject)
        {
            var fromAddress = new MailAddress("knihovna.san@gmail.com", "SAN Knihovna");
            var toAddress = new MailAddress(email);
            const string fromPassword = "san123san123";            
            string body = text;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
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
