(function () {
    angular.module("kasperskyApp").controller("authorController", authorController);

    authorController.$inject = ['authorService', '$uibModal'];

    function authorController(authorService, $uibModal) {
        var vm = this;
        vm.data = [];

        vm.init = function () {
            vm.getList();
        };

        vm.editItem = function (id) {
            authorService.getItem(id).then(function (result) {
                vm.currentItem = result;
                var modalInstance = $uibModal.open({
                    animation: true,
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: 'js/views/editAuthor.html',
                    controller: 'editAuthorController',
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
            authorService.remove(id).then(function (result) {
                vm.getList();
            });

        };
        vm.getList = function () {
            authorService.get().then(function (result) {
                vm.data = result;
            });
        };
    }
})();