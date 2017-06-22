IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='UserPreferenceKeySearch')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceKeySearch'
	DROP Procedure UserPreferenceKeySearch
END
GO

PRINT 'Creating Procedure UserPreferenceKeySearch'
GO
/******************************************************************************
**		File: 
**		Name: UserPreferenceKeySearch
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
			EXEC UserPreferenceKeySearch NULL	, NULL	, NULL
			EXEC UserPreferenceKeySearch NULL	, 'K'	, NULL
			EXEC UserPreferenceKeySearch 1		, 'K'	, NULL
			EXEC UserPreferenceKeySearch 1		, NULL	, NULL
			EXEC UserPreferenceKeySearch NULL	, NULL	, 'W'

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
Create Procedure dbo.UserPreferenceKeySearch
(
		@UserPreferenceKeyId	INT			= NULL	
	,	@ApplicationId			INT			= NULL
	,	@Name					VARCHAR(50)	= NULL			
	,	@DataTypeId				INT			= NULL		
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL
	,	@SystemEntityType		VARCHAR(50) = 'UserPreferenceKey'
)	
AS
BEGIN

	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000)  
	SET @InputParametersLocal		= 'Name'
	SET @InputValuesLocal			= @Name
	EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.UserPreferenceKeySearch'
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal

	-- if the DataType did not provide any values
	-- assume search on all possiblities ('%')
	SET	@Name = ISNULL(@Name,'%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(LTRIM(RTRIM(@Name))) = 0 
	BEGIN
		SET	@Name = '%'
	END
	 
	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	
	
	SELECT		a.UserPreferenceKeyId
		,		a.ApplicationId					
		,		a.Name		
		,		a.Value				
		,		a.Description				
		,		a.SortOrder						
		,		a.DataTypeId				
		,		b.Name					AS	'DataType'
	INTO		#TempMain
	FROM		dbo.UserPreferenceKey a
	INNER JOIN	UserPreferenceDataType  b	ON	a.DataTypeId = b.UserPreferenceDataTypeId	
	WHERE		a.Name LIKE @Name	+ '%'
	AND a.DataTypeId	  = ISNULL(@DataTypeId, a.DataTypeId)
	AND a.UserPreferenceKeyID	= ISNULL(@UserPreferenceKeyID, a.UserPreferenceKeyID)
	AND a.ApplicationId	  = ISNULL(@ApplicationId, a.ApplicationId)
	ORDER BY a.SortOrder	ASC,
			 a.Name			ASC,
			 a.UserPreferenceKeyId	ASC

	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.UserPreferenceKeyId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.UserPreferenceKeyId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.UserPreferenceKeyId
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
				ON	a.UserPreferenceKeyId	= b.UserPreferenceKeyId
	ORDER BY	a.SortOrder				ASC
			,	a.UserPreferenceKeyId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserPreferenceKeyId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
