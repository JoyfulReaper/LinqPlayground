//https://www.tutorialsteacher.com/linq/what-is-linq

using Common.DemoClasses;
using LinqPlayground.ExampleClasses;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

internal class Program
{
    private static void Main(string[] args)
    {
        //WhatIsLinq();
        //WhyLinq();
        //Where();
        //OfType();
        //OrderBy();
        //ThenBy();
        //GroupBy();
        //Join();
        //GroupJoin();
        //Select();
        //SelectMany();
        //AllAny();
        //Contains();
        Aggregate();
    }

    private static void Aggregate()
    {
        // Aggregation operators perform mathematical operations like Average, Count, Max, Min, Sum
        // Aggregate method performs an accumulate operation

        /*
         * first item of strList "One" will be pass as s1 and rest of the items will be passed as s2.
         * The lambda expression (s1, s2) => s1 + ", " + s2 will be treated like s1 = s1 + ", " + s1 where s1 will be accumulated for each item in the collection.
         * Thus, Aggregate method will return comma separated string.
         * */
        IList<string> strList = new List<string>() { "One", "Two", "Three", "Four", "Five" };
        var commaSeparated = strList.Aggregate((s1, s2) => s1 + ", " + s2); // Apply an accumulator over a sequence
        Console.WriteLine(commaSeparated);

        // Aggregate Method with Seed Value
        // Student collection
        IList<Student> studentList = new List<Student>() {
            new Student() { StudentId = 1, StudentName = "John", Age = 13 } ,
            new Student() { StudentId = 2, StudentName = "Moin", Age = 21 } ,
            new Student() { StudentId = 3, StudentName = "Bill", Age = 18 } ,
            new Student() { StudentId = 4, StudentName = "Ram", Age = 20 } ,
            new Student() { StudentId = 5, StudentName = "Ron", Age = 15 }
        };
        string seededCommaSeparated = studentList.Aggregate<Student, string>(
            "Student Names: ", // Seed
            (str, s) => str += s.StudentName + ",");
        Console.WriteLine(seededCommaSeparated);

        int sumOfStudentAges = studentList.Aggregate<Student, int>(0,
            (totalAge, s) => totalAge += s.Age);
        Console.WriteLine(sumOfStudentAges);

        // Aggregate Method with Result Selector
        string commaSeparatedWResultSelector = studentList.Aggregate<Student, string, string>(
            String.Empty, // Seed Value
            (str, s) => str += s.StudentName + ",", // returns result using seed value, String.Empty goes to lambda expression as str
            str => str.Substring(0, str.Length - 1)); // result selector that removes last comma

        Console.WriteLine(commaSeparatedWResultSelector);
    }

    private static void Contains()
    {
        // Contains operator checks whether a specified element exists in the collection or not, returning a boolean
        //An overload that accepts an IEqualityComparer also exists

        // Will work with primatives only
        IList<int> intList = new List<int>() { 1, 2, 3, 4, 5 };
        bool contains10 = intList.Contains(10);

        IList<Student> studentList = new List<Student>() {
            new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
            new Student() { StudentId = 2, StudentName = "Steve",  Age = 15 } ,
            new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
            new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
            new Student() { StudentId = 5, StudentName = "Ron" , Age = 19 }
        };

        // Will not work since Student is not a primative - Reference is compared
        Student std = new Student() { StudentId = 3, StudentName = "Bill" };
        bool result = studentList.Contains(std); //returns false
        Console.WriteLine($"No Comparer - Contains? {result}");

        // For this to work need to create a class that implements the IEqualityComparer interface:
        bool comparerResult = studentList.Contains(std, new StudentComparer());
        Console.WriteLine($"With Comparer - Contains? {comparerResult}");
    }       

    private static void AllAny()
    {
        // All, Any, Contains are Quantifier Operators - Evaluate elements of a sequence on some condition and return a boolean value
        // to indicate that some or all elements satisfy the condition. Quantifier Operators are not supported in query syntax

        // All operator evaluates each element in the given collection on a specified condition and returns true if all the elements
        // satisfy the condition

        IList<Student> studentList = new List<Student>() {
            new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
            new Student() { StudentId = 2, StudentName = "Steve",  Age = 15 } ,
            new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
            new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
            new Student() { StudentId = 5, StudentName = "Ron" , Age = 19 }
        };

        bool allStudentsAreTeens = studentList.All(s => s.Age > 12 && s.Age < 20);
        Console.WriteLine($"All students are teens: {allStudentsAreTeens}");

        bool anyStudentsAreTeens = studentList.Any(s => s.Age > 12 && s.Age < 20);
        Console.WriteLine($"Any students are teens: {anyStudentsAreTeens}");
    }

    private static void SelectMany()
    {
        // SelectMany operator projects sequences of values that are based on a transform function and then flattens them into one sequence
        List<Bouquet> bouquets = new()
        {
            new Bouquet { Flowers = new List<string> { "sunflower", "daisy", "daffodil", "larkspur" }},
            new Bouquet { Flowers = new List<string> { "tulip", "rose", "orchid" }},
            new Bouquet { Flowers = new List<string> { "gladiolis", "lily", "snapdragon", "aster", "protea" }},
            new Bouquet { Flowers = new List<string> { "larkspur", "lilac", "iris", "dahlia" }}
        };

        // IEnumerable of List of Strings
        var select = bouquets.Select(bq => bq.Flowers);

        // IEnumerable of strings - source lists were flattened
        var selectMany = bouquets.SelectMany(bq => bq.Flowers);

        // Select
        int i = 1;
        foreach(var bq in select)
        {
            Console.WriteLine($"Bouquet {i++}: ");
            foreach (var flower in bq)
            {
                Console.WriteLine($"Flower: {flower}");
            }
        }

        // SelectMany
        Console.WriteLine($"{Environment.NewLine}Flattened with SelectMany");
        foreach(var flower in selectMany)
        {
            Console.WriteLine($"Flower: {flower}");
        }
    }

    private static void Select()
    {
        // Select - Projection operator - Returns an IEnumerable collection which contains elements based on a transformation function
        IList<Student> studentList = new List<Student>() {
            new Student() { StudentId = 1, StudentName = "John", Age = 13 } ,
            new Student() { StudentId = 2, StudentName = "Moin",  Age = 21 } ,
            new Student() { StudentId = 3, StudentName = "Bill",  Age = 18 } ,
            new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
            new Student() { StudentId = 5, StudentName = "Ron" , Age = 15 }
        };

        var selectResult = from s in studentList
                           select s.StudentName;

        // Select operator can be used to formulate the result as per our requirements. Can be used to return a collection of a custom class or anonymous object.
        var selectResult2 =
            from s in studentList
            select new { Name = "Mr. " + s.StudentName, Age = s.Age };

        foreach (var item in selectResult2)
        {
            Console.WriteLine("Student Name: {0}, Age: {1}", item.Name, item.Age);
        }

        // Method Syntax
        var selectResultM = studentList.Select(s => new {
            Name = s.StudentName,
            Age = s.Age
        });
    }

    private static void GroupJoin()
    {
        // Performs the same task as join, except that GroupJoin returns the results in a group based on a specified group key.
        // Joins two sequences based on a key and groups the result by matching the key and then returns the collection of grouped results and keys.

        IList<Student> studentList = new List<Student>() {
            new Student() { StudentId = 1, StudentName = "John", StandardId =1 },
            new Student() { StudentId = 2, StudentName = "Moin", StandardId =1 },
            new Student() { StudentId = 3, StudentName = "Bill", StandardId =2 },
            new Student() { StudentId = 4, StudentName = "Ram",  StandardId =2 },
            new Student() { StudentId = 5, StudentName = "Ron" }
        };

        IList<Standard> standardList = new List<Standard>() {
            new Standard(){ StandardId = 1, StandardName="Standard 1"},
            new Standard(){ StandardId = 2, StandardName="Standard 2"},
            new Standard(){ StandardId = 3, StandardName="Standard 3"}
        };

        // GroupJoin: Groups the inner sequence (studentList) into studentGroup based on matching StandardId keys.
        // This operation creates a collection of grouped results where students are grouped into different standards by their StandardId.
        // Each group represents a unique StandardId and includes a collection of matching students and the StandardName.

        // GroupJoin: Groups the inner sequence (students) into studentGroup whose StandardId matches with std.StandardId
        // This operation groups the inner sequence (students) into groups based on matching keys (StandardId) and returns a collection of grouped results.
        // The students are grouped into different standards by their StandardId, creating a new group for each unique StandardId.
        var groupJoin = standardList.GroupJoin(// outer sequence (standardList)
            studentList,                                       // Inner sequence
            std => std.StandardId,                     // outer key selector
            s => s.StandardId,                         // inner key selector
            (std, studentGroup) => new // Result selector
            {
                Students = studentGroup,
                StandardFullName = std.StandardName
            });

        foreach (var item in groupJoin)
        {
            Console.WriteLine(item.StandardFullName);
            foreach (var student in item.Students)
            {
                Console.WriteLine(student.StudentName);
            }
        }

        // Group the inner sequence (standardList) into standardGroup whose StandardId matches with s.StandardId
        var groupJoin2 = studentList.GroupJoin(// outer sequence (studentList)
            standardList, // Inner sequence
            std => std.StandardId, // outer key selector
            s => s.StandardId, // inner key selector
            (std, standardGroup) => new // Result selector
            {
                Standards = standardGroup,
                StudentName = std.StudentName
            });

        Console.WriteLine();
        foreach (var item in groupJoin2)
        {
            Console.WriteLine(item.StudentName);
            foreach (var standard in item.Standards)
            {
                Console.WriteLine(standard.StandardName);
            }
        }

        // Group join Query Syntax
        var groupJoinQ =
            from std in standardList
            join s in studentList
                on std.StandardId equals s.StandardId
            into studentGroup
            select new
            {
                Students = studentGroup,
                StandardName = std.StandardName
            };

        Console.WriteLine();
        foreach (var item in groupJoinQ)
        {
            Console.WriteLine(item.StandardName);
            foreach (var student in item.Students)
            {
                Console.WriteLine(student.StudentName);
            }
        }
    }

    private static void Join()
    {
        // Join: Joins two sequences to product a result - Returns a new collection that contains elements from both the collections which satisfies the specified expression. Same as SQL inner join (Matching values on both tables)
        // GroupJoin: Joins two sequence based on keys and returns groups of sequences. Like Left Outer Join in SQL (All records on left table, matching records from left table)
        IList<string> strList1 = new List<string>() {
            "One",
            "Two",
            "Three",
            "Four"
        };

        IList<string> strlist2 = new List<string>() {
            "One",
            "Two",
            "Five",
            "Six"
        };

        var innerJoin = strList1.Join(strlist2, // inner
                                        str1 => str1, // Outer key selector
                                         str2 => str2, // Inner key selector
                                         (str1, str2) => str1); // Result selector (selecting just str1)


        ///////////////////////////

        IList<Student> studentList = new List<Student>() {
            new Student() { StudentId = 1, StudentName = "John", StandardId =1 },
            new Student() { StudentId = 2, StudentName = "Moin", StandardId =1 },
            new Student() { StudentId = 3, StudentName = "Bill", StandardId =2 },
            new Student() { StudentId = 4, StudentName = "Ram" , StandardId =2 },
            new Student() { StudentId = 5, StudentName = "Ron"  }
        };

        IList<Standard> standardList = new List<Standard>() {
            new Standard(){ StandardId = 1, StandardName="Standard 1"},
            new Standard(){ StandardId = 2, StandardName="Standard 2"},
            new Standard(){ StandardId = 3, StandardName="Standard 3"}
        };

        // studentList is outer sequence because the query starts from it
        // First parameter is the inner sequence
        // Second and third parameter specifies a field whose value should match using a lambda expression in order to include that element in the result (must match for element to be included in match)
        // -- In this example the outer key selector student.StandardId must match the inner sequence key standard.StandardId. If both of the values of the key match them include the element in the result
        // Fourth parameter is an expression to formulate the result
        var innerJoin2 = studentList.Join( // Outer sequence (studentList)
                                            standardList, // inner sequence 
                                            s => s.StandardId, // outer key selector
                                            s => s.StandardId, // inner key selector
                                            (student, standard) => new // Result selector
                                            { 
                                                StudentName = student.StudentName,
                                                StandardName = standard.StandardName
                                            });

        foreach (var student in innerJoin2)
        {
            Console.WriteLine($"{student.StudentName} {student.StandardName}");
        }

        // Join query Syntax
        IList<Student> studentList2 = new List<Student>() {
            new Student() { StudentId = 1, StudentName = "John", Age = 13, StandardId =1 },
            new Student() { StudentId = 2, StudentName = "Moin",  Age = 21, StandardId =1 },
            new Student() { StudentId = 3, StudentName = "Bill",  Age = 18, StandardId =2 },
            new Student() { StudentId = 4, StudentName = "Ram" , Age = 20, StandardId =2 },
            new Student() { StudentId = 5, StudentName = "Ron" , Age = 15 }
        };

        IList<Standard> standardList2 = new List<Standard>() {
            new Standard(){ StandardId = 1, StandardName="Standard 1"},
            new Standard(){ StandardId = 2, StandardName="Standard 2"},
            new Standard(){ StandardId = 3, StandardName="Standard 3"}
        };

        var innerJoinQ =
            from s in studentList2 // outer sequence
            join st in standardList2 // inner sequence
            on s.StandardId equals st.StandardId // Key selector
            select new
            {
                StudentName = s.StudentName,
                StandardName = st.StandardName
            };

        Console.WriteLine();
        Console.WriteLine("Query Syntax");
        foreach (var student in innerJoinQ)
        {
            Console.WriteLine($"{student.StudentName} {student.StandardName}");
        }
    }

    private static void GroupBy()
    {
        // Same as GroupBy in SQL - Create a group of elements based on the given key
        // IGrouping<TKey, TSource> TKey is key value, on which the group has been formed. TSource is the collection of elements that matches with the grouping key value

        // ToLookup is the same as GroupBy except execute is not deferred. Not supported in query syntax

        IList<Student> studentList = new List<Student>() {
            new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
            new Student() { StudentId = 2, StudentName = "Steve",  Age = 21 } ,
            new Student() { StudentId = 3, StudentName = "Bill",  Age = 18 } ,
            new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
            new Student() { StudentId = 5, StudentName = "Abram" , Age = 21 }
        };

        // Group by query Syntax
        var groupResult =
            from student in studentList
            group student by student.Age;

        // Iterate each group
        foreach (var ageGroup in groupResult)
        {
            Console.WriteLine($"Age Group: {ageGroup.Key}");
            foreach (var student in ageGroup)
            {
                Console.WriteLine(student.ToString());
            }
        }

        Console.WriteLine();
        Console.WriteLine("Fluent:");
        var groupResultF = studentList.GroupBy(s => s.Age);
        // Iterate each group
        foreach (var ageGroup in groupResultF)
        {
            Console.WriteLine($"Age Group: {ageGroup.Key}");
            foreach (var student in ageGroup)
            {
                Console.WriteLine(student.ToString());
            }
        }

        Console.WriteLine();
        Console.WriteLine("ToLookup:");
        var lookupResult = studentList.ToLookup(s => s.Age);
        foreach (var ageGroup in lookupResult)
        {
            Console.WriteLine($"Age Group: {ageGroup.Key}");
            foreach (var student in ageGroup)
            {
                Console.WriteLine(student.ToString());
            }
        }

        /*
         * When to Use Which
           Use ToLookup when you want a convenient way to access groups using keys and when you need fast lookup based on keys. Use GroupBy when you need more flexibility and want to perform 
           operations on the groups themselves, or when you don't need the constant-time lookup behavior provided by ToLookup.

          Remember that ToLookup creates a data structure optimized for fast lookups, while GroupBy provides more flexibility at the cost of slightly slower performance when accessing groups.
        */

    }

    private static void ThenBy()
    {
        // ThenBy and ThenByDescending are used for sorting on multiple fields
        IList<Student> studentList = new List<Student>() {
            new Student() { StudentId = 1, StudentName = "John", Age = 18 } ,
            new Student() { StudentId = 2, StudentName = "Steve",  Age = 15 } ,
            new Student() { StudentId = 3, StudentName = "Bill",  Age = 25 } ,
            new Student() { StudentId = 4, StudentName = "Ram" , Age = 20 } ,
            new Student() { StudentId = 5, StudentName = "Ron" , Age = 19 },
            new Student() { StudentId = 6, StudentName = "Ram" , Age = 18 }
        };

        var thenByResult = studentList.OrderBy(s => s.StudentName).ThenBy(s => s.Age);
        var thenByDescendingResult = studentList.OrderBy(s => s.StudentName).ThenByDescending(s => s.Age);

        foreach (var student in thenByResult)
        {
            Console.WriteLine(student.ToString());
        }
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
         * You can sort the collection on multiple fields separated by comma. 
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

        //Multiple sorting in method syntax works differently. Use ThenBy or ThenByDescending extension methods for secondary sorting.
    }

    private static void OfType()
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

    private static void Where()
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

        foreach (var student in filteredResult)
        {
            Console.WriteLine(student);
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
