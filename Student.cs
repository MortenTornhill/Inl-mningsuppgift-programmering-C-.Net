namespace WestCoastEducation;

public class Student
{
    public PersonDetails Details { get; }

    public Student(PersonDetails details)
    {
        Details = details;
    }

    public override string ToString() => $"Studerande: {Details}";
}
