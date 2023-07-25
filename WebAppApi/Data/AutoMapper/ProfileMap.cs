using AutoMapper;
using Data.Model;
using Data.ModelView;

namespace Data.AutoMapper
{
  public  class ProfileMap : Profile
    {
        public ProfileMap()
        {
            CreateMap<Product, ProductView>().ReverseMap();
            CreateMap<Category,CategoryView>().ReverseMap();
            CreateMap<User, UserView>().ReverseMap();
        }
    }
}
