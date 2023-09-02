using Common.DemoClasses;

namespace Common.DataSources;

public static class GenderSource
{
    private static readonly List<Gender> genders = new List<Gender>() {
            new Gender { GenderId = 1, GenderName = "Male" },
            new Gender { GenderId = 2, GenderName = "Female"},
            new Gender { GenderId = 3, GenderName = "Non-Binary" },
            new Gender { GenderId = 4, GenderName = "Other" }
        };

    public static IReadOnlyList<Gender> GetAllGenders()
    {
        return genders.AsReadOnly();
    }
}
