IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='OrderStatusSearch') 
BEGIN
	DROP Procedure OrderStatusSearch
END
GO

CREATE Procedure dbo.OrderStatusSearch
(
		@OrderStatusId				INT		= NULL
	,	@OrderId				INT		= NULL
	,	@Comments				VARCHAR(500) = NULL
	,	@OrderStatusTypeId				INT		= NULL
	,	@ApplicationId						INT	=	 NULL
	,	@AuditId						INT
	,	@AuditDate						DATETIME 					= NULL
	,	@SystemEntityType				VARCHAR(50)					= 'OrderStatus'
	,	@ApplicationMode				INT							= NULL
	,	@AddAuditInfo					INT							= 1
	,	@AddTraceInfo					INT							= 0
	,	@ReturnAuditInfo				INT							= 0
)
WITH RECOMPILE
AS
BEGIN


	DECLARE @SystemEntityTypeId AS INT
	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)

	--if blank, then assume search on all possiblities ('%')
	IF  @Comments  IS NULL OR LEN(RTRIM(LTRIM(@Comments))) = 0
	BEGIN
		SET	@Comments = '%'
	END

	SELECT 
			a. OrderStatusId
		,	a. OrderId
		,	a. Comments
		,	a. LastModifiedBy
		,	a. LastModifiedOn
		,	a. OrderStatusTypeId
		,	OrderStatusType.Code AS OrderStatusType
		,	a. ApplicationId
	INTO		#TempMain
	FROM		dbo.OrderStatus a
	INNER JOIN OrderStatusType ON OrderStatusType.OrderStatusTypeId = a.OrderStatusTypeId
	WHERE	a.ApplicationId = ISNULL(@ApplicationId, a.ApplicationId)
	AND		a.OrderStatusId = ISNULL(@OrderStatusId, a.OrderStatusId)
	AND		a.OrderId = ISNULL(@OrderId, a.OrderId)
	AND		a.Comments	LIKE	@Comments + '%'
	AND		a.OrderStatusTypeId = ISNULL(@OrderStatusTypeId, a.OrderStatusTypeId)
	ORDER BY	a.OrderStatusId ASC

	IF	@ApplicationMode = 1 
	BEGIN
		DELETE FROM #TempMain
		WHERE OrderStatusId < 0
	END

	IF @ReturnAuditInfo = 1
	BEGIN

		-- get Audit latest record matching on key, systementitytype
		SELECT	c.EntityKey
			,	MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'
		INTO		#HistortyInfo
		FROM 		#TempMain a	
		INNER JOIN	CommonServices.dbo.AuditHistory c ON	c.EntityKey			= a.OrderStatusId
		AND		c.SystemEntityId	= @SystemEntityTypeId
		AND		c.AuditActionId		IN (1,2)
		GROUP BY	c.EntityKey	

		-- Get Audit Date and CreatedByPersonId for given records
		SELECT	a.OrderStatusId
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId
			, 	c.CreatedDate					AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName	AS	'UpdatedBy'
			,	d.Name							AS	'LastAction'
		INTO		#HistortyInfoDetails
		FROM		#TempMain a
		INNER JOIN	#HistortyInfo							b ON	b.EntityKey			= a.OrderStatusId
		INNER JOIN	CommonServices.dbo.AuditHistory			c ON	c.AuditHistoryId	= b.MaxAuditHistoryId
		INNER JOIN	CommonServices.dbo.AuditAction			d ON	c.AuditActionId 	= d.AuditActionId
		INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e ON	c.CreatedByPersonId	= e.ApplicationUserId

		-- Show full details
		SELECT	a.*
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
		FROM	#TempMain		a
		LEFT JOIN	#HistortyInfoDetails	b ON	a.OrderStatusId=b.OrderStatusId
		ORDER BY	a.OrderStatusId
	END
	ELSE
	BEGIN
		DECLARE @StaticUpdatedDate AS DATETIME
		SET @StaticUpdatedDate = Convert(datetime, '1/1/1900', 103)

		SELECT	a.*
			,	UpdatedDate = @StaticUpdatedDate
			,	UpdatedBy	= 'Unknown'
			,	LastAction	= 'Unknown'
		FROM	#TempMain a	
		ORDER BY	a.OrderStatusId
	END

	IF @AddAuditInfo = 1 
	BEGIN
		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert 
				@SystemEntityType	= @SystemEntityType
			,	@EntityKey			= @OrderStatusId
			,	@AuditAction		= 'Search'
			,	@CreatedDate		= @AuditDate
			,	@CreatedByPersonId	= @AuditId
	END

END
GO
