$(function () {

    $(".table-container tr").each(function () {
        let items = $(this).find(".sticky");
        let leftInstance = 0;
        for (let i = 0; i < items.length; i++) {
            $(items[i]).css({
                'left': leftInstance + 'px'
            });
            leftInstance += $(items[i]).outerWidth(true);
        }
    });

    $(".check-all").on('change', function () {
        $(this).closest('.table-container').find('input[type="checkbox"]:not(.check-all)').prop('checked', $(this).is(':checked'));
    })
    $(".table-container").on('change', 'input[type="checkbox"]:not(.check-all)', function () {
        var tableContainer = $(this).closest('.table-container');
        var checkboxes = tableContainer.find('input[type="checkbox"]:not(.check-all)');
        var checkAll = tableContainer.find('.check-all');

        if (checkboxes.length === checkboxes.filter(':checked').length) {
            checkAll.prop('checked', true);
        } else {
            checkAll.prop('checked', false);
        }
    });
})