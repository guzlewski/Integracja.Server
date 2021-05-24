$(document).ready(function () {

    // https://datatables.net/plug-ins/sorting/custom-data-source/dom-checkbox
    $.fn.dataTable.ext.order['dom-checkbox'] = function (settings, col) {
        return this.api().column(col, { order: 'index' }).nodes().map(function (td, i) {
            return $('input', td).prop('checked') ? '1' : '0';
        });
    };

    var gameQuestionTable = $('#gameQuestionsTable').DataTable({
        stateSave: false,
        initComplete: function () {
            this.api().columns(1).every(function () {
                var column = this;
                var select = $('<select><option value=""></option></select>')
                    .appendTo($(column.header()).empty())
                    .on('change', function () {
                        var val = $.fn.dataTable.util.escapeRegex(
                            $(this).val()
                        );

                        column
                            .search(val ? '^' + val + '$' : '', true, false)
                            .draw();
                    });

                column.data().unique().sort().each(function (d, j) {
                    if (column.search() === '^' + d + '$') {
                        select.append('<option value="' + d + '" selected="selected">' + d + '</option>')
                    } else {
                        select.append('<option value="' + d + '">' + d + '</option>')
                    }
                });
            });
        },
        "aoColumnDefs": [
            { "aTargets": [2], "orderDataType": "dom-checkbox" },
        ]
    });

    $("#gameQuestionsCreateBtn").on("click", function () {

        var questionIds = new Array();
        $("input", gameQuestionTable.rows().nodes()).each(function () {

            var prop = $(this).prop("checked");
            if (prop) {
                var questionId = $(this).data("question-id");
                questionIds.push(questionId);
            }   
        });

        //console.log("length: " + questionIds.length);

        if (questionIds.length <= 0) {
            $("#gameQuestionsValidationMessage").text("Wybierz przynajmniej jedno pytanie");
        }
        else {
            var url = $(this).data("post-url");

            $.ajax({
                type: "POST",
                url: url,
                traditional: true,
                data: {
                    "gameQuestions": questionIds
                },
                async: false
            });

            var redirectUrl = $(this).data("redirect-url");
            window.location.href = redirectUrl;
        }
    });

});