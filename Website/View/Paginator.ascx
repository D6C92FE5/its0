<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/Manage/Paginator.ascx.cs" Inherits="Manage_Paginator" %>

<asp:Panel ID="Paginator" runat="server">
    <div class="pagination hide">
        <ul>
            <li><asp:HyperLink ID="ctShowAll" runat="server">显示全部</asp:HyperLink></li>
        </ul>
    </div>
    <asp:Panel ID="ctContainer" runat="server"></asp:Panel>
    <%= _.PackageScriptTag("jQuery/jquery-1.10.2.min.js") %>
    <%= _.PackageScriptTag("jQuery.simplePagination/jquery.simplePagination.js") %>
    <script>
        var query = location.search;
        var hrefs = query.match(/(.*[?&]<%= QueryField %>=)(\d+)(.*)/);
        if (hrefs === null) {
            hrefs = [query, (query === "" ? "?" : query + "&") + "<%= QueryField %>=", "1", ""];
        }
        $('#<%= ctContainer.ClientID %>').pagination({
            pages: <%= PageCount %>,
            edges: 1,
            currentPage: <%= CurrentPage %>,
            hrefTextPrefix: hrefs[1],
            hrefTextSuffix: hrefs[3],
            prevText: "&lsaquo; 上一页",
            nextText: "下一页 &rsaquo;",
            cssStyle: 'light-theme',
        });
        $('#<%= ctShowAll.ClientID %>').attr("href", hrefs[1]+"All"+hrefs[3]);
    </script>
</asp:Panel>

