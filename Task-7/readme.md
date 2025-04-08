# Task 7: Asynchronous Programming and Multi-threading in C#

## 📌 Objective
The objective of this task is to build a **console application** that demonstrates the use of **asynchronous programming** and **multi-threading** using C#. This application simulates fetching data from multiple sources concurrently using:
- **`async` and `await`** for non-blocking operations
- **`Task` and `Task.WhenAll()`** for concurrent execution
- **Exception handling** for robustness

---

## ✅ Requirements
- Develop a `Console` application that performs **multiple asynchronous operations**.
- Simulate fetching data from **multiple sources** using `Task.Delay()`.
- Collect and display all results **after all tasks complete**.
- Calculate and display the **average price** from all sources.
- **Handle exceptions** that may arise during asynchronous execution.

---

## 🔍 How the Program Works

### 1. **Simulating Data Fetching**
- The application includes three asynchronous methods:
  - `FetchDatafromSource1Async()`
  - `FetchDatafromSource2Async()`
  - `FetchDatafromSource3Async()`
- Each method simulates fetching data by:
  - Printing the source it’s fetching from
  - Delaying execution with `Task.Delay()`
  - Returning a tuple containing the source name and a price

- One of the sources (Source 3) can randomly **throw an exception** to simulate a failure.

### 2. **Running Tasks Concurrently**
- All three methods are called and stored in a list of tasks.
- `Task.WhenAll()` is used to run all tasks **in parallel**.
- Once all tasks are complete, the results are aggregated and displayed.
- The program also computes and displays the **average price** from all sources.

### 3. **Exception Handling**
- The `try-catch` block in the `Main()` method ensures that any errors encountered during asynchronous operations are caught and displayed.

---

## 🧠 Code Used

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                Console.WriteLine("All Tasks Completed Successfully");

                foreach (var (source, price) in results)
                {
                    Console.WriteLine($"{source} : {price}");
                }
                var average = results.Average(res => res.price);
                Console.WriteLine($"Average Price: {average}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
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
                throw new Exception("Source 3 failed unexpectedly.");
            return ("Source 3", 89.0);
        }
    }
}
```