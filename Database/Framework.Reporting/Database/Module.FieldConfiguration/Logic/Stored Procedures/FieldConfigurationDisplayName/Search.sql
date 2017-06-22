IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='FieldConfigurationDisplayNameSearch')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationDisplayNameSearch'
	DROP Procedure FieldConfigurationDisplayNameSearch
END
GO

PRINT 'Creating Procedure FieldConfigurationDisplayNameSearch'
GO
/******************************************************************************
**		File: 
**		Name: FieldConfigurationDisplayNameSearch
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
			EXEC FieldConfigurationDisplayNameSearch NULL	, NULL	, NULL
			EXEC FieldConfigurationDisplayNameSearch NULL	, 'K'	, NULL
			EXEC FieldConfigurationDisplayNameSearch 1		, 'K'	, NULL
			EXEC FieldConfigurationDisplayNameSearch 1		, NULL	, NULL
			EXEC FieldConfigurationDisplayNameSearch NULL	, NULL	, 'W'

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
Create Procedure dbo.FieldConfigurationDisplayNameSearch
(
		@FieldConfigurationDisplayNameId		INT			= NULL	
	,	@ApplicationId							INT			= NULL
	,	@Value									VARCHAR(50)	= NULL
	,	@LanguageId								INT			= NULL			
	,	@FieldConfigurationId					INT			= NULL
	,	@IsLanguage								INT			= NULL	
	,	@IsDefault								INT			= NULL	
	,	@AuditId								INT						
	,	@AuditDate								DATETIME	= NULL
	,	@SystemEntityType						VARCHAR(50) = 'FieldConfigurationDisplayName'	
	,	@ApplicationMode						INT			= NULL		
	,	@AddAuditInfo							INT			= 1
	,	@AddTraceInfo							INT			= 0
	,	@ReturnAuditInfo						INT			= 0
)	
WITH RECOMPILE
AS
BEGIN

	SET	NOCOUNT ON

	IF @AddTraceInfo = 1 
		BEGIN
		
			DECLARE @InputParametersLocal	VARCHAR(500)  
			DECLARE @InputValuesLocal		VARCHAR(5000)  
			SET @InputParametersLocal		= 'Value'
			SET @InputValuesLocal			= @Value
			EXEC dbo.StoredProcedureLogInsert
					@Name						= 'dbo.FieldConfigurationDisplayNameSearch'
				,	@InputParameters			= @InputParametersLocal
				,	@InputValues				= @InputValuesLocal
		
		END

	-- if the DataType did not provide any values
	-- assume search on all possiblities ('%')
	SET	@Value = ISNULL(@Value,'%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(LTRIM(RTRIM(@Value))) = 0 
	BEGIN
		SET	@Value = '%'
	END
	 
	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)	

	SELECT	a.FieldConfigurationDisplayNameId			
		,	a.ApplicationId	
		,	a.FieldConfigurationId		
		,	a.LanguageId			
		,	a.Value
		,	a.IsDefault	
		,	b.Name			AS 'FieldConfiguration'
		,	c.Name			AS 'Language'	
	FROM	dbo.FieldConfigurationDisplayName		a
	INNER JOIN dbo.FieldConfiguration	b 
		ON a.FieldConfigurationId = b.FieldConfigurationId 
	INNER JOIN dbo.Language			c
		ON a.LanguageId = c.LanguageId
	WHERE	a.Value						LIKE @Value	+ '%'
	AND		a.FieldConfigurationDisplayNameId			= ISNULL(@FieldConfigurationDisplayNameId, a.FieldConfigurationDisplayNameId)
	AND		a.ApplicationId				= ISNULL(@ApplicationId, a.ApplicationId)
	AND		a.LanguageId				= ISNULL(@LanguageId, a.LanguageId)
	AND		a.IsDefault					= ISNULL(@IsDefault, a.IsDefault)
	AND		ISNULL(a.FieldConfigurationId, -1)		= ISNULL(@FieldConfigurationId, ISNULL(a.FieldConfigurationId, -1))
	ORDER BY a.LanguageId	ASC,
			 a.Value			ASC,
			 a.FieldConfigurationDisplayNameId	ASC

	IF @ReturnAuditInfo = 1
		BEGIN
	
			-- get Audit latest record matching on key, systementitytype
			SELECT		c.EntityKey			
				,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
			INTO		#HistortyInfo
			FROM 		#TempMain a		
			INNER JOIN	CommonServices.dbo.AuditHistory c		
						ON	c.EntityKey			= a.FieldConfigurationDisplayNameId
						AND c.SystemEntityId	= @SystemEntityTypeId
						AND c.AuditActionId		IN (1,2)
			GROUP BY	c.EntityKey	
	
			-- Get Audit Date and CreatedByPersonId for given records
			SELECT		a.FieldConfigurationDisplayNameId	
					,	c.AuditActionId 
					,	c.CreatedDate
					,	c.CreatedByPersonId	
					, 	c.CreatedDate						AS	'UpdatedDate'
					,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
					,	d.Name								AS	'LastAction'
			INTO		#HistortyInfoDetails
			FROM		#TempMain a
			INNER JOIN	#HistortyInfo										b
						ON	b.EntityKey			= a.FieldConfigurationDisplayNameId
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
						ON	a.FieldConfigurationDisplayNameId	= b.FieldConfigurationDisplayNameId
			ORDER BY	a.LanguageId				ASC
					,	a.FieldConfigurationDisplayNameId
		
		END

	ELSE
		BEGIN

			SELECT 	a.*
				, 	UpdatedDate = '1/1/1900'
				,	UpdatedBy	= 'Unknown'
				,	LastAction	= 'Unknown'
			FROM	#TempMain a		
			ORDER BY	a.LanguageId				ASC
					,	a.FieldConfigurationDisplayNameId

		END

	IF @AddAuditInfo = 1 
		BEGIN

			--Create Audit Record
			EXEC dbo.AuditHistoryInsert
					@SystemEntityType		= @SystemEntityType
				,	@EntityKey				= @FieldConfigurationDisplayNameId
				,	@AuditAction			= 'Search'
				,	@CreatedDate			= @AuditDate
				,	@CreatedByPersonId		= @AuditId
		
		END

END
GO
