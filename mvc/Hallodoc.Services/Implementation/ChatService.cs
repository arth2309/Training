using HalloDoc.Repositories.Interfaces;
using HallodocServices.ModelView;
using HalloDoc.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HallodocServices.Interfaces;

namespace HallodocServices.Implementation
{
    public class ChatService:IChatService
    {
        private readonly IChatRepo _chatRepo;
        public ChatService(IChatRepo chatRepo) 
        {
            _chatRepo = chatRepo;
        }
        public List<ChatVM> GetChatlist(int SenderId,int RecieverId, int RequestId)
        {
            List<Chat> chats = _chatRepo.GetChats(SenderId,RecieverId,RequestId);
            List<ChatVM> chatVMs = new List<ChatVM>();

            foreach(var chat in chats) 
            {
                ChatVM chatVM = new ChatVM();
                chatVM.SenderId = chat.SenderId;
                chatVM.RecieverId = chat.RecieverId;
                chatVM.CreatedDate = chat.CreatedDate;
                chatVM.Chat = chat.Chat1;
                chatVM.RequestId = chat.RequestId;
                chatVMs.Add(chatVM);
            }

            return chatVMs;
        }

        public async Task<bool> AddChat(ChatVM chatVM)
        {
            Chat chat = new();
            chat.SenderId = chatVM.SenderId;
            chat.RecieverId = chatVM.RecieverId;
            chat.CreatedDate = DateTime.Now;
            chat.Chat1 = chatVM.Chat;
            chat.RequestId = chatVM.RequestId;
            bool result = await _chatRepo.AddData(chat);
            return result;
        }
    }
}
