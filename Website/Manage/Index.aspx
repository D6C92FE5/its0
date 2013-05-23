<%@ Page Title="首页" Language="C#" MasterPageFile="~/Manage/Manage.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Manage_Index" %>

<asp:Content ID="ctHead" ContentPlaceHolderID="_Head" Runat="Server">
    <style>
        .hero-unit p {
            padding-top: 40px;
        } 
    </style>
</asp:Content>
<asp:Content ID="ctBody" ContentPlaceHolderID="_Body" Runat="Server">
    <div class="hero-unit">
        <h1>欢迎进入管理系统</h1>
        <p>
            <a href="ArticleEdit.aspx" class="btn btn-success btn-large">发布文章</a>
            <asp:PlaceHolder ID="ctMyRecent" runat="server">
                <a href="ArticleEdit.aspx?ID=MyRecent" class="btn btn-inverse btn-large">
                    修改我最近的文章
                </a>
            </asp:PlaceHolder>
        </p>
    </div>
</asp:Content>
<asp:Content ID="ctScript" ContentPlaceHolderID="_Script" Runat="Server">
</asp:Content>
