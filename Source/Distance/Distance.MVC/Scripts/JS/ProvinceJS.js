
function SubmitBTN() {
    modalPopup.hide();
    bootbox.dialog({
        message: "<p>Do you want to save this data?</p>",
        title: "<span class=\"glyphicon glyphicon-info-sign\"></span> Message",
        buttons: {
            danger: {
                label: "Cancel",
                className: "btn-danger",
                callback: function () {
                    modalPopup.show();
                }
            },
            success: {
                label: "Save",
                className: "btn-success",
                callback: function () {
                    var $form = $('#formp');
                    Save($form)
                }
            }
        }
    });
}




function Save($form) {

    switch (modalAction) {
        case "Add":

            var url = '/Master/AddProvinceAjax'

            break;

        case "Edit":

            var url = '/Master/EditProvinceAjax'

            break;
        default:

            break;
    }



    var posting = $.post(url + "?" + (new Date()).getTime(), {
        Id: $form.find("input[name='Id']").val(),
        Name: $form.find("input[name='Name']").val(),
        Lat: $form.find("input[name='Lat']").val(),
        Lon: $form.find("input[name='Lon']").val(),
        Status: $form.find("select[name='StatusId']").val()
    });

    posting.fail(function (error) {

        ShowTextMessageAlertWarning(error.responseText, null, function () {
            modalPopup.show();
        }
        );

    });
    posting.done(function (acceptText) {


        if (acceptText == "Save Successfully") {
            ShowTextMessageAlertMessage(acceptText, null, function () {
                modalPopup.hide();
                location.reload();
            });
        }
        else {
            ShowTextMessageAlertWarning(acceptText, $("#myModal"), function () {
                modalPopup.show();
            });
        }

    });

}