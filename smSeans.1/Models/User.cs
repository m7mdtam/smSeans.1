public class User
{
    public int Id { get; set; }
    public string username { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public string google_id { get; set; }
    public string status { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
    public DateTime? last_login_date { get; set; }
    public int? session_count { get; set; }
    public string role_name { get; set; }
}
