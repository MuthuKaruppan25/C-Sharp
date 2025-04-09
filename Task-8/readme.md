# Generic Repository Pattern with Interfaces in C#

This project demonstrates how to implement **Generics** and **Interfaces** using the **Repository Pattern** in C#. The goal is to create a **generic in-memory repository** capable of performing basic CRUD operations on any entity class that implements a common interface.

---

## 🧠 Objective

To build a reusable, generic repository that:

- Supports CRUD operations.
- Enforces structure using interfaces.
- Utilizes type constraints to ensure safety and flexibility.
- Demonstrates functionality through a console-based UI with a sample entity.

---

## ✅ Features

- Generic repository interface (`IRepository<T>`)
- Type-safe operations using `where T : IEntity`
- In-memory list-based data storage
- Console UI for interaction
- Sample entity: `Student`

---

## 📦 Repository Implementation

```csharp
public class PublicRepository<T> : IRepository<T> where T : IEntity
{
    private List<T> items = new List<T>();

    public void Add(T item) => items.Add(item);
    public T? Get(int id) => items.FirstOrDefault(x => x.Id == id);
    public IEnumerable<T> GetAll() => items;
    public void Update(T item)
    {
        var index = items.FindIndex(x => x.Id == item.Id);
        if (index >= 0) items[index] = item;
    }
    public void Delete(int id)
    {
        var item = Get(id);
        if (item != null) items.Remove(item);
    }
}
```

---
## 💻 Interface Definitions

```csharp
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
```
---

## 🧪 Sample Entity: Student

```csharp
public class Student : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}
```
---

