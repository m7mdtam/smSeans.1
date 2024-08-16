public class ErrorLog
{
    public int Id { get; set; }
    public string error_message { get; set; }
    public string stack_trace { get; set; }
    public DateTime? created_at { get; set; }
    public int? user_id { get; set; }
}
