// zabrane z https://florianziegler.com/journal/css-tutorial-style-table-rows-to-look-like-cards/
$(document).ready(function () {
    var tables = $('.table-card-layout');

    // Create an array containing all table headers
    var table_headers = [];
    tables.each(function () {
        var th = [];
        $(this).find('thead th').each(function () {
            th.push($(this).text());
        });
        table_headers.push(th);
    });

    // Add a data-label attribute to each cell
    // with the value of the corresponding column header
    // Iterate through each table
    tables.each(function (table) {
        var table_index = table;
        // Iterate through each row
        $(this).find('tbody tr').each(function () {
            // Finally iterate through each column/cell
            $(this).find('td').each(function (column) {
                $(this).attr('data-label', table_headers[table_index][column]);
            });
        });
    });
});

