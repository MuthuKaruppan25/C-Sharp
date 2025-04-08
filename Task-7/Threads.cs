using System;

namespace AsyncOperations
{
    class Threads
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
                var results = new List<(string source, double price)>();

                while (tasks.Any())
                {
                    Task<(string source, double price)> finishedTask = await Task.WhenAny(tasks);
                    tasks.Remove(finishedTask);

                    try
                    {
                        var result = await finishedTask;
                        Console.WriteLine($"{result.source} : {result.price}");
                        results.Add(result);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"A Task Failed : {ex.Message}");
                    }
                }
                if (results.Any())
                {
                    var average = results.Average(r => r.price);
                    Console.WriteLine($"Average Price: {average}");
                }
                else
                {
                    Console.WriteLine("No successful responses to average price");
                }

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
            Console.WriteLine("Fetching Data from source 2");
            await Task.Delay(1300);
            return ("Source 2", 89.0);
        }
        static async Task<(string source, double price)> FetchDatafromSource3Async()
        {
            Console.WriteLine("Fetching Data from source 3");
            await Task.Delay(1500);
            if (new Random().Next(0, 3) == 0)
                throw new Exception("Source C failed unexpectedly.");
            return ("Source 3", 89.0);
        }
    }
}