$('#sidebarCollapse').on('click touchstart touchend', function () {
    $('#sidebar, .content').toggleClass('active');
});

saveForm = function (formId, url, callback) {
    return $.ajax({
        type: "POST",
        url: url,
        data: $("#" + formId).serialize(),
        success: callback
    });
}

submitForm = function (formId) {
    $("#" + formId).submit();
}

saveFormThenSubmitForm = function (formToSaveId, saveUrl, formToSubmitId) {
    saveForm(formToSaveId, saveUrl, submitForm(formToSubmitId));
}

saveFormThenSubmitForm2 = function () {
    var formToSaveId = this.dataset.saveForm;
    var saveUrl = this.dataset.saveUrl;
    var formToSubmitId = this.dataset.submitForm;
    saveForm(formToSaveId, saveUrl, submitForm(formToSubmitId));
}

disableFormInputs = function (formId) {
    $("#" + formId + " :input").attr("disabled", true);
}

emptyFunction = function () {

}