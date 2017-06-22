IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='NotificationEventTypeSearch')
BEGIN
	PRINT 'Dropping Procedure NotificationEventTypeSearch'
	DROP Procedure NotificationEventTypeSearch
END
GO

PRINT 'Creating Procedure NotificationEventTypeSearch'
GO

/******************************************************************************
**		File: 
**		Name: NotificationEventTypeSearch
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
			EXEC NotificationEventTypeSearch NULL	, NULL	, NULL
			EXEC NotificationEventTypeSearch NULL	, 'K'	, NULL
			EXEC NotificationEventTypeSearch 1		, 'K'	, NULL
			EXEC NotificationEventTypeSearch 1		, NULL	, NULL
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
Create procedure dbo.NotificationEventTypeSearch
(
		@NotificationEventTypeId			INT				= NULL 
	,	@Name					VARCHAR(50)		= ''
	,	@Description			VARCHAR(500)	= ''			 	
	,	@ApplicationId			INT				= NULL
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL
	,	@SystemEntityType		VARCHAR(50)		= 'NotificationEventType'
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
	SET @InputParametersLocal		= 'NotificationEventTypeId' + ', ' + 'Name' + ', ' + '@Description' 
	SET @InputValuesLocal			= CAST(@NotificationEventTypeId AS VARCHAR(50)) + ', '+ ISNULL(@Name, 'NULL') + ', '+ ISNULL(@Description, 'NULL') 
	EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.NotificationEventTypeSearch'
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

		SELECT	a.NotificationEventTypeId		
		,	a.ApplicationId 
		,	a.Name				 
		,	a.Description		 
		,	a.SortOrder
	INTO		#TempMain
	FROM		dbo.NotificationEventType a
	
	
	WHERE	a.Name LIKE @Name			+ '%'
	AND		a.Description	LIKE @Description + '%'
	AND		a.ApplicationId	  = ISNULL(@ApplicationId, a.ApplicationId)
	AND		a.NotificationEventTypeId	  = ISNULL(@NotificationEventTypeId, a.NotificationEventTypeId)
	ORDER BY a.SortOrder	ASC
		,	 a.NotificationEventTypeId	ASC		
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE NotificationEventTypeId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN

	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.NotificationEventTypeId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.NotificationEventTypeId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.NotificationEventTypeId
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
				ON	a.NotificationEventTypeId	= b.NotificationEventTypeId
	ORDER BY	a.SortOrder				ASC
			,	a.NotificationEventTypeId

	-- Show full details
	SELECT 		a.NotificationEventTypeId	
			,	a.ApplicationId	
			,	a.Name		
			,	a.Description		
			,	a.SortOrder	
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
	FROM		#TempMain				a
	LEFT JOIN	#HistortyInfoDetails	b	
				ON	a.NotificationEventTypeId	= b.NotificationEventTypeId
	ORDER BY	a.SortOrder				ASC
			,	a.NotificationEventTypeId
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
				,	a.NotificationEventTypeId
	END
	IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @NotificationEventTypeId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	END

END
GO



