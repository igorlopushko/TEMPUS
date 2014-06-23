$(document).on("click", ".deleteTimeRecord", function () {
    var timeReportId = $(this).data('id');
    $("a#deleteBtn").attr("href", "/TimeReport/Delete/" + timeReportId);
});