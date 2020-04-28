using AutoMapper;
using WebStore.Domain.Entities.Identity;
using WebStore.ViewModels.Identity;

namespace WebStore.Infrastructure.AutoMapper
{
    public class ViewModelsMapping : Profile
    {
        public ViewModelsMapping()
        {
            CreateMap<RegisterUserViewModel, User>()
               .ForMember(user => user.UserName, opt => opt.MapFrom(model => model.UserName));
            //если мы хотим запомнить свойство username из обЬекта RegisterUserViewModel то надо взять данные из свойства модели которе ноходится внутри ее свойства UserName
        }
    }
}