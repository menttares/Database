

function createTable() {

    // Очищяем таблицу
    var container = $('#table-container').html('');

    var table = $('<table id="ContentTable" class="table table-hover table-striped caption-top align-middle"></table>');

    var caption = $(`<caption id="TableCaption">${tableActive}</caption>`);

    var thead = $('<thead id="TableHead" class="table-dark"></thead>');

    GetColums(function(data){
        var tr_head = $('<tr></tr>');
        data.forEach(element => {
            let td = $(`<th>${element}</th>`);
            tr_head.append(td);
        });

        let td_actions = $('<th>Действия</th>');
        tr_head.append(td_actions);

        thead.append(tr_head);
    });

    var tbody = $('<tbody id="TableBody"></tbody>')

    GetTable(function(data){
        

        data.forEach(element => {
            console.log(element);
            var tr_body = $('<tr></tr>');

            for (const key in element) {
                var td = $(`<td>${element[key]}</td>`)
                tr_body.append(td);
                console.log(element[key]);
            }

            var td_actions = $(`<td>demo</td>`)

            tr_body.append(td_actions);

            tbody.append(tr_body);
        });
    });

    table.append(caption);
    table.append(thead);
    table.append(tbody);
    container.append(table);
}