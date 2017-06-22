﻿IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ApplicationSearch')
BEGIN
	PRINT 'Dropping Procedure ApplicationSearch'
	DROP Procedure ApplicationSearch
END
GO

PRINT 'Creating Procedure ApplicationSearch'
GO

/******************************************************************************
**		Task: 
**		Name: ApplicationSearch
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
			EXEC ApplicationSearch NULL	, NULL	, NULL
			EXEC ApplicationSearch NULL	, 'K'	, NULL
			EXEC ApplicationSearch 1	, 'K'	, NULL
			EXEC ApplicationSearch 1	, NULL	, NULL
			EXEC ApplicationSearch NULL	, NULL	, 'W'

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
Create procedure ApplicationSearch
(
		@ApplicationId			INT				= NULL 			
	,	@Name					VARCHAR(50)		= NULL 
	,	@Code					VARCHAR(50)		= NULL										
	,	@AuditId				INT								
	,	@AuditDate				DATETIME		= NULL
	,	@SystemEntityType		VARCHAR(50)		= 'Application'
	,	@ApplicationMode		INT				= NULL		
	,	@AddAuditInfo			INT				 = 1
	,	@AddTraceInfo			INT				 = 0
	,	@ReturnAuditInfo		INT				 = 0	
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
			@Name						= 'dbo.ApplicationSearch'
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal	
		--,	@ExecutedBy					= 'System'
	END	


	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	-- TRACE

	-- if the Application did not provide any values
	-- assume search on all possiblities ('%')
	SET @Name	= ISNULL(@Name, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Name))) = 0
		BEGIN
			SET	@NAME = '%'
		END	

	SET	@Code = ISNULL(@Code,'%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(LTRIM(RTRIM(@Code))) = 0 
	BEGIN
		SET	@Code = '%'
	END
	
	SELECT	a.ApplicationId	
		,	a.Name				
		,	a.Description			
		,	a.SortOrder	
		,	a.Code	
		,	(
				CASE 
					WHEN b.RenderApplicationFilter IS NULL
						THEN 0
					ELSE 
						b.RenderApplicationFilter
				END
			)		AS RenderApplicationFilter
	INTO	#TempMain
	FROM	dbo.Application a
	LEFT JOIN ApplicationAttribute b 
	ON b.ApplicationId = a.ApplicationId
	WHERE	a.Name LIKE @Name	+ '%' 
	AND		ISNULL(a.Code, '-1') LIKE @Code + '%' 
	AND		a.ApplicationId = ISNULL(@ApplicationId, a.ApplicationId )
	ORDER BY a.SortOrder		ASC,
			 a.Name				ASC,
			 a.Code				ASC,
			 a.ApplicationId	ASC

	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE ApplicationId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN


			 -- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.ApplicationId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.ApplicationId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.ApplicationId
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
				ON	a.ApplicationId	= b.ApplicationId
	ORDER BY	a.SortOrder				ASC
			,	a.ApplicationId
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
				,	a.ApplicationId
	END

	IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@EntityKey				= @ApplicationId
		,	@AuditAction			= 'Search'
		,	@SystemEntityType		= @SystemEntityType
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

	END
END
GO
	

