//https://www.tutorialsteacher.com/linq/what-is-linq

using LinqPlayground.ExampleClasses;

internal class Program
{
    private static void Main(string[] args)
    {
        //WhatIsLinq();
        WhyLinq();
    }

    private static void WhyLinq()
    {
        // Goal: Find a list of teenage students from an array of Student Objects

        // Before C# 2.0 - have to use for or foreach to traverse the collection and find a particular  object
        Student[] studentArray = {
            new Student() { StudentId = 1, StudentName = "John", Age = 18 },
            new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 },
            new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 },
            new Student() { StudentId = 4, StudentName = "Ram", Age = 20 },
            new Student() { StudentId = 5, StudentName = "Ron", Age = 31 },
            new Student() { StudentId = 6, StudentName = "Chris",  Age = 17 },
            new Student() { StudentId = 7, StudentName = "Rob", Age = 19  },
        };

        Student[] students = new Student[10];

        int i = 0;
        foreach(Student student in studentArray)
        {
            if (student.Age > 12 && student.Age < 20)
            {
                students[i] = student;
                i++;
            }
        }

        // C# 2.0 Introduced delegate, which can be used to handle this kind of a scenario:
        Student[] studentsDel = StudentExtension.Where(studentArray, delegate (Student std)
        {
            return std.Age > 12 && std.Age < 20;
        });

        // Don't need to use a for loop with different criteria, can use a delegate to find a student with any criteria
        Student[] studentsWithIdFive = StudentExtension.Where(studentArray, delegate (Student std)
        {
            return std.StudentId == 5;
        });

        //Also, use another criteria using same delegate
        Student[] studentsNamedBill = StudentExtension.Where(studentArray, delegate (Student std) {
            return std.StudentName == "Bill";
        });

        // C# 3.0 added Extension Methods, Lambda Expressions, Expression Trees, Anonymous types, and query expressions
        // These are the building blocks of LINQ
        // Using a LINQ Query with a Lambda Expression to find particular student(s)
        var teenStudents = studentArray.Where(s => s.Age > 12 && s.Age > 20).ToArray();

        Student? bill = studentArray.Where(s => s.StudentName == "Bill").FirstOrDefault();

        Student? studentFive = studentArray.Where(s => s.StudentId == 5).FirstOrDefault(); 

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