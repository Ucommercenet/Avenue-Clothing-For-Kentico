var uc_definitions_Resource = function($http) {
    var serviceUrl = UCommerceClientMgr.BaseServiceUrl;

    return {
         getDefinitionGraph: function (id) {
            var url = serviceUrl + 'Definitions/Definitiongraph';
            if (id != null) url += '/' + id;
            return $http.get(url).then(function (response) {
                return response.data;
            });
        },

		saveDefinitionGraph: function(definitionGraph, id) {
            var url = serviceUrl + 'Definitions/UpdateDefinitionGraph';
		    if (id != null) url += '/' + id;
			$http.post(
        		url,
		        {
		        	definitionGraph: definitionGraph
		        },
		        {
		        	dataType: "application/json"
		        }
	        );
        }
    };
}