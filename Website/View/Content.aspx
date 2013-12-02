<%@ Page Title="内容页" Language="C#" MasterPageFile="~/View/View.master" AutoEventWireup="true" CodeFile="Content.aspx.cs" Inherits="View_Content" %>

<asp:Content ID="ctHead" ContentPlaceHolderID="_Head" Runat="Server">
</asp:Content>
<asp:Content ID="ctBody" ContentPlaceHolderID="_Content" Runat="Server">
    <asp:Literal ID="ctTitle" runat="server"></asp:Literal>
    <asp:Literal ID="ctPostDate" runat="server"></asp:Literal>
    <asp:Literal ID="ctContent" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="ctScript" ContentPlaceHolderID="_Script" Runat="Server">
</asp:Content>

