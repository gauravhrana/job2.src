IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CommissionSplitSearch') 
BEGIN
	DROP Procedure CommissionSplitSearch
END
GO

CREATE Procedure dbo.CommissionSplitSearch
(
		@CommissionSplitId				INT		= NULL
	,	@CommissionSplitCode				VARCHAR(500) = NULL
	,	@CommissionCodeId				INT		= NULL
	,	@ApplicationId						INT	=	 NULL
	,	@AuditId						INT
	,	@AuditDate						DATETIME 					= NULL
	,	@SystemEntityType				VARCHAR(50)					= 'CommissionSplit'
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
	IF  @CommissionSplitCode  IS NULL OR LEN(RTRIM(LTRIM(@CommissionSplitCode))) = 0
	BEGIN
		SET	@CommissionSplitCode = '%'
	END

	SELECT 
			a. CommissionSplitId
		,	a. CommissionSplitCode
		,	a. CommissionSplitDescription
		,	a. FullRate
		,	a. NoneCCA
		,	a. CCA
		,	a. StartDate
		,	a. EndDate
		,	a. LastModifiedBy
		,	a. LastModifiedOn
		,	a. CommissionCodeId
		,	CommissionCode.CommissionCodeCode AS CommissionCode
		,	a. ApplicationId
	INTO		#TempMain
	FROM		dbo.CommissionSplit a
	INNER JOIN CommissionCode ON CommissionCode.CommissionCodeId = a.CommissionCodeId
	WHERE	a.ApplicationId = ISNULL(@ApplicationId, a.ApplicationId)
	AND		a.CommissionSplitId = ISNULL(@CommissionSplitId, a.CommissionSplitId)
	AND		a.CommissionSplitCode	LIKE	@CommissionSplitCode + '%'
	AND		a.CommissionCodeId = ISNULL(@CommissionCodeId, a.CommissionCodeId)
	ORDER BY	a.CommissionSplitId ASC

	IF	@ApplicationMode = 1 
	BEGIN
		DELETE FROM #TempMain
		WHERE CommissionSplitId < 0
	END

	IF @ReturnAuditInfo = 1
	BEGIN

		-- get Audit latest record matching on key, systementitytype
		SELECT	c.EntityKey
			,	MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'
		INTO		#HistortyInfo
		FROM 		#TempMain a	
		INNER JOIN	CommonServices.dbo.AuditHistory c ON	c.EntityKey			= a.CommissionSplitId
		AND		c.SystemEntityId	= @SystemEntityTypeId
		AND		c.AuditActionId		IN (1,2)
		GROUP BY	c.EntityKey	

		-- Get Audit Date and CreatedByPersonId for given records
		SELECT	a.CommissionSplitId
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId
			, 	c.CreatedDate					AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName	AS	'UpdatedBy'
			,	d.Name							AS	'LastAction'
		INTO		#HistortyInfoDetails
		FROM		#TempMain a
		INNER JOIN	#HistortyInfo							b ON	b.EntityKey			= a.CommissionSplitId
		INNER JOIN	CommonServices.dbo.AuditHistory			c ON	c.AuditHistoryId	= b.MaxAuditHistoryId
		INNER JOIN	CommonServices.dbo.AuditAction			d ON	c.AuditActionId 	= d.AuditActionId
		INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e ON	c.CreatedByPersonId	= e.ApplicationUserId

		-- Show full details
		SELECT	a.*
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
		FROM	#TempMain		a
		LEFT JOIN	#HistortyInfoDetails	b ON	a.CommissionSplitId=b.CommissionSplitId
		ORDER BY	a.CommissionSplitId
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
		ORDER BY	a.CommissionSplitId
	END

	IF @AddAuditInfo = 1 
	BEGIN
		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert 
				@SystemEntityType	= @SystemEntityType
			,	@EntityKey			= @CommissionSplitId
			,	@AuditAction		= 'Search'
			,	@CreatedDate		= @AuditDate
			,	@CreatedByPersonId	= @AuditId
	END

END
GO
