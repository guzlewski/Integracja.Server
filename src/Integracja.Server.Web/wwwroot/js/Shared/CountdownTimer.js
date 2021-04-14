$(document).ready(function () {
    updateTimer();
});

setInterval(function () { updateTimer(); }, 60 * 1000);

function getDurationString(timeTicks) {

    const DayTicks = 60 * 60 * 24 * 1000;
    const HourTicks = 60 * 60 * 1000;
    const MinuteTicks = 60 * 1000;

    var days = Math.floor(timeTicks / DayTicks);
    var hours = Math.floor((timeTicks % DayTicks) / HourTicks);
    var minutes = Math.floor((timeTicks % HourTicks) / MinuteTicks);
    //var seconds = Math.floor(  distance% MinuteTicks  / 1000 ); chyba nie będziemy używać sekund co

    var output = "error: getDurationString()";
    if (days > 0) {
        output = days + "d " + hours + "h " + minutes + "m";
    }
    else if (hours > 0) {
        output = hours + "h " + minutes + "m";
    }
    else if( minutes > 0){
        output = minutes + "m";
    }

    return output;
}

function updateTimer() {

    var timers = $('.countdown-timer');

    timers.each(function () {

        var ticksInput = $(this).data("countdown-date");

        var countdownDate = new Date(ticksInput).getTime();

        var now = new Date().getTime();

        var distance = countdownDate - now;

        var output = "";

        if (distance < 0) {
            output = "EXPIRED";
        }
        else {
            output = getDurationString(distance);
        }

        this.innerHTML = output;
    });
}
