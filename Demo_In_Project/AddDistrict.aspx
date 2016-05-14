<%@ Page Title="" Language="C#" MasterPageFile="~/GlobalMasterPage.master" AutoEventWireup="true" CodeFile="AddDistrict.aspx.cs" Inherits="Demo_In_Project_AddDistrict" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-lg-6">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="form-group">
                        <asp:DropDownList ID="dlcountry" AutoPostBack="true" OnSelectedIndexChanged="dlcountry_SelectedIndexChanged" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="valcountry" runat="server"
                            ControlToValidate="dlcountry"
                            ValidationGroup="validAdd"
                            Display="Dynamic"
                            ForeColor="Red"
                            ErrorMessage="Chưa chọn !"
                            InitialValue="0">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <asp:DropDownList ID="dlProvince" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ControlToValidate="dlProvince"
                            ValidationGroup="validAdd"
                            Display="Dynamic"
                            ForeColor="Red"
                            ErrorMessage="Chưa chọn !"
                            InitialValue="0">
                        </asp:RequiredFieldValidator>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="form-group">
                <asp:Label ID="lblcheckk" ForeColor="Red" runat="server"></asp:Label>
            </div>
            <div class="form-group">
                <asp:Button ID="btntem" CssClass="btn btn-primary" ValidationGroup="validAdd" OnClick="btntem_Click" runat="server" Text="Thèm" />
                <asp:Button ID="btnclear" CssClass="btn btn-danger" OnClick="btnclear_Click" runat="server" Text="clee" />
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtDistrict" TextMode="MultiLine" Rows="100" Width="500" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="col-lg-6">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="form-group">
                        <asp:DropDownList ID="dlcheckCountry" AutoPostBack="true" OnSelectedIndexChanged="dlcheckCountry_SelectedIndexChanged" runat="server"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:DropDownList ID="dlcheckProvinces" AutoPostBack="true" OnSelectedIndexChanged="dlcheckProvinces_SelectedIndexChanged" runat="server"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:GridView ID="gwdata" runat="server"></asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

