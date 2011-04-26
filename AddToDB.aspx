<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddToDB.aspx.cs" Inherits="AddToDB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpMainContent" Runat="Server">
    Select the language file and database table to which you would like to add data:
    <p></p>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <br />
    <p>
    <asp:DropDownList ID="DropDownList2" runat="server" style="height: 22px">
        <asp:ListItem Selected="True" Value="Format">Format table</asp:ListItem>
        <asp:ListItem Value="Tokens">Tokens table</asp:ListItem>
    </asp:DropDownList></p>
    <asp:Button ID="DataBtn" runat="server" Text="Add To Database" onclick="DataBtn_Click" />
    <br />
    <br />
    <asp:Label ID="Label1" runat="server"></asp:Label>
    <br />
    <asp:TextBox ID="TextBox2" runat="server" Height="143px" TextMode="MultiLine" 
        Width="755px"></asp:TextBox>
</asp:Content>

