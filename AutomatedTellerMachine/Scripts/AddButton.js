function initialize() {
    $(".datepicker").datepicker({ format: 'dd/mm/yyyy', autoclose: true, todayBtn: 'linked' })
    $(".datepicker").datepicker("update", new Date());

    $('#btnAddNew').click(function () {
        if ($('.add-item').length==0) {
            var newRow = $("<tr>");
            newRow.append("<td style='padding-left:20px'><button class='btn_grid add-item edit-mode' id='0'>Add</button>&nbsp;<button class='btn_grid remove-item edit-mode' id='0'>Cancel</button> </td>");
            newRow.append("<td>&nbsp;</td>");
            newRow.append("<td><input type='text' id='Question-Add'/></td>");

            $('#gridContent tbody:last').append(newRow);
            $(".datepicker").datepicker({ format: 'dd/mm/yyyy', autoclose: true, todayBtn: 'linked' })
            $(".datepicker").datepicker("update", new Date());
        }
        $('.add-item').on('click', function () {
            var tr = $(this).parents('tr:first');
            var ID = $(this).prop('Id');
            var Question = tr.find('#Question-Add').val();

            $.post(
                '/Example/SaveContact/',
                { ID: ID, Question: Question, LastName: LastName, Checked: Checked },
                function (item) {
                    tr.find('#Id').text(item.FirstName);
                    tr.find('#Question').text(item.LastName);
                    tr.find('#Checked').text(item.ContactNo);
                }, "json");
            tr.remove();
        });
    });
}