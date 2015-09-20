var zoom = 1.0;
var original_image;
var redraw = function () {
    var w = $(window).width();
    var h = $(window).height();

    //update canvas size and bounds
    $("#canvas").attr("width", original_image.width * zoom);
    $("#canvas").attr("height", original_image.height * zoom);

    //initialize the sketch
    $("#canvas").sketch();
};

$(document).ready(function () {
    //repaint the canvas first time in and on window resize
    original_image = new Image();
    original_image.src = $("#canvas").attr("data-img");

    redraw();
    $(window).resize(function () {
        redraw();
    });

    colorClick = function (color) {
        $("#canvas").css("cursor", "url(../Images/" + color + ".cur),url(../Images/" + color + ".gif),none");
    };

    toggleSpinner = function (show) {
        if (show) {
            $("#spinner_back").show();
            $("#spinner_front").show();
        }
        else {
            $("#spinner_back").hide();
            $("#spinner_front").hide();
        }
    };

    $("#btnCancel").click(function () { window.location = $("#return_url").val(); });
    $("#btnSave").click(function () {
        toggleSpinner(true);
        var canvas = document.getElementById("canvas");
        var data = JSON.stringify({
            token: $("#refresh_token").val(),
            image: canvas.toDataURL("image/png").substring(22),
            resource: $("#resource").val(),
            fileput: $("#file_put").val()
        });
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: "../api/Save",
            headers: {
                "content-type": "application/json"
            },

            data: data,
            dataType: "json",
            success: function (d) {
                toggleSpinner(false);
                window.location = $("#return_url").val();
            },
            error: function (e) {
                alert("Save failed");
            }
        });
    });

    $("#btnZoomin").click(function () {
        zoom += .25;

        //update canvas size and bounds
        $("#canvas").attr("width", original_image.width * zoom);
        $("#canvas").attr("height", original_image.height * zoom);
        $("#canvas").zoom(zoom);
    });

    $("#btnZoomout").click(function () {
        if (zoom != .25) {
            zoom -= .25;

            //update canvas size and bounds
            $("#canvas").attr("width", original_image.width * zoom);
            $("#canvas").attr("height", original_image.height * zoom);
            $("#canvas").zoom(zoom);
        }
    });

    $("#btnLarger").click(function () {
        $("#canvas").sizeMarker(true);
    });

    $("#btnSmaller").click(function () {
        $("#canvas").sizeMarker(false);
    });
});