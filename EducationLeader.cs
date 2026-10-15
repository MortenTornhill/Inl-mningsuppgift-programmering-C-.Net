namespace WestCoastEducation;

public class EducationLeader
{
    public PersonDetails Details { get; }
    public TeachingDetails Teaching { get; }
    public DateTime EmploymentDate { get; }

    public EducationLeader(PersonDetails details, string knowledgeArea, DateTime employmentDate)
    {
        Details = details;
        Teaching = new TeachingDetails(knowledgeArea);
        EmploymentDate = employmentDate.Date;
    }

    public override string ToString() => $"Utbildningsledare: {Details}, {Teaching}, anställd: {EmploymentDate:yyyy-MM-dd}";
}
