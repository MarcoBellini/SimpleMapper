namespace AutoMapper;

using System.Reflection;

/// <summary>
/// Implementation of mapper between classes and view models.
/// Use this class directly on offline apps, add a signleton service on
/// Blazor app using ISimpleMapperService interface
/// </summary>
public class SimpleMapper : ISimpleMapperService
{

    private bool PropertyIsInaccessible(PropertyInfo? propertyInfo)
    {
        if (propertyInfo is null) return true;
        if (!propertyInfo.CanRead) return true;
        if (!propertyInfo.CanWrite) return true;

        return false;
    }

    /// <summary>
    /// Convert a Class Instance to ViewModel
    /// </summary>
    /// <typeparam name="T">Class to map</typeparam>
    /// <typeparam name="U">ViewModel to map</typeparam>
    /// <param name="Class">Instance of class to Map</param>
    /// <returns>An Instance to a ViewModel object with mapped values</returns>
    /// <exception cref="NotImplementedException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public U ClassToViewModel<T, U>(T Class)
        where T : class
        where U : class
    {
        var ViewModelProperties = typeof(U).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var ViewModelInstance = Activator.CreateInstance<U>();


        foreach (var ViewModelProp in ViewModelProperties)
        {
            // Get property with MapPropertyName Attribute
            var ClassMapName = ViewModelProp.GetCustomAttribute<ClassPropertyNameAttribute>()?.GetPropertyName();
            
            if (string.IsNullOrEmpty(ClassMapName)) 
                continue;
           
            if (PropertyIsInaccessible(ViewModelProp)) 
                throw new NotImplementedException($"Missing get or set property on {ViewModelProp.Name}");

            // Find property in the class with the name from the attribute
            var ClassProperty = typeof(T).GetProperty(ClassMapName);

            if (ClassProperty is null) 
                throw new ArgumentNullException($"Missing property {ClassMapName} on class {nameof(T)}");

            if (PropertyIsInaccessible(ClassProperty)) 
                throw new NotImplementedException($"Missing get or set property on {ClassProperty.Name}");

            // Check if the types are compatible
            var ViewModelPropType = Nullable.GetUnderlyingType(ViewModelProp.PropertyType) ?? ViewModelProp.PropertyType;
            var ClassPropType = Nullable.GetUnderlyingType(ClassProperty.PropertyType) ?? ClassProperty.PropertyType;

            if(!ViewModelPropType.IsAssignableFrom(ClassPropType))            
                throw new InvalidCastException($"Cannot assign {ClassPropType.Name} to {ViewModelPropType.Name}");
            

            ViewModelProp.SetValue(ViewModelInstance, ClassProperty.GetValue(Class));
        }

        return ViewModelInstance;
    }

    /// <summary>
    /// Convert a ViewModel instance to a class
    /// </summary>
    /// <typeparam name="T">Class to Map</typeparam>
    /// <typeparam name="U">ViewModel to Map</typeparam>
    /// <param name="ViewModel">Instance to the VIewModel</param>
    /// <returns>Instance to a class with mapped values</returns>
    /// <exception cref="NotImplementedException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidCastException"></exception>
    public T ViewModelToClass<T, U>(U ViewModel)
        where T : class
        where U : class
    {
        var ViewModelProperties = typeof(U).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var ClassInstance = Activator.CreateInstance<T>();

        foreach (var ViewModelProp in ViewModelProperties)
        {
            // Get property with MapPropertyName Attribute
            var ClassMapName = ViewModelProp.GetCustomAttribute<ClassPropertyNameAttribute>()?.GetPropertyName();

            if (string.IsNullOrEmpty(ClassMapName))
                continue;

            if (PropertyIsInaccessible(ViewModelProp))
                throw new NotImplementedException($"Missing get or set property on {ViewModelProp.Name}");

            // Find property in the class with the name from the attribute
            var ClassProperty = typeof(T).GetProperty(ClassMapName);

            if (ClassProperty is null)
                throw new ArgumentNullException($"Missing property {ClassMapName} on class {nameof(T)}");

            if (PropertyIsInaccessible(ClassProperty))
                throw new NotImplementedException($"Missing get or set property on {ClassProperty.Name}");

            // Check if the types are compatible
            var ViewModelPropType = Nullable.GetUnderlyingType(ViewModelProp.PropertyType) ?? ViewModelProp.PropertyType;
            var ClassPropType = Nullable.GetUnderlyingType(ClassProperty.PropertyType) ?? ClassProperty.PropertyType;

            if (!ViewModelPropType.IsAssignableFrom(ClassPropType))
                throw new InvalidCastException($"Cannot assign {ClassPropType.Name} to {ViewModelPropType.Name}");

            ClassProperty.SetValue(ClassInstance, ViewModelProp.GetValue(ViewModel));
        }

        return ClassInstance;
    }
}
