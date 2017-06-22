IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='FieldConfigurationModeAccessModeSearch')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationModeAccessModeSearch'
	DROP Procedure FieldConfigurationModeAccessModeSearch
END
GO

PRINT 'Creating Procedure FieldConfigurationModeAccessModeSearch'
GO

/******************************************************************************
**		File: 
**		Name: FieldConfigurationModeAccessModeSearch
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
			EXEC FieldConfigurationModeAccessModeSearch NULL	, NULL	, NULL
			EXEC FieldConfigurationModeAccessModeSearch NULL	, 'K'	, NULL
			EXEC FieldConfigurationModeAccessModeSearch 1		, 'K'	, NULL
			EXEC FieldConfigurationModeAccessModeSearch 1		, NULL	, NULL
			EXEC FieldConfigurationModeAccessModeSearch NULL	, NULL	, 'W'

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
Create procedure dbo.FieldConfigurationModeAccessModeSearch
(
		@FieldConfigurationModeAccessModeId		INT				= NULL 			
	,	@Name									VARCHAR(100)	= NULL  
	,	@ApplicationId							INT				= NULL				
	,	@AuditId								INT								
	,	@AuditDate								DATETIME		= NULL			
	,	@SystemEntityType						VARCHAR(50)		= 'FieldConfigurationModeAccessMode'	
	,	@ApplicationMode						INT				= NULL		
	,	@AddAuditInfo							INT				= 1
	,	@AddTraceInfo							INT				= 0
	,	@ReturnAuditInfo						INT				= 0	
)
WITH RECOMPILE
AS
BEGIN

	SET	NOCOUNT ON

	IF @AddTraceInfo = 1 
		BEGIN
		
			DECLARE @InputParametersLocal	VARCHAR(500)  
			DECLARE @InputValuesLocal		VARCHAR(5000)  
			SET @InputParametersLocal		= 'FieldConfigurationModeAccessModeId' + ', ' + 'Name' 
			SET @InputValuesLocal			= CAST(@FieldConfigurationModeAccessModeId AS VARCHAR(50)) + ', '+ @Name
			EXEC dbo.StoredProcedureLogInsert
					@Name						= 'dbo.FieldConfigurationModeAccessModeSearch'	
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

	SELECT		a.FieldConfigurationModeAccessModeId
			,	a.ApplicationId		
			,	a.Name				
			,	a.Description			
			,	a.SortOrder	
	INTO		#TempMain	
	FROM	dbo.FieldConfigurationModeAccessMode a
	WHERE		a.Name				LIKE @Name + '%'
	AND			a.FieldConfigurationModeAccessModeId	= ISNULL(@FieldConfigurationModeAccessModeId, a.FieldConfigurationModeAccessModeId)
	AND			a.ApplicationId							= ISNULL(@ApplicationId, a.ApplicationId)	
	ORDER BY a.SortOrder
		,	 a.Name		ASC
		,	 a.FieldConfigurationModeAccessModeId	ASC


	IF @ReturnAuditInfo = 1
		BEGIN
	
			-- get Audit latest record matching on key, systementitytype
			SELECT		c.EntityKey			
				,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
			INTO		#HistortyInfo
			FROM 		#TempMain a		
			INNER JOIN	CommonServices.dbo.AuditHistory c		
						ON	c.EntityKey			= a.FieldConfigurationModeAccessModeId
						AND c.SystemEntityId	= @SystemEntityTypeId
						AND c.AuditActionId		IN (1,2)
			GROUP BY	c.EntityKey	

			-- Get Audit Date and CreatedByPersonId for given records
			SELECT		
						a.FieldConfigurationModeAccessModeId	
					,	c.AuditActionId 
					,	c.CreatedDate
					,	c.CreatedByPersonId	
					, 	c.CreatedDate						AS	'UpdatedDate'
					,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
					,	d.Name								AS	'LastAction'
			INTO		#HistortyInfoDetails
			FROM		#TempMain a
			INNER JOIN	#HistortyInfo										b
						ON	b.EntityKey			= a.FieldConfigurationModeAccessModeId
			INNER JOIN	CommonServices.dbo.AuditHistory						c
						ON	c.AuditHistoryId	= b.MaxAuditHistoryId
			INNER JOIN	CommonServices.dbo.AuditAction						d	
						ON	c.AuditActionId 	= d.AuditActionId
			INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
						ON	c.CreatedByPersonId	= e.ApplicationUserId

	
			-- Show full details
			SELECT 		a.FieldConfigurationModeAccessModeId	
					,	a.ApplicationId	
					,	a.Name			
					,	a.Description		
					,	a.SortOrder	
					,	b.UpdatedDate
					,	b.UpdatedBy
					,	b.LastAction
			FROM		#TempMain				a
			LEFT JOIN	#HistortyInfoDetails	b	
						ON	a.FieldConfigurationModeAccessModeId	= b.FieldConfigurationModeAccessModeId
			ORDER BY	a.SortOrder				ASC
					,	a.FieldConfigurationModeAccessModeId
		
		END

	ELSE
		BEGIN

			SELECT 	a.*
				, 	UpdatedDate = '1/1/1900'
				,	UpdatedBy	= 'Unknown'
				,	LastAction	= 'Unknown'
			FROM	#TempMain a		
			ORDER BY	a.SortOrder				ASC
					,	a.FieldConfigurationModeAccessModeId

		END

	IF @AddAuditInfo = 1 
		BEGIN

			-- Create Audit Record
			EXEC dbo.AuditHistoryInsert
					@SystemEntityType		= @SystemEntityType
				,	@EntityKey				= @FieldConfigurationModeAccessModeId
				,	@AuditAction			= 'Search'
				,	@CreatedDate			= @AuditDate
				,	@CreatedByPersonId		= @AuditId
		
		END

END
GO


