// wzięte z https://stackoverflow.com/questions/6982692/how-to-set-input-type-dates-default-value-to-today
Date.prototype.toDateInputValue = (function () {
    var local = new Date(this);
    local.setMinutes(this.getMinutes() - this.getTimezoneOffset());
    return local.toJSON().slice(0, 10);
});
Date.prototype.toTimeInputValue = (function () {
    var local = new Date(this);
    local.setMinutes(this.getMinutes() - this.getTimezoneOffset());
    var test = local.toJSON().slice(11,16);
    return test;
});

$(document).ready(function () {
    $('#startDate').val(new Date().toDateInputValue());
});
$(document).ready(function () {
    $('#endDate').val(new Date().toDateInputValue());
});
$(document).ready(function () {
    var start = new Date();
    
    var minutes = 60 - start.getMinutes(); // do pełnej godziny
    if (minutes < 5)
        minutes += 60;

    start.setTime(start.getTime() + minutes * 60 * 1000);
    $('#startTime').val(start.toTimeInputValue());
});
$(document).ready(function () {
    var end = new Date();

    var minutes = 60 - end.getMinutes();
    if (minutes < 5)
        minutes += 60;

    end.setTime(end.getTime() + (minutes+60) * 60 * 1000); // za godzinę od teraz
    $('#endTime').val(end.toTimeInputValue());
});