<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
        <div style="width:250px;margin-left:auto;margin-right:auto">
                <div class="form-group">
                    <label for="txt_email">E-mail:</label>
                    <asp:TextBox ID="txt_username" placeholder="something@example.com" class="form-control" type="email"  aria-describedby="usernameHelp" runat="server" ></asp:TextBox>
                    <asp:RegularExpressionValidator ID="vld_validemail" runat="server" ErrorMessage="The e-mail address is invalid" ControlToValidate="txt_username" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="vld_username" runat="server" ErrorMessage="The email or username is required" ControlToValidate="txt_username" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="vld_regex_username" runat="server" ErrorMessage="This field contains characteres not permited" ForeColor="Red" Display="Dynamic" ControlToValidate="txt_username" ValidationExpression="[^'`\s]+"></asp:RegularExpressionValidator>
                </div>
                <div class="form-group">
                    <label for="txt_password">Password:</label>
                    <asp:TextBox ID="txt_password" type="password" class="form-control"  aria-describedby="passwordHelp" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="vld_password" runat="server" ErrorMessage="A password is necesary to access your account" ControlToValidate="txt_password" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="vld_regex_password" runat="server" ErrorMessage="This field contains characteres not permited" ForeColor="Red" Display="Dynamic" ControlToValidate="txt_password" ValidationExpression="[^'`\s]+"></asp:RegularExpressionValidator>
                    <asp:CustomValidator ID="vld_login_credentials" runat="server" ErrorMessage="The username or password are incorrect" ForeColor="Red" Display="Dynamic" ControlToValidate="txt_password" OnServerValidate="vld_login_credentials_ServerValidate"></asp:CustomValidator>
                </div>
                <asp:Button ID="btn_submit" runat="server" Text="Sign in" class="btn btn-primary" OnClick="btn_submit_Click" />

            <asp:LinkButton ID="lnk_singup" runat="server" CausesValidation="False" OnClick="lnk_singup_Click">Sign up</asp:LinkButton> | <asp:LinkButton ID="lnk_resetpassword" runat="server" CausesValidation="False" OnClick="lnk_resetpassword_Click">Forgot password?</asp:LinkButton>
        </div>        
    </p>
</asp:Content>

