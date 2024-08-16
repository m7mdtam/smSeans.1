public class UserDocument
{
    public int Id { get; set; }
    public int? user_id { get; set; }
    public string document_type { get; set; }
    public string document_path { get; set; }
    public DateTime? uploaded_at { get; set; }
}
