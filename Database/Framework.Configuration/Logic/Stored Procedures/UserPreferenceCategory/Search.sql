IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='UserPreferenceCategorySearch')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceCategorySearch'
	DROP Procedure UserPreferenceCategorySearch
END
GO

PRINT 'Creating Procedure UserPreferenceCategorySearch'
GO

/******************************************************************************
**		Task: 
**		Name: UserPreferenceCategorySearch
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
			EXEC UserPreferenceCategorySearch NULL	, NULL	, NULL
			EXEC UserPreferenceCategorySearch NULL	, 'K'	, NULL
			EXEC UserPreferenceCategorySearch 1		, 'K'	, NULL
			EXEC UserPreferenceCategorySearch 1		, NULL	, NULL
			EXEC UserPreferenceCategorySearch NULL	, NULL	, 'W'

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
CREATE PROCEDURE dbo.UserPreferenceCategorySearch
(
		@UserPreferenceCategoryId		INT				= NULL
	,	@ApplicationId					INT		 		= NULL	
	,	@Name							VARCHAR(100)	= NULL 			
	,	@AuditId						INT								
	,	@AuditDate						DATETIME		= NULL
	,	@SystemEntityType				VARCHAR(50)		= 'UserPreferenceCategory'	
	,	@ApplicationMode				INT				= NULL		
	,	@AddAuditInfo					INT				= 1
	,	@AddTraceInfo					INT				= 0
	,	@ReturnAuditInfo				INT				= 0
)
WITH RECOMPILE
AS
BEGIN

	SET NOCOUNT ON

	IF @AddTraceInfo = 1 
		BEGIN
		
			DECLARE @InputParametersLocal	VARCHAR(500)  
			DECLARE @InputValuesLocal		VARCHAR(5000)  
			SET @InputParametersLocal		= 'Name'
			SET @InputValuesLocal			= @Name

			EXEC dbo.StoredProcedureLogInsert
					@Name			 = 'dbo.UserPreferenceCategorySearch'
				,	@InputParameters = @InputParametersLocal
				,	@InputValues	 = @InputValuesLocal	

				--   DECLARE @StoredProcedureLogDetailId INT
			--EXEC dbo.StoredProcedureLogDetailInsert
			--		@StoredProcedureLogDetailId		= @StoredProcedureLogDetailId OUTPUT
			--	,	@StoredProcedureLogId		= @StoredProcedureLogId
			--	,	@ParameterName  = 'Name'
			--	,   @ParameterValue = @Name
			-- if the client did not provide any values
		END

	-- assume search on all possiblities ('%')
	SET @Name	= ISNULL(@Name, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Name))) = 0
	BEGIN
		SET	@NAME = '%'
	END

	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)

	SELECT	a.UserPreferenceCategoryId
		,	a.ApplicationId		
		,	a.Name				
		,	a.Description			
		,	a.SortOrder	
	INTO	#TempMain
	FROM	dbo.UserPreferenceCategory a	
	WHERE	a.Name LIKE @Name	--+ '%'
	AND		a.ApplicationId					= ISNULL(@ApplicationId, a.ApplicationId)
	AND		a.UserPreferenceCategoryId		= ISNULL(@UserPreferenceCategoryId, a.UserPreferenceCategoryId)
	ORDER BY a.SortOrder	ASC,
			 a.Name			ASC,
			 a.UserPreferenceCategoryId	ASC

	IF @ReturnAuditInfo = 1
		BEGIN
			-- get Audit latest record matching on key, systementitytype
			SELECT		c.EntityKey			
				,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
			INTO		#HistortyInfo
			FROM 		#TempMain a		
			INNER JOIN	CommonServices.dbo.AuditHistory c		
						ON	c.EntityKey			= a.UserPreferenceCategoryId
						AND c.SystemEntityId	= @SystemEntityTypeId
						AND c.AuditActionId		IN (1,2)
			GROUP BY	c.EntityKey	
	
			-- Get Audit Date and CreatedByPersonId for given records
			SELECT		a.UserPreferenceCategoryId	
					,	c.AuditActionId 
					,	c.CreatedDate
					,	c.CreatedByPersonId	
					, 	c.CreatedDate						AS	'UpdatedDate'
					,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
					,	d.Name								AS	'LastAction'
			INTO		#HistortyInfoDetails
			FROM		#TempMain a
			INNER JOIN	#HistortyInfo										b
						ON	b.EntityKey			= a.UserPreferenceCategoryId
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
						ON	a.UserPreferenceCategoryId	= b.UserPreferenceCategoryId
			ORDER BY	a.SortOrder				ASC
					,	a.UserPreferenceCategoryId

		END
	ELSE
		BEGIN
			
			SELECT 	a.*
				, 	UpdatedDate = '1/1/1900'
				,	UpdatedBy	= 'Unknown'
				,	LastAction	= 'Unknown'
			FROM	#TempMain a		
			ORDER BY	a.SortOrder				ASC
					,	a.UserPreferenceCategoryId
		END

	IF @AddAuditInfo = 1 
		BEGIN
			-- Create Audit Record
			EXEC dbo.AuditHistoryInsert
					@SystemEntityType		= @SystemEntityType
				,	@EntityKey				= @UserPreferenceCategoryId
				,	@AuditAction			= 'Search'
				,	@CreatedDate			= @AuditDate
				,	@CreatedByPersonId		= @AuditId
		END

END
GO
	

