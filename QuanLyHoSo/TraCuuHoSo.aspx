﻿<%@ Page Title="" Language="C#" MasterPageFile="~/GlobalMasterPage.master" AutoEventWireup="true" CodeFile="TraCuuHoSo.aspx.cs" Inherits="QuanLyHoSo_TraCuuHoSo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- BEGIN PAGE HEADER-->
    <h1 class="page-title">Tra Cứu Hồ Sơ
    </h1>
    <div class="page-bar">
        <ul class="page-breadcrumb">
            <li>
                <i class="fa fa-home"></i>
                <a href="../Default.aspx">Home</a>
                <i class="fa fa-angle-right"></i>
            </li>
            <li>
                <a href="../QuanLyHoSo/TraCuuHoSo.aspx">Tra cứu hồ sơ</a>
            </li>
        </ul>
    </div>
    <!-- END PAGE HEADER-->
    <div class="row">
        <div class="col-lg-8">
            <div class="form-horizontal">
                <h4><i>Bộ Hồ Sơ</i></h4>
                <div class="form-body">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <%-- /Row --%>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label bold col-md-4">Mã Hồ Sơ : </label>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtProfileCode" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <h4><i>Thông Tin Hồ Sơ</i></h4>
            <div class="form-horizontal">
                <div class="form-body">
                    <%-- /Row --%>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-4">Loại Hồ Sơ : </label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="dlLoaiHoSo" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6"></div>
                    </div>
                    <%-- /Row --%>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-4">Họ Tên Khách Hàng : </label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtFullName" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-4">CMND Khách Hàng : </label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtCMND" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%-- /Row --%>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-4">Địa chỉ Email : </label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txttEmail" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label col-md-4">Số điện thoại : </label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtPhone" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%-- /Row --%>
                    <div class="row">
                        <div class="col-md-6">
                        </div>
                        <div class="col-md-6 text-right">

                            <a id="btnRefreshSearch" class="btn btn-default" onserverclick="btnRefreshSearch_ServerClick" runat="server"><i class="fa fa-refresh"></i>&nbsp Refresh</a>
                            <a id="btnSearchHocVien" class="btn btn-default" onserverclick="btnSearchHocVien_ServerClick" runat="server"><i class="fa fa-search"></i>&nbsp Tìm</a>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <!-- BEGIN Portlet PORTLET-->
        <div class="portlet light">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-list font-yellow-casablanca"></i>
                    <span class="caption-subject bold font-yellow-casablanca uppercase">Danh sách Hồ Sơ </span>
                    <span class="caption-helper">( Có
                        <asp:Label ID="lblsumketqua" runat="server" Text="0"></asp:Label>
                        kết quả tìm thấy...)</span>
                </div>
                <div class="actions">
                    <a id="btnViewInfor" title="Xem trong cửa sổ mới" onserverclick="btnViewInfor_ServerClick" runat="server">
                        <i class="icon-screen-tablet"></i>
                    </a>
                    <a id="btnViewProfile" href="#modalViewBagProfile" data-toggle="modal" title="Xem Hồ Sơ Lưu Trữ" runat="server">
                        <i class="glyphicon glyphicon-briefcase"></i>
                    </a>
                    <a id="btnRefreshListHocVien" class="btn btn-circle btn-icon-only btn-default" title="Làm mới danh sách" runat="server">
                        <i class="fa fa-refresh"></i>
                    </a>
                    <a class="btn btn-circle btn-icon-only btn-default fullscreen" href="#"></a>
                </div>
            </div>
            <div class="portlet-body">
                <asp:GridView ID="gwTraCuuHoSo" CssClass="table table-condensed" runat="server"
                    AutoGenerateColumns="False" RowStyle-BackColor="#A1DCF2" Font-Names="Arial" Font-Size="10pt"
                    HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White" OnSelectedIndexChanged="gwTraCuuHoSo_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" runat="server" Text='<%# Eval("RowNumber") %>'></asp:Label>
                                <asp:Label ID="lblInfoID" CssClass="display-none" runat="server" Text='<%# Eval("InfoID") %>'></asp:Label>
                                <asp:Label ID="LBLBasicInfoCode" CssClass="display-none" runat="server" Text='<%# Eval("BasicInfoCode") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mã Hồ Sơ">
                            <ItemTemplate>
                                <asp:Label ID="LBLProfileCode" runat="server" Text='<%# Eval("ProfileCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Họ Tên Khách Hàng">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("FullName") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <a style="font-size: 16px;" href='<%# "../QuanLyHoSo/CapNhatThongTinKhachHang.aspx?FileCode=" + Eval("BasicInfoCode") %>'>
                                    <asp:Label ID="Label1" CssClass="bold" runat="server" Text='<%# Bind("FullName") %>'></asp:Label></a>
                                <asp:Label ID="lblProfileID" CssClass="display-none" runat="server" Text='<%# Bind("ProfileID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ngày sinh">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Birthday") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Birthday","{0:dd/MM/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Giới tính">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Sex") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("Sex") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CMND">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("IdentityCard") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("IdentityCard") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email">
                            <ItemTemplate>
                                <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Điện Thoại">
                            <ItemTemplate>
                                <asp:Label ID="lblCellPhone" runat="server" Text='<%# Bind("CellPhone") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thông Tin Thêm (Ghi chú)">
                            <ItemTemplate>
                                <asp:Label ID="lblGhiChu" runat="server" Text='<%# Bind("GhiChu") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Hồ Sơ">
                            <ItemTemplate>
                                <li class='<%# Eval("BagProfileTypeID").ToString() == "1" ? "list-group-item bg-blue" : Eval("BagProfileTypeID").ToString() == "2" ? "list-group-item bg-danger" : Eval("BagProfileTypeID").ToString() == "3" ? "list-group-item bg-green" :"list-group-item bg-yellow" %>'>
                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("BagProfileTypeID").ToString()=="1"?"Du Học": Eval("BagProfileTypeID").ToString()=="2"?"Thực Tập": Eval("BagProfileTypeID").ToString()=="3"?"Du Lịch":"Định Cư" %>'></asp:Label>
                                </li>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%--<a class="btn btn-circle btn-icon-only btn-default"><i class="glyphicon glyphicon-folder-open"></i></a>--%>
                                <a class="btn btn-circle btn-icon-only btn-default">
                                    <i class='<%# Eval("NumBagProfile").ToString()=="0"?"glyphicon glyphicon-folder-close":"glyphicon glyphicon-folder-open" %>'></i></a>
                            </ItemTemplate>
                            <ItemStyle Width="30" />
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="True" />
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
                            </div>
                        </div>
                    </div>
                    <!-- END PAGINATOR -->
                </div>
            </div>
        </div>
        <!-- END Portlet PORTLET-->
    </div>


     <%-- Modal View BagProfile--%>
    <div class="modal fade" id="modalViewBagProfile" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">
                        <i class="glyphicon glyphicon-briefcase"></i> Hồ Sơ Lưu Trữ
                        </h4>
                </div>
                <div class="modal-body">
                    <div class="col-md-4"><h3>Tổng số : <asp:Label ID="lblSumBagFile" runat="server" Text="Label"></asp:Label></h3></div>
                    <%-- Gridview --%>
                    <asp:GridView ID="gwBagProfileManager" CssClass="table table-condensed" runat="server" AutoGenerateColumns="False" RowStyle-BackColor="#A1DCF2" Font-Names="Arial" Font-Size="10pt"
                        HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White">
                        <SelectedRowStyle BackColor="#79B782" ForeColor="Black" />
                        <Columns>
                            <asp:TemplateField HeaderText="Tên giấy tờ / Hồ sơ">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" CssClass="bold" runat="server" Text='<%# Bind("DocName") %>'></asp:Label>
                                    <asp:Label ID="lblBagProfileID" CssClass="display-none" runat="server" Text='<%# Eval("BagProfileID") %>'></asp:Label>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ghi chú">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("DocNote") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Thời gian lưu">
                                <ItemTemplate>
                                    <i class="fa fa-clock-o"></i>&nbsp <asp:Label ID="Label3" runat="server" Text='<%# Bind("DateOfCreate","{0:dd/MM/yyyy hh:mm:ss tt}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Attachments">
                                <ItemTemplate>
                                    <i class='<%# Eval("NumFileAtt").ToString()=="0"?"glyphicon glyphicon-ban-circle":"glyphicon glyphicon-ok" %>'></i>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FileTranslate">
                                <ItemTemplate>
                                    <i class='<%# Eval("NumFileTran").ToString()=="0"?"glyphicon glyphicon-ban-circle":"glyphicon glyphicon-ok" %>'></i>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle BackColor="#3AC0F2" ForeColor="White"></HeaderStyle>
                        <RowStyle BackColor="#A1DCF2"></RowStyle>
                    </asp:GridView>
                        <%-- End gridview --%>
                </div>
                <div class="modal-footer">
                    <a class="btn btn-warning" data-dismiss="modal">Close</a>
                </div>
            </div>
        </div>
    </div>
     <%--End Modal View BagProfile--%>
</asp:Content>

