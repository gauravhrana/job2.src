﻿Type.registerNamespace("Sys.Extended.UI.HtmlEditor.ToolbarButtons");Sys.Extended.UI.HtmlEditor.ToolbarButtons.Copy=function(n){Sys.Extended.UI.HtmlEditor.ToolbarButtons.Copy.initializeBase(this,[n])};Sys.Extended.UI.HtmlEditor.ToolbarButtons.Copy.prototype={canBeShown:function(){return Sys.Extended.UI.HtmlEditor.isIE},callMethod:function(){if(!Sys.Extended.UI.HtmlEditor.ToolbarButtons.Copy.callBaseMethod(this,"callMethod"))return!1;var n=this._designPanel;Sys.Extended.UI.HtmlEditor.isIE?(n.openWait(),setTimeout(function(){n.isShadowed();n._copyCut("c",!0);n.closeWait();n._ifShadow()},0)):n._copyCut("c",!0)}};Sys.Extended.UI.HtmlEditor.ToolbarButtons.Copy.registerClass("Sys.Extended.UI.HtmlEditor.ToolbarButtons.Copy",Sys.Extended.UI.HtmlEditor.ToolbarButtons.MethodButton);