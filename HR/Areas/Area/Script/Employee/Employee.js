

var app = angular.module("Hr",[]);
app.controller("Employee", function ($scope,$http,$compile,$filter) {

    $scope.Employee = {};

    $scope.Nationalities = JSON.parse('@Html.Raw(Json.Encode(ViewBag.Nationality))');
    $scope.Jobs = JSON.parse('@Html.Raw(Json.Encode(ViewBag.Jobs))');
    $scope.Maritalstatus = JSON.parse('@Html.Raw(Json.Encode(ViewBag.Maritalstatus))');
    $scope.Religion = JSON.parse('@Html.Raw(Json.Encode(ViewBag.Religion))');
    $scope.ServiceState = JSON.parse('@Html.Raw(Json.Encode(ViewBag.ServiceState))');
    $scope.Gender = JSON.parse('@Html.Raw(Json.Encode(ViewBag.Gender))');
    $scope.MilitaryService = JSON.parse('@Html.Raw(Json.Encode(ViewBag.MilitaryService))');

    $scope.$watch(function () {
        $('.selectpicker').selectpicker('refresh');
    });

    $scope.save = function () {
        $scope.Employee.HireDate = new Date($filter('date')($scope.Employee.HireDate, 'MM/dd/yyyy'));
        $scope.Employee.ActualHireDate = new Date($filter('date')($scope.Employee.ActualHireDate, 'MM/dd/yyyy'));
        $scope.Employee.BithDate = new Date($filter('date')($scope.Employee.BithDate, 'MM/dd/yyyy'));

        $http.post("/Employee/Save", $scope.Employee).then(function (response) {
            alert(response.data.res);
            $scope.$apply();
        });
    }

    $scope.showup = function () {
        $.get("/Area/Employee/EmpSearch", function (data) {
            $("#myModal").empty();
            var $el = $("#myModal").html(data);
            $compile($el)($scope);    
            $("#myModal").modal("show");
            $scope.SearchEmps = JSON.parse('@Html.Raw(Json.Encode(ViewBag.Emps))');
        });
    }


    $scope.new = function () {
        $scope.Employee = {};
    }

    $scope.searchchange = function () {
        $.get("/Area/Employee/getEmployee?ID=" + $scope.SearchID, function (response) {
                
            $scope.Employee = response.Empp;
            $scope.Employee.HireDate =new Date( $filter('date')(response.HireDate, 'dd/MM/yyyy'));
            $scope.Employee.ActualHireDate = new Date($filter('date')(response.ActualHireDate, 'dd/MM/yyyy'));
            $scope.Employee.BithDate = new Date($filter('date')(response.BithDate, 'dd/MM/yyyy'));
            $scope.$apply();
        });
    }

    $scope.searchByCode = function () {
        $.get("/Area/Employee/getEmpByCode?Code="+$scope.Employee.Code, function (response) {
            $scope.Employee = response;
            $scope.Employee.HireDate = new Date($filter('date')(response.HireDate, "dd/MM/yyyy"));
            $scope.Employee.ActualHireDate = new Date($filter('date')(response.ActualHireDate, "dd/MM/yyyy"));
            $scope.Employee.BithDate = new Date($filter('date')(response.BithDate, "dd/MM/yyyy"));
            $scope.$apply();
        });
    }

        
});

