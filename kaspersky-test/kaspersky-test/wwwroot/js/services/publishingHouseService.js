angular.module('kasperskyApp').service('publishingHouseService', publishingHouseService);
publishingHouseService.$inject = ['$http'];

function publishingHouseService($http) {
    function get() {
        return $http.get('/api/PublishingHouse')
            .then(function (data) { return data.data; });
    }

    function getItem(id) {
        return $http.get('/api/PublishingHouse/' + id)
            .then(function (data) { return data.data; });
    }

    function saveItem(currentItem) {
        return $http.post('/api/PublishingHouse/', currentItem);
    }

    function deleteItem(id) {
        return $http.delete('/api/PublishingHouse/' + id);
    }
    return {
        get: get,
        getItem: getItem,
        update: saveItem,
        remove: deleteItem
    };
}