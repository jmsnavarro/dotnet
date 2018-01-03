using System;
using System.Collections.Generic;

//C# Quick start: Collections
//https://docs.microsoft.com/en-us/dotnet/csharp/quick-starts/arrays-and-collections

namespace list_quickstart
{
    class Program
    {
        static void Main(string[] args)
        {
            //topic 4: New method
            WorkingWithStrings();

            //topic: List of other types
            Console.WriteLine();
            var fibonacciNumbers = new List<int> {1, 1};

            var previous = fibonacciNumbers[fibonacciNumbers.Count - 1];
            var previous2 = fibonacciNumbers[fibonacciNumbers.Count - 2];

            fibonacciNumbers.Add(previous + previous2);

            foreach (var item in fibonacciNumbers)
            {
                Console.WriteLine(item);
            }
        }

        public static void WorkingWithStrings()
        {
            //topic 1: Lists and Interpolated Strings
            var names = new List<string> {"<name>", "Ana", "Felipe"};
            
            // 1st set
            Console.WriteLine("\n1st set:");
            foreach (var name in names)
            {
                // uses interpolated strings and same as the next
                Console.WriteLine($"Hello {name.ToUpper()}!");        
                //Console.WriteLine("Hello " + name.ToUpper() + "!");        
            }
            
            //2nd set
            Console.WriteLine("\n2nd set:");
            names.Add("Maria");
            names.Add("Bill");
            names.Remove("Ana");
            
            foreach (var name in names)
            {
                Console.WriteLine($"Hello {name.ToUpper()}!");
            }

            //topic 2: Modify list contents
            //show specific items by index
            Console.WriteLine();
            Console.WriteLine($"My name is {names[1]}.");
            Console.WriteLine($"I've added {names[3]} and {names[2]} in the list.");
            Console.WriteLine($"The list have {names.Count} persons.");

            //topic 3: Search and sort lists
            Console.WriteLine();
            var index = names.IndexOf("Felipe");
            if (index == -1)
            {
                Console.WriteLine($"When an item is not found, IndexOf returns {index}");
            } else
            {
                Console.WriteLine($"The name {names[index]} is at index {index}");
            }

            index = names.IndexOf("Nicole");
            if (index == -1)
            {
                Console.WriteLine($"When an item is not found, IndexOf returns {index}");
            } else
            {
                Console.WriteLine($"The name {names[index]} is at index {index}");
            }

            //sort
            Console.WriteLine();
            names.Sort();
            foreach (var name in names)
            {
                Console.WriteLine($"Hello {name.ToUpper()}!");
            }
        }
    }
}