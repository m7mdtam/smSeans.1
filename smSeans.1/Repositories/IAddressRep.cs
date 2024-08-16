using System.Threading.Tasks;

public interface IAddressRepository
{
    Task<Address> GetAddressByIdAsync(int id);
    Task<int> CreateAddressAsync(Address address);
    Task <bool> UpdateAddressAsync(Address address);
    Task<bool> DeleteAddressAsync(int id);
}
