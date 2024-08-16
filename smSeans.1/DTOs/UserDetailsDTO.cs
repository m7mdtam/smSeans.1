public class UserDetailsCreateDto
{
    public int? user_id { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public DateTime? birth_date { get; set; }
    public string gender { get; set; }
    public string phone_number { get; set; }
    public string qr_code { get; set; }
    public string card_id { get; set; }
    public DateTime? card_issued_at { get; set; }
    public string card_status { get; set; }
    public int address_id { get; set; }
}

public class UserDetailsUpdateDto
{
    public int Id { get; set; }
    public int? user_id { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public DateTime? birth_date { get; set; }
    public string gender { get; set; }
    public string phone_number { get; set; }
    public string qr_code { get; set; }
    public string card_id { get; set; }
    public DateTime? card_issued_at { get; set; }
    public string card_status { get; set; }
    public int address_id { get; set; }
}
