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
        public EmployeesClient(IConfiguration Configuration) : base(Configuration, WebApi.Employees) 
        {
        }

        public void Add(Employee Employee) => throw new NotImplementedException();

        public bool Delete(int id) => throw new NotImplementedException();

        public void Edit(int id, Employee Employee) => throw new NotImplementedException();

        public IEnumerable<Employee> GetAll() => throw new NotImplementedException();

        public Employee GetById(int id) => throw new NotImplementedException();

        public void SaveChanges() => throw new NotImplementedException();
    }
}
