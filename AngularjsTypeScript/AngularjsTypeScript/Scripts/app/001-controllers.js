var ParticleList;
(function (ParticleList) {
    var Particle = (function () {
        function Particle(name, symbol, massMeV, subsymbol, supsymbol) {
            if (typeof subsymbol === "undefined") { subsymbol = ""; }
            if (typeof supsymbol === "undefined") { supsymbol = ""; }
            this.name = name;
            this.symbol = symbol;
            this.massMeV = massMeV;
            this.subsymbol = subsymbol;
            this.supsymbol = supsymbol;
        }
        return Particle;
    })();
    ParticleList.Particle = Particle;    
    var ParticleCtrl = (function () {
        function ParticleCtrl($scope) {
            $scope.particles = [
                new Particle("muon", "μ", 105.7, "", "-"), 
                new Particle("taon", "τ", 1777, "", "-"), 
                new Particle("electron", "e", 0.511, "", "-"), 
                new Particle("neutrino (electron)", "ν", 0, "e"), 
                new Particle("neutrino (muon)", "ν", 0, "μ"), 
                new Particle("neutrino (taon)", "ν", 0, "τ")
            ];
            $scope.title = "Leptons";
            $scope.orderProp = "name";
        }
        return ParticleCtrl;
    })();
    ParticleList.ParticleCtrl = ParticleCtrl;    
})(ParticleList || (ParticleList = {}));
