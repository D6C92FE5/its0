<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Manage.master" AutoEventWireup="true" CodeFile="Message.aspx.cs" Inherits="Manage_Message" %>

<asp:Content ID="ctHead" ContentPlaceHolderID="_Head" Runat="Server">
    <style>
        .hero-unit {
            padding-top: 80px;
            padding-bottom: 50px;
            text-align: center;
        }
        .hero-unit p {
            font-weight: bold;
        }
        .hero-unit a {
            font-size: 14px;
            font-weight: normal;
        }
    </style>
</asp:Content>
<asp:Content ID="ctBody" ContentPlaceHolderID="_Body" Runat="Server">
    <div class="hero-unit">
        <p class="pre-wrap"><asp:Literal ID="ctMessage" runat="server"></asp:Literal></p>
        <p>
            <asp:HyperLink ID="ctRedirect" runat="server" Visible="false">
                如果您的浏览器没有自动跳转，请点击这里
            </asp:HyperLink>
        </p>
    </div>
</asp:Content>
<asp:Content ID="ctScript" ContentPlaceHolderID="_Script" Runat="Server">
</asp:Content>

