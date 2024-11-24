namespace App.FieldPermission.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class FieldPermissionAttribute : Attribute
{
    public string FieldName { get; }

    public FieldPermissionAttribute(string fieldName)
    {
        FieldName = fieldName;
    }
}