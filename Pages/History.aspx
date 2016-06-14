<%@ Page Title="" Language="C#" MasterPageFile="~/GlobalMasterPage.master" AutoEventWireup="true" CodeFile="History.aspx.cs" Inherits="Pages_History" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- BEGIN PAGE HEADER-->
    <h1 class="page-title">History
    </h1>
    <div class="page-bar">
        <ul class="page-breadcrumb">
            <li>
                <i class="fa fa-home"></i>
                <a href="../Default.aspx">Home</a>
                <i class="fa fa-angle-right"></i>
            </li>
            <li>
                <a href="#">Setting</a>
                <i class="fa fa-angle-right"></i>
            </li>
            <li>
                <a href="../Pages/History.aspx">History</a>
            </li>
        </ul>
    </div>
    <%-- Pages is Valid --%>
    <div class="row">
        <div class="col-lg-12">
            <div class="alert alert-danger display-none" id="alertPageValid" runat="server">
                <asp:Label ID="lblPageValid" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <%--End Pages is Valid --%>
    <%-- Row Content --%>
    <div class="row">
        <div class="col-lg-12">
            <div class="form-group">
                <a class="btn btn-default">Clear History</a>
            </div>
            <div class="tabbable tabbable-custom tabbable-custom-profile">
                <ul class="nav nav-tabs">
                    <li class="active">
                        <a href="#tab_1_11" data-toggle="tab">History Login </a>
                    </li>
                    <li>
                        <a href="#tab_1_22" data-toggle="tab">Feeds </a>
                    </li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane active" id="tab_1_11">
                        <div class="tab-pane active" id="tab_1_1_1">
                            <div class="scroller" data-height="850px" data-always-visible="1" data-rail-visible1="1">
                                <%--<ul class="feeds">
                                    <asp:Repeater ID="rpHistoryLogin" runat="server">
                                        <ItemTemplate>
                                            <li>
                                                <div class="col1">
                                                    <div class="cont">
                                                        <div class="cont-col1">
                                                            <div class="label label-success">
                                                                <i class="fa fa-user"></i>
                                                            </div>
                                                        </div>
                                                        <div class="cont-col2">
                                                            <div class="desc">
                                                                <asp:Label ID="lblUserLogin" runat="server" Text='<%# Eval("UserName")+"_"+Eval("EmployeesCode") %>'></asp:Label>
                                                        </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col2">
                                                    <div class="datetime">
                                                        <i class="fa fa-clock-o"></i>
                                                        <asp:Label ID="lblDateOfLogin" runat="server" Text='<%# Eval("DateOfLogin") %>'></asp:Label>
                                                    </div>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>--%>
                                <asp:GridView ID="gwHistoryLogin" CssClass="table table-bordered" AutoGenerateColumns="False" ShowHeader="False" runat="server">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div class="label label-success">
                                                    <i class="fa fa-user"></i>
                                                </div>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUserLogin" runat="server" Text='<%# Eval("UserName")+"_"+Eval("EmployeesCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <i class="fa fa-clock-o"></i>
                                                <asp:Label ID="lblDateOfLogin" runat="server" Text='<%# Eval("DateOfLogin") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle BackColor="#FAF3DF"></RowStyle>
                                    <SelectedRowStyle BackColor="#79B782" ForeColor="Black" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <!--tab-pane-->
                    <div class="tab-pane" id="tab_1_22">
                        <div class="portlet-body">
                            <table class="table table-striped table-bordered table-advance table-hover">
                                <thead>
                                    <tr>
                                        <th>
                                            <i class="fa fa-briefcase"></i>Company
                                        </th>
                                        <th class="hidden-xs">
                                            <i class="fa fa-question"></i>Descrition
                                        </th>
                                        <th>
                                            <i class="fa fa-bookmark"></i>Amount
                                        </th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <a href="#">Pixel Ltd </a>
                                        </td>
                                        <td class="hidden-xs">Server hardware purchase
                                        </td>
                                        <td>52560.10$ <span class="label label-success label-sm">Paid </span>
                                        </td>
                                        <td>
                                            <a class="btn default btn-xs green-stripe" href="#">View </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <a href="#">Smart House </a>
                                        </td>
                                        <td class="hidden-xs">Office furniture purchase
                                        </td>
                                        <td>5760.00$ <span class="label label-warning label-sm">Pending </span>
                                        </td>
                                        <td>
                                            <a class="btn default btn-xs blue-stripe" href="#">View </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <a href="#">FoodMaster Ltd </a>
                                        </td>
                                        <td class="hidden-xs">Company Anual Dinner Catering
                                        </td>
                                        <td>12400.00$ <span class="label label-success label-sm">Paid </span>
                                        </td>
                                        <td>
                                            <a class="btn default btn-xs blue-stripe" href="#">View </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <a href="#">WaterPure Ltd </a>
                                        </td>
                                        <td class="hidden-xs">Payment for Jan 2013
                                        </td>
                                        <td>610.50$ <span class="label label-danger label-sm">Overdue </span>
                                        </td>
                                        <td>
                                            <a class="btn default btn-xs red-stripe" href="#">View </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <a href="#">Pixel Ltd </a>
                                        </td>
                                        <td class="hidden-xs">Server hardware purchase
                                        </td>
                                        <td>52560.10$ <span class="label label-success label-sm">Paid </span>
                                        </td>
                                        <td>
                                            <a class="btn default btn-xs green-stripe" href="#">View </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <a href="#">Smart House </a>
                                        </td>
                                        <td class="hidden-xs">Office furniture purchase
                                        </td>
                                        <td>5760.00$ <span class="label label-warning label-sm">Pending </span>
                                        </td>
                                        <td>
                                            <a class="btn default btn-xs blue-stripe" href="#">View </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <a href="#">FoodMaster Ltd </a>
                                        </td>
                                        <td class="hidden-xs">Company Anual Dinner Catering
                                        </td>
                                        <td>12400.00$ <span class="label label-success label-sm">Paid </span>
                                        </td>
                                        <td>
                                            <a class="btn default btn-xs blue-stripe" href="#">View </a>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <!--tab-pane-->
                </div>
            </div>
        </div>
    </div>
    <%-- End Row Content --%>
</asp:Content>

