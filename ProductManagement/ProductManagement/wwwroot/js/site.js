$(document).ready(function () {

    $.get("http://supermariobros.us-east-2.elasticbeanstalk.com/api/posts/temperature", function(data) {
        $("#temperatura_ambiente_sensor").text(data.sensor_1 + "° C");
    });

    setInterval(function () {
        $.get("http://supermariobros.us-east-2.elasticbeanstalk.com/api/posts/temperature", function(data) {
            $("#temperatura_ambiente_sensor").text(data.sensor_1 + "° C");

            $.ajax({
                type: "POST",
                url: "api/registo",
                data: JSON.stringify({
                    "Valor": data.sensor_1
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json"

            });

        });
    }, 10000);

});
