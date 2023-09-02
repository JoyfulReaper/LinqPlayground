using Common.DemoClasses;

namespace Common.DataSources;

public static class PersonSource
{
    private static readonly List<Person> people = new List<Person>() {
            new Person { PersonId = 1, GenderId = 1, Name = "John" },
            new Person { PersonId = 2, GenderId = 1, Name = "Jack" },
            new Person { PersonId = 3, GenderId = 1, Name = "James" },
            new Person { PersonId = 4, GenderId = 1, Name = "Jacob" },
            new Person { PersonId = 5, GenderId = 1, Name = "Jared" },
            new Person { PersonId = 6, GenderId = 2, Name = "Jessica" },
            new Person { PersonId = 7, GenderId = 2, Name = "Jasmine" },
            new Person { PersonId = 8, GenderId = 2, Name = "Jenna" },
            new Person { PersonId = 9, GenderId = 2, Name = "Jocelyn" },
            new Person { PersonId = 10, GenderId = 3, Name = "Jaden" },
            new Person { PersonId = 11, GenderId = 3, Name = "Jasper" },
            new Person { PersonId = 12, GenderId = 3, Name = "Jesse" },
            new Person { PersonId = 13, GenderId = 3, Name = "Jules" },
            new Person { PersonId = 14, GenderId = 4, Name = "Jamie" },
            new Person { PersonId = 15, GenderId = 4, Name = "Jules" },
            new Person { PersonId= 16, GenderId = 4, Name = "Shax" }
        };

    public static IReadOnlyList<Person> GetAllPeople()
    {
        return people.AsReadOnly();
    }
}
