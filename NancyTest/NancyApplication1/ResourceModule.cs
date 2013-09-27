namespace NancyApplication1
{
    using Nancy;

    public class ResourceModule : NancyModule
    {
        public ResourceModule() : base("/products")
        {
            Get["/list"] = _ => "The list of products";
            Get["/{category}"] = parameters => parameters.category;
        }
    }
}