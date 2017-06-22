IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='SystemForeignRelationshipSearch')
BEGIN
	PRINT 'Dropping Procedure SystemForeignRelationshipSearch'
	DROP Procedure SystemForeignRelationshipSearch
END
GO

PRINT 'Creating Procedure SystemForeignRelationshipSearch'
GO

/******************************************************************************
**		File: 
**		Name: SystemForeignRelationshipSearch
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**
**		Sample:   
**              
			EXEC SystemForeignRelationshipSearch NULL	, NULL	, NULL
			EXEC SystemForeignRelationshipSearch NULL	, 'K'	, NULL
			EXEC SystemForeignRelationshipSearch 1		, 'K'	, NULL
			EXEC SystemForeignRelationshipSearch 1		, NULL	, NULL
			EXEC SystemForeignRelationshipSearch NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/
Create procedure SystemForeignRelationshipSearch
(
		@SystemForeignRelationshipId			INT				= NULL
	,	@ApplicationId							INT				= NULL 	
	,	@PrimaryDatabaseId						INT				= NULL 	
	,	@PrimaryEntityId						INT				= NULL 	
	,	@ForeignDatabaseId						INT				= NULL 	
	,	@ForeignEntityId						INT				= NULL 	
	,	@FieldName								VARCHAR(50)		= NULL 	
	,	@SystemForeignRelationshipTypeId		INT				= NULL 	
	,	@AuditId								INT								
	,	@AuditDate								DATETIME		= NULL			
	,	@SystemEntityType						VARCHAR(50)		= 'SystemForeignRelationship'
	,	@ApplicationMode					INT				= NULL		
	,	@AddAuditInfo						INT				 = 1
	,	@AddTraceInfo						INT				 = 0
	,	@ReturnAuditInfo					INT				 = 0	
)
WITH RECOMPILE
AS
BEGIN

	SET  NOCOUNT ON
	IF @AddTraceInfo = 1 
	BEGIN	
	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000)  

	SET @InputParametersLocal		= 'SystemForeignRelationshipId' + ', ' + 'PrimaryDatabaseId' + ', ' + 'ForeignDatabaseId' 
	SET @InputValuesLocal			= CAST(@SystemForeignRelationshipId AS VARCHAR(50)) + ', '+ CAST(@PrimaryDatabaseId AS VARCHAR(50)) + ', '+ CAST(@ForeignDatabaseId AS VARCHAR(50))

	EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.SystemForeignRelationshipSearch'
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal	
		--,	@ExecutedBy					= 'System'	
	END

	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	
	
	SELECT	a.*	
	,b.Name	 AS 'PrimaryDatabase'	
	INTO	#TempMain
	FROM	dbo.SystemForeignRelationship a	
	INNER JOIN	dbo.SystemForeignRelationshipDatabase		b	ON	a.SystemForeignRelationshipDatabaseId	=	b.SystemForeignRelationshipDatabaseId
	WHERE	a.PrimaryDatabaseId				= ISNULL(@PrimaryDatabaseId, a.PrimaryDatabaseId )
	AND 	a.ForeignDatabaseId				= ISNULL(@ForeignDatabaseId, a.ForeignDatabaseId )
	AND		a.SystemForeignRelationshipId	= ISNULL(@SystemForeignRelationshipId, a.SystemForeignRelationshipId)	
	ORDER BY a.SystemForeignRelationshipId	ASC	
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE SystemForeignRelationshipId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN		 
		
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.SystemForeignRelationshipId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.SystemForeignRelationshipId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.SystemForeignRelationshipId
	INNER JOIN	CommonServices.dbo.AuditHistory						c
				ON	c.AuditHistoryId	= b.MaxAuditHistoryId
	INNER JOIN	CommonServices.dbo.AuditAction						d	
				ON	c.AuditActionId 	= d.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
				ON	c.CreatedByPersonId	= e.ApplicationUserId		
	
	SELECT 	a.*			
		, 	b.UpdatedDate
		,	b.UpdatedBy
		,	b.LastAction
	FROM #TempMain a
	LEFT JOIN #HistortyInfoDetails	b	
				ON	a.SystemForeignRelationshipId	= b.SystemForeignRelationshipId
	ORDER BY	a.SortOrder				ASC
			,	a.SystemForeignRelationshipId
	END
ELSE
	BEGIN
		DECLARE @StaticUpdatedDate AS DATETIME
		SET @StaticUpdatedDate = Convert(datetime, '1/1/1900', 103)
	
		SELECT 	a.*
		   	,	UpdatedDate = @StaticUpdatedDate
			,	UpdatedBy	= 'Unknown'
			,	LastAction	= 'Unknown'
		FROM	#TempMain a		
		ORDER BY	a.SortOrder				ASC
				,	a.SystemForeignRelationshipId
	END
	IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SystemForeignRelationship'
		,	@EntityKey				= @SystemForeignRelationshipId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	END
END
GO
	

