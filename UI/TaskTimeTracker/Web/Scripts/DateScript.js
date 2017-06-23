
function Fillup(dateRange, txtFrom, txtTo) {
   
        ApplicationContainer.UI.Web.AutoComplete.FillUpDate(dateRange,onSucess, onError);
        
        function onSucess(Dates) {
            txtFrom.value = Dates[0];
            txtTo.value = Dates[1];
         
        }
        function onError(Dates) {
            alert('error');
        }
      
    }
