using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mvc_migration.Models;

namespace mvc_migration.Models
{
    public class MovieFormViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }
        public Movie Movie { get; set; }
    }
}