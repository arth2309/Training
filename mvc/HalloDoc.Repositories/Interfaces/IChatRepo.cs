using HalloDoc.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc.Repositories.Interfaces
{
    public interface IChatRepo
    {
        Task<bool> AddData(Chat chat);

        List<Chat> GetChats(int SenderId, int RecieverId);
    }
}
