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
        public async Task SendMessage(int SenderId,int ReceiverId,int RequestId,string message)
        {
           
           //await  Clients.All.SendAsync("ReceiveMessage",ReceiverId,message);
           //await Clients.Users().SendAsync("ReceiveMessage", ReceiverId, message);
          
           string groupname = _chatService.MakeGroup(SenderId, ReceiverId,RequestId);
            await Clients.Group(groupname).SendAsync("ReceiveMessage", ReceiverId, message); 
            ChatVM chatVM = new();
            
                chatVM.SenderId = SenderId;
                chatVM.RecieverId = ReceiverId;
                chatVM.CreatedDate = DateTime.Now;
                chatVM.Chat = message;
                chatVM.RequestId = RequestId;
            bool result =  await _chatService.AddChat(chatVM);
            
          
        }

        public async Task JoinRoom(int SenderId, int ReceiverId, int RequestId)
        {
            string groupname = _chatService.MakeGroup(SenderId,ReceiverId,RequestId);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupname);
        }

        public async Task LeaveRoom(int SenderId, int ReceiverId,int RequestId)
        {
            string groupname = _chatService.MakeGroup(SenderId, ReceiverId, RequestId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupname);
        }
    }
}
