namespace LinqPlayground.ExampleClasses;
internal class StudentComparer : IEqualityComparer<Student>
{
    public bool Equals(Student? x, Student? y)
    {
        if(x == null || y == null)
            return false;

        if (x.StudentId == y.StudentId &&
                    x.StudentName.ToLower() == y.StudentName.ToLower())
            return true;

        return false;
    }

    public int GetHashCode(Student obj)
    {
        return obj.GetHashCode();
    }
}