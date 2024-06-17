namespace matriculate_microservice.models;

public class MatriculateModel
{
    public int ID { get; set; }
    public int StudentID { get; set; }
    // Array of course IDs
    public int[] Courses { get; set; }
    public string? Semester { get; set; } // semester of matriculation
    public string? School { get; set; } // professional school
    public int Year { get; set; } // year of matriculation
    public string? Status { get; set; } // active, inactive

}
