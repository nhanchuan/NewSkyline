<%@ Page Title="" Language="C#" MasterPageFile="~/GlobalMasterPage.master" AutoEventWireup="true" CodeFile="GhiDanhTiemNang.aspx.cs" Inherits="kus_admin_GhiDanhTiemNang" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
        </div>
    </div>
    <%--End Pages is Valid --%>
    <div class="row">
        <div class="col-lg-12">
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
                                    ServiceMethod="SearchHocVienCode"
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
                        <hr />
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <label class="control-label">Địa chỉ thường trú (<span class="required">*</span>)</label>
                                    <asp:TextBox ID="txtDCThuongTru" CssClass="form-control" runat="server"></asp:TextBox>
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
                        <div class="form-group">
                            <label class="control-label">Nghề Nghiệp</label>
                            <asp:TextBox ID="txtNgheNghiepPH" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label class="control-label">Điện thoại phụ huynh</label>
                            <asp:TextBox ID="txtDienThoaiPH" CssClass="form-control" runat="server"></asp:TextBox>
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
                <div class="form-group">
                    <asp:Button ID="btnAddNewHV" CssClass="btn btn-primary" ValidationGroup="validGhiDanhHV" runat="server" Text="LƯU THÔNG TIN" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>

