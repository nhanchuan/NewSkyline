<%@ Page Title="" Language="C#" MasterPageFile="~/GlobalMasterPage.master" AutoEventWireup="true" CodeFile="Popups.aspx.cs" Inherits="Pages_Popups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <div class="col-lg-4">
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
            <label>Upload File</label>
            <p>
                You can upload JPG, GIF, or PNG file. 
Maximum file size is 4MB.
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
        <div class="form-group text-right">
            <asp:Button ID="btnNewPopup" CssClass="btn btn-primary" ValidationGroup="validFileImgUpload" OnClick="btnNewPopup_Click" runat="server" Text="New Popups" />
        </div>
    </div>
    <div class="col-lg-8">
        <div class="row margin-bottom-25">
            <div class="col-lg-12">
                <a id="btnfixPopupInfor" href="#modalEditIamgeCategory" data-toggle="modal" runat="server"><i class="fa fa-cog"></i>&nbsp Edit popup infor</a>
                <asp:Label ID="lblConfirm" runat="server"></asp:Label>
            </div>
        </div>

        <asp:GridView ID="gwPopups" CssClass="table table-condensed" runat="server" AutoGenerateColumns="False" RowStyle-BackColor="#A1DCF2" Font-Names="Arial" Font-Size="10pt"
            HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
            OnRowDataBound="gwPopups_RowDataBound" OnRowDeleting="gwPopups_RowDeleting" OnSelectedIndexChanged="gwPopups_SelectedIndexChanged">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <img src='<%# "../" + Eval("PopupUrl") %>' style="width: 60px; height: auto;" />
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
                                    CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                                    OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%-- END  CONTENT --%>
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
    </script>
</asp:Content>

