--IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='FieldConfigurationCategorySearch')
--BEGIN
--	PRINT 'Dropping Procedure FieldConfigurationCategorySearch'
--	DROP Procedure FieldConfigurationCategorySearch
--END
--GO

--PRINT 'Creating Procedure FieldConfigurationCategorySearch'
--GO

--/******************************************************************************
--**		File: 
--**		Name: FieldConfigurationCategorySearch
--**		Desc: 
--**
--**		This template can be customized:
--**              
--**		Return values:
--** 
--**		Called by:   
--**
--**		Sample:   
--**              
--			EXEC FieldConfigurationCategorySearch NULL	, NULL	, NULL
--			EXEC FieldConfigurationCategorySearch NULL	, 'K'	, NULL
--			EXEC FieldConfigurationCategorySearch 1		, 'K'	, NULL
--			EXEC FieldConfigurationCategorySearch 1		, NULL	, NULL
--			EXEC FieldConfigurationCategorySearch NULL	, NULL	, 'W'

--**		Parameters:
--**		Input							Output
--**      ----------						-----------
--**
--**		Date: 
--*******************************************************************************
--**		Change History
--*******************************************************************************
--**		Date:		Author:				Description:
--**		--------	--------		-------------------------------------------
--**    
--*******************************************************************************/
--CREATE PROCEDURE dbo.FieldConfigurationCategorySearch
--(
--		@FieldConfigurationCategoryId	INT				= NULL 			
--	,	@Name				VARCHAR(100)		= NULL 
--	,	@Description		VARCHAR(500)	= NULL 
--	,	@ApplicationId		INT				= NULL				
--	,	@AuditId			INT								
--	,	@AuditDate			DATETIME		= NULL			
--	,	@SystemEntityType	VARCHAR(50)		= 'FieldConfigurationCategory'	
--	,	@AddAuditInfo			INT				 = 1
--	,	@AddTraceInfo			INT				 = 0
--	,	@ReturnAuditInfo		INT				 = 0	
--)
--WITH RECOMPILE
--AS
--BEGIN

--	SET  NOCOUNT ON

--	IF @AddTraceInfo = 1 
--	BEGIN

--		DECLARE @InputParametersLocal	VARCHAR(500)  
--		DECLARE @InputValuesLocal		VARCHAR(5000)  
--		SET @InputParametersLocal		= 'FieldConfigurationCategoryId' + ', ' + 'Name' 
--		SET @InputValuesLocal			= CAST(@FieldConfigurationCategoryId AS VARCHAR(50)) + ', '+ @Name

--		EXEC dbo.StoredProcedureLogInsert
--				@Name						= 'dbo.FieldConfigurationCategorySearch'	
--			,	@InputParameters			= @InputParametersLocal
--			,	@InputValues				= @InputValuesLocal		
	
--	END	
			
--	DECLARE @SystemEntityTypeId AS INT
--	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)

--	--if blank, then assume search on all possiblities ('%')
--	IF  @Name IS NULL OR LEN(RTRIM(LTRIM(@Name))) = 0
--	BEGIN
--		SET	@NAME = '%'
--	END

--	--if blank, then assume search on all possiblities ('%')
--	IF  @Description IS NULL OR LEN(RTRIM(LTRIM(@Description))) = 0
--	BEGIN
--		SET	@Description = '%'
--	END

--	SELECT		a.FieldConfigurationCategoryId
--			,	a.ApplicationId		
--			,	a.Name				
--			,	a.Description			
--			,	a.SortOrder	
--	INTO		#TempMain	
--	FROM	dbo.FieldConfigurationCategory a
--	WHERE		a.Name				LIKE @Name + '%'
--	AND			a.Description		LIKE @Description + '%'
--	AND			a.FieldConfigurationCategoryId			= ISNULL(@FieldConfigurationCategoryId, a.FieldConfigurationCategoryId)
--	AND			a.ApplicationId		= ISNULL(@ApplicationId, a.ApplicationId)	
--	ORDER BY a.SortOrder
--		,	 a.Name		ASC
--		,	 a.FieldConfigurationCategoryId	ASC

--	IF @ReturnAuditInfo = 1
--	BEGIN

--		-- get Audit latest record matching on key, systementitytype
--		SELECT		c.EntityKey			
--			,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
--		INTO		#HistortyInfo
--		FROM 		#TempMain a		
--		INNER JOIN	CommonServices.dbo.AuditHistory c		
--					ON	c.EntityKey			= a.FieldConfigurationCategoryId
--					AND c.SystemEntityId	= @SystemEntityTypeId
--					AND c.AuditActionId		IN (1,2)
--		GROUP BY	c.EntityKey	

--		-- Get Audit Date and CreatedByPersonId for given records
--		SELECT		
--					a.FieldConfigurationCategoryId	
--				,	c.AuditActionId 
--				,	c.CreatedDate
--				,	c.CreatedByPersonId	
--				, 	c.CreatedDate						AS	'UpdatedDate'
--				,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
--				,	d.Name								AS	'LastAction'
--		INTO		#HistortyInfoDetails
--		FROM		#TempMain a
--		INNER JOIN	#HistortyInfo										b
--					ON	b.EntityKey			= a.FieldConfigurationCategoryId
--		INNER JOIN	CommonServices.dbo.AuditHistory						c
--					ON	c.AuditHistoryId	= b.MaxAuditHistoryId
--		INNER JOIN	CommonServices.dbo.AuditAction						d	
--					ON	c.AuditActionId 	= d.AuditActionId
--		INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
--					ON	c.CreatedByPersonId	= e.ApplicationUserId
	
--		-- Show full details
--		SELECT 		a.FieldConfigurationCategoryId	
--				,	a.ApplicationId	
--				,	a.Name			
--				,	a.Description		
--				,	a.SortOrder	
--				,	b.UpdatedDate
--				,	b.UpdatedBy
--				,	b.LastAction
--		FROM		#TempMain				a
--		LEFT JOIN	#HistortyInfoDetails	b	
--					ON	a.FieldConfigurationCategoryId	= b.FieldConfigurationCategoryId
--		ORDER BY	a.SortOrder				ASC
--				,	a.FieldConfigurationCategoryId
--	END
--	ELSE
--	BEGIN
--		SELECT 	a.*
--			, 	UpdatedDate = '1/1/1900'
--			,	UpdatedBy	= 'Unknown'
--			,	LastAction	= 'Unknown'
--		FROM	#TempMain a		
--		ORDER BY	a.SortOrder				ASC
--				,	a.ClientId
--	END

--	IF @AddAuditInfo = 1 
--	BEGIN
--		-- Create Audit Record
--		EXEC dbo.AuditHistoryInsert
--				@SystemEntityType		= 'FieldConfigurationCategory'
--			,	@EntityKey				= @FieldConfigurationCategoryId
--			,	@AuditAction			= 'Search'
--			,	@CreatedDate			= @AuditDate
--			,	@CreatedByPersonId		= @AuditId
--	END

--END
--GO


