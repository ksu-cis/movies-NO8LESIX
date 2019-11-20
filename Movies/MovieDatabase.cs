using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Movies
{
    /// <summary>
    /// A class representing a database of movies
    /// </summary>
    public static class MovieDatabase
    {
        private static List<Movie> movies;

        public static List<Movie> All
        {
            get
            {
                if (movies == null)
                {
                    using (StreamReader file = System.IO.File.OpenText("movies.json"))
                    {
                        string json = file.ReadToEnd();
                        movies = JsonConvert.DeserializeObject<List<Movie>>(json);
                    }
                }
                return movies;
            }
        }

        public static List<Movie> SearchAndFilter(String search, List<string> rating)
        {
            //Case 0: Search string only
            if (search == null && rating.Count == 0)
                return All;

            List<Movie> matchingMovies = new List<Movie>();

            foreach (Movie movie in movies)
            {
                //Case 2: Both
                if (rating.Count > 0 && search != null)
                {
                    if (movie.Title != null
                        && movie.Title.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                        && rating.Contains(movie.MPAA_Rating))
                    {
                        matchingMovies.Add(movie);
                    }
                }
                //Case 1: Search string only
                else if (search != null)
                {
                    if (movie.Title != null && movie.Title.Contains(search, StringComparison.InvariantCultureIgnoreCase))
                    {
                        matchingMovies.Add(movie);
                    }
                }
                //Case 3: Rating string only
                else if (rating.Count > 0)
                {
                    if (rating.Contains(movie.MPAA_Rating))
                    {
                        matchingMovies.Add(movie);
                    }
                }
            }
            return matchingMovies;
        }

        public static List<Movie> Search(List<Movie> movies, string searching)
        {
            List<Movie> results = new List<Movie>();
            foreach (Movie movie in movies)
            {
                if (movie.Title != null && movie.Title.Contains(searching, StringComparison.InvariantCultureIgnoreCase))
                {
                    results.Add(movie);
                }
            }
            return results;
        }

        public static List<Movie> FilterByMPAA(List<Movie> movies, List<string> rating)
        {
            List<Movie> results = new List<Movie>();
            foreach (Movie mov in movies)
            {
                if (rating.Contains(mov.MPAA_Rating))
                {
                    results.Add(mov);
                }
            }
            return results;
        }
        public static List<Movie> FilterByMinIMDB(List<Movie> movies, float min)
        {
            List<Movie> results = new List<Movie>();
            foreach (Movie movie in movies)
            {
                if (movie.IMDB_Rating != null && movie.IMDB_Rating >= min)
                {
                    results.Add(movie);
                }
            }
            return results;
        }
        public static List<Movie> FilterByMaxIMDB(List<Movie> movies, float max)
        {
            List<Movie> results = new List<Movie>();
            foreach (Movie movie in movies)
            {
                if (movie.IMDB_Rating != null && movie.IMDB_Rating <= max)
                {
                    results.Add(movie);
                }
            }
            return results;
        }

    }
}
