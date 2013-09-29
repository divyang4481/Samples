using System.Collections.Generic;

namespace NancyApplication1
{
    using Nancy;
    using Nancy.ModelBinding;

    public class CarModule : NancyModule
    {
        public CarModule()
        {
            Get["/{make}/{model}"] = parameters =>
                {
                    var carQuery = this.Bind<BrowseCarQuery>();

                    //var dynamicModel = this.Bind();
                    //this.BindTo(instance:)

                    var listOfCars = new List<Car>
                        {
                            new Car(1, carQuery.Make, carQuery.Model),
                            new Car(2, carQuery.Make, carQuery.Model),
                            new Car(3, carQuery.Make, carQuery.Model)
                        };

                    return listOfCars;
                };
        }
    }

    public class Car
    {
        public Car(int id, string make, string model)
        {
            Id = id;
            Make = make;
            Model = model;
        }

        public int Id { get; private set; }
        public string Make { get; private set; }
        public string Model { get; private set; }
    }

    public class BrowseCarQuery
    {
        public string Make { get; set; }
        public string Model { get; set; }
    }
}