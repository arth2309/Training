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
    public class EmailLogServices : IEmailLogServices
    {
        private readonly IEmailLogRepo _emailLogRepo;

        public EmailLogServices(IEmailLogRepo emailLogRepo) 
        {
            _emailLogRepo = emailLogRepo;
        }

        public EmailLogVM GetLogs()
        {
            DateTime dateTime = new DateTime(0001, 01, 01);
            DateOnly dateOnly = DateOnly.FromDateTime(dateTime);

            EmailLogVM vm = new EmailLogVM();
            List<EmailLogList> emailLogLists = new List<EmailLogList>();
            List<EmailLog> emailLogs = _emailLogRepo.GetLogs(0, null, null, dateOnly, dateOnly);

            for (int i = 0; i < emailLogs.Count; i++)
            {
                string name = emailLogs[i].Request == null?emailLogs[i].Physician.FirstName + "," + emailLogs[i].Physician.LastName : emailLogs[i].Request.FirstName + "," + emailLogs[i].Request.LastName;

                string role;
                  if (emailLogs[i].RoleId == 1)
                {
                    role = "Admin";
                }
                  else if (emailLogs[i].RoleId == 2) 
                {
                    role = "Physician";
                }
                  else
                {
                    role = "Patient";
                }

                string sent;

                if (emailLogs[i].IsEmailSent[0])
                {
                    sent = "Yes";
                }
                else
                {
                    sent = "No";
                }

                EmailLogList emailLogList = new();
                emailLogList.Action = "good";
                emailLogList.SendDate = DateOnly.FromDateTime((DateTime)emailLogs[i].SentDate);
                emailLogList.CreatedDate = DateOnly.FromDateTime(emailLogs[i].CreateDate);
                emailLogList.Recipient = name;
                emailLogList.Email = emailLogs[i].EmailId;
                emailLogList.Confirmationnumber = emailLogs[i].ConfirmationNumber == null?"-" : emailLogs[i].ConfirmationNumber;
                emailLogList.roleName = role;
                emailLogList.EmailSent = sent;
                emailLogList.EmailTries = emailLogs[i].SentTries;

                emailLogLists.Add(emailLogList);
               
            }

            vm.list = PaginatedList<EmailLogList>.ToPagedList(emailLogLists,1,5);
            return vm;
        }

        public PaginatedList<EmailLogList> GetLogsFilter(string Name,string Email,DateOnly Sdate,DateOnly Cdate,int RoleId,int CurrentPage)
        {
            List<EmailLogList> emailLogLists = new List<EmailLogList>();
            List<EmailLog> emailLogs = _emailLogRepo.GetLogs(RoleId,Name,Email,Cdate,Sdate);

            for (int i = 0; i < emailLogs.Count; i++)
            {
                string name = emailLogs[i].Request == null ? emailLogs[i].Physician.FirstName + "," + emailLogs[i].Physician.LastName : emailLogs[i].Request.FirstName + "," + emailLogs[i].Request.LastName;

                string role;
                if (emailLogs[i].RoleId == 1)
                {
                    role = "Admin";
                }
                else if (emailLogs[i].RoleId == 2)
                {
                    role = "Physician";
                }
                else
                {
                    role = "Patient";
                }

                string sent;

                if (emailLogs[i].IsEmailSent[0])
                {
                    sent = "Yes";
                }
                else
                {
                    sent = "No";
                }

                EmailLogList emailLogList = new();
                emailLogList.Action = "good";
                emailLogList.SendDate = DateOnly.FromDateTime((DateTime)emailLogs[i].SentDate);
                emailLogList.CreatedDate = DateOnly.FromDateTime(emailLogs[i].CreateDate);
                emailLogList.Recipient = name;
                emailLogList.Email = emailLogs[i].EmailId;
                emailLogList.Confirmationnumber = emailLogs[i].ConfirmationNumber == null ? "-" : emailLogs[i].ConfirmationNumber;
                emailLogList.roleName = role;
                emailLogList.EmailSent = sent;
                emailLogList.EmailTries = emailLogs[i].SentTries;

                emailLogLists.Add(emailLogList);

            }

            return PaginatedList<EmailLogList>.ToPagedList(emailLogLists, CurrentPage, 5);

        }
    }
}
