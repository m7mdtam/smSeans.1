public class AuditLog
{
    public int Id { get; set; }
    public int? user_id { get; set; }
    public string action { get; set; }
    public string details { get; set; }
    public DateTime? created_at { get; set; }
}
