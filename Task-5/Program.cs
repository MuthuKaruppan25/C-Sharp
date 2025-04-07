using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Task5
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = "input.txt";
            string outputFilePath = "output.csv";
            string logFilePath = "log.txt";

            try
            {
                int totalWordCount = 0, totalLineCount = 0;
                Dictionary<string, int> freq = new Dictionary<string, int>();

                using (StreamReader reader = new StreamReader(inputFilePath))
                using (StreamWriter writer = new StreamWriter(outputFilePath, false, Encoding.UTF8))
                {
                    writer.WriteLine("Line Number, Word Count");

                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        totalLineCount++;
                        string[] words = line.Split(new char[] { ' ', ',', '.', '!', '?', ';', ':', '-', '\t' },
                                                    StringSplitOptions.RemoveEmptyEntries);
                        int wordcount = words.Length;
                        totalWordCount += wordcount;

                        foreach (string word in words)
                        {
                            if (freq.ContainsKey(word))
                            {
                                freq[word]++;
                            }
                            else
                            {
                                freq[word] = 1;
                            }
                        }
                        writer.WriteLine($"{totalLineCount},{wordcount}");
                    }
                }

                Console.WriteLine($"Total Lines Processed: {totalLineCount}");
                Console.WriteLine($"Total Words Counted: {totalWordCount}");

                // Write word frequency data to file
                string freqFilePath = "freq.csv";
                using (StreamWriter freqWriter = new StreamWriter(freqFilePath, false, Encoding.UTF8))
                {
                    freqWriter.WriteLine("Word, Frequency");
                    foreach (var entry in freq)
                    {
                        freqWriter.WriteLine($"{entry.Key},{entry.Value}");
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                LogFile(logFilePath, $"File Not Found: {e.Message}");
            }
            catch (IOException e)
            {
                LogFile(logFilePath, $"IO Exception: {e.Message}");
            }
            catch (Exception e)
            {
                LogFile(logFilePath, $"Unexpected Error: {e.Message}");
            }
        }

        static void LogFile(string logPath, string msg)
        {
            Console.WriteLine("Error: " + msg);
            File.AppendAllText(logPath, $"{DateTime.Now}: {msg}\n");
        }
    }
}
