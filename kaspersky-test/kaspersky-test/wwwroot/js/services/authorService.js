angular.module('kasperskyApp').service('authorService', authorService);
publishingHouseService.$inject = ['$http'];

function authorService($http) {
    function get() {
        return $http.get('/api/author')
            .then(function (data) {
                 return data.data;
            });
    }

    function getItem(id) {
        return $http.get('/api/author/' + id)
            .then(function (data) { return data.data; });
    }

    function saveItem(currentItem) {
        return $http.post('/api/author/', currentItem);
    }

    function deleteItem(id) {
        return $http.delete('/api/author/' + id);
    }
    return {
        get: get,
        getItem: getItem,
        update: saveItem,
        remove: deleteItem
    };
}