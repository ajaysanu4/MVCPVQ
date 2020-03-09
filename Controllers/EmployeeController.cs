using MVCTEST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCTEST.Data
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            EmployeeData data = new EmployeeData();
            var employeedata = data.GetEmployees();
            return View(employeedata);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            EmployeeData data = new EmployeeData();
            var employeedata1 = data.GetEmpById(id);
            return View(employeedata1);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee emp)
        {
            try
            {
                // TODO: Add insert logic here
                EmployeeData data = new EmployeeData();
                Employee ss = new Employee();
                var employeedata4 = data.createEmp(emp);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
           
            return View();
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Employee emp)
        {
            try
            {
                // TODO: Add update logic here
                EmployeeData data = new EmployeeData();
                 data.updateEmp(id, emp.empname);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                EmployeeData data = new EmployeeData();
                data.DeleteEmp(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
