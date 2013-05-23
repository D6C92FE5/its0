<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Paginator.ascx.cs" Inherits="Manage_Paginator" %>

<asp:Panel ID="Paginator" runat="server" CssClass="row-fluid">
    <div class="span2 pagination">
        <ul>
            <li><asp:HyperLink ID="ctShowAll" runat="server">显示全部</asp:HyperLink></li>
        </ul>
    </div>
    <asp:Panel ID="ctContainer" runat="server" CssClass="span10"></asp:Panel>
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
            prevText: "&lsaquo;",
            nextText: "下一页 &rsaquo;",
            cssStyle: 'pagination pagination-right',
        });
        $('#<%= ctShowAll.ClientID %>').attr("href", hrefs[1]+"All"+hrefs[3]);
    </script>
</asp:Panel>

