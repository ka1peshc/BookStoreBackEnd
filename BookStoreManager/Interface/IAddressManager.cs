using BookStoreModels;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BookStoreManager.Manager
{
    public interface IAddressManager
    {
        IConfiguration Configuration { get; }

        string AddNewAddress(AddressModel addModel);
        IEnumerable<AddressModel> DisplayAddress(int userId);
        string UpdateAddress(AddressModel addModel);
    }
}