public class RoleCreateDto
{
    public string role_name { get; set; }
    public string description { get; set; }
}

public class RoleUpdateDto
{
    public int Id { get; set; }
    public string role_name { get; set; }
    public string description { get; set; }
}
