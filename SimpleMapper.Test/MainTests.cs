namespace SimpleMapper.Test;

using SimpleMapper;


/// <summary>
/// Origin class to map
/// </summary>
public class MyClass
{
    public int Id { get; set; }

    public string? Name { get; set; }

    // Not mapped property
    public byte Version { get; set; } = 1;
}

/// <summary>
/// ViewModel where Types match with class types
/// </summary>
public class MyViewModel
{
    [ClassPropertyName(nameof(MyClass.Id))]
    public int MyId { get; set; }

    [ClassPropertyName(nameof(MyClass.Name))]
    public string? MyName { get; set; }

    public int MyAge { get; set; } = 18; // Not mapped property, but can be added
}

/// <summary>
/// ViewModel where Types do NOT match with class types
/// </summary>
public class MyViewModelWrong
{
    [ClassPropertyName(nameof(MyClass.Id))]
    public DateTime MyId { get; set; }

    [ClassPropertyName(nameof(MyClass.Name))]
    public string? MyName { get; set; }
}

public class MainTests
{
    /// <summary>
    /// Test try to map a class to a view model with correct types.
    /// </summary>
    [Fact]
    public void ClassToViewModel()
    {

        MyClass Cls = new MyClass() { Id = 1, Name = "My Name"};
        SimpleMapperService Mapper = new();

        var MyModel = Mapper.ClassToViewModel<MyClass, MyViewModel>(Cls);

        Assert.NotNull(MyModel);
        Assert.Equal(Cls.Id, MyModel.MyId);
        Assert.Equal(Cls.Name, MyModel.MyName);
    }

    /// <summary>
    /// Catch exception when trying to map a class to a view model with wrong types.
    /// </summary>
    [Fact]   
    public void ClassToViewModelWrong()
    {
        MyClass Cls = new MyClass() { Id = 1, Name = "My Name" };
        SimpleMapperService Mapper = new();     
    
        Assert.Throws<InvalidCastException>(() => 
        {
            var WrongModel = Mapper.ClassToViewModel<MyClass, MyViewModelWrong>(Cls);
        });
    }

    /// <summary>
    /// Test try to map a view model into a class with correct types.
    /// </summary>
    [Fact]
    public void ViewModelToClass()
    {

        MyViewModel Model = new MyViewModel() { MyId = 1, MyName = "My Name" };
        SimpleMapperService Mapper = new();

        var MyClass= Mapper.ViewModelToClass<MyClass, MyViewModel>(Model);

        Assert.NotNull(MyClass);
        Assert.Equal(Model.MyId, MyClass.Id);
        Assert.Equal(Model.MyName, MyClass.Name);
    }

    /// <summary>
    /// Test try to map a class into a view model with wrong types.
    /// </summary>
    [Fact]
    public void ViewModelToClassWrong()
    {

        MyViewModelWrong Model = new MyViewModelWrong() { MyId = DateTime.Today, MyName = "My Name" };
        SimpleMapperService Mapper = new();      

        Assert.Throws<InvalidCastException>(() =>
        {
            var WrongClass= Mapper.ViewModelToClass<MyClass, MyViewModelWrong>(Model);
        });
    }
}
