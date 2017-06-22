IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='FieldConfigurationModeSearch')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationModeSearch'
	DROP Procedure FieldConfigurationModeSearch
END
GO

PRINT 'Creating Procedure FieldConfigurationModeSearch'
GO

/******************************************************************************
**		File: 
**		Name: FieldConfigurationModeSearch
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
			EXEC FieldConfigurationModeSearch NULL	, NULL	, NULL
			EXEC FieldConfigurationModeSearch NULL	, 'K'	, NULL
			EXEC FieldConfigurationModeSearch 1		, 'K'	, NULL
			EXEC FieldConfigurationModeSearch 1		, NULL	, NULL
			EXEC FieldConfigurationModeSearch NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE dbo.FieldConfigurationModeSearch
(
		@FieldConfigurationModeId		INT				= NULL 			
	,	@Name							VARCHAR(100)	= NULL 
	,	@Description					VARCHAR(500)	= NULL 
	,	@ApplicationId					INT				= NULL				
	,	@AuditId						INT								
	,	@AuditDate						DATETIME		= NULL			
	,	@SystemEntityType				VARCHAR(50)		= 'FieldConfigurationMode'		
	,	@ApplicationMode				INT				= NULL		
	,	@AddAuditInfo					INT				 = 1
	,	@AddTraceInfo					INT				 = 0
	,	@ReturnAuditInfo				INT				 = 0
)
WITH RECOMPILE
AS
BEGIN

	SET  NOCOUNT ON

	IF @AddTraceInfo = 1 
		BEGIN

			DECLARE @InputParametersLocal	VARCHAR(500)  
			DECLARE @InputValuesLocal		VARCHAR(5000)  
			SET @InputParametersLocal		= 'FieldConfigurationModeId' + ', ' + 'Name' 
			SET @InputValuesLocal			= CAST(@FieldConfigurationModeId AS VARCHAR(50)) + ', '+ @Name

			EXEC dbo.StoredProcedureLogInsert
					@Name						= 'dbo.FieldConfigurationModeSearch'	
				,	@InputParameters			= @InputParametersLocal
				,	@InputValues				= @InputValuesLocal		
	
		END	
			
	DECLARE @SystemEntityTypeId AS INT
	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)

	--if blank, then assume search on all possiblities ('%')
	IF  @Name IS NULL OR LEN(RTRIM(LTRIM(@Name))) = 0
	BEGIN
		SET	@NAME = '%'
	END

	--if blank, then assume search on all possiblities ('%')
	IF  @Description IS NULL OR LEN(RTRIM(LTRIM(@Description))) = 0
	BEGIN
		SET	@Description = '%'
	END

	SELECT		a.FieldConfigurationModeId
			,	a.ApplicationId		
			,	a.Name				
			,	a.Description			
			,	a.SortOrder	
	INTO		#TempMain	
	FROM	dbo.FieldConfigurationMode a
	WHERE		a.Name				LIKE @Name + '%'
	AND			a.Description		LIKE @Description + '%'
	AND			a.FieldConfigurationModeId	= ISNULL(@FieldConfigurationModeId, a.FieldConfigurationModeId)
	AND			a.ApplicationId				= ISNULL(@ApplicationId, a.ApplicationId)	
	ORDER BY a.SortOrder
		,	 a.Name		ASC
		,	 a.FieldConfigurationModeId	ASC

	IF @ReturnAuditInfo = 1
		BEGIN

			-- get Audit latest record matching on key, systementitytype
			SELECT		c.EntityKey			
				,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
			INTO		#HistortyInfo
			FROM 		#TempMain a		
			INNER JOIN	CommonServices.dbo.AuditHistory c		
						ON	c.EntityKey			= a.FieldConfigurationModeId
						AND c.SystemEntityId	= @SystemEntityTypeId
						AND c.AuditActionId		IN (1,2)
			GROUP BY	c.EntityKey	

			-- Get Audit Date and CreatedByPersonId for given records
			SELECT		
						a.FieldConfigurationModeId	
					,	c.AuditActionId 
					,	c.CreatedDate
					,	c.CreatedByPersonId	
					, 	c.CreatedDate						AS	'UpdatedDate'
					,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
					,	d.Name								AS	'LastAction'
			INTO		#HistortyInfoDetails
			FROM		#TempMain a
			INNER JOIN	#HistortyInfo										b
						ON	b.EntityKey			= a.FieldConfigurationModeId
			INNER JOIN	CommonServices.dbo.AuditHistory						c
						ON	c.AuditHistoryId	= b.MaxAuditHistoryId
			INNER JOIN	CommonServices.dbo.AuditAction						d	
						ON	c.AuditActionId 	= d.AuditActionId
			INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
						ON	c.CreatedByPersonId	= e.ApplicationUserId
	
			-- Show full details
			SELECT 		a.FieldConfigurationModeId	
					,	a.ApplicationId	
					,	a.Name			
					,	a.Description		
					,	a.SortOrder	
					,	b.UpdatedDate
					,	b.UpdatedBy
					,	b.LastAction
			FROM		#TempMain				a
			LEFT JOIN	#HistortyInfoDetails	b	
						ON	a.FieldConfigurationModeId	= b.FieldConfigurationModeId
			ORDER BY	a.SortOrder				ASC
					,	a.FieldConfigurationModeId
		END
	ELSE
		BEGIN
			SELECT 	a.*
				, 	UpdatedDate = '1/1/1900'
				,	UpdatedBy	= 'Unknown'
				,	LastAction	= 'Unknown'
			FROM	#TempMain a		
			ORDER BY	a.SortOrder				ASC
					,	a.FieldConfigurationModeId
		END

	IF @AddAuditInfo = 1 
		BEGIN

			-- Create Audit Record
			EXEC dbo.AuditHistoryInsert
					@SystemEntityType		= 'FieldConfigurationMode'
				,	@EntityKey				= @FieldConfigurationModeId
				,	@AuditAction			= 'Search'
				,	@CreatedDate			= @AuditDate
				,	@CreatedByPersonId		= @AuditId
		END

END
GO


