IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='MSPAFileEventSearch') 
BEGIN
	DROP Procedure MSPAFileEventSearch
END
GO

CREATE Procedure dbo.MSPAFileEventSearch
(
		@MSPAFileEventId				INT		= NULL
	,	@Description				VARCHAR(500) = NULL
	,	@MSPAFileId				INT		= NULL
	,	@TradingEventTypeId				INT		= NULL
	,	@ApplicationId						INT	=	 NULL
	,	@AuditId						INT
	,	@AuditDate						DATETIME 					= NULL
	,	@SystemEntityType				VARCHAR(50)					= 'MSPAFileEvent'
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
	IF  @Description  IS NULL OR LEN(RTRIM(LTRIM(@Description))) = 0
	BEGIN
		SET	@Description = '%'
	END

	SELECT 
			a. MSPAFileEventId
		,	a. Description
		,	a. CreatedBy
		,	a. CreatedOn
		,	a. MSPAFileId
		,	MSPAFile.Filename AS MSPAFile
		,	a. TradingEventTypeId
		,	TradingEventType.Name AS TradingEventType
		,	a. ApplicationId
	INTO		#TempMain
	FROM		dbo.MSPAFileEvent a
	INNER JOIN MSPAFile ON MSPAFile.MSPAFileId = a.MSPAFileId
	INNER JOIN TradingEventType ON TradingEventType.TradingEventTypeId = a.TradingEventTypeId
	WHERE	a.ApplicationId = ISNULL(@ApplicationId, a.ApplicationId)
	AND		a.MSPAFileEventId = ISNULL(@MSPAFileEventId, a.MSPAFileEventId)
	AND		a.Description	LIKE	@Description + '%'
	AND		a.MSPAFileId = ISNULL(@MSPAFileId, a.MSPAFileId)
	AND		a.TradingEventTypeId = ISNULL(@TradingEventTypeId, a.TradingEventTypeId)
	ORDER BY	a.MSPAFileEventId ASC

	IF	@ApplicationMode = 1 
	BEGIN
		DELETE FROM #TempMain
		WHERE MSPAFileEventId < 0
	END

	IF @ReturnAuditInfo = 1
	BEGIN

		-- get Audit latest record matching on key, systementitytype
		SELECT	c.EntityKey
			,	MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'
		INTO		#HistortyInfo
		FROM 		#TempMain a	
		INNER JOIN	CommonServices.dbo.AuditHistory c ON	c.EntityKey			= a.MSPAFileEventId
		AND		c.SystemEntityId	= @SystemEntityTypeId
		AND		c.AuditActionId		IN (1,2)
		GROUP BY	c.EntityKey	

		-- Get Audit Date and CreatedByPersonId for given records
		SELECT	a.MSPAFileEventId
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId
			, 	c.CreatedDate					AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName	AS	'UpdatedBy'
			,	d.Name							AS	'LastAction'
		INTO		#HistortyInfoDetails
		FROM		#TempMain a
		INNER JOIN	#HistortyInfo							b ON	b.EntityKey			= a.MSPAFileEventId
		INNER JOIN	CommonServices.dbo.AuditHistory			c ON	c.AuditHistoryId	= b.MaxAuditHistoryId
		INNER JOIN	CommonServices.dbo.AuditAction			d ON	c.AuditActionId 	= d.AuditActionId
		INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e ON	c.CreatedByPersonId	= e.ApplicationUserId

		-- Show full details
		SELECT	a.*
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
		FROM	#TempMain		a
		LEFT JOIN	#HistortyInfoDetails	b ON	a.MSPAFileEventId=b.MSPAFileEventId
		ORDER BY	a.MSPAFileEventId
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
		ORDER BY	a.MSPAFileEventId
	END

	IF @AddAuditInfo = 1 
	BEGIN
		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert 
				@SystemEntityType	= @SystemEntityType
			,	@EntityKey			= @MSPAFileEventId
			,	@AuditAction		= 'Search'
			,	@CreatedDate		= @AuditDate
			,	@CreatedByPersonId	= @AuditId
	END

END
GO
