using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using DataModel.Framework.Core;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.TimeTracking;
using ApplicationContainer.UI.Web.CommonCode;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.UI.Web.BaseClasses;
using Framework.Components.DataAccess;

namespace ApplicationContainer.UI.Web.Scheduling.Schedule.Controls
{
	public partial class ScheduleDetailInsert : ControlGeneric
	{

		public DataTable ActivityCategoryData
		{
			get
			{
				if (ViewState["ActivityCategoryData"] == null)
				{
					ViewState["ActivityCategoryData"] = ScheduleDetailActivityCategoryDataManager.GetList(SessionVariables.RequestProfile);
				}
				return (DataTable)ViewState["ActivityCategoryData"];
			}
		}		
		public static int InitialValue = 0;

		public void SetId(int setId)
		{
			ViewState["SetId"] = setId;

			// load data
			//GetData((int)ViewState["SetId"]);
		}

		public void SetGridViewFooter(DataTable dt)
		{
			var labelTotal = gvScheduleDetails.FooterRow.FindControl("lblDateDiffHrsTotal");

			if (labelTotal != null)
			{
				foreach (DataRow chkrow in dt.Rows)
				{
					object value = chkrow["DateDiffHrs"];
					if (value != DBNull.Value)
						((Label)labelTotal).Text = (from row in dt.AsEnumerable() select Convert.ToDecimal(row["DateDiffHrs"])).Sum().ToString("0.00");
					else
						((Label)labelTotal).Text = "0.00";
				}


			}
		}

		protected void GetData(int scheduleId)
		{
			var data = new ScheduleDetailDataModel();
			data.ScheduleId = scheduleId;

			var dt = ScheduleDetailDataManager.Search(data, SessionVariables.RequestProfile);

			if (dt.Rows.Count > 0)
			{
				ViewState["CurrentTable"] = dt;
				gvScheduleDetails.DataSource = dt;
				gvScheduleDetails.DataBind();

				SetGridViewFooter(dt);
			}
			else
			{
				//AddNewRow();
				dt.Rows.Add(dt.NewRow());

				var getMaxDetailId = GetData();
				dt.Rows[0]["ScheduleDetailId"] = getMaxDetailId + 1;

				dt.Rows[0]["ScheduleId"] = scheduleId;
				dt.Rows[0]["InTime"] = DateTime.Now;
				dt.Rows[0]["OutTime"] = DateTime.Now;
				dt.Rows[0]["Message"] = "";
				dt.Rows[0]["WorkTicket"] = "N/A";
				dt.Rows[0]["CreatedDate"] = DateTime.Now;

				ViewState["CurrentTable"] = dt;

				gvScheduleDetails.DataSource = dt;
				gvScheduleDetails.DataBind();

				SetGridViewFooter(dt);
			}
		}

		protected int GetData()
		{
			var data = new ScheduleDetailDataModel();
			var dt = ScheduleDetailDataManager.Search(data, SessionVariables.RequestProfile);
			var maxRow = dt.Select("ScheduleDetailId = MAX(ScheduleDetailId)");
			var maxScheduleDetailId = Convert.ToInt32(maxRow[0]["ScheduleDetailId"].ToString());

			return maxScheduleDetailId + 1;
		}

		protected void gvScheduleDetails_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			var getMaxDetailId = GetData();

			var scheduleId = (int)ViewState["SetId"];

			if (e.CommandName.Equals("ADD"))
			{
				if (ViewState["CurrentTable"] != null)
				{
					var dt = (DataTable)ViewState["CurrentTable"];

					if (dt.Rows.Count > 0)
					{
						DataRow dr = null;
						//add a new blank row in dt.
						dr = dt.NewRow();

						var rowID = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["ScheduleDetailId"]);
						if (rowID <= getMaxDetailId)
							dr["ScheduleDetailId"] = getMaxDetailId + 1;
						else
							dr["ScheduleDetailId"] = rowID + 1;

						dr["ScheduleId"] = scheduleId;
						dr["InTime"] = DateTime.Now;
						dr["OutTime"] = DateTime.Now;
						dr["DateDiffHrs"] = 0;
						dr["Message"] = "";
						dr["WorkTicket"] = "N/A";
						dr["CreatedDate"] = DateTime.Now;


						// update the changed values of the previous rows in grid to dt.
						for (var i = 0; i < dt.Rows.Count; i++)
						{
							var drpActivityCategory = (DropDownList)gvScheduleDetails.Rows[i].FindControl("drpScheduleDetailActivityCategory");

							var txtEditInTime = (TextBox)gvScheduleDetails.Rows[i].FindControl("txtInsertInTime");
							var txtEditOutTime = (TextBox)gvScheduleDetails.Rows[i].FindControl("txtInsertOutTime");
							var txtEditMessage = (TextBox)gvScheduleDetails.Rows[i].FindControl("txtInsertMsg");
							var txtWorkTicket = (TextBox)gvScheduleDetails.Rows[i].FindControl("txtWorkTicket");

							dt.Rows[i]["ScheduleDetailActivityCategoryId"] = Convert.ToInt32(drpActivityCategory.SelectedValue);
							dt.Rows[i]["InTime"] = txtEditInTime.Text;
							dt.Rows[i]["OutTime"] = txtEditOutTime.Text;
							dt.Rows[i]["Message"] = txtEditMessage.Text;
							dt.Rows[i]["WorkTicket"] = txtWorkTicket.Text.Trim();

							var timeSpent = Convert.ToDateTime(dt.Rows[i]["OutTime"]) - Convert.ToDateTime(dt.Rows[i]["InTime"]);
							dt.Rows[i]["DateDiffHrs"] = timeSpent.TotalHours;

						}
						dt.Rows.Add(dr);

					}
					ViewState["CurrentTable"] = dt;

					gvScheduleDetails.DataSource = dt; // bind new datatable to grid
					gvScheduleDetails.DataBind();

					SetGridViewFooter(dt);
				}
			}

			if (e.CommandName.Equals("Delete"))
			{
				if (ViewState["CurrentTable"] != null)
				{
					var dt = (DataTable)ViewState["CurrentTable"];
					var ScheduleDetailID = (List<int>)ViewState["DeletedIds"];

					var gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
					var rowIndex = gvr.RowIndex;

					var lblScheduleDetailID = (Label)gvScheduleDetails.Rows[rowIndex].FindControl("lblScheduleDetailId");

					ScheduleDetailID.Add(Convert.ToInt32(lblScheduleDetailID.Text));

					ViewState["DeletedIds"] = ScheduleDetailID;
					dt.Rows.RemoveAt(rowIndex);
					ViewState["CurrentTable"] = dt;

					gvScheduleDetails.DataSource = dt; // bind new datatable to grid
					gvScheduleDetails.DataBind();

					SetGridViewFooter(dt);
				}
			}

		}

		protected void AddNewRow()
		{
			var getMaxDetailId = GetData();
			var scheduleId = (int)ViewState["SetId"];

			if (ViewState["CurrentTable"] != null)
			{
				var dt = (DataTable)ViewState["CurrentTable"];
				DataRow dr = null;

				if (dt.Rows.Count > 0)
				{
					dr = dt.NewRow();

					if (dt.Rows[dt.Rows.Count - 1]["ScheduleDetailId"] != DBNull.Value)
					{
						var rowID = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["ScheduleDetailId"]);

						if (rowID <= getMaxDetailId)
							dr["ScheduleDetailId"] = getMaxDetailId + 1;
						else
							dr["ScheduleDetailId"] = rowID + 1;
					}
					else
						dr["ScheduleDetailId"] = getMaxDetailId + 1;

					dr["ScheduleDetailActivityCategoryId"] = ActivityCategoryData.Rows[0]["ScheduleDetailActivityCategoryId"];
					dr["ScheduleDetailActivityCategory"] = ActivityCategoryData.Rows[0]["Name"].ToString();
					dr["ScheduleId"] = scheduleId;
					dr["InTime"] = DateTime.Now;
					dr["OutTime"] = DateTime.Now;
					dr["Message"] = "";
					dr["WorkTicket"] = "N/A";
					dr["CreatedDate"] = DateTime.Now;
					dr["DateDiffHrs"] = 0;

					dt.Rows.Add(dr);

					ViewState["CurrentTable"] = dt;

					gvScheduleDetails.DataSource = dt; // bind new datatable to grid
					gvScheduleDetails.DataBind();

					SetGridViewFooter(dt);
				}
			}
		}

		protected void lbtnAdd_Click(object sender, EventArgs e)
		{
			AddNewRow();
		}

		protected void gvScheduleDetails_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				var activityCategory = e.Row.FindControl("drpScheduleDetailActivityCategory");
				if (activityCategory != null)
				{
					var drpActivityCategory = (DropDownList)activityCategory;

					UIHelper.LoadDropDown(ActivityCategoryData, drpActivityCategory, ScheduleDetailActivityCategoryDataModel.DataColumns.Name,
						ScheduleDetailActivityCategoryDataModel.DataColumns.ScheduleDetailActivityCategoryId);

					if (((System.Data.DataRowView)(e.Row.DataItem)).Row["ScheduleDetailActivityCategoryId"] != DBNull.Value)
					{
						drpActivityCategory.SelectedValue = Convert.ToString(((System.Data.DataRowView)(e.Row.DataItem)).Row["ScheduleDetailActivityCategoryId"]);
					}

					//drpActivityCategory.SelectedValue = e.Row.DataItem
				}
			}

			if (e.Row.RowState == DataControlRowState.Edit)
			{
				TextBox tb = (TextBox)gvScheduleDetails.Rows[e.Row.RowIndex].FindControl("txtEditMessage");
				tb.Width = 400;
			}
			if (e.Row.RowType == DataControlRowType.Footer)
			{
				int getMaxDetailId = GetData();
				int scheduleID = (int)ViewState["SetId"];
			}
		}

		protected void gvScheduleDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{ }

		protected void gvScheduleDetails_Updating(object sender, GridViewUpdateEventArgs e)
		{
			var lblScheduleDetailID = (Label)gvScheduleDetails.Rows[e.RowIndex].FindControl("lblScheduleDetailId");
			var lblEditScheduleId = (Label)gvScheduleDetails.Rows[e.RowIndex].FindControl("lblScheduleId");
			var drpScheduleDetailActivityCategory = (DropDownList)gvScheduleDetails.Rows[e.RowIndex].FindControl("drpScheduleDetailActivityCategory");
			var txtEditInTime = (TextBox)gvScheduleDetails.Rows[e.RowIndex].FindControl("txtEditInTime");
			var txtEditOutTime = (TextBox)gvScheduleDetails.Rows[e.RowIndex].FindControl("txtEditOutTime");
			var txtWorkTicket = (TextBox)gvScheduleDetails.Rows[e.RowIndex].FindControl("txtWorkTicket");
			var txtEditMessage = (TextBox)gvScheduleDetails.Rows[e.RowIndex].FindControl("txtEditMessage");
			var lblCreatedDate = (Label)gvScheduleDetails.Rows[e.RowIndex].FindControl("txtEditCreatedDate");

			var data = new ScheduleDetailDataModel();
			data.ScheduleDetailId = Convert.ToInt32(lblScheduleDetailID.Text);
			data.ScheduleId = Convert.ToInt32(lblEditScheduleId.Text);
			data.ScheduleDetailActivityCategoryId = Convert.ToInt32(drpScheduleDetailActivityCategory.SelectedValue);

			var dtInTime = Convert.ToDateTime(txtEditInTime.Text.Trim());
			data.InTime = DateTime.Parse(dtInTime.ToString("t"), null);

			var dtOutTime = Convert.ToDateTime(txtEditOutTime.Text.Trim());
			data.OutTime = DateTime.Parse(dtOutTime.ToString("t"), null);

			data.WorkTicket = txtWorkTicket.Text.Trim();
			data.Message = txtEditMessage.Text;
			data.CreatedDate = DateTime.Parse(lblCreatedDate.Text);

			ScheduleDetailDataManager.Update(data, SessionVariables.RequestProfile);
			GetData(Convert.ToInt32(lblEditScheduleId.Text));
			gvScheduleDetails.EditIndex = -1;
			gvScheduleDetails.DataBind();
		}

		protected void gvScheduleDetails_Editing(object sender, GridViewEditEventArgs e)
		{
			GetData((int)ViewState["SetId"]);
			gvScheduleDetails.EditIndex = e.NewEditIndex;
			gvScheduleDetails.DataBind();
		}

		protected void gvScheduleDetails_CancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			GetData((int)ViewState["SetId"]);
			gvScheduleDetails.EditIndex = -1;
			gvScheduleDetails.DataBind();
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey = "ScheduleDetail";
		}

		protected override void OnLoad(EventArgs e)
		{
			if (!IsPostBack)
			{
				GetData((int)ViewState["SetId"]);
				ViewState["DeletedIds"] = new List<int>();
			}
		}

		protected DataTable GetDataOnly(int scheduleId)
		{
			var data = new ScheduleDetailDataModel();
			data.ScheduleId = scheduleId;

			var dt = ScheduleDetailDataManager.Search(data, SessionVariables.RequestProfile);
			return dt;
		}

		protected void lbtnSubmit_Click(object sender, EventArgs e)
		{
			try
			{
				var dt = GetDataOnly((int)ViewState["SetId"]);

				foreach (GridViewRow row in gvScheduleDetails.Rows)
				{
					var lblScheduleDetailID = (Label)gvScheduleDetails.Rows[row.RowIndex].FindControl("lblScheduleDetailId");
					var foundIds = dt.Select("ScheduleDetailId = '" + lblScheduleDetailID.Text + "'");
					var lblScheduleId = (Label)gvScheduleDetails.Rows[row.RowIndex].FindControl("lblScheduleId");
					var drpScheduleDetailActivityCategory = (DropDownList)gvScheduleDetails.Rows[row.RowIndex].FindControl("drpScheduleDetailActivityCategory");
					var txtInsertInTime = (TextBox)gvScheduleDetails.Rows[row.RowIndex].FindControl("txtInsertInTime");
					var txtInsertOutTime = (TextBox)gvScheduleDetails.Rows[row.RowIndex].FindControl("txtInsertOutTime");
					var txtWorkTicket = (TextBox)gvScheduleDetails.Rows[row.RowIndex].FindControl("txtWorkTicket");
					var txtInsertMsg = (TextBox)gvScheduleDetails.Rows[row.RowIndex].FindControl("txtInsertMsg");
					var lblCreatedDate = (Label)gvScheduleDetails.Rows[row.RowIndex].FindControl("lblCreatedDate");

					var data = new ScheduleDetailDataModel();

					data.ScheduleDetailId = Convert.ToInt32(lblScheduleDetailID.Text);
					data.ScheduleId = Convert.ToInt32(lblScheduleId.Text);
					data.ScheduleDetailActivityCategoryId = Convert.ToInt32(drpScheduleDetailActivityCategory.SelectedValue);
					data.WorkTicket = txtWorkTicket.Text.Trim();
					data.Message = txtInsertMsg.Text;

					var dtInTime = Convert.ToDateTime(txtInsertInTime.Text.Trim());
					data.InTime = DateTime.Parse(dtInTime.ToString("t"), null);

					var dtOutTime = Convert.ToDateTime(txtInsertOutTime.Text.Trim());
					data.OutTime = DateTime.Parse(dtOutTime.ToString("t"), null);

					// adding a day in case some one finishes a task next day. 
					if (data.OutTime.Value.Hour < 5 && data.InTime.Value.Hour > 5)
					{
						data.OutTime = data.OutTime.Value.AddDays(1);
					}

					if (foundIds.Length != 0)
					{
						data.CreatedDate = DateTime.Parse(lblCreatedDate.Text);

						ScheduleDetailDataManager.Update(data, SessionVariables.RequestProfile);
					}

					else
					{

						var dataInsert = new ScheduleDetailDataModel();
						dataInsert.ScheduleId = Convert.ToInt32(lblScheduleId.Text);

						var dataSearch = ScheduleDetailDataManager.GetDetails(dataInsert, SessionVariables.RequestProfile);
						if (dataSearch.Rows.Count == 0)
						{
							ScheduleDetailDataManager.Create(data, SessionVariables.RequestProfile);

						}
						else
						{
							for (var i = 0; i < dataSearch.Rows.Count; i++)
							{
								if (data.InTime.Equals(dataSearch.Rows[i][ScheduleDetailDataModel.DataColumns.InTime]))
								{
									Session["msg"] = "INTIME should be UNIQUE for Single ScheduleId";
									Response.Redirect(Page.GetRouteUrl("ScheduleEntityRoute", new { Action = "Update", SetId = data.ScheduleId.ToString() }) , false);
									throw new Exception("INTIME should be UNIQUE for Single ScheduleId");
								}
								else
								{
									ScheduleDetailDataManager.Create(data, SessionVariables.RequestProfile);
									break;
								}

							}

						}
					}

				}

				var ScheduleDetailIDList = (List<int>)ViewState["DeletedIds"];

				foreach (var ScheduleDetailID in ScheduleDetailIDList)
				{
					var data = new ScheduleDetailDataModel();

					data.ScheduleDetailId = ScheduleDetailID;
					ScheduleDetailDataManager.Delete(data, SessionVariables.RequestProfile);
				}

				GetData((int)ViewState["SetId"]);
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
				
			}
		}

	}
}