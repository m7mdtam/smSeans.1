using Dapper;
using System.Data;
using System.Threading.Tasks;

public class AddressRepository : IAddressRepository
{
    private readonly DapperContext _context;

    public AddressRepository(DapperContext context)
    {
        _context = context;
    }

    // Get an address by ID
    public async Task<Address> GetAddressByIdAsync(int id)
    {
        var query = "SELECT * FROM Address WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            return await connection.QuerySingleOrDefaultAsync<Address>(query, new { Id = id });
        }
    }

    // Create a new address and return the number of rows affected
    public async Task<int> CreateAddressAsync(Address address)
    {
        var query = @"INSERT INTO Address (street, city, state, zip_code, country) 
                      VALUES (@street, @city, @state, @zip_code, @country)";

        using (var connection = _context.CreateConnection())
        {
            return await connection.ExecuteAsync(query, new
            {
                address.street,
                address.city,
                address.state,
                address.zip_code,
                address.country
            });
        }
    }

    // Update an existing address and return true if successful (false if not)
    public async Task<bool> UpdateAddressAsync(Address address)
    {
        var query = @"UPDATE Address SET street = @street, city = @city, state = @state, 
                      zip_code = @zip_code, country = @country WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var rowsAffected = await connection.ExecuteAsync(query, new
            {
                address.street,
                address.city,
                address.state,
                address.zip_code,
                address.country,
                address.Id
            });

            return rowsAffected > 0;
        }
    }

    // Delete an address and return true if successful (false if not)
    public async Task<bool> DeleteAddressAsync(int id)
    {
        var query = "DELETE FROM Address WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }
    }
}

    