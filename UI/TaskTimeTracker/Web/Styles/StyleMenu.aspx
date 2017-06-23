<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StyleMenu.aspx.cs" Inherits="ApplicationContainer.UI.Web.Styles.StyleMenu" EnableViewState="false"  %>



.gutter-border [class*="col-"] {
  border: 1px solid #ddd;
  border: 1px solid rgba(86,61,124,.2);
}

.summary-bgcolor
{    
    background-color: #bfcbd6;
}
div.menu
{
    /* padding: 4px 0px 4px 8px; */
}

    div.menu ul
    {
        list-style: none;
        margin: 0px;
        padding: 0px;
        width: auto;
        z-index: 999;
    }

        div.menu ul li a, div.menu ul li a:visited
        {
            background-color: #465c71;
            border: 1px #4e667d solid;
            color: #dde4ec;
            display: block;
            line-height: 1.35em;
            padding: 4px 20px;
            text-decoration: none;
            white-space: nowrap;
        }

            div.menu ul li a:hover
            {
                background-color: #bfcbd6;
                color: #465c71;
                text-decoration: none;
            }

            div.menu ul li a:active
            {
                background-color: #465c71;
                color: #cfdbe6;
                text-decoration: none;
            }

.menuContainer
{
}

div.app-main-menu
{
}

div.app-main-menu ul
{
	list-style: none;
	padding: 0px;
    margin: 0px;
	width: auto;
	z-index: 999;
}
				
div.app-main-menu ul li a:active
{
	/* background-color: #465c71; */ 
	color: #cfdbe6;
	text-decoration: none;
}

div.app-main-menu ul li a, div.app-main-menu ul li a:visited
{
	display: block;
	/* line-height: 1.35em;
    padding: 4px 20px; */
    padding: 0px;
	text-decoration: none;
	white-space: nowrap;
	border: 1px solid <%=GetMenuBorderColor()%>;
} 

div.app-main-menu ul li a:hover
{ 
    color: #465c71;
    text-decoration: none;
    background-color:<%=GetMenuHoverColor()%>;
} 

a.stdMenuItem                                                           
{                                                                      
    color:<%=GetMenuForeGroundColor()%> !important ;                     
    background-color:<%=GetMenuBackGroundColor()%> !important;           
    font-family:<%=GetMenuFontFamily()%> !important;                     
    font-size:<%=GetMenuFontSize()%> !important;                         
    padding: 4px 20px !important;                     
    line-height: 1.35em;            
    display: block; text-decoration: none; white-space: nowrap;        
} 

a.stdMenuItem:hover	                            
{                                                    
    color: black  !important ;                      
    text-decoration: none;                          
    background:<%=GetMenuHoverColor()%> !important ;  
}   

span.stdMenuItem                                                           
{                                                                      
    color:<%=GetMenuForeGroundColor()%> !important ;                     
    background-color:<%=GetMenuBackGroundColor()%> !important;           
    font-family:<%=GetMenuFontFamily()%> !important;                     
    font-size:<%=GetMenuFontSize()%> !important;                         
    padding: 4px 20px !important;                     
    line-height: 1.35em;            
    display: block; text-decoration: none; white-space: nowrap;        
} 

span.stdMenuItem:hover	                            
{                                                    
    color: black  !important ;                      
    text-decoration: none;                          
    background:<%=GetMenuHoverColor()%> !important ;  
}                                      

.highlightMenuItem                                                      
{                                                                        
    background-color: yellow !important;                                
    font-family:<%=GetMenuFontFamily()%> !important;                      
    font-size:<%=GetMenuFontSize()%> !important;                          
    padding: 6px 22px !important;                  
    line-height: 1.35em;            
    display: block; text-decoration: none; white-space: nowrap;        
} 	

span.highlightMenuItem                                                      
{                                                                        
    background-color: yellow !important;                                
    font-family:<%=GetMenuFontFamily()%> !important;                      
    font-size:<%=GetMenuFontSize()%> !important;                          
    padding: 6px 22px !important;                  
    line-height: 1.35em;            
    display: block; text-decoration: none; white-space: nowrap;        
} 			

.highlightMenuCategoryMenuItem                                                      
{                                                                        
	background-color:<%=GetMenuColoredCategoryColor()%> !important;                                
	font-family:<%=GetMenuFontFamily()%> !important;                      
	font-size:<%=GetMenuFontSize()%> !important;                          
	padding: 6px 22px !important;                  
	line-height: 1.35em;            
	display: block; text-decoration: none; white-space: nowrap;        
} 

span.highlightMenuCategoryMenuItem                                                      
{                                                                        
	background-color:<%=GetMenuColoredCategoryColor()%> !important;                                
	font-family:<%=GetMenuFontFamily()%> !important;                      
	font-size:<%=GetMenuFontSize()%> !important;                          
	padding: 6px 22px !important;                  
	line-height: 1.35em;            
	display: block; text-decoration: none; white-space: nowrap;        
}  


.MenuDefaultMenuItemStyle
{
    background-color: #D5DCE1;
    color: #234875;
    padding: 2px;
    width: 100%;
}

.MenuDefaultSelectedStyle
{
    background-color: #3C5778;
    color: #FFFFFF;
    padding: 2px;
    width: 100%;
}

.MenuDefaultHoverStyle
{
    background-color: #666666;
    color: #FFFFFF;
    padding: 2px;
    width: 100%;
}

.MenuVerticalMenuItemStyle
{
    background-color: #FFFFFF;
    border: 1px solid #D5DCE1;
    color: #234875;
    height: 30px;
    padding: 2px;
    width: 100%;
}

.MenuVerticalSelectedStyle
{
    background-color: #003366;
    border: 1px solid #D5DCE1;
    color: #FFFFFF;
    height: 30px;
    padding: 2px;
    width: 100%;
}

.MenuVerticalHoverStyle
{
    background-color: #EEEEEE;
    border: 1px solid #000000;
    color: #234875;
    height: 30px;
    padding: 2px;
    width: 100%;
}