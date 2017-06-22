IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='UserPreferenceSearch')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceSearch'
	DROP  Procedure  UserPreferenceSearch
END
GO

PRINT 'Creating Procedure UserPreferenceSearch'
GO

/******************************************************************************
**		File: 
**		ApplicationUserId: UserPreferenceSearch
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
			EXEC UserPreferenceSearch NULL	, NULL	, NULL
			EXEC UserPreferenceSearch NULL	, 'K'	, NULL
			EXEC UserPreferenceSearch 1		, 'K'	, NULL
			EXEC UserPreferenceSearch 1		, NULL	, NULL
			EXEC UserPreferenceSearch NULL	, NULL	, 'W'

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
CREATE PROCEDURE dbo.UserPreferenceSearch
(
		@UserPreferenceId			INT				= NULL 		
	,	@ApplicationUserId			INT				= NULL 		
	,	@UserPreferenceKeyId		INT				= NULL		
	,	@UserPreferenceKey			VARCHAR(50)		= NULL
	,	@Value						VARCHAR(50)		= NULL	
	,	@UserPreferenceCategoryId	INT				= NULL		
	,	@DataTypeId					INT				= NULL		
	,	@ApplicationId				INT				= NULL		
	,	@AuditId					INT							
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'UserPreference'	
	,	@ApplicationMode			INT				= NULL		
	,	@AddAuditInfo				INT				= 1
	,	@AddTraceInfo				INT				= 0
	,	@ReturnAuditInfo			INT				= 0
)
WITH RECOMPILE
AS
BEGIN

	SET NOCOUNT ON;

	IF @AddTraceInfo = 1 
		BEGIN

			DECLARE @InputParametersLocal	VARCHAR(500)  
			DECLARE @InputValuesLocal		VARCHAR(5000)  
			SET @InputParametersLocal		= 'ApplicationUserId'
			SET @InputValuesLocal			= CAST(@ApplicationUserId AS VARCHAR(50))
			EXEC dbo.StoredProcedureLogInsert
					@Name		= 'dbo.UserPreferenceSearch'	
				,	@InputParameters = @InputParametersLocal
				,	@InputValues	 = @InputValuesLocal	

		 --   DECLARE @StoredProcedureLogDetailId INT
			--EXEC dbo.StoredProcedureLogDetailInsert
			--		@StoredProcedureLogDetailId		= @StoredProcedureLogDetailId OUTPUT
			--	,	@StoredProcedureLogId		= @StoredProcedureLogId
			--	,	@ParameterName  = 'ApplicationUserId'
			--	,   @ParameterValue = @ApplicationUserId

		END
	
	SET	@UserPreferenceKey = ISNULL(@UserPreferenceKey,'%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(LTRIM(RTRIM(@UserPreferenceKey))) = 0 
	BEGIN
		SET	@UserPreferenceKey = '%'
	END

	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)	

	SELECT	a.UserPreferenceId												
		,	a.Value																
		,	a.DataTypeId						
		,	d.Name								AS 'UserPreferenceDataType'											
		,	a.ApplicationUserId															
		,	b.FirstName + ' ' + b.LastName		AS 'ApplicationUser'												
		,	a.ApplicationId	 
		,	e.Name								AS 'Application'															
		,	a.UserPreferenceKeyId			
		,	c.Name								AS 'UserPreferenceKey'
		,	a.UserPreferenceCategoryId
		,	f.Name								AS 'UserPreferenceCategory'
	INTO		#TempMain
	FROM		dbo.UserPreference a
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	b ON a.ApplicationUserId		= b.ApplicationUserId
	INNER JOIN	dbo.UserPreferenceKey								c ON a.UserPreferenceKeyId		= c.UserPreferenceKeyId
	INNER JOIN	dbo.UserPreferenceDataType							d ON a.DataTypeId				= d.UserPreferenceDataTypeId
	INNER JOIN	AuthenticationAndAuthorization.dbo.Application		e ON a.ApplicationId			= e.ApplicationId
	INNER JOIN	dbo.UserPreferenceCategory							f ON a.UserPreferenceCategoryId = f.UserPreferenceCategoryId
	WHERE	a.ApplicationUserId			=	ISNULL(@ApplicationUserId			, a.ApplicationUserId)
	AND		a.UserPreferenceId			=	ISNULL(@UserPreferenceId			, a.UserPreferenceId)				
	AND		a.Value						=	ISNULL(@Value						, a.Value)	
	AND		c.UserPreferenceKeyId		=	ISNULL(@UserPreferenceKeyId			, c.UserPreferenceKeyId)
	AND		d.UserPreferenceDataTypeId	=	ISNULL(@DataTypeId					, d.UserPreferenceDataTypeId)
	AND		e.ApplicationId				=	ISNULL(@ApplicationId				, e.ApplicationId)	
	AND		f.UserPreferenceCategoryId	=	ISNULL(@UserPreferenceCategoryId	, f.UserPreferenceCategoryId)
	AND		c.Name						LIKE	@UserPreferenceKey + '%'	
	ORDER BY	a.ApplicationId		ASC 
			,	a.UserPreferenceId	ASC
	
	IF @ReturnAuditInfo = 1
		BEGIN
			-- get Audit latest record matching on key, systementitytype
			SELECT		c.EntityKey			
				,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
			INTO		#HistortyInfo
			FROM 		#TempMain a		
			INNER JOIN	CommonServices.dbo.AuditHistory c		
						ON	c.EntityKey			= a.UserPreferenceId
						AND c.SystemEntityId	= @SystemEntityTypeId
						AND c.AuditActionId		IN (1,2)
			GROUP BY	c.EntityKey	
	
			-- Get Audit Date and CreatedByPersonId for given records
			SELECT		a.UserPreferenceId	
					,	c.AuditActionId 
					,	c.CreatedDate
					,	c.CreatedByPersonId	
					, 	c.CreatedDate						AS	'UpdatedDate'
					,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
					,	d.Name								AS	'LastAction'
			INTO		#HistortyInfoDetails
			FROM		#TempMain a
			INNER JOIN	#HistortyInfo										b
						ON	b.EntityKey			= a.UserPreferenceId
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
						ON	a.UserPreferenceId	= b.UserPreferenceId
			ORDER BY	a.UserPreferenceId

		END
	ELSE
		BEGIN
			
			SELECT 	a.*
				, 	UpdatedDate = '1/1/1900'
				,	UpdatedBy	= 'Unknown'
				,	LastAction	= 'Unknown'
			FROM #TempMain a		
			ORDER BY	a.UserPreferenceId

		END

	IF @AddAuditInfo = 1 
		BEGIN

			--Create Audit Record
			EXEC dbo.AuditHistoryInsert
					@SystemEntityType		= @SystemEntityType
				,	@EntityKey				= @UserPreferenceId
				,	@AuditAction			= 'Search'
				,	@CreatedDate			= @AuditDate
				,	@CreatedByPersonId		= @AuditId
		
		END


END
GO
