(function () {
    angular.module("kasperskyApp").controller("editPublishingHouseController", editPublishingHouseController);

    editPublishingHouseController.$inject = ['publishingHouseService', 'currentItem', '$uibModalInstance', '$scope'];

    function editPublishingHouseController(publishingHouseService, currentItem, $uibModalInstance, $scope) {
        var vm = this;
        vm.currentItem = currentItem;
        vm.serverError = false;
        vm.serverMessage = "";
        if (vm.currentItem === "")
            vm.currentItem = {};

        vm.saveData = function () {
            if ($scope.editPublishingHouseForm.$valid)
                publishingHouseService.update(vm.currentItem).then(function (result) {
                    $uibModalInstance.close();
                }, function (error) {
                    vm.serverError = true;
                    vm.serverMessage = JSON.stringify(error.data);
                });
        };
        vm.cancel = function () {
            $uibModalInstance.close();
        };
    }
})();