using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class BusinessAction
    {

        /// <summary>
        /// Provede rezervaci na akci. Vyhazuje ReservationException s popisem chyby, pokud se na akci neni mozne zaregistrovat.
        /// Podle UC6: Rezrvace akce.
        /// </summary>
        /// <param name="actionId"></param>
        /// <param name="clientId"></param>
        static public void ReservationToAction(int actionId, int clientId)
        {
            // Nacteni akce z databaze
            DatabaseLibrary.Action action = new DatabaseLibrary.ActionTable().SelectOne(actionId);

            DatabaseLibrary.ActionReservationTable actResTable = new DatabaseLibrary.ActionReservationTable();

            // Overeni, zda je jeste volne misto    
            int numOfRegClients = actResTable.SelectCountOfRegUser(actionId);

            // Overeni, zda je datum konani akce > aktualni datum
            if (action.action_date <= DateTime.Now)
                throw new ReservationException("Na tuto akci se jiz neni mozne rezervovat.");

            // Jestlize pocet registrovanych uzivatelu je mensi nez kapacita akce, muzeme provest registraci pokud jiz klient neni registrovan
            if (numOfRegClients < action.action_capacity)
            {                
                // Zjisteni, zda jiz neni registrovan
                bool isReg = actResTable.IsUserRegisteredToAction(actionId, clientId);

                if (!isReg)
                {
                    DatabaseLibrary.ActionReservation actRes = new DatabaseLibrary.ActionReservation(actionId, clientId);

                    // Provedeme rezervaci - pokud dojde k vyhozeni vyjimky, klient jiz nema platne clenstvi.
                    try
                    {
                        actResTable.InsertActionReservation(actRes);                        
                    }
                    catch (DatabaseLibrary.MembershipExpiredException ex)
                    {
                        // Klientovi jiz skoncilo clenstvi. Registrovat se nemuze.
                        throw new ReservationException(ex.Message);
                        // Pozdeji by se misto toho mohl user presmerovat na stranku s prodluzovanim clenstvi.
                    }
                }
                else
                {
                    throw new ReservationException("Tuto akci jste jiz rezervoval.");
                }
            }
            else
            {                
                throw new ReservationException("Akce je jiz zaplnena.");
            }
        }

        /// <summary>
        /// Zrusi rezervaci na akci. Vyhazuje ReservationException s popisem chyby, pokud se rezervaci nepodari zrusit.
        /// Podle UC9: Odhlaseni z akce.
        /// </summary>
        /// <param name="actionId"></param>
        /// <param name="clientId"></param>
        static public void CancelReservation(int actionId, int clientId)
        {
            DatabaseLibrary.ActionReservationTable actResTable = new DatabaseLibrary.ActionReservationTable();

            // Musime overit, zda je mozne se jeste odhlasit. Odhlasovat se je mozno nejpozdeji 1 den pred zacatkem akce. Spravnou hodnotu jsem v dokumentu 
            // nenasel, proto jsem si rekl, ze to bude jeden den. To se da pripadne kdykoliv zmenit.
            // Nacteni akce z databaze
            DatabaseLibrary.Action action = new DatabaseLibrary.ActionTable().SelectOne(actionId);

            if (action.action_date <= DateTime.Now.AddDays(1))
                throw new ReservationException("Z teto akce se jiz neni mozne odhlasit. Muzete kontaktovat personal.");

            // Zruseni rezervace
            int res = actResTable.CancelClientReservation(actionId, clientId);

            if (res != 1)
                throw new ReservationException("Zruseni rezervace se nezdarilo.");
                
            
        }
    }
}
