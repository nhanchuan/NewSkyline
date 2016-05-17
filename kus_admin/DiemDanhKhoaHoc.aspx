<%@ Page Title="" Language="C#" MasterPageFile="~/GlobalMasterPage.master" AutoEventWireup="true" CodeFile="DiemDanhKhoaHoc.aspx.cs" Inherits="kus_admin_DiemDanhKhoaHoc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- BEGIN PAGE HEADER-->
    <h1 class="page-title">Điểm Danh Khóa Học : <asp:Label ID="lblTitlekhoahoc" runat="server"></asp:Label> , Ngày Khai Giảng : <asp:Label ID="lblTitleNKG" runat="server" Text="Label"></asp:Label>
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
       <div class="col-lg-4">
           <h3 style="color: #0078D7;">Ngày điểm danh</h3>
           <div class="form-group">
               <asp:GridView ID="gwNgayDiemDanh" runat="server"></asp:GridView>
           </div>
       </div>
    </div>
    <asp:Label ID="lblPageisValid" ForeColor="Red" runat="server"></asp:Label>
</asp:Content>