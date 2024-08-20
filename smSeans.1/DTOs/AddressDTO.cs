public class AddressCreateDto
{
  

    public required string street { get; set; }
    public required string city { get; set; }
    public required string state { get; set; }
    public required string zip_code { get; set; }
    public required string country { get; set; }
}

public class AddressUpdateDto
{
    public int Id { get; set; }
    public required string street { get; set; }
    public required string city { get; set; }
    public required string state { get; set; }
    public required string zip_code { get; set; }
    public  required string country { get; set; }
}
