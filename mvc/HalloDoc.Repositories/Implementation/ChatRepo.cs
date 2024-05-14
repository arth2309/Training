using HalloDoc.Repositories.DataContext;
using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDoc.Repositories.Implementation
{
    public class ChatRepo :IChatRepo
    {
        private readonly ApplicationDbContext _context;

        public ChatRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddData(Chat chat)
        {
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();
            return true;
        }

        public List<Chat> GetChats(int SenderId,int RecieverId) 
        {
            return _context.Chats.Where(a => (a.SenderId == SenderId && a.RecieverId == RecieverId) || (a.SenderId == RecieverId && a.RecieverId == SenderId) ).ToList();
        }
    }
           
}
