window.sccFantasy = window.sccFantasy || {};
window.sccFantasy.settings = window.sccFantasy.settings || {};

var __messages = {
    DefaultError: "Error. Please try again or contact the administrator",
    ModifySuccessfully: "Modified successfully",
    LoginSuccessfully: "Login successfully",
    RegisterSuccessfully: "Create account successfully"
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

function ValidationMessageProvider(formSelector, summarySelector) {

    this.formSelector = formSelector || 'form';
    this.$formEl = $(formSelector);
    this.summarySelector = summarySelector || '.validation-summary';

    ValidationMessageProvider.prototype.reset = function () {
        var validationSummaryEl = this.$formEl.find(this.summarySelector);
        $('[data-valmsg-for]', this.$formEl).each(function () { $(this).text('').addClass("field-validation-valid").removeClass("field-validation-error") })
        validationSummaryEl.empty();
    };

    ValidationMessageProvider.prototype.showErrors = function (errors) {
        var $validationSummaryEl = this.$formEl.find(this.summarySelector);
        var provider = this;
        errors.forEach(function (error) {
            var errorName = error.Error;
            var errorDescription = encodeSpecialCharacterForXTemplate(error.Description);
            var validationMessageEl = $('[data-valmsg-for="' + errorName + '"]', provider.$formEl)[0] || provider.queryValidationMessageForListItems(errorName);
            if (validationMessageEl != null) {
                $(validationMessageEl).html(errorDescription).removeClass("field-validation-valid").addClass("field-validation-error");
            } else {
                $validationSummaryEl.append('<p>' + errorDescription + '</p>');
            }
        });
    };

    ValidationMessageProvider.prototype.queryValidationMessageForListItems = function (propertyName) {
        var regex = /^(\w+)\[\d+\]((\[\w+\])*|(\.\w+)*)$/;
        if (regex.test(propertyName)) {
            var listPropName = propertyName.match(regex)[1];
            var propName = propertyName.match(regex)[2];
            if (propName) {
                listPropName += propName;
            }
            return $('[data-valmsg-for="' + listPropName + '"]', this.$formEl)[0];
        } else {
            return null;
        }
    }
}

function encodeSpecialCharacterForXTemplate(str) {
    if (checkString(str)) {
        return "";
    }
    var encodeStr = str;
    encodeStr = encodeStr.replace(/&/g, "&amp;");
    encodeStr = encodeStr.replace(/'/g, "&apos;");
    encodeStr = encodeStr.replace(/"/g, "&quot;");
    encodeStr = encodeStr.replace(/</g, "&lt;");
    encodeStr = encodeStr.replace(/>/g, "&gt;");
    return encodeStr;
}

function checkString(value) {
    return (!value || value == undefined || value == "" || value.length == 0);
}

var spinnerHelper = {
    createSpinnerByElement: function (el) {
        ej.popups.createSpinner({ target: el });
    },

    createSpinner: function (selector) {
        this.createSpinnerByElement(document.querySelector(selector));
    },

    showSpinner: function (selector) {
        var spinner = $(selector);
        spinner.removeClass("e-spin-hide");
        spinner.addClass("e-spin-show");
    },

    hideSpinner: function (selector) {
        var spinner = $(selector);
        spinner.addClass("e-spin-hide");
        spinner.removeClass("e-spin-show");
    }
}