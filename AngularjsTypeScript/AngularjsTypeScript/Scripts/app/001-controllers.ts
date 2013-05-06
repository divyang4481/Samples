/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/angularjs/angular.d.ts" />

module ParticleList
{
    export class Particle
    {
        constructor(public name: string, public symbol: string, public massMeV: number,
             public subsymbol: string = "", public supsymbol: string = "") {
        }
    }

    export interface Scope {
        title: string;
        particles: Particle[];
    }

    export class ParticleCtrl {
        constructor($scope: Scope) {
            $scope.particles = [
                new Particle("muon", "μ", 105.7, "", "-"),
                new Particle("taon", "τ", 1777, "", "-"),
                new Particle("electron", "e", 0.511, "", "-"),
                new Particle("neutrino (electron)", "ν", 0, "e"),
                new Particle("neutrino (muon)", "ν", 0, "μ"),
                new Particle("neutrino (taon)", "ν", 0, "τ")
            ];

            $scope.title = "Leptons";
        }
    }
}