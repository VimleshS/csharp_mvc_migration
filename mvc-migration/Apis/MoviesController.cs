using AutoMapper;
using mvc_migration.Dtos;
using mvc_migration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace mvc_migration.Apis
{
    public class MoviesController : ApiController
    {
        ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        #region Synchronised version of GetMovies
        [ResponseType(typeof(IEnumerable<MovieDto>))]
        public IHttpActionResult GetMovies()
        {
            var moviesdto = _context.Movies
                .Include(m => m.Genre)
                .Select(Mapper.Map<Movie, MovieDto>);
            return Ok(moviesdto);
        }
        #endregion

        #region Async Version
        //[ResponseType(typeof(IEnumerable<MovieDto>))]
        //public async Task<IHttpActionResult> GetMovies()
        //{
        //   var moviesdto = _context.Movies.Select(Mapper.Map<Movie, MovieDto>);
        //   return Ok(moviesdto);
        //}
        #endregion


        public IHttpActionResult GetMovies(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                NotFound();

            var moviesdto = Mapper.Map<Movie, MovieDto>(movie);
            return Ok(moviesdto);
        }

        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult CreateMovies(MovieDto moviedto)
        {
            if (!ModelState.IsValid)
                BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(moviedto);
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return Ok(movie);
        }

        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public int Delete(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);


            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return movie.Id;
        }

        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                BadRequest();

            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                NotFound();

            Mapper.Map<MovieDto, Movie>(movieDto, movie);
            _context.SaveChanges();

            return Ok(movie);
        }


    }
}
