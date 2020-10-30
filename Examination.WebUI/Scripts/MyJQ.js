//jQuery("#Add-Hint-Link").click(function () {
//    $.ajax({
//        url: this.href,
//        cache: false,
//        async: false,
//        success: function (html) {
//            jQuery("#camp-schedules").append(html);
//        }
//    });

//    return false;
//});

//jQuery('a.delete-schedule-link').live('click', function () {
//    var hasEnry = $(this).attr("data-hasEntry");
//    if (hasEnry == "False") {
//        jQuery(this).parents("div.camp-schedule:first").remove();
//    }

//    return false;

//});
$(document).ready(function () {
    $('#Add-Hint-Link').click(function () {
        $('#Add-Hint-Link').load('/Question/HtmlHint');
    });
});