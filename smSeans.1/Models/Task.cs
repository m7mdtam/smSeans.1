public class Task
{
    public int Id { get; set; }
    public int? user_id { get; set; }
    public string task_description { get; set; }
    public DateTime? due_date { get; set; }
    public string status { get; set; }
    public DateTime? created_at { get; set; }
    public int? session_id { get; set; }
    public string title { get; set; }

    public int? assigned_by { get; set; }
}
