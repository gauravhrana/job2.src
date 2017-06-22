<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StyleGrid.aspx.cs" Inherits="ApplicationContainer.UI.Web.Styles.StyleGrid" %>

.mGrid { 
    
 color: <%=GetGridRowForeColor()%>;
    background: <%=GetGridRowBackColor()%>;
    font-size: <%=GetGridRowFontSize()%>;
    line-height: <%=GetGridRowHeight()%>;
}
.mGrid td { 
    padding: 2px; 
    border: solid 1px #c1c1c1;     
}
.mGrid th { 
    padding: 4px 2px;  
    color: <%=GetGridHeaderForeColor()%>;
    background: <%=GetGridHeaderBackColor()%>;
    font-size: <%=GetGridHeaderFontSize()%>;
    line-height: <%=GetGridHeaderHeight()%>;
 
}
.mGrid .alt {
    background: <%=GetGridAlternatingRowBackColor()%>;
    font-size: <%=GetGridAlternatingRowFontSize()%>;
    line-height: <%=GetGridAlternatingRowHeight()%>;
}

