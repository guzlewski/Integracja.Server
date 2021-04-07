$('#sidebarCollapse').on('click touchstart touchend', function () {
    $('#sidebar, .content').toggleClass('active');
});

$(document).ready(function () {
    $('table').DataTable(
        {
            stateSave: true
        }
    );
});

// zabrane z https://stackoverflow.com/questions/12744145/how-to-remember-scroll-position-of-page/12744617
// i przeniesione na https://github.com/js-cookie/js-cookie
$(document).ready(function() {

    // If cookie is set, scroll to the position saved in the cookie.
    if ( Cookies.get("scroll") !== null ) {
        $(document).scrollTop( Cookies.get("scroll") );
    }

    // When a button is clicked...
    $('.btn').on("click", function() {

        // Set a cookie that holds the scroll position.
        Cookies.set("scroll", $(document).scrollTop() );
    });
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