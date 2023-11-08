const toastLiveExample = document.getElementById('liveToast');


var last_id = "none";

function createTable() {

    // Очищяем таблицу
    var container = $('#table-container').html('');
    $('#form').html("");

    var table = $('<table id="ContentTable" class="table table-hover table-striped caption-top align-middle"></table>');

    var caption = $(`<caption id="TableCaption">${tableActive}</caption>`);

    var thead = $('<thead id="TableHead" class="table-dark"></thead>');

    GetColums(function (data) {
        var tr_head = $('<tr></tr>');
        data.forEach(element => {
            if (element == "id") {
                let td = $(`<th>#</th>`);
                tr_head.append(td);
            }
            else {
                let td = $(`<th>${element}</th>`);
                tr_head.append(td);
            }
        });

        let td_actions = $('<th>Действия</th>');
        tr_head.append(td_actions);

        thead.append(tr_head);
    });

    var tbody = $('<tbody id="TableBody"></tbody>');

    GetTable(function (data) {

        CreateRows(data);

    });

    table.append(caption);
    table.append(thead);
    table.append(tbody);
    container.append(table);
}



$(".btn-event-add-row").on("click", function() {
    $(this).addClass("active");
    $('#form').html("");
    $('#form').load(`Home/GetForm?tableName=${tableActive}`);
});

$(".btn-event-add-filter").on("click", function() {
    $(this).addClass("active");
    $('#form').html("");
    $('#form').load(`Home/GetFilter?tableName=${tableActive}`);
});

function EventON() {

    const toastBootstrap = bootstrap.Toast.getOrCreateInstance(toastLiveExample);

    $('.btn-event-row').on("click", function () {
        let id = $(this).data('id');
        DeleteRow(function () {
            $(`tr.row-${id}`).remove();
            toastBootstrap.show();

        },
        id);
    });
}



function CreateRows(data){
    var tbody = $('#TableBody');
    tbody.html("");

    var count_row = 1;
    data.forEach(element => {

        var tr_body = $(`<tr id="row-${element.id}"></tr>`);

        for (const key in element) {
            if (key == "id") {
                var td = $(`<td>${count_row}</td>`);

                count_row++;
                last_id = count_row;
                tr_body.append(td);
                continue;
            }

            var td = $(`<td>${element[key]}</td>`)
            tr_body.append(td);
        }

        var td_actions = $(`<td></td>`);

        var btn_delete = $(`<div class="text-danger btn-event-row" data-id=${element.id} ><i class="bi bi-trash-fill"></i>убрать</div>`);

        td_actions.append(btn_delete);

        tr_body.append(td_actions);

        tbody.append(tr_body);

        EventON();
    });

}