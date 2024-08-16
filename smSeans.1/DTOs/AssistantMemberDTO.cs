public class AssistantMemberCreateDto
{
    public int? assistant_id { get; set; }
    public int? member_id { get; set; }
}

public class AssistantMemberUpdateDto
{
    public int assignment_id { get; set; }
    public int? assistant_id { get; set; }
    public int? member_id { get; set; }
    public DateTime? assigned_at { get; set; }
}
