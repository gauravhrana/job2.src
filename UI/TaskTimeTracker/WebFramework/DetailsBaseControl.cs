//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.ComponentModel;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//namespace Shared.UI.WebFramework
//{
//    [DefaultProperty("Text")]
//    [ToolboxData("<{0}:DetailsBaseControl runat=server></{0}:DetailsBaseControl>")]
//    public abstract class DetailsBaseControl : BaseControl
//    {

//        #region Properties

//        private int _setId;
//        public int SetId
//        {
//            get
//            {
//                return _setId;
//            }

//            set
//            {
//                _setId = value;
//                ShowData(_setId);
//            }
//        }

//        #endregion

//        #region Methods

//        protected virtual void ShowData(int setId)
//        {
//        }

//        protected virtual void SetBorderClass(string className)
//        {
//        }

//        protected virtual void SetBackgroundColor(string borderColor)
//        {
//        }

//        protected abstract void Clear();

//        protected abstract void PopulateLabelsText();
		

//        #endregion

//        protected virtual Void GetValidColumns()
//        {

//        }


//        protected virtual void EnableControl(bool enabled, ControlCollection controls)
//        {
//            foreach (Control childControl in controls)
//            {
//                try
//                {
//                    var webChildControl = (WebControl)childControl;
//                    webChildControl.Enabled = enabled;
//                }
//                catch
//                {
//                    //
//                }
//                finally
//                {
//                    // not sure if this is correct 
//                    EnableControl(enabled, childControl.Controls);
//                }
//            }
//        }


//    }
//}
