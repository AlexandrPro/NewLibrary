
$("#menu-toggle").click(function (e) {
    e.preventDefault();
    $("#wrapper").toggleClass("toggled");
});



$(window).resize(function () {
    var x = window.innerWidth;
    if (x <= 770
        && $("#wrapper").is(".toggled")
        ) {
        $("#wrapper").toggleClass("toggled");
    }
    if (x > 770 && $("#wrapper").is(":not(.toggled)")) {
        $("#wrapper").toggleClass("toggled");
    }
});