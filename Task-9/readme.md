# Reflection and Custom Attributes in C#

This project demonstrates how to use **Reflection** in C# to dynamically discover and invoke methods that are marked with a **custom attribute**. It is a powerful example of **runtime metadata analysis and execution** using attributes and the reflection API.

---

## 🧠 Objective

To build an application that:

- Defines a custom method-level attribute `[Runnable]`.
- Uses **reflection** to find methods across multiple classes marked with `[Runnable]`.
- Dynamically invokes those methods at runtime.
- Displays output and optional method descriptions.

---

## ✅ Features

- Custom Attribute: `[Runnable]`
- Reflection-based method discovery
- Dynamic instantiation and invocation
- Support for instance and static methods
- Console output of method metadata and results

---

## 🏷️ Custom Attribute Definition

```csharp
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class RunnableAttribute : Attribute
{
    public string? Description;

    public RunnableAttribute(string? description = null)
    {
        Description = description;
    }
}
```
---

## 🧩 Example Classes with [Runnable]

```csharp
class TaskOne
{
    [Runnable("Runs task one")]
    public void Run() => Console.WriteLine("TaskOne.Run executed!");
    
    public void noRun() => Console.WriteLine("TaskOne.Run not executed!");
}
```
Similar classes: TaskTwo, TaskThree, each with a [Runnable] method.

---

## 🔍 Reflection-Based Execution

The Main method uses reflection to:

- Load all types in the current assembly.
- Inspect their public instance/static methods.
- Identify methods marked with [Runnable].
- Create an instance of the class (if needed).
- Dynamically invoke the discovered method.

```csharp
Assembly currentAssembly = Assembly.GetExecutingAssembly();

foreach (Type type in currentAssembly.GetTypes())
{
    foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
    {
        if (method.GetCustomAttribute<RunnableAttribute>() is RunnableAttribute attr)
        {
            Console.WriteLine($"Found Runnable Method {method.Name} in {type.Name}");

            if (!string.IsNullOrWhiteSpace(attr.Description))
                Console.WriteLine($"Description: {attr.Description}");

            object? instance = method.IsStatic ? null : Activator.CreateInstance(type);
            
            Console.WriteLine("Invoking");
            method.Invoke(instance, null);
        }
    }
}
```
---

## 🧪 Sample Output

```Output
Found Runnable Method Run in TaskOne
Description: Runs task one
Invoking
TaskOne.Run executed!

Found Runnable Method Run in TaskTwo
Description: Runs task two
Invoking
TaskTwo.Run executed!

Found Runnable Method Run in TaskThree
Description: Runs task three
Invoking
TaskThree.Run executed!
```






