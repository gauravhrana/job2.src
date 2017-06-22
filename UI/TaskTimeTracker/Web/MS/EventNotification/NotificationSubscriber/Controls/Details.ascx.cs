﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;

namespace ApplicationContainer.UI.Web.EventNotification.NotificationSubscriber.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
    {

        #region private methods

        protected override void ShowData(int notificationSubscriberId)
        {
            base.ShowData(notificationSubscriberId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new NotificationSubscriberDataModel();
            data.NotificationSubscriberId = notificationSubscriberId;

			var items = Framework.Components.EventMonitoring.NotificationSubscriberDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                SetData(item);

                oHistoryList.Setup(PrimaryEntity, notificationSubscriberId, "NotificationSubscriber");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblNotificationSubscriberIdText, lblNameText, lblDescriptionText, lblSortOrderText });
            }

            return LabelListCore;
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new NotificationSubscriberDataModel();

            SetData(data);
        }

        public void SetData(NotificationSubscriberDataModel item)
        {
            SystemKeyId = item.NotificationSubscriberId;

            base.SetData(item);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DictionaryLabel = CacheConstants.NotificationSubscriberLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.NotificationSubscriber;

            PlaceHolderCore = dynNotificationSubscriberId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;

            CoreSystemKey = lblNotificationSubscriberId;
            CoreControlName = lblName;
            CoreControlDescription = lblDescription;
            CoreControlSortOrder = lblSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblNotificationSubscriberIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }
}