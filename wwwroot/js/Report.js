var ActiveReport = "none";

$(".btn-event-create-report").on("click", function (event) {
    event.preventDefault();
    ActiveReport = $(this).data('tablename');
    CreateReport();
});

function CreateReport() {
    // Очищяем таблицу
    var container = $('#table-container').html('');
    $('#form').html("");

    var table = $('<table id="ContentTable" class="table table-hover table-striped caption-top align-middle"></table>');

    var caption = $(`#headerReport`);
    caption.html("Отчет");

    var thead = $('<thead id="TableHead"></thead>');

    GetColums(function (data) {
        var tr_head = $('<tr></tr>');

        for (element in data) {
            if (element == "id") {
                continue;
            }
            else {
                let td = $(`<th>${data[element]}</th>`);
                tr_head.append(td);
            }
        }

        thead.append(tr_head);
    },ActiveReport);

    var tbody = $('<tbody id="TableBody"></tbody>');

    GetTable(function (data) {

        CreateRowsReport(data);

    },ActiveReport);


    table.append(thead);
    table.append(tbody);
    container.append(table);
}


function CreateRowsReport(data){
    var tbody = $('#TableBody');
    tbody.html("");

    data.forEach(element => {

        var tr_body = $(`<tr id="row-${element.id}"></tr>`);

        for (const key in element) {
            if (key == "id") {
                continue;
            }

            var td = $(`<td>${element[key]}</td>`)
            tr_body.append(td);
        }

        var td_actions = $(`<td></td>`);

        tbody.append(tr_body);

    });

}


