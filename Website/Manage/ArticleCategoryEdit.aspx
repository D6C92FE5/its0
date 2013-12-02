<%@ Page Title="文章分类编辑" Language="C#" MasterPageFile="~/Manage/Manage.master" AutoEventWireup="true" CodeFile="ArticleCategoryEdit.aspx.cs" Inherits="Manage_ArticleCategoryEdit" %>

<asp:Content ID="ctHead" ContentPlaceHolderID="_Head" Runat="Server">
    <style>
        form {
            margin-bottom: 10px;
        }

        form h1 {
            margin-bottom: 20px;
        }

        form .btn {
            width: 80px;
        }
    </style>
</asp:Content>
<asp:Content ID="ctBody" ContentPlaceHolderID="_Body" Runat="Server">
    <h1><%= isNewCategory ? "添加文章分类" : "编辑文章分类" %></h1>
    <div class="controls controls-row">
        <asp:TextBox ID="ctName" runat="server" CssClass="span12"
            placeholder="分类名（1-100个字符）"></asp:TextBox>
    </div>
    <asp:Button ID="ctSubmit" runat="server" Text="提交" CssClass="btn btn-primary" 
        OnClick="ctSubmit_Click" />
    <asp:ScriptManager ID="ctScriptManager" runat="server">
        <Services>
            <asp:ServiceReference Path="~/WebService.asmx" />
        </Services>
    </asp:ScriptManager>
</asp:Content>
<asp:Content ID="ctScript" ContentPlaceHolderID="_Script" Runat="Server">
    <%= _.PackageScriptTag("jQuery.Validate/jquery.validate.min.js") %>
    <script src="js/Validator.js"></script>
    <script>
        validate({
            fields: {
                <%= ctName.UniqueID %>: {
                    rule: [{
                        method: 'required',
                        message: "请填写分类名"
                    }, {
                        method: 'async',
                        argument: {
                            call: function (value, callback) {
                                if (value == '<%= ctName.Text %>') {
                                    setTimeout(function () { callback(true) })
                                } else {
                                    WebService.IsArticleCategoryExist(value, callback)
                                }
                            }
                        },
                        message: "文章分类名已存在"
                    }],
                    popover_placement: 'top'
                }, 
            }
        })
    </script>
</asp:Content>

