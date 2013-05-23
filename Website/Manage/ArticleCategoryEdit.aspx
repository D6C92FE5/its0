<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Manage.master" AutoEventWireup="true" CodeFile="ArticleCategoryEdit.aspx.cs" Inherits="Manage_ArticleCategoryEdit" %>

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
                    }],
                    popover_placement: 'top'
                }, 
            }
        })
    </script>
</asp:Content>

