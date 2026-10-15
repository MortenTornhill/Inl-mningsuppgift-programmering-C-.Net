namespace WestCoastEducation;

public class Course
{
    private readonly List<Student> students = new();

    public string CourseNumber { get; }
    public string Title { get; }
    public int LengthInDays { get; }
    public CourseFormat Format { get; }
    public CourseSchedule Schedule { get; private set; }
    public IReadOnlyList<Student> Students => students.AsReadOnly();

    public Course(string courseNumber, string title, int lengthInDays,
        DateTime startDate, DateTime endDate, CourseFormat format)
    {
        if (string.IsNullOrWhiteSpace(courseNumber) || string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Kursnummer och titel krävs.");
        if (lengthInDays <= 0)
            throw new ArgumentException("Kurslängden måste vara minst en dag.");
        if (format != CourseFormat.Classroom && format != CourseFormat.Distance)
            throw new ArgumentException("Ogiltig undervisningsform.");

        CourseNumber = courseNumber.Trim();
        Title = title.Trim();
        LengthInDays = lengthInDays;
        Format = format;
        // Composition: kursen skapar och äger sitt eget schema.
        Schedule = new CourseSchedule(startDate, endDate);
    }

    public void ChangeSchedule(DateTime startDate, DateTime endDate)
    {
        Schedule = new CourseSchedule(startDate, endDate);
    }

    public bool AddStudent(Student student)
    {
        // Aggregation: studenten finns även oberoende av kursen.
        foreach (Student existingStudent in students)
        {
            if (existingStudent.Details.PersonalNumber == student.Details.PersonalNumber)
                return false;
        }
        students.Add(student);
        return true;
    }

    public void ListStudents()
    {
        if (students.Count == 0)
        {
            Console.WriteLine("Kursen har inga deltagare.");
            return;
        }
        foreach (Student student in students)
            Console.WriteLine(student);
    }

    public override string ToString()
    {
        string formatText = Format == CourseFormat.Classroom ? "Klassrum" : "Distans";
        return $"{CourseNumber}: {Title}, {LengthInDays} undervisningsdagar, " +
               $"{Schedule}, {formatText}, deltagare: {students.Count}";
    }
}
