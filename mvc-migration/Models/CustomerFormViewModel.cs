using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_migration.Models
{
    public class CustomerFormViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public Customer Customer { get; set; }
    }
}