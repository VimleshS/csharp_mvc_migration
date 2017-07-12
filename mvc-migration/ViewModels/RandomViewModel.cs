using mvc_migration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_migration.ViewModels
{
    public class RandomViewModel
    {
        public Movie Movies { get; set; }
        public List<Customer> Customers { get; set; }
    }
}