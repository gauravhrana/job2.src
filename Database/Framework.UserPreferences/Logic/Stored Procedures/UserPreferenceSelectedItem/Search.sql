IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='UserPreferenceSelectedItemSearch')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceSelectedItemSearch'
	DROP  Procedure  UserPreferenceSelectedItemSearch
END
GO

PRINT 'Creating Procedure UserPreferenceSelectedItemSearch'
GO

/******************************************************************************
**		File: 
**		ApplicationUserId: UserPreferenceSelectedItemSearch
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
			EXEC UserPreferenceSelectedItemSearch NULL	, NULL	, NULL
			EXEC UserPreferenceSelectedItemSearch NULL	, 'K'	, NULL
			EXEC UserPreferenceSelectedItemSearch 1		, 'K'	, NULL
			EXEC UserPreferenceSelectedItemSearch 1		, NULL	, NULL
			EXEC UserPreferenceSelectedItemSearch NULL	, NULL	, 'W'

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
Create procedure dbo.UserPreferenceSelectedItemSearch
(
		@UserPreferenceSelectedItemId			INT				= NULL 		
	,	@ApplicationUserId						INT				= NULL 		
	,	@UserPreferenceKeyId					INT				= NULL		
	,	@ApplicationId							INT				= NULL		
	,	@ParentKey								VARCHAR(50)		= NULL
	,	@AuditId								INT							
	,	@AuditDate								DATETIME		= NULL	
	,	@SystemEntityType						VARCHAR(50)		= 'UserPreferenceSelectedItem'	
)
AS
BEGIN

	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000)  
	SET @InputParametersLocal		= 'ApplicationUserId'
	SET @InputValuesLocal			= CAST(@ApplicationUserId AS VARCHAR(50))
	EXEC dbo.StoredProcedureLogInsert
			@Name		= 'dbo.UserPreferenceSelectedItemSearch'	
		,	@InputParameters = @InputParametersLocal
		,	@InputValues	 = @InputValuesLocal	

 --   DECLARE @StoredProcedureLogDetailId INT
	--EXEC dbo.StoredProcedureLogDetailInsert
	--		@StoredProcedureLogDetailId		= @StoredProcedureLogDetailId OUTPUT
	--	,	@StoredProcedureLogId		= @StoredProcedureLogId
	--	,	@ParameterName  = 'ApplicationUserId'
	--	,   @ParameterValue = @ApplicationUserId

	-- if the ParentKey did not provide any values
	-- assume search on all possiblities ('%')
	SET	@ParentKey = ISNULL(@ParentKey,'%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(LTRIM(RTRIM(@ParentKey))) = 0 
	BEGIN
		SET	@ParentKey = '%'
	END

	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)	

	SELECT	a.UserPreferenceSelectedItemId
		,	a.ApplicationId				
		,	a.ApplicationUserId			
		,	a.UserPreferenceKeyId			
		,	a.ParentKey					
		,	a.Value						
		,	a.SortOrder
		,	b.Name								AS	'UserPreferenceKey'
	INTO	#TempMain
	FROM		dbo.UserPreferenceSelectedItem						a
	INNER JOIN	dbo.UserPreferenceKey								b ON a.UserPreferenceKeyId		= b.UserPreferenceKeyId
	WHERE	a.ApplicationUserId			=	ISNULL(@ApplicationUserId, a.ApplicationUserId)
	AND		a.UserPreferenceKeyId		=	ISNULL(@UserPreferenceKeyId, a.UserPreferenceKeyId)
	AND		a.ApplicationId				=	ISNULL(@ApplicationId, a.ApplicationId)
	AND		a.ApplicationId				=	ISNULL(@ApplicationId, a.ApplicationId)
	AND		a.ParentKey LIKE @ParentKey	+ '%'
	AND		a.UserPreferenceSelectedItemId			=
			CASE 		
				WHEN @UserPreferenceSelectedItemId IS NULL THEN a.UserPreferenceSelectedItemId
				ELSE @UserPreferenceSelectedItemId
			END
	ORDER BY	a.SortOrder	ASC 
		,	a.UserPreferenceSelectedItemId	ASC
	
	

	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.UserPreferenceSelectedItemId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.UserPreferenceSelectedItemId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.UserPreferenceSelectedItemId
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
				ON	a.UserPreferenceSelectedItemId	= b.UserPreferenceSelectedItemId
	ORDER BY	a.UserPreferenceSelectedItemId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserPreferenceSelectedItemId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO
