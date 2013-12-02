<%@ Page Title="日志" Language="C#" MasterPageFile="~/Manage/Manage.master" AutoEventWireup="true" CodeFile="Log.aspx.cs" Inherits="Manage_Log" %>

<%@ Register Src="~/Manage/Paginator.ascx" TagPrefix="uc1" TagName="Paginator" %>
<%@ Register Src="~/Manage/ListFilter.ascx" TagPrefix="uc1" TagName="ListFilter" %>

<asp:Content ID="ctHead" ContentPlaceHolderID="_Head" Runat="Server">
    <style>
        .table th, .table td {
            text-align: center;
        }
        .table .time {
            min-width: 140px;
        }
        .table .user_id {
            min-width: 80px;
        }
        .table .target_id {
            min-width: 80px;
        }
        .table .target_type {
            width: 40%;
        }
        .table .description {
            width: 50%;
        }
    </style>
</asp:Content>
<asp:Content ID="ctBody" ContentPlaceHolderID="_Body" Runat="Server">
    <h1>日志</h1>
    <uc1:ListFilter runat="server" ID="ctFilterUserID" Name="用户" QueryField="UserID" />
    <uc1:ListFilter runat="server" ID="ctFilterTargetID" Name="目标" QueryField="TargetID" />
    <uc1:ListFilter runat="server" ID="ctFilterTargetType" Name="用户" QueryField="TargetType" />
    <table class="table table-bordered table-hover table-condensed">
        <thead>
            <tr>
                <th class="time">时间</th>
                <th class="user_id">用户</th>
                <th class="target_id">目标</th>
                <th class="target_type">目标类型</th>
                <th class="description">描述</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="ctList" runat="server">
                <ItemTemplate>
            <tr>
                <td class="time">
                    <%# ((DateTime)Eval("Time")).ToString("yyyy/MM/dd HH:mm") %>
                </td>
                <td class="user_id">
                    <a href='<%# ctFilterUserID.Url(Eval("UserID").ToString()) %>'>
                        <%# Eval("UserID") %>
                    </a>
                </td>
                <td class="target_id">
                    <a href='<%# ctFilterTargetID.Url(Eval("TargetID").ToString()) %>'>
                        <%# Eval("TargetID") %>
                    </a>
                </td>
                <td class="target_type">
                    <a href='<%# ctFilterTargetType.Url(_.EncodeUrl(Eval("TargetType"))) %>'>
                        <%# _.EncodeHtml(Eval("TargetType")) %>
                    </a>
                </td>
                <td class="description">
                    <%# _.EncodeHtml(Eval("Description")) %>
                </td>
            </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
    <uc1:Paginator runat="server" ID="ctPaginator" />
</asp:Content>
<asp:Content ID="ctScript" ContentPlaceHolderID="_Script" Runat="Server">
</asp:Content>

