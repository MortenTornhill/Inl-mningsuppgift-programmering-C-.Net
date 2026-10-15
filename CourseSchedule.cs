namespace WestCoastEducation;

public class CourseSchedule
{
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }

    public CourseSchedule(DateTime startDate, DateTime endDate)
    {
        if (endDate.Date < startDate.Date)
        {
            throw new ArgumentException("Slutdatum får inte vara före startdatum.");
        }
        StartDate = startDate.Date;
        EndDate = endDate.Date;
    }

    public override string ToString() => $"{StartDate:yyyy-MM-dd} – {EndDate:yyyy-MM-dd}";
}
