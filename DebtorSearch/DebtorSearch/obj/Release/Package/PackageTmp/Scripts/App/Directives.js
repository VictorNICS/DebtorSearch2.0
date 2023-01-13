
var Directive = function ()
{
        return {
            require: 'ngModel',
            link: function (scope, element, attrs, modelCtrl) {
                var capitalize = function (inputValue) {
                    if (inputValue == undefined) inputValue = '';
                    var capitalized = inputValue.toUpperCase();
                    if (capitalized !== inputValue) {
                        // var selection = element[0].selectionStart;
                        modelCtrl.$setViewValue(capitalized);
                        modelCtrl.$render();

                        //  element[0].selectionStart = selection;
                        //  element[0].selectionEnd = selection;
                    }
                    return capitalized;
                }
                modelCtrl.$parsers.push(capitalize);
                capitalize(scope[attrs.ngModel]); // capitalize initial value
            }
        };
};

var DatePicker = function () {
    return {
        require: 'ngModel',
        restrict: "A",
        link: function (scope, el, attr) {
            el.datepicker({
                dateFormat: 'yy-mm-dd'
            });
        }
    };
};

