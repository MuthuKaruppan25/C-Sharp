# Task 6: Delegates, Events, and Basic Event Handling in C#

## 📌 Objective
The objective of this task is to build a **console-based event-driven application** in C#. This task demonstrates:
- The use of **delegates** and **events**
- Creating custom **EventArgs**
- Subscribing multiple **event handlers**
- Showing how **event-driven logic** decouples producers from consumers

---

## ✅ Requirements
- Create a console app that mimics a counter triggering an event when a threshold is reached.
- Define:
  - A **delegate** and an **event**
  - A **custom EventArgs class**
- Trigger the event when the counter exceeds a set value.
- Attach multiple **event handler methods** to the event:
  - One logs to console.
  - Another writes to a file.
- Use a loop to simulate user interactions that increase the counter.
- Let users exit by typing `"exit"`.

---

## 🔍 How the Program Works

### 1. **Custom EventArgs Class**
`Thresholdeventargs` inherits from `EventArgs` and stores:
- The threshold value
- The exact time the threshold was reached

### 2. **Counter Class**
Encapsulates:
- A count variable
- A predefined threshold
- An event `ThresholdReached` that uses `EventHandler<T>`

It contains:
- `Add()` method to increase the count
- `OnThresholdReached()` method that **invokes the event** when the count crosses the threshold

### 3. **Event Handlers**
Two handlers demonstrate the **subscriber (consumer)** logic:
- One logs a message to the console
- One appends the same message to a file

### 4. **Main Application Loop**
- Prompts the user to press Enter to simulate an action.
- Each action increments the counter.
- Event gets fired if the threshold is met or exceeded.
- The user can type `"exit"` to terminate.

---

## 🧠 Code Used

```csharp
using System;
using System.IO;

public class Thresholdeventargs : EventArgs
{
    public int threshold { get; set; }
    public DateTime reached { get; set; }
}

public class Counter
{
    private int count;
    private int threshold;

    public Counter(int thres)
    {
        threshold = thres;
    }

    public event EventHandler<Thresholdeventargs>? ThresholdReached;

    public void Add(int x)
    {
        count += x;
        Console.WriteLine($"[Counter] Current count: {count}");
        if (count >= threshold)
        {
            OnthresholdReached();
        }
    }

    void OnthresholdReached()
    {
        ThresholdReached?.Invoke(this, new Thresholdeventargs 
        { 
            threshold = this.threshold, 
            reached = DateTime.Now 
        });
    }
}

class Program
{
    static void Main(string[] args)
    {
        Counter counter = new Counter(5);

        counter.ThresholdReached += LogtoConsole;
        counter.ThresholdReached += LogFileWriter;

        Console.WriteLine("Press Enter to simulate button click. Type 'exit' to quit.");

        while (true)
        {
            string? input = Console.ReadLine();
            if (input?.ToLower() == "exit") break;

            counter.Add(1);
        }

        Console.WriteLine("Program ended.");
    }

    static void LogtoConsole(object? sender, Thresholdeventargs e)
    {
        Console.WriteLine($"Logger: Threshold {e.threshold} reached at {e.reached}");
    }

    static void LogFileWriter(object? sender, Thresholdeventargs e)
    {
        string logMessage = $"[File Logger] Threshold {e.threshold} reached at {e.reached}";
        File.AppendAllText("threshold_log.txt", logMessage + Environment.NewLine);
    }
}
```

---