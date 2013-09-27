namespace NancyApplication1
{
    using Nancy;

    public class IndexModule : NancyModule
    {
        // This is injected automatically because only one implementation of this interface is present
        private IDummyService service;

        public IndexModule(IDummyService service)
        {
            Before += ctx =>
                {
                    //return Response.AsRedirect("products/list");
                    //return "Before";
                    return null; // this means "no action"
                };

            Get["/"] = _ => { return View["index"]; }; //"Hello world";

            // After does not return value
            After += ctx =>
                {
                    // Modify the Response
                };
        }
    }
}