
namespace AutoMapper;

/// <summary>
/// Use this interface to add AutoMapper as Singleton service in blazor application.
/// </summary>
public interface ISimpleMapperService
{

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
    public U ClassToViewModel<T, U>(T Class) where T : class where U : class;

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
    public T ViewModelToClass<T, U>(U ViewModel) where T : class where U : class;

}