using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Runtime.Intrinsics.X86;
using HalloDoc.DataContext;
using HalloDoc.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using HallodocServices.ModelView;
using HallodocServices.Interfaces;
using Azure;
using HallodocServices.Implementation;
using System.Text.Json;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ClosedXML.Excel;
using System.Data;
using HalloDoc.Auth;
using HalloDoc.Repositories.PagedList;
using System.Reflection;
using System;
using HalloDoc.Repositories.DataModels;

namespace HalloDoc.Controllers
{
    public class AdminSiteController : Controller
    {
        private readonly IAdminDashBoardServices _dashBoardServices;
        private readonly IViewCaseServices _viewCaseServices;
        private readonly IViewNoteServices _viewNoteServices;
        private readonly ICancelCaseServices _cancelCaseServices;
        private readonly IAssignCaseServices _assignCaseServices;
        private readonly IBlockCaseServices _blockCaseServices;
        private readonly IViewUploadsServices _viewUploadsServices;
        private readonly IJwtServices _jwtServices;
        private readonly IPatientLoginServices _loginServices;
        private readonly ISendOrderServices _sendOrderServices;
        private readonly IClearCaseServices _clearCaseServices;
        private readonly ISendAgreementServices _sendAgreementServices;
        private readonly ICloseCaseServices _closeCaseServices;
        private readonly IAdminProfileServices _adminProfileServices;
        private readonly IAdminProviderInfoServices _adminProviderInfoServices;
        private readonly IAdminAccessRoleServices _adminAccessRoleServices;
        private readonly IEncounterFormServices _encounterFormServices;
        private readonly ICreateAdminAccountServices _createAdminAccountServices;
        private readonly ICreatePhysicianAccountServices _createPhysicianAccountServices;
        private readonly ISchedulingServices _schedulingServices;
        private readonly IProviderLocationServices _providerLocationServices;
        private readonly IProfessionMenuServices _professionMenuServices;
        private readonly IBlockHistoryServices _blockHistoryServices;
        private readonly IEmailLogServices _emailLogServices;
        private readonly ISMSLogServices _sMSLogServices;
        private readonly ISearchRecordServices _searchRecordServices;
        private readonly IPatientHistoryServices _patientHistoryServices;
        private readonly IPatientRecordServices _patientRecordServices;
        private readonly IEncryptionDecryptionServices _encryptionDecryptionServices;
        private readonly IPatientSendRequestServices _patientSendRequestServices;
        


        public AdminSiteController(IAdminDashBoardServices dashBoardServices, IViewCaseServices viewCaseServices, IViewNoteServices viewNoteServices, ICancelCaseServices cancelCaseServices, IAssignCaseServices assignCaseServices, IBlockCaseServices blockCaseServices, IViewUploadsServices viewUploadsServices, IJwtServices jwtServices, IPatientLoginServices loginServices, ISendOrderServices sendOrderServices, IClearCaseServices clearCaseServices, ISendAgreementServices sendAgreementServices, ICloseCaseServices closeCaseServices, IAdminProfileServices adminProfileServices, IAdminProviderInfoServices adminProviderInfoServices, IAdminAccessRoleServices adminAccessRoleServices, IEncounterFormServices encounterFormServices, ICreateAdminAccountServices createAdminAccountServices, ICreatePhysicianAccountServices createPhysicianAccountServices, ISchedulingServices schedulingServices, IProviderLocationServices providerLocationServices, IProfessionMenuServices professionMenuServices, IBlockHistoryServices blockHistoryServices, IEmailLogServices emailLogServices,ISMSLogServices sMSLogServices, ISearchRecordServices searchRecordServices, IPatientHistoryServices patientHistoryServices, IPatientRecordServices patientRecordServices, IEncryptionDecryptionServices encryptionDecryptionServices,IPatientSendRequestServices patientSendRequestServices)
        {
            _dashBoardServices = dashBoardServices;
            _viewCaseServices = viewCaseServices;
            _viewNoteServices = viewNoteServices;
            _cancelCaseServices = cancelCaseServices;
            _assignCaseServices = assignCaseServices;
            _blockCaseServices = blockCaseServices;
            _viewUploadsServices = viewUploadsServices;
            _jwtServices = jwtServices;
            _loginServices = loginServices;
            _sendOrderServices = sendOrderServices;
            _clearCaseServices = clearCaseServices;
            _sendAgreementServices = sendAgreementServices;
            _closeCaseServices = closeCaseServices;
            _adminProfileServices = adminProfileServices;
            _adminProviderInfoServices = adminProviderInfoServices;
            _adminAccessRoleServices = adminAccessRoleServices;
            _encounterFormServices = encounterFormServices;
            _createAdminAccountServices = createAdminAccountServices;
            _createPhysicianAccountServices = createPhysicianAccountServices;
            _schedulingServices = schedulingServices;
            _providerLocationServices = providerLocationServices;
            _professionMenuServices = professionMenuServices;
            _blockHistoryServices = blockHistoryServices;
            _emailLogServices = emailLogServices;
            _sMSLogServices = sMSLogServices;
            _searchRecordServices = searchRecordServices;
            _patientHistoryServices = patientHistoryServices;
            _patientRecordServices = patientRecordServices;
            _encryptionDecryptionServices = encryptionDecryptionServices;
            _patientSendRequestServices = patientSendRequestServices;
         
        }

        public IActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminLogin(PatientLogin patientLogin)
        {
            if (!ModelState.IsValid)
            {
                return View(patientLogin);
            }
            int id = _loginServices.ValidateUser(patientLogin);



            if (id == 0)

            {
                TempData["invalid-user"] = true;
                return View(patientLogin);
            }
            else
            {
                string role = _loginServices.GetRole(patientLogin.Email);
                string token = _jwtServices.GenerateJWTAuthetication(patientLogin.Email);
                Response.Cookies.Append("token", token);
                TempData["success"] = "Successfully Login";

                UserVM userVM = _loginServices.Object(id);
                Response.Cookies.Append("Name", userVM.UserName);
                Response.Cookies.Append("lid", userVM.Id.ToString());
                if (role == "Admin")
                {
                   
                    return RedirectToAction("AdminDashBoard");
                    
                }
                else
                {
                    string serailList = string.Join(",", userVM.MenuLists);
                    Response.Cookies.Append("list", serailList);
                    return RedirectToAction("ProviderDashBoard","Provider");
                }
              
            }



        }

        public IActionResult AdminLogout()
        {
            Response.Cookies.Delete("Name");
            Response.Cookies.Delete("lid");
            Response.Cookies.Delete("token");
            return RedirectToAction("AdminLogin");
        }

        [CustomAuthorize("Admin")]
        public IActionResult AdminDashBoard()
        {
            ViewBag.Name = Request.Cookies["Name"];
            AdminDashBoard adminDashBoard = _dashBoardServices.newStates(1, 1, 0, 0, null);
            return View(adminDashBoard);
        }

        public IActionResult CheckStatus(int statusI, int currentPage, int typeid, int regionid, string name)
        {
            List<NewState> newStates = _dashBoardServices.getStates(statusI, currentPage, typeid, regionid, name);


            if (statusI == 1)
            {
                return PartialView("_NewState", newStates);
            }

            else if (statusI == 2)
            {
                return PartialView("_PendingState", newStates);
            }
            else if (statusI == 3)
            {
                return PartialView("_ActiveState", newStates);
            }
            else if (statusI == 4)
            {
                return PartialView("_ConcludeState", newStates);
            }
            else if (statusI == 5)
            {
                return PartialView("_ToCloseState", newStates);
            }
            else if (statusI == 6)
            {
                return PartialView("_UnPaidState", newStates);
            }
            else
            {
                return PartialView("_NewState", newStates);
            }





        }

        [CustomAuthorize("Admin")]
        public IActionResult ViewCase(int rcid)
        {
            ViewBag.Name = Request.Cookies["Name"];
            AdminViewCase adminViewCase = _viewCaseServices.GetAdminViewCaseData(rcid);
            return View(adminViewCase);
        }

        [HttpPost]
        public async Task<IActionResult> ViewCase(AdminViewCase adminViewCase)
        {
            if (ModelState.IsValid)
            {
                TempData["VUpdate"] = "Data Updated Successfully";
                AdminViewCase adminViewCase1 = _viewCaseServices.EditViewData(adminViewCase);
                return RedirectToAction("ViewCase", new { rcid = adminViewCase.id });
            }

            return View(null);
        }

        [CustomAuthorize("Admin")]
        public IActionResult ChangeState(int rid)
        {
            AdminViewCase adminViewCase1 = _viewCaseServices.CancelViewData(rid);
            return RedirectToAction("AdminDashBoard");
        }

        [Auth.CustomAuthorize("Admin")]
        public IActionResult ViewNotes(int reqid)
        {
            ViewBag.Name = Request.Cookies["Name"];
            ViewBag.ViewNotesReqid = reqid;
            AdminViewNote adminViewNote = _viewNoteServices.GetViewNote(reqid);
            return View(adminViewNote);
        }

        [HttpPost]

        public async Task<IActionResult> ViewNotes(AdminViewNote adminViewNote)
        {
            if (ModelState.IsValid)
            {
                TempData["NUpdate"] = "Data Updated Successfully";
                AdminViewNote adminViewNote1 = _viewNoteServices.EditAdminNote(adminViewNote);
                return RedirectToAction("ViewNotes", new { reqid = adminViewNote.RequestId });
            }
            return View(null);
        }

        [HttpGet]
        public async Task<IActionResult> CancelCase(string data)
        {
            TempData["Cancel"] = "Request is Cancelled";
            var reqData = JsonSerializer.Deserialize<AdminCancelCase>(data);
            var result = _cancelCaseServices.CancelData(reqData);
            return Json(new { result });

        }


        [HttpGet]
        public async Task<IActionResult> AssignCase(string data)
        {
            TempData["Assign"] = "Request is Assigned";
            var reqData = JsonSerializer.Deserialize<AdminAssignCase>(data);
            var result = _assignCaseServices.AdminAssignCase(reqData);
            return Json(new { result });
        }



        [HttpGet]
        public async Task<IActionResult> TransferCase(string data)
        {
            TempData["Transfer"] = "Request is Transfered";
            var reqData = JsonSerializer.Deserialize<AdminAssignCase>(data);
            var result = await _assignCaseServices.AdminTransferCase(reqData);
            return Json(new { result });
        }

        [HttpGet]
        public async Task<IActionResult> BlockCase(string data)
        {
            TempData["Block"] = "Request is Blocked";
            var reqData = JsonSerializer.Deserialize<AdminBlockCase>(data);
            var result = _blockCaseServices.AdminBlockCase(reqData);
            return Json(new { result });
        }

        [CustomAuthorize("Admin")]
        public IActionResult ViewUploads(int reqID)

        {
            ViewBag.Name = Request.Cookies["Name"];
            AdminViewUpoads adminViewUpoads = _viewUploadsServices.GetUpoads(reqID);
            return View(adminViewUpoads);
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("token");
            return RedirectToAction("AdminLogin");
        }

        [HttpGet]
        public async Task<JsonResult> Delete(int id)
        {
            int reqId = _viewUploadsServices.GetReqIdService(id);
           await _viewUploadsServices.DeleteFileService(id);
            TempData["Delete"] = "File Deleted Successfully";
            return Json(new { redirect = Url.Action("ViewUploads", new { reqID = reqId }) });
        }

        [HttpPost]

        public async Task<IActionResult> ViewUploads(AdminViewUpoads adminViewUpoads)
        {
            if (adminViewUpoads.formFile != null)
            {
              await  _viewUploadsServices.AddFileData(adminViewUpoads);
                TempData["Upload"] = "File Uploaded Successfully";
                return RedirectToAction("ViewUploads", new { reqID = adminViewUpoads.requestId });
            }
            TempData["UploadF"] = "File is Required";
            return RedirectToAction("ViewUploads", new { reqID = adminViewUpoads.requestId });
        }

        public ActionResult DownloadAll(int ReqId)
        {
            byte[] fileBytes = _viewUploadsServices.GetFilesAsZip(ReqId);
            return File(fileBytes, "application/zip", "files.zip");
        }

        [HttpPost]
        public ActionResult? DownloadSelected(List<string> filesChecked)
        {
            
            
                byte[] fileBytes = _viewUploadsServices.GetSelectedFilesAsZip(filesChecked);
                return File(fileBytes, "application/zip", "files.zip");
            
           
        }



        public IActionResult ViewUploadsSendMail(int reqID)
        {
            _viewUploadsServices.SendEmail(reqID);
            return RedirectToAction("ViewUploads", new { reqID = reqID });
        }

        public IActionResult SendOrder(int reqID)

        {
            ViewBag.Name = Request.Cookies["Name"];
            ViewBag.reqID = reqID;
            return View();
        }

        public async Task<JsonResult> ClearCase(int RequestId)
        {
            TempData["Clear"] = "Request is Cleared";
           await  _clearCaseServices.ClearCase(RequestId);
            return Json(new { redirect = Url.Action("AdminDashBoard") });
        }

        [Route("admin/profession")]
        public JsonResult GetProfessions()
        {
            return Json(JsonSerializer.Serialize(_sendOrderServices.GetProfessionalTypes()));
        }

        [Route("admin/business")]
        public JsonResult GetBusinessListByProfessionId(int professionTypeId)
        {
            return Json(JsonSerializer.Serialize(_sendOrderServices.GetHealthProfessionalByType(professionTypeId)));
        }

        [Route("admin/business-data")]
        public JsonResult GetBusinessData(int vendorId)
        {
            return Json(JsonSerializer.Serialize(_sendOrderServices.GetProfessionalById(vendorId)));
        }

        [HttpPost]
        public async Task<IActionResult> SendOrder(AdminSendOrder adminSendOrder)
        {
            if (ModelState.IsValid)
            {
               await _sendOrderServices.AddDataServices(adminSendOrder);
                TempData["Order"] = "Placed Order Successfully";
                return RedirectToAction("AdminDashBoard");
            }
            return View(null);
        }

        public JsonResult GetRegions()
        {
            return Json(JsonSerializer.Serialize(_assignCaseServices.GetRegions()));
        }

        public JsonResult GetPhysiciansByRegion(int RegionId)
        {
            return Json(JsonSerializer.Serialize(_assignCaseServices.GetPhysciansByRegions(RegionId)));
        }

        [HttpGet]

        public IActionResult SendAgreement(string data)
        {

            var reqData = JsonSerializer.Deserialize<SendAgreement>(data);
            string token = _jwtServices.GenerateJWTTokenForSendAgreement(reqData.Requestid);
            _sendAgreementServices.SendEmail(reqData, token);
            return Json(new { token });

        }


        public JsonResult LoadAgreementData(int Requestid)
        {
            return Json(JsonSerializer.Serialize(_sendAgreementServices.LoadSendAgreementData(Requestid)));
        }

        [HttpGet]
        public JsonResult LoadBlockCaseData(int Requestid)
        {
            return Json(JsonSerializer.Serialize(_sendAgreementServices.LoadSendAgreementData(Requestid)));
        }

        public async Task<IActionResult> ViewAgreement(string token)
        {
            int id = await _sendAgreementServices.CheckViewAgreement(token);
            ViewBag.idForViewAgreement = id;

            if (id == 0)
            {
                return RedirectToAction("AccessDenied", "Patient");
            }

            return View();
        }

        public JsonResult LoadViewAgeement(int Requestid)
        {
            return Json(JsonSerializer.Serialize(_sendAgreementServices.LoadSendAgreementData(Requestid)));
        }

        public JsonResult CancelViewAgeement(int Requestid, string Description)
        {
            _sendAgreementServices.CancelViewAgreement(Requestid, Description);
            return Json(new { redirect = Url.Action("PatientLogin", "Patient") }); ;
        }

        public JsonResult AcceptViewAgreement(int Requestid)
        {
            _sendAgreementServices.AcceptViewAgreement(Requestid);
            return Json(new { redirect = Url.Action("PatientLogin", "Patient") }); ;
        }

        public IActionResult CloseCase(int reqID)
        {
            ViewBag.Name = Request.Cookies["Name"];
            AdminCloseCase adminClose = _closeCaseServices.GetCloseCaseData(reqID);
            return View(adminClose);
        }

        [HttpPost]
        public async Task<IActionResult> CloseCase(AdminCloseCase adminClose)
        {
            if (ModelState.IsValid)
            {
                await _closeCaseServices.UpdateCloseData(adminClose);
                return RedirectToAction("CloseCase", new { reqID = adminClose.reqid });
            }

            return View(null);
        }

        [HttpGet]
        public async Task<JsonResult> CloseRequest(int reqID)
        {
            await _closeCaseServices.UpdateStatus(reqID);
            return Json(new { redirect = Url.Action("AdminDashBoard", "AdminSite") });
        }

        public IActionResult AdminMyProfile()
        {
            int id = Int32.Parse(Request.Cookies["lid"]);
            ViewBag.Name = Request.Cookies["Name"];
            AdminProfile adminProfile = _adminProfileServices.GetAdminData(id);
            return View(adminProfile);

        }


        [HttpPost]
        public async Task<IActionResult> AdminResetPassword(AdminProfile adminProfile)
        {
            
                await _adminProfileServices.ResetPassword(adminProfile.Id, adminProfile.Password);
            
           
            return RedirectToAction("AdminProfile");
        }

        [HttpPost]
        public async Task<IActionResult> AdminInformation(AdminProfile adminProfile)
        {
            await _adminProfileServices.UpdateInformation(adminProfile.Id, adminProfile.AdminId, adminProfile.FirstName, adminProfile.LastName, adminProfile.Email, adminProfile.Mobile);
            return RedirectToAction("AdminMyProfile");
        }

        [HttpPost]
        public async Task<IActionResult> AdminBillingInformation(AdminProfile adminprofile)
        {
            await _adminProfileServices.UpDateBillingInformation(adminprofile.AdminId, adminprofile.address1, adminprofile.address2, adminprofile.City, adminprofile.ZipCode, adminprofile.Mobile);
            return RedirectToAction("AdminMyProfile");
        }

        public ActionResult WriteDataToExcel()
        {
            DataTable dt = _dashBoardServices.getData();
            //Name of File  
            string fileName = "Request.xlsx";
            using (XLWorkbook wb = new XLWorkbook())
            {
                //Add DataTable in worksheet  
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    //Return xlsx Excel File  
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
        }

        [HttpPost]
        public ActionResult ExportData(int statusid, int typeId, int regionId, string searchstr, int currentPage)
        {
            DataTable dt = _dashBoardServices.getExportData(statusid, currentPage, typeId, regionId, searchstr);
            //Name of File  
            string fileName = "Request.xlsx";
            using (XLWorkbook wb = new XLWorkbook())
            {
                //Add DataTable in worksheet  
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    //Return xlsx Excel File  
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
        }

        public JsonResult SendLink(string FirstName, string LastName, string Email)
        {
            _dashBoardServices.SendEmail(FirstName, LastName, Email);
            var result = "good";
            return Json(result);
        }

        public IActionResult AdminProviderInformation()
        {
            ViewBag.Name = Request.Cookies["Name"];
            AdminProviderInfo adminProviderInfo = _adminProviderInfoServices.GetProviderInfo();
            return View(adminProviderInfo);
        }

        public IActionResult GetListForInfo(int RegionId)
        {
            ProviderList providerList = _adminProviderInfoServices.GetProviderList(RegionId);
            return PartialView("_PhysicianList", providerList);
        }

        [HttpGet]
        public async Task<bool> ChangeNotification(int Id, bool IsNotificationChecked)
        {
            await _adminProviderInfoServices.NotificationServices(Id, IsNotificationChecked);
            return true;
        }

        [HttpGet]
        public JsonResult GetEmailForMessage(int id)
        {
            return Json(JsonSerializer.Serialize(_adminProviderInfoServices.GetPhysicianData(id)));

        }

        [HttpGet]
        public JsonResult SendEmailForMessage(string email, string description)

        {
            _adminProviderInfoServices.SendEmail(email, description);
            return Json(new { redirect = Url.Action("AdminProviderInformation") });

        }

        public IActionResult AdminAccessRole()
        {
            ViewBag.Name = Request.Cookies["Name"];
            AdminAccessRoleMV adminAccessRoleMV = _adminAccessRoleServices.GetAccessRoleData();
            return View(adminAccessRoleMV);
        }

        [HttpGet]

        public IActionResult GetMenuByRole(int roleid)
        {
            List<AdminRoleMenu> adminRoleMenus = _adminAccessRoleServices.GetAccessRoleDataById(roleid);
            return PartialView("_MenuList", adminRoleMenus);
        }

        public IActionResult EditAccessRole(int RoleId)
        {
            ViewBag.Name = Request.Cookies["Name"];
            AdminAccessRoleMV adminAccessRoleMV = _adminAccessRoleServices.EditAccessRoleData(RoleId);
            return View(adminAccessRoleMV);
        }

        [HttpGet]
        public async Task<JsonResult> CreateRole(string data)
        {
            AdminRoleMenu reqData = JsonSerializer.Deserialize<AdminRoleMenu>(data);
            await _adminAccessRoleServices.CreateRole(reqData);
            TempData["AddRole"] = "Role added Successfully";
            return Json(new { redirect = Url.Action("AdminAccountAccess") });
        }

        [HttpPost]
        public async Task<IActionResult> CreateUpdateAccessRole(AdminAccessRoleMV adminAccessRoleMV)
        {
            
           
            await _adminAccessRoleServices.CreateUpdateRole(adminAccessRoleMV);
            if(adminAccessRoleMV.IsUpdate)
            {
                TempData["UpdateRole"] = "Role updated Successfully";
            }
            else
            {
                TempData["AddRole"] = "Role added Successfully";
            }
            
            return RedirectToAction("AdminAccountAccess");
            
        }

        public IActionResult AdminAccountAccess()
        {
            ViewBag.Name = Request.Cookies["Name"];
            List<AdminAccountAccess> adminAccountAccesses = _adminAccessRoleServices.GetAdminAccountAccessList();
            return View(adminAccountAccesses);
        }

        public IActionResult EncounterForm(int reqID)
        {
            ViewBag.Name = Request.Cookies["Name"];
            if (ModelState.IsValid)
            {

                AdminEncounterForm adminEncounterForm = _encounterFormServices.GetEncounterFormData(reqID);
                return View(adminEncounterForm);
            }

            return View();
        }

        public async Task<IActionResult> DeleteRole(int RoleId)
        {
            await _adminAccessRoleServices.DeleteRole(RoleId);
            TempData["DeleteRole"] = "Role Deleted Successfully";
            return RedirectToAction("AdminAccountAccess");
        }

        public IActionResult CreateAdminAccount()
        {
            ViewBag.Name = Request.Cookies["Name"];
            AdminProfile adminProfile = _createAdminAccountServices.GetLists();
            return View(adminProfile);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdminAccount(AdminProfile adminProfile)
        {
            await _createAdminAccountServices.AddData(adminProfile);
            return RedirectToAction("AdminDashBoard");
        }

        public IActionResult CreateProviderAccount()
        {
            ViewBag.Name = Request.Cookies["Name"];
            AdminProfile adminProfile = _createPhysicianAccountServices.GetLists();
            return View(adminProfile);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProviderAccount(AdminProfile adminProfile)
        {
            await _createPhysicianAccountServices.CreatePhysicianAccount(adminProfile);
            return RedirectToAction("AdminDashBoard");
        }

        public IActionResult EditProviderAccount(int PhysicianId)
        {
            ViewBag.Name = Request.Cookies["Name"];
            AdminProfile adminProfile = _createPhysicianAccountServices.GetPhysician(PhysicianId);
            return View(adminProfile);
        }

        [HttpGet]
        public async Task<JsonResult> ProviderResetPassword(int Id, string Password,int Pid)
        {
            bool result = await _createPhysicianAccountServices.ResetPassword(Id, Password);
            return Json(new { redirect = Url.Action("EditProviderAccount", new { PhysicianId = Pid }) });

        }

        [HttpPost]
        public async Task<IActionResult> EditProviderAccountInformation(AdminProfile adminProfile)
        {
            bool result = await _createPhysicianAccountServices.EditProviderAccountInformation(adminProfile);
            return RedirectToAction("EditProviderAccount", new { PhysicianId = adminProfile.AdminId});
        }

        [HttpPost]

        public async Task<IActionResult> EditPhysicianInformation(AdminProfile adminProfile)
        {
            bool result = await _createPhysicianAccountServices.EditPhysicianInformation(adminProfile);
            return RedirectToAction("EditProviderAccount", new { PhysicianId = adminProfile.AdminId });
        }

        public IActionResult Scheduling()
        {
            ViewBag.Name = Request.Cookies["Name"];
            AdminScheduling adminScheduling = _schedulingServices.GetData(0, DateTime.Now);
            return View(adminScheduling);
        }

        public IActionResult SchedulingFilter(int Scheduling, int RegionId, string StartDay,string EndDay)
        {
            List<SchedulingList> schedulingList = _schedulingServices.GetSchedulingList(RegionId, DateTime.Parse(StartDay), DateTime.Parse(EndDay));
            MonthSchedulingVM monthSchedulingVM = _schedulingServices.GetMonthScheduling(RegionId,DateTime.Parse(StartDay), DateTime.Parse(EndDay));
            if (Scheduling == 1)
            {
                return PartialView("_DayWiseScheduling", schedulingList);
            }
            else if (Scheduling == 2)
            {
                return PartialView("_WeekWiseScheduling", schedulingList);
            }
            if (Scheduling == 3)
            {
                return PartialView("_MonthWiseScheduling",monthSchedulingVM);
            }
            else
            {
                return PartialView("_DayWiseScheduling", schedulingList);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateShift(AdminScheduling adminScheduling)
        {
            if (ModelState.IsValid)
            {
                await _schedulingServices.CreateShift(adminScheduling);
                return RedirectToAction("Scheduling");
            }
            else
            {
                return View(null);
            }
        }

        public IActionResult ShiftForReview()
        {
            ViewBag.Name = Request.Cookies["Name"];
            ShiftForReviewVM shiftForReviewVM = _schedulingServices.GetDataForReviewShift();
            return View(shiftForReviewVM);
        }

        [HttpGet]
        public IActionResult GetPhysicianListForShift(int CurrentPage, int RegionId, bool CurrentMonth)
        {
            PaginatedList<ShiftReviewList> shiftReviewLists = _schedulingServices.FilterDataForReviewShift(CurrentPage, RegionId, CurrentMonth);
            return PartialView("_ShiftReviewList", shiftReviewLists);
        }

        [HttpPost]
        public async Task<JsonResult> ApproveShift(List<int> List)
        {
            if (List.Count > 0)
            {
                await _schedulingServices.ApproveShiftServices(List);
            }
            else
            {
                TempData["shift"] = "Please Select Atleast One Shift";
            }

            return Json(List);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteShift(List<int> List)
        {
            await _schedulingServices.DeleteShiftServices(List);
            return Json(List);
        }

        [HttpGet]

        public JsonResult ViewShift(int ShiftDetailId)
        {
            return Json(JsonSerializer.Serialize(_schedulingServices.ViewShiftDetail(ShiftDetailId)));

        }

        [HttpGet]
        public async Task<JsonResult> EditViewShift(string Data)
        {
            SchedulingList schedulingList = JsonSerializer.Deserialize<SchedulingList>(Data);
            await _schedulingServices.EditViewShift(schedulingList);
            return Json(schedulingList);
        }

        [HttpGet]
        public async Task<JsonResult> DeleteViewShift(int Id)
        {
            await _schedulingServices.DeleteViewShift(Id);
            return Json(Id);
        }
        public IActionResult ProviderLocation()
        {
            ViewBag.Name = Request.Cookies["Name"];
            return View();
        }

        [HttpGet]
        public JsonResult GetProviderLocation()
        {
            return Json(JsonSerializer.Serialize(_providerLocationServices.GetLocation()));
        }

        public IActionResult MDOnCalls()
        {
            ViewBag.Name = Request.Cookies["Name"];
            MdsOnCallVM mdsOnCallVM = _schedulingServices.GetMdsOnCallList();
            return View(mdsOnCallVM);
        }

        [HttpGet]

        public IActionResult MDOnCallsByRegion(int RegionId)
        {
            PhysicianDutyList physicianDutyList = _schedulingServices.GetMdsOnCallListFilter(RegionId);
            return PartialView("_PhysicianDutyList", physicianDutyList);
        }

        public IActionResult ProfessionMenu()
        {
            ViewBag.Name = Request.Cookies["Name"];
            ProfessionMenuVM professionMenuVM = _professionMenuServices.GetVendorList();
            return View(professionMenuVM);
        }

        [HttpGet]
        public IActionResult ProfessionMenuFilter(int ProfessionId, string Name, int CurrentPage)
        {
            PaginatedList<VendorsList> vendorsLists = _professionMenuServices.GetVendorListFilter(ProfessionId, Name, CurrentPage);
            return PartialView("_VendorList", vendorsLists);
        }

        public IActionResult AddBusinessPage()
        {
            ViewBag.Name = Request.Cookies["Name"];
            return View();
        }

        [HttpPost]
        public async  Task<IActionResult> AddBusinessPage(BusinessVM businessVM)
        {
            if (ModelState.IsValid)
            {
                await _professionMenuServices.AddData(businessVM);
                return RedirectToAction("ProfessionMenu");
            }
            else
            {
                return View(null);
            }
           
        }

        public IActionResult UpdateBusinessPage(int VendorId)
        {

            ViewBag.Name = Request.Cookies["Name"];
            BusinessVM businessVM = _professionMenuServices.ShowData(VendorId);
            return View(businessVM);
        }

        [HttpPost]

        public async Task<IActionResult> UpdateBusinessPage(BusinessVM businessVM)
        {
            if (ModelState.IsValid)
            {
                await _professionMenuServices.UpdateData(businessVM);
                return RedirectToAction("ProfessionMenu");
            }
            else
            {
                return View(null);
            }
        }

        [HttpGet]
        public async Task<JsonResult> DeleteVendor(int VendorId)
        { 
            await _professionMenuServices.DeleteData(VendorId);
            return Json(new { Success = true });
        }

        public IActionResult BlockedHistory()
        {
            ViewBag.Name = Request.Cookies["Name"];
            BlockedHistoryVM blockedHistoryVM = _blockHistoryServices.GetBlockHistoryData();
            return View(blockedHistoryVM); 
        }

        public IActionResult BlockedHistoryFilter(string Name,string Email,DateOnly Date,string Mobile,int CurrentPage)
        {
            PaginatedList<BlockedList> blockedLists = _blockHistoryServices.GetBlockHistoryDataFilter(Name,Mobile,Email,Date,CurrentPage);
            return PartialView("_BlockList",blockedLists);
        }

        public async Task<IActionResult> UnBlockRequest(int ReqId)
        {
            await _blockHistoryServices.UnblockRequest(ReqId);
            return RedirectToAction("BlockedHistory");
        }

        public IActionResult SearchRecord()
        {
            ViewBag.Name = Request.Cookies["Name"];
            SearchRecordVM searchRecordVM = _searchRecordServices.GetList();
            return View(searchRecordVM);
        }

        [HttpGet]

        public IActionResult SearchRecordFilter(string PatientName, string ProviderName, int TypeId, string Email, string Mobile, DateOnly FDate, DateOnly TDate, int CurrentPage)
        {
            PaginatedList<SearchRecordList> searchRecordLists = _searchRecordServices.GetListFilter(PatientName,ProviderName,TypeId,Email,Mobile,FDate,TDate,CurrentPage);
            return PartialView("_RecordList",searchRecordLists);
        }

        public IActionResult SMSLogs()
        {
            ViewBag.Name = Request.Cookies["Name"];
            SMSLogVM sMSLogVM = _sMSLogServices.GetLogs();
            return View(sMSLogVM);
        }

        [HttpGet]
        public IActionResult SmsLogFilter(string Name, int RoleId, string Mobile, DateOnly CDate, DateOnly SDate, int CurrentPage)
        {
            PaginatedList<SMSLogList> LogLists = _sMSLogServices.GetLogsFilter(Name, Mobile, SDate, CDate, RoleId, CurrentPage);
            return PartialView("_SmsLogList", LogLists);
        }

        public IActionResult EmailLogs()
        {
            ViewBag.Name = Request.Cookies["Name"];
            EmailLogVM emailLogVM = _emailLogServices.GetLogs();
            return View(emailLogVM);
        }

        [HttpGet]
        public IActionResult EmailLogFilter(string Name,int RoleId,string Email,DateOnly CDate,DateOnly SDate,int CurrentPage) 
        {
            PaginatedList<EmailLogList> emailLogLists = _emailLogServices.GetLogsFilter(Name, Email, SDate, CDate, RoleId, CurrentPage);
            return PartialView("_EmailLogList", emailLogLists);
        }

        [HttpPost]
        public ActionResult ExportSearchRecord(string PatientName, string ProviderName, int TypeId, string Email, string Mobile, DateOnly FDate, DateOnly TDate)
        {
            DataTable dt = _searchRecordServices.getExportData(PatientName, ProviderName, TypeId, Email, Mobile, FDate, TDate);
            //Name of File  
            string fileName = "Record.xlsx";
            using (XLWorkbook wb = new XLWorkbook())
            {
                //Add DataTable in worksheet  
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    //Return xlsx Excel File  
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
        }

        public async Task<IActionResult> DeleteRecord(int RequestId)
        {
            await  _searchRecordServices.Delete(RequestId);
            return RedirectToAction("SearchRecord");
        }

        public IActionResult PatientHistory()
        {
            ViewBag.Name = Request.Cookies["Name"];
            PatientHistoryVM patientHistoryVM = _patientHistoryServices.GetList();
            return View(patientHistoryVM);
        }

        [HttpGet]
        public IActionResult PatientHistoryFilter(string FirstName,string LastName,string Email,string Mobile,int CurrentPage)
        {
            PaginatedList<PatientHistoryList> patientHistoryLists = _patientHistoryServices.GetListFilter(FirstName,LastName,Email,Mobile,CurrentPage);
            return PartialView("_PatientHistoryList",patientHistoryLists);
        }

        public IActionResult PatientRecord(int UserId)
        {
            ViewBag.Name = Request.Cookies["Name"];
            PatientRecordVM patientRecordVM = _patientRecordServices.GetPatientRecord(UserId);
            return View(patientRecordVM);
        }

        [HttpGet]
        public IActionResult PatientRecordFilter(int UserId,int CurrentPage)
        {
            PaginatedList<PatientRecordList> patientRecordLists = _patientRecordServices.GetRecordFilter(UserId,CurrentPage);
            return PartialView("_PatientRecordList", patientRecordLists);
        }

        public IActionResult CreateRequest()
        {
            ViewBag.Name = Request.Cookies["Name"];
            return View();
        }

        [HttpPost]
        public async  Task<IActionResult> CreateRequest(CreateRequestVM createRequestVM)
        {
           bool request = await _patientSendRequestServices.CreateRequest(createRequestVM);
            return RedirectToAction("AdminDashBoard");
        }

        [HttpPost]
        public async Task<IActionResult> DtvSupport(AdminDashBoard adminDashBoard)
        {
            bool result = await _dashBoardServices.SendEmailForSupport(adminDashBoard.Description);
            return RedirectToAction("AdminDashBoard");
        }


    }

}




