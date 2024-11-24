using System.Reflection;
using App.FieldPermission.Helpers;
using EfCore.Entities;
namespace App.FieldPermission.Attributes;

public class FieldPermissionService
{
    #region DI Implementations

    private readonly DapperHelper _helper;

    public FieldPermissionService( DapperHelper helper)
    {
        _helper = helper;
    }

    #endregion

    public async Task<bool> CanViewFieldAsync(int userId, int roleId, string entityName, string fieldName)
    {
        var fieldPermission = await _helper.ActionLinqAsync<bool, EfFieldPermissions>( 
        async (_, repo) =>
        {
            var fieldPer = await repo.FindAsync(
                rp => rp.EntityName == entityName && rp.FieldName == fieldName
                && (rp.RoleId == roleId || rp.UserId == userId)
            );
            
            return fieldPer?.IsCanView ?? true;
        });

        return fieldPermission;
    }
    
    public async Task<IEnumerable<EfFieldPermissions>> AllCanViewFieldAsync(int userId, int roleId, string entityName)
    {
        var fieldPermission =  await _helper.ActionLinqAsync<IEnumerable<EfFieldPermissions>, EfFieldPermissions>( 
        async (_, repo) =>
        {
            var fieldPer = await repo
                .FindAllAsync(
                rp => rp.EntityName == entityName
                && (rp.RoleId == roleId || rp.UserId == userId)
            );

            return fieldPer;
        });

        return fieldPermission;
    }

    public async Task<List<Dictionary<string, object>>> FilterFieldsAsync<T>(
        IEnumerable<T> data, int userId, int roleId, string entityName, bool useFieldQuery = false
    )
    {
        var result = new List<Dictionary<string, object>>();

        // Field query işlemi yapılacaksa, izin durumlarını sorgulama
        IEnumerable<EfFieldPermissions>? fieldStates = null;
        if (!useFieldQuery)
        {
            fieldStates = await AllCanViewFieldAsync(userId, roleId, entityName);
        }

        foreach (var item in data)
        {
            var dictionary = new Dictionary<string, object?>();
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var fieldPermissionAttribute = property.GetCustomAttribute<FieldPermissionAttribute>();
                if (fieldPermissionAttribute != null)
                {
                    var canView = useFieldQuery
                        ? await CanViewFieldAsync(userId, roleId, entityName, fieldPermissionAttribute.FieldName)
                        : GetCanViewValue(fieldStates!, fieldPermissionAttribute.FieldName);

                    if (canView)
                    {
                        dictionary.Add(property.Name, property.GetValue(item));
                    }
                }
            }

            result.Add(dictionary);
        }

        return result;
    }
    
    private bool GetCanViewValue(IEnumerable<EfFieldPermissions> fieldStates, string fieldName)
    {  
        // İlgili FieldName'e göre filtrele
        var relevantPermissions = fieldStates?.Where(s => s.FieldName == fieldName);

        if (relevantPermissions != null && relevantPermissions.Any())
        {
            // Tarihe göre en güncel izni al
            var latestPermission = relevantPermissions
                .OrderByDescending(f => f.CreatedTime) // En güncel izin en başta olacak
                .FirstOrDefault();

            // En güncel izin varsa, onun değerini döndür
            return latestPermission?.IsCanView ?? false;
        }

        // Eğer ilgili izin yoksa, varsayılan olarak erişim izni ver
        return true;
    }
}