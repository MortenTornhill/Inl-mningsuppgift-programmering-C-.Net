namespace WestCoastEducation;

public class SchoolRegister
{
    private readonly List<Course> courses = new();
    private readonly List<Student> students = new();
    private readonly List<Teacher> teachers = new();
    private readonly List<EducationLeader> educationLeaders = new();
    private readonly List<Administrator> administrators = new();

    public bool AddCourse(Course course)
    {
        if (FindCourse(course.CourseNumber) != null)
            return false;
        courses.Add(course);
        return true;
    }

    public bool AddStudent(Student student)
    {
        if (FindStudent(student.Details.PersonalNumber) != null)
            return false;
        students.Add(student);
        return true;
    }

    public void AddTeacher(Teacher teacher) => teachers.Add(teacher);
    public void AddEducationLeader(EducationLeader leader) => educationLeaders.Add(leader);
    public void AddAdministrator(Administrator administrator) => administrators.Add(administrator);

    public Course? FindCourse(string courseNumber)
    {
        foreach (Course course in courses)
        {
            if (string.Equals(course.CourseNumber, courseNumber, StringComparison.OrdinalIgnoreCase))
                return course;
        }
        return null;
    }

    public Student? FindStudent(string personalNumber)
    {
        foreach (Student student in students)
        {
            if (student.Details.PersonalNumber == personalNumber)
                return student;
        }
        return null;
    }

    public void ListCourses() => PrintList(courses);
    public void ListStudents() => PrintList(students);

    public void ListStaff()
    {
        Console.WriteLine("Lärare:");
        PrintList(teachers);
        Console.WriteLine("Utbildningsledare:");
        PrintList(educationLeaders);
        Console.WriteLine("Administratörer:");
        PrintList(administrators);
    }

    // T står för objektets typ. Samma utskriftskod kan användas för alla listor.
    private static void PrintList<T>(List<T> items)
    {
        if (items.Count == 0)
        {
            Console.WriteLine("Listan är tom.");
            return;
        }
        foreach (T item in items)
            Console.WriteLine(item);
    }
}
