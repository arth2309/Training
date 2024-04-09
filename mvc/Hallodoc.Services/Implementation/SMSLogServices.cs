using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;
using HalloDoc.Repositories.PagedList;
using HallodocServices.Interfaces;
using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Implementation
{
    public class SMSLogServices : ISMSLogServices
    {
        private readonly ISMSLogRepo _LogRepo;

        public SMSLogServices(ISMSLogRepo LogRepo)
        {
            _LogRepo = LogRepo;
        }

        public SMSLogVM GetLogs()
        {
            DateTime dateTime = new DateTime(0001, 01, 01);
            DateOnly dateOnly = DateOnly.FromDateTime(dateTime);

            SMSLogVM vm = new SMSLogVM();
            List<SMSLogList> LogLists = new List<SMSLogList>();
            List<Smslog> Logs = _LogRepo.GetLogs(0, null, null, dateOnly, dateOnly);

            for (int i = 0; i < Logs.Count; i++)
            {
                string name = Logs[i].Request == null ? Logs[i].Physician.FirstName + "," + Logs[i].Physician.LastName : Logs[i].Request.FirstName + "," + Logs[i].Request.LastName;

                string role;
                if (Logs[i].RoleId == 1)
                {
                    role = "Admin";
                }
                else if (Logs[i].RoleId == 2)
                {
                    role = "Physician";
                }
                else
                {
                    role = "Patient";
                }

                string sent;

                if (Logs[i].IsSmssent[0])
                {
                    sent = "Yes";
                }
                else
                {
                    sent = "No";
                }

                SMSLogList LogList = new();
                LogList.Action = "good";
                LogList.SendDate = DateOnly.FromDateTime((DateTime)Logs[i].SentDate);
               LogList.CreatedDate = DateOnly.FromDateTime(Logs[i].CreateDate);
               LogList.Recipient = name;
               LogList.Mobile = Logs[i].MobileNumber;
                LogList.Confirmationnumber = Logs[i].ConfirmationNumber == null ? "-" : Logs[i].ConfirmationNumber;
                LogList.roleName = role;
                LogList.SmsSent = sent;
                LogList.SmsTries = Logs[i].SentTries;

                LogLists.Add(LogList);

            }

            vm.list = PaginatedList<SMSLogList>.ToPagedList(LogLists, 1, 5);
            return vm;
        }

        public PaginatedList<SMSLogList> GetLogsFilter(string Name, string Email, DateOnly Sdate, DateOnly Cdate, int RoleId, int CurrentPage)
        {
            List<SMSLogList> LogLists = new List<SMSLogList>();
            List<Smslog> Logs = _LogRepo.GetLogs(RoleId, Name, Email, Cdate, Sdate);
            for (int i = 0; i < Logs.Count; i++)
            {
                string name = Logs[i].Request == null ? Logs[i].Physician.FirstName + "," + Logs[i].Physician.LastName : Logs[i].Request.FirstName + "," + Logs[i].Request.LastName;

                string role;
                if (Logs[i].RoleId == 1)
                {
                    role = "Admin";
                }
                else if (Logs[i].RoleId == 2)
                {
                    role = "Physician";
                }
                else
                {
                    role = "Patient";
                }

                string sent;

                if (Logs[i].IsSmssent[0])
                {
                    sent = "Yes";
                }
                else
                {
                    sent = "No";
                }

                SMSLogList LogList = new();
                LogList.Action = "good";
                LogList.SendDate = DateOnly.FromDateTime((DateTime)Logs[i].SentDate);
                LogList.CreatedDate = DateOnly.FromDateTime(Logs[i].CreateDate);
                LogList.Recipient = name;
                LogList.Mobile = Logs[i].MobileNumber;
                LogList.Confirmationnumber = Logs[i].ConfirmationNumber == null ? "-" : Logs[i].ConfirmationNumber;
                LogList.roleName = role;
                LogList.SmsSent = sent;
                LogList.SmsTries = Logs[i].SentTries;

                LogLists.Add(LogList);

            }

            return PaginatedList<SMSLogList>.ToPagedList(LogLists, CurrentPage, 5);

        }
    }
}
