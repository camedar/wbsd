<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="account.aspx.cs" Inherits="account" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div id="divc" style="width: 500px;margin-left:100px;margin-right:auto">
            <div style="width:35%;float:left">
                <ul class="list-group">
                  <li class="list-group-item active" id="li_profile" runat="server">
                      <asp:LinkButton ID="lnk_edit_profile" runat="server" OnClick="lnk_edit_profile_Click" CausesValidation="False">Profile</asp:LinkButton>
                  </li>
                    <li class="list-group-item" id="li_newemail" runat="server">
                        <asp:LinkButton ID="lnk_edit_email" runat="server" OnClick="lnk_edit_email_Click">Edit E-mail</asp:LinkButton>
                    </li>
                    <li class="list-group-item" id="li_newpassword" runat="server">
                        <asp:LinkButton ID="lnk_edit_password" runat="server" OnClick="lnk_edit_password_Click">Edit Pasword</asp:LinkButton>
                    </li>
                    <li class="list-group-item" id="li_newusername" runat="server">
                        <asp:LinkButton ID="lnk_edit_username" runat="server" OnClick="lnk_edit_username_Click">Edit Username</asp:LinkButton>
                    </li>
                </ul>
            </div>
            <div style="width:55%;float:right">
                <asp:FormView ID="userForm" runat="server" DataSourceID="sql_datasource_user" DefaultMode="Edit" DataKeyNames="userId" OnItemUpdating="userForm_ItemUpdating" OnDataBound="userForm_DataBound">
                    <EditItemTemplate>
                            <div class="form-group">
                                <label for="txt_firstname">Firstname:</label>
                                <asp:TextBox ID="txt_firstname" runat="server" Text='<%# Bind("firstname") %>' class="form-control" aria-describedby="txt_firstnameHelp" ValidationGroup="grp_profile" />
                            </div>
                            <div class="form-group">
                                <label for="txt_surname">Surname:</label>
                                <asp:TextBox ID="txt_surname" runat="server" Text='<%# Bind("surname") %>'  class="form-control" aria-describedby="txt_surnameHelp" ValidationGroup="grp_profile" />
                            </div>
                            <div class="form-group">
                                <label for="txt_username">Username:</label>
                                <asp:TextBox ID="txt_username" runat="server" Text='<%# Bind("username") %>'  class="form-control" aria-describedby="txt_usernameHelp" ReadOnly="True" ValidationGroup="grp_profile" />
                            </div>
                            <div class="form-group">
                                <label for="emailTextBox">E-mail:</label>
                                <asp:TextBox ID="emailTextBox" runat="server" Text='<%# Bind("email") %>'  class="form-control" aria-describedby="emailTextBoxHelp" ReadOnly="True" ValidationGroup="grp_profile" />
                            </div>
                        <asp:Button ID="UpdateButton" runat="server"  class="btn btn-primary"  CausesValidation="True" CommandName="Update" Text="Update" ValidationGroup="grp_profile" />

                        &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        firstname:
                        <asp:TextBox ID="txt_firstname" runat="server" Text='<%# Bind("firstname") %>' />
                        <br />
                        surname:
                        <asp:TextBox ID="txt_surname" runat="server" Text='<%# Bind("surname") %>' />
                        <br />
                        username:
                        <asp:TextBox ID="txt_username" runat="server" Text='<%# Bind("username") %>' />
                        <br />
                        email:
                        <asp:TextBox ID="emailTextBox" runat="server" Text='<%# Bind("email") %>' />
                        <br />
                        password:
                        <asp:TextBox ID="txt_password" runat="server" Text='<%# Bind("password") %>' />
                        <br />
                        <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
                        &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                    </InsertItemTemplate>
                    <ItemTemplate>
                        firstname:
                        <asp:Label ID="firstnameLabel" runat="server" Text='<%# Bind("firstname") %>' />
                        <br />
                        surname:
                        <asp:Label ID="surnameLabel" runat="server" Text='<%# Bind("surname") %>' />
                        <br />
                        username:
                        <asp:Label ID="usernameLabel" runat="server" Text='<%# Bind("username") %>' />
                        <br />
                        email:
                        <asp:Label ID="emailLabel" runat="server" Text='<%# Bind("email") %>' />
                        <br />
                        password:
                        <asp:Label ID="passwordLabel" runat="server" Text='<%# Bind("password") %>' />
                        <br />
                    </ItemTemplate>
                </asp:FormView>

                
                <asp:Panel runat="server" ID="pnl_newPassword">
                    <div class="form-group">
                        <label for="txt_currentPassword">Current password:</label>
                        <asp:TextBox ID="txt_currentPassword" type="password" class="form-control"  aria-describedby="tx_currentPasswordHelp" runat="server" TextMode="Password"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="vld_required_currentPassword" runat="server" ErrorMessage="Plaese provide your current password" ControlToValidate="txt_currentPassword" ForeColor="Red" Display="Dynamic" ValidationGroup="grp_newUserPassword"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="vld_regex_currentPassword" runat="server" ErrorMessage="This field contains characteres not permited" ForeColor="Red" Display="Dynamic" ControlToValidate="txt_currentPassword" ValidationExpression="[^'`\s]+" ValidationGroup="grp_newUserPassword"></asp:RegularExpressionValidator>
                        <asp:CustomValidator ID="vld_verify_txt_currentPassword" runat="server" ErrorMessage="The current password is wrong" ForeColor="Red" Display="Dynamic" ControlToValidate="txt_currentPassword" ValidationExpression="[^'`\s]+" ValidationGroup="grp_newUserPassword" OnServerValidate="vld_verify_txt_currentPassword_ServerValidate"></asp:CustomValidator>
                    </div>
                    <div class="form-group">
                        <label for="txt_password1">New password:</label>
                        <asp:TextBox ID="txt_password1" type="password" class="form-control"  aria-describedby="txt_password1Help" runat="server" TextMode="Password"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="vld_required_txt_password1" runat="server" ErrorMessage="Please provide your new password" ControlToValidate="txt_password1" ForeColor="Red" Display="Dynamic" ValidationGroup="grp_newUserPassword"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="vld_regex_password1" runat="server" ErrorMessage="This field contains characteres not permited" ForeColor="Red" Display="Dynamic" ControlToValidate="txt_password1" ValidationExpression="[^'`\s]+" ValidationGroup="grp_newUserPassword"></asp:RegularExpressionValidator>
                    </div>
                    <div class="form-group">
                        <label for="txt_password2">Confirm new password:</label>
                        <asp:TextBox ID="txt_password2" type="password" class="form-control"  aria-describedby="txt_password2Help" runat="server" TextMode="Password"></asp:TextBox>

                        <asp:CompareValidator ID="vld_passwordcomparison" runat="server" ErrorMessage="Passwords don't match" ControlToCompare="txt_password1" ControlToValidate="txt_password2" ForeColor="Red" Display="Dynamic" ValidationGroup="grp_newUserPassword"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="vld_required_password2" runat="server" ErrorMessage="Please confirm your new password" ControlToValidate="txt_password2" ForeColor="Red" Display="Dynamic" ValidationGroup="grp_newUserPassword"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="vld_regex_password2" runat="server" ErrorMessage="This field contains characteres not permited" ForeColor="Red" Display="Dynamic" ControlToValidate="txt_password2" ValidationExpression="[^'`\s]+" ValidationGroup="grp_newUserPassword"></asp:RegularExpressionValidator>
                    </div>
                    <asp:Button ID="btn_newPassword" runat="server" Text="Update" class="btn btn-primary" ValidationGroup="grp_newUserPassword" OnClick="btn_newPassword_Click"/>
                    
                </asp:Panel>

                <asp:Panel runat="server" ID="pnl_newUserName">
                    <div class="form-group">
                        <label for="txt_newUsername">New username:</label>
                        <asp:TextBox ID="txt_newUsername" runat="server" Text=''  class="form-control" aria-describedby="txt_newUsernameHelp" />
                        <asp:CustomValidator ID="vld_existence_txt_newUsername" runat="server" ErrorMessage="This username is already in use" ValidationGroup="grp_newUserName" ControlToValidate="txt_newUsername" Display="Dynamic" ForeColor="Red" OnServerValidate="vld_existence_txt_newUsername_ServerValidate"></asp:CustomValidator>
                    </div>
                    <asp:Button ID="btn_newUsername" runat="server" Text="Update" class="btn btn-primary" ValidationGroup="grp_newUserName" OnClick="btn_newUsername_Click" />
                </asp:Panel>
                
                <asp:Panel runat="server" ID="pnl_newEmail">
                    <div class="form-group">
                        <label for="txt_newEmail">E-mail:</label>
                        <asp:TextBox ID="txt_newEmail" runat="server" placeholder="something@example.com" Text=''  class="form-control" aria-describedby="txt_newEmailHelp" />
                        <asp:RequiredFieldValidator ID="vld_required_txt_newEmail" runat="server" ErrorMessage="Please provide your new email" ControlToValidate="txt_newEmail" ValidationGroup="grp_newEmail" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>                        
                        <asp:RegularExpressionValidator ID="vld_validemail_txt_newEmail" runat="server" ErrorMessage="The e-mail address is invalid" ControlToValidate="txt_newEmail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red" Display="Dynamic" ValidationGroup="grp_newEmail"></asp:RegularExpressionValidator>
                        <asp:CustomValidator ID="vld_existence_txt_newEmail" runat="server" ErrorMessage="This email is already associated with another account" ControlToValidate="txt_newEmail" Display="Dynamic" ForeColor="Red" ValidationGroup="grp_newEmail" OnServerValidate="vld_existence_txt_newEmail_ServerValidate"></asp:CustomValidator>
                    </div>
                    <asp:Button ID="btn_NewEmail" runat="server" Text="Update" class="btn btn-primary" ValidationGroup="grp_newEmail" OnClick="btn_NewEmail_Click" />
                </asp:Panel>

            </div>
    </div>
<asp:ScriptManager ID="scm_ajax" runat="server"></asp:ScriptManager>
<asp:SqlDataSource ID="sql_datasource_user" runat="server" ConnectionString="<%$ ConnectionStrings:dbConnectionString %>" SelectCommand="SELECT [userId], [firstname], [username], [surname], [email], [password] FROM [users] WHERE ([userId] = @userId)" DeleteCommand="DELETE FROM [users] WHERE [userId] = @userId" InsertCommand="INSERT INTO [users] ([firstname], [username], [surname], [email], [password]) VALUES (@firstname, @username, @surname, @email, @password)" UpdateCommand="UPDATE [users] SET [firstname] = @firstname, [username] = @username, [surname] = @surname, [email] = @email WHERE [userId] = @userId">
    <DeleteParameters>
        <asp:Parameter Name="userId" Type="Int32" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="firstname" Type="String" />
        <asp:Parameter Name="username" Type="String" />
        <asp:Parameter Name="surname" Type="String" />
        <asp:Parameter Name="email" Type="String" />
        <asp:Parameter Name="password" Type="String" />
    </InsertParameters>
    <SelectParameters>
        <asp:SessionParameter Name="userId" SessionField="UserId" Type="Int32" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="firstname" Type="String" />
        <asp:Parameter Name="username" Type="String" />
        <asp:Parameter Name="surname" Type="String" />
        <asp:Parameter Name="email" Type="String" />
        <asp:Parameter Name="password" Type="String" />
        <asp:Parameter Name="userId" Type="Int32" />
    </UpdateParameters>
</asp:SqlDataSource>
</asp:Content>

