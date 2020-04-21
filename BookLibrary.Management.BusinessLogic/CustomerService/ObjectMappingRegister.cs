using BookLibrary.Management.Contract.Model;
using Mapster;

namespace BookLibrary.Management.BusinessLogicLayer.CustomerService
{
    public class ObjectMappingRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //config.NewConfig<CustomerDto, Customer>()
                //.EnumMappingStrategy(EnumMappingStrategy.ByValue);

            //config.NewConfig<CustomerDto, Customer>()
            //    .Map(dest => dest.GenderId, src => src.Gender);
        }
    }
}
