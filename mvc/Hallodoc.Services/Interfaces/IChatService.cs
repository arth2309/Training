using HallodocServices.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.Interfaces
{
    public interface IChatService
    {
        List<ChatVM> GetChatlist(int SenderId, int RecieverId,int RequestId);
        Task<bool> AddChat(ChatVM chatVM);
    }
}
