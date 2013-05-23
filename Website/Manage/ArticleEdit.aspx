<%@ Page Title="文章编辑" Language="C#" MasterPageFile="~/Manage/Manage.master" AutoEventWireup="true" CodeFile="ArticleEdit.aspx.cs" Inherits="Manage_ArticleEdit" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="ctHead" ContentPlaceHolderID="_Head" Runat="Server">
    <style>
        form {
            margin-bottom: 10px;
        }

        form .btn {
            width: 80px;
            margin-top: 8px;
        }

        label.checkbox.inline  {
            padding-top: 0;
            padding-bottom: 8px;
        }

        /* 在 CKEditor 载入完成之前显示相同大小的输入框
            height 值应为 CKEditor 的 Height + 105 */
        form textarea {
            width: 100%;
            height: 405px!important;
            margin: 0;
            padding: 0;
            border: 1px solid #B6B6B6;
        }
            
        .tooltip.left {
            margin-left: -10px;
        }
    </style>
</asp:Content>
<asp:Content ID="ctBody" ContentPlaceHolderID="_Body" Runat="Server">
    <h1><%= isNewArticle ? "发布文章" : "修改文章" %></h1>
    <div class="controls controls-row">
        <asp:DropDownList ID="ctCategory" runat="server" CssClass="span2">
            <asp:ListItem Value="">分类</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="ctTitle" runat="server" placeholder="标题" CssClass="span7"></asp:TextBox>
        <asp:TextBox ID="ctFrom" runat="server" placeholder="来源" CssClass="span3"></asp:TextBox>
    </div>
    <asp:PlaceHolder ID="ctTopField" runat="server">
        <label class="checkbox inline"><asp:CheckBox ID="ctTop" runat="server" />置顶</label>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="ctRecommendField" runat="server">
        <label class="checkbox inline"><asp:CheckBox ID="ctRecommend" runat="server" />推荐</label>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="ctHideField" runat="server">
        <label class="checkbox inline"><asp:CheckBox ID="ctHide" runat="server" />隐藏</label>
    </asp:PlaceHolder>
    <CKEditor:CKEditorControl ID="ctContent" runat="server" Height="300px" 
        BasePath="~/Package/CKEditor"></CKEditor:CKEditorControl>
    <asp:Button ID="ctSubmit" runat="server" Text="提交" CssClass="btn btn-primary" OnClick="ctSubmit_OnClick" />
</asp:Content>
<asp:Content ID="ctScript" ContentPlaceHolderID="_Script" Runat="Server">
    <%= _.PackageScriptTag("jQuery.Validate/jquery.validate.min.js") %>
    <script src="js/Validator.js"></script>
    <script>
        validate({
            fields: {
                <%= ctCategory.UniqueID %>: {
                    rule: [{
                        method: 'required',
                        message: "请选择分类"
                    }],
                    popover_placement: 'left'
                }, 
                <%= ctTitle.UniqueID %>: {
                    rule: [{
                        method: 'required', 
                        message: "请填写标题"
                    }],
                    popover_placement: 'top'
                }
            }
        })
    </script>
</asp:Content>
