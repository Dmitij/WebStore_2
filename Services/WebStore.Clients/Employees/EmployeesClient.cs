using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Clients.Employees
{
    class EmployeesClient : BaseClient, IEmployeesData
    {        
        public EmployeesClient(IConfiguration Configuration) : base(Configuration, WebApi.Employees) { }

        public IEnumerable<Employee> GetAll() => Get<List<Employee>>(_ServiceAddress);

        public Employee GetById(int id) => Get<Employee>($"{_ServiceAddress}/{id}");

        public void Add(Employee Employee) => Post(_ServiceAddress, Employee);

        public void Edit(int id, Employee Employee) => Put($"{_ServiceAddress}/{id}", Employee);

        public bool Delete(int id) => Delete($"{_ServiceAddress}/{id}").IsSuccessStatusCode;

        public void SaveChanges() { }
    }
}
