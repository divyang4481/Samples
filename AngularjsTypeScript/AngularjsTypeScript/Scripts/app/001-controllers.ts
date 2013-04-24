/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/angularjs/angular.d.ts" />

module ParticleList
{
    export class Particle
    {
        constructor(public name: string, public symbol: string) {
        }
    }

    export interface Scope {
        title: string;
        particles: Particle[];
    }

    export class ParticleCtrl {
        constructor($scope: Scope) {
            $scope.particles = [
                new Particle("mion", "μ"),
                new Particle("taon", "τ"),
                new Particle("electron", "e")
            ];

            $scope.title = "Leptons";
        }
    }
}