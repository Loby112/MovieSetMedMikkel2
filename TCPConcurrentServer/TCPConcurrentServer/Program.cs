using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MovieLib;

namespace TCPConcurrentServer {
    internal class Program{
        private static string url = "https://movierestapitobiasmikkel.azurewebsites.net/api/movies";
        private static List<Movie> movieList = new List<Movie>(){
            new Movie(){Country = "TestLand", Id = 1, LengthInMinutes = 123, Name = "Train To Busan"},
            new Movie(){Country = "Tesstlanddds", Id = 2, LengthInMinutes = 184, Name = "Your Name"},
            new Movie(){Country = "United Kingdom", Id = 3, LengthInMinutes = 191, Name = "Harry Potter"},
            new Movie(){Country = "Hygge", Id = 4, LengthInMinutes = 141, Name = "Druk"},
            new Movie(){Country = "Denmark", Id = 6, LengthInMinutes = 111, Name = "Olsen Banden"},
            new Movie(){Country = "Fiskeland", Id = 5, LengthInMinutes = 231, Name = "Star Wars"},
        };  
        static void Main(string[] args){
            TcpListener listener = new TcpListener(IPAddress.Any, 43214);

            listener.Start();

            while (true){

                TcpClient socket = listener.AcceptTcpClient();

                Task.Run(() => {
                    HandleClient(socket);
                });
            }
        }


        public static void HandleClient(TcpClient socket){

            NetworkStream ns = socket.GetStream();

            StreamReader reader = new StreamReader(ns);

            StreamWriter writer = new StreamWriter(ns);

            string message = reader.ReadLine();
            
            
            //APIMovies();

            Thread.Sleep(500);

            if (message == "GetAllMovies"){
                string movies = GetAllMovies();
                writer.WriteLine(movies);
            }
            else if (message.StartsWith("GetByCountry")){
                string moviesByCountry = GetByCountry(message);
                writer.WriteLine(moviesByCountry);
            }
            else{
                writer.WriteLine("Type GetAllCountries or GetByCountry [country you want])");
            }

            writer.Flush();
            
            writer.Close();

        }

        static string GetAllMovies(){
            string serializedData = JsonSerializer.Serialize(movieList);
            return serializedData;
        }

        static string GetByCountry(string country){
            string[] words = country.Split(" ");
            var result = movieList.FindAll(m => m.Country.ToLower() == words[1].ToLower());
            string serializedData = JsonSerializer.Serialize(result);
            return serializedData;
        }

        async static void APIMovies(){
            using (HttpClient client = new HttpClient()){
                var result = await client.GetAsync(url);
                movieList = await result.Content.ReadFromJsonAsync<List<Movie>>();
            }
        }

    }
}
