(function () {

  var app= angular.module('app', []);
  app.controller('Controller', Controller);
    app.directive('uppercase', Directive);
    app.directive('datepicker', DatePicker);
  Controller.$inject = ['$scope', '$http'];  
})();
