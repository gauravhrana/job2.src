<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="d3Sample.aspx.cs"
    Inherits="ApplicationContainer.UI.Web.Scheduling.Schedule.d3Sample" %>


<html>

<head>
    <title></title>
    
    <meta charset="utf-8">
    <style>
        .bar
        {
            fill: steelblue;
        }

            .bar:hover
            {
                fill: brown;
            }

        .axis
        {
            font: 10px sans-serif;
        }

            .axis path,
            .axis line
            {
                fill: none;
                stroke: #000;
                shape-rendering: crispEdges;
            }

        .x.axis path
        {
            display: none;
        }
    </style>


</head>

<body>
    <div id="viz"></div>
    <div id="title">Schedule Graph</div>
    <div id="graph"></div>
    <p></p>
    <script>
        //var data = [4, 8, 15, 16, 23, 42];

        //var width = 420,
        //    barHeight = 20;

        //var x = d3.scale.linear()
        //    .domain([0, d3.max(data)])
        //    .range([0, width]);

        //var chart = d3.select(".chart")
        //    .attr("width", width)
        //    .attr("height", barHeight * data.length);

        //var bar = chart.selectAll("g")
        //    .data(data)
        //  .enter().append("g")
        //    .attr("transform", function (d, i) { return "translate(0," + i * barHeight + ")"; });

        //bar.append("rect")
        //    .attr("width", x)
        //    .attr("height", barHeight - 1);

        //bar.append("text")
        //    .attr("x", function (d) { return x(d) - 3; })
        //    .attr("y", barHeight / 2)
        //    .attr("dy", ".35em")
        //    .text(function (d) { return d; });



    </script>
    <%--<script>

        var width = 420,
            barHeight = 20;

        var x = d3.scale.linear()
            .range([0, width]);

        var chart = d3.select(".chart")
            .attr("width", width);

        d3.tsv("data.tsv", type, function (error, data) {
            x.domain([0, d3.max(data, function (d) { return d.value; })]);
            alert(data.length);

            chart.attr("height", barHeight * data.length);

            var bar = chart.selectAll("g")
                .data(data)
              .enter().append("g")
                .attr("transform", function (d, i) { return "translate(0," + i * barHeight + ")"; });

            bar.append("rect")
                .attr("width", function (d) { return x(d.value); })
                .attr("height", barHeight - 1);

            bar.append("text")
                .attr("x", function (d) { return x(d.value) - 3; })
                .attr("y", barHeight / 2)
                .attr("dy", ".35em")
                .text(function (d) { return d.value; });
        });

        function type(d) {
            d.value = +d.value; // coerce to number
            return d;
        }

</script>--%>

    <script>       
        var margin = { top: 20, right: 20, bottom: 30, left: 40 }
        
     width = 960 - margin.left - margin.right,
     height = 500 - margin.top - margin.bottom;
        
        var x = d3.scale.ordinal()
            .rangeRoundBands([0, width], .1);
        //var x = d3.scale.linear()
       // .range([0, width]);

        var y = d3.scale.linear()
            .range([height, 0]);
        

        var xAxis = d3.svg.axis()
            .scale(x)
            .orient("bottom");

        var yAxis = d3.svg.axis()
            .scale(y)
            .orient("left")
            //.tickFormat(d3.format(".2s"));
            //.ticks(10,"%");

        var svg = d3.select("body").append("svg")
            .attr("width", width + margin.left + margin.right)
            .attr("height", height + margin.top + margin.bottom)
            .append("g")
            .attr("transform", "translate(" + margin.left + "," + margin.top + ")");
        var arr = new Array();

        d3.csv("myFile.csv", function (error, data) {
            x.domain(data.map(function (d) { return d.FirstName; }));
            y.domain([0, d3.max(data, function (d) { return d.TotalHoursWorked; })]);
           
            svg.append("g")
            .attr("class", "x axis")
            .attr("transform", "translate(0," + height + ")")
            .call(xAxis); 

            svg.append("g")
                .attr("class", "y axis")
                .call(yAxis)
              .append("text")
                .attr("transform", "rotate(-90)")
                 .attr("y", 6)
                .attr("dy", ".71em")
              //  .style("text-anchor", "end")
                .text("TotalHoursWorked");

            svg.selectAll(".bar")
                .data(data)
              .enter().append("rect")
                .attr("class", "bar")
                .attr("x", function (d) { return x(d.FirstName); })
                .attr("width", x.rangeBand())
                .attr("y", function (d) { return y(d.TotalHoursWorked); })
                .attr("height", function (d) { return (height - y(d.TotalHoursWorked)) });
                //.attr("height", function (d) { return height - y(d.TotalHoursWorked); });            
        }.bind(this));

        function type(d) {
            d.TotalHoursWorked = +d.TotalHoursWorked;
            return d;
        }
        //Add data to the graph and call enter.
        //var dataEnter = d3.select("body").selectAll("p").data(data).enter();
        //var header = d3.select("body").selectAll("p").data(d3.keys(data[0])).enter();
        //header.append("span").html(function (d) {
        //    return d +" ";       
        //});
        ////dataEnter.html(d3.keys(data[0]) + "</br>");
        //dataEnter.append("div").html(function (d) {                
        //    return d["WorkDate"] + "," + d["TotalHoursWorked"] + "," + d["FirstName"] +  "</br>";
        //});

        //    //Add data to the graph and call enter.
        //    var dataEnter = svg.selectAll("rect").data(myData).enter();
        //    //Draw the bars.
        //    dataEnter.append("rect").attr("x", function (d, i) {
        //        return i * horizontalBarDistance;
        //    }).attr("y", function (d) {
        //        return graphHeight - d * barHeightMultiplier;
        //    }).attr("width", function (d) {
        //        return barWidth;
        //    }).attr("height", function (d) {
        //        return d * barHeightMultiplier;
        //    });

        //    dataEnter.append("text").text(function (d) {
        //        return d;
        //    }).attr("x", function (d, i) {
        //        return i * horizontalBarDistance + textXOffset;
        //    }).attr("y", textYPosition);
        //});

        // });




        //    
        //    



        //Draw the text.


        //});


        //var parsedCSV = d3.csv.parse(datasetText);

        //    var parsedCSV = d3.csv.parse(datasetText, function (d) {
        //        return {
        //            //year: new Date(+d.Year, 0, 1), // convert "Year" column to Date
        //            FirstName: d.FirstName,
        //            WorkDate : d.WorkDate
        //            //model: d.Model,
        //            //length: +d.Length // convert "Length" column to number
        //        };
        //    }, function (error, rows) {
        //        console.log(rows);
        //    });
        //    var svg = d3.select("body").append("svg").attr("width", "100%").attr("height", "100%");


        //    var dataEnter = svg.selectAll("rect").data(parsedCSV).enter();
        //    var graphHeight = 450;
        //    var barWidth = 20;
        //    var barSeparation = 10;
        //    var maxData = 105;
        //    var horizontalBarDistance = barWidth + barSeparation;
        //    var textYOffset = horizontalBarDistance / 2 - 12;
        //    var textXOffset = 20;
        //    var barHeightMultiplier = graphHeight / maxData;


        //    //Draw the bars.
        //    dataEnter.append("rect").attr("y", function (d, i) {
        //        return i * horizontalBarDistance;
        //    }).attr("x", function (d) {
        //        return 100;
        //    }).attr("height", function (d) {
        //        return barWidth;
        //    }).attr("width", function (d) {
        //        return d * barHeightMultiplier;
        //    });


        //    //Draw the text.
        //    dataEnter.append("text").text(function (d) {
        //        return d;
        //    }).attr("y", function (d, i) {
        //        return i * horizontalBarDistance + textXOffset;
        //    }).attr("x");
        //});


        //    var sampleHTML = d3.select("#viz")
        //        .append("table")
        //        .style("border-collapse", "collapse")
        //        .style("border", "2px black solid")

        //        .selectAll("tr")
        //        .data(parsedCSV)
        //        .enter().append("tr")

        //        .selectAll("td")
        //        .data(function (d) { return d; })
        //        .enter().append("td")
        //        .style("border", "1px black solid")
        //        .style("padding", "5px")
        //        .on("mouseover", function () { d3.select(this).style("background-color", "aliceblue") })
        //        .on("mouseout", function () { d3.select(this).style("background-color", "white") })
        //        .text(function (d) { return d; })
        //        .style("font-size", "12px");
        //});

    </script>
</body>

</html>
