<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="d3PieSampleControl.ascx.cs" Inherits="ApplicationContainer.UI.Web.Prototype.D3.Controls.d3PieSampleControl" %>


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
    </style>

    <script>

        var width = 960,
            height = 500,
            radius = Math.min(width, height) / 2;

        var color = d3.scale.ordinal()
            .range(["#98abc5", "#8a89a6", "#7b6888", "#6b486b", "#a05d56", "#d0743c", "#ff8c00"]);

        var arc = d3.svg.arc()
            .outerRadius(radius - 10)
            .innerRadius(0);

        var pie = d3.layout.pie()
            .sort(null)
            .value(function (d) { return d.TotalHoursWorked; });

        var svg = d3.select("#PieChart").append("svg:svg")
            .attr("width", width)
            .attr("height", height)
          .append("g")
            .attr("transform", "translate(" + width / 2 + "," + height / 2 + ")");
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
                
                var g = svg.selectAll(".arc")
                        .data(pie(dt))
                      .enter().append("g")
                        .attr("class", "arc");

                    g.append("path")
                        .attr("d", arc)
                        .style("fill", function (d) { return color(d.data.FirstName); });

                    g.append("text")
                        .attr("transform", function (d) { return "translate(" + arc.centroid(d) + ")"; })
                        .attr("dy", ".35em")
                        .style("text-anchor", "middle")
                        .text(function (d) { return d.data.FirstName; });
            }
        });

        //d3.csv("myFile.csv", function (error, data) {

        //    data.forEach(function (d) {
        //        d.TotalHoursWorked = +d.TotalHoursWorked;
        //    });

        //    var g = svg.selectAll(".arc")
        //        .data(pie(data))
        //      .enter().append("g")
        //        .attr("class", "arc");

        //    g.append("path")
        //        .attr("d", arc)
        //        .style("fill", function (d) { return color(d.data.FirstName); });

        //    g.append("text")
        //        .attr("transform", function (d) { return "translate(" + arc.centroid(d) + ")"; })
        //        .attr("dy", ".35em")
        //        .style("text-anchor", "middle")
        //        .text(function (d) { return d.data.FirstName; });

        //});

    </script>

    <div id="PieChart"></div>
</div>
