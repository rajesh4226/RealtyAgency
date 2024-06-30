$(document).ready(function() {
    /*For Responsive Data Table*/
    //$('#example').DataTable({
    //    responsive: {
    //        details: {
    //            type: 'column',
    //            target: -1
    //        }
    //    },
    //    columnDefs: [{
    //        className: 'control',
    //        orderable: false,
    //        targets: -1
    //    }]
    //});

    // toggle navigation bar

    $(".tgl-btn").click(function() {
        $("body").toggleClass("hide-nav");
    });

    $(document).on("click", '.menu-icon a', function () {
        console.log('console working')
        $("body").toggleClass("toggled");
    });

    //$(document).on("click", '.mainMenu li a', function () {
    //    $(this).parents('.mainMenu').find('.subMenu').removeClass('openMenu');
    //    $(this).parent().find('.subMenu').toggle('openMenu')
    //});
    $("ul.mainMenu li a").click(function () {
        $(".subMenu").slideUp();
        $(this).next().slideDown();

    });
    $(document).click(function () {
        var container = $("#hamMenu");
        if (!container.is(event.target) &&
            !container.has(event.target).length) {
            $(".subMenu").hide();
            //$(".subMenu").hide();
        }
    });

});