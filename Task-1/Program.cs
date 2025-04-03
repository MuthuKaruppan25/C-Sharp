// See https://aka.ms/new-console-template for more information

using System;

namespace Helloworld{
    class Factorial{
        static void Main(string[] args)
        {
            int number;
            Console.Write("Enter a positive integer: ");
            string? num = Console.ReadLine();


            number = Convert.ToInt32(num);
            
            if(number >= 0){
                long facLoop = calFactLoop(number);
                Console.WriteLine($"Factorial using Loop: {facLoop}");
                long facRec = calFactRec(number);
                Console.WriteLine($"Factorial using Recursion: {facRec}");
            }
        }
        static long calFactLoop(int num){
            long result = 1;
            for(int i=1;i<=num;i++){
                result *= i;
            }
            return result;
        }

        static long calFactRec(int num){
            if(num == 1 || num == 0){
                return 1;
            }
            return num * calFactRec(num-1);
        }
    }
}