IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='SetUpConfigurationSearch')
BEGIN
	PRINT 'Dropping Procedure SetUpConfigurationSearch'
	DROP Procedure SetUpConfigurationSearch
END
GO

PRINT 'Creating Procedure SetUpConfigurationSearch'
GO

/******************************************************************************
**		File: 
**		Name: SetUpConfigurationSearch
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
			EXEC SetUpConfigurationSearch NULL	, NULL	, NULL
			EXEC SetUpConfigurationSearch NULL	, 'K'	, NULL
			EXEC SetUpConfigurationSearch 1		, 'K'	, NULL
			EXEC SetUpConfigurationSearch 1		, NULL	, NULL
			EXEC SetUpConfigurationSearch NULL	, NULL	, 'W'

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
Create procedure dbo.SetUpConfigurationSearch
(
		@SetupConfigurationId		INT				= NULL 				
	,	@EntityName					VARCHAR(50)		= NULL
	,	@ApplicationId				INT				= NULL
	,	@ConnectionKeyName			VARCHAR(50)		= NULL
	,	@AuditId					INT				= NULL					
	,	@AuditDate					DATETIME		= NULL		
	,	@SystemEntityType			VARCHAR(50)		= 'SetUpConfiguration'			 
	,	@ApplicationMode			INT				= NULL		
	,	@AddAuditInfo				INT				= 1
	,	@AddTraceInfo				INT				= 0
	,	@ReturnAuditInfo			INT				= 0	
)
WITH RECOMPILE
AS
BEGIN

	SET  NOCOUNT ON

	-- if the client did not provide any values
	-- assume search on all possiblities ('%')
	SET @EntityName	= ISNULL(@EntityName, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@EntityName))) = 0
		BEGIN
			SET	@EntityName = '%'
		END

	SET @ConnectionKeyName	= ISNULL(@ConnectionKeyName, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@ConnectionKeyName))) = 0
		BEGIN
			SET	@ConnectionKeyName = '%'
		END

	-- Get Main System Entity Type ID
	DECLARE @SystemEntityId AS INT
	Select @SystemEntityId = dbo.GetSystemEntityTypeId(@SystemEntityType)

	SELECT	a.SetupConfigurationId
		,	a.EntityName
		,	a.ConnectionKeyName
		,	a.ApplicationId
		,	b.SystemEntityTypeId
	INTO		#TempMain
	FROM		dbo.SetUpConfiguration	a
	INNER JOIN	dbo.SystemEntityType	b	ON	a.EntityName	=	b.EntityName
	WHERE	a.EntityName		LIKE @EntityName		+ '%'
	AND		a.ConnectionKeyName LIKE @ConnectionKeyName	+ '%'
	AND		a.ApplicationId				= ISNULL(@ApplicationId, a.ApplicationId )
	AND		a.SetupConfigurationId		= ISNULL(@SetupConfigurationId, a.SetupConfigurationId )
	ORDER BY a.EntityName						ASC
		,	 a.SetupConfigurationId				ASC

	IF	@ApplicationMode = 1 
		BEGIN		
			DELETE FROM #TempMain
			WHERE SetupConfigurationId < 0
		END
			
	IF @ReturnAuditInfo = 1
		BEGIN

			-- get Audit latest record matching on key, systementitytype
			SELECT		c.EntityKey			
				,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
			INTO		#HistortyInfo
			FROM 		#TempMain a		
			INNER JOIN	CommonServices.dbo.AuditHistory c		
						ON	c.EntityKey			= a.SetupConfigurationId
						AND c.SystemEntityId	= @SystemEntityId
						AND c.AuditActionId		IN (1,2)
			GROUP BY	c.EntityKey	
	
			-- Get Audit Date and CreatedByPersonId for given records
			SELECT		a.SetupConfigurationId	
					,	c.AuditActionId 
					,	c.CreatedDate
					,	c.CreatedByPersonId	
					, 	c.CreatedDate						AS	'UpdatedDate'
					,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
					,	d.Name								AS	'LastAction'
			INTO		#HistortyInfoDetails
			FROM		#TempMain a
			INNER JOIN	#HistortyInfo										b
						ON	b.EntityKey			= a.SetupConfigurationId
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
						ON	a.SetupConfigurationId	= b.SetupConfigurationId
			ORDER BY	a.SetupConfigurationId	ASC

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
			ORDER BY	a.SetupConfigurationId

		END
	
	IF @AddAuditInfo = 1 
		BEGIN

			-- Create Audit Record
			EXEC dbo.AuditHistoryInsert
					@SystemEntityType		= @SystemEntityType
				,	@EntityKey				= NULL
				,	@AuditAction			= 'Search'
				,	@CreatedDate			= @AuditDate
				,	@CreatedByPersonId		= @AuditId	
	
		END

END
GO
