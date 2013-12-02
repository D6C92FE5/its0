<%@ Page Title="" Language="C#" MasterPageFile="~/View/View.master" AutoEventWireup="true" CodeFile="~/Manage/Message.aspx.cs" Inherits="Manage_Message" %>

<asp:Content ID="ctHead" ContentPlaceHolderID="_Head" Runat="Server">
</asp:Content>
<asp:Content ID="ctBody" ContentPlaceHolderID="_Content" Runat="Server">
	<div class="message">
		<p class="pre-wrap"><asp:Literal ID="ctMessage" runat="server"></asp:Literal></p>
		<p>
			<asp:HyperLink ID="ctRedirect" runat="server" Visible="false">
				如果您的浏览器没有自动跳转，请点击这里
			</asp:HyperLink>
		</p>
	</div>
</asp:Content>

