angular.module('kasperskyApp').service('bookService', bookService);
bookService.$inject = ['$http'];

function bookService($http) {
    function get(sortByYear, sortByDesc) {
        if (!sortByDesc)
            sortByDesc = false;
        var url = '/api/book/?sortByDesc=' + sortByDesc;
        if (sortByYear)
            url += '&sortByYear=true';
        return $http.get(url)
            .then(function (data) {
                return data.data;
            });
    }

    function getItem(id) {
        return $http.get('/api/book/' + id)
            .then(function (data) { return data.data; });
    }

    function saveItem(currentItem, files) {
        var fd = new FormData();
        fd.append('file', files[0]);
        for (var prop in currentItem) {
            if (currentItem.hasOwnProperty(prop)) {
                fd.append(prop, JSON.stringify(currentItem[prop]));
            }
        }
        return $http.post('/api/book/',
                fd,
                {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                })
            .then(function() { });
    }

    function deleteItem(id) {
        return $http.delete('/api/book/' + id)
            .then(function (data) { });
    }
    return {
        get: get,
        getItem: getItem,
        update: saveItem,
        remove: deleteItem
    };
}