
# Simple Mapper

A Simple Mapper between EF Core Entities and View Models for Blazor pages


## Usage/Examples

1. Install NuGet Package or Clone this repo and add a reference to this project

2. Add Singleton Service in `Program.cs` of your app:
```c#
builder.Services.AddSingleton<ISimpleMapperService, SimpleMapperService>();
```
3. Add namespace reference in your `_Imports.Razor`:
```c#
 using SimpleMapper;
```
4. Add `[ClassPropertyName(nameof(MyEntity.Id))]` attribute to every ViewModel property you want to map:

```c#
// Origin entity to map 
class MyEntity
{
    public int Id { get; set; }

    public string? Name { get; set; }

    // Not mapped property
    public string? Address { get; set; }
}


// ViewModel where Types match with entity types
class MyViewModel
{
    [ClassPropertyName(nameof(MyEntity.Id))]
    public int MyId { get; set; }

    [ClassPropertyName(nameof(MyEntity.Name))]
    public string? MyName { get; set; }

    // Not mapped property, but can be added
    public int MyAge { get; set; } = 30; 
}
```
5. Use SimpleMapper in your blazor component:
```c#
@inject ISimpleMapperService Mapper

// . . . 

@code
{
    private async Task YourFunction()
    {
        // Create new ViewModel instance form Entity
        var MyModel = Mapper.ClassToViewModel<MyEntity, MyViewModel>(Entity);

        // Create new Entity instance from ViewModel
        var MyEntity = Mapper.ViewModelToClass<MyEntity, MyViewModel>(ViewModel);

        NavigationManager.NavigateTo("/home");
    }
}
```



