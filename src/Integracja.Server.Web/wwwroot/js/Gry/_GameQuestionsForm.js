$(document).ready(function () {

    $('#gameQuestionsTable').DataTable({
        initComplete: function () {
            this.api().columns(1).every(function () {
                var column = this;
                var select = $('<select><option value=""></option></select>')
                    .appendTo($(column.footer()).empty())
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
            { "aTargets": [2], "orderDataType": "dom-text", type: 'string' },
        ]
    });

    $("#gameQuestionsCreateBtn").on("click", function () {

        if ($('[name="gameQuestionCheckbox"]:checked').length <= 0) {
            alert('Please select minimum one data');
        }
        else {

            var questionIds = new Array();
            $('[name="gameQuestionCheckbox"]:checked').each(function (data) {

                var questionId = $(this).data("question-id");

                questionIds.push(questionId);
            });

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