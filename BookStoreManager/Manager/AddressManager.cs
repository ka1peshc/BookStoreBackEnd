using BookStoreModels;
using BookStoreRepository.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManager.Manager
{
    public class AddressManager : IAddressManager
    {
        private readonly IAddressRepository repository;
        public AddressManager(IAddressRepository repository, IConfiguration configuration)
        {
            this.repository = repository;
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public string AddNewAddress(AddressModel addModel)
        {
            try
            {
                return this.repository.InsertAddress(addModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string UpdateAddress(AddressModel addModel)
        {
            try
            {
                return this.repository.UpdateAddress(addModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<AddressModel> DisplayAddress(int userId)
        {
            try
            {
                return this.repository.FetchAddressOfUser(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
