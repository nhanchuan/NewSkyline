<%@ Page Title="" Language="C#" MasterPageFile="~/GlobalMasterPage.master" AutoEventWireup="true" CodeFile="VideoUpload.aspx.cs" Inherits="Pages_VideoUpload" %>

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
    <%-- Pages is Valid --%>
    <div class="row">
        <div class="col-lg-12">
            <div class="alert alert-danger display-none" id="alertPageValid" runat="server">
                <asp:Label ID="lblPageValid" runat="server"></asp:Label>
            </div>

        </div>
    </div>
    <%--End Pages is Valid --%>
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
                    <asp:TextBox ID="txtVideosname" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Link</label>
                    <asp:TextBox ID="txtLink" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Mô tả ngắn</label>
                    <asp:TextBox ID="txtShortdescrition" TextMode="MultiLine" Rows="3" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Button ID="btnAddVideos" CssClass="btn btn-primary" OnClick="btnAddVideos_Click" runat="server" Text="Thêm Video" />
                </div>
            </div>
            <div class="col-lg-7">
                <a id="btnEditVideo" data-toggle="modal" href="#modalEditVideo" runat="server"><i class="fa fa-cog"></i>Chỉnh sửa thông tin video</a>
                <%--<a class="player" style="height: 500px; width: 100%; display: block;" href="../Handler/ReaderVideo.ashx?videoId=2"></a>--%>
                <asp:GridView ID="gwVideos" CssClass="table table-condensed" runat="server"
                    AutoGenerateColumns="False" RowStyle-BackColor="#A1DCF2" Font-Names="Arial" Font-Size="10pt"
                    HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White" OnRowDataBound="gwVideos_RowDataBound" OnRowDeleting="gwVideos_RowDeleting" OnSelectedIndexChanged="gwVideos_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <img src='<%# hinhvideo(Eval("VideoUrl").ToString()) %>' style="width: 60px; height: auto;" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Videos Name">
                            <ItemTemplate>
                                <a href='<%# Eval("VideoUrl") %>' target="_blank" class="bold">
                                    <asp:Label ID="lblVideoName" runat="server" Text='<%# Eval("VideoName") %>'></asp:Label></a>
                                <asp:Label ID="lblVideoID" CssClass="display-none" runat="server" Text='<%# Eval("VideoID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <i class="fa fa-clock-o"></i>
                                <asp:Label ID="lblDateOfCreate" runat="server" Text='<%# Eval("DateOfCreate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="linkBtnDel" CssClass="btn btn-circle btn-icon-only btn-default" runat="server" CausesValidation="False" CommandName="Delete" ToolTip="Delete" Text="Delete"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="30px" />
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
                            </div>
                        </div>
                    </div>
                    <!-- END PAGINATOR -->
                </div>

            </div>
        </div>
    </div>

    <%-- Modal Edit Video --%>
    <div id="modalEditVideo" class="modal fade modal-scroll" tabindex="-1" data-replace="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <i class="fa fa-video-camera"></i>Video Infor
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <label>Chuyên mục videos</label>
                                <asp:DropDownList ID="dlEVideoCatwegory" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Tên video</label>
                                <asp:TextBox ID="txtEvideoname" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Link</label>
                                <asp:TextBox ID="txtELink" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Mô tả ngắn</label>
                                <asp:TextBox ID="txtEShortDesc" TextMode="MultiLine" Rows="3" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" data-dismiss="modal" class="btn">Close</button>
                    <asp:Button ID="btnSeveEdit" CssClass="btn btn-primary" OnClick="btnSeveEdit_Click" runat="server" Text="Lưu Thay Đổi" />
                </div>
            </div>
        </div>
    </div>
    <%--End Modal Edit Video --%>


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

