public class UserDocumentCreateDto
{
    public int? user_id { get; set; }
    public string document_type { get; set; }
    public string document_path { get; set; }
}

public class UserDocumentUpdateDto
{
    public int Id { get; set; }
    public int? user_id { get; set; }
    public string document_type { get; set; }
    public string document_path { get; set; }
}
