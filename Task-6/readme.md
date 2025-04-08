# 📚 Task 4: Working with Collections and LINQ in C#

## 📌 Objective
The objective of this task is to build a **Student Management Console Application** using C#. This application demonstrates the use of:

- **Collections** (`List<Student>`) to store and manage student data.
- **LINQ (Language Integrated Query)** for filtering and sorting data.
- **Console-based interaction** for user input and dynamic filtering.

---

## ✅ Requirements

- Create a `Student` class with properties: `Name`, `Age`, and `Grade`.
- Collect and store multiple student records using `List<Student>`.
- Allow filtering students based on:
  - A user-defined **age threshold**
  - A user-defined **grade threshold**
- Sort the filtered students by:
  - **Grade (descending)**
  - **Name (ascending)**
- Display the final list to the user.
- Continue filtering until the user exits.

---

## 🔍 How the Program Works

### 1. **Defining the Student Class**

A simple `Student` class holds:
- `name`: Student's name
- `age`: Student's age
- `grade`: Student's grade (as an integer)

---

### 2. **Collecting Student Data**

- The user is prompted to enter how many students they want to add.
- For each student, the user provides:
  - Name
  - Age
  - Grade
- Each student is stored as a `Student` object and added to the `students` list.

---

### 3. **Filtering and Sorting**

The program allows the user to choose how to filter students:

- **By Age**: Uses a LINQ *query expression* to:
  - Filter students whose age is greater than or equal to the threshold.
  - Sort them by grade (descending), then by name (ascending).

- **By Grade**: Uses a LINQ *method syntax* to:
  - Filter students whose grade is greater than the threshold.
  - Sort them by grade (descending), then by name (ascending).

---

### 4. **Loop and Exit**

- The application continuously prompts the user to filter by `age`, `grade`, or `exit`.
- Typing `exit` gracefully terminates the application.

---

## 🧠 Code Used

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

namespace Task4
{
    class Student
    {
        public string name { get; set; }
        public int age { get; set; }
        public int grade { get; set; }
    }

    class Program
    {
        static void Main(string[] args)  
        {
            List<Student> students = new List<Student>();

            Console.WriteLine("Enter the number of students to add:");
            string? no = Console.ReadLine();
            int tot = Convert.ToInt32(no);

            for (int i = 0; i < tot; i++)
            {
                Console.WriteLine("Enter name:");
                string? name = Console.ReadLine();
                Console.WriteLine("Enter age:");
                string? age = Console.ReadLine();
                int n_age = Convert.ToInt32(age);
                Console.WriteLine("Enter grade:"); 
                string? grade = Console.ReadLine();
                int n_grade = Convert.ToInt32(grade);
                Student student_data = new Student { name = name, age = n_age, grade = n_grade };
                students.Add(student_data);
            }

            while (true)
            {
                Console.WriteLine("Options: Filter the students by 'age' or 'grade' (threshold), or type 'exit' to quit:");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "age":
                        Console.WriteLine("Enter the age threshold:");
                        string? age_thres = Console.ReadLine();
                        int n_age_thres = Convert.ToInt32(age_thres);

                        var filteredStudentsByAge = from stud in students
                                                    where stud.age >= n_age_thres
                                                    orderby stud.grade descending, stud.name ascending
                                                    select stud;  

                        Console.WriteLine("\nStudents with age above " + n_age_thres + ":");
                        foreach (var student in filteredStudentsByAge)
                        {
                            Console.WriteLine($"{student.name} - Grade: {student.grade}, Age: {student.age}");
                        }
                        break;

                    case "grade":
                        Console.WriteLine("Enter the grade threshold:");
                        string? grade_thres = Console.ReadLine();
                        int n_grade_thres = Convert.ToInt32(grade_thres);

                        var filteredStudentsByGrade = students
                                                      .Where(student => student.grade > n_grade_thres)
                                                      .OrderByDescending(student => student.grade)
                                                      .ThenBy(student => student.name);

                        Console.WriteLine("\nStudents with grade above " + n_grade_thres + ":");
                        foreach (var student in filteredStudentsByGrade)
                        {
                            Console.WriteLine($"{student.name} - Grade: {student.grade}, Age: {student.age}");
                        }
                        break;

                    case "exit":
                        Console.WriteLine("Exiting the program.");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please enter 'age', 'grade', or 'exit'.");
                        break;
                }
            }
        }
    }
}
