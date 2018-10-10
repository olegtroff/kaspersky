(function () {
    angular.module("kasperskyApp").controller("publishingHouseController", publishingHouseController);

    publishingHouseController.$inject = ['publishingHouseService', '$uibModal'];

    function publishingHouseController(publishingHouseService, $uibModal) {
        var vm = this;
        vm.data = [];

        vm.init = function () {
            vm.getList();
        };

        vm.editItem = function (id) {
            publishingHouseService.getItem(id).then(function (result) {
                vm.currentItem = result;
                var modalInstance = $uibModal.open({
                    animation: true,
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: 'js/views/editPublishingHouse.html',
                    controller: 'editPublishingHouseController',
                    controllerAs: '$ctrl',
                    resolve: {
                        currentItem: function () {
                            return result;
                        }
                    }
                });

                modalInstance.result.then(function (data) {
                    vm.getList();
                });
            });
        };
        vm.deleteItem = function (id) {
            publishingHouseService.remove(id).then(function (result) {
                vm.getList();
            });

        };
        vm.getList = function () {
            publishingHouseService.get().then(function (result) {
                vm.data = result;
            });
        };
    }
})();