<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CodeHandler.ascx.cs" Inherits="CodeHandler" %>

<asp:TextBox ID="CodeText" runat="server" Height="437px" TextMode="MultiLine" 
    Width="611px" ontextchanged="TextBox1_TextChanged"></asp:TextBox>

<br />
<asp:Button ID="CodeButton" runat="server" onclick="CodeButton_Click" Text="Submit" />
