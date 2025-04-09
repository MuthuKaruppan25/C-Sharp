using System;
using System.Reflection;

[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class RunnableAttribute : Attribute
{
    public string? Description;

    public RunnableAttribute(string? description = null)
    {
        Description = description;
    }
}


class TaskOne
{
    [Runnable("Runs task one")]
    public void Run()
    {
        Console.WriteLine("TaskOne.Run executed!");
    }

    public void noRun()
    {
        Console.WriteLine("TaskOne.Run not executed!");
    }
}

class TaskTwo
{
    [Runnable("Runs task two")]
    public void Run()
    {
        Console.WriteLine("TaskTwo.Run executed!");
    }

    public void noRun()
    {
        Console.WriteLine("TaskTwo.Run not executed!");
    }
}

class TaskThree
{
    [Runnable("Runs task three")]
    public void Run()
    {
        Console.WriteLine("TaskThree.Run executed!");
    }

    public void noRun()
    {
        Console.WriteLine("TaskThree.Run not executed!");
    }
}

class Program
{
    static void Main()
    {

        Assembly currentAssembly = Assembly.GetExecutingAssembly();

        foreach (Type type in currentAssembly.GetTypes())
        {
            foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
            {
                if (method.GetCustomAttribute<RunnableAttribute>() is RunnableAttribute attr)
                {
                    Console.WriteLine($"Found Runnable Method {method.Name} in {type.Name}");

                    if(!string.IsNullOrWhiteSpace(attr.Description))
                    {
                        Console.WriteLine($"Description: {attr.Description}");
                    }
                    object? ins = null;

                    if(!method.IsStatic)
                    {
                        ins = Activator.CreateInstance(type);
                    }

                    Console.WriteLine("Invoking");
                    method.Invoke(ins,null);
                }

            }
        }
    }
}