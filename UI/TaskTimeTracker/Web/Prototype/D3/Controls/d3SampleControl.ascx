<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="d3SampleControl.ascx.cs" Inherits="ApplicationContainer.UI.Web.Prototype.D3.Controls.d3SampleControl" %>

<div id="borderdiv">
    
    <style>
        .arc path
        {
            stroke: #fff;
        }

        body
        {
            font: 14px sans-serif;
        }

        .y.axisRight text
        {
            fill: orange;
        }

        .y.axisLeft text
        {
            fill: steelblue;
        }

        .axis path,
        .axis line
        {
            fill: none;
            stroke: #000;
            shape-rendering: crispEdges;
        }

        .bar1
        {
            fill: steelblue;
        }

        .bar2
        {
            fill: orange;
        }

        .x.axis path
        {
            display: none;
        }

        .bar
        {
            fill: steelblue;
        }

            .bar:hover
            {
                fill: brown;
            }

        .chart rect
        {
            fill: steelblue;
        }

        .chart text
        {
            fill: white;
            font: 10px sans-serif;
            text-anchor: end;
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

    <script>
        var margin = { top: 40, right: 30, bottom: 30, left: 80 };
        var width = 900 - margin.left - margin.right;
        var height = 500 - margin.top - margin.bottom;

        var x = d3.scale.ordinal().rangeRoundBands([0, width], .01);
        var y0 = d3.scale.linear().domain([300, 1100]).range([height, 0]);

        var xAxis = d3.svg.axis()
            .scale(x)
            .orient("bottom");

        // create left yAxis
        var yAxisLeft = d3.svg.axis().scale(y0).orient("left").ticks(10);
        // create right yAxis
        //var yAxisRight = d3.svg.axis().scale(y1).ticks(6).orient("right");

        var svg = d3.select("#SampleChart").append("svg:svg")
            .attr("width", width + margin.left + margin.right)
            .attr("height", height + margin.top + margin.bottom)
          .append("g")
            .attr("class", "graph")
            .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

        $.ajax({
            type: "POST",
            url: "http://localhost:53331/API/AutoComplete.asmx/GetScheduleData",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (data) {  
                var jSonData = data.d;
                var results,
                dt = [];
                results = d3.map(jSonData);

                results.forEach(function (key, val) {
                    var result = {};
                    result.FirstName = val.FirstName;
                    result.TotalHoursWorked = parseFloat(val.TotalHoursWorked);
                    dt.push(result);
                });

                x.domain(dt.map(function (d) { return d.FirstName; }));
                y0.domain([0, d3.max(dt, function (d) { return d.TotalHoursWorked; })]);

                svg.append("g")
                    .attr("class", "x axis")
                    .attr("transform", "translate(0," + height + ")")
                    .call(xAxis);

                svg.append("g")
                    .attr("class", "y axis axisLeft")
                    .attr("transform", "translate(0,0)")
                    .call(yAxisLeft)
                    .append("text")
                    .text("TotalHoursWorked");

                bars = svg.selectAll(".bar").data(dt).enter();

                bars.append("rect")
                 .attr("class", "bar1")
                 .attr("x", function (d) { return x(d.FirstName); })
                 .attr("width", x.rangeBand() / 2)
                 .attr("y", function (d) { return y0(d.TotalHoursWorked); })
                 .attr("height", function (d, i, j) { return height - y0(d.TotalHoursWorked); });
            }

        });

    </script>

    <div id="SampleChart"></div>

</div>
