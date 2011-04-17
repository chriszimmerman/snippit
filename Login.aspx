<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="cpMainContent" Runat="Server">

        <h1>Log in to SnippIt</h1>
        <asp:LoginView ID="LoginView1" runat="server">
            <AnonymousTemplate>
                <asp:Login ID="Login1" runat="server" DestinationPageUrl="Home.aspx"
                    CreateUserText="Sign up for SnippIt!" CreateUserUrl="Signup.aspx" 
                    Font-Names="Arial">
                </asp:Login>
            </AnonymousTemplate>
            <LoggedInTemplate>
                You are already logged in, {0}.
            </LoggedInTemplate>
        </asp:LoginView>
</asp:Content>

