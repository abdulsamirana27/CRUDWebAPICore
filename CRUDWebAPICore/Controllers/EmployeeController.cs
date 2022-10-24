using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CRUDWebAPICore.Models;
using System.Data;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using System.Net;

namespace CRUDWebAPICore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {

        private readonly ILogger<EmployeeController> _logger;


        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger=logger;
        }

        db dbop = new db();
        Common common = new Common();
        string msg = string.Empty;
        // GET: EmployeeController
        [HttpGet]
        [Route("[action]")]
        public  List<Employee> Get()
        {

 //           Tuple<int, string, List<string>> person =
 //new Tuple<int, string, List<string>>(0, "", new List<string>());

            Employee emp = new Employee();  
            emp.type = "get";
            DataSet ds = dbop.EmployeeGet(emp, out msg);
            List<Employee> List = new List<Employee>();
            foreach (DataRow dr in ds.Tables[0]?.Rows)
            {

               List.Add(new Employee
                {
                   //ID = common.checkModelKey<int>(Convert.ToInt32(dr[""]),dr),
                   //Email = dr["Email"].ToString(),
                   //Emp_Name = dr["Emp_Name"].ToString(),
                   //DesignationID = dr["DesignationID"].ToString(),
                   //DesignationName = dr["DesignationName"].ToString(),       

                   ID = Convert.ToInt32(dr["ID"]),
                    Email = dr["Email"].ToString(),
                   Emp_Name = dr["Emp_Name"].ToString(),
                   DesignationID = dr["DesignationID"].ToString(),
                   DesignationName = dr["DesignationName"].ToString(),
               }) ;
            }
            return List;
                
            
           
        }

        // GET: EmployeeController/Details/5
        [HttpGet]
        [Route("[action]")]
        public List<Employee> Details(int id)
        {
            Employee emp = new Employee();
            emp.type = "getid";
            emp.ID = id;
            DataSet ds = dbop.EmployeeGet(emp, out msg);
            List<Employee> List = new List<Employee>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                List.Add(new Employee
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    Email = dr["Email"].ToString(),
                    Emp_Name = dr["Emp_Name"].ToString(),
                    DesignationID = dr["DesignationID"].ToString(),
                });
            }
            return List;
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [Route("[action]")]
        public async Task<string> Create([FromBody] Employee emp)
        {
            string msg = string.Empty;
            try
            {
                emp.type = "insert";
                msg = await dbop.EmployeeOpt(emp);

            }
            catch (Exception e)
            {
                msg = e.Message;

            }

            return msg;
        }



        // POST: EmployeeController/Edit/5
        [HttpPut]
        [Route("[action]/id")]
        public  async Task<string> Edit(int id, [FromBody] Employee emp)
        {
            string msg = string.Empty;
            try
            {
                emp.type = "getid";
                msg = await dbop.EmployeeOpt(emp);

            }
            catch (Exception e)
            {
                msg = e.Message;

            }

            return msg;
        }


        //POST: EmployeeController/Delete/5
        [HttpDelete]
        [Route("[action]/id")]
        public async Task<IActionResult> Delete(int id)
        {
            Employee emp = new Employee();
            string msg = string.Empty;
       try
            {
                emp.type = "delete";
                //if (emp.ID > 0)
                //{
                //    emp.ID = id;
                //    msg = await dbop.EmployeeOpt(emp);
                //}
                if (id>0)
                {
                    emp.ID = id;
                    msg = await dbop.EmployeeOpt(emp);
                }
                else
                {
                    return Ok("Delete Unsuccessful");

                }
               

            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            return Ok(new { isSuccess= true,msg, code= HttpStatusCode.OK });
        }





    }

}
