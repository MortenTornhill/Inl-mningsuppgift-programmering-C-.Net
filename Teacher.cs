namespace WestCoastEducation;

public class Teacher
{
    public PersonDetails Details { get; }
    public TeachingDetails Teaching { get; }

    public Teacher(PersonDetails details, string knowledgeArea)
    {
        Details = details;
        Teaching = new TeachingDetails(knowledgeArea);
    }

    public override string ToString() => $"Lärare: {Details}, {Teaching}";
}
