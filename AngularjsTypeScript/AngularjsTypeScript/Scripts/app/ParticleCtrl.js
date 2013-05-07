var ParticleApp;
(function (ParticleApp) {
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
    ParticleApp.Particle = Particle;    
    var ParticleCtrl = (function () {
        function ParticleCtrl($scope, $http) {
            $http.get('Particle/ListLeptons').success(function (data) {
                debugger;

                $scope.particles = data;
            });
            $scope.title = "Leptons";
            $scope.orderProp = "name";
        }
        return ParticleCtrl;
    })();
    ParticleApp.ParticleCtrl = ParticleCtrl;    
})(ParticleApp || (ParticleApp = {}));
