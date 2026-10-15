using System.Globalization;

namespace WestCoastEducation;

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        var register = new SchoolRegister();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n--- WestCoast Education ---");
            Console.WriteLine("1. Lägg till kurs");
            Console.WriteLine("2. Lista kurser");
            Console.WriteLine("3. Lägg till studerande");
            Console.WriteLine("4. Lista studerande");
            Console.WriteLine("5. Lägg till lärare");
            Console.WriteLine("6. Lägg till utbildningsledare");
            Console.WriteLine("7. Lägg till administratör");
            Console.WriteLine("8. Lista personal");
            Console.WriteLine("9. Lägg till studerande på kurs");
            Console.WriteLine("10. Lista deltagare på kurs");
            Console.WriteLine("0. Avsluta");
            Console.Write("Välj: ");
            string? choice = Console.ReadLine();
            if (choice == null) break;

            try
            {
                switch (choice.Trim())
                {
                    case "1": AddCourse(register); break;
                    case "2": register.ListCourses(); break;
                    case "3":
                        bool added = register.AddStudent(new Student(ReadPersonDetails()));
                        Console.WriteLine(added ? "Studerande tillagd." : "Personnumret finns redan.");
                        break;
                    case "4": register.ListStudents(); break;
                    case "5":
                        var teacher = new Teacher(ReadPersonDetails(), ReadText("Kunskapsområde"));
                        AssignCourses(teacher.Teaching, register);
                        register.AddTeacher(teacher);
                        Console.WriteLine("Lärare tillagd.");
                        break;
                    case "6":
                        var leader = new EducationLeader(ReadPersonDetails(), ReadText("Kunskapsområde"),
                            ReadDate("Anställningsdatum"));
                        AssignCourses(leader.Teaching, register);
                        register.AddEducationLeader(leader);
                        Console.WriteLine("Utbildningsledare tillagd.");
                        break;
                    case "7":
                        var administrator = new Administrator(ReadPersonDetails(), ReadText("Kunskapsområde"),
                            ReadDate("Anställningsdatum"));
                        AssignCourses(administrator.Teaching, register);
                        register.AddAdministrator(administrator);
                        Console.WriteLine("Administratör tillagd.");
                        break;
                    case "8": register.ListStaff(); break;
                    case "9": EnrollStudent(register); break;
                    case "10":
                        Course? course = register.FindCourse(ReadText("Kursnummer"));
                        if (course == null) Console.WriteLine("Kursen finns inte.");
                        else course.ListStudents();
                        break;
                    case "0": running = false; break;
                    default: Console.WriteLine("Ogiltigt menyval. Försök igen."); break;
                }
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine($"Kunde inte spara: {exception.Message}");
            }
            catch (EndOfStreamException)
            {
                running = false;
            }
        }
        Console.WriteLine("Programmet avslutades.");
    }

    private static void AddCourse(SchoolRegister register)
    {
        string number = ReadText("Kursnummer");
        string title = ReadText("Titel");
        int days = ReadPositiveNumber("Längd i undervisningsdagar");
        DateTime start = ReadDate("Startdatum");
        DateTime end = ReadDate("Slutdatum");
        int format;
        do
        {
            format = ReadPositiveNumber("Undervisningsform (1 = klassrum, 2 = distans)");
            if (format != 1 && format != 2) Console.WriteLine("Välj 1 eller 2.");
        } while (format != 1 && format != 2);

        var course = new Course(number, title, days, start, end, (CourseFormat)format);
        Console.WriteLine(register.AddCourse(course) ? "Kurs tillagd." : "Kursnumret finns redan.");
    }

    private static PersonDetails ReadPersonDetails()
    {
        return new PersonDetails(ReadText("Förnamn"), ReadText("Efternamn"),
            ReadText("Telefon"), ReadText("Personnummer"), ReadText("Adress"),
            ReadText("Postnummer"), ReadText("Ort"));
    }

    private static void AssignCourses(TeachingDetails teaching, SchoolRegister register)
    {
        register.ListCourses();
        while (true)
        {
            Console.Write("Kursnummer att ansvara för (Enter för att fortsätta): ");
            string number = ReadLine().Trim();
            if (number == "") return;
            Course? course = register.FindCourse(number);
            if (course == null) Console.WriteLine("Kursen finns inte.");
            else Console.WriteLine(teaching.AddCourse(course) ? "Kurs kopplad." : "Kursen är redan kopplad.");
        }
    }

    private static void EnrollStudent(SchoolRegister register)
    {
        Course? course = register.FindCourse(ReadText("Kursnummer"));
        Student? student = register.FindStudent(ReadText("Studerandes personnummer"));
        if (course == null || student == null)
        {
            Console.WriteLine("Kursen eller den studerande finns inte i registret.");
            return;
        }
        Console.WriteLine(course.AddStudent(student) ? "Deltagare tillagd." : "Den studerande är redan med på kursen.");
    }

    private static string ReadLine() => Console.ReadLine() ?? throw new EndOfStreamException();

    private static string ReadText(string label)
    {
        while (true)
        {
            Console.Write($"{label}: ");
            string text = ReadLine().Trim();
            if (text.Length > 0) return text;
            Console.WriteLine("Fältet får inte vara tomt.");
        }
    }

    private static int ReadPositiveNumber(string label)
    {
        while (true)
        {
            string text = ReadText(label);
            if (int.TryParse(text, out int number) && number > 0) return number;
            Console.WriteLine("Ange ett positivt heltal.");
        }
    }

    private static DateTime ReadDate(string label)
    {
        while (true)
        {
            string text = ReadText($"{label} (ÅÅÅÅ-MM-DD)");
            if (DateTime.TryParseExact(text, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime date)) return date;
            Console.WriteLine("Ange ett giltigt datum, exempelvis 2026-11-02.");
        }
    }
}
