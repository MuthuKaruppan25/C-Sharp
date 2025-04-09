using System;

public interface IEntity
{
    int Id { get; set; }

}
public interface IRepository<T> where T : IEntity
{
    void Add(T item);
    T? Get(int id);
    IEnumerable<T> GetAll();
    void Update(T item);
    void Delete(int id);
}

public class PublicRepository<T> : IRepository<T> where T : IEntity
{

    private List<T> items;

    public PublicRepository()
    {
        items = new List<T>();
    }

    public IEnumerable<T> GetAll()
    {
        return items;
    }

    public void Add(T item)
    {
        items.Add(item);
    }

    public T? Get(int id)
    {
        return items.FirstOrDefault(x => x.Id == id);
    }

    public void Update(T item)
    {
        var index = items.FindIndex(x => x.Id == item.Id);
        if (index >= 0)
        {
            items[index] = item;
        }
    }

    public void Delete(int id)
    {
        var item = Get(id);
        if (item != null)
        {
            items.Remove(item);
        }
    }

}

public class Student : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        IRepository<Student> repo = new PublicRepository<Student>();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Student Repository");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. View All Students");
            Console.WriteLine("3. View Student by ID");
            Console.WriteLine("4. Update Student");
            Console.WriteLine("5. Delete Student");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");


            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Enter ID:");
                    int id = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter name:");
                    string? name = Console.ReadLine();
                    Console.WriteLine("Enter age:");
                    int age = int.Parse(Console.ReadLine());

                    repo.Add(new Student { Id = id, Name = name, Age = age });
                    Console.WriteLine("Student Added Successfully");
                    break;

                case "2":
                    var students = repo.GetAll();
                    foreach (var s in students)
                    {
                        Console.WriteLine($"ID: {s.Id}, Name: {s.Name}, Age: {s.Age}");
                    }
                    break;

                case "3":
                    Console.Write("Enter Student ID: ");
                    int sid = int.Parse(Console.ReadLine()!);
                    var student = repo.Get(sid);
                    if (student != null)
                        Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Age: {student.Age}");
                    else
                        Console.WriteLine("Student not found.");
                    break;

                case "4":
                    Console.Write("Enter ID to update: ");
                    int uid = int.Parse(Console.ReadLine());

                    var existing = repo.Get(uid);

                    if (existing != null)
                    {
                        Console.Write("Enter new Name: ");
                        existing.Name = Console.ReadLine()!;
                        Console.Write("Enter new Age: ");
                        existing.Age = int.Parse(Console.ReadLine()!);
                        repo.Update(existing);
                        Console.WriteLine("Student Updated");
                    }
                    else
                    {
                        Console.WriteLine("Student not found.");
                    }
                    break;
                case "5":
                    Console.Write("Enter ID to Delete: ");
                    int kid = int.Parse(Console.ReadLine());
                    repo.Delete(kid);
                    Console.WriteLine("Student Deleted");
                    break;
                case "6":
                    exit = true;
                    return;
                default:
                    Console.WriteLine("Enter Valid Choice");
                    break;

            }

        }

    }
}