angular.module('ng-polymer-elements-example', ['ng-polymer-elements'])

function bootstrap() {
    angular.bootstrap(wrap(document), ['ng-polymer-elements-example']);
}

if (angular.isDefined(document.body.attributes['unresolved'])) {
    var readyListener = function () {
        bootstrap();
        window.removeEventListener('polymer-ready', readyListener);
    }
    window.addEventListener('polymer-ready', readyListener);
} else {
    bootstrap();
}