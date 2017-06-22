IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='FundXLegalEntitySearch') 
BEGIN
	DROP Procedure FundXLegalEntitySearch
END
GO

CREATE Procedure dbo.FundXLegalEntitySearch
(
		@FundXLegalEntityId				INT		= NULL
	,	@FundId				INT		= NULL
	,	@LegalEntityId				INT		= NULL
	,	@ApplicationId						INT	=	 NULL
	,	@AuditId						INT
	,	@AuditDate						DATETIME 					= NULL
	,	@SystemEntityType				VARCHAR(50)					= 'FundXLegalEntity'
	,	@ApplicationMode				INT							= NULL
	,	@AddAuditInfo					INT							= 1
	,	@AddTraceInfo					INT							= 0
	,	@ReturnAuditInfo				INT							= 0
)
WITH RECOMPILE
AS
BEGIN

	SET  NOCOUNT ON

	IF @AddTraceInfo = 1 
	BEGIN

		-- TRACE --
		DECLARE @InputParametersLocal	VARCHAR(500)  
		DECLARE @InputValuesLocal		VARCHAR(5000)  
		SET @InputParametersLocal		=  'FundXLegalEntityId' 
		SET @InputValuesLocal			=  CAST(@FundXLegalEntityId AS VARCHAR(50))

		EXEC dbo.StoredProcedureLogInsert
				@Name					= 'dbo.FundXLegalEntitySearch'
			,	@InputParameters		= @InputParametersLocal
			,	@InputValues			= @InputValuesLocal	
			-- TRACE --		

	END	

	DECLARE @SystemEntityTypeId AS INT
	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)


	SELECT 
			a. FundXLegalEntityId
		,	a. FundId
		,	a. LegalEntityId
		,	Fund.Name AS Fund
		,	LegalEntity.Name AS LegalEntity
		,	a. ApplicationId
	INTO		#TempMain
	FROM		dbo.FundXLegalEntity a
	INNER JOIN Fund ON Fund.FundId = a.FundId
	INNER JOIN LegalEntity ON LegalEntity.LegalEntityId = a.LegalEntityId
	WHERE	a.ApplicationId = ISNULL(@ApplicationId	, a.ApplicationId)	
	AND		a.FundXLegalEntityId =
			CASE
				WHEN @FundXLegalEntityId IS NULL THEN a.FundXLegalEntityId
				ELSE @FundXLegalEntityId
			END
	AND		a.FundId =
			CASE
				WHEN @FundId IS NULL THEN a.FundId
				ELSE @FundId
			END
	AND		a.LegalEntityId =
			CASE
				WHEN @LegalEntityId IS NULL THEN a.LegalEntityId
				ELSE @LegalEntityId
			END
	ORDER BY	a.FundXLegalEntityId ASC

	IF	@ApplicationMode = 1 
	BEGIN
		DELETE FROM #TempMain
		WHERE FundXLegalEntityId < 0
	END

	IF @ReturnAuditInfo = 1
	BEGIN

		-- get Audit latest record matching on key, systementitytype
		SELECT	c.EntityKey
			,	MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'
		INTO		#HistortyInfo
		FROM 		#TempMain a	
		INNER JOIN	CommonServices.dbo.AuditHistory c ON	c.EntityKey			= a.FundXLegalEntityId
		AND		c.SystemEntityId	= @SystemEntityTypeId
		AND		c.AuditActionId		IN (1,2)
		GROUP BY	c.EntityKey	

		-- Get Audit Date and CreatedByPersonId for given records
		SELECT	a.FundXLegalEntityId
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId
			, 	c.CreatedDate					AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName	AS	'UpdatedBy'
			,	d.Name							AS	'LastAction'
		INTO		#HistortyInfoDetails
		FROM		#TempMain a
		INNER JOIN	#HistortyInfo							b ON	b.EntityKey			= a.FundXLegalEntityId
		INNER JOIN	CommonServices.dbo.AuditHistory			c ON	c.AuditHistoryId	= b.MaxAuditHistoryId
		INNER JOIN	CommonServices.dbo.AuditAction			d ON	c.AuditActionId 	= d.AuditActionId
		INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e ON	c.CreatedByPersonId	= e.ApplicationUserId

		-- Show full details
		SELECT	a.*
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
		FROM	#TempMain		a
		LEFT JOIN	#HistortyInfoDetails	b ON	a.FundXLegalEntityId=b.FundXLegalEntityId
		ORDER BY	a.FundXLegalEntityId
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
		ORDER BY	a.FundXLegalEntityId
	END

	IF @AddAuditInfo = 1 
	BEGIN
		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert 
				@SystemEntityType	= @SystemEntityType
			,	@EntityKey			= @FundXLegalEntityId
			,	@AuditAction		= 'Search'
			,	@CreatedDate		= @AuditDate
			,	@CreatedByPersonId	= @AuditId
	END

END
GO
