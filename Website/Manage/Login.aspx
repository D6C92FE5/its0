<%@ Page Title="登录" Language="C#" MasterPageFile="~/Bootstrap.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Manage_Login" %>

<asp:Content ID="ctHead" ContentPlaceHolderID="_Head" Runat="Server">
    <style type="text/css">
        body {
            padding-top: 40px;
            padding-bottom: 40px;
            background-color: #f5f5f5;
        }

        .container {
            width: auto;
            min-width: 400px;
        }
        .container form {
            width: 300px;
            padding: 19px 29px 29px;
            margin: 0 auto 20px;
            background-color: #fff;
            border: 1px solid #e5e5e5;
            border-radius: 5px;
            box-shadow: 0 1px 2px rgba(0,0,0,.05);
        }
        .container h1,
        .container .checkbox {
            margin-bottom: 10px;
        }
        .container input[type="text"],
        .container input[type="password"] {
            font-size: 16px;
            height: auto;
            margin-bottom: 15px;
            padding: 7px 9px;
        }

        /* 表单验证未通过的区域 */
        form .error {
            border-color: #E9322D;
        }
        form .error:focus {
            border-color: #E9322D;
            box-shadow: 0 0 6px #F8B9B7;
        }

        .tooltip {
            font-size: 14px;
        }

        span.error {
            color: #E9322D;
            padding-left: 18px;
        }
    </style>
</asp:Content>
<asp:Content ID="ctBody" ContentPlaceHolderID="_Body" Runat="Server">
    <div class="container">
        <form id="Form1" runat="server">
            <h2>请登录</h2>
            <div class="controls">
                <asp:TextBox ID="ctName" runat="server" CssClass="input-block-level" 
                    placeholder="用户名"></asp:TextBox>
                <asp:TextBox ID="ctPassword" runat="server" CssClass="input-block-level" 
                    placeholder="密码" TextMode="Password"></asp:TextBox>
                <label class="checkbox"><asp:CheckBox ID="ctRemember" runat="server" />
                    记住我</label>
            </div>
            <asp:Button ID="ctSubmit" runat="server" Text="登录" 
                CssClass="btn btn-large btn-primary" OnClick="ctSubmit_Click" />
            <asp:Label ID="ctError" runat="server" CssClass="error"></asp:Label>
            <asp:HiddenField ID="ctPasswordHashed" runat="server" />
        </form>
    </div>
</asp:Content>
<asp:Content ID="ctScript" ContentPlaceHolderID="_Script" Runat="Server">
    <%= _.PackageScriptTag("CryptoJS/rollups/hmac-sha1.js") %>
    <script src="js/Password.js"></script>
    <%= _.PackageScriptTag("jQuery.Validate/jquery.validate.min.js") %>
    <script src="js/Validator.js"></script>
    <script>
        validate({
            fields: {
                <%= ctName.UniqueID %>: {
                    rule: [{
                        method: 'required',
                        message: "请填写用户名"
                    }],
                    popover_placement: 'right'
                }, 
                <%= ctPassword.UniqueID %>: {
                    rule: [{
                        method: 'required', 
                        message: "请填写密码"
                    }],
                    popover_placement: 'right'
                }
            },
            onsubmit: function () {
                // 在客户端对密码进行一次哈希再向服务器传输
                HashPassword(
                    "<%= Config.HmacStaticKey %>",
                    $("#<%= ctPassword.ClientID %>"),
                    $("#<%= ctPasswordHashed.ClientID %>")
                )
            }
        })
    </script>
</asp:Content>

