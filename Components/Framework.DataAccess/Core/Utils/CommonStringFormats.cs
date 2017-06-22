using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.CommonServices.BusinessDomain.Utils
{
	#region class StringFormats
	public class CommonStringFormats
	{
        /// <summary>
        /// "-"
        /// </summary>
		public static readonly string EmptyStringSign = "-";


        /// <summary>
        /// "nnn.nn"
        /// </summary>
        public static readonly string PercentMaskFormat2 = "nnn.nn";


        /// <summary>
        /// "-nnn,nnn,nnn,nnn,nnn,nnn,nnn,nnn,nnn"
        /// </summary>
        public static readonly string MoneyMaskFormat = "-nnn,nnn,nnn,nnn,nnn,nnn,nnn,nnn,nnn";

        

        /// <summary>
        /// "-nnn,nnn,nnn,nnn,nnn,nnn,nnn,nnn,nnn.nn"
        /// </summary>
        public static readonly string MoneyMaskFormat2 = "-nnn,nnn,nnn,nnn,nnn,nnn,nnn,nnn,nnn.nn";
        
        /// <summary>
        /// "-nnn,nnn,nnn,nnn,nnn,nnn,nnn,nnn,nnn.nnnn"
        /// </summary>
        public static readonly string MoneyMaskFormat4 = "-nnn,nnn,nnn,nnn,nnn,nnn,nnn,nnn,nnn.nnnn";
        
        /// <summary>
        /// "-nnn,nnn,nnn,nnn,nnn,nnn,nnn,nnn,nnn.nnnnnnn"
        /// </summary>
        public static readonly string MoneyMaskFormat7 = "-nnn,nnn,nnn,nnn,nnn,nnn,nnn,nnn,nnn.nnnnnnn";

        /// <summary>
        /// "#,##0;-#,##0;0"
        /// </summary>
		public static readonly string MoneyFormat = "#,##0;-#,##0;0";


		/// <summary>
		/// "#,##0.0;-#,##0.0;0.0"
		/// </summary>
		public static readonly string MoneyFormat1 = "#,##0.0;-#,##0.0;0.0";


        /// <summary>
        /// "#,##0.00;-#,##0.00;0.00"
        /// </summary>
        public static readonly string MoneyFormat2 = "#,##0.00;-#,##0.00;0.00";
		
        /// <summary>
        /// "#,##0.0000;-#,##0.0000;0.0000"
        /// </summary>
        public static readonly string MoneyFormat4 = "#,##0.0000;-#,##0.0000;0.0000";

        /// <summary>
        /// "#,##0.0000000;-#,##0.0000000;0.0000000"
        /// </summary>
        public static readonly string MoneyFormat7 = "#,##0.0000000;-#,##0.0000000;0.0000000";


		/// <summary>
		/// "#,##0;-#,##0;0"
		/// </summary>
		public static readonly string MoneyFormatMillions3 = "#,##0.000M;(#,##0.000M);0.000M";


        /// <summary>
        /// "#,##0;(#,##0);0";
        /// </summary>
        public static readonly string MoneyAccountingFormat = "#,##0;(#,##0);0";

        /// <summary>
        /// "#,##0.0;(#,##0.0);0.0"
        /// </summary>
		public static readonly string MoneyAccountingFormat1 = "#,##0.0;(#,##0.0);0.0";

        /// <summary>
        /// "#,##0.00;(#,##0.00);0.00"
        /// </summary>
		public static readonly string MoneyAccountingFormat2 = "#,##0.00;(#,##0.00);0.00";

        /// <summary>
        /// "#,##0.000;(#,##0.000);0.000"
        /// </summary>
		public static readonly string MoneyAccountingFormat3 = "#,##0.000;(#,##0.000);0.000";

        /// <summary>
        /// "#,##0;-#,##0;0"
        /// </summary>
        public static readonly string MoneyAccountingFormat5 = "#,##0;-#,##0;0";

        /// <summary>
        /// "#,##0.00;-#,##0.00;0.00"
        /// </summary>
        public static readonly string MoneyAccountingFormat6 = "#,##0.00;-#,##0.00;0.00";

        /// <summary>
        /// "#,##0.0;(#,##0.0);N/A";
        /// </summary>
        public static readonly string MoneyAccountingWithNAForZeroFormat1 = "#,##0.0;(#,##0.0);N/A";

        /// <summary>
        /// "#,##0.00;(#,##0.00);N/A"
        /// </summary>
        public static readonly string MoneyAccountingWithNAForZeroFormat2 = "#,##0.00;(#,##0.00);N/A";

        /// <summary>
        /// "#,##0.000;(#,##0.000);N/A"
        /// </summary>
        public static readonly string MoneyAccountingWithNAForZeroFormat3 = "#,##0.000;(#,##0.000);N/A";


        /// <summary>
        /// "#,##0.0000;(#,##0.0000);0.0000"
        /// </summary>
		public static readonly string MoneyAccountingFormat4 = "#,##0.0000;(#,##0.0000);0.0000";

        /// <summary>
        /// "$#,##0;($#,##0);$0"
        /// </summary>
		public static readonly string CurrencyAccountingFormat = "$#,##0;($#,##0);$0";

        /// <summary>
        /// "$#,##0.0;($#,##0.0);$0.0"
        /// </summary>
		public static readonly string CurrencyAccountingFormat1 = "$#,##0.0;($#,##0.0);$0.0";
        
        /// <summary>
        /// "$#,##0.00;($#,##0.00);$0.00"
        /// </summary>
		public static readonly string CurrencyAccountingFormat2 = "$#,##0.00;($#,##0.00);$0.00";

        /// <summary>
        /// "#,##0;(#,##0);N/A";
        /// </summary>
        public static readonly string IntegerWithNAForZeroFormat = "#,##0;(#,##0);N/A";
        
        /// <summary>
        /// "$-n,nnn,nnn,nnn,nnn.n"
        /// </summary>
        public static readonly string CurrencyAccountingMaskFormat1 = "$-n,nnn,nnn,nnn,nnn.n";
        /// <summary>
        /// "$-n,nnn,nnn,nnn,nnn.nn"
        /// </summary>
        public static readonly string CurrencyAccountingMaskFormat2 = "$-n,nnn,nnn,nnn,nnn.nn";

        /// <summary>
        /// "#,##0.0\%;(#,##0.0\%);0.0\%"
        /// </summary>
		public static readonly string PercentAccountingFormat1 = @"#,##0.0\%;(#,##0.0\%);0.0\%";
		
        /// <summary>
        /// "#,##0.00\%;(#,##0.00\%);0.00\%"
        /// </summary>
        public static readonly string PercentAccountingFormat2 = @"#,##0.00\%;(#,##0.00\%);0.00\%";

        /// <summary>
        /// "#,##0.00%;(#,##0.00%);0.00%"
        /// </summary>
        public static readonly string Percent100AccountingFormat2 = @"#,##0.00%;(#,##0.00%);0.00%";


		/// <summary>
		/// "#,##0\%;-#,##0\%;0\%"
		/// </summary>
		public static readonly string PercentFormat = @"#,##0\%;-#,##0\%;0\%";

        /// <summary>
        /// "#,##0.0\%;-#,##0.0\%;0.0\%"
        /// </summary>
		public static readonly string PercentFormat1 = @"#,##0.0\%;-#,##0.0\%;0.0\%";
		
        /// <summary>
        /// "#,##0.00\%;-#,##0.00\%;0.00\%"
        /// </summary>
        public static readonly string PercentFormat2 = @"#,##0.00\%;-#,##0.00\%;0.00\%";
		
        /// <summary>
        /// "+#,##0.00\%;-#,##0.00\%;0.00\%"
        /// </summary>
        public static readonly string PercentFormatWithSign2 = @"+#,##0.00\%;-#,##0.00\%;0.00\%";
        
        
        /// <summary>
        /// "#,##0%;-#,##0%;0%"
        /// </summary>
        public static readonly string Percent100Format = @"#,##0%;-#,##0%;0%";

        /// <summary>
        /// "#,##0.00%;-#,##0.00%;0.00%"
        /// </summary>
        public static readonly string Percent100Format2 = @"#,##0.00%;-#,##0.00%;0.00%";


        /// <summary>
        /// "#,##0.0%;-#,##0.0%;0.0%"
        /// </summary>
        public static readonly string Percent100Format1 = @"#,##0.0%;-#,##0.0%;0.0%";

        /// <summary>
        /// "#,##0.000%;-#,##0.000%;0.000%"
        /// </summary>
        public static readonly string Percent100Format3 = @"#,##0.000%;-#,##0.000%;0.000%";
        

        /// <summary>
        /// "#,##0.00%;-#,##0.00%;0.00%"
        /// </summary>
        public static readonly string Percent100Format5 = @"#,##0.00000%;-#,##0.00000%;0.00000%";

        /// <summary>
        /// "d-MMM-yy"
        /// </summary>
		public static readonly string DateFormat = "d-MMM-yy";


        public static readonly string DateTimeShortFormat = "d-MMM-yy hh:mm";

        public static readonly string DateTimeFormat = "d-MMM-yy hh:mm:ss";
		/// <summary>
		/// "d-MMM-yy"
		/// </summary>
		public static readonly string DateFormatLongTime = "d-MMM-yy hh:mm:ss tt 'GMT'zz";

        /// <summary>
        /// "d"
        /// </summary>
        public static readonly string DateLongFormat = "d";

        /// <summary>
        /// "d"
        /// </summary>
		public static readonly string DateShortFormat = "d";

        /// <summary>
        /// MM/dd/yyyy
        /// </summary>
		public static readonly string DateMediumFormat = "MM/dd/yyyy";



        public static readonly string PromptSelect = "[Select...]";


	}
	#endregion class StringFormats
}
