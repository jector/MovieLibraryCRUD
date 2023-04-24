using System.Globalization;
using Microsoft.EntityFrameworkCore;
using MovieLibraryEntities.Context;
using System.Linq;
using MovieLibraryEntities.Models;


namespace MovieLibrary
{
    
    public class MainService : IMainService
    {
        
        private MovieContext _dbContext;
        private readonly IDbContextFactory<MovieContext> _dbContextFactory;

        //Default Constructor
        public MainService(IDbContextFactory<MovieContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _dbContext = _dbContextFactory.CreateDbContext();
        }
        
        public void Invoke()
        {
            string choice;
            do
            {
                Console.WriteLine("1. Search Movie");
                Console.WriteLine("2. Add Movie");
                Console.WriteLine("3. Update Movie");
                Console.WriteLine("4. Delete Movie");
                Console.WriteLine("x) Quit");
                choice = Console.ReadLine();
                
                if (choice == "1")
                {
                    GetMovie();
                }
                else if (choice == "2")
                {
                    AddMovie();
                }
                else if (choice == "3")
                {
                    Update();
                }
                else if (choice == "4")
                {
                    Delete();
                }
               
            } while (choice != "x");
        }
        //Example
        public void GetOccupation()
        {
             Console.WriteLine("Enter an occupation");
             var occupation = Console.ReadLine();
            
            var occupations = _dbContext.Occupations;

            Console.WriteLine("The occupation are :");
            foreach (var occ in occupations)
            {
                Console.WriteLine(occ.Name);
            }
        }
        
        public void GetMovie()
        {
            
            Console.WriteLine("Enter Movie Name or the first Letters: ");
            string mov = Console.ReadLine();
            
            var movie = _dbContext.Movies.Where(x => x.Title.Contains(mov) && x.Title.StartsWith(mov)).ToList();
            foreach (var mo in movie)
            {
                Console.WriteLine($"({mo.Id}). {mo.Title}");
            }
        }

        public void AddMovie()
        {
            Console.WriteLine("Enter new Movie: ");
            var addmovie = Console.ReadLine();
            Console.WriteLine("Enter movie year: ");
            var movieDate = Console.ReadLine();
            
            DateTime dt;
            DateTime.TryParseExact(movieDate, "yyyy", CultureInfo.CurrentCulture,
                DateTimeStyles.None, out dt);
                       /*
                       DateTime dt;
                       if(DateTime.TryParseExact(
                              movieDate, "yyyy", CultureInfo.CurrentCulture,
                              DateTimeStyles.None, out dt))
                       {
                           Console.WriteLine("New Date");
                       }
                       */
            Console.WriteLine($"{addmovie} ({movieDate}) date: {dt}");
            
            var movie = new Movie();
            movie.Title = addmovie + " (" + movieDate + ")";
            movie.ReleaseDate = dt;

            _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();
        }

        public void Update()
        {
            Console.WriteLine("Enter Movie Name to Update: ");
            var oldMovie = Console.ReadLine();
            Console.WriteLine("Enter Updated Movie Name: ");
            var moUpdate = Console.ReadLine();

            var updateMovie = _dbContext.Movies.FirstOrDefault(x => x.Title == oldMovie);
            Console.WriteLine($"({updateMovie.Id}) {updateMovie.Title}");

            updateMovie.Title = moUpdate;

            _dbContext.Movies.Update(updateMovie);
            _dbContext.SaveChanges();
        }

        public void Delete()
        {
            Console.WriteLine("Enter Movie Name to Delete: ");
            var moDelete = Console.ReadLine();
            
            var deleteMovie = _dbContext.Movies.FirstOrDefault(x => x.Title == moDelete);
            Console.WriteLine($"({deleteMovie.Id}) {deleteMovie.Title}");

            // verify exists first
            _dbContext.Movies.Remove(deleteMovie);
            _dbContext.SaveChanges();
        }
    }
}