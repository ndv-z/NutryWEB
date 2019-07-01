// HANDLE THE ON SUBMIT OF THE FORM

function jsonToServer(json, controller, action, paramName) {

    var data = JSON.stringify(json);

    $('#regForm').submit(function (e) {

        // prevent default!
        e.preventDefault();

        // SEND JSON TO CONTROLLER
        $.ajax({
            url: "@Url.Action(" + action + "," + controller + ")",
            type: "POST",
            data: { paramName: data },
            // ON FAILURE
            error: function (response) {
                alert('controller response failed!');
                return "error";
            },
            // ON SUCCESS, USE RETURNED JSON RESULT:
            success: function (response) {
                //var json_obj = JSON.parse(response);
                return response;

            }
        });

    });



}


