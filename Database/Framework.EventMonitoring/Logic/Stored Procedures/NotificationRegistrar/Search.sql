IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='NotificationRegistrarSearch')
BEGIN
	PRINT 'Dropping Procedure NotificationRegistrarSearch'
	DROP Procedure NotificationRegistrarSearch
END
GO

PRINT 'Creating Procedure NotificationRegistrarSearch'
GO

/******************************************************************************
**		File: 
**		Name: NotificationRegistrarSearch
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
			EXEC NotificationRegistrarSearch NULL	, NULL	, NULL
			EXEC NotificationRegistrarSearch NULL	, 'K'	, NULL
			EXEC NotificationRegistrarSearch 1		, 'K'	, NULL
			EXEC NotificationRegistrarSearch 1		, NULL	, NULL
			EXEC NotificationRegistrarSearch NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Developer:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/
Create procedure NotificationRegistrarSearch
(
		@NotificationRegistrarId	INT				= NULL 	
	,	@ApplicationId				INT				= NULL		
	,	@NotificationEventTypeId	INT				= NULL
	,	@NotificationPublisherId	INT				= NULL
	,	@Message					VARCHAR(255)	= NULL
	,	@PublishDateId				INT				= NULL				
	,	@PublishTimeId				INT				= NULL	
	,	@AuditId					INT								
	,	@AuditDate					DATETIME		= NULL			
	,	@SystemEntityType			VARCHAR(50)		= 'NotificationRegistrar'
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
	SET @InputParametersLocal		= 'Message' 
	SET @InputValuesLocal			= @Message 
	EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.NotificationRegistrarSearch'
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal	
		--,	@ExecutedBy					= 'System'	
	END
	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	-- TRACE

	-- if the NotificationRegistrar did not provide any values
	-- assume search on all possiblities ('%')
	SET @Message	= ISNULL(@Message, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Message))) = 0
		BEGIN
			SET	@Message = '%'
		END
	
	SELECT	a.NotificationRegistrarId			
		,	a.ApplicationId
		,	a.NotificationEventTypeId
		,	a.NotificationPublisherId
		,	a.Message			
		,	a.PublishDateId			
		,	a.PublishTimeId			
		,	b.Name					AS	'NotificationEventType'
		,	c.Name					AS	'NotificationPublisher'
		INTO	#TempMain
	FROM		dbo.NotificationRegistrar		a
	INNER JOIN	dbo.NotificationEventType			b
		ON	a.NotificationEventTypeId			=	b.NotificationEventTypeId
	INNER JOIN	dbo.NotificationPublisher			c
		ON	a.NotificationPublisherId			=	c.NotificationPublisherId
	WHERE	a.Message LIKE @Message	+ '%'
	AND		a.NotificationEventTypeId		= ISNULL(@NotificationEventTypeId, a.NotificationEventTypeId )
	AND		a.NotificationPublisherId		= ISNULL(@NotificationPublisherId, a.NotificationPublisherId )
	AND		a.NotificationRegistrarId		= ISNULL(@NotificationRegistrarId, a.NotificationRegistrarId )
	AND		a.ApplicationId					= ISNULL(@ApplicationId, a.ApplicationId )
	ORDER BY a.NotificationRegistrarId	ASC			 
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE NotificationRegistrarId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.NotificationRegistrarId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.NotificationRegistrarId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.NotificationRegistrarId
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
				ON	a.NotificationRegistrarId	= b.NotificationRegistrarId
	ORDER BY	a.NotificationRegistrarId
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
		ORDER BY	a.NotificationRegistrarId
	END
	IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'NotificationRegistrar'
		,	@EntityKey				= @NotificationRegistrarId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
	END
END
GO
	

