﻿<%@ Page Title="" Language="C#" MasterPageFile="~/GlobalMasterPage.master" AutoEventWireup="true" CodeFile="Post-All.aspx.cs" Inherits="Pages_Post_All" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .background {
            background: url('../../images/backgrounds/noise_light-grey.jpg');
            font-family: 'Helvetica Neue', arial, sans-serif;
            font-weight: 200;
        }

        .pagination_lst {
            border-radius: 0;
            margin: 0;
        }

            .pagination_lst > a {
                font-size: 18px;
                display: inline-block;
                margin-left: 5px;
                border-radius: 1px !important;
                border: none;
            }

                .pagination_lst > a:active {
                    background: #fb2712;
                }

                .pagination_lst > a:hover {
                    background: #555;
                }

        .page_disabled {
            background: #E85507;
        }
    </style>
    <!-- BEGIN PAGE HEADER-->
    <h1 class="page-title">Bài Viết <small>Post manager</small>
    </h1>
    <div class="page-bar">
        <ul class="page-breadcrumb">
            <li>
                <i class="fa fa-home"></i>
                <a href="../Default.aspx">Home</a>
                <i class="fa fa-angle-right"></i>
            </li>
            <li>
                <a href="../Pages/Post-All.aspx">Bài viết</a>
                <i class="fa fa-angle-right"></i>
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
    <div id="PostManager" runat="server">
        <div class="col-lg-12 margin-bottom-30">
            <a class="btn green" href="../Pages/Post-New.aspx">Viết bài mới</a>
        </div>
        <hr />
        <div class="clearfix"></div>
        <div class="col-lg-12">
            <!-- BEGIN USERS TABLE PORTLET-->
            <div class="portlet box blue">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-edit"></i>Bài viết
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse"></a>
                        <a href="javascript:;" class="remove"></a>
                    </div>
                </div>
                <div class="portlet-body background">
                    <div class="row">
                        <div class="col-lg-2">
                            <asp:DropDownList ID="dlFilterStatus" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="dlFilterStatus_SelectedIndexChanged" runat="server">
                                <asp:ListItem>-- Trạng thái --</asp:ListItem>
                                <asp:ListItem Value="0">Chờ Xét Duyệt</asp:ListItem>
                                <asp:ListItem Value="1">Đăng Bài</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-2">
                            <asp:DropDownList ID="dlCategory" Style="margin-left: 100px;" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="dlCategory_SelectedIndexChanged" runat="server"></asp:DropDownList>
                        </div>
                        <div class="col-lg-3"></div>
                        <div class="col-lg-5">
                            <div class="input-group">
                                <div class="input-icon">
                                    <i class="fa fa-search"></i>
                                    <input id="txtsearchVideo" class="form-control" type="text" placeholder="Search Post" runat="server" />
                                </div>
                                <span class="input-group-btn">
                                    <button id="btnsearchPost" class="btn btn-success" type="button" runat="server"><i class="fa fa-arrow-left fa-fw"></i>Search</button>
                                </span>
                            </div>
                        </div>
                    </div>
                    <br />
                    <asp:GridView ID="gwPostmanager" CssClass="table table-condensed" runat="server" AutoGenerateColumns="False" RowStyle-BackColor="#A1DCF2" Font-Names="Arial" Font-Size="10pt"
                        HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White" OnRowDataBound="gwPostmanager_RowDataBound" OnRowDeleting="gwPostmanager_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="Ảnh Bài viết">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ImagesUrl") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <img src='<%# "../" + Eval("ImagesUrl") %>' style="width: 60px; height: auto;" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tiêu Đề Bài Viết">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("PostTitle") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPostID" CssClass="display-none" runat="server" Text='<%# Eval("PostID") %>'></asp:Label>
                                    <a class="control-label" style="font-size: larger; text-transform: uppercase; color: black;" href='<%#"../Pages/Post-Update.aspx?PostCode="+Eval("PostID") %>'>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("PostTitle") %>'></asp:Label>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Meta Tag ">
                                <ItemTemplate>
                                    <label class="bold"><i class="fa fa-key"></i>Meta Keywords :</label>
                                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("MetaKeywords") %>'></asp:Label><br />
                                    <label class="bold"><i class="fa fa-list"></i>Meta Description :</label>
                                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("MetaDescription") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Lịch Đăng">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("PostModified") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("PostModified","{0:dd/MM/yyyy hh:mm:ss tt}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ngày tạo">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("DateOfCreate") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("DateOfCreate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Người Đăng">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("UserName") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trạng thái">
                                <ItemTemplate>
                                    <i class='<%# (Eval("PostStatus").ToString().Length==0)?"glyphicon glyphicon-unchecked" : Convert.ToBoolean(Eval("PostStatus"))?"glyphicon glyphicon-check":"glyphicon glyphicon-unchecked" %>'></i>&nbsp Duyệt
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Lượt xem">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("ViewCount") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("ViewCount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%--<asp:LinkButton ID="linkBtnDelPostItem" runat="server" CausesValidation="False" CommandName="Delete" ToolTip="Delete" Text="Delete"><img src="../images/icon/Actions-edit-delete-icon.png" width="20" height="20" /></asp:LinkButton>--%>
                                    <asp:LinkButton ID="linkBtnDelPostItem" CssClass="btn btn-circle btn-icon-only btn-default" runat="server" CausesValidation="False" CommandName="Delete" ToolTip="Delete" Text="Delete"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="30px" />
                            </asp:TemplateField>
                        </Columns>

                        <HeaderStyle BackColor="#3AC0F2" ForeColor="White"></HeaderStyle>

                        <RowStyle BackColor="#A1DCF2"></RowStyle>
                    </asp:GridView>
                </div>
            </div>
            <div class="form-group">
                <!-- BEGIN PAGINATOR -->
                <div class="row">
                    <div class="col-md-12 col-sm-12">
                        <div class="pagination_lst pull-right">
                            <asp:Repeater ID="rptPager" runat="server">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                        CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                                        OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div class="clearfix"></div>
                            <asp:Repeater ID="rptStt" runat="server">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkSttPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                        CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                                        OnClick="Stt_Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
                <!-- END PAGINATOR -->
            </div>
        </div>
    </div>

</asp:Content>

