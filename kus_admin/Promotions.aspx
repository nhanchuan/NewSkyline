<%@ Page Title="" Language="C#" MasterPageFile="~/GlobalMasterPage.master" AutoEventWireup="true" CodeFile="Promotions.aspx.cs" Inherits="kus_admin_Promotions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../App_Themes/admin/pagination.css" rel="stylesheet" />
    <style type="text/css">
        .page_disabled {
            background: #D64635;
        }
    </style>
    <!-- BEGIN PAGE HEADER-->
    <h1 class="page-title">Chương Trình Khuyến Mãi
        <asp:Label ID="lblPhieuGhiDanh" CssClass="bold" runat="server"></asp:Label>
    </h1>
    <div class="page-bar">
        <ul class="page-breadcrumb">
            <li>
                <i class="fa fa-home"></i>
                <a href="../Default.aspx">Home</a>
                <i class="fa fa-angle-right"></i>
            </li>
            <li>
                <a href="#">Quản lý</a>
                <i class="fa fa-angle-right"></i>
            </li>
            <li>
                <a href="../kus_admin/Promotions.aspx">Chương trình khuyến mãi</a>
            </li>
        </ul>
    </div>
    <!-- END PAGE HEADER-->
    <%-- Pages is Valid --%>
    <div class="row">
        <div class="col-lg-12">
            <div class="alert alert-danger display-none" id="alertPageValid" runat="server">
                <asp:Label ID="lblPageValid" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <%--End Pages is Valid --%>
    <div class="col-lg-4">
        <div class="row">
            <h2>Thêm chương trình khuyến mãi</h2>
            <div class="form-group">
                <label>Mã Khuyến Mãi</label>
                <asp:TextBox ID="txtPromCode" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPromCode" ValidationGroup="validNewPromotion" runat="server" ErrorMessage="Yêu cầu nhập mã !" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtPromCode" ForeColor="Red" Display="Dynamic" ValidationGroup="validNewPromotion" ValidationExpression="(.){0,100}$" runat="server" ErrorMessage="Tên từ 0-100 ký tự !"></asp:RegularExpressionValidator>
            </div>
            <div class="form-group">
                <label>Tên Khuyến Mãi</label>
                <asp:TextBox ID="txtPromName" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Rate (%)</label>
                <asp:TextBox ID="txtReducedRate" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <a id="btniscustom" class="label label-info" onserverclick="btniscustom_ServerClick" runat="server"><i class="fa fa-sort"></i>&nbsp Switch</a>
            </div>
            <div class="form-group">
                <label>Discount</label>
                <asp:TextBox ID="txtDiscount" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Ngày bắt đầu</label>
                <%-- Date picker --%>
                <div class="input-group date date-picker" data-date-format="dd-mm-yyyy">
                    <asp:TextBox ID="txtStartDate" CssClass="form-control" runat="server"></asp:TextBox>
                    <span class="input-group-btn">
                        <button class="btn default" type="button"><i class="fa fa-calendar"></i></button>
                    </span>
                </div>
                <%-- Date picker --%>
            </div>
            <div class="form-group">
                <label>Ngày Kết Thúc</label>
                <%-- Date picker --%>
                <div class="input-group date date-picker" data-date-format="dd-mm-yyyy">
                    <asp:TextBox ID="txtEndDate" CssClass="form-control" runat="server"></asp:TextBox>
                    <span class="input-group-btn">
                        <button class="btn default" type="button"><i class="fa fa-calendar"></i></button>
                    </span>
                </div>
                <%-- Date picker --%>
                <asp:CompareValidator ID="cvtxtStartDate" runat="server"
                    ControlToCompare="txtStartDate" 
                    CultureInvariantValues="true"
                    Display="Dynamic" 
                    EnableClientScript="true"
                    ControlToValidate="txtEndDate"
                    ValidationGroup="validNewPromotion"
                    ForeColor="Red"
                    ErrorMessage="Start date must be earlier than end date"
                    Type="Date" SetFocusOnError="true" 
                    Operator="GreaterThanEqual"
                    Text="Thời điểm bắt đầu phải trước thời điểm kết thúc !">
                </asp:CompareValidator>
            </div>
            <div class="form-group">
                <label>Duyệt</label>
                <asp:CheckBox ID="chkIsActive" runat="server" />
            </div>
            <div class="form-group">
                <asp:Button ID="btnSavePromotions" ValidationGroup="validNewPromotion" CssClass="btn green pull-right" OnClick="btnSavePromotions_Click" runat="server" Text="Thêm Mới" />
            </div>
        </div>

    </div>
    <div class="col-lg-8">
        <a id="btneditpromotion" href="#modalEditPromotions" data-toggle="modal" runat="server">Chỉnh sửa thông tin khuyến mãi</a>
        <asp:GridView ID="gwPromotions" CssClass="table table-condensed" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="gwPromotions_SelectedIndexChanged"
            OnRowDataBound="gwPromotions_RowDataBound" OnRowDeleting="gwPromotions_RowDeleting">
            <Columns>
                <asp:TemplateField HeaderText="Mã Khuyến Mãi">
                    <ItemTemplate>
                        <asp:Label ID="lblPromCode" runat="server" Text='<%# Eval("PromCode") %>'></asp:Label>
                        <asp:Label ID="lblPromotionID" CssClass="display-none" runat="server" Text='<%# Eval("PromotionID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tên Khuyến Mãi">
                    <ItemTemplate>
                        <asp:Label ID="lblPromName" runat="server" Text='<%# Eval("PromName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Rate (%)">
                    <ItemTemplate>
                        <asp:Label ID="lblReducedRate" runat="server" Text='<%# (Eval("ReducedRate").ToString().Length==0)?"N/A":Eval("ReducedRate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Discount">
                    <ItemTemplate>
                        <asp:Label ID="lblDiscount" runat="server" Text='<%# (Eval("Discount").ToString().Length==0)?"N/A":Eval("Discount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Start Date">
                    <ItemTemplate>
                        <asp:Label ID="lblStartDate" runat="server" Text='<%# Eval("StartDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="End Date">
                    <ItemTemplate>
                        <asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("EndDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Trạng thái">
                    <ItemTemplate>
                        <i class='<%# (Eval("IsActive").ToString().Length==0)?"glyphicon glyphicon-unchecked" : Convert.ToBoolean(Eval("IsActive"))?"glyphicon glyphicon-check":"glyphicon glyphicon-unchecked" %>'></i>&nbsp Duyệt
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
                        <asp:LinkButton ID="lkSelect" runat="server" CausesValidation="False" CommandName="Select" Text="Chọn"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="50px" />
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#3AC0F2" ForeColor="White"></HeaderStyle>
            <SelectedRowStyle BackColor="#79B782" ForeColor="Black" />
            <RowStyle BackColor="#A1DCF2"></RowStyle>
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
                                    CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "btn btn-default page_enabled" : "btn btn-default page_disabled" %>'
                                    OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <!-- END PAGINATOR -->
        </div>
    </div>

    <%-- Modal New Khoa Hoc --%>
    <div id="modalEditPromotions" class="modal fade modal-scroll" tabindex="-1" data-replace="true" role="dialog" data-backdrop="static" data-keyboard="false" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <h2><i class="fa fa-list"></i>Chỉnh sửa thông tin khuyến mãi</h2>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <label>Mã Khuyến Mãi</label>
                                <asp:TextBox ID="txtEPromCode" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtEPromCode" ValidationGroup="validEditPromotion" runat="server" ErrorMessage="Yêu cầu nhập mã !" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtEPromCode" ForeColor="Red" Display="Dynamic" ValidationGroup="validEditPromotion" ValidationExpression="(.){0,100}$" runat="server" ErrorMessage="Tên từ 0-100 ký tự !"></asp:RegularExpressionValidator>
                            </div>
                            <div class="form-group">
                                <label>Tên Khuyến Mãi</label>
                                <asp:TextBox ID="txtEPromName" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <label>Rate (%)</label>
                                        <asp:TextBox ID="txtEReducedRate" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <a id="btnswitchedit" class="label label-info" onserverclick="btnswitchedit_ServerClick" runat="server"><i class="fa fa-sort"></i>&nbsp Switch</a>
                                    </div>
                                    <div class="form-group">
                                        <label>Discount</label>
                                        <asp:TextBox ID="txtEDiscount" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="form-group">
                                <label>Ngày bắt đầu</label>
                                <%-- Date picker --%>
                                <div class="input-group date date-picker" data-date-format="dd-mm-yyyy">
                                    <asp:TextBox ID="txtEStartDate" CssClass="form-control" runat="server"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <button class="btn default" type="button"><i class="fa fa-calendar"></i></button>
                                    </span>
                                </div>
                                <%-- Date picker --%>
                            </div>
                            <div class="form-group">
                                <label>Ngày Kết Thúc</label>
                                <%-- Date picker --%>
                                <div class="input-group date date-picker" data-date-format="dd-mm-yyyy">
                                    <asp:TextBox ID="txtEEndDate" CssClass="form-control" runat="server"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <button class="btn default" type="button"><i class="fa fa-calendar"></i></button>
                                    </span>
                                </div>
                                <%-- Date picker --%>
                            </div>
                            <div class="form-group">
                                <label>Duyệt</label>
                                <asp:CheckBox ID="chkEIsActive" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <a class="btn btn-warning" data-dismiss="modal">Hủy</a>
                    <asp:Button ID="btnSaveEditPromotion" ValidationGroup="validEditPromotion" CssClass="btn btn-primary" OnClick="btnSaveEditPromotion_Click" runat="server" Text="Cập Nhật" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

