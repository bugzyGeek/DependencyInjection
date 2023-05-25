# DependencyInjection-Factory

This is a C# library that provides some classes and methods for adding and initializing services to an `IServiceCollection` using a factory pattern. The factory pattern is a design pattern that allows creating objects without specifying the exact class or constructor of the object. This library can help you with dependency injection and creating instances of different types at runtime.

![GitHub Workflow Status](https://img.shields.io/github/workflow/status/your-username/your-repo-name/your-workflow-name)
![GitHub release (latest by date)](https://img.shields.io/github/v/release/your-username/your-repo-name)
![Nuget](https://img.shields.io/nuget/v/your-package-name)
![License](https://img.shields.io/github/license/your-username/your-repo-name)

## Features

- `FactoryServices`: A static class that extends the `IServiceCollection` interface and provides some extension methods for adding and initializing services using a factory pattern.
- `FactoryScope`: An enum that defines the three types of service lifetimes: Transient, Scoped, and Singleton.
- `IFactory<T>`: An interface that defines a method for creating instances of type `T`.
- `Factory<T>`: A class that implements the `IFactory<T>` interface and creates instances of type `T` using a `Func<Type, T?>` delegate and a type parameter.

## Usage

To use this library, you need to add a reference to it in your project and import the namespace:

```csharp
using DependencyInjection.Factory;
```

Then, you can use the `FactoryServices` class to add and initialize services to an `IServiceCollection`. For example:

```csharp
// Create an IServiceCollection
var services = new ServiceCollection();

// Initialize the factory services
services.InitializeFactory();

// Add a service of type SomeInterface with the implementation type SomeImplementation using a scoped lifetime
services.AddFactory<SomeInterface, SomeImplementation>(FactoryScope.Scope);

// Add another service of type SomeInterface with the implementation type AnotherImplementation using a scoped lifetime
services.AddFactory<SomeInterface, AnotherImplementation>(FactoryScope.Scope);

// Add a service of type AnotherClass using a transient lifetime
services.AddFactory<AnotherClass>(FactoryScope.Transient);
```

The `AddFactory` extension method registers the service type and the implementation type with the specified lifetime to the `IServiceCollection` parameter. It also registers an `IFactory<T>` service and a `Factory<T>` service for each service type `T`. The `IFactory<T>` interface defines a method for creating instances of type `T`, and the `Factory<T>` class implements this interface and creates instances of type `T` using a `Func<Type, T?>` delegate and a type parameter. The `Func<Type, T?>` delegate takes a `Type` parameter and returns an instance of type `T` that matches that `Type` parameter. If no matching service is found, it creates a new service scope using the `CreateScope` method and tries again. If still no matching service is found, it returns null. The `AddFactory` extension method also registers a `Func<Type, T?>` delegate as a singleton service for each service type `T`. This delegate uses the `GetServices<T>` method to get all the registered services of type `T` from the `IServiceCollection` parameter and returns the first one that matches the `Type` parameter.

You can also use the `Factory<T>` class or the `IFactory<T>` interface to create instances of different types at runtime. For example:

```csharp
// Create an instance of SomeClass that takes an IFactory<SomeInterface> as a parameter using dependency injection
public class SomeClass
{
    private readonly IFactory<SomeInterface> _factory;

    public SomeClass(IFactory<SomeInterface> factory)
    {
        _factory = factory;
    }

    // Create an instance of SomeImplementation using the factory
    public SomeInterface Create()
    {
        return _factory.Create<SomeImplementation>();
    }
}

// Create an instance of AnotherClass that takes an IFactory<SomeInterface> as a parameter using dependency injection
public class AnotherClass
{
    private readonly IFactory<SomeInterface> _factory;

    public AnotherClass(IFactory<SomeInterface> factory)
    {
        _factory = factory;
    }

    // Create an instance of AnotherImplementation using the factory
    public SomeInterface Create()
    {
        return _factory.Create<AnotherImplementation>();
    }
}

// Create an instance of YetAnotherClass that takes an IFactory<AnotherClass> as a parameter using dependency injection
public class YetAnotherClass
{
    private readonly IFactory<AnotherClass> _factory;

    public YetAnotherClass(IFactory<AnotherClass> factory)
    {
        _factory = factory;
    }

    // Create an instance of AnotherClass using the factory
    public AnotherClass Create()
    {
        return _factory.Create<AnotherClass>();
    }
}

// Create an instance of OneMoreClass that takes an IFactory<OneMoreClass> as a parameter using dependency injection
public class OneMoreClass
{
    private readonly IFactory<OneMoreClass> _factory;

    public OneMoreClass(IFactory<OneMoreClass> factory)
    {
        _factory = factory;
    }

    // Create an instance of OneMoreClass using the factory
    public OneMoreClass Create()
    {
        return _factory.Create<OneMoreClass>();
    }
}
```

## Examples

Here are some examples of using this library in different projects or frameworks:

### ASP.NET Core

In this example, we use this library to create instances of different email and payment services in an ASP.NET Core web application.

```csharp
// In Startup.cs, configure the services and add the factory services
public void ConfigureServices(IServiceCollection services)
{
    // Add the default ASP.NET Core services
    services.AddControllersWithViews();

    // Initialize the factory services
    services.InitializeFactory();

    // Add some services using the factory pattern
    services.AddFactory<IEmailService, GmailService>(FactoryScope.Singleton);
    services.AddFactory<IEmailService, OutlookService>(FactoryScope.Singleton);
    services.AddFactory<IPaymentService, PayPalService>(FactoryScope.Transient);
    services.AddFactory<IPaymentService, StripeService>(FactoryScope.Transient);
}

// In HomeController.cs, inject the factory services and use them to create instances of different service types
public class HomeController : Controller
{
    private readonly IFactory<IEmailService> _emailFactory;
    private readonly IFactory<IPaymentService> _paymentFactory;

    public HomeController(IFactory<IEmailService> emailFactory, IFactory<IPaymentService> paymentFactory)
    {
        _emailFactory = emailFactory;
        _paymentFactory = paymentFactory;
    }

    public IActionResult Index()
    {
        // Create an instance of GmailService using the email factory
        var gmail = _emailFactory.Create<GmailService>();
        gmail.SendEmail("Hello from Gmail");

        // Create an instance of OutlookService using the email factory
        var outlook = _emailFactory.Create<OutlookService>();
        outlook.SendEmail("Hello from Outlook");

        // Create an instance of PayPalService using the payment factory
        var paypal = _paymentFactory.Create<PayPalService>();
        paypal.MakePayment(100);

        // Create an instance of StripeService using the payment factory
        var stripe = _paymentFactory.Create<StripeService>();
        stripe.MakePayment(200);

        return View();
    }
}
```

### Blazor

In this example, we use this library to create instances of different greeting services in a Blazor web application.

```csharp
// In Program.cs, configure the services and add the factory services
public static async Task Main(string[] args)
{
  var builder = WebAssemblyHostBuilder.CreateDefault(args);
  builder.RootComponents.Add<App>("app");

  // Initialize the factory services
  builder.Services.InitializeFactory();

  // Add some services using the factory pattern
  builder.Services.AddFactory<IGreetingService, EnglishGreetingService>(FactoryScope.Singleton);
  builder.Services.AddFactory<IGreetingService, SpanishGreetingService>(FactoryScope.Singleton);
  builder.Services.AddFactory<IGreetingService, FrenchGreetingService>(FactoryScope.Singleton);

  await builder.Build().RunAsync();
}

// In Index.razor, inject the factory service and use it to create instances of different greeting types
@page "/"
@inject IFactory<IGreetingService> GreetingFactory

<h1>Hello, world!</h1>

<select @bind="SelectedLanguage" @bind:event="onchange">
  <option value="English">English</option>
  <option value="Spanish">Spanish</option>
  <option value="French">French</option>
</select>

<p>@Greeting</p>

@code {
  private string SelectedLanguage { get; set; } = "English";
  private string Greeting { get; set; } = "Hello";

  protected override void OnInitialized()
  {
      UpdateGreeting();
  }

  private void UpdateGreeting()
  {
      switch (SelectedLanguage)
      {
          case "English":
              var english = GreetingFactory.Create<EnglishGreetingService>();
              Greeting = english.SayHello();
              break;
          case "Spanish":
              var spanish = GreetingFactory.Create<SpanishGreetingService>();
              Greeting = spanish.SayHello();
              break;
          case "French":
              var french = GreetingFactory.Create<FrenchGreetingService>();
              Greeting = french.SayHello();
              break;
      }
  }
}
```

### dotnet Maui

In this example, we use this library to create instances of different color services in a dotnet Maui application.

```csharp
// In Startup.cs, configure the services and add the factory services
public class Startup : IStartup
{
    public void Configure(IAppHostBuilder appBuilder)
    {
        appBuilder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            })
            .ConfigureServices(services =>
            {
                // Initialize the factory services
                services.InitializeFactory();

                // Add some services using the factory pattern
                services.AddFactory<IColorService, RedColorService>(FactoryScope.Singleton);
                services.AddFactory<IColorService, GreenColorService>(FactoryScope.Singleton);
                services.AddFactory<IColorService, BlueColorService>(FactoryScope.Singleton);
            });
    }
}

// In MainPage.xaml.cs, inject the factory service and use it to create instances of different color types
public partial class MainPage : ContentPage
{
    private readonly IFactory<IColorService> _colorFactory;

    public MainPage(IFactory<IColorService> colorFactory)
    {
        InitializeComponent();
        _colorFactory = colorFactory;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        // Get the button that was clicked
        var button = sender as Button;

        // Get the text of the button
        var text = button.Text;

        // Create an instance of a color service based on the text of the button
        switch (text)
        {
            case "Red":
                var red = _colorFactory.Create<RedColorService>();
                BackgroundColor = red.GetColor();
                break;
            case "Green":
                var green = _colorFactory.Create<GreenColorService>();
                BackgroundColor = green.GetColor();
                break;
            case "Blue":
                var blue = _colorFactory.Create<BlueColorService>();
                BackgroundColor = blue.GetColor();
                break;
        }
    }
}
```