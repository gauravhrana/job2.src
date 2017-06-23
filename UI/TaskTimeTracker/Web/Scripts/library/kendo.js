
function libary_kendo_getData_groupby(localUrl, localElement, primaryEntity, fcModeId) {
    $(localElement).kendoComboBox({
        placeholder: 'Select ...'
    }
    );

    $.ajax(
    {
            type: 'POST'
        ,   url: localUrl
        ,   data: "{ 'entityName': '" + primaryEntity + "', 'mode': '" + fcModeId + "' }"
        ,   contentType: 'application/json; charset=utf-8'
        ,   dataType: 'json'
        ,   success: function (msg)
        {
                $(localElement).kendoComboBox(
                {
                    dataSource: msg.d
                    , dataTextField: 'FieldConfigurationDisplayName'
                    , dataValueField: 'Value'
                    , placeholder: 'Select ...'
                    , dataBound: function () {
                        var dataSource = this.dataSource;
                        var data = dataSource.data();

                        if (!this._adding) {
                            // check for "None" option before adding it.
                            if (data.length > 0 && data[0].Name != "None") {
                                this._adding = true;
                                data.splice(0, 0, { "FieldConfigurationDisplayName" : "None", "Name": "None" });
                                this._adding = false;
                            }
                        }
                    }
                    , filter: 'startswith'
                });
            }
        ,   error: function (request, status, error)
            {
                alert(request.responseText);
            }
    });
}

function libary_kendo_getData(localUrl, localElement, localDataTextField, localDataValueField, localAddElement)
{

    $(localElement).kendoComboBox({
        placeholder: 'Select ...'
        }
    );

    $.ajax(
    {
            type: 'POST'
        ,	url: localUrl
        ,	data:'{}'
        ,	contentType: 'application/json; charset=utf-8'
        ,   dataType: 'json'        
        ,	success:    function (msg)
        {
                $(localElement).kendoComboBox(
                {
                        dataSource: msg.d
                    ,   dataTextField: localDataTextField
                    ,	dataValueField: localDataValueField
                    ,   placeholder: 'Select ...'
                    ,	dataBound:	function() 
                                    {
                                        var dataSource = this.dataSource;
                                        var data = dataSource.data();

                                        if (localAddElement && !this._adding) {

                                            // check for "All" option before adding it.
                                            if (data.length > 0 && data[0][localDataTextField] != "All") {
                                                var var1 = '{"' + localDataTextField + '": "All", "' + localDataValueField + '": "-1" }';
                                                var addParam = JSON.parse(var1);

                                                this._adding = true;
                                                data.splice(0, 0, addParam);
                                                this._adding = false;
                                            }
                                            else {
                                                var var1 = '{"' + localDataTextField + '": "Select...", "' + localDataValueField + '": "-1" }';
                                                var addParam = JSON.parse(var1);

                                                this._adding = true;
                                                data.splice(0, 0, addParam);
                                                this._adding = false;

                                            }
                                        }
                                    }                                
                    ,	filter: 'startswith'
                });
            }
        ,   error: function (request, status, error)
            {
                alert(request.responseText);
            }
        });
}

function libary_kendo_cascade_getData(localUrl, localElement, localDataTextField, localDataValueField, localAddElement, cascadeParent) {
    $(localElement).kendoComboBox({
        placeholder: 'Select ...'
    }
    );

    $.ajax(
    {
        type: 'POST'
        , url: localUrl
        , data: '{}'
        , contentType: 'application/json; charset=utf-8'
        , dataType: 'json'
        , success: function (msg) {
            $(localElement).kendoComboBox(
            {
                    dataSource: msg.d
                ,   cascadeFrom: cascadeParent
                ,   dataTextField: localDataTextField
                ,   dataValueField: localDataValueField
                ,   placeholder: 'Select ...'
                ,   dataBound: function () {
                    var dataSource = this.dataSource;
                    var data = dataSource.data();

                    if (localAddElement && !this._adding) {

                        // check for "All" option before adding it.
                        if (data.length > 0 && data[0][localDataTextField] != "All") {
                            var var1 = '{"' + localDataTextField + '": "All", "' + localDataValueField + '": "-1" }';
                            var addParam = JSON.parse(var1);

                            this._adding = true;
                            data.splice(0, 0, addParam);
                            this._adding = false;
                        }
                    }
                }
                //, filter: 'startswith'
            });
        }
        , error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}

function onResize() {
    //var completionList = $find("AutoCompleteEx").get_completionList();
}


function pageLoad() {
    //$find("AutoCompleteEx").get_element().focus();
    //$addHandler(window, "resize", onResize);
}


function getLeft(e) {
    var offset = e.offsetLeft;
    if (e.offsetParent != null) offset += getLeft(e.offsetParent);
    return offset;
}
function getTop(e) {
    var offset = e.offsetTop;
    if (e.offsetParent != null) offset += getTop(e.offsetParent);
    return offset;
}

function SetDevBoxValue(dropdownid, txtbox1id) {
    var dropdown = document.getElementById(dropdownid);
    var txtbox1 = document.getElementById(txtbox1id);
    txtbox1.value = dropdown.value;
}