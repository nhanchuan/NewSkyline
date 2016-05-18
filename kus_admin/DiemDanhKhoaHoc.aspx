<%@ Page Title="" Language="C#" MasterPageFile="~/GlobalMasterPage.master" AutoEventWireup="true" CodeFile="DiemDanhKhoaHoc.aspx.cs" Inherits="kus_admin_DiemDanhKhoaHoc" %>

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
        <div class="col-lg-4">
            <h3 style="color: #0078D7;">Ngày điểm danh</h3>
            <div class="form-group">
                <a id="btnLoadLichDiemDanh" class="btn green" onserverclick="btnLoadLichDiemDanh_ServerClick" runat="server">View List <i class="glyphicon glyphicon-collapse-down"></i></a>
            </div>
            <div class="form-group">
                <asp:Label ID="lblMessageDiemDanh" ForeColor="Red" runat="server"></asp:Label>
            </div>
            <div id="panelLichDiemDanh" runat="server">
                <div class="form-group">
                    <asp:GridView ID="gwNgayDiemDanh" CssClass="table table-condensed" AutoGenerateColumns="false" RowStyle-BackColor="#A1DCF2" Font-Names="Arial" Font-Size="10pt"
                        HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White" runat="server">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <i class="glyphicon glyphicon-check"></i>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ngày điểm danh">
                                <ItemTemplate>
                                    <asp:Label ID="lblNgayDiemDanh" runat="server" Text='<%# Eval("NgayDiemDanh","{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="Select"><i class="btn btn-circle btn-icon-only btn-default"><i class="glyphicon glyphicon-share-alt"></i></i></asp:LinkButton>
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
    </div>
    <asp:Label ID="lblPageisValid" ForeColor="Red" runat="server"></asp:Label>
</asp:Content>
