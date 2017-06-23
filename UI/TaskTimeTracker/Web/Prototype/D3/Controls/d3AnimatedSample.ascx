<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="d3AnimatedSample.ascx.cs" Inherits="ApplicationContainer.UI.Web.Prototype.D3.Controls.d3AnimatedSample" %>


<div id="borderdiv">
    <style>
        #tooltip
        {
            position: absolute;
            width: 90px;
            height: auto;
            padding: 10px;
            background-color: white;
            -webkit-border-radius: 10px;
            -moz-border-radius: 10px;
            border-radius: 10px;
            -webkit-box-shadow: 4px 4px 10px rgba(0, 0, 0, 0.4);
            -moz-box-shadow: 4px 4px 10px rgba(0, 0, 0, 0.4);
            box-shadow: 4px 4px 10px rgba(0, 0, 0, 0.4);
            pointer-events: none;
        }

            #tooltip.hidden
            {
                display: none;
            }

            #tooltip p
            {
                margin: 0;
                font-family: sans-serif;
                font-size: 12px;
                line-height: 16px;
            }

        .axis path,
        .axis line
        {
            fill: none;
            stroke: black;
            shape-rendering: crispEdges;
        }

        .axis text
        {
            font-family: sans-serif;
            font-size: 11px;
        }

        rect
        {
            -moz-transition: all 0.3s;
            -webkit-transition: all 0.3s;
            -o-transition: all 0.3s;
            transition: all 0.3s;
        }

            rect:hover
            {
                fill: orange;
            }
    </style>

    <script>
        var margin = { top: 40, right: 30, bottom: 30, left: 80 };
        var w = 900 - margin.left - margin.right;
        var h = 500 - margin.top - margin.bottom;

        var x = d3.scale.ordinal()
        .rangeRoundBands([0, w], 0.05);

        var y = d3.scale.linear()
                        .domain([300, 1100])
                        .range([h, 0]);

        var xAxis = d3.svg.axis()
                    .scale(x)
                    .orient("bottom");
        //.tickFormat(formatPercent);
        // create left yAxis
        var yAxis = d3.svg.axis().scale(y).orient("left").ticks(10);

        var svg = d3.select("#AnimatedChart")
                            .append("svg")
             .attr("width", w + margin.left + margin.right)
            .attr("height", h + margin.top + margin.bottom)
        .append("g")
        .attr("transform", "translate(" + margin.left + "," + margin.top + ")");
        //.attr("width", w)
        //.attr("height", h).append("g");

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
                y.domain([0, d3.max(dt, function (d) { return d.TotalHoursWorked; })]);

                ////Create SVG element
                //var key = function (d) {
                //    return d.FirstName;
                //};

                svg.append("g")
               .attr("class", "x axis")
               //.append("text")
               .attr("transform", "translate(0," + h + ")")
               //.text("Names") 
               .call(xAxis).selectAll("text")
		.style("text-anchor", "end")
		.attr("dx", "-.8em")
		.attr("dy", ".15em")
		.attr("transform", function (d) {
		    return "rotate(-25)";
		}) ;

                svg.append("g")
                    .attr("class", "y axis")
                    .attr("transform", "translate(0,0)")
                    .call(yAxis)
                    .append("text")                 
                    .text("TotalHoursWorked");

                bars = svg.selectAll("rect").data(dt).enter();

                bars.append("rect")
                    .attr("x", function (d) { return x(d.FirstName); })
                    .attr("y", function (d) { return y(d.TotalHoursWorked); })

                   .attr("width", x.rangeBand() / 2)
                   .attr("height", function (d, i, j) {
                       return h - y(d.TotalHoursWorked);
                   })
                   .attr("fill", function (d) {
                       return "rgb(0, 0, " + (d.TotalHoursWorked * 10) + ")";
                   })

                    //Tooltip                   
                .on("mouseover", function (d, i) {
                    //Get this bar's x/y values, then augment for the tooltip
                    var xPosition = parseFloat(x(i) + x.rangeBand());
                    var yPosition = h / 2;

                    //Update Tooltip Position & value
                    d3.select("#tooltip")
                        .style("left", xPosition + "px")
                        .style("top", yPosition + "px")
                        .select("#keyword")
                        .text(d.TotalHoursWorked);
                    d3.select("#tooltip").classed("hidden", false);
                })
                .on("mouseout", function () {
                    //Remove the tooltip
                    d3.select("#tooltip").classed("hidden", true);
                });

                //Create labels
                     bars.append("text")
                   .attr("x", function (d, i) {
                       return x(i) + x.rangeBand() / 4;
                   })
                   .attr("y", function (d) {
                       return  y(d.TotalHoursWorked) + 20;
                   })
                .attr("text-anchor", "middle")
               
                .attr("font-family", "sans-serif")
                .attr("font-size", "12px")
                .attr("fill", "white")
	            .text(function (d) {
	                return d.TotalHoursWorked;
	            });
                
            }
        });


    </script>

    <br />
    <div id="AnimatedChart"></div>
    <div id="tooltip" class="hidden">
        <p class="heading"><span id="keyword">TotalHoursWorked</span></p>
    </div>
</div>
