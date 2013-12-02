<%@ Page Title="关于" Language="C#" MasterPageFile="~/Manage/Manage.master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="Manage_About" %>

<asp:Content ID="ctHead" ContentPlaceHolderID="_Head" Runat="Server">
    <style>
        h1 {
            margin-bottom: 20px;
        }

        p span {
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="ctBody" ContentPlaceHolderID="_Body" Runat="Server">
    <h1>关于</h1>
    <p>
        <span>开发制作:</span>
        <a href="http://it.ouc.edu.cn/itstudio/" target="_blank">爱特工作室</a>
    </p>
    <p>
        <span>项目主页:</span>
        <a href="https://github.com/D6C92FE5/its0" target="_blank">github.com/D6C92FE5/its0</a>
    </p>
</asp:Content>
<asp:Content ID="ctScript" ContentPlaceHolderID="_Script" Runat="Server">
</asp:Content>

