<%@ Page Title="" Language="C#" MasterPageFile="~/View/View.master" AutoEventWireup="true" CodeFile="List.aspx.cs" Inherits="View_List" %>

<%@ Register Src="~/View/Paginator.ascx" TagPrefix="uc1" TagName="Paginator" %>

<asp:Content ID="ctHead" ContentPlaceHolderID="_Head" Runat="Server">
    <link rel="stylesheet" href="../Package/jQuery.simplePagination/simplePagination.css" />
</asp:Content>
<asp:Content ID="ctBody" ContentPlaceHolderID="_Content" Runat="Server">
    <asp:Literal ID="ctCategory" runat="server"></asp:Literal>
    <asp:Repeater ID="ctList" runat="server">
        <ItemTemplate>
            <a href='Content.aspx?ID=<%# Eval("ID") %>'><%# _.EncodeHtml(Eval("Title")) %></a>
            <%# ((DateTime)Eval("PostDate")).ToString("yyyy/MM/dd") %>
        </ItemTemplate>
    </asp:Repeater>
    <uc1:Paginator runat="server" ID="ctPaginator" />
</asp:Content>
<asp:Content ID="ctScript" ContentPlaceHolderID="_Script" Runat="Server">
</asp:Content>

