﻿<%@ Page Title="" Language="C#" MasterPageFile="~/GlobalMasterPage.master" AutoEventWireup="true" CodeFile="VideoUpload.aspx.cs" Inherits="Pages_VideoUpload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- BEGIN PAGE HEADER-->
    <h3 class="page-title">Thêm Videos
    </h3>
    <div class="page-bar">
        <ul class="page-breadcrumb">
            <li>
                <i class="fa fa-home"></i>
                <a href="../Default.aspx">Home</a>
                <i class="fa fa-angle-right"></i>
            </li>
            <li>
                <a href="#">Media Library</a>
                <i class="fa fa-angle-right"></i>
            </li>
            <li>
                <a href="../Pages/VideoUpload.aspx">Videos Upload</a>
            </li>
        </ul>
        <div class="page-toolbar">
            <div id="dashboard-report-range" class="pull-right tooltips btn btn-fit-height grey-salt" data-placement="top" data-original-title="Change dashboard date range">
                <i class="icon-calendar"></i>&nbsp;
						<span class="thin uppercase visible-lg-inline-block">&nbsp;</span>&nbsp;
						<i class="fa fa-angle-down"></i>
            </div>
        </div>
    </div>
    <!-- END PAGE HEADER-->
    <div class="panel-default">
        <div class="panel-body">
            <h1>Thêm videos</h1>
            <hr />
            <div class="col-lg-5">

                <div class="clearfix"></div>
                <%--<div class="form-group">
                    <label class="control-label">Chọn mục loại Video</label>
                    <asp:DropDownList ID="dlVideoType" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <label class="control-label">Mô tả ngắn</label>
                    <asp:TextBox ID="txtShortDesciption" CssClass="form-control" runat="server"></asp:TextBox>
                    <i>Mô tả ngắn cho đoạn Video (Không quá 200 ký tự)</i>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtShortDesciption" Display="Dynamic" ForeColor="Red" ValidationGroup="validFileVideoUpload" ValidationExpression="(.){0,200}" runat="server" ErrorMessage="Mô tả ngắn không quá 200 ký tự !"></asp:RegularExpressionValidator>
                </div>
                <br />--%>
                <%--<h3>Chọn Video upload</h3>
                <br />
                <i>Chỉ hổ trợ Upload file MP4, FLV, Dung lượng Tải lên tối đa <strong>2 GB</strong></i>
                <div class="form-group">
                    <asp:AjaxFileUpload ID="UploaderVideo" MaximumNumberOfFiles="10" AllowedFileTypes="mp4,MP4,flv,FLV" OnUploadComplete="UploaderVideo_UploadComplete" runat="server" />
                    <br />
                    <div class="form-group pull-right">
                        <strong>Back to Video Library </strong><a class="btn green" href="../Pages/VideoManager.aspx"><i class="fa fa-backward"></i>Click here</a>
                    </div>
                </div>--%>


                <div class="form-group">
                    <label>Chuyên mục videos</label>
                    <asp:DropDownList ID="dlvideoscategory" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <label>Tên video</label>
                    <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Link</label>
                    <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Mô tả ngắn</label>
                    <asp:TextBox ID="TextBox4" TextMode="MultiLine" Rows="3" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <div class="form-group">
                    <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Thêm Video" />
                </div>
            </div>
            <div class="col-lg-7">
                <%--<a class="player" style="height: 500px; width: 100%; display: block;" href="../Handler/ReaderVideo.ashx?videoId=2"></a>--%>
                <asp:GridView ID="gwVideos" CssClass="table table-condensed" runat="server"
                    AutoGenerateColumns="False" RowStyle-BackColor="#A1DCF2" Font-Names="Arial" Font-Size="10pt"
                    HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
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
    <script src="../libs/FlowPlayer/flowplayer-3.2.12.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        flowplayer("a.player", "../libs/FlowPlayer/flowplayer-3.2.16.swf", {
            plugins: {
                pseudo: { url: "../libs/FlowPlayer/flowplayer.pseudostreaming-3.2.12.swf" }
            },
            clip: { provider: 'pseudo', autoPlay: false },
        });
    </script>


</asp:Content>

