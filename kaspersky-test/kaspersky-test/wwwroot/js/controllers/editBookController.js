(function () {
    angular.module("kasperskyApp").controller("editBookController", editBookController);

    editBookController.$inject = ['bookService', 'currentItem', '$uibModalInstance', '$scope'];

    function editBookController(bookService, currentItem, $uibModalInstance, $scope) {
        var vm = this;
        vm.currentItem = currentItem;

        if (vm.currentItem === "")
            vm.currentItem = {};

        vm.saveData = function () {
            if ($scope.editBookForm.$valid)
                bookService.update(vm.currentItem.book, $("#bookImage")[0].files).then(function (result) {
                    $uibModalInstance.close();
                });
        };
        vm.cancel = function () {
            $uibModalInstance.close();
        };
    }
})();