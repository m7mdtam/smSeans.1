public class SessionCreateDto
{
    public int? user_id { get; set; }
    public DateTime session_date { get; set; }
    public int? credits_used { get; set; }
    public int? trainer_id { get; set; }
    public bool? isCompleted { get; set; }
    public int? PlannedDuration { get; set; }
    public int? ActualDuration { get; set; }
}

public class SessionUpdateDto
{
    public int Id { get; set; }
    public int? user_id { get; set; }
    public DateTime session_date { get; set; }
    public int? credits_used { get; set; }
    public int? trainer_id { get; set; }
    public bool? isCompleted { get; set; }
    public int? PlannedDuration { get; set; }
    public int? ActualDuration { get; set; }
}
