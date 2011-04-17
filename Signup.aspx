<%@ Page Title="Sign Up for SnippIt!" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Signup.aspx.cs" Inherits="Signup" %>

<asp:Content ID="Header" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="cpMainContent" Runat="Server">
    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" ContinueDestinationPageUrl="~/Home.aspx">
    <WizardSteps>
        <asp:CreateUserWizardStep runat="server" />
        <asp:CompleteWizardStep runat="server" />
    </WizardSteps>
    </asp:CreateUserWizard>
</asp:Content>

