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
                <a class="btn btn-default" href="#modalClearHistory" data-toggle="modal">Clear History</a>
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
                                                <i class="glyphicon glyphicon-map-marker"></i>
                                                <asp:Label ID="lblClientIP" runat="server" Text='<%# Eval("ClientIP") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" />
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
                            <div class="scroller" data-height="850px" data-always-visible="1" data-rail-visible1="1">
                                <table class="table table-striped table-bordered table-advance table-hover">
                                    <thead>
                                        <tr>
                                            <th>
                                                <i class="fa fa-briefcase"></i>&nbsp Users
                                            </th>
                                            <th class="hidden-xs">
                                                <i class="fa fa-question"></i>&nbsp Interactive content
                                            </th>
                                            <th>
                                                <i class="fa fa-clock-o"></i>&nbsp Time
                                            </th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rpInteractiveHistory" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <a href="#"><%# Eval("UserInt") %> </a>
                                                    </td>
                                                    <td class="hidden-xs"><%# Eval("InteractiveContent") %>
                                                    </td>
                                                    <td><%# Eval("Createdate") %>
                                                    </td>
                                                    <td>
                                                        <a class="btn default btn-xs green-stripe" href='<%# Eval("InteractiveLink") %>'>View </a>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <!--tab-pane-->
                </div>
            </div>
        </div>
    </div>
    <%-- End Row Content --%>
    <%-- Modal clear History data --%>
    <div id="modalClearHistory" class="modal fade modal-scroll" tabindex="-1" data-replace="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <i class="icon-trash"></i>&nbsp Clear History data
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-horizontal">
                                <div class="form-body">
                                    <%-- /Row --%>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="control-label col-md-8">Obliterate the following items from:</label>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="dlItemsFrom" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="1">the past hour</asp:ListItem>
                                                        <asp:ListItem Value="2">the past day</asp:ListItem>
                                                        <asp:ListItem Value="3">the past week</asp:ListItem>
                                                        <asp:ListItem Value="4">the last 4 weeks</asp:ListItem>
                                                        <asp:ListItem Value="5" Selected="True">the beginning of time</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <asp:CheckBox ID="chkLoginHistory" Text="Lịch sử đăng nhập" runat="server" />
                            </div>
                            <div class="form-group">
                                <asp:CheckBox ID="chkInteractiveHistory" Text="Lịch sử tương tác" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnDeleteHistory" CssClass="btn btn-warning" OnClick="btnDeleteHistory_Click"  runat="server" Text="Clear data" />
                    <a class="btn btn-default" data-dismiss="modal">Cancel</a>
                </div>
            </div>
        </div>
    </div>
    <%-- End Modal clear History data --%>
</asp:Content>
