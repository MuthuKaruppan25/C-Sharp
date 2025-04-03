using System;

namespace Task2
{
    class Person
    {
        public required string name { get; set; }
        public int age { get; set; }

        public virtual void introduce()
        {
            Console.WriteLine($"Hi, I am {name} and I'm {age} years old.");
        }
    }

    class Employee : Person
    {
        public required string role { get; set; }

        public override void introduce()
        {
            Console.WriteLine($"Hi, I am {name} and I'm {age} years old, and I work as a {role}.");
        }
    }

    class Student : Person
    {
        public required string grade { get; set; }

        public override void introduce()
        {
            Console.WriteLine($"Hi, I am {name} and I'm {age} years old, and I'm studying in {grade}.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            
            Person person = new Person { name = "Muthu", age = 21 };
            person.introduce();

            Employee employee = new Employee { name = "Gowtham", age = 30, role = "Software Engineer" };
            employee.introduce();

            Student student = new Student { name = "Mano", age = 18, grade = "12th Grade" };
            student.introduce();
        }
    }
}
