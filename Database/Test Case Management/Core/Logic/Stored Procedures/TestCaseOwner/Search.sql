﻿IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='TestCaseOwnerSearch')
BEGIN
	PRINT 'Dropping Procedure TestCaseOwnerSearch'
	DROP Procedure TestCaseOwnerSearch
END
GO

PRINT 'Creating Procedure TestCaseOwnerSearch'
GO

/******************************************************************************
**		File: 
**		Name: TestCaseOwnerSearch
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
			EXEC TestCaseOwnerSearch NULL	, NULL	, NULL
			EXEC TestCaseOwnerSearch NULL	, 'K'	, NULL
			EXEC TestCaseOwnerSearch 1		, 'K'	, NULL
			EXEC TestCaseOwnerSearch 1		, NULL	, NULL
			EXEC TaskEntitySearch NULL	, NULL	, 'W'

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
Create procedure dbo.TestCaseOwnerSearch
(
		@TestCaseOwnerId			INT				= NULL 
	,	@Name					VARCHAR(50)		= ''
	,	@Description			VARCHAR(500)	= ''			 	
	,	@ApplicationId			INT				= NULL
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL
	,	@SystemEntityType		VARCHAR(50)		= 'TestCaseOwner'
	,	@ApplicationMode		INT				= NULL		
	,	@AddAuditInfo			INT				= 1
	,	@AddTraceInfo			INT				= 0
	,	@ReturnAuditInfo		INT				= 0	
)
WITH RECOMPILE
AS
BEGIN

	SET  NOCOUNT ON

	IF @AddTraceInfo = 1 
		BEGIN

			DECLARE @InputParametersLocal	VARCHAR(500)  
			DECLARE @InputValuesLocal		VARCHAR(5000)  
			SET @InputParametersLocal		= 'TestCaseOwnerId' + ', ' + 'Name' + ', ' + '@Description' 
			SET @InputValuesLocal			= CAST(@TestCaseOwnerId AS VARCHAR(50)) + ', '+ ISNULL(@Name, 'NULL') + ', '+ ISNULL(@Description, 'NULL') 
			EXEC dbo.StoredProcedureLogInsert
					@Name						= 'dbo.TestCaseOwnerSearch'
				,	@InputParameters			= @InputParametersLocal
				,	@InputValues				= @InputValuesLocal	

		END

	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	
	-- if the client did not provide any values
	-- assume search on all possiblities ('%')
	SET @Name	= ISNULL(@Name, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Name))) = 0
		BEGIN
			SET	@NAME = '%'
		END

	SELECT	a.TestCaseOwnerId		
		,	a.ApplicationId 
		,	a.Name				 
		,	a.Description		 
		,	a.SortOrder
	INTO		#TempMain
	FROM		dbo.TestCaseOwner a	
	WHERE	a.Name LIKE @Name			+ '%'
	AND		a.Description	LIKE @Description + '%'
	AND a.ApplicationId	  = ISNULL(@ApplicationId, a.ApplicationId)
	AND a.TestCaseOwnerId	  = ISNULL(@TestCaseOwnerId, a.TestCaseOwnerId)
	ORDER BY a.SortOrder	ASC
		,	 a.TestCaseOwnerId	ASC

	IF @ReturnAuditInfo = 1
		BEGIN

			-- get Audit latest record matching on key, systementitytype
			SELECT		c.EntityKey			
				,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
			INTO		#HistortyInfo
			FROM 		#TempMain a		
			INNER JOIN	CommonServices.dbo.AuditHistory c		
						ON	c.EntityKey			= a.TestCaseOwnerId
						AND c.SystemEntityId	= @SystemEntityTypeId
						AND c.AuditActionId		IN (1,2)
			GROUP BY	c.EntityKey	
	
			-- Get Audit Date and CreatedByPersonId for given records
			SELECT		a.TestCaseOwnerId	
					,	c.AuditActionId 
					,	c.CreatedDate
					,	c.CreatedByPersonId	
					, 	c.CreatedDate						AS	'UpdatedDate'
					,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
					,	d.Name								AS	'LastAction'
			INTO		#HistortyInfoDetails
			FROM		#TempMain a
			INNER JOIN	#HistortyInfo										b
						ON	b.EntityKey			= a.TestCaseOwnerId
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
						ON	a.TestCaseOwnerId	= b.TestCaseOwnerId
			ORDER BY	a.SortOrder				ASC
					,	a.TestCaseOwnerId

			-- Show full details
			SELECT 		a.TestCaseOwnerId	
					,	a.ApplicationId	
					,	a.Name			
					,	a.Description		
					,	a.SortOrder	
					,	b.UpdatedDate
					,	b.UpdatedBy
					,	b.LastAction
			FROM		#TempMain				a
			LEFT JOIN	#HistortyInfoDetails	b	
						ON	a.TestCaseOwnerId	= b.TestCaseOwnerId
			ORDER BY	a.SortOrder				ASC
					,	a.TestCaseOwnerId
		
		END
	ELSE
		BEGIN
			
			SELECT 	a.*
				, 	UpdatedDate = '1/1/1900'
				,	UpdatedBy	= 'Unknown'
				,	LastAction	= 'Unknown'
			FROM	#TempMain a		
			ORDER BY	a.SortOrder				ASC
					,	a.TestCaseOwnerId

		END

	IF @AddAuditInfo = 1 
		BEGIN

			-- Create Audit Record
			EXEC dbo.AuditHistoryInsert
					@SystemEntityType		= @SystemEntityType
				,	@EntityKey				= @TestCaseOwnerId
				,	@AuditAction			= 'Search'
				,	@CreatedDate			= @AuditDate
				,	@CreatedByPersonId		= @AuditId
		
		END

END
GO

