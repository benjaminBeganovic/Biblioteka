app = angular.module('BibliotekaApp');
app.controller("UploadCtrl", ["$scope", "BibliotekaService", '$sce', '$http', '$translate',
    function ($scope, BibliotekaService, $sce, $http, $translate) {


        var dropboxToken = "9xDxW8zeW8AAAAAAAAAAB6_TB11kJcC6yyruPljBt2x8kvvxnvNc8PKrbPLneLsA";

        var numberOfUploads = 0;
        var uploading = false;

        $scope.refresh = function () {
            if (uploading)
                return;
            document.getElementById("procenat").innerHTML = "";
            document.getElementById("slikeZaUpload").value = "";
            document.getElementById("uploadFile").value = "";
        };

        $scope.uploadImage = function () {
            if (uploading)
                return;
            var listOfFiles = document.getElementById("slikeZaUpload").files;
            var odabrano = "";
            for (i = 0; i < listOfFiles.length; i++) {
                odabrano = odabrano + listOfFiles[i].name + ",   ";
            }
            document.getElementById("uploadFile").value = odabrano.substring(0, odabrano.length - 4);
        };


        $scope.uploadFiles = function () {

            if (uploading)
                return;

            var pom = document.getElementById("slikeZaUpload").value;
            if (pom == null || pom == "") {
                document.getElementById("procenat").innerHTML = "No files selected!";
                return;
            }

            uploading = true;

            var listOfFiles = document.getElementById("slikeZaUpload").files;

            for (i = 0; i < listOfFiles.length; i++) {

                var file = listOfFiles[i];

                var xhr = new XMLHttpRequest();

                xhr.upload.onprogress = function (evt) {
                    var percentComplete = parseInt(100.0 * evt.loaded / evt.total);
                    var procenat = "" + percentComplete + "%";
                    document.getElementById("procenat").innerHTML = procenat;
                };

                xhr.onload = function () {
                    if (xhr.status === 200) {
                        var fileInfo = JSON.parse(xhr.response);

                        numberOfUploads++;

                        if (numberOfUploads == listOfFiles.length) {
                            document.getElementById("procenat").innerHTML = "Zavrseno!";
                            document.getElementById("slikeZaUpload").value = "";
                            numberOfUploads = 0;
                            uploading = false;
                            document.getElementById("uploadFile").value = "";
                            $scope.downloadFilesPaths();
                        }
                    }
                    else {
                        var errorMessage = xhr.response || 'Unable to upload file';

                        numberOfUploads++;

                        if (numberOfUploads == listOfFiles.length) {
                            document.getElementById("procenat").innerHTML = "Upload failed!";
                            document.getElementById("slikeZaUpload").value = "";
                            numberOfUploads = 0;
                            uploading = false;
                            document.getElementById("uploadFile").value = "";
                            $scope.downloadFilesPaths();
                        }
                    }
                };

                xhr.open('POST', 'https://content.dropboxapi.com/2/files/upload');
                xhr.setRequestHeader('Authorization', 'Bearer ' + dropboxToken);
                xhr.setRequestHeader('Content-Type', 'application/octet-stream');
                xhr.setRequestHeader('Dropbox-API-Arg', JSON.stringify({
                    path: '/nwtslike/' + file.name,
                    mode: 'add',
                    autorename: true,
                    mute: false
                }));
                xhr.send(file);
            }
        };

        //download files path
        $scope.downloadFilesPaths = function () {

            var xhr = new XMLHttpRequest();
            xhr.onload = function () {
                if (xhr.status === 200) {

                    obj = JSON.parse(xhr.response);
                    var titles = [];
                    for (i = 0; i < obj.entries.length; i++) {
                        titles.push(obj.entries[i].path_lower.substring(10, obj.entries[i].path_lower.length));
                    }

                    var myJsonString = JSON.parse(JSON.stringify(titles));
                    $scope.lista_titleova = myJsonString;
                    $scope.$apply();//da se renda :D
                }
                else {
                    var errorMessage = xhr.response || 'Unable to download file';
                    //alert(xhr.response);//TEST
                }
            };
            xhr.open('POST', 'https://api.dropboxapi.com/2/files/list_folder');
            xhr.setRequestHeader('Authorization', 'Bearer ' + dropboxToken);
            xhr.setRequestHeader("Content-type", 'application/json');
            var data = '{ "path": "/nwtslike", "recursive": false, "include_media_info": false, "include_deleted": false, "include_has_explicit_shared_members": false }';
            xhr.send(data);
        };
        $scope.downloadFilesPaths();

        $scope.deleteFun = function (title) {

            //document.getElementById(title).style.display = 'none';
            var xhr = new XMLHttpRequest();
            xhr.onload = function () {
                if (xhr.status === 200) {

                    $scope.downloadFilesPaths();
                }
                else {
                    var errorMessage = xhr.response || 'Unable to download file';
                    //alert(xhr.response);//TEST
                }
            };
            xhr.open('POST', 'https://api.dropboxapi.com/2/files/delete');
            xhr.setRequestHeader('Authorization', 'Bearer ' + dropboxToken);
            xhr.setRequestHeader("Content-type", 'application/json');
            var data = '{ "path": "/nwtslike/' + title + '" }';
            xhr.send(data);

        };

    }]);