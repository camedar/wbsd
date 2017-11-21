<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="confirmEmail.aspx.cs" Inherits="confirmAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
        <div style="width:20%;margin-left:5%;margin-right:auto">
             <div class="form-group">
                <label for="txt_confirmationCode">Confirmation code:</label>
                <asp:TextBox ID="txt_confirmationCode" placeholder="" class="form-control"  aria-describedby="confirmationCodeHelp" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="vld_confirmationCode" runat="server" ErrorMessage="The confirmation code is required" ControlToValidate="txt_confirmationCode" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="vld_regex_confirmationCode" runat="server" ErrorMessage="This field contains characteres not permited" ForeColor="Red" Display="Dynamic" ControlToValidate="txt_confirmationCode" ValidationExpression="[\w]*"></asp:RegularExpressionValidator>
                <asp:CustomValidator ID="vld_existence_confirmationCode" runat="server" ErrorMessage="The confirmation code is not valid" OnServerValidate="vld_existence_confirmationCode_ServerValidate" ForeColor="Red" Display="Dynamic"></asp:CustomValidator>
            </div>
            <asp:Button ID="btn_confirm" runat="server" Text="Confirm email" class="btn btn-primary" OnClick="btn_confirm_Click"/>
            <p>
                <div class="form-group">
                    <label for="txt_email">E-mail:</label>
                    <asp:TextBox ID="txt_email" placeholder="something@example.com" class="form-control" type="email"  aria-describedby="emailHelp" runat="server" ></asp:TextBox>
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbl_responseEmail" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lnk_resendCode" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>            
                </div>
                <asp:LinkButton ID="lnk_resendCode" runat="server" OnClick="lnk_resendCode_Click" CausesValidation="False">Resend Code</asp:LinkButton>
            </p>
        </div>
    </p>
</asp:Content>

