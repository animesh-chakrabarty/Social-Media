using System;

class PrintArray
{
    public static void PrintIntArray(int[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            Console.Write(arr[i] + " ");
        }

        Console.WriteLine();
    }

    public static void PrintStringArray(string[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            Console.Write(arr[i] + " ");
        }

        Console.WriteLine();
    }
}

class Car
{
    string model;
    int year;

    public Car(string model, int year)
    {
        this.model = model;
        this.year = year;
    }

    public void displayInfo()
    {
        Console.WriteLine($"Model: {this.model} \nYear: {this.year}");
    }
}

class MainClass
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hii from Main class");
        int[] arr = { 1, 2, 3, 4, 5 };
        string[] arrStr = { "str1", "str2" };

        PrintArray.PrintIntArray(arr);
        PrintArray.PrintStringArray(arrStr);

        Car c1 = new Car("Toyota", 2022);
        c1.displayInfo();
    }
}

