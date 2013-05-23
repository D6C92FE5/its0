<%@ Page Title="修改密码" Language="C#" MasterPageFile="~/Manage/Manage.master" AutoEventWireup="true" CodeFile="MyPassword.aspx.cs" Inherits="Manage_MyPassword" %>

<asp:Content ID="ctHead" ContentPlaceHolderID="_Head" Runat="Server">
    <style>
        form {
            margin-bottom: 10px;
        }

        form .btn {
            width: 80px;
            margin-top: 8px;
        }

        .form-horizontal {
            margin: 30px auto 0 auto;
            width: 640px;
        }

        .tooltip.left {
            margin-left: -13px;
        }

        .controls-row {
            width: 300px;
        }
    </style>
</asp:Content>
<asp:Content ID="ctBody" ContentPlaceHolderID="_Body" Runat="Server">
    <h1>修改密码</h1>
    <div class="form-horizontal">
        <div class="control-group">
            <label class="control-label" for="<%= ctPassword.ClientID %>">密码</label>
            <div class="controls controls-row">
                <asp:TextBox ID="ctPassword" runat="server" CssClass="span12" autocomplete="off"
                    placeholder="密码（1-20个字符）" TextMode="Password"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="<%= ctPassword.ClientID %>">确认密码</label>
            <div class="controls controls-row password">
                <asp:TextBox ID="ctPassword2" runat="server" CssClass="span12"
                    placeholder="确认密码" TextMode="Password"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <div class="controls">
                <asp:Button ID="ctSubmit" runat="server" Text="提交" CssClass="btn btn-primary" 
                    OnClick="ctSubmit_Click" />
            </div>
        </div>
        <asp:HiddenField ID="ctPasswordHashed" runat="server" />
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
                <%= ctPassword.UniqueID %>: {
                    rule: [{
                        method: 'required', 
                        message: "请填写密码"
                    }],
                    popover_placement: 'right'
                },
                <%= ctPassword2.UniqueID %>: {
                    rule: [{
                        method: 'equalTo', 
                        argument: "#<%= ctPassword.ClientID %>",
                        message: "两次输入的密码不一致"
                    }],
                    popover_placement: 'right'
                }
            },
            onsubmit: function () {
                // 在客户端对密码进行一次哈希再向服务器传输
                HashPassword(
                    "<%= Config.HmacStaticKey %>",
                    $(".password input"),
                    $("#<%= ctPasswordHashed.ClientID %>")
                )
            }
        })
    </script>
    <script>
        // autocomplete="off" 再一次确保避免了浏览器的自动完成
        $(".password input").val("");
    </script>
</asp:Content>

