public class PaymentCreateDto
{
    public int? user_id { get; set; }
    public decimal amount { get; set; }
    public DateTime payment_date { get; set; }
    public string method { get; set; }
}

public class PaymentUpdateDto
{
    public int Id { get; set; }
    public int? user_id { get; set; }
    public decimal amount { get; set; }
    public DateTime payment_date { get; set; }
    public string method { get; set; }
}
