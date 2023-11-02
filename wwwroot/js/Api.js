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

// Обработка нажатия кнопки добавления записи
$(".btn-event-add-row").on("click", function () {

});

// Получение столбцов таблицы
function GetColums(callback, name) {

    if (name === null || name === undefined)
        name = tableActive;

    $.get('api/DB/columsTable', { tableName: name })
        .done(function (data) {
            callback(data)
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
function PostRow() {
    
}


$(function () {

});



