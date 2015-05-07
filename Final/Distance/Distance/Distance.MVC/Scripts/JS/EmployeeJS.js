
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


                    var fistInput = document.getElementById("pass1").value;
                    var secondInput = document.getElementById("pass2").value;
                    var n = fistInput.localeCompare(secondInput);

                    if (n == 0) {

                        var $form = $('#formemp');
                        Save($form)
                    }
                    else {
                        ShowTextMessageAlertWarning("password not match", $("#myModal"), function () {
                            modalPopup.show();
                        });
                    }

                }
            }
        }
    });
}




function Save($form) {

    switch (modalAction) {
        case "Add":

            var url = '/Employee/AddAjax'

            break;

        case "Edit":

            var url = '/Employee/EditAjax'

            break;
        default:

            break;
    }

    var posting = $.post(url + "?" + (new Date()).getTime(), {
        Id: $form.find("input[name='id']").val(),
        UserName: $form.find("input[name='UserName']").val(),
        Password: $form.find("input[name='Password']").val(),
        FirstName: $form.find("input[name='FirstName']").val(),
        LastName: $form.find("input[name='LastName']").val(),
        NickName: $form.find("input[name='NickName']").val(),
        Email: $form.find("input[name='Email']").val(),
        Phone: $form.find("input[name='Phone']").val(),
        Role: $form.find("select[name='RoleId']").val(),
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