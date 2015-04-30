function SubmitBTN() {
    modalPopupProfile.hide();
    bootbox.dialog({
        message: "<p>Do you want to save this data?</p>",
        title: "<span class=\"glyphicon glyphicon-info-sign\"></span> Message",
        buttons: {
            danger: {
                label: "Cancel",
                className: "btn-danger",
                callback: function () {
                    modalPopupProfile.show();
                }
            },
            success: {
                label: "Save",
                className: "btn-success",
                callback: function () {
                    var $form = $('#formprofile');
                    Save($form)
                }
            }
        }
    });
}




function Save($form) {

   

    var url = '/Profile/AjaxEdit'

     

    var posting = $.post(url + "?" + (new Date()).getTime(), {
        Id: $form.find("input[name='id']").val(),
     
        Password: $form.find("input[name='Password']").val(),
        FirstName: $form.find("input[name='FirstName']").val(),
        LastName: $form.find("input[name='LastName']").val(),
        NickName: $form.find("input[name='NickName']").val(),
        Email: $form.find("input[name='Email']").val(),
        Phone: $form.find("input[name='Phone']").val(),
        
    });

    posting.fail(function (error) {

        ShowTextMessageAlertWarning(error.responseText, null, function () {
            modalPopupProfile.show();
        }
        );

    });
    posting.done(function (acceptText) {


        if (acceptText == "Save Successfully") {
            ShowTextMessageAlertMessage(acceptText, null, function () {
                modalPopupProfile.hide();
                location.reload();
            });
        }
        else {
            ShowTextMessageAlertWarning(acceptText, $("#myModal"), function () {
                modalPopupProfile.show();
            });
        }

    });

}