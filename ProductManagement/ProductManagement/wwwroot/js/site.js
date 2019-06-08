$(document).ready(function () {

    $.get("http://supermariobros.us-east-2.elasticbeanstalk.com/api/posts/temperature", function(data) {
        $("#temperatura_ambiente_sensor").text(data.sensor_1 + "° C");
    });

    setInterval(function () {
        $.get("http://supermariobros.us-east-2.elasticbeanstalk.com/api/posts/temperature", function(data) {
            $("#temperatura_ambiente_sensor").text(data.sensor_1 + "° C");
        });
    }, 5000000);

});
