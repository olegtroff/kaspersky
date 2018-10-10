(function () {
    angular.module("kasperskyApp").controller("editAuthorController", editAuthorController);

    editAuthorController.$inject = ['authorService', 'currentItem', '$uibModalInstance', '$scope'];

    function editAuthorController(authorService, currentItem, $uibModalInstance, $scope) {
        var vm = this;
        vm.currentItem = currentItem;
        vm.serverError = false;
        vm.serverMessage = "";

        if (vm.currentItem === "")
            vm.currentItem = {};

        vm.saveData = function () {
            if ($scope.editAuthorForm.$valid)
                authorService.update(vm.currentItem).then(function(result) {
                    $uibModalInstance.close();
                }, function(error) {
                    vm.serverError = true;
                    vm.serverMessage = JSON.stringify(error.data);
                });
        };
        vm.cancel = function () {
            $uibModalInstance.close();
        };
    }
})();