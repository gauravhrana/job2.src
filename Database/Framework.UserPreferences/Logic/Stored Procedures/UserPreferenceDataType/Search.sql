IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceDataTypeSearch')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceDataTypeSearch'
	DROP  Procedure  UserPreferenceDataTypeSearch
END
GO

PRINT 'Creating Procedure UserPreferenceDataTypeSearch'
GO

/******************************************************************************
**		File: 
**		Name: UserPreferenceDataTypeSearch
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
			EXEC UserPreferenceDataTypeSearch NULL	, NULL	, NULL
			EXEC UserPreferenceDataTypeSearch NULL	, 'K'	, NULL
			EXEC UserPreferenceDataTypeSearch 1		, 'K'	, NULL
			EXEC UserPreferenceDataTypeSearch 1		, NULL	, NULL
			EXEC UserPreferenceDataTypeSearch NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.UserPreferenceDataTypeSearch
(
		@UserPreferenceDataTypeId	INT				= NULL
	,	@ApplicationId				INT				= NULL
	,	@Name						VARCHAR(50)		= NULL			
	,	@AuditId					INT								
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'UserPreferenceDataType'		
)
AS
BEGIN
	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000)  
	SET @InputParametersLocal		= 'Name'
	SET @InputValuesLocal			= @Name
	EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.UserPreferenceDataTypeSearch'
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal

    
	-- if the UserPreferenceDataType did not provide any values
	-- assume search on all possiblities ('%')
	SET @Name	= ISNULL(@Name, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(LTRIM(RTRIM(@Name))) = 0 
	BEGIN
		SET	@Name = '%'
	END
	 
	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	

	SELECT	a.UserPreferenceDataTypeId	
		,	a.ApplicationId	
		,	a.Name				
		,	a.Description			
		,	a.SortOrder	
	INTO	#TempMain
	FROM	dbo.UserPreferenceDataType a	
	WHERE	a.Name LIKE @Name	+ '%'
	AND a.UserPreferenceDataTypeId	  = ISNULL(@UserPreferenceDataTypeId, a.UserPreferenceDataTypeId)
	AND a.ApplicationId	  = ISNULL(@ApplicationId, a.ApplicationId)
	ORDER BY a.SortOrder	ASC,
			 a.Name			ASC,
			 a.UserPreferenceDataTypeId	ASC

	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.UserPreferenceDataTypeId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.UserPreferenceDataTypeId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.UserPreferenceDataTypeId
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
				ON	a.UserPreferenceDataTypeId	= b.UserPreferenceDataTypeId
	ORDER BY	a.SortOrder				ASC
			,	a.UserPreferenceDataTypeId

	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
	      	@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserPreferenceDataTypeId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId 
	
	
	
END
GO
