var ParticleList;
(function (ParticleList) {
    var Particle = (function () {
        function Particle(name, symbol) {
            this.name = name;
            this.symbol = symbol;
        }
        return Particle;
    })();
    ParticleList.Particle = Particle;    
    var ParticleCtrl = (function () {
        function ParticleCtrl($scope) {
            $scope.particles = [
                new Particle("mion", "μ"), 
                new Particle("taon", "τ"), 
                new Particle("electron", "e")
            ];
        }
        return ParticleCtrl;
    })();
    ParticleList.ParticleCtrl = ParticleCtrl;    
})(ParticleList || (ParticleList = {}));
