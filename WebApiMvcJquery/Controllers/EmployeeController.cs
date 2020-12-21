using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiMvcJquery.Controllers
{
    public class EmployeeController : ApiController
    {
        EmployeeDBJqueryEntities db = new EmployeeDBJqueryEntities();
        public IEnumerable<Employee> Get()
        {
            return db.Employees.ToList();
        }
        public HttpResponseMessage Get(int id)
        {
            Employee emp = db.Employees.FirstOrDefault(x => x.Id == id);
            if (emp == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "hatalı");
            }
            return Request.CreateResponse(HttpStatusCode.OK, emp);
        }
        public HttpResponseMessage Post(Employee employee)
        {
            try
            {
                db.Employees.Add(employee);
                if (db.SaveChanges() > 0)
                {
                    HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.OK, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + employee.Id);
                    return message;

                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "yandan yemiş");
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }



        }
        public HttpResponseMessage Put(Employee employee)
        {
            Employee emp = db.Employees.FirstOrDefault(x => x.Id == employee.Id);
            try
            {
                if (emp == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "" + emp.Id);

                    //HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.OK, employee);
                    //message.Headers.Location = new Uri(Request.RequestUri + "/" + employee.Id);
                    //return message;

                }
                else
                {
                    emp.Name = employee.Name;
                    emp.Salary = employee.Salary;
                    emp.SurName = employee.SurName;
                    if (db.SaveChanges() > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, employee);
                    }

                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "yandan yemiş");
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }



        }
        public HttpResponseMessage Delete(int id)
        {
            Employee emp = db.Employees.FirstOrDefault(x => x.Id == id);
            try
            {
                if (emp == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, id + ": bulunamadı");
                }
                else
                {
                    db.Employees.Remove(emp);
                    if (db.SaveChanges() > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, id + "silindi");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Dont Delete");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }
        }
        //public HttpResponseMessage Get(string surname = "aydın", int? top = 0)
        //{
        //    IQueryable<Employee> query = db.Employees;
        //    surname = surname.ToLower();
        //    switch (surname)
        //    {
        //        case "all":
        //            break;
        //        case "aydın":
        //        case "uruk":
        //            query = query.Where(x => x.SurName.ToLower() == surname);
        //            break;
        //        default:
        //            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"{surname} bu soyadı taşıyan kişiler kayıtlı değil");
        //    }
        //    if (top > 0)
        //    {
        //        query = query.Take(top.Value);
        //    }
        //    return Request.CreateResponse(HttpStatusCode.OK, query.ToList());
        //}
    }
}
