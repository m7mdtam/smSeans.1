public class Promotion
{
    public int Id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public DateTime? start_date { get; set; }
    public DateTime? end_date { get; set; }
    public DateTime? created_at { get; set; }
    public int? user_id { get; set; }
}
