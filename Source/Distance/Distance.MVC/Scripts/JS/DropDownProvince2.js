$(function () {
    var DDAmphur2 = $("#DDAmphur2");
    $('.selectpicker').selectpicker();
    $("#dx-dropdown[name=ProvinceId2]").change(function (e) {
        e.preventDefault();

        var ProvinceId = $(e.currentTarget).val();

        DDAmphur2.find("select").val("");
        DDAmphur2.find("select").prop('disabled', true);

        setTimeout(function () {
            $('.selectpicker').selectpicker();
        }, 100);
        $("#DDAmphur2").load("../DropDown/AmphurList2", {
            SelectedProperty: "AmphurId2",
            SelectedValue: -1,
            IsMultiselect: false,
            CSSClass: "form-control selectpicker",
            HasSearch: true,
            ProvinceId: ProvinceId
        }, function () {
            $("#dx-dropdown[name=AmphurId2]").on('change', function (e) {
                e.preventDefault();
                var AmphurId = $(e.currentTarget).val();
                setTimeout(function () {
                    $('.selectpicker').selectpicker();
                }, 100);

            });

        });

    });
    $('.selectpicker').selectpicker();
    $("#dx-dropdown[name=AmphurId2]").on('change', function (e) {
        e.preventDefault();

        var AmphurId = $(e.currentTarget).val();

    });

});


