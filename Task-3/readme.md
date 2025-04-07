# Task 3: Basic Collections and String Manipulation in C#

## 📌 Objective
The goal of this task is to practice working with collections (specifically `List<string>`) and apply basic string manipulation techniques using C#. Users should be able to interactively add, remove, and view a list of string items like names or tasks. This program emphasizes usage of:
- `List<string>` for dynamic data storage,
- Loop constructs for continuous user interaction,
- String methods like `Trim()` and `ToUpper()` to handle and process input.

---

## ✅ Requirements
- Use a `List<string>` to maintain a list of items.
- Implement options for users to **Add**, **Remove**, **Display**, or **Exit** the list.
- Handle user input using `Trim()` (to remove unwanted spaces) and `ToUpper()` (for case-insensitive command matching).
- Validate input and handle incorrect or edge-case scenarios gracefully.

---

## 🔍 How the Program Works

### 1. **User Interaction Loop**
A `while (true)` loop runs until the user decides to exit. Inside the loop, the program:
- Prompts for a command (ADD, REMOVE, DISPLAY, EXIT),
- Processes the input after trimming spaces and converting to uppercase for case-insensitive matching,
- Uses a `switch` statement to handle commands.

### 2. **Adding Items**
When the user types `ADD`:
- They're prompted to input the item name.
- Input is trimmed and validated to ensure it's not empty.
- If valid, the item is added to the list.

### 3. **Removing Items**
When the user types `REMOVE`:
- The user can input either the item name or its index.
- If a valid index is entered, the item at that index is removed.
- If a name is entered, the item is searched and removed if found.
- Appropriate messages are shown for invalid or empty inputs.

### 4. **Displaying Items**
When the user types `DISPLAY`:
- If the list is empty, it notifies the user.
- Otherwise, it prints all items in the list.

### 5. **Exiting**
Typing `EXIT` ends the program gracefully.

---

## 🧠 Code Used

```csharp
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
