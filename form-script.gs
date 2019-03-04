// Put your OpenFaaS gateway public URL here
var baseURL = "http://02626413.ngrok.io";
var functionURL = "/function/signup-form";

function onSubmit(entry) {
  var response = entry.response.getItemResponses();
  
  var answers = {};

    for (var j = 0; j < response.length; j++) {
        var itemResponse = response[j];
        answers[itemResponse.getItem().getTitle()] = itemResponse.getResponse();
    }
  
  // Build request
    var options = {
        method: "post",
        payload: JSON.stringify(answers)
    };
  // Send to OpenFaaS
  UrlFetchApp.fetch(baseURL + functionURL, options);
};