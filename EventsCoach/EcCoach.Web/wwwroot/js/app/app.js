var EventsController = function() {
    var init = function () {

        $(".js-toggle-attendance").click(toggleAAttendance);

    };

    var toggleAAttendance = function(e) {
        var button = $(e.target);
        if (button.hasClass("btn-default")) {
            $.post("api/attendances",
                    {
                        eventId: button.attr("data-event-id")
                    })
                .done(function() {
                    button.removeClass("btn-default").addClass("btn-info").text("Attending");
                })
                .fail(fail);
        }
        else {
            $.ajax({
                    url: "/api/attendances/" + button.attr("data-event-id"),
                    method: "DELETE"
                })
                .done(function() {
                    button.removeClass("btn-info").addClass("btn-default").text("Going?");
                })
                .fail(fail);
        }
    };

    var fail = function() {
        alert("Something fail");
    };

    return { init: init };
}();

var g = function () {
    var name = "Starling from other place";
    console.log(name);
}();

function initEvents() {

   

}


$(".js-toggle-follow").click(function (e) {
    var button = $(e.target);

    $.post("/api/followings/", { followeeId: button.attr("data-user-id") })
        .done(function() {
            button.removeClass("btn-light").addClass("btn-info").text('Following');
        })
        .fail(function() {
            alert("Someting fail");
        });
});
