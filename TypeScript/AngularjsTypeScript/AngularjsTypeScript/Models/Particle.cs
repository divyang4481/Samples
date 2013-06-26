using System;

namespace AngularjsTypeScript.Models
{
    [Serializable]
    public class Particle
    {
        public Particle(string name, char symbol, double massMeV)
        {
            Name = name;
            Symbol = symbol;
            MassMeV = massMeV;
        }

        public string Name { get; set; }
        public double MassMeV { get; set; }
        public char Symbol { get; set; }
        public char SubSymbol { get; set; }
        public char SupSymbol { get; set; }
    }
}