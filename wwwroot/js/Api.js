// Скрипт Jquery для api

var tableActive = "none";

// Обработка нажатия кнопки таблиц
$(".btn-event-get-table").on("click", function () {

    // Удаляем класс "active" у всех элементов с классом ".btn-event-get-table"
    $(".btn-event-get-table").removeClass("active");

    tableActive = $(this).data('tablename');

    createTable();

    // Добавляем класс "active" к текущему элементу
    $(this).addClass("active");
});



// Получение столбцов таблицы
function GetColums(callback, name) {

    if (name === null || name === undefined)
        name = tableActive;

    $.get('api/DB/columsTable', { tableName: name })
        .done(function (data) {
            callback(data);
        });
}


// Получение данных таблицы
function GetTable(callback, name) {
    if (name === null || name === undefined)
        name = tableActive;

    $.get('api/DB/getTable', { tableName: name })
    .done(function (data) {
        callback(data)
    });
}

// добавление данных
function PostRow(callback, name, data) {
    $.ajax({
        url: `Home/PostForm?tableName=${name}`, 
        type: 'POST',
        data: data,
        success: function (response) {
            callback(response)
        },
        error: function (error) {
            
        }
    });
}

function DeleteRow(callback, id) {
    $.ajax({
        url: `api/DB/daleteRow?tableName=${tableActive}&id=${id}`,
        type: 'DELETE',
        success: function() {
            callback();
        },
        error: function(jqXHR, textStatus, errorThrown) {
            // Обработка ошибок
        }
    });
}

function FilterTable(callback, data) {
    $.ajax({
        url: `Home/PostFilterOrder`, 
        type: 'POST',
        data: data,
        success: function (response) {
            callback(response);
        },
        error: function (error) {

        }
    });
}

$('#inputSearchBtn').on("click", function(){
    if ($('#inputSearch').val() === undefined)
    {
        GetTable(function (data) {

            CreateRows(data);

        });
    }
    else {
        $.ajax({
            url: `api/DB/Search?tableName=${tableActive}`, 
            type: 'GET',
            data: { 'search' : $('#inputSearch').val()},
            success: function (response) {
                CreateRows(response);
            },
            error: function (error) {
    
            }
        });
    }
});


$(function () {
    
});



