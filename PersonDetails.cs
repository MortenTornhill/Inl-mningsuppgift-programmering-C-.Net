namespace WestCoastEducation;

// Gemensamma personuppgifter återanvänds utan att använda arv.
public class PersonDetails
{
    public string FirstName { get; }
    public string LastName { get; }
    public string Phone { get; }
    public string PersonalNumber { get; }
    public string Address { get; }
    public string PostalCode { get; }
    public string City { get; }

    public PersonDetails(string firstName, string lastName, string phone,
        string personalNumber, string address, string postalCode, string city)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        PersonalNumber = personalNumber;
        Address = address;
        PostalCode = postalCode;
        City = city;
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName}, telefon: {Phone}, " +
               $"adress: {Address}, {PostalCode} {City}";
    }
}
