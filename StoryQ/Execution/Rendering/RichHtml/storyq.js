$(document).ready(function () {

    $("#tags").tagcloud({ type: "list", colormin: "555", colormax: "000", seed: 0 });
    $("#tree").treeview();

    $("#tree a").click(function (event) {
        event.preventDefault();
        $("#tree a").removeClass("selected");
        $(this).addClass("selected");
        reFilter();
        this.blur();

    });

    $("#tags a").click(function (event) {
        event.preventDefault();
        $(this).toggleClass("tagged");
        reFilter();
        this.blur();

    });

    $("a.exception").click(function (event) {
        event.preventDefault();
        this.blur();
        var maskHeight = $(document).height();
        var maskWidth = $(window).width();

        //Set height and width to mask to fill up the whole screen
        $('#mask').css({ 'width': maskWidth, 'height': maskHeight });

        //transition effect
        $('#mask').fadeTo(200, 0.8);

        $("#dialogContent").html($(this).attr("title").replace(/\n/g, "<br/>"));

        var d = $("#dialog");
        d.css('top', $(window).height() / 2 - d.height() / 2 + $(window).scrollTop());
        d.fadeIn(250);


    });

    $('#mask, #dialogHeading a').click(function () {
        $("#mask").fadeOut(150);
        $("#dialog").fadeOut(150);

    });

    var speed = 150;


    function reFilter() {
        //clear the tracking classes
        $("#stories div.story").removeClass().addClass("story");

        //add the class "tagMatch" to any story that matches the current tag selection
        var tags = $("#tags a.tagged");
        if (tags.length == 0) {
            $("#stories div.story").addClass("tagMatch");
        }
        else {
            tags.each(function () {
                var tag = $(this).attr("href").replace("#", "");
                $("#stories div.story[tags~=" + tag + "]").addClass("tagMatch");
            });
        }

        //add the class "treeMatch" to any story that matches the current hierarchy
        var selection = $("#tree a.selected").attr("href").replace("#", "");
        $("#stories div[classHierarchy^=" + selection + "]").addClass("treeMatch");

        //display the matched stories
        $("#stories div.story.tagMatch.treeMatch:hidden").slideDown(speed);
        //hide the unmatched ones
        $("#stories div.story:visible").not(".tagMatch.treeMatch").slideUp(speed);

        //display the matched "headings"
        $("#stories a.itemHeading[name^=" + selection + "]:hidden").slideDown(speed);
        $("#stories a.itemHeading:visible").not("[name^=" + selection + "]").slideUp(speed);
    }
});