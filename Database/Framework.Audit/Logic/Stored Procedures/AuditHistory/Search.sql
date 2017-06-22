IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AuditHistorySearch')
BEGIN
	PRINT 'Dropping Procedure AuditHistorySearch'
	DROP  Procedure  AuditHistorySearch
END
GO

PRINT 'Creating Procedure AuditHistorySearch'
GO
/******************************************************************************
**		File: 
**		Name: AuditHistorySearch
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------					   ---------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/
ALTER Procedure dbo.AuditHistorySearch
(	  
		@SystemEntityId			INT				= NULL	
	,	@EntityKey				INT				= NULL	
	,	@AuditActionId			INT				= NULL	
	,	@TraceId				INT				= NULL	   
	--,	@FromSearchDate			DATETIME		= NULL	
	--,	@ToSearchDate			DATETIME		= NULL	
	,	@CreatedByPersonId		INT				= NULL								
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'AuditHistory'
	,	@ApplicationMode	  	INT				= NULL		
	,	@AddAuditInfo		  	INT				 = 1
	,	@AddTraceInfo		  	INT				 = 0
	,	@ReturnAuditInfo	  	INT				 = 0	 
)
WITH RECOMPILE
AS 
BEGIN
	
	SET  NOCOUNT ON

	IF @AddTraceInfo = 1 
	BEGIN
		DECLARE @InputParametersLocal	VARCHAR(500)  
		DECLARE @InputValuesLocal		VARCHAR(5000)  
		SET @InputParametersLocal		= 'SystemEntityId' + ', ' + 'EntityKey' + ', ' + 'AuditActionId' +' , '+'TraceId'
		SET @InputValuesLocal			= CAST(@SystemEntityId AS VARCHAR(50)) + ', ' + CAST(@EntityKey AS VARCHAR(50)) + ', ' + CAST(@AuditActionId AS VARCHAR(50)) + ', ' + CAST(@TraceId AS VARCHAR(50))
		EXEC dbo.StoredProcedureLogInsert
				@Name						= 'dbo.AuditHistorySearch'	
			,	@InputParameters			= @InputParametersLocal
			,	@InputValues				= @InputValuesLocal	
	
	-- TRACE --	
	END	
	
	DECLARE @SystemEntityTypeId AS INT
	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	
	SELECT		a.AuditHistoryId		
			,	a.SystemEntityId		
			,	a.EntityKey			
			,	a.AuditActionId		
			,	a.CreatedDate
			,	a.CreatedDate					AS 'Date'
			,	a.CreatedByPersonId
			,	a.CreatedByPersonId				AS 'CreatedPersonById'
			,	b.EntityName					AS 'SystemEntity'
			,	c.Name							AS 'AuditAction'
			,	d.FirstName + ' ' + d.LastName	AS 'Action By'	
			,	d.FirstName + ' ' + d.LastName	AS 'Person'	
			,	e.Name							AS 'Trace'
	INTO		#TempMain	
	FROM		dbo.AuditHistory		a
	INNER JOIN	Configuration.dbo.SystemEntityType	b		ON		a.SystemEntityId = b.SystemEntityTypeId
	INNER JOIN	dbo.AuditAction						c		ON		a.AuditActionId = c.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser  				d		ON		a.CreatedByPersonId = d.ApplicationUserId
	INNER JOIN	dbo.Trace			e		ON		a.TraceId = e.TraceId
	WHERE		a.SystemEntityId		=		 COALESCE(@SystemEntityId, a.SystemEntityId)				
	AND			a.EntityKey				=		 COALESCE(@EntityKey, a.EntityKey)	
	AND			a.AuditActionId			=		 COALESCE(@AuditActionId, a.AuditActionId)				
	AND         a.TraceId				=		 COALESCE(@TraceId, a.TraceId)				   
	AND			a.CreatedByPersonId		=		 COALESCE(@CreatedByPersonId, a.CreatedByPersonId)
	ORDER BY	a.CreatedDate DESC
			,	a.AuditActionId DESC
			,	a.CreatedByPersonId DESC
	
			
   
   	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE AuditHistoryId < 0
	END
	
	IF @ReturnAuditInfo = 1
	BEGIN


		-- get Audit latest record matching on key, systementitytype
		SELECT		c.EntityKey			
			,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
		INTO		#HistortyInfo
		FROM 		#TempMain a		
		INNER JOIN	CommonServices.dbo.AuditHistory c		
					ON	c.EntityKey			= a.AuditHistoryId
					AND c.SystemEntityId	= @SystemEntityTypeId
					AND c.AuditActionId		IN (1,2)
		GROUP BY	c.EntityKey	

	-- Get Audit Date and CreatedByPersonId for given records
	SELECT	
				a.AuditHistoryId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.AuditHistoryId
	INNER JOIN	CommonServices.dbo.AuditHistory						c
				ON	c.AuditHistoryId	= b.MaxAuditHistoryId
	INNER JOIN	CommonServices.dbo.AuditAction						d	
				ON	c.AuditActionId 	= d.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
				ON	c.CreatedByPersonId	= e.ApplicationUserId	

	-- Show full details
	SELECT 	a.*				
		,	b.UpdatedDate
		,	b.UpdatedBy
		,	b.LastAction
	FROM		#TempMain				a
	LEFT JOIN	#HistortyInfoDetails	b	
				ON	a.AuditHistoryId	= b.AuditHistoryId
	ORDER BY	a.AuditActionId

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
		ORDER BY   	a.AuditHistoryId
	END
	
	IF @AddAuditInfo = 1 
	BEGIN

		--Create Audit Record
		EXEC dbo.AuditHistoryInsert
				@SystemEntityType		= @SystemEntityType
			,	@EntityKey				= NULL
			,	@AuditAction			= 'Search'
			,	@CreatedDate			= @AuditDate
			,	@CreatedByPersonId		= @AuditId
	END


END
GO


GO