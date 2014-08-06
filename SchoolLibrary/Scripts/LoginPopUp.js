
function Common() {
    _this = this;

    this.init = function () {
        $("#LoginPopup").click(function () {
            _this.showPopup("/Account/AjaxLogin", initLoginPopup);
        });
    }

    this.showPopup = function (url, callback) {
        $.ajax({
            type: "GET",
            url: url,
            success: function (data) {
                showModalData(data, callback);
            }
        });
    }

    function initLoginPopup(modal) {
        $("#LoginButton").click(function () {
            $.ajax({
                type: "POST",
                url: "/Account/AjaxLogin",
                data: $("#registration").serialize(),
                success: function (data) {
                    if (data.Success) {
                        window.location.reload();
                    } else {
                        var $errorContainer = $(modal).find('.validation-summary-error');
                        $errorContainer.text(data.ErrorMessage);
                    }
                    //showModalData(data);
                    //initLoginPopup(modal);
                }
            });
        });
    }

    function showModalData(data, callback) {
        $(".modal-backdrop").remove();
        var popupWrapper = $("#PopupWrapper");
        popupWrapper.empty();
        popupWrapper.html(data);
        var popup = $(".modal", popupWrapper);
        $(".modal", popupWrapper).modal();
        if (callback != undefined) {
            callback(popup);
        }
    }
}

var common = null;
$().ready(function () {
    common = new Common();
    common.init();
});