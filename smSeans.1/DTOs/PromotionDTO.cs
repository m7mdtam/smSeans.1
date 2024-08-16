public class PromotionCreateDto
{
    public string title { get; set; }
    public string description { get; set; }
    public DateTime? start_date { get; set; }
    public DateTime? end_date { get; set; }
    public int? user_id { get; set; }
}

public class PromotionUpdateDto
{
    public int Id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public DateTime? start_date { get; set; }
    public DateTime? end_date { get; set; }
    public int? user_id { get; set; }
}
