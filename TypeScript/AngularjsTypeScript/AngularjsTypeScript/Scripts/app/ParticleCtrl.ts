/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/angularjs/angular.d.ts" />

module ParticleApp {
    // Model
    export class Particle {
        constructor(public name: string, public symbol: string, public massMeV: number,
             public subsymbol: string = "", public supsymbol: string = "") {
        }
    }

    export interface Scope {
        title: string;
        particles: Particle[];
        orderProp: string;
    }

    // Controller
    export class ParticleCtrl {
        constructor($scope: Scope, $http: any) {

            //$scope.particles = [
            //    new Particle("muon", "μ", 105.7, "", "-"),
            //    new Particle("taon", "τ", 1777, "", "-"),
            //    new Particle("electron", "e", 0.511, "", "-"),
            //    new Particle("neutrino (electron)", "ν", 0, "e"),
            //    new Particle("neutrino (muon)", "ν", 0, "μ"),
            //    new Particle("neutrino (taon)", "ν", 0, "τ")];

            $http.get('Particle/ListLeptons').success(function (data) {
                $scope.particles = data;
            });

            $scope.title = "Leptons";
            $scope.orderProp = "name";
        }
    }
}