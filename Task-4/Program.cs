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
