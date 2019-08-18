# AdaptiveClient.Utilities

Utilities for using [AdaptiveClient](https://github.com/leaderanalytics/AdaptiveClient).

## Classes

### `ServiceManifestFactory`

AdaptiveClient supports the concept of a Service Manifest.  A Service Manifest  is basically a collection of services that can be injected as a single object into your ViewModel, Controller, etc.  A Service Manifest is defined as follows:

* Create an Interface:
````csharp
public interface ISFServiceManifest : IDisposable
{
    // Services defined by StoreFront API:
    IOrdersService OrdersService { get; }
    IProductsService ProductsService { get; }
    // more services here...
}
````
* Create an implementation that derives from ServiceMaifestFactory and implements your interface.  Call `Create<T>` to allow AdaptiveClient to lazily resolve your service when called:

````csharp
public class SFServiceManifest : ServiceManifestFactory, ISFServiceManifest
{
    // Services defined by StoreFront API:
    public IOrdersService OrdersService { get => Create<IOrdersService>(); }
    public IProductsService ProductsService { get => Create<IProductsService>(); }
    // more services here...
}
```` 
* Inject `IAdaptiveClient<ISFServiceManifest>` into your Controller or ViewModel:

````csharp
public class OrdersModel : BasePageModel
{
    private IAdaptiveClient<ISFServiceManifest> serviceClient;

    public OrdersModel(IAdaptiveClient<ISFServiceManifest> serviceClient)
    {
        this.serviceClient = serviceClient;
    }

    private async Task GetOrders()
    {
        // All services defined on ISFServiceManifest are available here:
        Orders = await serviceClient.CallAsync(x => x.OrdersService.GetOrders());
        Products = await serviceClient.CallAsync(x => x.ProductsService.GetProducts());
        // etc...
    }
}
````
---

### `Http_EndPointValidator`

Implements `IEndPointValidator`.  Makes a call to a HTTP server and returns true if the call is successful.

---

### `MySQL_EndPointValidator`

Implements `IEndPointValidator`.  Attempts to open a connection to a MySQL database server.  Returns true of the call is successful.

---

### `MSSQL_EndPointValidator`

Implements `IEndPointValidator`. Attempts to open a connection to a MSSQL database server.  Returns true of the call is successful.

---

## RegistrationHelper Extensions

* **RegisterServiceManifest**  - Keys a class derived from `ServiceManifestFactory` to a specific API name, provider, and end point type.
