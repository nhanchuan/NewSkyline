<%@ Page Title="" Language="C#" MasterPageFile="~/GlobalMasterPage.master" AutoEventWireup="true" CodeFile="GhiDanhTiemNang.aspx.cs" Inherits="kus_admin_GhiDanhTiemNang" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../App_Themes/admin/StylePortlet.css" rel="stylesheet" />
    <!-- BEGIN PAGE HEADER-->
    <h1 class="page-title">Ghi Danh Học Viên Tiềm Năng</h1>
    <div class="page-bar">
        <ul class="page-breadcrumb">
            <li>
                <i class="fa fa-home"></i>
                <a href="../Default.aspx">Home</a>
                <i class="fa fa-angle-right"></i>
            </li>
            <li>
                <a href="../kus_admin/GhiDanhTiemNang.aspx">Ghi danh học viên tiềm năng</a>
            </li>
        </ul>
    </div>
    <!-- BEGIN PAGE HEADER-->
    <%-- Pages is Valid --%>
    <div class="row">
        <div class="col-lg-12">
            <div class="alert alert-danger display-none" id="alertPageValid" runat="server">
                <asp:Label ID="lblPageValid" runat="server"></asp:Label>
            </div>

            <asp:ValidationSummary ID="ValidationSummary1" ForeColor="Red" CssClass="bold margin-bottom-20" DisplayMode="SingleParagraph" ShowSummary="true" ValidationGroup="validGhiDanhHV" runat="server" />
            <asp:ValidationSummary ID="ValidationSummary2" ForeColor="Red" CssClass="bold margin-bottom-20" DisplayMode="SingleParagraph" ShowSummary="true" ValidationGroup="validGDHVAvailable" runat="server" />
        </div>
    </div>
    <%--End Pages is Valid --%>
    <div class="row">
        <div class="col-lg-12">

            <!--BEGIN TABS-->
            <div class="tabbable tabbable-custom tabbable-full-width">
                <ul class="nav nav-tabs">
                    <li class="active">
                        <a href="#tab_info" data-toggle="tab">Thông tin học viên </a>
                    </li>
                    <li>
                        <a href="#tab_class" data-toggle="tab">Lớp tiềm năng </a>
                    </li>
                    <li>
                        <a href="#tab_selectedCoSo" data-toggle="tab">Cơ sở</a>
                    </li>
                </ul>
                <div class="tab-content">

                    <%-- Tab Hoc Vien --%>
                    <div class="tab-pane active" id="tab_info">
                        <div class="row">
                            <div class="col-lg-3">
                                <div class="form-group">
                                    <label class="control-label"><i class="fa fa-graduation-cap"></i>Thông tin học viên</label>
                                    <asp:DropDownList ID="dlChoseLoaiGD" AutoPostBack="true" OnSelectedIndexChanged="dlChoseLoaiGD_SelectedIndexChanged" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="0">Học Viên chưa đăng ký</asp:ListItem>
                                        <asp:ListItem Value="1">Học Viên đã học</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <%-- Panel Hoc Vien Dang Ki Moi --%>
                        <div class="row" id="panelGhiDanhMoi" runat="server">
                            <div class="panel panel-info background">
                                <div class="panel-body">
                                    <h2>THÔNG TIN GHI DANH </h2>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <label class="btn default btn-xs red-stripe margin-bottom-20"><a href="#collapPhieuTV" data-toggle="collapse">Lấy thông tin phiếu tư vấn</a></label>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group panel-collapse collapse" id="collapPhieuTV">
                                                <label class="control-label">Tên Phiếu</label>
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtPhieuTvInfor" AutoPostBack="true" OnTextChanged="txtPhieuTvInfor_TextChanged" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <span class="input-group-btn">
                                                        <button id="btnSearchKhoaHoc" class="btn btn-circle-right btn-default" type="submit" runat="server">Go!</button>
                                                    </span>
                                                </div>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1"
                                                    TargetControlID="txtPhieuTvInfor"
                                                    MinimumPrefixLength="2"
                                                    CompletionInterval="10"
                                                    EnableCaching="false"
                                                    CompletionSetCount="10"
                                                    FirstRowSelected="false"
                                                    ServiceMethod="SearchPhieuTuVanInfo"
                                                    runat="server">
                                                </asp:AutoCompleteExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <label class="control-label">Họ (<span class="required">*</span>)</label>
                                                    <asp:TextBox ID="txtLastNameHV" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtLastNameHV" ValidationGroup="validGhiDanhHV" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="Họ không được để trống !"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <label class="control-label">Tên (<span class="required">*</span>)</label>
                                                    <asp:TextBox ID="txtFirstNameHV" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtFirstNameHV" ValidationGroup="validGhiDanhHV" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="Tên không được để trống !"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <label class="control-label">Ngày sinh (<span class="required">*</span>)</label>
                                                    <%-- Date picker --%>
                                                    <div class="input-group date date-picker" data-date-format="dd-mm-yyyy">
                                                        <asp:TextBox ID="txtNgaySinh" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <span class="input-group-btn">
                                                            <button class="btn default" type="button"><i class="fa fa-calendar"></i></button>
                                                        </span>
                                                    </div>
                                                    <%-- Date picker --%>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtNgaySinh" ValidationGroup="validGhiDanhHV" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="Ngày sinh không được để trống !"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <label class="control-label">Nơi sinh</label>
                                                    <asp:TextBox ID="txtNoiSinhHV" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">

                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <label class="control-label">Giới tính (*)</label>
                                                    <div class="radio-list">
                                                        <label class="radio-inline">
                                                            <input type="radio" name="optionsRadios" id="rdformnam" value="option1" runat="server" />
                                                            Nam
                                                        </label>
                                                        <label class="radio-inline">
                                                            <input type="radio" name="optionsRadios" id="rdformnu" value="option2" runat="server" />
                                                            Nữ
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <label class="control-label">Địa chỉ thường trú (<span class="required">*</span>)</label>
                                                    <asp:TextBox ID="txtDCThuongTru" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                                                        ControlToValidate="txtDCThuongTru"
                                                        ValidationGroup="validGhiDanhHV"
                                                        runat="server"
                                                        ForeColor="Red"
                                                        Display="Dynamic"
                                                        ErrorMessage="Địa chỉ không được để trống !"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <label class="control-label">Địa chỉ tạm trú</label>
                                                    <asp:TextBox ID="txtDCTamTru" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <label class="control-label">Email</label>
                                                    <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                                                        ControlToValidate="txtEmail"
                                                        ValidationGroup="validGhiDanhHV"
                                                        ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                                        Display="Dynamic" ForeColor="Red"
                                                        ErrorMessage="E-mail không hợp lệ ! { Ví dụ hợp lệ: abc@gmail.com }">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <label class="control-label">Điện thoại (<span class="required">*</span>)</label>
                                                    <asp:TextBox ID="txtPhoneHV" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtPhoneHV" ValidationGroup="validGhiDanhHV" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="Điện thoại không được để trống !"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                                                        ControlToValidate="txtPhoneHV"
                                                        ValidationGroup="validGhiDanhHV"
                                                        ValidationExpression="\(?([0-9]{3,4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})"
                                                        ErrorMessage="<%$Resources:Resource, incorectphonenumber %>"
                                                        ForeColor="Red"
                                                        Display="Dynamic">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label">Họ tên phụ huynh</label>
                                            <asp:TextBox ID="txtHoTenPH" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <hr />
                                        <a href="#collapviewmoreinfor" data-toggle="collapse">Thông tin thêm <i class="glyphicon glyphicon-chevron-down"></i></a>
                                        <hr />
                                        <%--collap More Infor --%>
                                        <div class="form-group panel-collapse collapse" id="collapviewmoreinfor">
                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <div class="form-group">
                                                        <label class="control-label">Số CMND</label>
                                                        <asp:TextBox ID="txtSoCMNDHV" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9"
                                                            ControlToValidate="txtSoCMNDHV"
                                                            ValidationGroup="validGhiDanhHV"
                                                            ForeColor="Red" Display="Dynamic"
                                                            ValidationExpression="^[0-9]{9,12}"
                                                            runat="server" ErrorMessage="CMND chỉ được nhập số  - Tối đa 12 ký tự .(VD: 245156321)"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="form-group">
                                                        <label class="control-label">Ngày cấp</label>
                                                        <%-- Date picker --%>
                                                        <div class="input-group date date-picker" data-date-format="dd-mm-yyyy">
                                                            <asp:TextBox ID="txtNgayCapCMND" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <span class="input-group-btn">
                                                                <button class="btn default" type="button"><i class="fa fa-calendar"></i></button>
                                                            </span>
                                                        </div>
                                                        <%-- Date picker --%>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <div class="form-group">
                                                        <label class="control-label">Nơi cấp</label>
                                                        <asp:TextBox ID="txtNoiCapCMND" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label">Nghề Nghiệp</label>
                                                <asp:TextBox ID="txtNgheNghiepPH" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label">Điện thoại phụ huynh</label>
                                                <asp:TextBox ID="txtDienThoaiPH" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- More Info --%>
                                    <div class="col-lg-6">
                                        <h3>Thông tin thêm</h3>
                                        <div class="form-group">
                                            <label class="control-label">Học viên giới thiệu</label>
                                            <asp:TextBox ID="txtHVGioiThieu" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label">Trình độ học vấn</label>
                                            <div class="radio-list">
                                                <label class="radio-inline">
                                                    <input type="radio" id="chkMauGiao" runat="server" />
                                                    Mẫu giáo
                                                </label>
                                                <label class="radio-inline">
                                                    <input type="radio" id="chkTieuHoc" runat="server" />
                                                    Tiểu học
                                                </label>
                                                <label class="radio-inline">
                                                    <input type="radio" id="chkTHCS" runat="server" />
                                                    Trung học cơ sở
                                                </label>
                                                <label class="radio-inline">
                                                    <input type="radio" id="chkTHPT" runat="server" />
                                                    Trung học phổ thông
                                                </label>
                                                <label class="radio-inline">
                                                    <input type="radio" id="chkDH" runat="server" />
                                                    Đại Học - Cao Đẳng
                                                </label>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label">Tên trường</label>
                                            <asp:TextBox ID="txtTenTruong" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label">Chứng chỉ Tiếng Anh</label>
                                            <div class="checkbox-list">
                                                <label class="checkbox-inline">
                                                    <input type="checkbox" id="chkCC1" runat="server" />
                                                    Starters
                                                </label>
                                                <label class="checkbox-inline">
                                                    <input type="checkbox" id="chkCC2" runat="server" />
                                                    Movers
                                                </label>
                                                <label class="checkbox-inline">
                                                    <input type="checkbox" id="chkCC3" runat="server" />
                                                    Flyers
                                                </label>
                                                <label class="checkbox-inline">
                                                    <input type="checkbox" id="chkCC4" runat="server" />
                                                    KET
                                                </label>
                                                <label class="checkbox-inline">
                                                    <input type="checkbox" id="chkCC5" runat="server" />
                                                    PET
                                                </label>
                                                <label class="checkbox-inline">
                                                    <input type="checkbox" id="chkCC6" runat="server" />
                                                    FCE
                                                </label>
                                                <label class="checkbox-inline">
                                                    <%--<input type="checkbox" id="chk7" runat="server" />--%>
                                                    <%--<asp:CheckBox ID="chk7" runat="server" />--%>
                                                    <a href="#collapCCKHac" data-toggle="collapse">
                                                        <i class="fa fa-file-o"></i>
                                                        Khác
                                                    </a>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="form-group panel-collapse collapse" id="collapCCKHac">
                                            <label class="control-label">Tên C/C khác</label>
                                            <asp:TextBox ID="txtCCKHac" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="form-group">
                                            <label class="control-label">Biết trung tâm qua</label>
                                            <div class="checkbox-list">
                                                <label class="checkbox-inline">
                                                    <input type="checkbox" id="chkBTT1" runat="server" />
                                                    Báo chí
                                                </label>
                                                <label class="checkbox-inline">
                                                    <input type="checkbox" id="chkBTT2" runat="server" />
                                                    Internet
                                                </label>
                                                <label class="checkbox-inline">
                                                    <input type="checkbox" id="chkBTT3" runat="server" />
                                                    Bạn bè
                                                </label>
                                                <label class="checkbox-inline">
                                                    <input type="checkbox" id="chkBTT4" runat="server" />
                                                    Website
                                                </label>
                                                <label class="checkbox-inline">
                                                    <input type="checkbox" id="chkBTT5" runat="server" />
                                                    Trực tiếp tại trung tâm
                                                </label>
                                                <label class="checkbox-inline">
                                                    <a href="#collapBTTKHac" data-toggle="collapse">
                                                        <i class="fa fa-file-o"></i>
                                                        Khác
                                                    </a>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="form-group panel-collapse collapse" id="collapBTTKHac">
                                            <label class="control-label">Ngồn khác</label>
                                            <asp:TextBox ID="txtBTTKHac" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label">Ghi chú</label>
                                            <asp:TextBox ID="txtGhiChu" CssClass="form-control" TextMode="MultiLine" Rows="5" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <%-- End More Info --%>
                                </div>
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <label class="control-label bold"><span class="required">Số tiền Đặt cọc trước (VND): </span></label>
                                        <asp:TextBox ID="txtDatCoc" CssClass="form-control bold" ForeColor="Red" runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtDatCoc" ValidationGroup="validGhiDanhHV" ValidationExpression="^\d+$" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="Chỉ được nhập số lớn hơn 0 !"></asp:RegularExpressionValidator>
                                        <asp:RangeValidator ID="RangeValidator1" ControlToValidate="txtDatCoc" ValidationGroup="validGhiDanhHV" MinimumValue="0" MaximumValue="100000000" Type="Integer" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="Số tiền không được vượt quá 100.000.000"></asp:RangeValidator>
                                        <asp:Label ID="lblValidDatCoc" ForeColor="Red" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="clearfix"></div>

                            </div>
                        </div>

                        <%-- Panel Hoc Vien Da dang ky --%>
                        <div class="row" id="panelDaGhiDanh" runat="server">
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label class="control-label">Mã Học Viên <span class="required">*</span>: </label>
                                    <asp:TextBox ID="txtHocVienCode" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtHocVienCode_TextChanged" runat="server"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="AutoCompleteHocVienCode"
                                        TargetControlID="txtHocVienCode"
                                        MinimumPrefixLength="2"
                                        CompletionInterval="10"
                                        EnableCaching="true"
                                        CompletionSetCount="10"
                                        FirstRowSelected="false"
                                        ServiceMethod="AutoCompleteCode"
                                        runat="server">
                                    </asp:AutoCompleteExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtHocVienCode"
                                        ValidationGroup="validGDHVAvailable" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="Mã Học Viên không để trống !"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label class="control-label">Ghi chú : </label>
                                    <asp:TextBox ID="txtGhiChuAvailable" CssClass="form-control" TextMode="MultiLine" Rows="4" runat="server"></asp:TextBox>

                                </div>
                                <div class="form-group">
                                    <label class="control-label bold"><span class="required">Số tiền Đặt cọc trước (VND): </span></label>
                                    <asp:TextBox ID="txtDatCocAvailable" CssClass="form-control bold" ForeColor="Red" runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtDatCocAvailable"
                                        ValidationGroup="validGDHVAvailable" ValidationExpression="^\d+$" ForeColor="Red"
                                        Display="Dynamic" runat="server" ErrorMessage="Chỉ được nhập số !"></asp:RegularExpressionValidator>
                                </div>

                            </div>
                            <div class="col-lg-8">
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        Thông tin học viên
                                    </div>
                                    <div class="panel-body">
                                        <div class="col-lg-2">
                                            <img src="../images/default_images.jpg" id="imgHocVienB" runat="server" style="width: 100%; height: auto;" />
                                        </div>
                                        <div class="col-lg-10">
                                            <table class="table table-bordered" border="1">
                                                <tr>
                                                    <td style="width: 15%;" class="bold">Mã Học Viên : </td>
                                                    <td>
                                                        <asp:Label ID="lblMaHocVienB" runat="server" Text="Label"></asp:Label></td>
                                                    <td style="width: 15%;" class="bold">E-Mail : </td>
                                                    <td>
                                                        <asp:Label ID="lblEmailHocVienB" runat="server" Text="Label"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td class="bold">Họ và Tên : </td>
                                                    <td>
                                                        <asp:Label ID="lblHoTenHocVienB" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                    <td class="bold">Điện thoại : </td>
                                                    <td>
                                                        <asp:Label ID="lblDienThoaiHocVienB" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="bold">Ngày sinh : </td>
                                                    <td>
                                                        <asp:Label ID="lblBirthdayHocVienB" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                    <td class="bold">Giới tính : </td>
                                                    <td>
                                                        <asp:Label ID="lblSexHocVienB" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-success">
                                    <div class="panel-heading">
                                        Các khóa đã ghi danh
                                    </div>
                                    <div class="panel-body">
                                        <asp:Label ID="lblMessageKhoaHoc" runat="server" Text="Không tìm dữ liệu !"></asp:Label>
                                        <asp:GridView ID="gwChonLopGhiDanh" CssClass="table table-condensed table-responsive" runat="server" AutoGenerateColumns="False" RowStyle-BackColor="#A1DCF2" Font-Names="Arial" Font-Size="10pt"
                                            HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Mã khóa học">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMaKhoaHoc" runat="server" Text='<%# Eval("MaKhoaHoc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tên Khóa">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTenKhoaHoc" runat="server" Text='<%# Eval("TenKhoaHoc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ngày đăng ký">
                                                    <ItemTemplate>
                                                        <i class="fa fa-calendar-o"></i>
                                                        <asp:Label ID="lblNgayDangKy" runat="server" Text='<%# Eval("NgayDangKy","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Khai giảng">
                                                    <ItemTemplate>
                                                        <i class="fa fa-calendar-o"></i>
                                                        <asp:Label ID="lblNgayKhaiGiang" runat="server" Text='<%# Eval("NgayKhaiGiang","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Kết thúc">
                                                    <ItemTemplate>
                                                        <i class="fa fa-calendar-o"></i>
                                                        <asp:Label ID="lblNgayKetThuc" CssClass='<%# HasOutdate(Eval("NgayKetThuc").ToString())?"label label-danger":"label label-success" %>' runat="server" Text='<%# Eval("NgayKetThuc","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Công Nợ">
                                                    <ItemTemplate>
                                                        <i>
                                                            <asp:Label ID="lblRemainFee" ForeColor="Red" CssClass="bold" runat="server" Text='<%# string.IsNullOrEmpty(Eval("RemainFee").ToString())?"0":Eval("RemainFee") %>'></asp:Label>
                                                            <span class="text-danger">₫</span></i>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <SelectedRowStyle BackColor="#79B782" ForeColor="Black" />
                                            <HeaderStyle BackColor="#FFB848" ForeColor="White"></HeaderStyle>
                                            <RowStyle BackColor="#FAF3DF"></RowStyle>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%-- Tab Class --%>
                    <div class="tab-pane" id="tab_class">
                        <div class="scroller" data-height="850px" data-always-visible="1" data-rail-visible1="1">

                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>

                                    <div class="row">
                                        <div class="form-horizontal">
                                            <div class="form-body">
                                                <%-- /Row --%>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="control-label bold col-md-4">Thuộc loại chương trình</label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="dlThuocLoaiChuongTrinh" AutoPostBack="true" OnSelectedIndexChanged="dlThuocLoaiChuongTrinh_SelectedIndexChanged" CssClass="form-control" runat="server"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="control-label bold col-md-4">Thuộc chương trình</label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="dlChuongTrinh" AutoPostBack="true" OnSelectedIndexChanged="dlChuongTrinh_SelectedIndexChanged" CssClass="form-control" runat="server"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:GridView ID="gwSelectClass" CssClass="table table-bordered" AutoGenerateColumns="False" ShowHeader="true" runat="server">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <%--<asp:CheckBox ID="CheckBox1" CausesValidation="False" CommandName="Select" runat="server" />--%>
                                                    <asp:LinkButton ID="lkSelect" runat="server" CausesValidation="False" CommandName="Select" Text="Select"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="30" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mã">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLopHocCode" runat="server" Text='<%# Eval("LopHocCode") %>'></asp:Label>
                                                    <asp:Label ID="lblID" CssClass="display-none" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tên">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTenLopHoc" runat="server" Text='<%# Eval("TenLopHoc") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Thời lượng">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblThoiLuong" runat="server" Text='<%# Eval("ThoiLuong") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bằng cấp">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBangCap" runat="server" Text='<%# Eval("BangCap") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mức học phí">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMucHocPhi" runat="server" Text='<%# Eval("MucHocPhi") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Thuộc chương trình">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblct_Ten" runat="server" Text='<%# Eval("ct_Ten") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle BackColor="#FAF3DF"></RowStyle>
                                        <SelectedRowStyle BackColor="#79B782" ForeColor="Black" />
                                    </asp:GridView>

                                </ContentTemplate>
                            </asp:UpdatePanel>


                        </div>
                    </div>

                    <%-- Tab Cơ Sở --%>
                    <div class="tab-pane" id="tab_selectedCoSo">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="form-body">
                                    <%-- /Row --%>
                                    <div class="row">
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label class="control-label bold col-md-4">Thuộc Hệ thống chi nhánh</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="dlHTChiNhanh" AutoPostBack="true" OnSelectedIndexChanged="dlHTChiNhanh_SelectedIndexChanged" CssClass="form-control" runat="server"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label class="control-label bold col-md-4">Thuộc Cơ Sở  (<span class="required">*</span>)</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="dlCoSo" CssClass="form-control" runat="server"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                                ControlToValidate="dlCoSo"
                                                                ValidationGroup="validGhiDanhHV"
                                                                Display="Static"
                                                                ForeColor="Red"
                                                                ErrorMessage="Chưa chọn Cơ Sở !"
                                                                InitialValue="0">
                                                            </asp:RequiredFieldValidator>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                                                ControlToValidate="dlCoSo"
                                                                ValidationGroup="validGDHVAvailable"
                                                                Display="Static"
                                                                ForeColor="Red"
                                                                ErrorMessage="Chưa chọn Cơ Sở !"
                                                                InitialValue="0">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="dlHTChiNhanh" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </div>

        <div class="col-lg-12 text-right">
            <div class="form-group">
                <asp:Button ID="btnAddNewHV" ValidationGroup="validGhiDanhHV" OnClick="btnAddNewHV_Click" runat="server" Text="LƯU THÔNG TIN(1)" />
                <asp:Button ID="btnAddOldHV" CssClass="btn btn-primary" ValidationGroup="validGDHVAvailable" OnClick="btnAddOldHV_Click" runat="server" Text="LƯU THÔNG TIN(2)" />
            </div>
        </div>
    </div>

</asp:Content>

