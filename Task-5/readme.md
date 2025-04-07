# Task 5 – File I/O and Exception Handling in C#

## 📋 Objective

This project demonstrates how to perform **file input/output (I/O)** operations and implement **exception handling** in C#. The application reads data from a text file, processes the content (counts words and lines), writes results to a CSV file, and logs any errors encountered during the process.

---

## 🛠 Requirements Implemented

- ✅ Read text from a file (`input.txt`)
- ✅ Process the data (count total lines, words per line, and word frequencies)
- ✅ Write results to new CSV files (`output.csv` and `freq.csv`)
- ✅ Log file-related errors using exception handling (`log.txt`)

---

## 📂 Project Files

| File Name     | Purpose |
|---------------|---------|
| `input.txt`   | Input file containing text data to be processed |
| `output.csv`  | Output file containing line-wise word count |
| `freq.csv`    | Output file containing word frequencies |
| `log.txt`     | Log file for capturing any exceptions or errors |
| `Program.cs`  | Main application code with file processing and exception handling logic |

---

## 🧾 Code Explanation

### 1. **File Reading (`StreamReader`)**
- Reads content line by line from `input.txt`.
- Each line is split into words using multiple delimiters (spaces, punctuation, etc.).

### 2. **Data Processing**
- **Line Count**: Tracks the number of lines read.
- **Word Count**: Counts the number of words in each line.
- **Word Frequency**: Maintains a dictionary to count occurrences of each word.

### 3. **Writing to Output Files**
- `output.csv`: Stores line number and word count per line.
- `freq.csv`: Stores each word and its frequency.

### 4. **Exception Handling**
- Uses `try-catch` blocks to catch and handle:
  - `FileNotFoundException`: If `input.txt` doesn't exist.
  - `IOException`: For other I/O-related issues.
  - Generic `Exception`: For any unexpected runtime errors.
- Errors are logged with a timestamp to `log.txt`.

### 5. **Logging Utility**
```csharp
static void LogFile(string logPath, string msg)
{
    Console.WriteLine("Error: " + msg);
    File.AppendAllText(logPath, $"{DateTime.Now}: {msg}\n");
}
