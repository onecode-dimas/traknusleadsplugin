function getQuickViewFormAttributeValues() {
	debugger;
	
try{
	var recordId = Xrm.Page.data.entity.getId();
	var formId = "86AAB7EF-9167-4340-853E-51F5C1FAB6FE"; // Leads form: Resolved Leads.
	var parameters = {
		etn: "agt_leads"
	};

	var queryString = ("?formid=" + encodeURIComponent(formId));
	queryString += ("&data=" + encodeURIComponent(JSON.stringify(parameters)));
	queryString += ("&id=" + encodeURIComponent(recordId));

	var webResource = "agt_WebPage.CustomControl.FormDialog";
	cachedShowModalDialog(webResource + queryString, "", "dialogWidth:600px;dialogHeight:400px;center:1;");
	
	/*
	var params = {};

	var thisAccount = {
		entityType: "agt_leads",
		id: Xrm.Page.data.entity.getId()
	};
	
	Xrm.Utility.openQuickCreate("agt_leads", thisAccount, params).then(function () { console.log("Success"); }, function (error) {
		console.log(error.message);
	});
	*/
 
 
	/*var thisAccount = {
		entityType: "agt_leads",
		id: Xrm.Page.data.entity.getId()
	};
	var callback = function (obj) {
		console.log("Created new " + obj.savedEntityReference.entityType + " named '" + obj.savedEntityReference.name + "' with id:" + obj.savedEntityReference.id);
	}
	var setName = { agt_resolvedate: "Child account of " + Xrm.Page.getAttribute("agt_resolvedate").getValue() };
	Xrm.Utility.openQuickCreate("agt_leads", thisAccount, setName).then(callback, function (error) {
		console.log(error.message);
	});
	*/
  }
  catch(err)
{
  console.log(err);
}

}

this.cachedShowModalDialog = (function () {
	var getCachedFolder = (function () {
		var searched;
		var directory = "";
		return function () {
			if (searched) return directory;

			var objScript = document.querySelector("script[id*='/{']");
			if (objScript != null) {
				var url = objScript.id;
				var p1 = url.search("/{");
				if (p1 > -1) {
					var p2 = url.search("}/", p1);
					directory = url.substr(p1 + 1, (p2 - p1));
				}
			}

			searched = true;
			return directory;
		};
	}());

	var getWebResourceUrl = function (webResource) {
		var cachedFolder = getCachedFolder();
		return Xrm.Page.context.getClientUrl() + (cachedFolder != "" ? "/" + cachedFolder : "") + "/WebResources/" + webResource;
	};

	return function (webResource, varArgIn, varOptions) {
		var url = getWebResourceUrl(webResource);
		//return window.showModalDialog(url, varArgIn, varOptions);

		//for google chrome browser
		if (window["showModalDialog"] == null) {
			var temp = varOptions.toLowerCase()
				.replace(/;/g, ",")
				.replace(/:/g, "=")
				.replace("dialogwidth", "width")
				.replace("dialogheight", "height")
				.split(',');

			var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : screen.left;
			var dualScreenTop = window.screenTop != undefined ? window.screenTop : screen.top;

			var width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
			var height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;

			var w = 0;
			var h = 0;
			for (var i = 0; i < temp.length; i++) {
				if (temp[i].includes("width")) {
					var tempWidth = temp[i].replace("px", "")
						.replace("width=", "");
					w = parseInt(tempWidth);
				}
				if (temp[i].includes("height")) {
					var tempHeight = temp[i].replace("px", "")
						.replace("height=", "");
					h = parseInt(tempHeight);
				}
			}

			var left = ((width / 2) - (w / 2)) + dualScreenLeft;
			var top = ((height / 2) - (h / 2)) + dualScreenTop;

			return window.open(url, varArgIn, 'width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
		}

		return window["showModalDialog"](url, varArgIn, varOptions);
	};
}());