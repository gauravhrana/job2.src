using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;

namespace Framework.UI.Web.BaseClasses
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:BaseClasses.ControlDetails runat=server></{0}:BaseClasses.ControlDetails>")]
	public abstract class ControlDetails : ControlCommon
    {
        private int _setId;
        public int SetId
        {
	        get
            {
                return _setId;
            }

	        set
	        {
		        _setId = value;
		        ShowData(_setId);
	        }
        }

		#region variables
	
		//public string BackGroundColor
		//{
		//	get
		//	{
		//		return MainTable.BgColor;
		//	}
		//	set
		//	{
		//		MainTable.BgColor = value;
		//	}

		//}
		#endregion

	    protected virtual void ShowData(int setId)
	    {

	    }

		//public virtual void SetBackgroundColor(string borderColor)
		//{
		//	MainTable.BgColor = borderColor;
		//}

		protected System.Web.UI.HtmlControls.HtmlTable MainTable			{ get; set; }

		protected List<Label> LabelListCore									{ get; set; }

	    protected virtual List<Label> GetLabels()
	    {
		    return new List<Label>();
	    }

		protected string DictionaryLabel { get; set; }

		protected override void Clear()
		{

		}

		protected virtual void PopulateLabelsText()
		{
			UIHelper.PopulateLabelsText(ValidColumns, PrimaryEntity, SessionVariables.RequestProfile.AuditId, GetLabels());
		}

		protected virtual Dictionary<string, string> ValidColumns
		{
			get
			{
				Dictionary<string, string> validColumns;

				// first time add to chache
				if (Cache[DictionaryLabel] == null)
				{
					validColumns = UIHelper.GetLabelDictonaryObject(PrimaryEntity, SessionVariables.RequestProfile.AuditId);

					Cache.Add(DictionaryLabel
							  , validColumns
							  , null
							  , System.Web.Caching.Cache.NoAbsoluteExpiration
							  , new TimeSpan(0, 0, 60)
							  , System.Web.Caching.CacheItemPriority.Default
							  , null);
				}
				else
				{
					validColumns = (Dictionary<string, string>)Cache[DictionaryLabel];
				}

				return validColumns;
			}
		}

		protected void PopulateLabelsTextInternal(List<Label> labelslist, string cacheConstantsDictionary)
		{
			UIHelper.PopulateLabelsText(ValidColumns, PrimaryEntity, SessionVariables.RequestProfile.AuditId, labelslist);
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			EnableControl(SessionVariables.IsTesting, PlaceHolderCore.Controls);
		}

    }
}
