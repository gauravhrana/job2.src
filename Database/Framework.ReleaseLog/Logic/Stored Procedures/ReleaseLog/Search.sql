IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ReleaseLogSearch')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogSearch'
	DROP Procedure ReleaseLogSearch
END
GO

PRINT 'Creating Procedure ReleaseLogSearch'
GO

/******************************************************************************
**		File: 
**		Name: ReleaseLogSearch
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
			EXEC ReleaseLogSearch NULL	, NULL	, NULL
			EXEC ReleaseLogSearch NULL	, 'K'	, NULL
			EXEC ReleaseLogSearch 1		, 'K'	, NULL
			EXEC ReleaseLogSearch 1		, NULL	, NULL
			EXEC ReleaseLogSearch NULL	, NULL	, 'W'

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
Create procedure dbo.ReleaseLogSearch
(
		@ReleaseLogId			INT				= NULL 		
	,	@ApplicationId			INT				= NULL 	
	,	@ReleaseLogStatusId		INT				= NULL 		
	,	@Name					VARCHAR(50)		= NULL 	
	,	@AuditId				INT		
	,	@ReleaseDateMin			DATETIME		= NULL	
	,	@ReleaseDateMax			DATETIME		= NULL	
	,	@AuditDate				DATETIME		= NULL	
	,	@SystemEntityType		VARCHAR(50)		= 'ReleaseLog'	 	
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
	
			DECLARE @InputParametersLocal	VARCHAR(500)  
			DECLARE @InputValuesLocal		VARCHAR(5000)  
			SET @InputParametersLocal		= 'Name' 
			SET @InputValuesLocal			= @Name
			EXEC dbo.StoredProcedureLogInsert
					@Name						= 'dbo.ReleaseLogSearch'
				,	@InputParameters			= @InputParametersLocal
				,	@InputValues				= @InputValuesLocal
		
		END	

	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	
	-- if the ReleaseLog did not provide any values
	-- assume search on all possiblities ('%')
	SET @Name	= ISNULL(@Name, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Name))) = 0
		BEGIN
			SET	@NAME = '%'
		END

	SELECT	a.*
		,	b.Name							as 'Application'		
		,	c.Name							AS	'ReleaseLogStatus'
	INTO	#TempMain
	FROM	dbo.ReleaseLog a
	INNER JOIN AuthenticationAndAuthorization.dbo.Application b 
		ON	a.ApplicationId = b.ApplicationId
	INNER JOIN	dbo.ReleaseLogStatus				c
		ON	a.ReleaseLogStatusId = c.ReleaseLogStatusId
	WHERE	a.Name LIKE @Name	+ '%'
	AND a.ApplicationId	= ISNULL(@ApplicationId, a.ApplicationId)
	AND a.ReleaseLogStatusId	= ISNULL(@ReleaseLogStatusId, a.ReleaseLogStatusId)
	AND a.ReleaseLogId	= ISNULL(@ReleaseLogId, a.ReleaseLogId)
	AND a.ReleaseDate  >= ISNULL(@ReleaseDateMin, a.ReleaseDate)
	AND a.ReleaseDate  <= ISNULL(@ReleaseDateMax, a.ReleaseDate)
	ORDER BY SortOrder			ASC
		,	 ReleaseLogId		ASC

	IF @ReturnAuditInfo = 1
		BEGIN
	
			-- get Audit latest record matching on key, systementitytype
			SELECT		c.EntityKey			
				,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
			INTO		#HistortyInfo
			FROM 		#TempMain a		
			INNER JOIN	CommonServices.dbo.AuditHistory c		
						ON	c.EntityKey			= a.ReleaseLogId
						AND c.SystemEntityId	= @SystemEntityTypeId
						AND c.AuditActionId		IN (1,2)
			GROUP BY	c.EntityKey	
	
			-- Get Audit Date and CreatedByPersonId for given records
			SELECT		a.ReleaseLogId	
					,	c.AuditActionId 
					,	c.CreatedDate
					,	c.CreatedByPersonId	
					, 	c.CreatedDate						AS	'UpdatedDate'
					,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
					,	d.Name								AS	'LastAction'
			INTO		#HistortyInfoDetails
			FROM		#TempMain a
			INNER JOIN	#HistortyInfo										b
						ON	b.EntityKey			= a.ReleaseLogId
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
						ON	a.ReleaseLogId	= b.ReleaseLogId
			ORDER BY	a.SortOrder				ASC
					,	a.ReleaseLogId
		
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
					,	a.ReleaseLogId

		END

	IF @AddAuditInfo = 1 
		BEGIN

			-- Create Audit Record
			EXEC dbo.AuditHistoryInsert			
					@SystemEntityType		= @SystemEntityType
				,	@EntityKey				= @ReleaseLogId
				,	@AuditAction			= 'Search'
				,	@CreatedDate			= @AuditDate
				,	@CreatedByPersonId		= @AuditId
		
		END

END
GO
	

