﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="SecurityTokenVisualizerControl" Namespace="Microsoft.Samples.DPE.Identity.Controls"
    TagPrefix="cc1" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to ASP.NET!
    </h2>
    <p>
        To learn more about ASP.NET visit <a href="http://www.asp.net" title="ASP.NET Website">www.asp.net</a>.
    </p>
    <p>
        You can also find <a href="http://go.microsoft.com/fwlink/?LinkID=152368&amp;clcid=0x409"
            title="MSDN ASP.NET Docs">documentation on ASP.NET at MSDN</a>.
    </p>    
    <asp:Panel Visible="false" CssClass="secretContent" runat="server" ID="SecretContent">
        Secret Content (Only administrators can access this section)
    </asp:Panel>
    <cc1:SecurityTokenVisualizerControl ID="SecurityTokenVisualizerControl1" runat="server"></cc1:SecurityTokenVisualizerControl>
</asp:Content>