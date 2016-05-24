app = angular.module('BibliotekaApp');
app.controller("GalerijaCtrl", ["$scope", "BibliotekaService", '$sce', '$http', '$translate',
    function ($scope, BibliotekaService, $sce, $http, $translate) {

        var paths = [];
        var tmpPic = 0;
        var numOfPic = 0;

        $scope.downloadFilePaths = function () {

            var xhr = new XMLHttpRequest();
            xhr.onload = function () {
                if (xhr.status === 200) {

                    obj = JSON.parse(xhr.response);
                    for (i = 0; i < obj.entries.length; i++) {
                        paths.push(obj.entries[i].path_lower);
                    }
                    numOfPic = obj.entries.length - 1;//indeksiranje od 0
                    if (obj.entries.length > 0)
                        $scope.downloadFiles();
                }
                else {
                    var errorMessage = xhr.response || 'Unable to download file';
                    //alert(xhr.response);//TEST
                }
            };
            xhr.open('POST', 'https://api.dropboxapi.com/2/files/list_folder');
            xhr.setRequestHeader('Authorization', 'Bearer ' + '9xDxW8zeW8AAAAAAAAAAuUDfRaODVvcFgOS8A2S6FbpEiDcT412hPSm4yzsHzD6S');
            xhr.setRequestHeader("Content-type", 'application/json');
            var data = '{ "path": "/nwtslike", "recursive": false, "include_media_info": false, "include_deleted": false, "include_has_explicit_shared_members": false }';
            xhr.send(data);
        };
        $scope.downloadFilePaths();

        $scope.downloadFiles = function () {

            var xhr = new XMLHttpRequest();
            xhr.responseType = 'arraybuffer';

            xhr.onload = function () {
                if (xhr.status === 200) {

                    var blob = new Blob([xhr.response], { type: 'application/octet-stream' });
                    var urlCreator = window.URL || window.webkitURL;
                    var imageUrl = urlCreator.createObjectURL(blob);
                    document.getElementById("slikaId").src = imageUrl;
                    var naslov = paths[tmpPic].substring(10, paths[tmpPic].length - 4);
                    document.getElementById("picTitle").innerHTML = naslov.charAt(0).toUpperCase() + naslov.slice(1);
                }
                else {
                    var errorMessage = xhr.response || 'Unable to download file';
                    //alert(errorMessage); //Test
                    $scope.nextPic();
                }
            };

            xhr.open('POST', 'https://content.dropboxapi.com/2/files/download');
            xhr.setRequestHeader('Authorization', 'Bearer ' + '9xDxW8zeW8AAAAAAAAAAuUDfRaODVvcFgOS8A2S6FbpEiDcT412hPSm4yzsHzD6S');
            xhr.setRequestHeader('Dropbox-API-Arg', JSON.stringify({
                path: paths[tmpPic]
            }));
            xhr.send();

        };


        $scope.nextPic = function () {

            if (tmpPic == numOfPic)
                tmpPic = 0;
            else
                tmpPic++;

            $scope.downloadFiles();
        };
        $scope.previousPic = function () {

            if (tmpPic == 0)
                tmpPic = numOfPic;
            else
                tmpPic--;

            $scope.downloadFiles();
        };

    }]);