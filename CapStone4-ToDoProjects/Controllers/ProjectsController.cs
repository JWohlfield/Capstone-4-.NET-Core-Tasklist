using CapStone4_ToDoProjects.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapStone4_ToDoProjects.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ProjectsDBContext _projectsDB;

        public ProjectsController(ProjectsDBContext projectContext)
        {
            _projectsDB = projectContext;
        }
        public IActionResult Index(User u)
        {
            TempData["TempUserId"] = u.Id;
            List<Project> p = _projectsDB.Projects.Where(x => x.UserId == u.Id).ToList();
            return View(p);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult UserLogin()
        {
            return View();
        }

        public IActionResult LoginError()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserLogin(string UserName, string UserPassword)
        {
            if(ModelState.IsValid)
            {
                List<User> users = _projectsDB.Users.ToList();
                foreach (User u in users)
                {
                    if (u.UserName.ToLower() == UserName.Trim().ToLower() 
                        && u.UserPassword == UserPassword)
                    {
                        TempData["TempUser"] = UserName;
                        
                        return RedirectToAction("Index", u);
                    }
                }
            }

            return RedirectToAction("LoginError");
        }

        public IActionResult UserRegistration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserRegistration(User u)
        {
            if (ModelState.IsValid)
            {
                List<User> users = _projectsDB.Users.ToList();
                foreach (User user in users)
                {
                    if(user.UserName.ToLower() == u.UserName.Trim().ToLower())
                    {
                        return RedirectToAction("UserRegistrationError", u);
                    }
                }
                _projectsDB.Users.Add(u);
                _projectsDB.SaveChanges();
                //return View();
                return RedirectToAction("Index", u.Id);
            }
            else
            {
                return View();
            }
        }

        public IActionResult UserRegistrationError(User u)
        {
            return View(u);
        }

        public IActionResult UserDetails(User u)
        {
            return View(u);
        }

        public IActionResult ProjectCreate(int userId)
        {
            TempData["TempUserId"] = userId;
            return View();
        }

        [HttpPost]
        public IActionResult ProjectCreate(Project p)
        {
            if(ModelState.IsValid)
            {
                
                _projectsDB.Projects.Add(p);
                _projectsDB.SaveChanges();
                return RedirectToAction("Index", p.UserId);
            }

            return View(p.UserId);
        }
    }
}
