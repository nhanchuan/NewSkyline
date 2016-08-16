<%@ Page Title="" Language="C#" MasterPageFile="~/GlobalMasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- BEGIN PAGE HEADER-->
    <h3 class="page-title">Dashboard <small>reports & statistics</small>
    </h3>
    <div class="page-bar">
        <ul class="page-breadcrumb">
            <li>
                <i class="fa fa-home"></i>
                <a href="../Default.aspx">Home</a>
                <i class="fa fa-angle-right"></i>
            </li>
            <li>
                <a href="#">Dashboard</a>
            </li>
        </ul>
        <div class="page-toolbar">
            <div id="dashboard-report-range" class="pull-right tooltips btn btn-fit-height grey-salt" data-placement="top" data-original-title="Change dashboard date range">
                <i class="icon-calendar"></i>&nbsp;
						<span class="thin uppercase visible-lg-inline-block">&nbsp;</span>&nbsp;
						<i class="fa fa-angle-down"></i>
            </div>
        </div>
    </div>
    <!-- END PAGE HEADER-->
    <!-- BEGIN DASHBOARD STATS -->
    <div class="row">
        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
            <div class="dashboard-stat blue-madison">
                <div class="visual">
                    <i class="glyphicon glyphicon-briefcase"></i>
                </div>
                <div class="details">
                    <div class="number">
                        <asp:Label ID="lblSumBoHoSo" runat="server"></asp:Label>
                    </div>
                    <div class="desc">
                        Bộ Hồ Sơ
                    </div>
                </div>
                <a class="more" href="#">View more <i class="m-icon-swapright m-icon-white"></i>
                </a>
            </div>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
            <div class="dashboard-stat red-intense">
                <div class="visual">
                    <i class="fa fa-bar-chart-o"></i>
                </div>
                <div class="details">
                    <div class="number">
                        10,1M$
                    </div>
                    <div class="desc">
                        Total Profit
                    </div>
                </div>
                <a class="more" href="#">View more <i class="m-icon-swapright m-icon-white"></i>
                </a>
            </div>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
            <div class="dashboard-stat green-haze">
                <div class="visual">
                    <i class="fa fa-shopping-cart"></i>
                </div>
                <div class="details">
                    <div class="number">
                        <asp:Label ID="lblNumGhiDanh" runat="server" Text="Label"></asp:Label>
                    </div>
                    <div class="desc">
                        Học viên ghi danh
                    </div>
                </div>
                <a class="more" href="#">View more <i class="m-icon-swapright m-icon-white"></i>
                </a>
            </div>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
            <div class="dashboard-stat purple-plum">
                <div class="visual">
                    <i class="fa fa-globe"></i>
                </div>
                <div class="details">
                    <div class="number">
                        +89%
                    </div>
                    <div class="desc">
                        Brand Popularity
                    </div>
                </div>
                <a class="more" href="#">View more <i class="m-icon-swapright m-icon-white"></i>
                </a>
            </div>
        </div>
    </div>
    <!-- END DASHBOARD STATS -->
    <div class="clearfix">
    </div>

    <div class="row ">
        <div class="col-md-6 col-sm-6">
            <!-- BEGIN PORTLET-->
            <div class="portlet paddingless">
                <div class="portlet-title line">
                    <div class="caption">
                        <i class="fa fa-bell-o"></i>Feeds
                    </div>
                    <div class="tools">
                        <a href="" class="collapse"></a>
                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                        <a href="" class="reload"></a>
                        <a href="" class="remove"></a>
                    </div>
                </div>
                <div class="portlet-body">
                    <!--BEGIN TABS-->
                    <div class="tabbable tabbable-custom">
                        <ul class="nav nav-tabs">
                            <li class="active">
                                <a href="#tab_1_1" data-toggle="tab">System </a>
                            </li>
                            <li>
                                <a href="#tab_1_2" data-toggle="tab">TRUNG TÂM ANH NGỮ </a>
                            </li>
                            <li>
                                <a href="#tab_1_3" data-toggle="tab">QUẢN LÝ HỒ SƠ </a>
                            </li>
                        </ul>
                        <div class="tab-content">
                            <div class="tab-pane active" id="tab_1_1">
                                <div class="scroller" style="height: 560px;" data-always-visible="1" data-rail-visible="0">
                                    <ul class="feeds">
                                        <asp:Repeater ID="rptfeedssystem" runat="server">
                                            <ItemTemplate>
                                                <li>
                                                    <div class="row">
                                                        <div class="col-md-9">
                                                            <div class="row">
                                                                <div class="cont">
                                                                    <div class="col-md-1">
                                                                        <div class="label label-sm label-danger">
                                                                            <i class="fa fa-bolt"></i>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-11">
                                                                        <div class="row">
                                                                            <%# Eval("UserInt") %>  | <%# Eval("InteractiveContent") %>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 text-right">
                                                            <div class="date">
                                                                <%# Eval("Createdate") %>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </ul>
                                </div>
                            </div>
                            <div class="tab-pane" id="tab_1_2">
                                <div class="scroller" style="height: 560px;" data-always-visible="1" data-rail-visible1="1">
                                    <ul class="feeds">
                                        <li>
                                            <a href="#">
                                                <div class="col1">
                                                    <div class="cont">
                                                        <div class="cont-col1">
                                                            <div class="label label-sm label-success">
                                                                <i class="fa fa-bell-o"></i>
                                                            </div>
                                                        </div>
                                                        <div class="cont-col2">
                                                            <div class="desc">
                                                                New user registered
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col2">
                                                    <div class="date">
                                                        Just now
                                                    </div>
                                                </div>
                                            </a>
                                        </li>
                                        <li>
                                            <a href="#">
                                                <div class="col1">
                                                    <div class="cont">
                                                        <div class="cont-col1">
                                                            <div class="label label-sm label-success">
                                                                <i class="fa fa-bell-o"></i>
                                                            </div>
                                                        </div>
                                                        <div class="cont-col2">
                                                            <div class="desc">
                                                                New order received
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col2">
                                                    <div class="date">
                                                        10 mins
                                                    </div>
                                                </div>
                                            </a>
                                        </li>
                                        <li>
                                            <div class="col1">
                                                <div class="cont">
                                                    <div class="cont-col1">
                                                        <div class="label label-sm label-danger">
                                                            <i class="fa fa-bolt"></i>
                                                        </div>
                                                    </div>
                                                    <div class="cont-col2">
                                                        <div class="desc">
                                                            Order #24DOP4 has been rejected. <span class="label label-sm label-danger ">Take action <i class="fa fa-share"></i>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col2">
                                                <div class="date">
                                                    24 mins
                                                </div>
                                            </div>
                                        </li>
                                        <li>
                                            <a href="#">
                                                <div class="col1">
                                                    <div class="cont">
                                                        <div class="cont-col1">
                                                            <div class="label label-sm label-success">
                                                                <i class="fa fa-bell-o"></i>
                                                            </div>
                                                        </div>
                                                        <div class="cont-col2">
                                                            <div class="desc">
                                                                New user registered
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col2">
                                                    <div class="date">
                                                        Just now
                                                    </div>
                                                </div>
                                            </a>
                                        </li>
                                        <li>
                                            <a href="#">
                                                <div class="col1">
                                                    <div class="cont">
                                                        <div class="cont-col1">
                                                            <div class="label label-sm label-success">
                                                                <i class="fa fa-bell-o"></i>
                                                            </div>
                                                        </div>
                                                        <div class="cont-col2">
                                                            <div class="desc">
                                                                New user registered
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col2">
                                                    <div class="date">
                                                        Just now
                                                    </div>
                                                </div>
                                            </a>
                                        </li>
                                        <li>
                                            <a href="#">
                                                <div class="col1">
                                                    <div class="cont">
                                                        <div class="cont-col1">
                                                            <div class="label label-sm label-success">
                                                                <i class="fa fa-bell-o"></i>
                                                            </div>
                                                        </div>
                                                        <div class="cont-col2">
                                                            <div class="desc">
                                                                New user registered
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col2">
                                                    <div class="date">
                                                        Just now
                                                    </div>
                                                </div>
                                            </a>
                                        </li>
                                        <li>
                                            <a href="#">
                                                <div class="col1">
                                                    <div class="cont">
                                                        <div class="cont-col1">
                                                            <div class="label label-sm label-success">
                                                                <i class="fa fa-bell-o"></i>
                                                            </div>
                                                        </div>
                                                        <div class="cont-col2">
                                                            <div class="desc">
                                                                New user registered
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col2">
                                                    <div class="date">
                                                        Just now
                                                    </div>
                                                </div>
                                            </a>
                                        </li>
                                        <li>
                                            <a href="#">
                                                <div class="col1">
                                                    <div class="cont">
                                                        <div class="cont-col1">
                                                            <div class="label label-sm label-success">
                                                                <i class="fa fa-bell-o"></i>
                                                            </div>
                                                        </div>
                                                        <div class="cont-col2">
                                                            <div class="desc">
                                                                New user registered
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col2">
                                                    <div class="date">
                                                        Just now
                                                    </div>
                                                </div>
                                            </a>
                                        </li>
                                        <li>
                                            <a href="#">
                                                <div class="col1">
                                                    <div class="cont">
                                                        <div class="cont-col1">
                                                            <div class="label label-sm label-success">
                                                                <i class="fa fa-bell-o"></i>
                                                            </div>
                                                        </div>
                                                        <div class="cont-col2">
                                                            <div class="desc">
                                                                New user registered
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col2">
                                                    <div class="date">
                                                        Just now
                                                    </div>
                                                </div>
                                            </a>
                                        </li>
                                        <li>
                                            <a href="#">
                                                <div class="col1">
                                                    <div class="cont">
                                                        <div class="cont-col1">
                                                            <div class="label label-sm label-success">
                                                                <i class="fa fa-bell-o"></i>
                                                            </div>
                                                        </div>
                                                        <div class="cont-col2">
                                                            <div class="desc">
                                                                New user registered
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col2">
                                                    <div class="date">
                                                        Just now
                                                    </div>
                                                </div>
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                            <div class="tab-pane" id="tab_1_3">
                                <div class="scroller" style="height: 560px;" data-always-visible="1" data-rail-visible1="1">
                                    <div class="row">
                                        <div class="col-md-6 user-info">
                                            <img alt="" src="../../assets/admin/layout/img/avatar.png" class="img-responsive" />
                                            <div class="details">
                                                <div>
                                                    <a href="#">Robert Nilson </a>
                                                    <span class="label label-sm label-success label-mini">Approved </span>
                                                </div>
                                                <div>
                                                    29 Jan 2013 10:45AM
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6 user-info">
                                            <img alt="" src="../../assets/admin/layout/img/avatar.png" class="img-responsive" />
                                            <div class="details">
                                                <div>
                                                    <a href="#">Lisa Miller </a>
                                                    <span class="label label-sm label-info">Pending </span>
                                                </div>
                                                <div>
                                                    19 Jan 2013 10:45AM
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6 user-info">
                                            <img alt="" src="../../assets/admin/layout/img/avatar.png" class="img-responsive" />
                                            <div class="details">
                                                <div>
                                                    <a href="#">Eric Kim </a>
                                                    <span class="label label-sm label-info">Pending </span>
                                                </div>
                                                <div>
                                                    19 Jan 2013 12:45PM
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6 user-info">
                                            <img alt="" src="../../assets/admin/layout/img/avatar.png" class="img-responsive" />
                                            <div class="details">
                                                <div>
                                                    <a href="#">Lisa Miller </a>
                                                    <span class="label label-sm label-danger">In progress </span>
                                                </div>
                                                <div>
                                                    19 Jan 2013 11:55PM
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6 user-info">
                                            <img alt="" src="../../assets/admin/layout/img/avatar.png" class="img-responsive" />
                                            <div class="details">
                                                <div>
                                                    <a href="#">Eric Kim </a>
                                                    <span class="label label-sm label-info">Pending </span>
                                                </div>
                                                <div>
                                                    19 Jan 2013 12:45PM
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6 user-info">
                                            <img alt="" src="../../assets/admin/layout/img/avatar.png" class="img-responsive" />
                                            <div class="details">
                                                <div>
                                                    <a href="#">Lisa Miller </a>
                                                    <span class="label label-sm label-danger">In progress </span>
                                                </div>
                                                <div>
                                                    19 Jan 2013 11:55PM
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6 user-info">
                                            <img alt="" src="../../assets/admin/layout/img/avatar.png" class="img-responsive" />
                                            <div class="details">
                                                <div>
                                                    <a href="#">Eric Kim </a>
                                                    <span class="label label-sm label-info">Pending </span>
                                                </div>
                                                <div>
                                                    19 Jan 2013 12:45PM
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6 user-info">
                                            <img alt="" src="../../assets/admin/layout/img/avatar.png" class="img-responsive" />
                                            <div class="details">
                                                <div>
                                                    <a href="#">Lisa Miller </a>
                                                    <span class="label label-sm label-danger">In progress </span>
                                                </div>
                                                <div>
                                                    19 Jan 2013 11:55PM
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6 user-info">
                                            <img alt="" src="../../assets/admin/layout/img/avatar.png" class="img-responsive" />
                                            <div class="details">
                                                <div>
                                                    <a href="#">Eric Kim </a>
                                                    <span class="label label-sm label-info">Pending </span>
                                                </div>
                                                <div>
                                                    19 Jan 2013 12:45PM
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6 user-info">
                                            <img alt="" src="../../assets/admin/layout/img/avatar.png" class="img-responsive" />
                                            <div class="details">
                                                <div>
                                                    <a href="#">Lisa Miller </a>
                                                    <span class="label label-sm label-danger">In progress </span>
                                                </div>
                                                <div>
                                                    19 Jan 2013 11:55PM
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6 user-info">
                                            <img alt="" src="../../assets/admin/layout/img/avatar.png" class="img-responsive" />
                                            <div class="details">
                                                <div>
                                                    <a href="#">Eric Kim </a>
                                                    <span class="label label-sm label-info">Pending </span>
                                                </div>
                                                <div>
                                                    19 Jan 2013 12:45PM
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6 user-info">
                                            <img alt="" src="../../assets/admin/layout/img/avatar.png" class="img-responsive" />
                                            <div class="details">
                                                <div>
                                                    <a href="#">Lisa Miller </a>
                                                    <span class="label label-sm label-danger">In progress </span>
                                                </div>
                                                <div>
                                                    19 Jan 2013 11:55PM
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--END TABS-->
                </div>
            </div>
            <!-- END PORTLET-->
        </div>
        <div class="col-md-6 col-sm-6">
            <!-- BEGIN PORTLET-->
            <div class="portlet box blue-madison calendar">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-calendar"></i>Calendar
                    </div>
                </div>
                <div class="portlet-body light-grey">
                    <div id="calendar">
                    </div>
                </div>
            </div>
            <!-- END PORTLET-->
        </div>
    </div>

</asp:Content>

