using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Authentication;
using MovieLib;

namespace MovieRestAPI.Managers {
    public class MovieManagers{
        private static int nextId = 1; 
        private static List<Movie> moviesList = new List<Movie>(){
            new Movie(){Country = "Korea", Id = nextId++, LengthInMinutes = 123, Name = "Train To Busan"},
            new Movie(){Country = "Japan", Id = nextId++, LengthInMinutes = 184, Name = "Your Name"},
            new Movie(){Country = "United Kingdom", Id = nextId++, LengthInMinutes = 191, Name = "Harry Potter"},
            new Movie(){Country = "Denmark", Id = nextId++, LengthInMinutes = 141, Name = "Druk"},
            new Movie(){Country = "USA", Id = nextId++, LengthInMinutes = 231, Name = "Star Wars"},
        };

        public IEnumerable<Movie> GetMovies(int? filter){
            var result = moviesList;
            if (filter != null){
                result = result.FindAll(m => m.LengthInMinutes < filter);
            }
            return result;
        }

        public Movie GetById(int id){
            var result = moviesList.Find(m => m.Id == id);
            return result;
        }

        public Movie AddMovie(Movie newMovie){
            newMovie.Id = nextId++;
            moviesList.Add(newMovie);
            return newMovie;
        }

        public Movie DeleteMovie(int id){
            var index = moviesList.FindIndex(m => m.Id == id);
            var movie = moviesList[index];
            moviesList.Remove(moviesList[index]);
            return movie;
        }

        public Movie UpdateMovie(int id, Movie updatedMovie){
            var index = moviesList.FindIndex(m => m.Id == id);
            updatedMovie.Id = id;
            moviesList[index] = updatedMovie;
            return updatedMovie;
        }
    }
}
 