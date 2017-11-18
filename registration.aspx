<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="registration.aspx.cs" Inherits="registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <p>
        <div style="width:50%;margin-left:auto;margin-right:auto">
                <div class="form-group">
                    <label for="txt_username">Username:</label>
                    <asp:TextBox ID="txt_username" placeholder="Enter username" class="form-control"  aria-describedby="usernameHelp" runat="server"></asp:TextBox>
                    <small id="usernameHelp" class="form-text text-muted">This is your nickname in te comunity.</small>
                    <br />
                    <asp:RequiredFieldValidator ID="vld_username" runat="server" ErrorMessage="User name is required" ControlToValidate="txt_username" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label for="txt_firstname">First Name:</label>
                    <asp:TextBox ID="txt_firstname" placeholder="" class="form-control"  aria-describedby="firstnameHelp" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="vld_firstname" runat="server" ErrorMessage="First name is required" ControlToValidate="txt_firstname" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label for="txt_surname">Surname:</label>
                    <asp:TextBox ID="txt_surname" placeholder="" class="form-control"  aria-describedby="surnameHelp" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="vld_surname" runat="server" ErrorMessage="Surname is required" ControlToValidate="txt_surname" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label for="txt_email">E-mail:</label>
                    <asp:TextBox ID="txt_email" placeholder="something@example.com" class="form-control" type="email"  aria-describedby="emailHelp" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="vld_email" runat="server" ErrorMessage="The e-mail is required" ControlToValidate="txt_email" ForeColor="Red"></asp:RequiredFieldValidator>
                    <br />
                    <asp:RegularExpressionValidator ID="vld_validemail" runat="server" ErrorMessage="The e-mail address is invalid" ControlToValidate="txt_email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red"></asp:RegularExpressionValidator>
                </div>
                <div class="form-group">
                    <label for="txt_password">Password:</label>
                    <asp:TextBox ID="txt_password" type="password" class="form-control"  aria-describedby="passwordHelp" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="vld_password" runat="server" ErrorMessage="A password is necesary to access your account" ControlToValidate="txt_password" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label for="txt_password2">Confirm password:</label>
                    <asp:TextBox ID="txt_password2" type="password" class="form-control"  aria-describedby="passwordHelp" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:CompareValidator ID="vld_passwordcomparison" runat="server" ErrorMessage="Passwords don't match" ControlToCompare="txt_password" ControlToValidate="txt_password2" ForeColor="Red"></asp:CompareValidator>
                    <br />
                    <asp:RequiredFieldValidator ID="vld_password2" runat="server" ErrorMessage="Please confirm your password" ControlToValidate="txt_password2" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
                <asp:Button ID="btn_submit" runat="server" Text="Sign up" class="btn btn-primary" OnClick="btn_submit_Click" />
        </div>
    </p>
</asp:Content>

