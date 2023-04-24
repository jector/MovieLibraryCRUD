using System.Reflection.Metadata;
using MovieLibraryEntities.Context;
using Microsoft.Extensions.DependencyInjection;
using MovieLibrary;
using Microsoft.EntityFrameworkCore;

namespace MovieLibrary
{
    public class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var startup = new Startup();
                var serviceProvider = startup.ConfigureServices();
                var service = serviceProvider.GetService<IMainService>();

                service?.Invoke();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }
    }
}

