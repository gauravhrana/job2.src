IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CommissionCodeSearch') 
BEGIN
	DROP Procedure CommissionCodeSearch
END
GO

CREATE Procedure dbo.CommissionCodeSearch
(
		@CommissionCodeId				INT		= NULL
	,	@CommissionCodeCode				VARCHAR(500) = NULL
	,	@BrokerId				INT		= NULL
	,	@ApplicationId						INT	=	 NULL
	,	@AuditId						INT
	,	@AuditDate						DATETIME 					= NULL
	,	@SystemEntityType				VARCHAR(50)					= 'CommissionCode'
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
	IF  @CommissionCodeCode  IS NULL OR LEN(RTRIM(LTRIM(@CommissionCodeCode))) = 0
	BEGIN
		SET	@CommissionCodeCode = '%'
	END

	SELECT 
			a. CommissionCodeId
		,	a. CommissionCodeCode
		,	a. CommissionCodeDescription
		,	a. BrokerId
		,	Broker.Name AS Broker
		,	a. ApplicationId
	INTO		#TempMain
	FROM		dbo.CommissionCode a
	INNER JOIN Broker ON Broker.BrokerId = a.BrokerId
	WHERE	a.ApplicationId = ISNULL(@ApplicationId, a.ApplicationId)
	AND		a.CommissionCodeId = ISNULL(@CommissionCodeId, a.CommissionCodeId)
	AND		a.CommissionCodeCode	LIKE	@CommissionCodeCode + '%'
	AND		a.BrokerId = ISNULL(@BrokerId, a.BrokerId)
	ORDER BY	a.CommissionCodeId ASC

	IF	@ApplicationMode = 1 
	BEGIN
		DELETE FROM #TempMain
		WHERE CommissionCodeId < 0
	END

	IF @ReturnAuditInfo = 1
	BEGIN

		-- get Audit latest record matching on key, systementitytype
		SELECT	c.EntityKey
			,	MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'
		INTO		#HistortyInfo
		FROM 		#TempMain a	
		INNER JOIN	CommonServices.dbo.AuditHistory c ON	c.EntityKey			= a.CommissionCodeId
		AND		c.SystemEntityId	= @SystemEntityTypeId
		AND		c.AuditActionId		IN (1,2)
		GROUP BY	c.EntityKey	

		-- Get Audit Date and CreatedByPersonId for given records
		SELECT	a.CommissionCodeId
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId
			, 	c.CreatedDate					AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName	AS	'UpdatedBy'
			,	d.Name							AS	'LastAction'
		INTO		#HistortyInfoDetails
		FROM		#TempMain a
		INNER JOIN	#HistortyInfo							b ON	b.EntityKey			= a.CommissionCodeId
		INNER JOIN	CommonServices.dbo.AuditHistory			c ON	c.AuditHistoryId	= b.MaxAuditHistoryId
		INNER JOIN	CommonServices.dbo.AuditAction			d ON	c.AuditActionId 	= d.AuditActionId
		INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e ON	c.CreatedByPersonId	= e.ApplicationUserId

		-- Show full details
		SELECT	a.*
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
		FROM	#TempMain		a
		LEFT JOIN	#HistortyInfoDetails	b ON	a.CommissionCodeId=b.CommissionCodeId
		ORDER BY	a.CommissionCodeId
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
		ORDER BY	a.CommissionCodeId
	END

	IF @AddAuditInfo = 1 
	BEGIN
		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert 
				@SystemEntityType	= @SystemEntityType
			,	@EntityKey			= @CommissionCodeId
			,	@AuditAction		= 'Search'
			,	@CreatedDate		= @AuditDate
			,	@CreatedByPersonId	= @AuditId
	END

END
GO