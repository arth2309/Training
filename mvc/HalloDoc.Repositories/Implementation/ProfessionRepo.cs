using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataModels;
using HalloDoc.Repositories.Interfaces;
using HalloDoc.Repositories.DataContext;


namespace HalloDoc.Repositories.Implementation
{
    public class ProfessionRepo : IProfessionRepo
    {
        private readonly ApplicationDbContext _DbContext;

        public ProfessionRepo(ApplicationDbContext context)
        {
            _DbContext = context;
        }

        public List<HealthProfessionalType> ProfessionalTypes()
        {
            return _DbContext.HealthProfessionalTypes.ToList();
        }
    }
}
