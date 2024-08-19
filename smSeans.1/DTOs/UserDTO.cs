public class UserDTO
{
    public string username { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public string google_id { get; set; }
    public string status { get; set; }
    public string role_name { get; set; }
   
}
public class UserUpdateDto
{
    public int Id { get; set; }
    public string username { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public string google_id { get; set; }
    public string status { get; set; }
    public string role_name { get; set; }
}