using App.FieldPermission.Dtos;
using App.FieldPermission.Models;
using AutoMapper;
using EfCore.Entities;
namespace App.FieldPermission.Profiles;


public class PermissionProfiles : Profile
{
    public PermissionProfiles()
    {
        CreateMap<EfProducts, ProductDtos.Get>();
        CreateMap<AddPermission, EfFieldPermissions>();
        CreateMap<UpdatePermission, EfFieldPermissions>();
    }
}