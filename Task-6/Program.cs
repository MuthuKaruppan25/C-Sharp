using System;

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
        ThresholdReached?.Invoke(this, new Thresholdeventargs { threshold = this.threshold, reached = DateTime.Now });
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