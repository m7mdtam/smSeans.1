public class AuditLogCreateDto
{
    public int? user_id { get; set; }
    public string action { get; set; }
    public string details { get; set; }
}

public class AuditLogUpdateDto
{
    public int Id { get; set; }
    public int? user_id { get; set; }
    public string action { get; set; }
    public string details { get; set; }
}
