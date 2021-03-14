var elements = document.getElementsByClassName("script-save-submit");
for (var i = 0; i < elements.length; i++) {
    elements[i].addEventListener("click", saveFormThenSubmitForm2, false);
}
