using AutoMapper;
using mvc_migration.Dtos;
using mvc_migration.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace mvc_migration.Apis
{
    public class CustomersController : ApiController
    {
        ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }


        [HttpGet]
        public IEnumerable<CustomerDto> Customers()
        {
            //return _contex.Customers.ToList();
            return _context.Customers
                .Include(c => c.MembershipType)
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);
        }

        [HttpGet]
        public IHttpActionResult Customers(int id)
        {
            #region for returning domain model
            //var customer =  _contex.Customers.ToList().SingleOrDefault(c => c.Id == id);
            //if (customer == null)
            //    throw new HttpResponseException(HttpStatusCode.NotFound);

            //return customer;
            #endregion

            #region changes for Dto
            //var customer = _contex.Customers.ToList().SingleOrDefault(c => c.Id == id);
            //if (customer == null)
            //    throw new HttpResponseException(HttpStatusCode.NotFound);
            //var customerDto = Mapper.Map<Customer, CustomerDto>(customer);

            //return customerDto;
            #endregion

            #region for using IHttpActionResult
            var customer = _context.Customers.ToList().SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();
            var customerDto = Mapper.Map<Customer, CustomerDto>(customer);

            return Ok(customerDto);
            #endregion
        }

        [HttpPost]
        //public Customer Customer(Customer customer)
        public IHttpActionResult Customer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();
            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri + "/" + customerDto.Id), customerDto);
        }

        [HttpDelete]
        public int Customer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);


            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return customer.Id;
        }


        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(customerDto, customerInDb);

            _context.SaveChanges();
        }

    }
}
