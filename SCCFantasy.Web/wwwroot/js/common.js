
var __messages = {
    DefaultError: "Error. Please try again or contact the administrator",
    ModifySuccessfully: "Modified successfully"
}

function ToastService() {
    this.defaultError = __messages.DefaultError;

    this.defaultSettings = {
        showCloseButton: true,
        position: { X: 'Right', Y: 'Bottom' },
        target: document.body,
    };
    ToastService.prototype.show = function (toast, toastSettings) {

        let settings = Object.assign(this.defaultSettings, toastSettings);
        let toastObj = new ej.notifications.Toast(settings);
        toastObj.appendTo('#toast-container');
        toastObj.show(toast)
    };

    ToastService.prototype.success = function (content, toastSettings) {
        let toast = { title: 'Success', content: content, cssClass: 'e-toast-success', icon: 'e-success toast-icons' };
        this.show(toast, toastSettings);
    };

    ToastService.prototype.info = function (content, toastSettings) {
        let toast = { title: 'Info', content: content, cssClass: 'e-toast-info', icon: 'e-info toast-icons' };
        this.show(toast, toastSettings);
    };

    ToastService.prototype.warning = function (content, toastSettings) {
        let toast = { title: 'Warning', content: content, cssClass: 'e-toast-warning', icon: 'e-warning toast-icons' };
        this.show(toast, toastSettings);
    }
    ToastService.prototype.error = function (content, toastSettings) {
        let toast = { title: 'Error', content: content || this.defaultError, cssClass: 'e-toast-danger', icon: 'e-error toast-icons' };
        this.show(toast, toastSettings);
    }
};

var __maxPlayerSetting = [{ positionId: 1, maxCount: 2 }, { positionId: 2, maxCount: 5 }, { positionId: 3, maxCount: 5 }, { positionId: 4, maxCount: 3 }];