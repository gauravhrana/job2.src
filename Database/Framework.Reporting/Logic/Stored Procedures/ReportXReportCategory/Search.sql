IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ReportXReportCategorySearch')
BEGIN
	PRINT 'Dropping Procedure ReportXReportCategorySearch'
	DROP Procedure ReportXReportCategorySearch
END
GO

PRINT 'Creating Procedure ReportXReportCategorySearch'
GO

/******************************************************************************
**		File: 
**		Name: ReportXReportCategorySearch
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
			EXEC ReportXReportCategorySearch NULL	, NULL	, NULL
			EXEC ReportXReportCategorySearch NULL	, 'K'	, NULL
			EXEC ReportXReportCategorySearch 1		, 'K'	, NULL
			EXEC ReportXReportCategorySearch 1		, NULL	, NULL
			EXEC ReportXReportCategorySearch NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------
**    
*******************************************************************************/
Create procedure dbo.ReportXReportCategorySearch
(
		@ReportXReportCategoryId	INT				= NULL 	
	,	@ReportCategoryId			INT				= NULL 	
	,	@ReportId					INT				= NULL	
	,	@ApplicationId				INT				= NULL 		
	,	@AuditId					INT							
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'ReportXReportCategory'	
	,	@ApplicationMode			INT				= NULL		
	,	@AddAuditInfo				INT				 = 1
	,	@AddTraceInfo				INT				 = 0
	,	@ReturnAuditInfo			INT				 = 0		 
)	
WITH RECOMPILE
AS
BEGIN	

	SET  NOCOUNT ON

	SELECT	a.ReportXReportCategoryId												
		,	a.ReportCategoryId																
		,	a.ReportId		
		,	b.Name					AS 'ReportCategory'
		,	c.Name					AS 'Report'		
	INTO #TempMain
	FROM		dbo.ReportXReportCategory	a
	INNER JOIN	dbo.ReportCategory			b ON a.ReportCategoryId			= b.ReportCategoryId
	INNER JOIN	dbo.Report					c ON a.ReportId				= c.ReportId	
	WHERE	a.ReportCategoryId			= ISNULL(@ReportCategoryId, a.ReportCategoryId)
	AND a.ReportId						= ISNULL(@ReportId, a.ReportId)	
	AND a.ReportXReportCategoryId		= ISNULL(@ReportXReportCategoryId, a.ReportXReportCategoryId)
	AND a.ApplicationId					= ISNULL(@ApplicationId, a.ApplicationId)
	ORDER BY a.ReportId				ASC
		,	 a.ReportCategoryId			ASC
		,	 a.ReportXReportCategoryId	ASC

	IF @ReturnAuditInfo = 1
		BEGIN

			CREATE TABLE #TempRecords
			(	
					ReportXReportCategoryId	INT	
				,	AuditActionId				INT			
				, 	LastUpdatedDate				DATETIME
				,	LastUpdatedBy				INT
			)

				-- Get Main System Entity Type ID
			DECLARE @SystemEntityTypeId AS INT
			Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	
			DECLARE @AuditHistoryId AS INT
			Select @AuditHistoryId = dbo.GetAuditHistoryId	(a.ProjectId, @SystemEntityTypeId) from TaskTimeTracker.dbo.Project a		
	
			SELECT		
					c.EntityKey			
				,	b.EntityName	
				,	MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
				INTO		#HistortyInfo
			FROM 		#TempMain a		
			INNER JOIN	CommonServices.dbo.AuditHistory c		ON		c.EntityKey			= a.ReportXReportCategoryId
			INNER JOIN	Configuration.dbo.SystemEntityType b	ON 		b.EntityName		= @SystemEntityType
			INNER JOIN	CommonServices.dbo.AuditAction d	    ON      d.AuditActionId		IN (1,2)
			GROUP BY	c.EntityKey			
					,	b.EntityName

			INSERT 
			INTO		#TempRecords
			SELECT		
					a.ReportXReportCategoryId	
				,	c.AuditActionId 
				,	c.CreatedDate
				,	c.CreatedByPersonId	
			FROM		#TempMain a
			INNER JOIN	#HistortyInfo b
				ON		b.EntityKey			= a.ReportXReportCategoryId
			INNER JOIN	CommonServices.dbo.AuditHistory c
				ON		c.AuditHistoryId	= b.MaxAuditHistoryId

			SELECT 	a.*	
				, 	b.LastUpdatedDate					AS	'UpdatedDate'
				,	d.FirstName + ' ' + d.LastName		AS	'UpdatedBy'
				,	c.Name								AS	'LastAction'
			FROM #TempMain a
			INNER JOIN #TempRecords			b		ON						a.ReportXReportCategoryId			= b.ReportXReportCategoryId
			INNER JOIN CommonServices.dbo.AuditAction						c		ON		b.AuditActionId	 	= c.AuditActionId
			INNER JOIN AuthenticationAndAuthorization.dbo.ApplicationUser	d		ON		b.LastUpdatedBy		= d.ApplicationUserId

		END

	ELSE
		BEGIN

			SELECT 	a.*
				, 	UpdatedDate = '1/1/1900'
				,	UpdatedBy	= 'Unknown'
				,	LastAction	= 'Unknown'
			FROM	#TempMain a	

		END

	IF @AddAuditInfo = 1 
		BEGIN

			-- Create Audit Record
			EXEC dbo.AuditHistoryInsert
					@SystemEntityType		= 'ReportXReportCategory'
				,	@EntityKey				= @ReportXReportCategoryId
				,	@AuditAction			= 'Search'
				,	@CreatedDate			= @AuditDate
				,	@CreatedByPersonId		= @AuditId
		
		END

END
GO
	

