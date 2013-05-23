<%@ Page Title="文章分类" Language="C#" MasterPageFile="~/Manage/Manage.master" AutoEventWireup="true" CodeFile="ArticleCategoryList.aspx.cs" Inherits="Manage_ArticleCategoryList" %>

<asp:Content ID="ctHead" ContentPlaceHolderID="_Head" Runat="Server">
    <style>
        .new_category {
            margin: 20px 10px 10px 0;
        }

        .table .category {
            text-align: center;
        }
        .table .operate {
            text-align: center;
            width: 120px;
        }
        .table td {
            vertical-align: middle;
        }
    </style>
</asp:Content>
<asp:Content ID="ctBody" ContentPlaceHolderID="_Body" Runat="Server">
    <h1>文章分类</h1>
    <a href="ArticleCategoryEdit.aspx" class="btn btn-success pull-right new_category">
        <i class="icon-plus icon-white"></i> 添加文章分类</a>
    <table class="table table-bordered table-hover table-condensed">
        <thead>
            <tr>
                <th class="category">文章分类</th>
                <th class="operate">操作</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="ctList" runat="server">
                <ItemTemplate>
            <tr>
                <td class="category">
                    <a href='ArticleList.aspx?Category=<%# Eval("ID") %>'>
                        <%# Eval("Name") %>
                    </a>
                </td>
                <td class="operate">
                    <a href='ArticleCategoryEdit.aspx?ID=<%# Eval("ID") %>' class="btn btn-mini">
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
</asp:Content>
<asp:Content ID="ctScript" ContentPlaceHolderID="_Script" Runat="Server">
    <script>
        $('.delete').on('click', function () {
            return confirm("确定删除文章分类？");
        });
    </script>
</asp:Content>

