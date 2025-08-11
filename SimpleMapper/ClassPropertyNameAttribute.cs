namespace SimpleMapper;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public class ClassPropertyNameAttribute : Attribute
{
    private string PropertyName;

    /// <summary>
    /// Name of the property in the origin class to map to this property
    /// into the view model.
    /// </summary>
    /// <param name="OriginPropertyName"></param>
    public ClassPropertyNameAttribute(string OriginPropertyName)
    {
        PropertyName = OriginPropertyName;
    }

    /// <summary>
    /// Get Name of mapped property
    /// </summary>
    /// <returns></returns>
    public string GetPropertyName() => PropertyName;
}
