using DSM.UI.Api.Models.Monitoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSM.UI.Api.Services
{
    public interface IMonitoringService
    {
        IEnumerable<Alerts> GetAlertsItems(int pagenumber);
        IEnumerable<object> GetContactItems(int alertId);
    }

    public class MonitoringService : IMonitoringService
    {
        private const int _pageItemCount = 100;
        private readonly DSMStorageDataContext _context;
        private const string _numberFormat = "{0:#,#}";
        public MonitoringService(DSMStorageDataContext context)
        {
            _context = context;
        }

        public IEnumerable<Alerts> GetAlertsItems(int pagenumber)
        {
            var query = from alert in this._context.Alert select alert;
            //join alertcontacts in this._context.AlertContacts
            //    on alerts.AlertId equals alertcontacts.AlertId
            //join extendedcontact in this._context.ExtendedContact
            //    on alertcontacts.ExtendedContactId equals extendedcontact.ExtendedContactId
            //select new 
            //{
            //    AlertDescription = alerts.AlertDescription,
            //    Action1 = alerts.Action1,
            //    Action2 = alerts.Action2,
            //    Action3 = alerts.Action3,
            //    Action4 = alerts.Action4,
            //    AlertSource = alerts.AlertSource,
            //    Domain = alerts.Domain,
            //    AlertNotes = alerts.Notes,
            //    ActionType =alertcontacts.ActionType,
            //    Priority = alertcontacts.Priority,
            //    Department = extendedcontact.Department,
            //    EMail = extendedcontact.EMail,
            //    FullName = extendedcontact.FullName,
            //    ExtendedNotes = extendedcontact.Notes,
            //    Phone1 = extendedcontact.Phone1,
            //    Phone2 = extendedcontact.Phone2,
            //    Unit = extendedcontact.Unit
            //};

            if (pagenumber < 2)
            {
                return query.Take(_pageItemCount).AsEnumerable().Distinct();
            }
            else
            {
                return query.Skip((pagenumber - 1) * _pageItemCount).Take(_pageItemCount).AsEnumerable().Distinct();
            }
        }

        public IEnumerable<object> GetContactItems(int alertId)
        {
            var query = from contact in this._context.ExtendedContact
                        join alertContact in this._context.AlertContacts
                            on contact.ExtendedContactId equals alertContact.ExtendedContactId
                        where alertContact.AlertId == alertId
                        select new
                        {
                            contact.ExtendedContactId,
                            contact.ContactId,
                            contact.FullName,
                            contact.EMail,
                            contact.Phone1,
                            contact.Phone2,
                            contact.Department,
                            contact.Unit,
                            contact.ManagerContactId,
                            contact.Notes,
                            alertContact.Priority,
                            alertContact.ActionType
                        };

            return query.AsEnumerable();
        }
    }
}
