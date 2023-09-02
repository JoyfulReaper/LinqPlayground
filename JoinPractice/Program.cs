using Common.DataSources;

internal class Program
{
    private static void Main(string[] args)
    {
        JoinPractice1();
    }

    private static void JoinPractice1()
    {
        var genders = GenderSource.GetAllGenders();
        var people = PersonSource.GetAllPeople();

        var joined =
            people.Join(
                genders,
                p => p.GenderId,
                g => g.GenderId,
                (person, gender) => new
                {
                    Name = person.Name,
                    Gender = gender.GenderName
                });

        foreach (var person in joined)
        {
            Console.WriteLine($"Name: {person.Name}, Gender {person.Gender}");
        }
    }
}