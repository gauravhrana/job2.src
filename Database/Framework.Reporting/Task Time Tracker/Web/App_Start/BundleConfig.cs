using System.Web;
using System.Web.Optimization;

namespace MvcApplication3
{
	public class BundleConfig
	{
		private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles)
		{
			Log.Debug("RegisterBundles()");

			BundleTable.EnableOptimizations = false;

			// ***
			// Style
			// ***

			bundles.Add(new StyleBundle("~/bundles/none").Include("~/Content/none.styles.css", "~/Content/none.ng-grid.css"));
			//bundles.Add(new StyleBundle("~/bundles/amelia").Include("~/Content/amelia.styles.css", "~/Content/amelia.ng-grid.css"));
			//bundles.Add(new StyleBundle("~/bundles/cerulean").Include("~/Content/cerulean.styles.css", "~/Content/cerulean.ng-grid.css"));
			//bundles.Add(new StyleBundle("~/bundles/cosmo").Include("~/Content/cosmo.styles.css", "~/Content/cosmo.ng-grid.css"));
			//bundles.Add(new StyleBundle("~/bundles/cyborg").Include("~/Content/cyborg.styles.css", "~/Content/cyborg.ng-grid.css"));
			//bundles.Add(new StyleBundle("~/bundles/darkly").Include("~/Content/darkly.styles.css", "~/Content/darkly.ng-grid.css"));
			//bundles.Add(new StyleBundle("~/bundles/flatly").Include("~/Content/flatly.styles.css", "~/Content/flatly.ng-grid.css"));
			//bundles.Add(new StyleBundle("~/bundles/journal").Include("~/Content/journal.styles.css", "~/Content/journal.ng-grid.css"));
			//bundles.Add(new StyleBundle("~/bundles/lumen").Include("~/Content/lumen.styles.css", "~/Content/lumen.ng-grid.css"));
			//bundles.Add(new StyleBundle("~/bundles/readable").Include("~/Content/readable.styles.css", "~/Content/readable.ng-grid.css"));
			//bundles.Add(new StyleBundle("~/bundles/simplex").Include("~/Content/simplex.styles.css", "~/Content/simplex.ng-grid.css"));
			//bundles.Add(new StyleBundle("~/bundles/slate").Include("~/Content/slate.styles.css", "~/Content/slate.ng-grid.css"));
			//bundles.Add(new StyleBundle("~/bundles/spacelab").Include("~/Content/spacelab.styles.css", "~/Content/spacelab.ng-grid.css"));
			//bundles.Add(new StyleBundle("~/bundles/superhero").Include("~/Content/superhero.styles.css", "~/Content/superhero.ng-grid.css"));
			//bundles.Add(new StyleBundle("~/bundles/united").Include("~/Content/united.styles.css", "~/Content/united.ng-grid.css"));
			//bundles.Add(new StyleBundle("~/bundles/yeti").Include("~/Content/yeti.styles.css", "~/Content/yeti.ng-grid.css"));

			bundles.Add(new StyleBundle("~/bundles/css").Include(
				"~/Styles/Reset.css",
				"~/Styles/Site.css",
				"~/Content/Site.css",
				"~/Content/Tabs.css",
				"~/Styles/Controls/Search.css",

				"~/Content/css/adf.css",
				"~/Content/animations.css",

				"~/bower_components/select2/select2.css",
				"~/bower_components/select2/select2.bootstrap.css",
				"~/bower_components/components-font-awesome/css/font-awsome.css",
				"~/bower_components/angular-ui-grid/ui-grid.css",
				"~/bower_components/ng-table/dist/ng-table.min.css",

				"~/Content/css/angular-multi-select.css",
				"~/Content/app.css"
			));

			bundles.Add(new StyleBundle("~/Content/sliderMenu").Include(
				"~/Styles/SlideAndPushMenu/component.css",
				"~/Styles/font-awesome/css/font-awesome.min.css"
			));

			bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
				"~/bower_components/jquery-ui/themes/base/core.css",
				"~/bower_components/jquery-ui/themes/base/resizable.css",
				"~/bower_components/jquery-ui/themes/base/selectable.css",
				"~/bower_components/jquery-ui/themes/base/accordion.css",
				"~/bower_components/jquery-ui/themes/base/autocomplete.css",
				"~/bower_components/jquery-ui/themes/base/button.css",
				"~/bower_components/jquery-ui/themes/base/dialog.css",
				"~/bower_components/jquery-ui/themes/base/slider.css",
				"~/bower_components/jquery-ui/themes/base/tabs.css",
				"~/bower_components/jquery-ui/themes/base/datepicker.css",
				"~/bower_components/jquery-ui/themes/base/progressbar.css",
				"~/bower_components/jquery-ui/themes/base/theme.css"));

			// bootstrap
			bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
				"~/bower_components/bootstrap/dist/css/bootstrap.css",
				"~/bower_components/bootstrap/dist/css/bootstrap-theme.css"
			));

			// Kendo
			bundles.Add(new StyleBundle("~/Content/kendo").Include(
				"~/bower_components/kendo-ui-core/styles/kendo.common.min.css",
				"~/bower_components/kendo-ui-core/styles/kendo.rtl.min.css",
				"~/bower_components/kendo-ui-core/styles/kendo.default.min.css"
			));

			// custom accordion
			bundles.Add(new StyleBundle("~/Content/accordion").Include(
				"~/Styles/Nunito.css",
				"~/Styles/Accordin.css"
			));

			// ***
			// Scripts
			// ***

			// jQuery
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(

				"~/bower_components/jquery/dist/jquery.js"

				, "~/Scripts/json2.js"
				, "~/Scripts/jquery.cookie.js"

				, "~/bower_components/select2/select2.js"
				, "~/bower_components/numeral/numeral.js"
				, "~/bower_components/underscore/underscore.js"
				, "~/bower_components/ace-builds/src/ace.js"

				, "~/Scripts/Showdown.js"
				, "~/Scripts/highcharts/js/highcharts.js"
			));

			bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
				"~/bower_components/jquery-ui/jquery-ui.js"
			));

			// move to bower ?
			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
				"~/Scripts/jquery.unobtrusive*"
				, "~/Scripts/jquery.validate*"
			));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
				"~/Scripts/modernizr-2.8.3.js"
			));

			// angluar
			bundles.Add(new ScriptBundle("~/bundles/angular").Include(
					"~/bower_components/angular-full/angular.min.js"
				, "~/bower_components/angular-animate/angular-animate.min.js"
				, "~/bower_components/angular-full/angular-sanitize.min.js"
				, "~/bower_components/angular-full/angular-route.min.js"
				, "~/bower_components/angular-cookies/angular-cookies.min.js"

				, "~/bower_components/angular-resource/angular-resource.js"
				, "~/bower_components/angular-markdown-directive/markdown.js"

				, "~/bower_components/angular-strap/dist/angular-strap.js"
				, "~/bower_components/angular-strap/dist/angular-strap.tpl.js"

				, "~/bower_components/angular-bootstrap/ui-bootstrap-tpls.js"
				, "~/bower_components/Sortable/Sortable.js"

				, "~/bower_components/angular-numeraljs/dist/angular-numeraljs.js"
				, "~/bower_components/restangular/dist/restangular.min.js"

				, "~/Scripts/select2.ui.js"

				, "~/bower_components/ng-table/dist/ng-table.min.js"
				, "~/bower_components/angular-ui-grid/ui-grid.js"

				, "~/bower_components/angular-strap/dist/modules/dimensions.js"
				, "~/bower_components/angular-strap/dist/modules/tooltip.js"
				, "~/bower_components/angular-strap/dist/modules/date-parser.js"
				, "~/bower_components/angular-strap/dist/modules/datepicker.js"

				, "~/bower_components/highcharts/highcharts.js"
				, "~/bower_components/highcharts-ng/dist/highcharts-ng.min.js"

                , "~/bower_components/ngStorage/src/angularLocalStorage.js"
			));

			// rdash
			bundles.Add(new ScriptBundle("~/bundles/rdash").Include(
                  "~/app/core/directives/rdash/loading.js"
				, "~/app/core/directives/rdash/widget-body.js"
				, "~/app/core/directives/rdash/widget-footer.js"
				, "~/app/core/directives/rdash/widget-header.js"
				, "~/app/core/directives/rdash/widget.js"
			));

			// Business JS
			bundles.Add(new ScriptBundle("~/bundles/AppConfiguration").Include(
				"~/app/app.js"
            ));

            // CapitalMarkets
            bundles.Add(new ScriptBundle("~/bundles/CM").Include(

                // Capital Markets application Services and Controllers
				 "~/app/BM/CapitalMarkets/controllers/*.js"
				, "~/app/BM/CapitalMarkets/services/*.js"

                , "~/app/BM/CapitalMarkets/services/Gen/*.js"
                , "~/app/BM/CapitalMarkets/controllers/Gen/Detail/*.js"
                , "~/app/BM/CapitalMarkets/controllers/Gen/List/*.js"
                , "~/app/BM/CapitalMarkets/controllers/Gen/Save/*.js"
                , "~/app/BM/CapitalMarkets/controllers/Gen/CrossReference/*.js"

                , "~/app/BM/CapitalMarkets/controllers/rdash/masterCtrl.js"
			));

			// DayCare
			bundles.Add(new ScriptBundle("~/bundles/DayCare").Include(

				// Day Care application Services and Controllers
				 "~/app/BM/DayCare/controllers/*.js"
				
                 , "~/app/BM/DayCare/services/*.js"
				, "~/app/BM/DayCare/services/Gen/*.js"

				, "~/app/BM/DayCare/controllers/*.js"
				, "~/app/BM/DayCare/controllers/Gen/List/*.js"
                , "~/app/BM/DayCare/controllers/Gen/Detail/*.js"
                , "~/app/BM/DayCare/controllers/Gen/Save/*.js"

				, "~/app/BM/DayCare/controllers/rdash/masterCtrl.js"
                
			));

			// TimeEntry
			bundles.Add(new ScriptBundle("~/bundles/TimeEntry").Include(
                "~/app/BM/TimeEntry/app.js",
                "~/app/core/controllers/rdash/masterCtrl.js",
                "~/app/core/services/userPreferenceUtilityService.js",

				// application Services and Controllers
				//"~/app/BM/TimeEntry/Services/*.js",
				"~/app/BM/TimeEntry/controllers/*.js",

				"~/app/examples/Services/*.js",
				"~/app/examples/controllers/*.js",

                "~/app/core/services/*.js",
                "~/app/schedule/services/*.js",
                "~/app/schedule/controllers/*.js"

			));

			// SystemAdmin
			bundles.Add(new ScriptBundle("~/bundles/SystemAdmin").Include(

				"~/app/BM/SystemAdmin/app.js",
                "~/app/core/controllers/rdash/masterCtrl.js",
                "~/app/core/services/userPreferenceUtilityService.js",

				// application Services and Controllers
				"~/app/BM/SystemAdmin/controllers/*.js",

				"~/app/examples/Services/*.js",
				"~/app/examples/controllers/*.js"
			));

			// Legal
			bundles.Add(new ScriptBundle("~/bundles/Legal").Include(
                
                 "~/app/core/services/userPreferenceUtilityService.js"

				// application Services and Controllers
				, "~/app/BM/Legal/services/Gen/*.js"
				, "~/app/BM/Legal/controllers/Gen/Detail/*.js"
				, "~/app/BM/Legal/controllers/Gen/List/*.js"
				, "~/app/BM/Legal/controllers/Gen/Save/*.js"

                // including References to Transaction Type related files
                , "~/app/BM/CapitalMarkets/services/Gen/transactionTypeService.js"
                , "~/app/BM/CapitalMarkets/controllers/Gen/Detail/detailTransactionTypeCtrl.js"
                , "~/app/BM/CapitalMarkets/controllers/Gen/List/listTableTransactionTypeCtrl.js"
                , "~/app/BM/CapitalMarkets/controllers/Gen/Save/saveTransactionTypeCtrl.js"

                , "~/app/BM/Legal/controllers/rdash/masterCtrl.js"
                
			));

			// ApplicationAdministration
			bundles.Add(new ScriptBundle("~/bundles/ApplicationAdministration").Include(

				"~/app/BM/ApplicationAdministration/app.js",

				// application Services and Controllers
				"~/app/BM/ApplicationAdministration/Services/*.js",
				"~/app/BM/ApplicationAdministration/controllers/*.js"
			));

			// ReferenceData
			bundles.Add(new ScriptBundle("~/bundles/ReferenceData").Include(

				"~/app/BM/ReferenceData/app.js",

                "~/app/core/controllers/rdash/masterCtrl.js",
                "~/app/core/services/userPreferenceUtilityService.js",

				// application Services and Controllers
				"~/app/BM/ReferenceData/Services/*.js",
				"~/app/BM/ReferenceData/controllers/*.js"
            ));

            // Prototype
            bundles.Add(new ScriptBundle("~/bundles/Prototype").Include(

                "~/app/SM/Prototype/app.js",
                "~/app/core/controllers/rdash/masterCtrl.js",
                "~/app/core/services/userPreferenceUtilityService.js",

                // application Services and Controllers
                "~/app/SM/Prototype/Services/*.js",
                "~/app/SM/Prototype/controllers/*.js"
            ));

			// Business JS common ?
			bundles.Add(new ScriptBundle("~/bundles/mvc").Include(

				// Ace Editor
				"~/bower_components/ace-builds/src/ace.js",
				"~/bower_components/angular-ui-ace/ui-ace.js",

				//directives
				"~/app/core/directives/fillHeight.js",
				"~/app/core/directives/ngEnter.js",
				"~/app/core/directives/angular-multi-select.js",

				//core controllers
				"~/app/core/controllers/*.js",

				// field configuration service
				"~/app/core/services/fieldConfigurationService.js",

				// data autocomplete service
				"~/app/core/services/dataAutoCompleteService.js",

				// schedule
				"~/app/schedule/controllers/*.js",
				"~/app/schedule/services/*.js",

				// admin
				"~/app/admin/controllers/*.js",
				"~/app/admin/services/*.js",

				// demo
				"~/app/BM/Demo/controllers/*.js",
				"~/app/BM/Demo/services/*.js",

				//report dashboard widgets
				"~/app/reports/widgets/markdown/markdown.js",
				"~/app/reports/widgets/grid/grid.js",
				"~/app/reports/widgets/table/table.js",
				"~/app/reports/widgets/barchart/barchart.js",

				// scratch control
				"~/app/configuration/controllers/testCtrl.js",

				//factories
				"~/app/reports/factories/dashboardStateFactory.js",

				//services
				"~/app/configuration/services/referenceDataService.js",
				"~/app/configuration/services/calculationService.js",
				"~/app/configuration/services/calculationSelectionService.js",
				"~/app/configuration/services/detailService.js",
				"~/app/configuration/services/genevaQueryService.js",
				"~/app/configuration/services/summaryService.js",
				"~/app/core/services/utilityUIService.js",
				"~/app/core/services/userService.js",
				"~/app/core/services/loggingService.js",
				"~/app/process/services/processRunInfoService.js",
				"~/app/process/services/processInfoService.js",
				"~/app/process/services/processRequestService.js",
				"~/app/reports/services/explorerService.js",
				"~/app/reports/services/reportService.js",
				"~/app/reports/services/displayFormatService.js",
				"~/app/core/services/modalService.js",
				"~/app/reports/services/dashboardService.js",
				"~/app/core/services/cachingService.js"
			));

            // Business Angular common JS
            bundles.Add(new ScriptBundle("~/bundles/CoreAngular").Include(
                "~/app/core/services/menuService.js")
            );

			// Bootstrap
			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
				"~/bower_components/bootstrap/dist/js/bootstrap.js")
            );

			// Kendo -- TODO : these all seem WRONG
			bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
					"~/Scripts/Kendo/Full/kendo.all.min.js"
				, "~/Scripts/library/kendo.js"
				, "~/Scripts/Kendo/Full/kendo.angular.min.js"
				, "~/scripts/kendo/full/kendo.web.min.js"
				, "~/Prototype/Kendo/content/shared/js/console.js"
			));

			// Our Code
			bundles.Add(new ScriptBundle("~/bundles/appScripts").Include(
				"~/scripts/SearchControl.js",
				"~/scripts/ListControl.js"
			));

			//bundles.Add(new ScriptBundle("~/bundles/sliderMenu").Include(
			//"~/Scripts/SlideAndPushMenu/modernizr.custom.js"));

			// bower components

			bundles.Add(new StyleBundle("~/Content/bower").Include(
				"~/bower_components/angular-ui-tree/dist/angular-ui-tree.min.css"
				, "~/bower_components/angular-tree-control/css/tree-control.css"
			));

			bundles.Add(new ScriptBundle("~/bundles/bower").Include(

				"~/bower_components/angular-ui-tree/dist/angular-ui-tree.js"
				, "~/bower_components/angular-tree-control/angular-tree-control.js"

				, "~/bower_components/d3/d3.min.js"

				, "~/bower_components/react/react.js"
				, "~/bower_components/react/react-with-addons.js"
				, "~/bower_components/react/JSXTransformer.js"
				, "~/bower_components/ngReact/ngReact.js"
				
			));

		}
	}
}
