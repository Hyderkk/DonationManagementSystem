var $toastMessages = (function () {

    var _message = "";
    var showSuccessToast = function (message) {

        _message = "";
        _message = message.charAt(0).toUpperCase() + message.slice(1);
        _message += message[message.length - 1] != "." ? "." : "";

        $('.notifyjs-corner').empty();
        $.notify(_message, "success");
    }
    var showErrorToast = function (message) {

        _message = "";
        _message = message.charAt(0).toUpperCase() + message.slice(1);
        _message += message[message.length - 1] != "." ? "." : "";

        $('.notifyjs-corner').empty();
        $.notify(_message, "error");
    }
    var showInfoToast = function (message) {

        _message = "";
        _message = message.charAt(0).toUpperCase() + message.slice(1);
        _message += message[message.length - 1] != "." ? "." : "";

        $('.notifyjs-corner').empty();
        $.notify(_message, "info");
    }
    var showWarningToast = function (message) {

        _message = "";
        _message = message.charAt(0).toUpperCase() + message.slice(1);
        _message += message[message.length - 1] != "." ? "." : "";

        $('.notifyjs-corner').empty();
        $.notify(_message, "warn");
    }

    return {
        showSuccess: function (message) { showSuccessToast(message); },
        showError: function (message) { showErrorToast(message); },
        showInfo: function (message) { showInfoToast(message); },
        showWarning: function (message) { showWarningToast(message); }
    }
})();