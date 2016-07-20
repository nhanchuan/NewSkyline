<%@ Page Title="" Language="C#" MasterPageFile="~/GlobalMasterPage.master" AutoEventWireup="true" CodeFile="Popups.aspx.cs" Inherits="Pages_Popups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../App_Themes/admin/pagination.css" rel="stylesheet" />
    <!-- BEGIN PAGE HEADER-->
    <h1 class="page-title">Popups manager
    </h1>
    <div class="page-bar">
        <ul class="page-breadcrumb">
            <li>
                <i class="fa fa-home"></i>
                <a href="../Default.aspx">Home</a>
                <i class="fa fa-angle-right"></i>
            </li>
            <li>
                <a href="#">Setting</a>
                <i class="fa fa-angle-right"></i>
            </li>
            <li>
                <a href="../Pages/Popups.aspx">Popups</a>
            </li>
        </ul>
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
    <%-- CONTENT --%>
    <div class="row">
        <div class="col-lg-12 margin-bottom-30">
            <div class="row margin-bottom-25">
                <div class="col-lg-12">
                    <a id="btnfixPopupInfor" href="#modalEditPopup" data-toggle="modal" runat="server"><i class="fa fa-cog"></i>&nbsp Edit popup infor</a>
                    <asp:Label ID="lblConfirm" runat="server"></asp:Label>
                </div>
            </div>
            <asp:GridView ID="gwPopups" CssClass="table table-condensed table-responsive" runat="server" AutoGenerateColumns="False" RowStyle-BackColor="#A1DCF2" Font-Names="Arial" Font-Size="10pt"
                HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
                OnRowDataBound="gwPopups_RowDataBound" OnRowDeleting="gwPopups_RowDeleting" OnSelectedIndexChanged="gwPopups_SelectedIndexChanged">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <img src='<%# "../" + Eval("PopupUrl") %>' style="width: 60px; height: auto;" />
                            <asp:Label ID="lblID" CssClass="display-none" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Permalink">
                        <ItemTemplate>
                            <asp:Label ID="lblPermalink" runat="server" Text='<%# Eval("Permalink") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Short Description">
                        <ItemTemplate>
                            <asp:Label ID="lblShortDescription" runat="server" Text='<%# Eval("ShortDescription") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="View On Pages">
                        <ItemTemplate>
                            <asp:Label ID="lblViewOnPage" runat="server" Text='<%# Eval("ViewOnPage") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Upload by">
                        <ItemTemplate>
                            <asp:Label ID="lblUploadBy" runat="server" Text='<%# Eval("LastName")+" "+Eval("FirstName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Redirect Link">
                        <ItemTemplate>
                            <asp:Label ID="lblRedirectLink" runat="server" Text='<%# Eval("RedirectLink") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="For POST">
                        <ItemTemplate>
                            <asp:Label ID="lblPostTitle" runat="server" Text='<%# Eval("PostTitle") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Show">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkShow" Text="Show" Checked='<%# Eval("PopupStatus") %>' AutoPostBack="true" OnCheckedChanged="chkShow_CheckedChanged" runat="server" />
                            <asp:HiddenField ID="hiddenField1" Value='<%# Eval("ID").ToString() %>' runat="server" />
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
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="Select"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="50px" />
                    </asp:TemplateField>
                </Columns>
                <SelectedRowStyle BackColor="#79B782" ForeColor="Black" />
                <HeaderStyle BackColor="#FFB848" ForeColor="White"></HeaderStyle>
                <RowStyle BackColor="#FAF3DF"></RowStyle>
            </asp:GridView>
            <div class="row">
                <div class="col-lg-12">
                    <div class="form-group">
                        <!-- BEGIN PAGINATOR -->
                        <div class="pagination_lst pull-right">
                            <asp:Repeater ID="rptPager" runat="server">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                        CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "btn btn-default page_enabled" : "btn btn-default page_disabled" %>'
                                        OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="panel panel-danger">
            <div class="panel-body">
                <div class="col-lg-8">
                    <h2>Popup Infor</h2>
                    <div class="form-group">
                        <label>Permalink</label>
                        <asp:TextBox ID="txtPermalink" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Short Description</label>
                        <asp:TextBox ID="txtShortDescription" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>View On Page</label>
                        <asp:TextBox ID="txtViewOnPage" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Redirect Link</label>
                        <asp:TextBox ID="txtRedirectLink" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>For POST</label>
                        <asp:DropDownList ID="dlforPost" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="form-group">
                        <label>Upload File</label>
                        <p>
                            You can upload JPG, GIF, or PNG file. Maximum file size is 4MB.
                        </p>
                        <asp:FileUpload ID="FileImgUpload" CssClass="form-control margin-bottom-25" onchange="previewFile()" runat="server" />
                        <asp:RequiredFieldValidator ErrorMessage="Required"
                            ControlToValidate="FileImgUpload" ValidationGroup="validFileImgUpload"
                            runat="server" Display="Dynamic" ForeColor="Red" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.jpg|.gif|.png|.JPG|.GIF|.PNG)$"
                            ControlToValidate="FileImgUpload"
                            ValidationGroup="validFileImgUpload"
                            runat="server" ForeColor="Red"
                            ErrorMessage="Please select a valid Images file !"
                            Display="Dynamic" />
                        <div class="clearfix"></div>
                        <div class="col-lg-2"></div>
                        <div class="col-lg-8">
                            <asp:Image ID="imgUpload" CssClass="img-responsive" runat="server" />
                        </div>
                        <div class="col-lg-2"></div>
                    </div>
                </div>
            </div>
            <div class="panel-footer text-right">
                <asp:Button ID="btnNewPopup" CssClass="btn btn-primary" ValidationGroup="validFileImgUpload" OnClick="btnNewPopup_Click" runat="server" Text="New Popups" />
            </div>
        </div>
    </div>
    <%-- END  CONTENT --%>
    <%-- Modal edit PP --%>
    <div class="modal fade" id="modalEditPopup" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false" aria-hidden="true">
        <div class="modal-dialog modal-full">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">Fix Popup    
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-8">
                            <h2>Popup Infor</h2>
                            <div class="form-group">
                                <label>Permalink</label>
                                <asp:TextBox ID="txtEPermalink" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Short Description</label>
                                <asp:TextBox ID="txtEShortDescription" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>View On Page</label>
                                <asp:TextBox ID="txtEVireOnPage" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Redirect Link</label>
                                <asp:TextBox ID="txtERedirectLink" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>For POST</label>
                                <asp:DropDownList ID="dlEForPost" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label>Upload File</label>
                                <p>
                                    You can upload JPG, GIF, or PNG file. Maximum file size is 4MB.
                                </p>
                                <asp:FileUpload ID="FileUploadUpdateImg" CssClass="form-control margin-bottom-25" onchange="previewFileUpdate()" runat="server" />
                                <asp:RequiredFieldValidator ErrorMessage="Required"
                                    ControlToValidate="FileUploadUpdateImg" ValidationGroup="validUploadFileImgUpload"
                                    runat="server" Display="Dynamic" ForeColor="Red" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.jpg|.gif|.png|.JPG|.GIF|.PNG)$"
                                    ControlToValidate="FileUploadUpdateImg"
                                    ValidationGroup="validUploadFileImgUpload"
                                    runat="server" ForeColor="Red"
                                    ErrorMessage="Please select a valid Images file !"
                                    Display="Dynamic" />
                                <div class="clearfix"></div>
                                <div class="col-lg-2"></div>
                                <div class="col-lg-8">
                                    <asp:Image ID="ImageUpdate" CssClass="img-responsive" runat="server" />
                                </div>
                                <div class="col-lg-2"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnUpdatePop" CssClass="btn btn-primary" ValidationGroup="validUploadFileImgUpload" runat="server" Text="Update Popups" />
                </div>
            </div>
        </div>
    </div>
    <%-- End Modal Edit PP --%>


    <script type="text/javascript">
        function previewFile() {
            var preview = document.querySelector('#<%=imgUpload.ClientID %>');
            var file = document.querySelector('#<%=FileImgUpload.ClientID %>').files[0];
            var reader = new FileReader();
            <%--document.getElementById('<%=HiddenUploadimg.ClientID %>').value = "";
            document.getElementById('<%=txtTenAnh.ClientID %>').value = "Image tag name";--%>
            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }
        function previewFileUpdate() {
            var preview = document.querySelector('#<%=ImageUpdate.ClientID %>');
            var file = document.querySelector('#<%=FileUploadUpdateImg.ClientID %>').files[0];
            var reader = new FileReader();
            <%--document.getElementById('<%=HiddenUploadimg.ClientID %>').value = "";
            document.getElementById('<%=txtTenAnh.ClientID %>').value = "Image tag name";--%>
            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }
    </script>
</asp:Content>

