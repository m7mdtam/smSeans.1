public class NotificationSettingCreateDto
{
    public int? user_id { get; set; }
    public bool? email_notifications { get; set; }
    public bool? sms_notifications { get; set; }
    public bool? push_notifications { get; set; }
}

public class NotificationSettingUpdateDto
{
    public int Id { get; set; }
    public int? user_id { get; set; }
    public bool? email_notifications { get; set; }
    public bool? sms_notifications { get; set; }
    public bool? push_notifications { get; set; }
}
