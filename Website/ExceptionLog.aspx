<%@ Page Title="异常日志" Language="C#" MasterPageFile="~/Bootstrap.master" AutoEventWireup="true" CodeFile="ExceptionLog.aspx.cs" Inherits="ExceptionLog" %>

<asp:Content ID="ctHead" ContentPlaceHolderID="_Head" Runat="Server">
    <style>
        .pre {
            white-space: pre;
        }
        .pre-wrap {
            white-space: pre-wrap;
        }

        a:hover {
            text-decoration: none;
            color: #08c;
            background-color: #f5f5f5;
        }

        h2 {
            font-size: 14px;
            line-height: normal;
            margin-bottom: 0;
        }

        ul {
            list-style: none;
            margin: 0;
        }

        ul p {
            margin: 0;
        }

        .label {
            display: inline;
        }

        .accordion {
            margin: 20px;
            min-width: 600px;
        }

        .accordion-inner span {
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="ctBody" ContentPlaceHolderID="_Body" Runat="Server">
    <div class="accordion">
        <asp:Repeater ID="ctList" runat="server">
            <ItemTemplate>
        <div class="accordion-group">
            <div class="accordion-heading">
                <a class="accordion-toggle" data-toggle="collapse" href='#<%# Eval("ID") %>'>
                    <span class="label label-info"><%# Eval("Time") %></span>
                    <span class="label label-important"><%# ((Type)Eval("Type")).Name %></span>
                    <span class="pre-wrap"><%# Eval("Message") %></span>
                </a>
            </div>
            <div id='<%# Eval("ID") %>' class="accordion-body collapse">
                <div class="accordion-inner">
                    <ul>
                        <li>
                            <span>发生时刻:</span>
                            <%# Eval("Time") %>
                        </li>
                        <li>
                            <span>URL:</span>
                            <%# Eval("Url") %>
                        </li>
                        <li>
                            <span>类型:</span>
                            <%# Eval("Type") %>
                        </li>
                        <li>
                            <span>消息:</span>
                            <%# Eval("Message") %>
                        </li>
                        <li>
                            <span>堆栈跟踪:</span>
                            <p class="pre"><%# Eval("StackTrace") %></p>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
<asp:Content ID="ctScript" ContentPlaceHolderID="_Script" Runat="Server">
</asp:Content>

