$(function () {
    $(".plus").click(function () {
        var id = $(this).attr("post-id");
        $.ajax({
            url: "/api/Plus/Post/" + id,
            type: "POST",
            data: {},
            datatype: "json",
            success: function (data) {
                $.get("/api/Plus/GetPostCounter/" + id,
                    function (data) {
                        var item = $("button[post-id=" + id + "]");
                        if ($(item).attr("post-id") === id) {
                            if (item.hasClass("btn-default")) {
                                item.addClass("btn-success")
                                    .removeClass("btn-default")
                                    .css("font-weight", "Bold")
                                    .text("+" + data);
                            } else if (item.hasClass("btn-success")) {
                                item.addClass("btn-default")
                                    .removeClass("btn-success")
                                    .css("font-weight", "Bold")
                                    .text(data);
                            }
                        }
                    });               
            },
            error: function () {
                console.log("ERROR");
            }
        });
    });
});



$(function () {
    $("#addPost").click(function () {
        $.ajax({
            url: "api/Posts/Post/",
            type: "POST",
            data: { Content: $("#Content").val() },
            datatype: "json",
            success: function (data) {
                $("#addPost").unbind("submit").submit().html(data);
            },
            error: function () {
                console.log("ERROR");
            }
        });
    });
});

$(function () {
    $(".commentplus").click(function () {
        var id = $(this).attr("comment-id");
        $.ajax({
            url: "/api/Plus/Comment/" + id,
            type: "POST",
            data: {},
            datatype: "json",
            success: function (data) {
                $.get("/api/Plus/GetCommentCounter/" + id,
                    function (data) {
                        var item = $("button[comment-id=" + id + "]");
                        if ($(item).attr("comment-id") === id) {
                            if (item.hasClass("btn-default")) {
                                item.addClass("btn-success")
                                    .removeClass("btn-default")
                                    .css("font-weight", "Bold")
                                    .text("+" + data);
                            } else if (item.hasClass("btn-success")) {
                                item.addClass("btn-default")
                                    .removeClass("btn-success")
                                    .css("font-weight", "Bold")
                                    .text(data);
                            }
                        }
                    });
            },
            error: function () {
                console.log("ERROR");
            }
        });
    });
});