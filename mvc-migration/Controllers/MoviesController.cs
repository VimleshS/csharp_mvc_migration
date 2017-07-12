using mvc_migration.Models;
using mvc_migration.ViewModels;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace mvc_migration.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            //var movies = _context.Movies.Include(m => m.Genre);
            //return View(movies);

            //Render View as per required.
            if (!User.IsInRole(RoleName.CanManageMovies))
                return View("IndexReadOnlyView");

            return View();
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).FirstOrDefault(m => m.Id == id);
            return View(movie);
        }

        public ActionResult New()
        {
            var movieFormViewModel = new MovieFormViewModel()
            {
                Genres = _context.Genre.ToList(),
                Movie = new Movie()
            };
            return View("MovieForm", movieFormViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var movieFormViewModel = new MovieFormViewModel()
                {
                    Genres = _context.Genre.ToList(),
                    Movie = movie
                };
                return View("MovieForm", movieFormViewModel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieIndb = _context.Movies.SingleOrDefault(m => m.Id == movie.Id);
                movieIndb.Name = movie.Name;
                movieIndb.Genre = movie.Genre;
                movieIndb.ReleaseDate = movie.ReleaseDate;
                movieIndb.NumberInStock = movie.NumberInStock;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.Include(c => c.Genre)
                .SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            var movieFormViewModel = new MovieFormViewModel()
            {
                Movie = movie,
                Genres = _context.Genre.ToList()

            };
            return View("MovieForm", movieFormViewModel);
        }

        // GET: Movie
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek" };
            var customers = new List<Customer>() {
                new Customer() { Name = "Cusomer 1" },
                new Customer() { Name = "Customer 2" }
            };
            var randomViewModel = new RandomViewModel()
            {
                Movies = movie,
                Customers = customers
            };
            return View(randomViewModel);
        }
    }
}