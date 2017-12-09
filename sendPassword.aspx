<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="sendPassword.aspx.cs" Inherits="sendPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width:25%;margin-left:200px;margin-right:auto">
        <div class="form-group">
            <asp:TextBox ID="txt_email" placeholder="something@example.com" class="form-control" type="email"  aria-describedby="emailHelp" runat="server" ></asp:TextBox>
            <asp:RequiredFieldValidator ID="vld_required_txt_email" runat="server" ErrorMessage="The e-mail is required" ControlToValidate="txt_email" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="vld_regex_txt_email" runat="server" ErrorMessage="The e-mail address is invalid" ControlToValidate="txt_email" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
            <asp:CustomValidator ID="vld_existence_txt_email" runat="server" ErrorMessage="There is no account associated with this email" ControlToValidate="txt_email" Display="Dynamic" ForeColor="Red" OnServerValidate="vld_existence_txt_email_ServerValidate"></asp:CustomValidator>
            <asp:Label ID="lbl_success_sendPassword" runat="server" ForeColor="Red"></asp:Label>
        </div>
        
        <asp:Button ID="btn_submit" runat="server" Text="Send password" class="btn btn-primary" OnClick="btn_submit_Click"/>
    </div>
</asp:Content>

