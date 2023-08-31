//https://www.tutorialsteacher.com/linq/what-is-linq

internal class Program
{
    private static void Main(string[] args)
    {
        WhatIsLinq();
    }

    private static void WhatIsLinq()
    {
        // Data source
        string[] names = { "Bill", "Steve", "James", "Mohan" };

        // LINQ Query
        var myLinqQuery =
            from name in names
            where name.Contains('a')
            select name;

        var myLinqQuery2 =
            names.Where(name => name.Contains('a'));

        // Query Execution
        foreach (var item in myLinqQuery2)
        {
            Console.WriteLine(item + " ");
        }
    }
}