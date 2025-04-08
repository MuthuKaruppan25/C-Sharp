using System;

namespace AsyncOperations
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Fetching data from all the sources:");
            try
            {
                List<Task<(string source, double price)>> tasks = new List<Task<(string source, double price)>> {
                FetchDatafromSource1Async(),
                FetchDatafromSource2Async(),
                FetchDatafromSource3Async()
            };

                var results = await Task.WhenAll(tasks);
                results.ToList();
                Console.WriteLine("All Tasks Completed Successfully");

                foreach (var (source, price) in results)
                {
                    Console.WriteLine($"{source} : {price}");
                }
                var average = results.Average(res => res.price);
                Console.WriteLine($"Average Price:{average}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured:{ex.Message}");
            }

        }

        static async Task<(string source, double price)> FetchDatafromSource1Async()
        {
            Console.WriteLine("Fetching Data from source 1");
            await Task.Delay(1000);
            return ("Source 1", 89.0);
        }
        static async Task<(string source, double price)> FetchDatafromSource2Async()
        {
            Console.WriteLine("Fetching Data from source 1");
            await Task.Delay(1300);
            return ("Source 2", 89.0);
        }
        static async Task<(string source, double price)> FetchDatafromSource3Async()
        {
            Console.WriteLine("Fetching Data from source 1");
            await Task.Delay(1500);
            if (new Random().Next(0, 3) == 0)
                throw new Exception("Source C failed unexpectedly.");
            return ("Source 3", 89.0);
        }

    }
}