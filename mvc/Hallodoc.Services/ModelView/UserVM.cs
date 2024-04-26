using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class UserVM
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public int RoleId { get; set; }

        public List<int>? MenuLists { get; set; }
    }
}
