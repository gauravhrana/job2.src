<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="KendoEditor.ascx.cs" Inherits="Shared.UI.Web.Controls.KendoTextEditor.KendoEditor" %>


 <div id="example" class="k-content">
    <textarea id="editor" rows="10" cols="30" style="height:140px; width:800px;"  >
        
    </textarea>
     <asp:HiddenField ID="hdnDescription" runat="server" Value="kendo"   />
    <script>
        function OnChange(e) {        
            SetDescription(); return true;
        }
        

        $(document).ready(function () {

            var value = $("#<%= hdnDescription.ClientID %>").val();
            $("#editor").val(value);
            $("#editor").kendoEditor({
                change: OnChange
            });
            $('#editor').bind('input propertychange', function() {
                // As control having runat="server" their ids get changed
                // selector would be like thisn
                $("#<%= hdnDescription.ClientID %>").val($(this).text());
                alert($("#<%= hdnDescription.ClientID %>").val());
            });
        });
    </script>
     <script type="text/javascript">
        
         function SetDescription() {
             var txtarea = document.getElementById("editor");
             var hdndescription = document.getElementById('<%=hdnDescription.ClientID%>');
             hdndescription.value = txtarea.value;
             return false;
         }
     </script> 
</div>