<%@ Page Title="文章列表" Language="C#" MasterPageFile="~/Manage/Manage.master" AutoEventWireup="true" CodeFile="ArticleList.aspx.cs" Inherits="Manage_ArticleList" %>

<%@ Register Src="~/Manage/Paginator.ascx" TagPrefix="uc1" TagName="Paginator" %>
<%@ Register Src="~/Manage/ListFilter.ascx" TagPrefix="uc1" TagName="ListFilter" %>



<asp:Content ID="ctHead" ContentPlaceHolderID="_Head" Runat="Server">
    <style>
        .new_article {
            margin: 20px 10px 10px 0;
        }

        .table .category {
            width: 70px;
        }
        .table td.title {
            text-align: left;
            white-space: normal;
            padding: 4px 8px;
        }
        .table .publisher {
            width: 80px;
        }
        .table .post_date {
            width: 120px;
        }
        .table .operate {
            width: 120px;
        }
    </style>
</asp:Content>
<asp:Content ID="ctBody" ContentPlaceHolderID="_Body" Runat="Server">
    <h1>文章列表</h1>
    <uc1:ListFilter runat="server" ID="ctFilterCategory" Name="分类" QueryField="Category" />
    <uc1:ListFilter runat="server" ID="ctFilterPublisher" Name="发布者" QueryField="Publisher" />
    <a href="ArticleEdit.aspx" class="btn btn-success pull-right new_article">
        <i class="icon-plus icon-white"></i> 发布文章</a>
    <table class="table table-bordered table-hover table-condensed">
        <thead>
            <tr>
                <th class="category">分类</th>
                <th class="title">标题</th>
                <th class="publisher">发布者</th>
                <th class="post_date">发布时间</th>
                <th class="operate">操作</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="ctList" runat="server">
                <ItemTemplate>
            <tr>
                <td class="category">
                    <a href='<%# ctFilterCategory.Url(Eval("Category.ID").ToString()) %>'>
                        <%# Eval("Category.Name") %>
                    </a> 
                </td>
                <td class="title">
                    <a href='<%# ResolveUrl(string.Format(Config.ArtilceDisplayUrlTemplate, 
                        Eval("ID").ToString())) %>' target="_blank">
                        <%# Eval("Title") %>
                    </a>
                </td>
                <td class="publisher">
                    <a href='<%# ctFilterPublisher.Url(Eval("Publisher.ID").ToString()) %>'>
                        <%# Eval("Publisher.Name") %>
                    </a>
                </td>
                <td class="post_date">
                    <%# ((DateTime)Eval("PostDate")).ToString("yyyy/MM/dd HH:mm") %>
                </td>
                <td class="operate">
                    <a href='ArticleEdit.aspx?ID=<%# Eval("ID") %>' class="btn btn-mini">
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
            return confirm("确定删除文章？");
        });
    </script>
</asp:Content>
