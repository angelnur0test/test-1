function InitDataTable(ob, rowtableId) {
    if ($.fn.dataTable.isDataTable('#' + rowtableId)) {
        $('#' + rowtableId).DataTable().destroy();
    }
    $('#' + rowtableId).DataTable({
        data: ob.data,
        lengthMenu: [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
        columns: ob.columns,
        columnDefs: [
            {
                targets: [1],
                visible: false
            },
            {
                targets: [ob.DetailsLinkColumnNumber],
                render: function (data, type, row, meta) {
                    return '<a href="/details/' + row[1] + '">' + data + '</a>'; //target="_blank"
                }
            }
        ],
        initComplete: function () {

            var speed = 1500;
            var elementTop = $('#' + rowtableId).offset().top;
            var elementHeight = $('#' + rowtableId + '_wrapper').outerHeight(true);
            var windowHeight = $(window).height();
            var windowScroll = $(document).scrollTop();

            if (elementTop + elementHeight < windowScroll + windowHeight) {
                console.log(1);
            } else if (elementHeight < windowHeight) {
                $([document.documentElement, document.body]).animate({
                    scrollTop: elementTop + elementHeight - windowHeight
                }, speed);
                console.log(2);
            } else {
                $([document.documentElement, document.body]).animate({
                    scrollTop: elementTop
                }, speed);
                console.log(3);
            }
        }

    });
}