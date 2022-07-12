namespace Application.SeedWork;

public class EnumerationResponse
{
    public int Id { get; set; }
    public string Name { get; set; }

    public EnumerationResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
