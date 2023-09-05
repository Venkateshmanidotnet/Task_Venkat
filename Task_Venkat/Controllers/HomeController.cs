using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using Task_Venkat.Models;

namespace Task_Venkat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public static List<TaskDetail> taskDetails = new List<TaskDetail>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
  
            if (taskDetails.Count == 0)
            {
                TaskDetail taskDetail = new TaskDetail() { Deadline = "Wednesday", Description = ".NET", Name = "Venkatesh", Status = "InProgress", TaskId = 1 };
                taskDetails.Add(taskDetail);

                taskDetail = new TaskDetail() { Deadline = "Monday", Description = "JAVA", Name = "John", Status = "InProgress", TaskId = 2 };
                taskDetails.Add(taskDetail);
            }

            return View(taskDetails);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(TaskDetail taskDetail)
        {
            if (ModelState.IsValid)
            {
                var cdate = DateTime.Now;
                taskDetails.Add(taskDetail);
                TempData["ResultOk"] = "Record Added Successfully !";
                return RedirectToAction("Index");
            }

            return View(taskDetails);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
   

            var taskDT = (from tas in taskDetails
                             where tas.TaskId == id
                             select tas).FirstOrDefault();

            if (taskDT == null)
            {
                return NotFound();
            }
            return View(taskDT);
        }


        [HttpPost]
  
        public IActionResult Edit(TaskDetail taskDetail)
        {
            if (ModelState.IsValid)
            {
                //foreach (var tas in taskDetails.Where(r => r.TaskId == taskDetail.TaskId))
                foreach (var tas in taskDetails.Where(r => r.Name == taskDetail.Name))
                {
                    tas.Deadline = taskDetail.Deadline;
                    tas.Name = taskDetail.Name;
                    tas.Description = taskDetail.Description;
                    tas.Status = taskDetail.Status;
                }
                
                TempData["ResultOk"] = "Data Updated Successfully !";
                return RedirectToAction("Index");
            }

            return View(taskDetails);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var taskDT = (from tas in taskDetails
                             where tas.TaskId == id
                             select tas).FirstOrDefault();

            if (taskDT == null)
            {
                return NotFound();
            }
            return View(taskDT);
        }

        [HttpPost]
        public IActionResult DeleteTask(int? taskId)
        {
            if (ModelState.IsValid)
            {

                foreach (var tas in taskDetails.Where(r => r.TaskId == taskId))
                {
                    taskDetails.Remove(tas);
                }

                TempData["ResultOk"] = "Data Deleted Successfully !";
                return RedirectToAction("Index");
            }

            return View(taskDetails);
        }

       
    }
}