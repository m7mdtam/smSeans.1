public class ErrorLogCreateDto
{
    public string error_message { get; set; }
    public string stack_trace { get; set; }
    public int? user_id { get; set; }
}

public class ErrorLogUpdateDto
{
    public int Id { get; set; }
    public string error_message { get; set; }
    public string stack_trace { get; set; }
    public int? user_id { get; set; }
}
