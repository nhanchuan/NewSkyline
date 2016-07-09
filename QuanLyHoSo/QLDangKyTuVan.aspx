<%@ Page Title="" Language="C#" MasterPageFile="~/GlobalMasterPage.master" AutoEventWireup="true" CodeFile="QLDangKyTuVan.aspx.cs" Inherits="QuanLyHoSo_QLDangKyTuVan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../assets/admin/pages/css/profile-old.css" rel="stylesheet" />
    <link href="../App_Themes/admin/StylePortlet.css" rel="stylesheet" />
    <!-- BEGIN PAGE HEADER-->
    <h1 class="page-title">Quản Lý Đăng ký tư vấn <small>Registration Form Advisory Manager</small>
    </h1>
    <div class="page-bar">
        <ul class="page-breadcrumb">
            <li>
                <i class="fa fa-home"></i>
                <a href="../Default.aspx">Home</a>
                <i class="fa fa-angle-right"></i>
            </li>
            <li>
                <a href="../QuanLyHoSo/QLDangKyTuVan.aspx">QL Đăng ký tư vấn</a>
            </li>
        </ul>
    </div>
    <!-- END PAGE HEADER-->
    <div class="row">
        <div class="col-md-8 profile-info">
            <h1>
                <asp:Label ID="lblUserFullName" runat="server" Text="Nguyễn Nhân Chuẩn"></asp:Label>
            </h1>
            <asp:Label ID="lblregency" CssClass="control-label label-default" Text="Nhân Viên Tư Vấn" runat="server"></asp:Label><br />
            <asp:Label ID="lblMaNV" CssClass="bold" runat="server" Text="Label"></asp:Label>
            <p>
                <i class="fa fa-globe" style="color: dodgerblue;"></i>
                <a href="#">
                    <asp:Label ID="lblmywebsite" runat="server" Text="Label"></asp:Label>
                </a>
            </p>
            <ul class="list-inline">
                <li>
                    <i class="fa fa-map-marker"></i>
                    <asp:Label ID="lbladdress" runat="server" Text="Label"></asp:Label>
                </li>
                <li>
                    <i class="fa fa-calendar"></i>
                    <asp:Label ID="lblBirthday" runat="server" Text="Label"></asp:Label>
                </li>
                <li>
                    <i class="fa fa-briefcase"></i>
                    <asp:Label ID="lblOccupation" runat="server" Text="Label"></asp:Label>
                </li>
                <li>
                    <i class="fa fa-star"></i>Top Seller
                </li>
                <li>
                    <i class="fa fa-heart"></i>BASE Jumping
                </li>
            </ul>
        </div>
        <!--end col-md-8-->
        <div class="col-md-4">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="portlet sale-summary">
                        <div class="portlet-title">
                            <div class="caption bold">
                                Tóm tắt tư vấn
                            </div>
                            <div class="tools">
                                <a class="reload" href="javascript:;" onclick="reloadclick()"></a>
                            </div>
                        </div>
                        <div class="portlet-body">

                            <ul class="list-unstyled">
                                <li>
                                    <span class="sale-info">Trong ngày <i class="fa fa-img-up"></i>
                                    </span>
                                    <span class="sale-num">
                                        <asp:Label ID="lblDaySum" runat="server" Text="Label"></asp:Label>
                                    </span>
                                </li>
                                <li>
                                    <span class="sale-info">Trong tháng <i class="fa fa-img-down"></i>
                                    </span>
                                    <span class="sale-num">
                                        <asp:Label ID="lblMonthSum" runat="server" Text="Label"></asp:Label>
                                    </span>
                                </li>
                                <li>
                                    <span class="sale-info">Tổng số </span>
                                    <span class="sale-num">
                                        <asp:Label ID="lblTotalSum" runat="server" Text="Label"></asp:Label>
                                    </span>
                                </li>
                            </ul>

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!--end col-md-4-->
    </div>
    <%-- END ROW --%>
    <div class="clearfix"></div>
    <div class="row">
        <div class="col-lg-12 margin-bottom-20">
            <a class="btn green" href="../QuanLyHoSo/PhieuDangKyTuVan.aspx"><i class="fa fa-arrow-left fa-fw"></i>Phiếu Đăng Ký Tư Vấn</a>
        </div>

        <div class="clearfix"></div>
        <div class="col-lg-12">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <!-- BEGIN TABLE PORTLET-->
                    <div class="portlet box blue">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class="fa fa-edit"></i>Danh sách Phiếu đã chuyển
                            </div>

                            <div class="tools">
                                <a href="javascript:;" class="collapse"></a>
                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                <%--<button id="btnreload" class="btn green" onserverclick="btnreload_ServerClick" runat="server"><i class="fa fa-refresh"></i></button>--%>
                                <a href="javascript:;" class="remove"></a>
                            </div>
                            <div class="actions">
                                <a data-toggle="modal" href="#modalExport2Excel" class="btn btn-default btn-sm">
                                    <i class="fa fa-file-excel-o"></i>&nbsp Export to Excel </a>
                            </div>
                        </div>
                        <div class="portlet-body background">
                            <div class="panel panel-primary">
                                <div class="panel-body">
                                    <div class="col-lg-1">
                                        <asp:Button ID="btnSelectAll" CssClass="btn orange" OnClick="btnSelectAll_Click" runat="server" Text="Select All" />
                                        <asp:Button ID="btnUncheckAll" CssClass="btn orange" OnClick="btnUncheckAll_Click" runat="server" Text="Uncheck All" />
                                    </div>
                                    <div class="col-lg-2">
                                        <a href="#modalChangeUserAdv" data-toggle="modal" class="btn btn-warning"><i class="fa fa-reply"></i>Chuyển Phiếu Tư Vấn</a>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="input-group">
                                            <div class="input-icon">
                                                <i class="fa fa-search"></i>
                                                <input id="txtsearchAdv" class="form-control" type="text" placeholder="Tìm kiếm phiếu tư vấn" runat="server" />
                                            </div>
                                            <span class="input-group-btn">
                                                <button id="btnsearchAdv" class="btn btn-success" type="button" onserverclick="btnsearchAdv_ServerClick" runat="server"><i class="fa fa-arrow-left fa-fw"></i>Search</button>
                                                <a class="btn btn-info" style="margin-left: 5px;" data-toggle="modal" href="#modalSeaarch">Search more</a>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="input-icon">
                                            <i class="fa fa-qq"></i>
                                            <asp:DropDownList ID="dlEmployeesAdvisory" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="dlEmployeesAdvisory_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-2">
                                        <div class="input-icon">
                                            <i class="fa fa-filter"></i>
                                            <asp:DropDownList ID="dlProgressForm" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="dlProgressForm_SelectedIndexChanged" runat="server">
                                                <asp:ListItem Value="-1">-- Tình trạng --</asp:ListItem>
                                                <asp:ListItem Value="0">Expired</asp:ListItem>
                                                <asp:ListItem Value="1"></asp:ListItem>
                                                <asp:ListItem Value="2">Đang làm hồ sơ</asp:ListItem>
                                                <asp:ListItem Value="3">Hoàn thành hồ sơ</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                            </div>
                            <br />
                            <%-- Begin content --%>
                            <asp:GridView ID="gwAdvisoryManager" CssClass="table table-condensed" runat="server" AutoGenerateColumns="False" RowStyle-BackColor="#A1DCF2" Font-Names="Arial" Font-Size="10pt"
                                HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White">

                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkrow" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="40px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Phiếu đăng ký tư vấn">
                                        <ItemTemplate>
                                            <%-- /Row --%>
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <a style="font-size: 16px;" href='<%# "../QuanLyHoSo/PhieuDangKyTuVan_Info.aspx?FormID=" + Eval("RegistrationID") %>'>
                                                        <asp:Label ID="lblFullName" runat="server" Text='<%# Bind("FullName") %>'></asp:Label></a>
                                                </div>
                                                <div class="col-lg-12" style="padding-left: 45px;">
                                                    <i class="fa fa-building"></i>
                                                    <asp:Label ID="lblAddress_form" runat="server" Text='<%# Bind("Address_form") %>'></asp:Label><br />
                                                    <i class="fa fa-phone"></i>
                                                    <asp:Label ID="lblPhone" runat="server" Text='<%# Bind("Phone") %>'></asp:Label><br />
                                                    <i class="fa fa-folder-o"></i>
                                                    <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                                                </div>
                                            </div>
                                            <%-- /End row --%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Loại Đăng Ký">
                                        <ItemTemplate>
                                            <span class='<%# Eval("TypeName").ToString() == "Tư Vấn Du Học" ? "label label-primary" : Eval("TypeName").ToString() == "Tư Vấn Thực Tập" ? "label label-default" : Eval("TypeName").ToString() == "Tư Vấn Du Lịch" ? "label label-success" :"label label-warning" %>'>
                                                <strong><i class="fa fa-pencil-square-o"></i>
                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("TypeName") %>'></asp:Label></strong></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Trình độ học vấn">
                                        <ItemTemplate>
                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tư vấn du học">
                                        <ItemTemplate>
                                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("CountryName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ngày đăng ký">
                                        <ItemTemplate>
                                            <asp:Label ID="Label8" runat="server" Text='<%# Bind("DateOfCreate","{0:dd/MM/yyyy hh:mm:ss tt}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="From">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFFStatus" runat="server" Text='<%# Bind("FFStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nhân viên tư vấn">
                                        <ItemTemplate>
                                            <i style="color: #d64d25;" class="glyphicon glyphicon-user"></i>&nbsp <i><%# Eval("EmpFullName")+" - Mã NV: "+ Eval("EmployeesCode") %></i>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tình trạng">
                                        <ItemTemplate>
                                            <span class='<%# Eval("ProgressForm").ToString()=="0"?"btn default btn-xs red-stripe": Eval("ProgressForm").ToString()=="1"?"btn default btn-xs blue-stripe": Eval("ProgressForm").ToString()=="2"?"btn default btn-xs yellow-stripe":"btn default btn-xs green-stripe" %>'>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("ProgressForm").ToString()=="0"?"Chờ xử lý": Eval("ProgressForm").ToString()=="1"?"Đang tư vấn": Eval("ProgressForm").ToString()=="2"?"Đang làm hồ sơ":"Hoàn thành hồ sơ" %>'></asp:Label>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#3AC0F2" ForeColor="White"></HeaderStyle>
                                <RowStyle BackColor="#A1DCF2"></RowStyle>
                            </asp:GridView>
                            <%-- End Content --%>
                        </div>
                    </div>
                    <!-- END TABLE PORTLET-->
                    <div class="form-group">
                        <!-- BEGIN PAGINATOR -->
                        <div class="row">
                            <div class="col-md-4 col-sm-4 items-info">
                            </div>
                            <div class="col-md-8 col-sm-8">
                                <div class="pagination_lst pull-right">
                                    <asp:Repeater ID="rptPager" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                                                OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <div class="clearfix"></div>
                                    <asp:Repeater ID="Repeatersearch" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSearchPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                                                OnClick="DLPage_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Repeater ID="RepeaterKeySearch" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkKeySearch" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                                                OnClick="KeySearchPage_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Repeater ID="RepeaterUserAdv" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkUserAdv" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                                                OnClick="UserAdvPage_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Repeater ID="RepeaterFilterProgress" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkFilter" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                                                OnClick="FilterProgessPage_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                        <!-- END PAGINATOR -->
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <span class="label label-primary"><strong><i class="fa fa-pencil-square-o"></i>
                <label>Tư Vấn Du Học</label></strong></span>
            <span class="label label-default"><strong><i class="fa fa-pencil-square-o"></i>
                <label>Tư Vấn Thực Tập</label></strong></span>
            <span class="label label-success"><strong><i class="fa fa-pencil-square-o"></i>
                <label>Tư Vấn Du Lịch</label></strong></span>
            <span class="label label-warning"><strong><i class="fa fa-pencil-square-o"></i>
                <label>Tư Vấn Định Cư</label></strong></span>
        </div>
    </div>
    <%-- Modal Search --%>
    <div class="modal fade" id="modalSeaarch" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">Select Options</h4>
                </div>
                <div class="modal-body background">
                    <div class="row">
                        <div class="col-lg-3">
                            <asp:DropDownList ID="dlRegistration_Type" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                        <div class="col-lg-3">
                            <asp:DropDownList ID="dlEducationLV" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                        <div class="col-lg-3">
                            <asp:DropDownList ID="dlCountryAdvisory" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                        <div class="col-lg-2">
                            <asp:Button ID="btnSearchMore" CssClass="btn btn-primary" OnClick="btnSearchMore_Click" runat="server" Text="SEARCH" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--End Modal Search --%>
    <%-- Modal Change --%>
    <div class="modal fade" id="modalChangeUserAdv" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title"><i class="fa fa-reply"></i>Chuyển Phiếu Từ Vấn Cho Nhân Viên Tư Vấn Khác !</h4>
                </div>
                <div class="modal-body background">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <label class="control-label">Chọn Nhân Viên Tư Vấn</label>
                                <asp:DropDownList ID="dlChangeEmpAdvisory" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-header text-right">
                    <asp:Button ID="btnChangeEmpAdvisory" CssClass="btn red" OnClick="btnChangeEmpAdvisory_Click" runat="server" Text="Chuyển Phiếu" />
                    <a class="btn btn-warning" data-dismiss="modal" aria-hidden="true">Hủy</a>
                </div>
            </div>
        </div>
    </div>
    <%--End Modal Change --%>
    <%-- Modal Export to Excel --%>
    <div class="modal fade" id="modalExport2Excel" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title"><i class="fa fa-file-excel-o"></i>&nbsp Export to Excel</h4>
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
                                                <label class="control-label bold col-md-3">Nhân viên Tư Vấn</label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="dlExNhanVien" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- /Row --%>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="control-label bold col-md-3">Loại ĐK Tư Vấn</label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="dlExLoaiDK" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- /Row --%>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="control-label bold col-md-3">Từ ngày</label>
                                                <div class="col-md-8">
                                                    <%-- Date picker --%>
                                                    <div class="input-group date date-picker" data-date-format="dd-mm-yyyy">
                                                        <asp:TextBox ID="txtStartDate" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <span class="input-group-btn">
                                                            <button class="btn default" type="button"><i class="fa fa-calendar"></i></button>
                                                        </span>
                                                    </div>
                                                    <%-- Date picker --%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- /Row --%>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="control-label bold col-md-3">Đến ngày</label>
                                                <div class="col-md-8">
                                                    <%-- Date picker --%>
                                                    <div class="input-group date date-picker" data-date-format="dd-mm-yyyy">
                                                        <asp:TextBox ID="txtFinishDate" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <span class="input-group-btn">
                                                            <button class="btn default" type="button"><i class="fa fa-calendar"></i></button>
                                                        </span>
                                                    </div>
                                                    <asp:CompareValidator ID="cvtxtStartDate" runat="server"
                                                        ControlToCompare="txtStartDate" CultureInvariantValues="true"
                                                        Display="Dynamic" EnableClientScript="true"
                                                        ControlToValidate="txtFinishDate"
                                                        ForeColor="Red"
                                                        ErrorMessage="Start date must be earlier than finish date"
                                                        Type="Date" SetFocusOnError="true" Operator="GreaterThanEqual"
                                                        Text="Start date must be earlier than finish date"></asp:CompareValidator>
                                                    <%-- Date picker --%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <a class="btn btn-warning" data-dismiss="modal">Hủy</a>
                    <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Export to Excel" />
                </div>
            </div>
        </div>
    </div>
    <%-- End Modal Export to Excel --%>
    <script>
        function reloadclick() {
            window.location.reload();
        }
    </script>
</asp:Content>

