<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Style.aspx.cs" Inherits="ApplicationContainer.UI.Web.Styles.Style" EnableViewState="false" %>

.Dynamic
{
    font-size: <%=GetSize()%>
    font: 22pt verdana;
    font-weight:200;
    color:orange;   
}

.searchtd
{
    /*border-color: <%=GetColor()%>;
    border-width: <%=GetWidth()%>;
    border-style: <%=GetStyle()%>; */
    text-align: left;
}
