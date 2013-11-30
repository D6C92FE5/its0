<%@ Page Title="用户列表" Language="C#" MasterPageFile="~/Manage/Manage.master" AutoEventWireup="true" CodeFile="UserList.aspx.cs" Inherits="Manage_UserList" %>

<%@ Register Src="~/Manage/Paginator.ascx" TagPrefix="uc1" TagName="Paginator" %>
<%@ Register Src="~/Manage/ListFilter.ascx" TagPrefix="uc1" TagName="ListFilter" %>

<asp:Content ID="ctHead" ContentPlaceHolderID="_Head" Runat="Server">
    <style>
        .new_user {
            margin: 20px 10px 10px 0;
        }

        .alert {
            display: inline-block;
            vertical-align: bottom;
            margin: 0 3px 10px 3px;
            padding-right: 10px;
        }
        .alert .close {
            position: static;
            padding-left: 10px;
        }
        .alert span {
            font-weight: bold;
        }

        .table .username {
            text-align: center;
            width: 50%;
        }
        .table .permission {
            text-align: center;
            width: 50%;
        }
        .table .operate {
            text-align: center;
            min-width: 120px;
        }
        .table td {
            vertical-align: middle;
        }
    </style>
</asp:Content>
<asp:Content ID="ctBody" ContentPlaceHolderID="_Body" Runat="Server">
    <h1>用户列表</h1>
    <uc1:ListFilter runat="server" ID="ctFilterPermission" Name="权限" QueryField="Permission" />
    <a href="UserEdit.aspx" class="btn btn-success pull-right new_user">
        <i class="icon-plus icon-white"></i> 添加用户</a>
    <table class="table table-bordered table-hover table-condensed">
        <thead>
            <tr>
                <th class="username">用户名</th>
                <th class="permission">权限</th>
                <th class="operate">操作</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="ctList" runat="server">
                <ItemTemplate>
            <tr>
                <td class="username">
                    <%# Eval("Name") %>
                </td>
                <td class="permission">
                    <a href='<%# ctFilterPermission.Url(Eval("Permission.ID").ToString()) %>'>
                        <%# Eval("Permission.Name") %>
                    </a>
                </td>
                <td class="operate">
                    <a href='UserEdit.aspx?ID=<%# Eval("ID") %>' class="btn btn-mini">
                        <i class="icon-pencil"></i> 修改</a>
                    <asp:LinkButton ID="ctDelete" runat="server" CssClass="btn btn-mini delete"
                        OnCommand="ctDelete_Command" CommandArgument='<%# Eval("ID") %>'>
                        <i class="icon-trash"></i> 删除</asp:LinkButton>
                </td>
            </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
    <uc1:Paginator runat="server" ID="ctPaginator" />
</asp:Content>
<asp:Content ID="ctScript" ContentPlaceHolderID="_Script" Runat="Server">
    <script>
        $('.delete').on('click', function () {
            return confirm("确定删除用户？");
        });
    </script>
</asp:Content>

