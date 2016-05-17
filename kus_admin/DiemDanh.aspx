<%@ Page Title="" Language="C#" MasterPageFile="~/GlobalMasterPage.master" AutoEventWireup="true" CodeFile="DiemDanh.aspx.cs" Inherits="kus_admin_DiemDanh" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- BEGIN PAGE HEADER-->
    <h1 class="page-title">Điểm Danh Khóa Học
    </h1>
    <div class="page-bar">
        <ul class="page-breadcrumb">
            <li>
                <i class="fa fa-home"></i>
                <a href="../Default.aspx">Home</a>
                <i class="fa fa-angle-right"></i>
            </li>
            <li>
                <a href="#">Khóa học</a>
                <i class="fa fa-angle-right"></i>
            </li>
            <li>
                <a href="../kus_admin/DiemDanh.aspx">Điểm danh</a>
            </li>
        </ul>
    </div>
    <!-- END PAGE HEADER-->
    <div class="row">
       <!-- BEGIN Portlet PORTLET-->
        <div class="portlet light">
            <div class="portlet-title">
                <div class="caption">
                    <i class="glyphicon glyphicon-list-alt font-yellow-casablanca"></i>
                    <span class="caption-subject bold font-yellow-casablanca uppercase">Danh sách khóa học </span>
                    <span class="caption-helper">more samples...</span>
                </div>
                <div class="inputs">
                    <div class="portlet-input input-inline input-medium">
                        <div class="input-group">
                            <input id="txtsearch" type="text" class="form-control input-circle-left" placeholder="search..." title="Tìm Mã hoặc Tên khóa học" runat="server" />
                            <span class="input-group-btn">
                                <button id="btnSearchKhoaHoc" class="btn btn-circle-right btn-default" type="submit" onserverclick="btnSearchKhoaHoc_ServerClick" runat="server">Go!</button>
                            </span>
                        </div>
                    </div>
                </div>
                <div class="actions">
                    <a id="btnDiemDanh" onserverclick="btnDiemDanh_ServerClick" runat="server"><i class="icon-emoticon-smile"></i> Điểm danh</a>
                    <a id="btnRefreshLstKhoaHoc" class="btn btn-circle btn-icon-only btn-default" title="Làm mới danh sách" runat="server" href="#">
                        <i class="fa fa-refresh"></i>
                    </a>
                    <a class="btn btn-circle btn-icon-only btn-default fullscreen" href="#"></a>
                </div>
            </div>
            <div class="portlet-body">
                <asp:GridView ID="gwKhoaHoc" CssClass="table table-condensed" runat="server"
                    AutoGenerateColumns="False" RowStyle-BackColor="#A1DCF2" Font-Names="Arial" Font-Size="10pt"
                    HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White" OnSelectedIndexChanged="gwKhoaHoc_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" runat="server" Text='<%# Eval("RowNumber") %>'></asp:Label>
                                <asp:Label ID="lblID" CssClass="display-none" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mã khóa học">
                            <ItemTemplate>
                                <asp:Label ID="lblMaKhoaHoc" runat="server" Text='<%# Eval("MaKhoaHoc") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tên khóa học">
                            <ItemTemplate>
                                <asp:Label ID="lblTenKhoaHoc" runat="server" Text='<%# Eval("TenKhoaHoc") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Số lượng học viên">
                            <ItemTemplate>
                                <asp:Label ID="lblSoLuong" runat="server" Text='<%# Eval("SoLuong") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ngày khai giảng">
                            <ItemTemplate>
                                <asp:Label ID="lblNgayKhaiGiang" runat="server" Text='<%# Eval("NgayKhaiGiang","{0:dd/MM/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ngày kết thúc">
                            <ItemTemplate>
                                <asp:Label ID="lblNgayKetThuc" runat="server" Text='<%# Eval("NgayKetThuc","{0:dd/MM/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thời lượng">
                            <ItemTemplate>
                                <asp:Label ID="lblThoiLuong" runat="server" Text='<%# Eval("ThoiLuong") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thuộc lớp">
                            <ItemTemplate>
                                <asp:Label ID="lblTenLopHoc" runat="server" Text='<%# Eval("TenLopHoc") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thuộc Chương trình">
                            <ItemTemplate>
                                <asp:Label ID="lblTenChuongTrinh" runat="server" Text='<%# Eval("TenChuongTrinh") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thuộc Loại chương trình">
                            <ItemTemplate>
                                <asp:Label ID="lblTenLoaiChuongTrinh" runat="server" Text='<%# Eval("TenLoaiChuongTrinh") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thuộc Cơ Sở">
                            <ItemTemplate>
                                <asp:Label ID="lblTenCoSo" runat="server" Text='<%# Eval("TenCoSo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="lkSelect" runat="server" CausesValidation="False" CommandName="Select" Text="Select"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                    </Columns>
                    <SelectedRowStyle BackColor="#79B782" ForeColor="Black" />
                    <HeaderStyle BackColor="#FFB848" ForeColor="White"></HeaderStyle>
                    <RowStyle BackColor="#FAF3DF"></RowStyle>
                </asp:GridView>
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
                                <asp:Repeater ID="rptSearch" runat="server">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkSearch" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                            CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                                            OnClick="Search_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                    <!-- END PAGINATOR -->
                </div>
            </div>
        </div>
        <!-- END Portlet PORTLET-->
    </div>
</asp:Content>

