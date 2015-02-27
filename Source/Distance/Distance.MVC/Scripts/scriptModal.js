

var modalPopup = $("#myModal");
var modalPopupTitle = $("#myModal div .modal-title")
var modalPopupBody = $("#myModal div .modal-body");
var modalPopupFooter = $("#myModal div .modal-footer");
var modalAction = "";


$(".modal-footer  button.btn-danger").click(function () {
    modalPopupBody[0].innerHTML = "";
    modalPopup.modal("hide");
    window.location.reload();

});

$("#myModal").on('hidden.bs.modal', function (e) {
    $('#myModal form').unbind("submit");
    modalPopupBody[0].innerHTML = "";
    modalPopup.modal("hide");
    modalPopup.unbind(e);
    window.location.reload();
});



function ShowTextMessageAlertMessage(str, modal, callback) {

    $("#ConfirmModalMessage div .modal-title")[0].innerHTML = "<p>" + "<i class=\"glyphicon glyphicon-info-sign\"></i> Message" + "</p>";
    $("#ConfirmModalMessage div .modal-body")[0].innerHTML = "<p>" + str + "</p>";
    $("#ConfirmModalMessage div .modal-footer").show();
    $("#ConfirmModalMessage").on("show.bs.modal", function (e) {
        $("#ConfirmModalMessage button.btn-success").text("OK");
        $("#ConfirmModalMessage button.btn-danger").hide();
        $("#ConfirmModalMessage button.btn-success").click(function (e) {
            // e.preventDefault();
            $("#ConfirmModalMessage").modal("hide");
            //modal.show();
            if (callback != null) {
                callback();
            }
        });
    });
    $("#ConfirmModalMessage").on("hidden.bs.modal", function (e) {

        $("#ConfirmModalMessage").modal("hide");
        if (callback != null) {
            callback();
        }
    });

    $("#ConfirmModalMessage").modal();
}

function ShowTextMessageAlertWarning(str, modal, callback) {

    $("#ConfirmModalWarning div .modal-title")[0].innerHTML = "<p>" + "<i class=\"glyphicon glyphicon-warning-sign\"></i> Warning" + "</p>";
    $("#ConfirmModalWarning div .modal-body")[0].innerHTML = "<p>" + str + " ! </p>";
    $("#ConfirmModalWarning div .modal-footer").show();
    $("#ConfirmModalWarning").on("show.bs.modal", function (e) {
        $("#ConfirmModalWarning button.btn-success").text("OK");
        $("#ConfirmModalWarning button.btn-danger").hide();
        $("#ConfirmModalWarning button.btn-success").click(function (e) {
            // e.preventDefault();
            $("#ConfirmModalWarning").modal("hide");
            //modal.show();
            if (callback != null) {
                callback();
            }
        });
    });
    $("#ConfirmModalWarning").on("hidden.bs.modal", function (e) {

        $("#ConfirmModalWarning").modal("hide");
        if (callback != null) {
            callback();
        }
    });

    $("#ConfirmModalWarning").modal();
}

//function ShowTextMessageAlert(str, modal, callback) {

//    $("#ConfirmModal div .modal-title")[0].innerHTML = "<p>" + "<i class=\"glyphicon glyphicon-info-sign\"></i> Message " + "</p>";
//    $("#ConfirmModal div .modal-body")[0].innerHTML = "<p>" + str + "</p>";
//    $("#ConfirmModal div .modal-footer").show();
//    $("#ConfirmModal").on("show.bs.modal", function (e) {
//        $("#ConfirmModal button.btn-success").text("OK");
//        $("#ConfirmModal button.btn-danger").hide();
//        $("#ConfirmModal button.btn-success").click(function (e) {
//            // e.preventDefault();
//            $("#ConfirmModal").modal("hide");
//            //modal.show();
//            if (callback != null) {
//                callback();
//            }
//        });
//    });
//    $("#ConfirmModal").on("hidden.bs.modal", function (e) {

//        $("#ConfirmModal").modal("hide");
//        if (callback != null) {
//            callback();
//        }
//    });

//    $("#ConfirmModal").modal();
//}

