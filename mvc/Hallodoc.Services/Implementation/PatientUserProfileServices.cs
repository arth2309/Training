using HalloDoc.Repositories.Interfaces;
using HallodocServices.Interfaces;
using HallodocServices.ModelView;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Repositories.DataContext;
using HalloDoc.Repositories.DataModels;

namespace HallodocServices.Implementation
{
    public class PatientUserProfileServices : IPatientUserProfileServices
    {
        private readonly IUserRepo _userRepo;

        public PatientUserProfileServices(IUserRepo userRepo) 
        {
            _userRepo = userRepo;
        }

        public PatientUserProfile GetUserProfile(int Userid) 
        {

            User user = _userRepo.GetUserData(Userid);
            PatientUserProfile showProfile = new()
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Mobile = user.Mobile,
                Street = user.Street,
                City = user.City,
                State = user.State,
                ZipCode = user.ZipCode
            };

            return showProfile;
        }

        public async Task<PatientUserProfile> EditUserProfile(PatientUserProfile profile)
        {
            User user = _userRepo.GetUserData(profile.UserId);
            user.UserId = profile.UserId;
            user.FirstName = profile.FirstName;
            user.LastName = profile.LastName;
            user.Email = profile.Email;
            user.Mobile = profile.Mobile;
            user.Street = profile.Street;
            user.City = profile.City;
            user.State = profile.State;
            user.ZipCode = profile.ZipCode;

            await _userRepo.UpdateTable(user);
            return profile;
        }

    }
}
