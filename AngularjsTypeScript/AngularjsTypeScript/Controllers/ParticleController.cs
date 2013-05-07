using AngularjsTypeScript.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngularjsTypeScript.Controllers
{
    public class ParticleController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        public ContentResult ListLeptons()
        {
            var leptons = new List<Particle> 
            { 
                new Particle("muon", 'μ', 105.7) { SupSymbol = '-' },
                new Particle("taon", 'τ', 1777) { SupSymbol = '-' },
                new Particle("electron", 'e', 0.511) { SupSymbol = '-' },
                new Particle("neutrino (electron)", 'ν', 0) { SubSymbol = 'e' },
                new Particle("neutrino (muon)", 'ν', 0) { SubSymbol = 'μ' },
                new Particle("neutrino (taon)", 'ν', 0) { SubSymbol = 'τ' }
            };


            // Serialization using Json.NET.
            // With Json.NET I have fine grained control over how the object is serialized.
            return Content(JsonConvert.SerializeObject(leptons, new JsonSerializerSettings 
            { 
                ContractResolver = new CamelCasePropertyNamesContractResolver(), 
                Formatting = Formatting.Indented, 
            }), "application/json");
        }
    }
}
