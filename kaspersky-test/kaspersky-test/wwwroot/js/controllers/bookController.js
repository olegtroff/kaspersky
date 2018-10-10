(function () {
    angular.module("kasperskyApp").controller("bookController", bookController);

    bookController.$inject = ['bookService', '$uibModal', '$location'];

    function bookController(bookService, $uibModal, $location) {
        var vm = this;
        vm.data = [];
        vm.sortByDesc = false;
        vm.sortByYear = false;
        vm.location = $location;

        vm.init = function () {
            vm.sortByDesc = vm.location.$$search.sortByDesc !== undefined;
            vm.sortByYear = vm.location.$$search.sortByYear !== undefined;
            vm.getList();
        };

        vm.editItem = function (id) {
            bookService.getItem(id).then(function (result) {
                vm.currentItem = result;
                var modalInstance = $uibModal.open({
                    animation: true,
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: 'js/views/editBook.html',
                    controller: 'editBookController',
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
            bookService.remove(id).then(function (result) {
                vm.getList();
            });
        };
        vm.sortList = function (byYear) {
            vm.sortByYear = byYear;
            vm.sortByDesc = !vm.sortByDesc;

            var url = "/";
            if (vm.sortByYear)
                url += "?sortByYear=true";
            if (vm.sortByDesc) {
                if (url !== "/") url += "&";
                else url += "?";
                url += "sortByDesc=true";
            }
            vm.location.url(url);
        };
        vm.getList = function () {
            bookService.get(vm.sortByYear, vm.sortByDesc).then(function (result) {
                vm.data = result;
            });
        };
    }
})();