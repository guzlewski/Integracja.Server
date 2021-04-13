$.extend(true, $.fn.dataTable.defaults, {
    stateSave: true,
    language:
    {
        url: '/other/DataTables/localization/pl.json'
    }
});

$(document).ready(function () {
    $('.data-table').DataTable();
});