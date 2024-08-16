public class UserRoleMappingCreateDto
{
    public int? user_id { get; set; }
    public int? role_id { get; set; }
}

public class UserRoleMappingUpdateDto
{
    public int Id { get; set; }
    public int? user_id { get; set; }
    public int? role_id { get; set; }
    public DateTime? assigned_at { get; set; }
}
