(function () {
    'use strict';

    angular
        .module('app')
        .controller('main', main);

    main.$inject = ['$location'];

    function main($location) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'main';

        activate();

        function activate() { }
    }
})();
