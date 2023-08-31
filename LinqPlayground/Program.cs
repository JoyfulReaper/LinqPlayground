//https://www.tutorialsteacher.com/linq/what-is-linq

using LinqPlayground.ExampleClasses;
using System.Collections;

internal class Program
{
    private static void Main(string[] args)
    {
        //WhatIsLinq();
        //WhyLinq();
        //LinqWhere();
        //LinqOfType();
        OrderBy();
    }


    private static void OrderBy()
    {
        IList<Student> studentList = new List<Student>() {
            new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
            new Student() { StudentId = 2, StudentName = "Steve",  Age = 15 } ,
            new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
            new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
            new Student() { StudentId = 5, StudentName = "Ron" , Age = 19 }
        };

        // OrderBy Sorts the elements in the collection based on specified fields in ascending or descending order.
        var orderedResult =
                from s in studentList
                orderby s.StudentName
                select s;

        var descOrderedResults =
               from s in orderedResult
               orderby s.StudentName descending
               select s;

        // OrderBy method syntax: two overloads, second overload accepts an IComparer to use for custom sorting
        var studentsAscOrder = studentList.OrderBy(s => s.StudentName);
        var studentDescOrder = studentList.OrderByDescending(s => s.StudentName);

        /*
         * You can sort the collection on multiple fields seperated by comma. 
         * The given collection would be first sorted based on the first field and 
         * then if value of first field would be the same for two elements then it would use second field for sorting and so on.
         */
        IList<Student> studentList2 = new List<Student>() {
            new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
            new Student() { StudentId = 2, StudentName = "Steve",  Age = 15 } ,
            new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
            new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
            new Student() { StudentId = 5, StudentName = "Ron" , Age = 19 },
            new Student() { StudentId = 6, StudentName = "Ram" , Age = 18 }
    };

        var orderByResult =
            from s in studentList2
            orderby s.StudentName, s.Age
            select new { s.StudentName, s.Age };

        foreach (var s in orderByResult)
        {
            Console.WriteLine(s);
        }

        //Multiple sorting in method syntax works differently. Use ThenBy or ThenByDecending extension methods for secondary sorting.
        // TODO: Add Example
    }

    private static void LinqOfType()
    {
        // Filter the collection based on the ability to cast an element in a collection to a specified type
        IList mixedList = new ArrayList();
        mixedList.Add(0);
        mixedList.Add("One");
        mixedList.Add("Two");
        mixedList.Add(3);
        mixedList.Add(new Student() { StudentId = 1, StudentName = "Bill" });

        var stringResult = from s in mixedList.OfType<string>()
                                           select s;    

        var intResult =
            from s in mixedList.OfType<int>()
            select s;

        var stringResultF = mixedList.OfType<string>();
        var intResultF = mixedList.OfType<int>();
    }

    private static void LinqWhere()
    {
        // Where returns values from the collection based on a predicate function
        IList<Student> studentList = new List<Student>() {
            new Student() { StudentId = 1, StudentName = "John", Age = 13} ,
            new Student() { StudentId = 2, StudentName = "Moin",  Age = 21 } ,
            new Student() { StudentId = 3, StudentName = "Bill",  Age = 18 } ,
            new Student() { StudentId = 4, StudentName = "Ram" , Age = 20} ,
            new Student() { StudentId = 5, StudentName = "Ron" , Age = 15 }
        };

        var filteredResult =
                from s in studentList
                where s.Age > 12 && s.Age < 20
                select $"{s.StudentName} {s.Age}";

        // func parameter
        var filteredResults2 =
            from s in studentList
            where IsTeenAger(s)
            select s;

        var fluentResult =
            studentList.Where(s => s.Age > 12 && s.Age < 20);

        // Where overload that includes index:
        var eventStudents = studentList.Where((s, i) => {
            if (i % 2 == 0)
                return true;
            return false;
        });

        foreach ( var student in filteredResult )
        {
            Console.WriteLine( student );
        }

        // Multiple where clauses
        var multiWhere = 
            from s in studentList
            where s.Age > 12
            where s.Age < 20
            select s;

        var multiWhere2 = studentList.Where(s => s.Age > 12).Where(s => s.Age < 20);
    }

    private static void LinqMethodSyntax()
    {
        // Method Syntax / Fluent syntax

        // string collection
        IList<string> stringList = new List<string>() {
            "C# Tutorials",
            "VB.NET Tutorials",
            "Learn C++",
            "MVC Tutorials" ,
            "Java"
        };

        IEnumerable<string>? results = stringList.Where(s => s.Contains("Tutorial"));

        // Student collection
        IList<Student> studentList = new List<Student>() {
            new Student() { StudentId = 1, StudentName = "John", Age = 13} ,
            new Student() { StudentId = 2, StudentName = "Moin",  Age = 21 } ,
            new Student() { StudentId = 3, StudentName = "Bill",  Age = 18 } ,
            new Student() { StudentId = 4, StudentName = "Ram" , Age = 20} ,
            new Student() { StudentId = 5, StudentName = "Ron" , Age = 15 }
        };

        var teenStudents =
            studentList.Where(s => s.Age > 12 && s.Age < 20);
    }

    private static void LinqQuerySyntax()
    {
        // Query Syntax/Query Expression Syntax and Method Syntax/Fluent Syntax

        // Query Syntax:
        // from <range variable> in <IEnumerable<T> or IQueryable<T> Collection>
        // <Standard Query Operators> <lambda expression>
        // <select or groupBy operator> <result formation>

        IList<string> stringList = new List<string>() {
            "C# Tutorials",
            "VB.NET Tutorials",
            "Learn C++",
            "MVC Tutorials",
            "Java"
        };

        var result =
            from s in stringList
            where s.Contains("Tutorials")
            select s;

        // Student collection
        IList<Student> studentList = new List<Student>() {
            new Student() { StudentId = 1, StudentName = "John", Age = 13} ,
            new Student() { StudentId = 2, StudentName = "Moin",  Age = 21 } ,
            new Student() { StudentId = 3, StudentName = "Bill",  Age = 18 } ,
            new Student() { StudentId = 4, StudentName = "Ram" , Age = 20} ,
            new Student() { StudentId = 5, StudentName = "Ron" , Age = 15 }
         };

        var teenStudents =
            from student in studentList
            where student.Age > 12 && student.Age < 20
            select student;
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
        foreach (Student student in studentArray)
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
        Student[] studentsNamedBill = StudentExtension.Where(studentArray, delegate (Student std)
        {
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

    public static bool IsTeenAger(Student stud)
    {
        return stud.Age > 12 && stud.Age < 20;
    }
}