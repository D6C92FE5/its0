<%@ Page Title="默认页" Language="C#" MasterPageFile="~/Bootstrap.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="ctHead" ContentPlaceHolderID="_Head" Runat="Server">
    <style>
        body {
            padding: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="ctBody" ContentPlaceHolderID="_Body" Runat="Server">
    <form runat="server">
        <a class="btn btn-large btn-primary" href="Manage/Index.aspx">网站管理系统</a>
        <asp:Button ID="ctInitDatabase" runat="server" Text="创建并初始化数据库" 
            CssClass="btn btn-large btn-success" OnClick="ctInitDatabase_Click" />
    </form>
    <p>初始用户名 ad , 初始密码 1</p>
</asp:Content>
<asp:Content ID="ctScript" ContentPlaceHolderID="_Script" Runat="Server">
</asp:Content>

