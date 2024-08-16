public class ComplaintDetailsCreateDto
{
    public int complaintId { get; set; }
    public string complaintExplanation { get; set; }
    public int? isImportant { get; set; }
    public bool? isArchived { get; set; }
    public string status { get; set; }
    public string solution { get; set; }
    public DateTime? timeToBeSolved { get; set; }
}

public class ComplaintDetailsUpdateDto
{
    public int Id { get; set; }
    public int complaintId { get; set; }
    public string complaintExplanation { get; set; }
    public int? isImportant { get; set; }
    public bool? isArchived { get; set; }
    public string Status { get; set; }
    public string Solution { get; set; }
    public DateTime? timeToBeSolved { get; set; }
}
