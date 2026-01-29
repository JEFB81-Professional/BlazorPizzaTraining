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
