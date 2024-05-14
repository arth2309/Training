using Microsoft.AspNetCore.SignalR;
using System.Reflection.Metadata;
using HallodocServices.Interfaces;
using HallodocServices.ModelView;

namespace HalloDoc.Hubs
{
    public class Chathub : Hub
    {

        private readonly IChatService _chatService;

        public Chathub(IChatService chatService)
        {
            _chatService = chatService;
        }
        public async Task SendMessage(int SenderId,int ReceiverId,string message)
        {
            ChatVM chatVM = new();
            
                chatVM.SenderId = SenderId;
                chatVM.RecieverId = ReceiverId;
                chatVM.CreatedDate = DateTime.Now;
                chatVM.Chat = message;
            bool result = await _chatService.AddChat(chatVM);
            
           await  Clients.All.SendAsync("ReceiveMessage",message);
        }
    }
}
