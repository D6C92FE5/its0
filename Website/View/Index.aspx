<%@ Page Title="首页" Language="C#" MasterPageFile="~/View/View.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="View_Index" %>

<asp:Content ID="ctHead" ContentPlaceHolderID="_Head" Runat="Server">
</asp:Content>
<asp:Content ID="ctBody" ContentPlaceHolderID="_Content" Runat="Server">
    <asp:repeater ID="ctList" runat="server"></asp:repeater>
</asp:Content>
<asp:Content ID="ctScript" ContentPlaceHolderID="_Script" Runat="Server">
</asp:Content>

