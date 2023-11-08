var ActiveChart = "none";

$(".btn-event-create-chart").on("click", function (event) {
    event.preventDefault();

    ActiveChart = $(this).data('tablename');
    GetTable(function (data) {



        CreateStolbChart(data)
    }, ActiveChart);
});


function CreateStolbChart(rawData) {
    // Преобразуйте данные в агрегированный формат
    var aggregatedData = {};
    rawData.forEach(function (item) {
        var date = new Date(item.ДатаЗаказа);
        var month = date.getMonth();
        if (!aggregatedData[month]) {
            aggregatedData[month] = 0;
        }
        aggregatedData[month] += item.Сумма;
    });


    // Создайте массив для данных, понятных Chart.js
    var data = {
        labels: ["Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"],
        datasets: [
            {
                data: Object.values(aggregatedData),
                backgroundColor: ["#FF6384", "#36A2EB", "#FFCE56", "#FF6384", "#36A2EB", "#FFCE56", "#FF6384", "#36A2EB", "#FFCE56", "#FF6384", "#36A2EB", "#FFCE56"],
            },
        ],
    };

    // Получите элемент <canvas> по его идентификатору
    var ctx = document.getElementById("myChart").getContext("2d");

    // Создайте круговую диаграмму
    var myChart = new Chart(ctx, {
        type: "doughnut",
        data: data,
    }
    );
}