using Common.DataSources;

internal class Program
{
    private static void Main(string[] args)
    {
        GroupJoinPractice1();
    }

    private static void GroupJoinPractice1()
    {
        // Goal: Group people into gender groups

        var genders = GenderSource.GetAllGenders();
        var people = PersonSource.GetAllPeople();

        var genderGrouped = genders.GroupJoin( // Outer Sequence (genders) - Sequence to "group by"
            people, // Inner Sequence - Elements of this sequence are "grouped" (into gender groups here)
            g => g.GenderId, // Outer Key Selector
            p => p.GenderId, // Inner Key Selector
            (gender, peopleGroup) => new { // Result Selector
                Gender = gender.GenderName,
                PeopleGroup = peopleGroup
            });

        foreach (var peopleGroup in genderGrouped)
        {
            Console.WriteLine($"{peopleGroup.Gender}");
            foreach (var person in peopleGroup.PeopleGroup)
            {
                Console.WriteLine($"{person.Name}");
            }
            Console.WriteLine();
        }
    }
}