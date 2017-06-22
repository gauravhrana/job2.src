IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name ='ReleaseLogDetailSearch')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogDetailSearch'
	DROP Procedure ReleaseLogDetailSearch
END
GO

PRINT 'Creating Procedure ReleaseLogDetailSearch'
GO

/******************************************************************************
**		File: 
**		Description: ReleaseLogSearch
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
Create procedure dbo.ReleaseLogDetailSearch
(
		@ReleaseLogDetailId		INT				= NULL 	
	,	@ApplicationId			INT				= NULL
	,	@ReleaseLogId			INT				= NULL  
	,	@Description			VARCHAR(50)		= NULL 	
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL
	,	@StoredProcedureLogId   INT				= NULL
	,	@SystemEntityType		VARCHAR(50)		= 'ReleaseLogDetails'
)
AS
BEGIN

	--DECLARE @InputParametersLocal	VARCHAR(500)  
	--DECLARE @InputValuesLocal		VARCHAR(5000)  
	--SET @InputParametersLocal		= 'ReleaseLogId' 
	--SET @InputValuesLocal			= CAST(ISNULL(@ReleaseLogId, '%') AS VARCHAR(50))
	--EXEC dbo.StoredProcedureLogInsert
	--		@Name						= 'dbo.ReleaseLogDetailSearch'
	--	,	@InputParameters			= @InputParametersLocal
	--	,	@InputValues				= @InputValuesLocal	

 --   DECLARE @StoredProcedureLogDetailId INT
	--EXEC dbo.StoredProcedureLogDetailInsert
	--		@StoredProcedureLogDetailId		= @StoredProcedureLogDetailId OUTPUT
	--	,	@StoredProcedureLogId		= @StoredProcedureLogId
	--	,	@ParameterName  = 'ReleaseLogId'
	--	,   @ParameterValue = @ReleaseLogId
	
	-- if the ReleaseLog did not provide any values
	-- assume search on all possiblities ('%')
	SET @Description	= ISNULL(@Description, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Description))) = 0
		BEGIN
			SET	@Description = '%'
		END

	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)	
	
	SELECT	a.ReleaseLogDetailId
		,	a.ApplicationId		
	    ,   a.ReleaseLogId              
		,	a.ItemNo                    
		,	a.Description				
		,	a.SortOrder					
		,	a.RequestedBy               
		,	a.PrimaryDeveloper          
		,	a.RequestedDate				
		,	b.Name AS 'ReleaseLog'
	INTO	#TempMain 
	FROM	dbo.ReleaseLogDetail a
	INNER JOIN ReleaseLog b ON a.ReleaseLogId = b.ReleaseLogId
	WHERE	a.Description LIKE @Description	+ '%'
	AND		a.ApplicationId =
			CASE
				WHEN @ApplicationId IS NULL THEN a.ApplicationId
				ELSE @ApplicationId
			END
	AND		a.ReleaseLogId =
			CASE
				WHEN @ReleaseLogId IS NULL THEN a.ReleaseLogId 
				ELSE @ReleaseLogId
			END
	AND		a.ReleaseLogDetailId =
			CASE
				WHEN @ReleaseLogDetailId IS NULL THEN a.ReleaseLogDetailId
				ELSE @ReleaseLogDetailId
			END
	ORDER BY a.SortOrder				ASC
		,	 a.ReleaseLogDetailId		ASC
		,	 a.ReleaseLogId				ASC

	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.ReleaseLogDetailId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.ReleaseLogDetailId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.ReleaseLogDetailId
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
				ON	a.ReleaseLogDetailId	= b.ReleaseLogDetailId
	ORDER BY	a.ReleaseLogDetailId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ReleaseLogDetailId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
	

