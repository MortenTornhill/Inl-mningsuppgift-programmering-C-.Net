namespace WestCoastEducation;

// Återanvänds av lärare, utbildningsledare och administratörer.
public class TeachingDetails
{
    private readonly List<Course> courses = new();
    public string KnowledgeArea { get; }
    public IReadOnlyList<Course> Courses => courses.AsReadOnly();

    public TeachingDetails(string knowledgeArea)
    {
        KnowledgeArea = knowledgeArea;
    }

    public bool AddCourse(Course course)
    {
        // Aggregation: en befintlig kurs kopplas till en ansvarig person.
        foreach (Course existingCourse in courses)
        {
            if (string.Equals(existingCourse.CourseNumber, course.CourseNumber,
                StringComparison.OrdinalIgnoreCase))
                return false;
        }
        courses.Add(course);
        return true;
    }

    public override string ToString()
    {
        var titles = new List<string>();
        foreach (Course course in courses)
            titles.Add($"{course.CourseNumber} – {course.Title}");
        string courseText = titles.Count == 0 ? "inga tilldelade kurser" : string.Join(", ", titles);
        return $"kunskapsområde: {KnowledgeArea}, kursansvar: {courseText}";
    }
}
