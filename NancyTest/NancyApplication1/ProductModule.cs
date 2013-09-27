namespace NancyApplication1
{
    using Nancy;

    public class ProductModule : NancyModule
    {
        public ProductModule()
        {
            // "product/{Id}" is called "module path"
            Get["product/{Id}"] = parameters =>
                {
                    var model = new Product {Name = "NancyFx", Id = parameters.Id};
                    return View[model];
                };
        }
    }

    public class Product
    {
        public int Id { get; set; }  
        public string Name { get; set; }
    }
}