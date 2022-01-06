using BookStoreModels;
using System.Collections.Generic;

namespace BookStoreRepository.Repository
{
    public interface IAddressRepository
    {
        string connectionString { get; set; }

        IEnumerable<AddressModel> FetchAddressOfUser(int userId);
        string InsertAddress(AddressModel addModel);
        string UpdateAddress(AddressModel addModel);
    }
}