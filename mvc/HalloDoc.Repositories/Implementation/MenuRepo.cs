using HalloDoc.Repositories.DataContext;
using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;

namespace HalloDoc.Repositories.Implementation
{
    public class MenuRepo : IMenuRepo
    {
        private readonly ApplicationDbContext _context;

        public MenuRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Menu> getMenuByRole(int Roleid)
        {
            if (Roleid == 0)
            {
                return _context.Menus.ToList();
            }
            else
            {
                return _context.Menus.Where(a => a.AccountType == Roleid).ToList();
            }
        }
    }

}
