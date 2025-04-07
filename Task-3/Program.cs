using System;
using System.Collections.Generic;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> list = new List<string>();

            while (true)
            {
                Console.WriteLine("\nOptions: ADD / REMOVE / DISPLAY / EXIT");
                Console.Write("Enter Your choice: ");
                string? choice = Console.ReadLine()?.Trim().ToUpper();

                switch (choice)
                {
                    case "ADD":
                        Console.Write("Enter item to add: ");
                        string? item = Console.ReadLine()?.Trim();

                        if (item is not null and { Length: > 0 })
                        {
                            list.Add(item);
                            Console.WriteLine($"'{item}' added to the list.");

                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Item cannot be empty.");
                        }
                        break;

                    case "REMOVE":
                        if (list.Count == 0)
                        {
                            Console.WriteLine("The list is empty.");
                            break;
                        }

                        Console.Write("Enter item name or index to remove: ");
                        string? input = Console.ReadLine()?.Trim();

                        if (int.TryParse(input, out int index) && index >= 0 && index < list.Count)
                        {
                            Console.WriteLine($"'{list[index]}' removed from the list.");
                            list.RemoveAt(index);
                        }
                        else if (list.Remove(input!)) 
                        {
                            Console.WriteLine($"'{input}' removed from the list.");
                        }
                        else
                        {
                            Console.WriteLine($"'{input}' not found or invalid input.");
                        }
                        break;

                    case "DISPLAY":
                        if (list.Count == 0)
                        {
                            Console.WriteLine("The list is empty.");
                        }
                        else
                        {
                            Console.WriteLine("Current List:");
                            foreach (string list_item in list)
                            {
                                Console.WriteLine(list_item); 
                            }
                        }
                        break;

                    case "EXIT":
                        Console.WriteLine("Exiting the program.");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please enter ADD, REMOVE, DISPLAY, or EXIT.");
                        break;
                }
            }
        }
    }
}
