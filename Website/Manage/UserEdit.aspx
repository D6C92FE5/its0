<%@ Page Title="用户编辑" Language="C#" MasterPageFile="~/Manage/Manage.master" AutoEventWireup="true" CodeFile="UserEdit.aspx.cs" Inherits="Manage_UserEdit" %>

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
    <h1><%= isNewUser ? "添加用户" : "编辑用户" %></h1>
    <div class="form-horizontal">
        <div class="control-group">
            <label class="control-label" for="<%= ctName.ClientID %>">用户名</label>
            <div class="controls controls-row">
                <asp:TextBox ID="ctName" runat="server" CssClass="span12"
                    placeholder="用户名（1-20个字符）"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="<%= ctPassword.ClientID %>">密码</label>
            <div class="controls controls-row password">
                <asp:TextBox ID="ctPassword" runat="server" CssClass="span6" autocomplete="off"
                    placeholder="密码（1-20个字符）" TextMode="Password"></asp:TextBox>
                <asp:TextBox ID="ctPassword2" runat="server" CssClass="span6"
                    placeholder="确认密码" TextMode="Password"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="<%= ctPassword2.ClientID %>">权限</label>
            <div class="controls controls-row">
                <asp:DropDownList ID="ctPermission" runat="server" CssClass="span12"
                    ></asp:DropDownList>
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
    <asp:ScriptManager ID="ctScriptManager" runat="server">
        <Services>
            <asp:ServiceReference Path="~/WebService.asmx" />
        </Services>
    </asp:ScriptManager>
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
                    }, {
                        method: 'async',
                        argument: {
                            call: function (value, callback) {
                                if (value == '<%= ctName.Text %>') {
                                    setTimeout(function () { callback(true) })
                                } else {
                                    WebService.IsUserNameNotUsed(value, callback)
                                }
                            }
                        },
                        message: "用户名已存在"
                    }],
                    popover_placement: 'right'
                }, 
                <%= ctPassword.UniqueID %>: {
                    rule: [{
                        method: 'required', 
                        disabled: <%= isNewUser ? "false" : "true" %>,
                        message: "请填写密码"
                    }],
                    popover_placement: 'left'
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

