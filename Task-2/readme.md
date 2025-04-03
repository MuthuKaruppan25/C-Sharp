# Simple Object-Oriented Programming (OOP)

## Objective
The goal of this task is to implement basic object-oriented programming (OOP) concepts in C#. This involves creating a `Person` class, defining properties, implementing methods, and demonstrating polymorphism through inheritance.

## Requirements
- Create a `Person` class with properties `Name` and `Age`.
- Implement a method `Introduce()` that prints a personalized greeting.
- Extend the `Person` class to create `Employee` and `Student` classes with additional properties.
- Override the `Introduce()` method to provide customized messages.
- Instantiate and demonstrate object interaction in the `Main` method.

## Implementation

### Code Snippet
```csharp
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
```

## Explanation

### `Person` Class
- Contains two properties: `name` (required) and `age`.
- Implements the `Introduce()` method, which prints a basic introduction.

### `Employee` Class (Inheritance & Polymorphism)
- Inherits from `Person`.
- Introduces an additional property `role`.
- Overrides the `Introduce()` method to include job details.

### `Student` Class (Inheritance & Polymorphism)
- Inherits from `Person`.
- Introduces an additional property `grade`.
- Overrides the `Introduce()` method to include educational details.

### `Main` Method (Execution)
- Instantiates `Person`, `Employee`, and `Student` objects.
- Calls `Introduce()` on each to demonstrate polymorphism.

## Expected Output
```
Hi, I am Muthu and I'm 21 years old.
Hi, I am Gowtham and I'm 30 years old, and I work as a Software Engineer.
Hi, I am Mano and I'm 18 years old, and I'm studying in 12th Grade.
```

## Key Takeaways
- **Encapsulation**: Properties in the `Person` class.
- **Inheritance**: `Employee` and `Student` extend `Person`.
- **Polymorphism**: Method overriding in derived classes.

This project demonstrates fundamental OOP principles in C# with real-world applicability. 🚀

