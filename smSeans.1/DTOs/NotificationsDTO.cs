public class NotificationCreateDto
{
    public int? user_id { get; set; }
    public string message { get; set; }
    public bool? read_status { get; set; }
}

public class NotificationUpdateDto
{
    public int Id { get; set; }
    public int? user_id { get; set; }
    public string message { get; set; }
    public bool? read_status { get; set; }
}
