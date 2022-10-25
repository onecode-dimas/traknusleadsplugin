var LeadAgit = LeadAgit || {};
(function() {
    this.FormOnLoad = function(){
        this.FilterOrderType();
    };
     
    this.FilterOrderType = (function () {
        var filtered;
        var filterFunction = function () {
         
			var filterDivision = "";
			var filterBranch = "";
			var divisionRef = Xrm.Page.getAttribute("agt_leadsdivision").getValue();
			if(divisionRef)
			{
				filterDivision = "<condition attribute='agt_division' operator='eq' value='" + divisionRef[0].id + "'/>";
			}
			
			var branchRef = Xrm.Page.getAttribute("agt_branch1").getValue();
			if(branchRef){
				filterBranch = "<condition attribute='new_branch' operator='eq' value='" + branchRef[0].id + "'/>";
			}
			
			
            var filterXml = [
                "<filter type='and'>",
                filterDivision, 
				filterBranch, 
                "</filter>"
            ];

			Xrm.Page.getAttribute("agt_assignto").controls.forEach(function (ctrl) { ctrl.addCustomFilter(filterXml.join("")); });
            Xrm.Page.getAttribute("agt_assignto2").controls.forEach(function (ctrl) { ctrl.addCustomFilter(filterXml.join("")); });
        };

        return function () {
            if (filtered) { return; }
			Xrm.Page.getAttribute("agt_assignto").controls.forEach(function (ctrl) { ctrl.addPreSearch(filterFunction); });
            Xrm.Page.getAttribute("agt_assignto2").controls.forEach(function (ctrl) { ctrl.addPreSearch(filterFunction); });
            filtered = true;
        };
    }());
    
}).apply(LeadAgit);