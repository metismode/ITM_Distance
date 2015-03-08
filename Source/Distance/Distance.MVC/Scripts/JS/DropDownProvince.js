$(function () {
    var DDTambon = $("#DDTambon");
    var DDAmphur = $("#DDAmphur");
    $('.selectpicker').selectpicker();
    $("#dx-dropdown[name=ProviceId]").change(function (e) {
        e.preventDefault();
        var ProviceId = $(e.currentTarget).val();
        
        DDTambon.find("select").val("");
        DDTambon.find("select").prop('disabled', true);
        DDAmphur.find("select").val("");
        DDAmphur.find("select").prop('disabled', true);
        
        setTimeout(function () {
            $('.selectpicker').selectpicker();
        }, 50);
        $("#DDAmphur").load("../DropDown/AmphurList", {
            SelectedProperty: "AmphurId",
            SelectedValue: -1,
            IsMultiselect: false,
            CSSClass: "form-control selectpicker",
            HasSearch: true,
            ProviceId: ProviceId
        }, function () {
            $("#dx-dropdown[name=AmphurId]").on('change', function (e) {
                e.preventDefault();
                var AmphurId = $(e.currentTarget).val();
                setTimeout(function () {
                    $('.selectpicker').selectpicker();
                }, 50);
                DDTambon.load("../DropDown/TambonList", {
                    SelectedProperty: "TambonId",
                    SelectedValue: -1,
                    IsMultiselect: false,
                    CSSClass: "form-control selectpicker",
                    HasSearch: true,
                    AmphurId: AmphurId
                });
            });

        });
        //setTimeout(function () {
        //    $('.selectpicker').selectpicker();
        //}, 300);
        //DDTambon.load("../DropDown/TambonList", {
        //    SelectedProperty: "TambonId",
        //    SelectedValue: -1,
        //    IsMultiselect: false,
        //    CSSClass: "form-control selectpicker",
        //    HasSearch: true,
        //    AmphurId: -1
        //});
    });
    $('.selectpicker').selectpicker();
    $("#dx-dropdown[name=AmphurId]").on('change', function (e) {
        e.preventDefault();
        var AmphurId = $(e.currentTarget).val();
       
        DDTambon.load("../DropDown/TambonList", {
            SelectedProperty: "TambonId",
            SelectedValue: -1,
            IsMultiselect: false,
            CSSClass: "form-control selectpicker",
            HasSearch: true,
            AmphurId: AmphurId
        });
    });
    

});