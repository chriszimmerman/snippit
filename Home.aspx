<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>
<%@ Register Src="~/CodeHandler.ascx" TagName="CodeHandler" TagPrefix="cn" %>

<asp:Content ID="Header" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="cpMainContent" Runat="Server">
    <h1>Welcome to SnippIt!</h1>
    <p>SnippIt is a collaboration tool for programmers. Enter some code below, select the language,
        and click the SnippIt button to create a well-formatted page containing your code.</p>

    <asp:TextBox id="_codeText" runat="server" Height="437px" TextMode="MultiLine" Width="814px" Wrap="true">
    </asp:TextBox>
    <p>

    <asp:DropDownList ID="DropDownList1" runat="server" 
        style="height: 22px" 
            onselectedindexchanged="DropDownList1_SelectedIndexChanged">
        <asp:ListItem Selected="True" Value="none">General formatting</asp:ListItem>
        <asp:ListItem Value="java">Java</asp:ListItem>

    </asp:DropDownList>

    <p>
    <asp:Button ID="CodeButton" runat="server" Text="Submit" onclick="CodeButton_Click1" />

</asp:Content>

