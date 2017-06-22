using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using DataModel.Framework.Core;
using DataModel.Framework.Configuration;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityXFunctionalityImage.Controls
{
    public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
    {

        #region variables

        public FunctionalityXFunctionalityImageDataModel SearchParameters
        {
            get
            {
                var data = new FunctionalityXFunctionalityImageDataModel();

                data.FunctionalityId = GetParameterValueAsInt(FunctionalityXFunctionalityImageDataModel.DataColumns.FunctionalityId);

                data.FunctionalityImageId = GetParameterValueAsInt(FunctionalityXFunctionalityImageDataModel.DataColumns.FunctionalityImageId);

                return data;
            }
        }
        
        #endregion

        #region private methods

        private int SaveSearchKey()
        {
            var searchKeyId = 0;
            if (SearchColumns != null)
            {
                var data = new SearchKeyDataModel();
                data.Name = DateTime.Now.ToLongTimeString();
                data.View = "FunctionalityXFunctionalityImage";
                data.SortOrder = 1;
                data.Description = "FunctionalityXFunctionalityImage";

				searchKeyId = Framework.Components.Core.SearchKeyDataManager.Create(data, SessionVariables.RequestProfile);

                foreach (DataRow dr in SearchColumns.Rows)
                {
                    try
                    {
                        var columnName = Convert.ToString(dr["Name"]);
                        var columnValue = CheckAndGetFieldValue(columnName, false).ToString();

                        var dataDetail = new SearchKeyDetailDataModel();
                        dataDetail.SearchKeyId = searchKeyId;

                        //ApplicationCommon.UpdateUserPreference(SettingCategory, columnName, columnValue);
                        dataDetail.SearchParameter = columnName;
                        dataDetail.SortOrder = 1;
						var detailId = Framework.Components.Core.SearchKeyDetailDataManager.Create(dataDetail, SessionVariables.RequestProfile);

                        var dataDetailItem = new SearchKeyDetailItemDataModel();
                        dataDetailItem.SearchKeyDetailId = detailId;
                        dataDetailItem.SortOrder = 1;

                        dataDetailItem.Value = columnValue;
						Framework.Components.Core.SearchKeyDetailItemDataManager.Create(dataDetailItem, SessionVariables.RequestProfile);

                    }
                    catch
                    { }
                }
            }
            return searchKeyId;
        }

        #endregion

        #region Events

        protected void SearchParametersRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var label = (Label)e.Item.FindControl("label");
                var hdnfield = (HiddenField)e.Item.FindControl("hdnfield");
                //var txtbox = (TextBox)e.Item.FindControl("txtbox");
                var txtbox1 = (TextBox)e.Item.FindControl("txtbox1");
                var dropdownlist = (DropDownList)e.Item.FindControl("dropdownlist");

                if (label != null && dropdownlist != null)
                {
                    var name = hdnfield.Value;
                    var data = new FieldConfigurationDataModel();
                    data.Name = name;
                    data.FieldConfigurationModeId = SessionVariables.SearchControlColumnsModeId;
					var dt = Framework.Components.UserPreference.FieldConfigurationDataManager.Search(data, SessionVariables.RequestProfile);
                    if (dt.Rows.Count >= 1)
                    {
                        var controltype = dt.Rows[0]
                            [FieldConfigurationDataModel.DataColumns.ControlType].ToString();
                        //if (controltype.Equals("TextBox"))
                        //{
                        //    txtbox.Visible = true;
                        //    txtbox1.Visible = false;
                        //    dropdownlist.Visible = false;
                        //}
                        //else 
                        if (controltype.Equals("DropDownList"))
                        {
                            //txtbox.Visible = false;
                            txtbox1.Visible = true;
                            dropdownlist.Visible = true;

                            if (name.Equals("FunctionalityId"))
                            {
                                var functionalityData = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityDataManager.GetList(SessionVariables.RequestProfile);
                                UIHelper.LoadDropDown(functionalityData, dropdownlist,
                                    StandardDataModel.StandardDataColumns.Name,
                                    FunctionalityDataModel.DataColumns.FunctionalityId);

                            }

                            if (name.Equals("FunctionalityImageId"))
                            {
								var functionalityImageData = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityImageDataManager.GetList(SessionVariables.RequestProfile);
                                UIHelper.LoadDropDown(functionalityImageData, dropdownlist,
                                FunctionalityImageDataModel.DataColumns.Title,
                                FunctionalityImageDataModel.DataColumns.FunctionalityImageId);

                            }
                        }
                    }
                    e.Item.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(name + "Visibility", SettingCategory);
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityXFunctionalityImage;
            PrimaryEntityKey = "FunctionalityXFunctionalityImage";
            FolderLocationFromRoot = "/Shared/QualityAssurarnce";

            SearchActionBarCore = oSearchActionBar;
            SearchParametersRepeaterCore = SearchParametersRepeater;
        }

        #endregion

    }
}