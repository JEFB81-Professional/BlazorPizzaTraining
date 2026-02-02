# add nuget
 dotnet add package Learn.ExampleLibrary001 -s ../ExampleLibrary001/bin/Release --prerelease 
 when using version   <Version>0.1.0-beta</Version>
 -- use command ```-- prerelease``` when the build is not final:
 - dotnet add package Learn.ExampleLibrary001 -s ../ExampleLibrary001/bin/Release --prerelease 
 When final then: 
 - dotnet add package Learn.ExampleLibrary001 -s ../ExampleLibrary001/bin/Release

# Differences between a class library and a Razor class library
A class library is a common package delivery structure in .NET applications, and a Razor class library is similar in structure with a few other features configured in the project file.
- <ProjectReference Include="..\ExampleLibrary001\ExampleLibrary001.csproj" />

# Razor class libraries
A Razor class library is a .NET project type. It contains Razor components, pages, HTML, Cascading Style Sheet (CSS) files, JavaScript, images, and other static web content that a Blazor application can reference. Like other .NET class library projects, Razor class libraries can be bundled as a NuGet package and shared on NuGet package repositories such as NuGet.org.

# Handle form validations server-side on form submission
When you use an EditForm component, three events are available for responding to form submission:

OnSubmit: This event fires whenever the user submits a form, regardless of the results of validation.
OnValidSubmit: This event fires when the user submits a form and their input passes validation.
OnInvalidSubmit: This event fires when the user submits a form and their input fails validation.

- If you use OnSubmit, the other two events aren't fired. Instead, you can use the EditContext parameter to check whether to process the input data or not.

# Control your app's form validation
Blazor performs validation at two different times:

- Field validation is executed when a user tabs out of a field. Field validation ensures that a user is aware of the validation problem at the earliest possible time.
- Model validation is executed when the user submits the form. Model validation ensures that invalid data isn't stored.
If a form fails validation, messages are displayed in the ValidationSummary and ValidationMessage components. To customize these messages, you can add an ErrorMessage attribute to the data annotation for each field in the model:
```csharp
public class Pizza
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "You must set a name for your pizza.")]
    public string Name { get; set; }
    ...
```
- you can create a custom validation attribute. Start by creating a class that inherits from the ValidationAttribute class and overrides the IsValid method:
```csharp
public class PizzaBase : ValidationAttribute
{
    public string GetErrorMessage() => $"Sorry, that's not a valid pizza base.";

    protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
    {
        if (value != "Tomato" || value != "Pesto")
        {
            return new ValidationResult(GetErrorMessage());
        }

        return ValidationResult.Success;
    }
}
```
- Now, you can use your custom validation attribute as you use the built-in attributes in the model class


# Add validation components to the form
- To configure your form to use data-annotation validation, first make sure the input control is bound to the model properties. Then, add the DataAnnotationsValidator component somewhere within the EditForm component. 
- To display the messages that validation generates, use the ValidationSummary component, which shows all the validation messages for all controls in the form.
example:
```csharp
@page "/admin/createpizza"

<h1>Add a new pizza</h1>

<EditForm Model="@pizza">
    <DataAnnotationsValidator />
    <ValidationSummary />
    
    <InputText id="name" @bind-Value="pizza.Name" />
    <ValidationMessage For="@(() => pizza.Name)" />
    
    <InputText id="description" @bind-Value="pizza.Description" />
    
    <InputText id="chefemail" @bind-Value="pizza.ChefEmail" />
    <ValidationMessage For="@(() => pizza.ChefEmail)" />
    
    <InputNumber id="price" @bind-Value="pizza.Price" />
    <ValidationMessage For="@(() => pizza.Price)" />
</EditForm>

@code {
    private Pizza pizza = new();
}
```

# Annotations:
includes the [Required] attribute to ensure that the Name and Price values are always completed. It also uses the [Range] attribute to check that the price entered is within a sensible range for a pizza. Finally, it uses the [EmailAddress] attribute to check the ChefEmail value entered is a valid email address.
Other annotations that you can use in a model include:

[ValidationNever]: Use this annotation when you want to ensure that the field is never included in validation.
[CreditCard]: Use this annotation when you want to record a valid credit card number from the user.
[Compare]: Use this annotation when you want to ensure that two properties in the model match.
[Phone]: Use this annotation when you want to record a valid telephone number from the user.
[RegularExpression]: Use this annotation to check the format of a value by comparing it to a regular expression.
[StringLength]: Use this annotation to check that the length of a string value doesn't exceed a maximum length.
[Url]: Use this annotation when you want to record a valid URL from the user.

# validations forms:
When you use the EditForm component in Blazor, you have versatile validation options available without writing complex code:

- In your model, you can use data annotations against each property to tell Blazor when values are required and what format they should be in.
Within your EditForm component, add the DataAnnotationsValidator component, which checks the model annotations against the user's entered values.
- Use the ValidationSummary component when you want to display a summary of all the validation messages in a submitted form.
- Use the ValidationMessage component when you want to display the validation message for a specific model property.

The ValidationSummary component is placed at the top of the form (after DataAnnotationsValidator) and will display all validation errors in a summary list when the form is submitted with invalid data.

# Validate user input in Blazor forms
When you collect information from a website user, it's important to check that it makes sense and is in the right form:

For business reasons: Customer information such as a telephone number or order details must be correct to give good service to users. For example, if your webpage can spot a malformed telephone number as soon as the user enters it, you can prevent costly delays later.
For technical reasons: If your code uses form input for calculations or other processing, incorrect input can cause errors and exceptions.
For security reasons: Malicious users might try to inject code by exploiting input fields that aren't checked.
# Use an EventCallback to handle events across components
-  to pass events between Blazor components.
A Blazor page can contain one or more Blazor components, and components can be nested in a parent-child relationship. An event in a child component can trigger an event-handler method in a parent component by using an EventCallback. A callback references a method in the parent component. The child component can run the method by invoking the callback. This mechanism is similar to using a delegate to reference a method in a C# application.
A callback can take a single parameter. EventCallback is a generic type. The type parameter specifies the type of the argument passed to the callback.

The following code shows the TextDisplay component. It provides the input string in the form of an <input> element that enables the user to enter a text value.
```csharp
@* TextDisplay component *@
@using WebApplication.Data;

<p>Enter text:</p>
<input @onkeypress="HandleKeyPress" value="@data" />

@code {
    [Parameter]
    public EventCallback<KeyTransformation> OnKeyPressCallback { get; set; }

    private string data;

    private async Task HandleKeyPress(KeyboardEventArgs e)
    {
        KeyTransformation t = new KeyTransformation() { Key = e.Key };
        await OnKeyPressCallback.InvokeAsync(t);
        data += t.TransformedKey;
    }
}
```
- The TextDisplay component uses an EventCallback object named OnKeyPressCallback. The code in the HandleKeypress method invokes the callback. The @onkeypress event handler runs each time a key is pressed and calls the HandleKeypress method. The HandleKeypress method creates a KeyTransformation object using the key the user pressed and passes this object as the parameter to the callback. The KeyTransformation type is a simple class with two fields:
```csharp
namespace WebApplication.Data
{
    public class KeyTransformation
    {
        public string Key { get; set; }
        public string TransformedKey { get; set; }
    }
}
```
# default action
you can override the default action with the preventDefault attribute of the event, like this:
- <input value=@data @onkeypress="ProcessKeyPress" @onkeypress:preventDefault />

Some events in a child element in the DOM can trigger events in their parent elements. I
<div> element contains an @onclick event handler. The <button> inside the <div> has its own @onclick event handler.

## example
<div @onclick="HandleDivClick">
    <button class="btn btn-primary" @onclick="IncrementCount">Click me</button>
    <input value=@data @onkeypress="ProcessKeyPress" @onkeypress:preventDefault />
</div>

```csharp
@code {
    private async Task HandleDivClick()
    {
        await JS.InvokeVoidAsync("alert", "Div click");
    }

    private async Task ProcessKeyPress(KeyboardEventArgs e)
    {
        // Omitted for brevity
    }

    private int currentCount = 0;

    private void IncrementCount(MouseEventArgs e)
    {
        // Omitted for brevity
    }
}
```
- When the app runs, if the user clicks any element (or empty space) in the area occupied by the <div> element, the method HandleDivClick runs and displays a message. 
If the user selects the Click me button, the IncrementCount method runs, followed by HandleDivClick;
the @onclick event propagates up the DOM tree. 
If the <div> was part of another element that also handled the @onclick event, that event handler would also run, and so on, to the root of the DOM tree. 
You can curtail this upwards proliferation of events with the stopPropagation attribute of an event, as shown here:
``` html
<div @onclick="HandleDivClick">
    <button class="btn btn-primary" @onclick="IncrementCount" @onclick:stopPropagation>Click me</button>
    <!-- Omitted for brevity -->
</div>
```

# Override default DOM actions for events
Several DOM events have default actions that run when the event occurs, regardless of whether there's an event handler available for that event. For example, the @onkeypress event for an <input> element always displays the character that corresponds to the key pressed by the user and then handles the key press. 
For example, the @onkeypress event for an <input> element always displays the character that corresponds to the key pressed by the user and then handles the key press. In the next example, the @onkeypress event is used to convert the user's input to uppercase. Additionally, if the user types an @ character, the event handler displays an alert:
```csharp
<input value=@data @onkeypress="ProcessKeyPress"/>

@code {
    private string data;

    private async Task ProcessKeyPress(KeyboardEventArgs e)
    {
        if (e.Key == "@")
        {
            await JS.InvokeVoidAsync("alert", "You pressed @");
        }
        else
        {
            data += e.Key.ToUpper();
        }
    }
}
```
- If you run this code and press the @ key, the alert is displayed, but the @ character is also added to the input. The addition of the @ character is the default action of the eve

# provide other arguments for an event-handling 
the method HandleClick takes a MouseEventArgs parameter in the same way as an ordinary click event handler, but it also accepts a string parameter. 
The method processes the click event as before, but also displays the message if the user presses the Ctrl key. 
The lambda expression calls the HandleCLick method, passing in the MouseEventArgs parameter (mouseEvent), and a string.
```csharp
@page "/counter"
@inject IJSRuntime JS

<h1>Counter</h1>

<p id="currentCount">Current count: @currentCount</p>

<button class="btn btn-primary" @onclick='mouseEvent => HandleClick(mouseEvent, "Hello")'>Click me</button>

@code {
    private int currentCount = 0;

    private async Task HandleClick(MouseEventArgs e, string msg)
    {
        if (e.CtrlKey) // Ctrl key pressed as well
        {
            await JS.InvokeVoidAsync("alert", msg);
            currentCount += 5;
        }
        else
        {
            currentCount++;
        }
    }
}
```
- This example uses the JavaScript alert function to display the message because there's no equivalent function in Blazor. 

# Write inline event handlers
C# supports lambda expressions. 
A lambda expression enables you to create an anonymous function. 
A lambda expression is useful if you have a simple event handler that you don't need to reuse elsewhere in a page or component.
```csharp
@page "/counter"
<h1>Counter</h1>
<p>Current count: @currentCount</p>

<button class="btn btn-primary" @onclick="() => currentCount++">Click me</button>

@code {
    private int currentCount = 0;
}
```
# Use an event to set the focus to a DOM element
The simplest way to perform this task is to use the FocusAsync method. This method is an instance method of an ElementReference object. 
The ElementReference should reference the item to which you want to set the focus. 
You designate an element reference with the @ref attribute and create a C# object with the same name in your code.
- In the following example, the @onclick event handler for the <button> element sets the focus on the <input> element.
```html
<button class="btn btn-primary" @onclick="ChangeFocus">Click me to change focus</button>
<input @ref=InputField @onfocus="HandleFocus" value="@data"/>
```
```csharp
@code {
    private ElementReference InputField;
    private string data;

    private async Task ChangeFocus()
    {
        await InputField.FocusAsync();
    }

    private async Task HandleFocus()
    {
        data = "Received focus";
    }
```
# Handle events asynchronously
By default, Blazor event handlers are synchronous. If an event handler performs a potentially long-running operation, such as calling a web service, the thread on which the event handler runs is blocked until the operation completes. This situation can lead to poor response in the user interface
# use async in methods
- To combat this problem, you can designate an event handler method as asynchronous. Use the C# async keyword. The method must return a Task object. You can then use the await operator inside the event handler method to initiate any long-running tasks on a separate thread and free the current thread for other work.
```csharp
<button @onclick="DoWork">Run time-consuming operation</button>

@code {
    private async Task DoWork()
    {
        // Call a method that takes a long time to run and free the current thread
        var data = await timeConsumingOperation();

        // Omitted for brevity
    }
}
```
# MouseEventArgs
You don't need to provide this parameter when you call the method; the Blazor runtime adds it automatically. You can query this parameter in the event handler. The following code increments the counter shown in the previous example by five if the user presses the Ctrl key at the same time as clicking the button:
```csharp
@page "/counter"
<h1>Counter</h1>
<p>Current count: @currentCount</p>
<button class="btn btn-primary" @onclick="IncrementCount">Click me</button>

@code {
    private int currentCount = 0;
    private void IncrementCount(MouseEventArgs e)
    {
        if (e.CtrlKey) // Ctrl key pressed as well
        {
            currentCount += 5;
        }
        else
        {
            currentCount++;
        }
    }
}
```
- ther events provide different EventArgs parameters. 
For instance, the @onkeypress event passes a KeyboardEventArgs parameter that indicates which key the user pressed. 
For any of the DOM events, if you don't need this information, you can omit the EventArgs parameter from the event handling method.

# Blazor event handlers
Most HTML elements expose events that are triggered when something significant happens. Such as, when a page finishes loading, the user clicks a button, or the contents of an HTML element are changed. An app can handle an event in several ways:

The app can ignore the event.
The app can run an event handler written in JavaScript to process the event.
The app can run a Blazor event handler written in C# to process the event.
# Handle an event with Blazor and C#

# App.razor component
If you want to apply a default layout to every component in all folders of your web app, you can do so in the App.razor component, where you configure the Router component, as you learned in unit 2. In the <RouteView> tag, use the DefaultLayout attribute.
<Router AppAssembly="@typeof(Program).Assembly">
    <Found Context="routeData">
        <RouteView RouteData="@routeData" DefaultLayout="@typeof(BlazingPizzasMainLayout)" />
    </Found>
    <NotFound>
        <p>Sorry, there's nothing at this address.</p>
    </NotFound>
</Router>
- Components that have a layout specified in their own @layout directive, or in an _Imports.razor file, override this default layout setting.

# template overall with _Imports.razor file
If you want to apply a template to all the Blazor components in a folder, you can use the _Imports.razor file as a shortcut.
When the Blazor compiler finds this file, it includes its directives in all the components in the folder automatically. This technique removes the need to add the @layout directive to every component and applies to components in the same folder as the _Imports.razor file and all its subfolders.
- Don't add a @layout directive to the _Imports.razor file in the root folder of your project because it results in an infinite loop of layouts.
# Use a layout in a Blazor component
To use a layout from another component, add the @layout directive with the name of the layout to apply. 
The component's HTML is rendered in the position of the @Body directive
```csharp
@page "/FavoritePizzas/{favorite}"
@layout BlazingPizzasMainLayout

<h1>Choose a Pizza</h1>

<p>Your favorite pizza is: @Favorite</p>

@code {
    [Parameter]
    public string Favorite { get; set; }
}
```
-- in the body will contain the html code of the razor page after it is rendered
# Code a Blazor layout
Two requirements are unique to Blazor layout components:

- You must inherit the LayoutComponentBase class. (@inherits LayoutComponentBase)
- You must include the @Body directive in the location where you want to render the content of the components that you're referencing. (@Body)
- The app's default layout is the Shared/MainLayout.razor component.
## example:
```csharp
@inherits LayoutComponentBase

<header>
    <h1>Blazing Pizza</h1>
</header>

<nav>
    <a href="Pizzas">Browse Pizzas</a>
    <a href="Toppings">Browse Extra Toppings</a>
    <a href="FavoritePizzas">Tell us your favorite</a>
    <a href="Orders">Track Your Order</a>
</nav>

@Body

<footer>
    @new MarkdownString(TrademarkMessage)
</footer>

@code {
    public string TrademarkMessage { get; set; } = "All content is &copy; Blazing Pizzas 2021";
}
```
- Layout components don't include a @page directive because they don't handle requests directly and shouldn't have a route created for them. 

# What are Blazor layouts?
A layout component in Blazor is one that shares its rendered markup with all the components that reference it. You place common UI elements like navigation menus, branding, and footers on the layout. Then you reference that layout from multiple other components. When the page is rendered, unique elements such as the details of the requested pizza, come from the referencing component. But, common elements come from the layout. You only have to code the common UI elements once, in the layout. Then if you need to rebrand the site, or make some other change, you only have to correct the layout. The change automatically applies to all the referencing components.
# Build reusable Blazor components using layouts
Suppose you're working in the pizza delivery company's website and you created the content for most of the main pages as a set of Blazor components. You want to ensure that these pages have the same branding, navigation menus, and footer section. However, you don't want to have to copy and paste that code into multiple files.
# Set a catch-all route parameter
Now suppose the user tries to specify two favorites by requesting the URI http://www.contoso.com/favoritepizza/margherita/hawaiian.
The page displays the message Your favorite pizza is: margherita and ignores the subfolder hawaiian. 
You can change this behavior by using a catch-all route parameter, which captures paths across multiple URI folder boundaries (forward slashes). 
Prefix an asterisk (*) to the route parameter name to make the route parameter catch-all:
```csharp
@page "/FavoritePizza/{*favorites}"
...
```

# Route constraints
 the consequence of requesting the URI http://www.contoso.com/favoritepizza/2 is the nonsensical message "Your favorite pizza is: 2". 
 In other cases, type mismatches like that one might cause an exception and display an error to the user.
- Consider specifying a type for the route parameter:
```csharp
@page "/FavoritePizza/{preferredsize:int}"

<h1>Choose a Pizza</h1>

<p>Your favorite pizza size is: @FavoriteSize inches.</p>

@code {
    [Parameter]
    public int FavoriteSize { get; set; }
}
```
## other types in a constraint:
| Constraint	|Example	| Example matches|
|bool|	{vegan:bool}|	http://www.contoso.com/pizzas/true|
|datetime|	{birthdate:datetime}|	http://www.contoso.com/customers/1995-12-12|
|decimal|	{maxprice:decimal}|	http://www.contoso.com/pizzas/15.00|
|double|	{weight:double}|	http://www.contoso.com/pizzas/1.234|
|float|	{weight:float}|	http://www.contoso.com/pizzas/1.564|
|guid|	{pizza id:guid}|	http://www.contoso.com/pizzas/CD2C1638-1638-72D5-1638-DEADBEEF1638|
|long|	{totals ales:long}|	http://www.contoso.com/pizzas/568192454|
# Optional route parameters
the {favorite} parameter is required. To make the route parameter optional, use a question mark:
- It's a good idea to set a default value for the optional parameter. 
```csharp
... 
@code {
    [Parameter]
    public string Favorite { get; set; }
    
    protected override void OnInitialized()
    {
        Favorite ??= "Fiorentina";
    }
}
```
- The OnInitialized method runs when users request the page for the first time. 
# Route parameters
You often want to use other parts of the URI as a value in your rendered page. For example, suppose the user requests:

http://www.contoso.com/favoritepizza/hawaiian

By using the @page directive, you saw how to route this request to, for example, the FavoritePizza.razor component. Now you want to make use of the value hawaiian in your component. To obtain this value, you can declare it as a route parameter.

Use the @page directive to specify the parts of the URI that are passed to the component as route parameters. In your component's code, you can obtain the value of a route parameter in the same way as you would obtain the value of a component parameter:

```csharp
@page "/FavoritePizzas/{favorite}"

<h1>Choose a Pizza</h1>

<p>Your favorite pizza is: @Favorite</p>

@code {
    [Parameter]
    public string Favorite { get; set; }
}
```
- Component parameters are values sent from a parent component to a child component. In the parent, you specify the component parameter value as an attribute of the child component's tag. Route parameters are specified differently. 
# Use NavLink components
In Blazor, use the NavLink component to render <a> tags because it toggles an active CSS class when the link's href attribute matches the current URL.
By styling the active class, you can make it clear to the user which navigation link is for the current page.

When you use NavLink, the home page link example looks like the following code:
```csharp
@page "/pizzas"
@inject NavigationManager NavManager

<h1>Buy a Pizza</h1>

<p>I want to order a: @PizzaName</p>

<NavLink href=@HomePageURI Match="NavLinkMatch.All">Home Page</NavLink>

@code {
    [Parameter]
    public string PizzaName { get; set; }
    
    public string HomePageURI { get; set; }
    
    protected override void OnInitialized()
    {
        HomePageURI = NavManager.BaseUri;
    }
}
```
The Match attribute in the NavLink component is used to manage when the link is highlighted. There are two options:

- NavLinkMatch.All: When you use this value, the link is only highlighted as the active link when its href matches the entire current URL.
- NavLinkMatch.Prefix: When you use this value, the link is highlighted as active when its href matches the first part of the current URL. Suppose, for example, that you had the link <NavLink href="pizzas" Match="NavLinkMatch.Prefix">. This link would be highlighted as active when the current URL was http://www.contoso.com/pizzas and for any location within that URL, such as http://www.contoso.com/pizzas/formaggio. This behavior can help the user understand which section of the website they're currently viewing.

# navigation information
You can use a NavigationManager object to obtain all these values.
you might need access to navigation information like:

The current full URI, such as http://www.contoso.com/pizzas/margherita?extratopping=pineapple.
The base URI, such as http://www.contoso.com/.
The base relative path, such as pizzas/margherita.
The query string, such as ?extratopping=pineapple.
- inject the object into the component and then you can access its properties. This code uses the NavigationManager object to obtain the website's base URI and then uses it to set a link to the home page:
```csharp
@page "/pizzas"
@inject NavigationManager NavManager

<h1>Buy a Pizza</h1>

<p>I want to order a: @PizzaName</p>

<a href=@HomePageURI>Home Page</a>

@code {
    [Parameter]
    public string PizzaName { get; set; }
    
    public string HomePageURI { get; set; }
    
    protected override void OnInitialized()
    {
        HomePageURI = NavManager.BaseUri;
    }
}
```
To access the query string, you must parse the full URI. To execute this parse, use the QueryHelpers class from the Microsoft.AspNetCore.WebUtilities assembly:
```csharp
@page "/pizzas"
@using Microsoft.AspNetCore.WebUtilities
@inject NavigationManager NavManager

<h1>Buy a Pizza</h1>

<p>I want to order a: @PizzaName</p>

<p>I want to add this topping: @ToppingName</p>

@code {
    [Parameter]
    public string PizzaName { get; set; }
    
    private string ToppingName { get; set; }
    
    protected override void OnInitialized()
    {
        var uri = NavManager.ToAbsoluteUri(NavManager.Uri);
        if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("extratopping", out var extraTopping))
        {
            ToppingName = System.Convert.ToString(extraTopping);
        }
    }
}
```
With the preceding component deployed, if a user requested the URI http://www.contoso.com/pizzas?extratopping=Pineapple, they would see the message "I want to add this topping: Pineapple" in the rendered page.

You can also use the NavigationManager object to send your users to another component in code by calling the NavigationManager.NavigateTo() method:

```csharp
@page "/pizzas/{pizzaname}"
@inject NavigationManager NavManager

<h1>Buy a Pizza</h1>

<p>I want to order a: @PizzaName</p>

<button class="btn" @onclick="NavigateToPaymentPage">
    Buy this pizza!
</button>

@code {
    [Parameter]
    public string PizzaName { get; set; }
    
    private void NavigateToPaymentPage()
    {
        NavManager.NavigateTo("buypizza");
    }
}
```


# Using the @page directive
In a Blazor component, the @page directive specifies that the component should handle requests directly. You can specify a RouteAttribute in the @page directive by passing it as a string. For example, this attribute specifies that the page handles requests to the /Pizzas route:
- @page "/Pizzas"
to specify more than one route to the component, use two or more @page directives, like in this example:
@page "/Pizzas"
@page "/CustomPizzas"


# Using route templates
When the user makes a request for a page from your web app, they can specify what they want to see with information in the URI. For example:

http://www.contoso.com/pizzas/margherita?extratopping=pineapple
# binding controls
Blazor allows you to bind HTML controls to C# properties to update when their values change.
- Customers should see what pizzas they're ordering and how the size they choose affects the price.
# Format bound values
If you display dates to the user, you might want to use a localized data format. For example, suppose you write a page specifically for UK users, who prefer to write dates with the day first. You can use the @bind:format directive to specify a single date format string:
```csharp
@page "/ukbirthdaypizza"

<h1>Order a pizza for your birthday!</h1>

<p>
    Enter your birth date:
    <input @bind="birthdate" @bind:format="dd-MM-yyyy" />
</p>

@code {
    private DateTime birthdate { get; set; } = new(2000, 1, 1);
}
```
## alternative to using the @bind:format
write C# code to format a bound value. Use the get and set accessors in the member definition, as in this example:
-----------
```csharp
@page "/pizzaapproval"
@using System.Globalization

<h1>Pizza: @PizzaName</h1>

<p>Approval rating: @approvalRating</p>

<p>
    <label>
        Set a new approval rating:
        <input @bind="ApprovalRating" />
    </label>
</p>

@code {
    private decimal approvalRating = 1.0;
    private NumberStyles style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign;
    private CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
    
    private string ApprovalRating
    {
        get => approvalRating.ToString("0.000", culture);
        set
        {
            if (Decimal.TryParse(value, style, culture, out var number))
            {
                approvalRating = Math.Round(number, 3);
            }
        }
    }
}
```
-----------
# Bind elements to specific events 
The @bind directive is smart and understands the controls it uses. For example, when you bind a value to a textbox <input>, it binds the value attribute. An HTML checkbox <input> has a checked attribute instead of a value attribute. The @bind attribute automatically uses this checked attribute instead. By default, the control is bound to the DOM onchange event. For example, consider this page
## @bind
```csharp
@page "/"

<h1>My favorite pizza is: @favPizza</h1>

<p>
    Enter your favorite pizza:
    <input @bind="favPizza" />
</p>

@code {
    private string favPizza { get; set; } = "Margherita"
}
```
When the page is rendered, the default value Margherita is displayed in both the <h1> element and the textbox. When you enter a new favorite pizza in the textbox, the <h1> element doesn't change until you tab out of the textbox or select Enter because that's when the onchange DOM event fires.

## the @bind-value and @bind-value:event
Often, that's the behavior you want. But suppose you want the <h1> element to update as soon as you enter any character in the textbox. 
You can achieve this outcome by binding to the oninput DOM event instead. 
To bind to this event, you must use the @bind-value and @bind-value:event directives:
```csharp
@page "/"

<h1>My favorite pizza is: @favPizza</h1>

<p>
    Enter your favorite pizza:
    <input @bind-value="favPizza" @bind-value:event="oninput" />
</p>

@code {
    private string favPizza { get; set; } = "Margherita"
}
```
- In this case, the title changes as soon as you type any character in the textbox.


# What is data binding?
If you want an HTML element to display a value, you can write code to alter the display. You need to write extra code to update the display when the value changes. In Blazor, you can use data binding to connect an HTML element to a field, property, or expression. This way, when the value changes, the HTML element is automatically updated. The update usually happens quickly after the change, and you don't have to write any update code.

To bind a control, you would use the @bind directive:
```csharp
@page "/"

<p>
    Your email address is:
    <input @bind="customerEmail" />
</p>

@code {
    private string customerEmail = "user@contoso.com"
}
```
- In the preceding page, whenever the customerEmail variable changes its value, the <input> value updates.

# Share data in Blazor applications
Blazor includes several ways to share information between components. You can use component parameters or cascading parameters to send values from a parent component to a child component. The AppState pattern is another approach you can use to store values and access them from any component in the application.

## three techniscs to share data between components
# Sharing information with other components by using component parameters
In a Blazor web app, each component renders a portion of HTML. Some components render a complete page but others render smaller fragments of markup, such as a table, a form, or a single control. If your component renders only a section of markup, you must use it as a child component within a parent component. Your child component can also be a parent to other smaller components that render within it. Child components are also known as nested components.

In this hierarchy of parent and child components, you can share information between them by using component parameters.
 Define these parameters on child components, and then set their values in the parent. For example, if you have a child component that displays pizza photos, you could use a component parameter to pass the pizza ID. The child component looks up the pizza from the ID and obtains pictures and other data. If you want to display many different pizzas, you can use this child component multiple times on the same parent page, passing a different ID to each child.

# Contributing

This project welcomes contributions and suggestions.  Most contributions require you to agree to a
Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit https://cla.opensource.microsoft.com.

When you submit a pull request, a CLA bot will automatically determine whether you need to provide
a CLA and decorate the PR appropriately (e.g., status check, comment). Simply follow the instructions
provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

# Legal Notices

Microsoft and any contributors grant you a license to the Microsoft documentation and other content
in this repository under the [Creative Commons Attribution 4.0 International Public License](https://creativecommons.org/licenses/by/4.0/legalcode),
see the [LICENSE](LICENSE) file, and grant you a license to any code in the repository under the [MIT License](https://opensource.org/licenses/MIT), see the
[LICENSE-CODE](LICENSE-CODE) file.

Microsoft, Windows, Microsoft Azure and/or other Microsoft products and services referenced in the documentation
may be either trademarks or registered trademarks of Microsoft in the United States and/or other countries.
The licenses for this project do not grant you rights to use any Microsoft names, logos, or trademarks.
Microsoft's general trademark guidelines can be found at http://go.microsoft.com/fwlink/?LinkID=254653.

Privacy information can be found at https://privacy.microsoft.com/en-us/

Microsoft and any contributors reserve all other rights, whether under their respective copyrights, patents,
or trademarks, whether by implication, estoppel or otherwise.
