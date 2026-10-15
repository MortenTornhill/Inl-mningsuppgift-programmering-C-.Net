namespace WestCoastEducation;

public class Administrator
{
    public PersonDetails Details { get; }
    public TeachingDetails Teaching { get; }
    public DateTime EmploymentDate { get; }

    public Administrator(PersonDetails details, string knowledgeArea, DateTime employmentDate)
    {
        Details = details;
        Teaching = new TeachingDetails(knowledgeArea);
        EmploymentDate = employmentDate.Date;
    }

    public override string ToString() => $"Administratör: {Details}, {Teaching}, anställd: {EmploymentDate:yyyy-MM-dd}";
}
