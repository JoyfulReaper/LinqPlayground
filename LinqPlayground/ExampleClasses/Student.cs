namespace LinqPlayground.ExampleClasses;

internal class Student
{
    public int StudentId { get; set; }
    public string StudentName { get; set; } = default!;
    public int Age { get; set; }
    public int StandardId { get; set; }

    public override string ToString()
    {
        return $"Name: {StudentName} Age: {Age} Id: {StudentId}";
    }
}
