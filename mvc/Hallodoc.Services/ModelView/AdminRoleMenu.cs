using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallodocServices.ModelView
{
    public class AdminRoleMenu
    {
        public int? Menuid { get; set; }
        public int Roleid { get; set; }
        public string? Name { get; set; }


        public List<string>? MenuLists { get; set; }

        public List<int?>? SelectedMenu { get; set; }

    }
}
