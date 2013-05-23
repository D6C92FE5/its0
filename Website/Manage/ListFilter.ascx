<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ListFilter.ascx.cs" Inherits="Manage_ListFilter" %>

<asp:Panel ID="ctContainer" runat="server" CssClass="alert alert-info filterbox">
    <asp:Literal ID="ctName" runat="server"></asp:Literal>:
    <asp:HyperLink ID="ctCancel" runat="server" CssClass="close">&times;</asp:HyperLink>
    <asp:Label ID="ctValue" runat="server" Text=""></asp:Label>
</asp:Panel>

