IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationSearch')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationSearch'
	DROP  Procedure FieldConfigurationSearch
END
GO

PRINT 'Creating Procedure FieldConfigurationSearch'
GO
/******************************************************************************
**		File: 
**		Name: FieldConfigurationSearch
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
			EXEC FieldConfigurationSearch NULL	, NULL	, NULL
			EXEC FieldConfigurationSearch NULL	, 'K'	, NULL
			EXEC FieldConfigurationSearch 1	, 'K'	, NULL
			EXEC FieldConfigurationSearch 1	, NULL	, NULL
			EXEC FieldConfigurationSearch NULL	, NULL	, 'W'

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

CREATE PROCEDURE dbo.FieldConfigurationSearch
(
		@FieldConfigurationId			INT				= NULL 				
	,	@Name							VARCHAR(50)		= NULL 
	,	@SystemEntityTypeId				INT				= NULL				
	,	@Value					        VARCHAR(50)		= NULL 				
	,	@Width							NUMERIC(7,2)	= NULL
	,	@Formatting					    VARCHAR(50)		= NULL 				
	,	@ControlType                    VARCHAR(50)		= NULL 
	,	@HorizontalAlignment			VARCHAR(50)		= NULL
	,	@FieldConfigurationModeId		INT				= NULL	
	,	@DisplayColumn					INT				= NULL	
	,	@CellCount						INT				= NULL		
	,	@ApplicationId					INT				= NULL
	,	@AuditId						INT				= NULL
	,	@AuditDate						DATETIME		= NULL
	,	@SystemEntityType				VARCHAR(50)		= 'FieldConfiguration'
	,	@ApplicationMode				INT				= NULL	
	,	@AddAuditInfo					INT				 = 1
	,	@AddTraceInfo					INT				 = 0
	,	@ReturnAuditInfo				INT				 = 0	
)
WITH RECOMPILE
AS
BEGIN

	SET NOCOUNT ON;

	IF @AddTraceInfo = 1
		BEGIN

			DECLARE @InputParametersLocal	VARCHAR(500)  
			DECLARE @InputValuesLocal		VARCHAR(5000)  
			SET @InputParametersLocal		= 'Name' 
			SET @InputValuesLocal			= @Name
			EXEC TaskTimeTracker.dbo.StoredProcedureLogInsert
					@Name						= 'dbo.ApplicationEntityLabelSearch'
				,	@InputParameters			= @InputParametersLocal
				,	@InputValues				= @InputValuesLocal	

		END 

	-- Get Main System Entity Type ID
	DECLARE @MainSystemEntityTypeId AS INT
	SELECT @MainSystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	
	-- if the client did not provide any values
	-- assume search on all possiblities ('%')
	SET @Name = ISNULL(@Name, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Name))) = 0
	BEGIN
		SET	@NAME = '%'
	END
	
	SELECT	a.FieldConfigurationId		    
		,	a.Name									
		,	a.Value									
		,	a.SystemEntityTypeId					
		,	a.Width									
		,	a.Formatting							
		,	a.ControlType
		,	a.HorizontalAlignment
		,	a.ApplicationId	
		,	a.GridViewPriority
		,	a.DetailsViewPriority
		,	a.FieldConfigurationModeId		
		,	a.DisplayColumn
		,	a.CellCount
		,	b.Value			AS 'FieldConfigurationDisplayName'
		,	c.Name			AS 'FieldConfigurationMode'	
		,	d.EntityName	AS	'SystemEntityType'
	INTO		#TempMain
	FROM		dbo.FieldConfiguration a
	LEFT JOIN	dbo.FieldConfigurationDisplayName	b ON	a.FieldConfigurationId		= b.FieldConfigurationId
	INNER JOIN	dbo.FieldConfigurationMode			c ON	a.FieldConfigurationModeId	= c.FieldConfigurationModeId
	INNER JOIN	dbo.SystemEntityType				d ON	a.SystemEntityTypeId		= d.SystemEntityTypeId
	WHERE		a.Name LIKE @Name	--+ '%'
	AND			a.ApplicationId				= ISNULL(@ApplicationId				, a.ApplicationId)
	AND			a.DisplayColumn				= ISNULL(@DisplayColumn				, a.DisplayColumn)
	AND			a.CellCount					= ISNULL(@CellCount					, a.CellCount)
	AND			b.FieldConfigurationId		= ISNULL(@FieldConfigurationId		, b.FieldConfigurationId)
	AND			b.IsDefault					= 1
	AND			c.FieldConfigurationModeId	= ISNULL(@FieldConfigurationModeId	, c.FieldConfigurationModeId)
	AND			d.SystemEntityTypeId		= ISNULL(@SystemEntityTypeId		, d.SystemEntityTypeId)		
	ORDER BY	a.Value					ASC
			,	a.Name					ASC
			,	a.FieldConfigurationId	ASC
								
	IF @ReturnAuditInfo = 1
		BEGIN
		
			-- get Audit latest record matching on key, systementitytype
			SELECT		c.EntityKey			
				,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
			INTO		#HistortyInfo
			FROM 		#TempMain a		
			INNER JOIN	CommonServices.dbo.AuditHistory c		
						ON	c.EntityKey			= a.FieldConfigurationId
						AND c.SystemEntityId	= @MainSystemEntityTypeId
						AND c.AuditActionId		IN (1,2)
			GROUP BY	c.EntityKey		

			-- Get Audit Date and CreatedByPersonId for given records
			SELECT		a.FieldConfigurationId	
					,	c.AuditActionId 
					,	c.CreatedDate
					,	c.CreatedByPersonId	
					, 	c.CreatedDate						AS	'UpdatedDate'
					,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
					,	d.Name								AS	'LastAction'
			INTO		#HistortyInfoDetails
			FROM		#TempMain a
			INNER JOIN	#HistortyInfo										b
						ON	b.EntityKey			= a.FieldConfigurationId
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
						ON	a.FieldConfigurationId	= b.FieldConfigurationId
			ORDER BY	a.FieldConfigurationId

		END
	ELSE
		BEGIN
				
			SELECT 	a.*
				, 	UpdatedDate = '1/1/1900'
				,	UpdatedBy	= 'Unknown'
				,	LastAction	= 'Unknown'
			FROM #TempMain a		
			ORDER BY	a.FieldConfigurationId

		END

	IF @AddAuditInfo = 1 
		BEGIN

			-- Create Audit Record
			EXEC dbo.AuditHistoryInsert
					@SystemEntityType		= @SystemEntityType
				,	@EntityKey				= @FieldConfigurationId
				,	@AuditAction			= 'Search'
				,	@CreatedDate			= @AuditDate
				,	@CreatedByPersonId		= @AuditId
		
		END
	
	

END

GO

