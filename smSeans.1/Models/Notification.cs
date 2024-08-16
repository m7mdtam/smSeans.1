public class Notification
{
    public int Id { get; set; }
    public int? user_id { get; set; }
    public string message { get; set; }
    public bool? read_status { get; set; }
    public DateTime? created_at { get; set; }
}
