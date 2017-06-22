﻿IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ThemeKeySearch')
BEGIN
	PRINT 'Dropping Procedure ThemeKeySearch'
	DROP Procedure ThemeKeySearch
END
GO

PRINT 'Creating Procedure ThemeKeySearch'
GO

/******************************************************************************
**		File: 
**		Name: ThemeKeySearch
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
			EXEC ThemeKeySearch NULL	, NULL	, NULL
			EXEC ThemeKeySearch NULL	, 'K'	, NULL
			EXEC ThemeKeySearch 1		, 'K'	, NULL
			EXEC ThemeKeySearch 1		, NULL	, NULL
			EXEC ThemeKeySearch NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/
Create procedure ThemeKeySearch
(
		@ThemeKeyId			INT				= NULL 	
	,	@ApplicationId		INT				= NULL		
	,	@Name				VARCHAR(50)		= NULL 			
	,	@AuditId			INT								
	,	@AuditDate			DATETIME		= NULL			
	,	@SystemEntityType	VARCHAR(50)		= 'ThemeKey'
	,	@ApplicationMode					INT				= NULL		
	,	@AddAuditInfo						INT				 = 1
	,	@AddTraceInfo						INT				 = 0
	,	@ReturnAuditInfo					INT				 = 0	
)
WITH RECOMPILE
AS
BEGIN

	SET  NOCOUNT ON
	IF @AddTraceInfo = 1 
	BEGIN	
	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000)  
	SET @InputParametersLocal		= 'Name' 
	SET @InputValuesLocal			= @Name  
	EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.ThemeKeySearch'
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal	
		--,	@ExecutedBy					= 'System'	
	END
	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	-- TRACE

	-- if the ThemeKey did not provide any values
	-- assume search on all possiblities ('%')
	SET @Name	= ISNULL(@Name, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Name))) = 0
		BEGIN
			SET	@NAME = '%'
		END
	
	SELECT	a.ThemeKeyId	
		,	a.ApplicationId	
		,	a.Name				
		,	a.Description			
		,	a.SortOrder	
	INTO	#TempMain
	FROM	dbo.ThemeKey a	
	WHERE	a.Name LIKE @Name	+ '%'
	AND a.ThemeKeyId		= ISNULL(@ThemeKeyId, a.ThemeKeyId )
	AND a.ApplicationId			= ISNULL(@ApplicationId, a.ApplicationId )
	ORDER BY a.SortOrder	ASC,
			 a.Name			ASC,
			 a.ThemeKeyId	ASC		
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE ThemeKeyId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN	 
		
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.ThemeKeyId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.ThemeKeyId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.ThemeKeyId
	INNER JOIN	CommonServices.dbo.AuditHistory						c
				ON	c.AuditHistoryId	= b.MaxAuditHistoryId
	INNER JOIN	CommonServices.dbo.AuditAction						d	
				ON	c.AuditActionId 	= d.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
				ON	c.CreatedByPersonId	= e.ApplicationUserId		
	
	SELECT 	a.ThemeKeyId
		,	a.ApplicationId			
		,	a.Name			
		,	a.Description		
		,	a.SortOrder			
		, 	b.UpdatedDate
		,	b.UpdatedBy
		,	b.LastAction
	FROM #TempMain a
	LEFT JOIN #HistortyInfoDetails	b	
				ON	a.ThemeKeyId	= b.ThemeKeyId
	ORDER BY	a.SortOrder				ASC
			,	a.ThemeKeyId
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
		ORDER BY	a.SortOrder				ASC
				,	a.ThemeKeyId
	END
	IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ThemeKey'
		,	@EntityKey				= @ThemeKeyId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
	END
END
GO
	

