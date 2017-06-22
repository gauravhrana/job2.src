﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
    public partial class OrderItemDataModel : BaseModel
    {
        [PrimaryKey, IncludeInSearch]
        public int? OrderItemId { get; set; }

		[PrimaryKey, IncludeInSearch]
        public int OrderId                  { get; set; }


		public string SecurityCode          { get; set; }
        public decimal? Quantity            { get; set; }
        public decimal? QuantityFilled      { get; set; }
        public decimal? QuantityOriginal    { get; set; }
        public decimal? PriceLimit          { get; set; }
        public string StrategyCode          { get; set; }
        public string StrategyGroupCode     { get; set; }
        public string ClassificationCode    { get; set; }
        public string BbergCode             { get; set; }
		public string Notes					{ get; set; }
		public decimal? AvgPrice			{ get; set; }
		public decimal? RefPrice			{ get; set; }
		public decimal? TargetBps			{ get; set; }
		public string AutoGeneratedNotes	{ get; set; }
		public decimal? AutoPercentTraded	{ get; set; }
		public decimal? PositionSizeChange  { get; set; }
		public string SubmissionType		{ get; set; }
		public string PrimaryBrokerCode		{ get; set; }
		public string ExecutingBrokerCode	{ get; set; }
		public string RoutingDestination	{ get; set; }
		public string Description			{ get; set; }
		public decimal? TotalOrderPercent   { get; set; }


		//[Newtonsoft.Json.JsonConverter(typeof(NullableDateConverter))]
		public DateTime EventDate { get; set; }

		//[Newtonsoft.Json.JsonConverter(typeof(NullableDateConverter))]
		public DateTime? LastModifiedOn { get; set; }

		//[Newtonsoft.Json.JsonConverter(typeof(NullableDateConverter))]
		public DateTime? ExpireOn { get; set; }

		[Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter))]
		public int? AutoOrderResultTypeId { get; set; }

		[Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter))]
        public int? BbergUniqueId            { get; set; }

		[Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter))]
        public int? LastOrderStatusId        { get; set; }


		[Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter))]
		public int? RefFxRate { get; set; }

        
		
        [ForeignKey("OrderRequest"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? OrderRequestId { get; set; }

        [ForeignKeyName("OrderRequest", "OrderRequestId", "OrderRequestId", "Notes"), OnlyProperty]
        public string OrderRequest { get; set; }

        [ForeignKey("OrderAction"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? OrderActionId { get; set; }

        [ForeignKeyName("OrderAction", "OrderActionId", "OrderActionId", "OrderActionCode"), OnlyProperty]
        public string OrderAction { get; set; }

        [ForeignKey("OrderType"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? OrderTypeId { get; set; }

        [ForeignKeyName("OrderType", "OrderTypeId", "OrderTypeId", "Code"), OnlyProperty]
        public string OrderType { get; set; }

        [ForeignKey("Strategy"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? StrategyId { get; set; }

        [ForeignKeyName("Strategy", "StrategyId", "StrategyId", "Name"), OnlyProperty]
        public string Strategy { get; set; }

        [ForeignKey("Security"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? SecurityId { get; set; }

        [ForeignKeyName("Security", "SecurityId", "SecurityId", "Name"), OnlyProperty]
        public string Security { get; set; }

        [ForeignKey("Portfolio"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? PortfolioId { get; set; } 

        [ForeignKeyName("Portfolio", "PortfolioId", "PortfolioId", "Name"), OnlyProperty]
        public string Portfolio { get; set; }

           
    }
}
