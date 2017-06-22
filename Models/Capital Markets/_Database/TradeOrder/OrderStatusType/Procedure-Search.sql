IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='OrderStatusTypeSearch') 
BEGIN
	DROP Procedure OrderStatusTypeSearch
END
GO

CREATE Procedure dbo.OrderStatusTypeSearch
(
		@OrderStatusTypeId				INT		= NULL
	,	@Code				VARCHAR(500) = NULL
	,	@OrderStatusGroupId				INT		= NULL
	,	@ApplicationId						INT	=	 NULL
	,	@AuditId						INT
	,	@AuditDate						DATETIME 					= NULL
	,	@SystemEntityType				VARCHAR(50)					= 'OrderStatusType'
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
	IF  @Code  IS NULL OR LEN(RTRIM(LTRIM(@Code))) = 0
	BEGIN
		SET	@Code = '%'
	END

	SELECT 
			a. OrderStatusTypeId
		,	a. Code
		,	a. Description
		,	a. OrderStatusGroupId
		,	OrderStatusGroup.OrderStatusGroupCode AS OrderStatusGroup
		,	a. ApplicationId
	INTO		#TempMain
	FROM		dbo.OrderStatusType a
	INNER JOIN OrderStatusGroup ON OrderStatusGroup.OrderStatusGroupId = a.OrderStatusGroupId
	WHERE	a.ApplicationId = ISNULL(@ApplicationId, a.ApplicationId)
	AND		a.OrderStatusTypeId = ISNULL(@OrderStatusTypeId, a.OrderStatusTypeId)
	AND		a.Code	LIKE	@Code + '%'
	AND		a.OrderStatusGroupId = ISNULL(@OrderStatusGroupId, a.OrderStatusGroupId)
	ORDER BY	a.OrderStatusTypeId ASC

	IF	@ApplicationMode = 1 
	BEGIN
		DELETE FROM #TempMain
		WHERE OrderStatusTypeId < 0
	END

	IF @ReturnAuditInfo = 1
	BEGIN

		-- get Audit latest record matching on key, systementitytype
		SELECT	c.EntityKey
			,	MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'
		INTO		#HistortyInfo
		FROM 		#TempMain a	
		INNER JOIN	CommonServices.dbo.AuditHistory c ON	c.EntityKey			= a.OrderStatusTypeId
		AND		c.SystemEntityId	= @SystemEntityTypeId
		AND		c.AuditActionId		IN (1,2)
		GROUP BY	c.EntityKey	

		-- Get Audit Date and CreatedByPersonId for given records
		SELECT	a.OrderStatusTypeId
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId
			, 	c.CreatedDate					AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName	AS	'UpdatedBy'
			,	d.Name							AS	'LastAction'
		INTO		#HistortyInfoDetails
		FROM		#TempMain a
		INNER JOIN	#HistortyInfo							b ON	b.EntityKey			= a.OrderStatusTypeId
		INNER JOIN	CommonServices.dbo.AuditHistory			c ON	c.AuditHistoryId	= b.MaxAuditHistoryId
		INNER JOIN	CommonServices.dbo.AuditAction			d ON	c.AuditActionId 	= d.AuditActionId
		INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e ON	c.CreatedByPersonId	= e.ApplicationUserId

		-- Show full details
		SELECT	a.*
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
		FROM	#TempMain		a
		LEFT JOIN	#HistortyInfoDetails	b ON	a.OrderStatusTypeId=b.OrderStatusTypeId
		ORDER BY	a.OrderStatusTypeId
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
		ORDER BY	a.OrderStatusTypeId
	END

	IF @AddAuditInfo = 1 
	BEGIN
		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert 
				@SystemEntityType	= @SystemEntityType
			,	@EntityKey			= @OrderStatusTypeId
			,	@AuditAction		= 'Search'
			,	@CreatedDate		= @AuditDate
			,	@CreatedByPersonId	= @AuditId
	END

END
GO
