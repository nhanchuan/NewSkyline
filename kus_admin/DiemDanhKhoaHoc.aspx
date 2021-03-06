﻿<%@ Page Title="" Language="C#" MasterPageFile="~/GlobalMasterPage.master" AutoEventWireup="true" CodeFile="DiemDanhKhoaHoc.aspx.cs" Inherits="kus_admin_DiemDanhKhoaHoc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- BEGIN PAGE HEADER-->
    <h1 class="page-title">Điểm Danh Khóa Học :
        <asp:Label ID="lblTitlekhoahoc" runat="server"></asp:Label>
        , Ngày Khai Giảng :
        <asp:Label ID="lblTitleNKG" runat="server" Text="Label"></asp:Label>
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
                <i class="fa fa-angle-right"></i>
            </li>
            <li>
                <a href="#">Điểm Danh Khóa Học</a>
            </li>
        </ul>
    </div>
    <!-- END PAGE HEADER-->
    <div class="row">
        <div class="col-lg-12">
            <div class="alert alert-danger display-none" id="alertPageValid" runat="server">
                <asp:Label ID="lblPageValid" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-4">
            <h3 style="color: #0078D7;">Ngày điểm danh</h3>
            <div class="form-group">
                <a id="btnLoadLichDiemDanh" class="btn green" onserverclick="btnLoadLichDiemDanh_ServerClick" runat="server">View List &nbsp<i class="glyphicon glyphicon-collapse-down"></i></a>
            </div>
            <div class="form-group">
                <asp:Label ID="lblMessageDiemDanh" ForeColor="Red" runat="server"></asp:Label>
            </div>
            <div id="panelLichDiemDanh" runat="server">
                <div class="form-group">
                    <asp:GridView ID="gwNgayDiemDanh" CssClass="table table-condensed" AutoGenerateColumns="false" RowStyle-BackColor="#A1DCF2" Font-Names="Arial" Font-Size="10pt"
                        HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
                        OnSelectedIndexChanged="gwNgayDiemDanh_SelectedIndexChanged"
                        OnRowDataBound="gwNgayDiemDanh_RowDataBound"
                        runat="server">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <i class='<%# Eval("SumNgayDD").ToString()=="0"?"glyphicon glyphicon-unchecked":"glyphicon glyphicon-check" %>'></i>
                                    <asp:Label ID="lblSumNgayDD" CssClass="display-none" runat="server" Text='<%# Eval("SumNgayDD") %>'></asp:Label>
                                    <asp:Label ID="lblNgayID" CssClass="display-none" runat="server" Text='<%# Eval("NgayID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ngày điểm danh">
                                <ItemTemplate>
                                    <asp:Label ID="lblNgayDiemDanh" runat="server" Text='<%# Eval("NgayDiemDanh","{0:dd/MM/yyyy}") %>'></asp:Label>
                                    <asp:Label ID="lblGetNgayDiemDanh" CssClass="display-none" runat="server" Text='<%# Eval("NgayDiemDanh","{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkselect" runat="server" CausesValidation="False" CommandName="Select" Text="Select"><i class="btn btn-circle btn-icon-only btn-default"><i class="glyphicon glyphicon-share-alt"></i></i></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="30px" />
                            </asp:TemplateField>
                        </Columns>
                        <SelectedRowStyle BackColor="#79B782" ForeColor="Black" />
                        <HeaderStyle BackColor="#FFB848" ForeColor="White"></HeaderStyle>
                        <RowStyle BackColor="#FAF3DF"></RowStyle>
                    </asp:GridView>
                </div>
            </div>
        </div>
        <div class="col-lg-8">
            <h3 style="color: #0078D7;">Bảng điểm danh</h3>
            <div class="form-group">
                <a class="btn green" id="btnSaveDiemDanh" onserverclick="btnSaveDiemDanh_ServerClick" runat="server"><i class="glyphicon glyphicon-ok"></i>&nbsp Lưu bảng điểm danh</a>
                <a class="btn btn-default disabled" id="btnDownloadExcel" style="background: #F78E4B;" runat="server"><i class="fa fa-file-excel-o"></i>Download file Excel</a>
            </div>
            <div class="form-group" id="divGhiChu" runat="server">
                <div class="panel-default">
                    <div class="panel-heading">
                        <h4 class="tile bold">Đánh Giá - Nhận xét học viên trong ngày</h4>
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-6">
                            <div class="form-horizontal">
                                <h4><i>Thông tin học viên</i></h4>
                                <div class="form-body">
                                    <%-- /Row --%>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <label class="control-label col-md-4">Ngày đánh giá :</label>
                                                <label class="control-label col-md-7" style="border:1px black solid;">
                                                    <asp:Label ID="lblNgayDanhGia" CssClass="bold pull-left" runat="server" Text="Label"></asp:Label>
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- /Row --%>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <label class="control-label col-md-4">Mã HV :</label>
                                                <label class="control-label col-md-7" style="border:1px black solid;">
                                                    <asp:Label ID="lblShowMaHV" CssClass="bold pull-left" runat="server" Text="Label"></asp:Label>
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- /Row --%>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <label class="control-label col-md-4">Họ tên HV :</label>
                                                <label class="control-label col-md-7" style="border:1px black solid;">
                                                    <asp:Label ID="lblShowHVName" CssClass="bold pull-left" runat="server" Text="Label"></asp:Label>
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                    <h4><i>Nhận xét - Đánh giá</i></h4>
                                    <%-- /Row --%>
                                    <div class="col-lg-12">
                                        <div class="form-group">
                                            <asp:Label ID="lblNhanXetDanhGia" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <a class="btn green" id="btnSaveNhanxet" onserverclick="btnSaveNhanxet_ServerClick" runat="server"><i class="glyphicon glyphicon-check"></i>&nbsp Lưu nhận xét</a>
                            </div>
                            <div class="form-group">
                                <cc1:Editor ID="EditorGhiChu" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <asp:GridView ID="gwDiemDanh" CssClass="table table-condensed" AutoGenerateColumns="false" RowStyle-BackColor="#A1DCF2" Font-Names="Arial" Font-Size="10pt"
                    HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White" OnRowDataBound="gwDiemDanh_RowDataBound" OnSelectedIndexChanged="gwDiemDanh_SelectedIndexChanged" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="Mã Học Viên">
                            <ItemTemplate>
                                <asp:Label ID="lblHocVienCode" runat="server" Text='<%# Eval("HocVienCode") %>'></asp:Label>
                                <asp:Label ID="lblDiemDanhID" CssClass="display-none" runat="server" Text='<%# Eval("DiemDanhID") %>'></asp:Label>
                                <asp:Label ID="lblGhiChu" CssClass="display-none" runat="server" Text='<%# Eval("GhiChu") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Họ">
                            <ItemTemplate>
                                <asp:Label ID="lblLastName" runat="server" Text='<%# Eval("LastName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tên">
                            <ItemTemplate>
                                <asp:Label ID="lblFirstName" runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Giới tính">
                            <ItemTemplate>
                                <asp:Label ID="lblSex" runat="server" Text='<%# Eval("Sex").ToString()=="1"?"Nam":"Nữ" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ngày sinh">
                            <ItemTemplate>
                                <asp:Label ID="lblBirthday" runat="server" Text='<%# Eval("Birthday","{0:dd/MM/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Điểm Danh" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <HeaderTemplate>
                                Điểm Danh<br />
                                <a id="btnSelectAllDD" onserverclick="btnSelectAllDD_ServerClick" runat="server">Select All</a> | <a id="btnUnselectAllDD" onserverclick="btnUnselectAllDD_ServerClick" runat="server">Unselect All</a>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkrowDiemDanh" Checked='<%# string.IsNullOrEmpty(Eval("DiemDanh").ToString())?false: Eval("DiemDanh").ToString()=="0"? false:true %>' runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Có Phép" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <HeaderTemplate>
                                Có Phép<br />
                                <a id="btncophepall" onserverclick="btncophepall_ServerClick" runat="server">Select All</a>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkrowCoPhep" Checked='<%# string.IsNullOrEmpty(Eval("CoPhep").ToString())?false: Eval("CoPhep").ToString()=="0"? false:true %>' runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lkselectDD" ToolTip="Viết Đánh Giá - Nhận xét học viên trong ngày" runat="server" CausesValidation="False" CommandName="Select"><i class="btn btn-circle btn-icon-only btn-default"><i class='<%# string.IsNullOrEmpty(Eval("GhiChu").ToString())?"glyphicon glyphicon-unchecked" :"glyphicon glyphicon-edit" %>'></i></i></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="30px" />
                        </asp:TemplateField>
                    </Columns>
                    <SelectedRowStyle BackColor="#D64635" ForeColor="Black" />
                    <HeaderStyle BackColor="#367CC2" ForeColor="White"></HeaderStyle>
                    <RowStyle BackColor="#FAF3DF"></RowStyle>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
