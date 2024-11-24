using App.FieldPermission.Attributes;
using App.FieldPermission.Dtos;
using App.FieldPermission.Helpers;
using App.FieldPermission.Models;
using AutoMapper;
using EfCore.Entities;
using Microsoft.AspNetCore.Mvc;

namespace App.FieldPermission.Controllers;

[ApiController, Route("api/[controller]"), ApiExplorerSettings(GroupName = "v1")]
public class FieldPermissionController : ControllerBase
{
    private readonly DapperHelper _helper;
    private readonly FieldPermissionService _fieldPermissionService;
    private readonly IMapper _mapper;

    public FieldPermissionController(FieldPermissionService fieldPermissionService, IMapper mapper, DapperHelper helper)
    {
        _fieldPermissionService = fieldPermissionService;
        _mapper = mapper;
        _helper = helper;
    }

    [HttpGet("Products")]
    public async Task<IActionResult> GetProducts(
        [FromQuery] int userId,
        [FromQuery] int roleId
    )
    {
        var products = await _helper.ActionLinqAsync<IEnumerable<EfProducts>, EfProducts >(
            async (_, repo) =>
            {
                var list = await repo.FindAllAsync();
                return list;
            }
        ); 

        var mappedDto = _mapper.Map<IEnumerable<ProductDtos.Get>>(products);
        var filteredProducts = await _fieldPermissionService.FilterFieldsAsync(
            mappedDto, userId, roleId, "ProductDtos.Get"
        );

        return Ok(filteredProducts);
    }


    [HttpGet("Permissions")]
    public async Task<IActionResult> GetPermissions(
        [FromQuery] int? userId,
        [FromQuery] int? roleId,
        [FromQuery] string? entityName = "ProductDtos.Get"
    )
    {
        var list = await _helper.ActionLinqAsync<IEnumerable<EfFieldPermissions>, EfFieldPermissions >(
            async (_, repo) =>
            {
                if (!string.IsNullOrEmpty(entityName?.Trim()))
                {
                    var list = new List<EfFieldPermissions>();

                    if (roleId.HasValue && userId.HasValue)
                    {
                        var l= await repo.FindAllAsync(
                            s => (s.UserId == userId || s.RoleId == roleId)
                            && s.EntityName == entityName
                        );
                        list.AddRange(l);
                    }
                    
                    else if (roleId.HasValue)
                    {
                        var l= await repo.FindAllAsync(
                            s => s.RoleId == roleId
                            && s.EntityName == entityName
                        );
                        list.AddRange(l);
                    }
                    
                    else if (userId.HasValue)
                    {
                        var l= await repo.FindAllAsync(
                            s => s.UserId == userId
                                 && s.EntityName == entityName
                        );
                        list.AddRange(l);
                    }

                    else
                    {
                        var l= await repo.FindAllAsync(
                            s => s.EntityName == entityName
                        );
                        list.AddRange(l);
                    }
                    
                    return list;
                }
                else
                {
                    var l = await repo.FindAllAsync();
                    return l;
                }
            }
        );

        if (!list.Any())
        {
            return NotFound("No Permissions Found ");
        }
 
        return Ok(list);
    }

    
    [HttpPost("Permissions/Add")]
    public async Task<IActionResult> AddPermissions(
        [FromBody] AddPermission info
    )
    {
        await _helper.ActionLinqAsync<int, EfFieldPermissions >(
            async (_, repo) =>
            {
                var mappedModel = _mapper.Map<EfFieldPermissions>(info);
                mappedModel.CreatedTime = DateTime.Now;
                mappedModel.Id = Guid.NewGuid();
                
                await repo.BulkInsertAsync(new [] { mappedModel });
                
                return 1;
            }
        ); 
        
        return Ok();
    }
    
    [HttpPost("Permissions/Update")]
    public async Task<IActionResult> UpdatePermissions(
        [FromBody] UpdatePermission info
    )
    {
        await _helper.ActionLinqAsync<int, EfFieldPermissions >(
            async (_, repo) =>
            {
                var mappedModel = _mapper.Map<EfFieldPermissions>(info);
                mappedModel.CreatedTime = DateTime.Now;
                
                await repo.UpdateAsync(mappedModel);
                
                return 1;
            }
        ); 
        
        return Ok();
    }
    
    [HttpDelete("Permissions/{id}")]
    public async Task<IActionResult> UpdatePermissions(
        [FromRoute] Guid id
    )
    {
        await _helper.ActionLinqAsync<int, EfFieldPermissions >(
            async (_, repo) =>
            {
                await repo.DeleteAsync(s => s.Id == id);   
                return 1;
            }
        ); 
        
        return Ok();
    }
    
}