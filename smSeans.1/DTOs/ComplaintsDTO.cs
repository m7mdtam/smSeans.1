public class ComplaintsCreateDto
{
    public string ComplaintType { get; set; }
    public int? user_id { get; set; }
}

public class ComplaintsUpdateDto
{
    public int Id { get; set; }
    public string ComplaintType { get; set; }
    public int? user_id { get; set; }
}
