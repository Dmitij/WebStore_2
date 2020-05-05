using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.ServiceHosting.Controllers
{
    //[Route("api/[controller]")]
    [Route(WebApi.Employees)]  //жесткий адрес контроллера

    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeesData
    {
        //тут указываем то что он будет контролировать

        private readonly IEmployeesData _EmployeesData;

        public EmployeesApiController(IEmployeesData EmployeesData) => _EmployeesData = EmployeesData;

        [HttpPost]
        public void Add([FromBody] Employee Employee) => _EmployeesData.Add(Employee);  //значения для сотрудника берем из теля сообщения

        [HttpDelete("{id}")]
        public bool Delete(int id) => _EmployeesData.Delete(id);


        [HttpPut("{id}")]
        public void Edit(int id, [FromBody] Employee Employee) => _EmployeesData.Edit(id, Employee);

        [HttpGet]
        public IEnumerable<Employee> GetAll() => _EmployeesData.GetAll();

        [HttpGet("{id}")]
        public Employee GetById(int id) => _EmployeesData.GetById(id);

        [NonAction]
        public void SaveChanges() => _EmployeesData.SaveChanges();
    }

}