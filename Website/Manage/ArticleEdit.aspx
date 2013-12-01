<%@ Page Title="文章编辑" Language="C#" MasterPageFile="~/Manage/Manage.master" AutoEventWireup="true" CodeFile="ArticleEdit.aspx.cs" Inherits="Manage_ArticleEdit" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="ctHead" ContentPlaceHolderID="_Head" Runat="Server">
    <style>
        form {
            margin-bottom: 10px;
        }

        form .btn-primary {
            width: 80px;
        }

        select, input[type="text"] {
            margin-bottom: 6px;
        }
        .controls > .radio, .controls > .checkbox {
            padding-top: 5px!important;
        }
        .radio input[type="radio"], .checkbox input[type="checkbox"] {
            margin-left: -16px;
        }
        .radio, .checkbox {
            padding-left: 16px;
        }
        .checkbox.inline  {
            padding-top: 0;
            padding-bottom: 4px;
        }

        .picture-scroll-selector a {
            padding: 2px 4px 0 4px;
            margin: 0 4px;
            position: relative;
            bottom: 2px;
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

        .cke.cke_reset {
            margin-top: 4px;
            margin-bottom: 8px;
        }
           
        .tooltip.left {
            margin-left: -10px;
        }
    </style>

    <!--[if lte IE 6]>
    <style>
        /* CKEditor 不兼容 IE6 */
        form textarea {
            display: inline-block!important;
            visibility: visible!important;
        }
    </style>
    <![endif]-->
</asp:Content>
<asp:Content ID="ctBody" ContentPlaceHolderID="_Body" Runat="Server">
    <h1><%= isNewArticle ? "发布文章" : "修改文章" %></h1>
    <div class="controls controls-row">
        <asp:DropDownList ID="ctCategory" runat="server" CssClass="span2"></asp:DropDownList>
        <asp:TextBox ID="ctTitle" runat="server" placeholder="标题" CssClass="span7"></asp:TextBox>
        <asp:TextBox ID="ctFrom" runat="server" placeholder="来源" CssClass="span3"></asp:TextBox>
    </div>
    <div class="controls controls-row">
        <asp:PlaceHolder ID="ctTopField" runat="server">
            <label class="checkbox inline"><asp:CheckBox ID="ctTop" runat="server" />置顶</label>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="ctRecommendField" runat="server">
            <label class="checkbox inline"><asp:CheckBox ID="ctRecommend" runat="server" />推荐</label>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="ctHideField" runat="server">
            <label class="checkbox inline"><asp:CheckBox ID="ctHide" runat="server" />隐藏</label>
        </asp:PlaceHolder>
    </div>
    <asp:PlaceHolder ID="ctPictureScrollField" runat="server">
        <div class="controls controls-row picture-scroll">
            <label class="checkbox inline"><asp:CheckBox ID="ctPictureScroll" runat="server" />出现在图片滚动中</label>
            <span class="picture-scroll-selector hide">
                <a class="btn btn-small">选择或上传在图片滚动中显示的图片</a>
                <asp:TextBox ID="ctPictureScrollUrl" runat="server" CssClass="hide"></asp:TextBox>
                <span></span>
            </span>
        </div>
    </asp:PlaceHolder>
    <!--[if lte IE 6]>
    <p>您的浏览器严重过时，不支持所见即所得的编辑方式，请直接在下面的文本框中输入HTML代码</p>
    <![endif]-->
    <CKEditor:CKEditorControl ID="ctContent" runat="server" Height="300px" 
        BasePath="~/Package/CKEditor"></CKEditor:CKEditorControl>
    <asp:Button ID="ctSubmit" runat="server" Text="提交" CssClass="btn btn-primary" OnClick="ctSubmit_OnClick" />
</asp:Content>
<asp:Content ID="ctScript" ContentPlaceHolderID="_Script" Runat="Server">
    <%= _.PackageScriptTag("jQuery.Validate/jquery.validate.min.js") %>
    <%= _.PackageScriptTag("CKFinder/ckfinder.js") %>
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

        $('.picture-scroll :checkbox').change(function () {
            if (this.checked) {
                $('.picture-scroll-selector').removeClass('hide')
            } else {
                $('.picture-scroll-selector').addClass('hide')
            }
        }).change()
        var setDisplayFilename = function () {
            var fileUrl = $('#<%= ctPictureScrollUrl.ClientID %>').val()
            if (fileUrl) {
                $('.picture-scroll-selector span').text(fileUrl.slice(fileUrl.lastIndexOf('/') + 1))
            }
        }
        $('.picture-scroll-selector a').click(function () {
            var finder = new CKFinder()
            finder.basePath = '<%= ResolveUrl("~/Package/CKFinder/") %>'
            finder.selectActionFunction = function (fileUrl) {
                $('#<%= ctPictureScrollUrl.ClientID %>').val(fileUrl)
                setDisplayFilename()
            }
	        finder.popup()
        })
        setDisplayFilename()
    </script>
</asp:Content>
