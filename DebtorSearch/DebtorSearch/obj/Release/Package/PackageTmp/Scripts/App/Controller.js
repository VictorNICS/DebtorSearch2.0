var Controller = function ($scope, $http) {
   
    var vm = $scope;
    vm.Auto = false;


    vm.ButtonClick = function (Data) {
        swal('Loading...')
        swal.showLoading()
        var valid = vm.isFormValid();
        if (valid === true)
        {
            var Report = {};
            Report.Report = Data.ReportType;
            Report.Client = Data.Client;
           
            var post = $http({
                method: "POST",
                url: "/Reports/Home",
                dataType: 'json',
                data: Report,
                headers: { "Content-Type": "application/json" }
            }).then(function successCallback(response) {

                swal({
                    title: 'Success',
                    text: "Notification Successfully Created!!",
                    type: 'success',
                    showCancelButton: false,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Ok!'
                }).then((result) => {
                    if (result.value) {
                        location.href = '/Search/Index';
                      
                    }
                })
            });
                
           
        }
    };
     vm.mandatoryIsValid = function () {

         var valid = angular.element(ReportForm).scope().isFormValid();

        return valid;

    }
    vm.mandatoryErrorMessage = "Please ensure that you complete the following mandatory fields before submitting:<br/><br/>";
    vm.isFormValid = function () {

        if (!vm.ReportForm.$valid) {
            var errorMessage = vm.mandatoryErrorMessage
            var counter = 0;
            var requiredElements = vm.ReportForm.$error.required;
            
            var n = 0;
            if (angular.isDefined(requiredElements)) {
                n = requiredElements.length;
            }
           
            var uniqueArr = [];
            for (var i = 0; i < n; i++) {


                var fieldName = requiredElements[i].$name;

                if (fieldName !== '' && fieldName !== undefined && fieldName !== null && uniqueArr.indexOf(fieldName) == -1) {
                    uniqueArr.push(fieldName);
                    counter++;
                    if (counter <= 10) {
                        errorMessage += "- " + uniqueArr[uniqueArr.length - 1] + "<br/>";
                    }
                }
            }
            if (counter > 10) {
                errorMessage += "<br/>Note that you have more than 10 fields that need to be completed. These fields will be displayed once the above mentioned fields have been corrected.";
            }

            swal('Error', errorMessage,'error');
            return false;
        }

        return true;
    }

}
  
