# Factorial Calculator

## Objective
This project is a simple console application that calculates the factorial of a given number in C#. The program provides two approaches:
1. Using a loop (iterative method)
2. Using recursion

## Requirements
- Read an integer from the user.
- Validate the input (ensure it is a positive integer).
- Compute the factorial using both a loop and recursion.
- Display the results in the console.

## Code Explanation

### 1. Reading and Validating User Input
```csharp
Console.Write("Enter a positive integer: ");
string? num = Console.ReadLine();
number = Convert.ToInt32(num);
```
- The program prompts the user to enter a number.
- `Console.ReadLine()` reads the input as a string.
- The string is converted to an integer using `Convert.ToInt32()`.
- (Note: This implementation does not handle exceptions for invalid input, which can be improved.)

### 2. Checking for Valid Input
```csharp
if(number >= 0){
    long facLoop = calFactLoop(number);
    Console.WriteLine($"Factorial using Loop: {facLoop}");
    long facRec = calFactRec(number);
    Console.WriteLine($"Factorial using Recursion: {facRec}");
}
```
- If the number is non-negative, it proceeds to compute the factorial using both methods.
- Calls `calFactLoop()` and `calFactRec()` functions and prints the results.

### 3. Iterative Approach (Using Loop)
```csharp
static long calFactLoop(int num){
    long result = 1;
    for(int i=1; i<=num; i++){
        result *= i;
    }
    return result;
}
```
- Uses a `for` loop to multiply numbers from 1 to `num`.
- Stores the computed factorial in `result` and returns it.

### 4. Recursive Approach
```csharp
static long calFactRec(int num){
    if(num == 1 || num == 0){
        return 1;
    }
    return num * calFactRec(num-1);
}
```
- If `num` is 0 or 1, returns 1 (base case).
- Otherwise, calls itself with `num-1` and multiplies `num` with the result (recursive case).

## Running the Program
1. Open a terminal/command prompt.
2. Run the code:
   ```sh
   dotenv run
   ```
3. Enter a positive integer when prompted.
4. View the calculated factorial using both methods.



