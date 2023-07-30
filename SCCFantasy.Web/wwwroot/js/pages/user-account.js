window.sccFantasy.userAccount = new UserAccountPage();
function UserAccountPage() {
    var authenDivSelector = "#authentication-div";
    var authenFormSelector = "#form-authentication";
    var spinnerSelector = "#authentication-div > .e-spinner-pane";

    this.validationMessageProvider = new AccountFormValidationMessageProvider(new ValidationMessageProvider(authenFormSelector, '.validation-summary'));

    this.toastService = new ToastService();

    $(authenDivSelector).on('click', '.btn[data-action=login]', this, function (e) {
        e.preventDefault();

        var pageInstance = e.data;
        var ignoreValidation = "input[type=hidden], :not([name])";
        pageInstance.submitForm(ignoreValidation, "Login");
    });

    $(authenDivSelector).on('click', '.btn[data-action=register]', this, function (e) {
        e.preventDefault();

        var pageInstance = e.data;
        var ignoreValidation = "input[type=hidden], :not([name])";
        pageInstance.submitForm(ignoreValidation, "Register");
    });

    this.submitForm = function (ignoreValidation, action) {
        var pageInstance = this;

        pageInstance.validationMessageProvider.reset();

        var formEl = $(authenFormSelector);
        formEl.data("validator").settings.ignore = ignoreValidation;

        var isValidForm = formEl.valid();
        pageInstance.validationMessageProvider.displayErrorSections();

        if (isValidForm) {
            spinnerHelper.showSpinner(spinnerSelector);

            $(formEl).ajaxSubmit(function (res) {
                if (res.success) {
                    if (action == "Login") {
                        pageInstance.toastService.success(__messages.LoginSuccessfully);
                    } else {
                        pageInstance.toastService.success(__messages.RegisterSuccessfully);
                    }

                    window.location.href = window.serverUrl + "/Team/Index";
                }
                else if (res.errors && Array.isArray(res.errors)) {
                    spinnerHelper.hideSpinner(spinnerSelector);
                    pageInstance.validationMessageProvider.showErrors(res.errors);
                }
                else {
                    spinnerHelper.hideSpinner(spinnerSelector);
                    pageInstance.toastService.error("Error");
                }
            });
        }
    }

    $(document).ready(function () {
        spinnerHelper.createSpinner("#authentication-div");
    });
}

function AccountFormValidationMessageProvider(commonProvider) {
    this.commonProvider = commonProvider;
    this.$formEl = this.commonProvider.$formEl;

    var clearValidationError = function () {
        commonProvider.$formEl.find('.validation-error').each(function () {
            $(this).removeClass('validation-error')
        });
    }

    AccountFormValidationMessageProvider.prototype.reset = function () {
        this.commonProvider.reset();
        clearValidationError();
    };

    AccountFormValidationMessageProvider.prototype.showErrors = function (errors) {
        this.commonProvider.showErrors(errors);
        this.displayErrorSections();
    };

    AccountFormValidationMessageProvider.prototype.displayErrorSections = function () {
        clearValidationError();
        this.$formEl.find('.field-validation-error').each(function () {
            var validationMessageEl = this;
            var index = $(validationMessageEl).parents('[data-tab-content]').attr('data-tab-index');
        });
    };
};