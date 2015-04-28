$(function () {
    var DDTambon = $("#DDTambon");
    var DDAmphur = $("#DDAmphur");
    $('.selectpicker').selectpicker();
    $("#dx-dropdown[name=ProvinceId]").change(function (e) {
        e.preventDefault();
       
        var ProvinceId = $(e.currentTarget).val();

        DDTambon.find("select").val("");
        DDTambon.find("select").prop('disabled', true);
        DDAmphur.find("select").val("");
        DDAmphur.find("select").prop('disabled', true);
        
        setTimeout(function () {
            $('.selectpicker').selectpicker();
        }, 100);
        $("#DDAmphur").load("../DropDown/AmphurList", {
            SelectedProperty: "AmphurId",
            SelectedValue: -1,
            IsMultiselect: false,
            CSSClass: "form-control selectpicker",
            HasSearch: true,
            ProvinceId: ProvinceId
        }, function () {
            $("#dx-dropdown[name=AmphurId]").on('change', function (e) {
                e.preventDefault();
                var AmphurId = $(e.currentTarget).val();
                setTimeout(function () {
                    $('.selectpicker').selectpicker();
                }, 100);
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