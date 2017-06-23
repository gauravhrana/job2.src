using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;
using DayCare.Components.BusinessLayer;
using DataModel.Framework.DataAccess;
using DataModel.DayCare;

namespace ApplicationContainer.UI.Web.UnitTest.DayCareTest
{
	public partial class Sample : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void Insert_Click(object sender, EventArgs e)
		{
			//var model = new DataModel.DayCare.EventTypeDataModel();
			//model.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
			//model.Description = "Day Care Entities are getting tested using sample Data";
			//model.Name = "Testing Event Type";
			//model.SortOrder = 1;

			//DayCare.Components.BusinessLayer.EventTypeDataManager.Create(model, SessionVariables.RequestProfile);
			
			//var model = new DataModel.DayCare.AccidentPlaceDataModel();
			//model.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
			//model.Description = "Day Care Entities are getting tested using sample Data";
			//model.Name = "Testing Accident Place";
			//model.SortOrder = 1;

			//DayCare.Components.BusinessLayer.AccidentPlaceDataManager.Create(model, SessionVariables.RequestProfile);

			//var model = new DataModel.DayCare.AccidentReportDataModel();
			//model.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
			//model.Description = "Day Care Entities are getting tested using sample Data";
			//model.Name = "Testing Accident Report";
			//model.SortOrder = 1;

			//DayCare.Components.BusinessLayer.AccidentReportDataManager.Create(model, SessionVariables.RequestProfile);

			//var model = new DataModel.DayCare.ActivitySubTypeDataModel();
			//model.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
			//model.Description = "Day Care Entities are getting tested using sample Data";
			//model.Name = "Testing Activity Sub Type";
			//model.SortOrder = 1;

			//DayCare.Components.BusinessLayer.ActivitySubTypeDataManager.Create(model, SessionVariables.RequestProfile);


			//var model = new DataModel.DayCare.ActivityTypeDataModel();
			//model.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
			//model.Description = "Day Care Entities are getting tested using sample Data";
			//model.Name = "Testing Activity Type";
			//model.SortOrder = 1;

			//DayCare.Components.BusinessLayer.ActivityTypeDataManager.Create(model, SessionVariables.RequestProfile);


			//var model = new DataModel.DayCare.BathRoomDataModel();
			//model.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
			//model.Description = "Day Care Entities are getting tested using sample Data";
			//model.Name = "Testing BathRoom Entity";
			//model.SortOrder = 1;

			//DayCare.Components.BusinessLayer.BathRoomDataManager.Create(model, SessionVariables.RequestProfile);

			//var model = new DataModel.DayCare.CommentDataModel();
			//model.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
			//model.Description = "Day Care Entities are getting tested using sample Data";
			//model.Name = "Testing Comment Entity";
			//model.SortOrder = 1;

			//DayCare.Components.BusinessLayer.CommentDataManager.Create(model, SessionVariables.RequestProfile);

			//var model = new DataModel.DayCare.DiaperStatusDataModel();
			//model.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
			//model.Description = "Day Care Entities are getting tested using sample Data";
			//model.Name = "Testing Diaper Status Entity";
			//model.SortOrder = 1;

			//DayCare.Components.BusinessLayer.DiaperStatusDataManager.Create(model, SessionVariables.RequestProfile);

			//var model = new DataModel.DayCare.FoodTypeDataModel();
			//model.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
			//model.Description = "Day Care Entities are getting tested using sample Data";
			//model.Name = "Testing food Type Entity";
			//model.SortOrder = 1;

			//DayCare.Components.BusinessLayer.FoodTypeDataManager.Create(model, SessionVariables.RequestProfile);

			//var model = new DataModel.DayCare.MealTypeDataModel();
			//model.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
			//model.Description = "Day Care Entities are getting tested using sample Data";
			//model.Name = "Testing meal Type Entity";
			//model.SortOrder = 1;

			//DayCare.Components.BusinessLayer.MealTypeDataManager.Create(model, SessionVariables.RequestProfile);

			//var model = new DataModel.DayCare.NeedItemDataModel();
			//model.ApplicationId = SessionVariables.RequestProfile.ApplicationId;
			//model.Description = "Day Care Entities are getting tested using sample Data";
			//model.Name = "Testing Need Type Entity";
			//model.SortOrder = 1;

			//DayCare.Components.BusinessLayer.NeedItemDataManager.Create(model, SessionVariables.RequestProfile);
		}

		protected void Update_Click(object sender, EventArgs e)
		{
			//var model = new DataModel.DayCare.EventTypeDataModel();
			//model.EventTypeId = 37469;
			//model.Name = "Testing Update of EventType";
			//model.Description = "Testing Update of EventType";
			//model.SortOrder = 10;
			//DayCare.Components.BusinessLayer.EventTypeDataManager.Update(model, SessionVariables.RequestProfile);

			//var model = new DataModel.DayCare.AccidentPlaceDataModel();
			//model.AccidentPlaceId = 37477;
			//model.Name = "Testing Update of Accident Place";
			//model.Description = "Testing Update of Accident Place";
			//model.SortOrder = 10;
			//DayCare.Components.BusinessLayer.AccidentPlaceDataManager.Update(model, SessionVariables.RequestProfile);

			//var model = new DataModel.DayCare.AccidentReportDataModel();
			//model.AccidentReportId = 37472;
			//model.Name = "Testing Update of Accident Report";
			//model.Description = "Testing Update of Accident Report";
			//model.SortOrder = 10;
			//DayCare.Components.BusinessLayer.AccidentReportDataManager.Update(model, SessionVariables.RequestProfile);

			//var model = new DataModel.DayCare.ActivitySubTypeDataModel();
			//model.ActivitySubTypeId = 37472;
			//model.Name = "Testing Update of Activity Sub Type";
			//model.Description = "Testing Update of Activity Sub Type";
			//model.SortOrder = 10;
			//DayCare.Components.BusinessLayer.ActivitySubTypeDataManager.Update(model, SessionVariables.RequestProfile);

			//var model = new DataModel.DayCare.ActivityTypeDataModel();
			//model.ActivityTypeId = 37472;
			//model.Name = "Testing Update of Activity  Type";
			//model.Description = "Testing Update of Activity  Type";
			//model.SortOrder = 10;
			//DayCare.Components.BusinessLayer.ActivityTypeDataManager.Update(model, SessionVariables.RequestProfile);

			//var model = new DataModel.DayCare.BathRoomDataModel();
			//model.BathRoomId = 37472;
			//model.Name = "Testing Update of BathRoom";
			//model.Description = "Testing Update of BathRoom";
			//model.SortOrder = 10;
			//DayCare.Components.BusinessLayer.BathRoomDataManager.Update(model, SessionVariables.RequestProfile);

			//var model = new DataModel.DayCare.CommentDataModel();
			//model.CommentId = 37472;
			//model.Name = "Testing Update of Comment";
			//model.Description = "Testing Update of Comment";
			//model.SortOrder = 10;
			//DayCare.Components.BusinessLayer.CommentDataManager.Update(model, SessionVariables.RequestProfile);

			//var model = new DataModel.DayCare.DiaperStatusDataModel();
			//model.DiaperStatusId = 37472;
			//model.Name = "Testing Update of DiaperStatus";
			//model.Description = "Testing Update of DiaperStatus";
			//model.SortOrder = 10;
			//DayCare.Components.BusinessLayer.DiaperStatusDataManager.Update(model, SessionVariables.RequestProfile);

			//var model = new DataModel.DayCare.FoodTypeDataModel();
			//model.FoodTypeId = 37472;
			//model.Name = "Testing Update of FoodType";
			//model.Description = "Testing Update of FoodType";
			//model.SortOrder = 10;
			//DayCare.Components.BusinessLayer.FoodTypeDataManager.Update(model, SessionVariables.RequestProfile);

			//var model = new DataModel.DayCare.MealTypeDataModel();
			//model.MealTypeId = 37472;
			//model.Name = "Testing Update of MealType";
			//model.Description = "Testing Update of MealType";
			//model.SortOrder = 10;
			//DayCare.Components.BusinessLayer.MealTypeDataManager.Update(model, SessionVariables.RequestProfile);

			//var model = new DataModel.DayCare.NeedItemDataModel();
			//model.NeedItemId = 37472;
			//model.Name = "Testing Update of NeedItem";
			//model.Description = "Testing Update of NeedItem";
			//model.SortOrder = 10;
			//DayCare.Components.BusinessLayer.NeedItemDataManager.Update(model, SessionVariables.RequestProfile);
		}

		protected void Search_Click(object sender, EventArgs e)
		{
		}

		protected void Delete_Click(object sender, EventArgs e)
		{

		}


	}
}