<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="account.aspx.cs" Inherits="account" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
        <div style="width: 500px;margin-left:100px;margin-right:auto">
            <asp:FormView ID="userForm" runat="server" DataSourceID="sql_datasource_user" DefaultMode="Edit" DataKeyNames="userId" OnItemUpdating="userForm_ItemUpdating" OnDataBound="userForm_DataBound">
                <EditItemTemplate>
                        <div class="form-group">
                            <label for="txt_firstname">Firstname:</label>
                            <asp:TextBox ID="txt_firstname" runat="server" Text='<%# Bind("firstname") %>' class="form-control" aria-describedby="txt_firstnameHelp" />
                        </div>
                        <div class="form-group">
                            <label for="txt_surname">Surname:</label>
                            <asp:TextBox ID="txt_surname" runat="server" Text='<%# Bind("surname") %>'  class="form-control" aria-describedby="txt_surnameHelp" />
                        </div>
                        <div class="form-group">
                            <label for="txt_username">Username:</label>
                            <asp:TextBox ID="txt_username" runat="server" Text='<%# Bind("username") %>'  class="form-control" aria-describedby="txt_usernameHelp" />
                        </div>
                        <div class="form-group">
                            <label for="emailTextBox">E-mail:</label>
                            <asp:TextBox ID="emailTextBox" runat="server" Text='<%# Bind("email") %>'  class="form-control" aria-describedby="emailTextBoxHelp" />
                        </div>
                        <div class="form-group">
                            <label for="txt_password">Password:</label>
                            <asp:TextBox ID="txt_password" runat="server" Text='<%# Bind("password") %>'  class="form-control" aria-describedby="txt_passwordHelp" />
                        </div>
                    
                    <asp:Button ID="UpdateButton" runat="server"  class="btn btn-primary"  CausesValidation="True" CommandName="Update" Text="Update" />

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
        </div>
    </p>
<asp:SqlDataSource ID="sql_datasource_user" runat="server" ConnectionString="<%$ ConnectionStrings:dbConnectionString %>" SelectCommand="SELECT [userId], [firstname], [username], [surname], [email], [password] FROM [users] WHERE ([userId] = @userId)" DeleteCommand="DELETE FROM [users] WHERE [userId] = @userId" InsertCommand="INSERT INTO [users] ([firstname], [username], [surname], [email], [password]) VALUES (@firstname, @username, @surname, @email, @password)" UpdateCommand="UPDATE [users] SET [firstname] = @firstname, [username] = @username, [surname] = @surname, [email] = @email, [password] = @password WHERE [userId] = @userId">
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

