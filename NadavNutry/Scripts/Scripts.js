// Stringify (and return) FORM DATA into JSON
function formToJson(form_name) {
    let form = document.forms[form_name];
    let fd = new FormData(form);
    let data = {};
    for (let [key, prop] of fd) {
        data[key] = prop;
    }
    data = JSON.stringify(data, null, 2);
    //alert(data);
    return data;
}


function ShowImagePreview(imageUploader, previewImage) {
    if (imageUploader.files && imageUploader.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImage).attr('src', e.target.result);
        }
        reader.readAsDataURL(imageUploader.files[0]);
    }
}

// GENERAL JQUERY POST FOR ANY TYPE OF FORM, WITH OR WITHOUT FILE UPLOAD
function jQueryAjaxPost(form) {

    $.validator.unobtrusive.parse(form);
    var succ = false;
    if ($(form).valid())
    {
        var ajaxConfig = {
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            success: function (response) {
                $('#firstTab').html(response);
                refreshAddNewTab($(form).attr('data-resetUrl'), true);
            }
        }
        if ($(form).attr('enctype') == "multipart/form-data") {
            ajaxConfig["contentType"] = false;
            ajaxConfig["processData"] = false;
        }
        $.ajax(ajaxConfig);

    }
    
    return false;
}

function refreshAddNewTab(resetUrl, showViewTab) {
    $.ajax({
        type: 'GET',
        url: resetUrl,
        success: function (response) {
            $('#secondTab').html(response);
            $('ul.nav.nav-tabs a:eq(1)').html('Add New');
            if (showViewTab) {
                $('ul.nav.nav-tabs a:eq(0)').tab('show');
            }
        }
    });
}

$('.message a').click(function () {
    $('form').animate({ height: "toggle", opacity: "toggle" }, "slow");
});