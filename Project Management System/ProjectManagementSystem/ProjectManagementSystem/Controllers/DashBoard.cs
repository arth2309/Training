using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystemServices.ModelView;
using ProjectManagementSystemServices.Interface;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ProjectManagementSystem.Controllers
{
    public class DashBoard : Controller
    {
        private readonly IDashBoardServices _services;

        public DashBoard(IDashBoardServices services) 
        {
            _services = services;
        }
        public IActionResult ProjectDashBoard()
        {
            ProjectVM vm = _services.GetList();
            return View(vm);
        }

       

        [HttpGet]
        public IActionResult ProjectDashBoardFilter(string srchStr)
        {
           List<ProjectList> list = _services.GetListFilter(srchStr);
            return PartialView("_ProjectList",list);
        }

        [HttpGet]
        public async Task<JsonResult> Delete(int Id)
        {
            bool result = await _services.DeleteData(Id);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddUpdateForm(ProjectList projectList)
        {
           
           
                if (projectList.ProjectId > 0)
                {
                    bool result = await _services.UpdateData(projectList);
                    return RedirectToAction("ProjectDashBoard");
                }
                else
                {
                    bool result1 = await _services.AddData(projectList);
                    return RedirectToAction("ProjectDashBoard");
                }
            

           
     
            
        }

        [HttpGet]
        public IActionResult EmptyFormData()
        {
            
            return PartialView("_TaskForm");
        }

        [HttpGet]
        public IActionResult GetFormData(int Pid)
        {
            ProjectList projectList = _services.GetData(Pid);
            return PartialView("_TaskForm",projectList);
        }

    }
}
