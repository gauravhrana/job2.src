IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ReleaseLogStatusSearch')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogStatusSearch'
	DROP Procedure ReleaseLogStatusSearch
END
GO

PRINT 'Creating Procedure ReleaseLogStatusSearch'
GO

/******************************************************************************
**		File: 
**		Name: ReleaseLogStatusSearch
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
			EXEC ReleaseLogStatusSearch NULL	, NULL	, NULL
			EXEC ReleaseLogStatusSearch NULL	, 'K'	, NULL
			EXEC ReleaseLogStatusSearch 1		, 'K'	, NULL
			EXEC ReleaseLogStatusSearch 1		, NULL	, NULL
			EXEC ReleaseLogStatusSearch NULL	, NULL	, 'W'
			EXEC ReleaseLogStatusSearch @AuditId = 5

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
Create procedure dbo.ReleaseLogStatusSearch
(
		@ReleaseLogStatusId		INT				= NULL 	
	,	@ApplicationId			INT				= NULL
	,	@Name					VARCHAR(50)		= NULL 
	,	@Description			VARCHAR(500)	= NULL 			
	,	@AuditId				INT								
	,	@AuditDate				DATETIME		= NULL			
	,	@SystemEntityType		VARCHAR(50)		= 'ReleaseLogStatus'
	,	@ApplicationMode		INT				= NULL		
	,	@AddAuditInfo			INT				 = 1
	,	@AddTraceInfo			INT				 = 0
	,	@ReturnAuditInfo		INT				 = 0			
)
WITH RECOMPILE
AS
BEGIN

	SET	NOCOUNT ON

	IF @AddTraceInfo = 1 
		BEGIN

			-- TRACE --
			DECLARE @InputParametersLocal	VARCHAR(500)  
			DECLARE @InputValuesLocal		VARCHAR(5000)  

			SET @InputParametersLocal		= 'ReleaseLogStatusId' + ', ' + 'Name' + ', ' + 'Description' 
			SET @InputValuesLocal			= CAST(@ReleaseLogStatusId AS VARCHAR(50)) + ', '+ ISNULL(@Name, 'NULL') + ', '+ ISNULL(@Description, 'NULL') 

			EXEC dbo.StoredProcedureLogInsert
				@Name						= 'dbo.ReleaseLogStatusSearch'
			,	@InputParameters			= @InputParametersLocal
			,	@InputValues				= @InputValuesLocal	
			-- TRACE --		
		
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

	SELECT		a.ReleaseLogStatusId
			,	a.ApplicationId		
			,	a.Name				
			,	a.Description			
			,	a.SortOrder		
			,	b.Name					AS	'Application'	
	INTO		#TempMain		
	FROM		dbo.ReleaseLogStatus a	
	INNER JOIN	AuthenticationAndAuthorization.dbo.Application	b
		ON	b.ApplicationId = a.ApplicationId
	WHERE		a.Name			LIKE @Name + '%'
	AND			a.Description	LIKE @Description + '%'
	AND			a.ReleaseLogStatusId		= ISNULL(@ReleaseLogStatusId		, a.ReleaseLogStatusId)
	AND			a.ApplicationId				= ISNULL(@ApplicationId	, a.ApplicationId)	
	ORDER BY	a.SortOrder				ASC
		,		a.Name					ASC
		,		a.ReleaseLogStatusId	ASC

	IF @ReturnAuditInfo = 1
		BEGIN
			
			-- get Audit latest record matching on key, systementitytype
			SELECT		c.EntityKey			
				,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
			INTO		#HistortyInfo
			FROM 		#TempMain a		
			INNER JOIN	CommonServices.dbo.AuditHistory c		
						ON	c.EntityKey			= a.ReleaseLogStatusId
						AND c.SystemEntityId	= @SystemEntityTypeId
						AND c.AuditActionId		IN (1,2)
			GROUP BY	c.EntityKey			
	
			-- Get Audit Date and CreatedByPersonId for given records
			SELECT		
						a.ReleaseLogStatusId	
					,	c.AuditActionId 
					,	c.CreatedDate
					,	c.CreatedByPersonId	
					, 	c.CreatedDate						AS	'UpdatedDate'
					,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
					,	d.Name								AS	'LastAction'
			INTO		#HistortyInfoDetails
			FROM		#TempMain a
			INNER JOIN	#HistortyInfo										b
						ON	b.EntityKey			= a.ReleaseLogStatusId
			INNER JOIN	CommonServices.dbo.AuditHistory						c
						ON	c.AuditHistoryId	= b.MaxAuditHistoryId
			INNER JOIN	CommonServices.dbo.AuditAction						d	
						ON	c.AuditActionId 	= d.AuditActionId
			INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
						ON	c.CreatedByPersonId	= e.ApplicationUserId
	
			-- Show full details
			SELECT 		a.ReleaseLogStatusId	
					,	a.ApplicationId	
					,	a.Name			
					,	a.Description		
					,	a.SortOrder	
					,	a.Application
					,	b.UpdatedDate
					,	b.UpdatedBy
					,	b.LastAction
			FROM		#TempMain				a
			LEFT JOIN	#HistortyInfoDetails	b	
						ON	a.ReleaseLogStatusId	= b.ReleaseLogStatusId
			ORDER BY	a.SortOrder				ASC
					,	a.ReleaseLogStatusId
		
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
					,	a.ReleaseLogStatusId

		END

	IF @AddAuditInfo = 1 
		BEGIN

			-- Create Audit Record
			EXEC dbo.AuditHistoryInsert
					@SystemEntityType		= 'ReleaseLogStatus'
				,	@EntityKey				= @ReleaseLogStatusId
				,	@AuditAction			= 'Search'
				,	@CreatedDate			= @AuditDate
				,	@CreatedByPersonId		= @AuditId	
		
		END
	
END
GO
	
