﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Libra._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <div class="">

        <div class="row top_tiles">
            <div class="animated flipInY col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <div class="tile-stats">
                    <div class="icon">
                        <i class="fa fa-shopping-bag"></i>
                    </div>
                    <div class="count">&nbsp;</div>

                    <h3>Venda</h3>
                    <p>Realizar uma nova venda.</p>
                </div>
            </div>
            <div class="animated flipInY col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <div class="tile-stats">
                    <div class="icon">
                        <i class="fa fa-users"></i>
                    </div>
                    <div class="count">&nbsp;</div>

                    <h3>Clientes</h3>
                    <p>Consultar/Cadastrar clientes</p>
                </div>
            </div>
            <div class="animated flipInY col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <div class="tile-stats">
                    <div class="icon">
                        <i class="fa fa-server"></i>
                    </div>
                    <div class="count">&nbsp;</div>

                    <h3>Produtos</h3>
                    <p>Consulta de preços e estoque de produtos.</p>
                </div>
            </div>
            <div class="animated flipInY col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <div class="tile-stats">
                    <div class="icon">
                        <i class="fa fa-check-square-o"></i>
                    </div>
                    <div class="count">&nbsp;</div>

                    <h3>Caixa</h3>
                    <p>Consutar caixa.</p>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="x_panel">
                    <div class="x_title">
                        <h2>Transaction Summary <small>Weekly progress</small></h2>
                        <div class="filter">
                            <div id="reportrange" class="pull-right" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc">
                                <i class="glyphicon glyphicon-calendar fa fa-calendar"></i>
                                <span>December 30, 2014 - January 28, 2015</span> <b class="caret"></b>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <div class="col-md-9 col-sm-12 col-xs-12">
                            <div class="demo-container" style="height: 280px">
                                <div id="placeholder33x" class="demo-placeholder"></div>
                            </div>
                            <div class="tiles">
                                <div class="col-md-4 tile">
                                    <span>Total Sessions</span>
                                    <h2>231,809</h2>
                                    <span class="sparkline11 graph" style="height: 160px;">
                                        <canvas width="200" height="60" style="display: inline-block; vertical-align: top; width: 94px; height: 30px;"></canvas>
                                    </span>
                                </div>
                                <div class="col-md-4 tile">
                                    <span>Total Revenue</span>
                                    <h2>$231,809</h2>
                                    <span class="sparkline22 graph" style="height: 160px;">
                                        <canvas width="200" height="60" style="display: inline-block; vertical-align: top; width: 94px; height: 30px;"></canvas>
                                    </span>
                                </div>
                                <div class="col-md-4 tile">
                                    <span>Total Sessions</span>
                                    <h2>231,809</h2>
                                    <span class="sparkline11 graph" style="height: 160px;">
                                        <canvas width="200" height="60" style="display: inline-block; vertical-align: top; width: 94px; height: 30px;"></canvas>
                                    </span>
                                </div>
                            </div>

                        </div>

                        <div class="col-md-3 col-sm-12 col-xs-12">
                            <div>
                                <div class="x_title">
                                    <h2>Top Profiles</h2>
                                    <ul class="nav navbar-right panel_toolbox">
                                        <li><a href="#"><i class="fa fa-chevron-up"></i></a>
                                        </li>
                                        <li class="dropdown">
                                            <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false"><i class="fa fa-wrench"></i></a>
                                            <ul class="dropdown-menu" role="menu">
                                                <li><a href="#">Settings 1</a>
                                                </li>
                                                <li><a href="#">Settings 2</a>
                                                </li>
                                            </ul>
                                        </li>
                                        <li><a href="#"><i class="fa fa-close"></i></a>
                                        </li>
                                    </ul>
                                    <div class="clearfix"></div>
                                </div>
                                <ul class="list-unstyled top_profiles scroll-view">
                                    <li class="media event">
                                        <a class="pull-left border-aero profile_thumb">
                                            <i class="fa fa-user aero"></i>
                                        </a>
                                        <div class="media-body">
                                            <a class="title" href="#">Ms. Mary Jane</a>
                                            <p><strong>$2300. </strong>Agent Avarage Sales </p>
                                            <p>
                                                <small>12 Sales Today</small>
                                            </p>
                                        </div>
                                    </li>
                                    <li class="media event">
                                        <a class="pull-left border-green profile_thumb">
                                            <i class="fa fa-user green"></i>
                                        </a>
                                        <div class="media-body">
                                            <a class="title" href="#">Ms. Mary Jane</a>
                                            <p><strong>$2300. </strong>Agent Avarage Sales </p>
                                            <p>
                                                <small>12 Sales Today</small>
                                            </p>
                                        </div>
                                    </li>
                                    <li class="media event">
                                        <a class="pull-left border-blue profile_thumb">
                                            <i class="fa fa-user blue"></i>
                                        </a>
                                        <div class="media-body">
                                            <a class="title" href="#">Ms. Mary Jane</a>
                                            <p><strong>$2300. </strong>Agent Avarage Sales </p>
                                            <p>
                                                <small>12 Sales Today</small>
                                            </p>
                                        </div>
                                    </li>
                                    <li class="media event">
                                        <a class="pull-left border-aero profile_thumb">
                                            <i class="fa fa-user aero"></i>
                                        </a>
                                        <div class="media-body">
                                            <a class="title" href="#">Ms. Mary Jane</a>
                                            <p><strong>$2300. </strong>Agent Avarage Sales </p>
                                            <p>
                                                <small>12 Sales Today</small>
                                            </p>
                                        </div>
                                    </li>
                                    <li class="media event">
                                        <a class="pull-left border-green profile_thumb">
                                            <i class="fa fa-user green"></i>
                                        </a>
                                        <div class="media-body">
                                            <a class="title" href="#">Ms. Mary Jane</a>
                                            <p><strong>$2300. </strong>Agent Avarage Sales </p>
                                            <p>
                                                <small>12 Sales Today</small>
                                            </p>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>

    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server">
    <!-- chart js -->
    <script src="Scripts/chartjs/chart.min.js"></script>
    <!-- sparkline -->
    <script src="Scripts/sparkline/jquery.sparkline.min.js"></script>
    <!-- flot js -->
  <!--[if lte IE 8]><script type="text/javascript" src="js/excanvas.min.js"></script><![endif]-->
  <script type="text/javascript" src="Scripts/flot/jquery.flot.js"></script>
  <script type="text/javascript" src="Scripts/flot/jquery.flot.pie.js"></script>
  <script type="text/javascript" src="Scripts/flot/jquery.flot.orderBars.js"></script>
  <script type="text/javascript" src="Scripts/flot/jquery.flot.time.min.js"></script>
  <script type="text/javascript" src="Scripts/flot/date.js"></script>
  <script type="text/javascript" src="Scripts/flot/jquery.flot.spline.js"></script>
  <script type="text/javascript" src="Scripts/flot/jquery.flot.stack.js"></script>
  <script type="text/javascript" src="Scripts/flot/curvedLines.js"></script>
  <script type="text/javascript" src="Scripts/flot/jquery.flot.resize.js"></script>
    <script type="text/javascript">
        //define chart clolors ( you maybe add more colors if you want or flot will add it automatic )
        var chartColours = ['#96CA59', '#3F97EB', '#72c380', '#6f7a8a', '#f7cb38', '#5a8022', '#2c7282'];

        //generate random number for charts
        randNum = function () {
            return (Math.floor(Math.random() * (1 + 40 - 20))) + 20;
        }

        $(function () {
            var d1 = [];
            //var d2 = [];

            //here we generate data for chart
            for (var i = 0; i < 30; i++) {
                d1.push([new Date(Date.today().add(i).days()).getTime(), randNum() + i + i + 10]);
                //    d2.push([new Date(Date.today().add(i).days()).getTime(), randNum()]);
            }

            var chartMinDate = d1[0][0]; //first day
            var chartMaxDate = d1[20][0]; //last day

            var tickSize = [1, "day"];
            var tformat = "%d/%m/%y";

            //graph options
            var options = {
                grid: {
                    show: true,
                    aboveData: true,
                    color: "#3f3f3f",
                    labelMargin: 10,
                    axisMargin: 0,
                    borderWidth: 0,
                    borderColor: null,
                    minBorderMargin: 5,
                    clickable: true,
                    hoverable: true,
                    autoHighlight: true,
                    mouseActiveRadius: 100
                },
                series: {
                    lines: {
                        show: true,
                        fill: true,
                        lineWidth: 2,
                        steps: false
                    },
                    points: {
                        show: true,
                        radius: 4.5,
                        symbol: "circle",
                        lineWidth: 3.0
                    }
                },
                legend: {
                    position: "ne",
                    margin: [0, -25],
                    noColumns: 0,
                    labelBoxBorderColor: null,
                    labelFormatter: function (label, series) {
                        // just add some space to labes
                        return label + '&nbsp;&nbsp;';
                    },
                    width: 40,
                    height: 1
                },
                colors: chartColours,
                shadowSize: 0,
                tooltip: true, //activate tooltip
                tooltipOpts: {
                    content: "%s: %y.0",
                    xDateFormat: "%d/%m",
                    shifts: {
                        x: -30,
                        y: -50
                    },
                    defaultTheme: false
                },
                yaxis: {
                    min: 0
                },
                xaxis: {
                    mode: "time",
                    minTickSize: tickSize,
                    timeformat: tformat,
                    min: chartMinDate,
                    max: chartMaxDate
                }
            };
            var plot = $.plot($("#placeholder33x"), [{
                label: "Email Sent",
                data: d1,
                lines: {
                    fillColor: "rgba(150, 202, 89, 0.12)"
                }, //#96CA59 rgba(150, 202, 89, 0.42)
                points: {
                    fillColor: "#fff"
                }
            }], options);
        });
  </script>
    <!-- /flot -->
    <!--  -->
    <script>
        $('document').ready(function () {
            $(".sparkline_one").sparkline([2, 4, 3, 4, 5, 4, 5, 4, 3, 4, 5, 6, 4, 5, 6, 3, 5, 4, 5, 4, 5, 4, 3, 4, 5, 6, 7, 5, 4, 3, 5, 6], {
                type: 'bar',
                height: '125',
                barWidth: 13,
                colorMap: {
                    '7': '#a1a1a1'
                },
                barSpacing: 2,
                barColor: '#26B99A'
            });

            $(".sparkline11").sparkline([2, 4, 3, 4, 5, 4, 5, 4, 3, 4, 6, 2, 4, 3, 4, 5, 4, 5, 4, 3], {
                type: 'bar',
                height: '40',
                barWidth: 8,
                colorMap: {
                    '7': '#a1a1a1'
                },
                barSpacing: 2,
                barColor: '#26B99A'
            });

            $(".sparkline22").sparkline([2, 4, 3, 4, 7, 5, 4, 3, 5, 6, 2, 4, 3, 4, 5, 4, 5, 4, 3, 4, 6], {
                type: 'line',
                height: '40',
                width: '200',
                lineColor: '#26B99A',
                fillColor: '#ffffff',
                lineWidth: 3,
                spotColor: '#34495E',
                minSpotColor: '#34495E'
            });

            var doughnutData = [{
                value: 30,
                color: "#455C73"
            }, {
                value: 30,
                color: "#9B59B6"
            }, {
                value: 60,
                color: "#BDC3C7"
            }, {
                value: 100,
                color: "#26B99A"
            }, {
                value: 120,
                color: "#3498DB"
            }];

            Chart.defaults.global.legend = {
                enabled: false
            };

            var canvasDoughnut = new Chart(document.getElementById("canvas1i"), {
                type: 'doughnut',
                showTooltips: false,
                tooltipFillColor: "rgba(51, 51, 51, 0.55)",
                data: {
                    labels: [
                      "Symbian",
                      "Blackberry",
                      "Other",
                      "Android",
                      "IOS"
                    ],
                    datasets: [{
                        data: [15, 20, 30, 10, 30],
                        backgroundColor: [
                          "#BDC3C7",
                          "#9B59B6",
                          "#455C73",
                          "#26B99A",
                          "#3498DB"
                        ],
                        hoverBackgroundColor: [
                          "#CFD4D8",
                          "#B370CF",
                          "#34495E",
                          "#36CAAB",
                          "#49A9EA"
                        ]

                    }]
                }
            });

            var canvasDoughnut = new Chart(document.getElementById("canvas1i2"), {
                type: 'doughnut',
                showTooltips: false,
                tooltipFillColor: "rgba(51, 51, 51, 0.55)",
                data: {
                    labels: [
                      "Symbian",
                      "Blackberry",
                      "Other",
                      "Android",
                      "IOS"
                    ],
                    datasets: [{
                        data: [15, 20, 30, 10, 30],
                        backgroundColor: [
                          "#BDC3C7",
                          "#9B59B6",
                          "#455C73",
                          "#26B99A",
                          "#3498DB"
                        ],
                        hoverBackgroundColor: [
                          "#CFD4D8",
                          "#B370CF",
                          "#34495E",
                          "#36CAAB",
                          "#49A9EA"
                        ]

                    }]
                }
            });

            var canvasDoughnut = new Chart(document.getElementById("canvas1i3"), {
                type: 'doughnut',
                showTooltips: false,
                tooltipFillColor: "rgba(51, 51, 51, 0.55)",
                data: {
                    labels: [
                      "Symbian",
                      "Blackberry",
                      "Other",
                      "Android",
                      "IOS"
                    ],
                    datasets: [{
                        data: [15, 20, 30, 10, 30],
                        backgroundColor: [
                          "#BDC3C7",
                          "#9B59B6",
                          "#455C73",
                          "#26B99A",
                          "#3498DB"
                        ],
                        hoverBackgroundColor: [
                          "#CFD4D8",
                          "#B370CF",
                          "#34495E",
                          "#36CAAB",
                          "#49A9EA"
                        ]

                    }]
                }
            });
        });
  </script>
    <!-- -->
    <!-- datepicker -->
    <script type="text/javascript">
        $(document).ready(function () {

            var cb = function (start, end, label) {
                console.log(start.toISOString(), end.toISOString(), label);
                $('#reportrange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
                //alert("Callback has fired: [" + start.format('MMMM D, YYYY') + " to " + end.format('MMMM D, YYYY') + ", label = " + label + "]");
            }

            var optionSet1 = {
                startDate: moment().subtract(29, 'days'),
                endDate: moment(),
                minDate: '01/01/2012',
                maxDate: '12/31/2015',
                dateLimit: {
                    days: 60
                },
                showDropdowns: true,
                showWeekNumbers: true,
                timePicker: false,
                timePickerIncrement: 1,
                timePicker12Hour: true,
                ranges: {
                    'Today': [moment(), moment()],
                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                },
                opens: 'left',
                buttonClasses: ['btn btn-default'],
                applyClass: 'btn-small btn-primary',
                cancelClass: 'btn-small',
                format: 'MM/DD/YYYY',
                separator: ' to ',
                locale: {
                    applyLabel: 'Submit',
                    cancelLabel: 'Clear',
                    fromLabel: 'From',
                    toLabel: 'To',
                    customRangeLabel: 'Custom',
                    daysOfWeek: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
                    monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
                    firstDay: 1
                }
            };
            $('#reportrange span').html(moment().subtract(29, 'days').format('MMMM D, YYYY') + ' - ' + moment().format('MMMM D, YYYY'));
            $('#reportrange').daterangepicker(optionSet1, cb);
            $('#reportrange').on('show.daterangepicker', function () {
                console.log("show event fired");
            });
            $('#reportrange').on('hide.daterangepicker', function () {
                console.log("hide event fired");
            });
            $('#reportrange').on('apply.daterangepicker', function (ev, picker) {
                console.log("apply event fired, start/end dates are " + picker.startDate.format('MMMM D, YYYY') + " to " + picker.endDate.format('MMMM D, YYYY'));
            });
            $('#reportrange').on('cancel.daterangepicker', function (ev, picker) {
                console.log("cancel event fired");
            });
            $('#options1').click(function () {
                $('#reportrange').data('daterangepicker').setOptions(optionSet1, cb);
            });
            $('#options2').click(function () {
                $('#reportrange').data('daterangepicker').setOptions(optionSet2, cb);
            });
            $('#destroy').click(function () {
                $('#reportrange').data('daterangepicker').remove();
            });
        });
  </script>
    <!-- /datepicker -->
</asp:Content>
