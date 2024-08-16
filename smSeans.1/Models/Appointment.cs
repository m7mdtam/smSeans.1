public class Appointment
{
    public int Id { get; set; }
    public int? member_id { get; set; }
    public int? assistant_id { get; set; }
    public DateTime appointment_date { get; set; }
    public string status { get; set; }
    public DateTime? created_at { get; set; }
}
