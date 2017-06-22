IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='SystemEntityTypeSearch')
BEGIN
	PRINT 'Dropping Procedure SystemEntityTypeSearch'
	DROP Procedure SystemEntityTypeSearch
END
GO

PRINT 'Creating Procedure SystemEntityTypeSearch'
GO

/******************************************************************************
**		File: 
**		Name: SystemEntityTypeSearch
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
			EXEC SystemEntityTypeSearch NULL	, NULL	, NULL
			EXEC SystemEntityTypeSearch NULL	, 'K'	, NULL
			EXEC SystemEntityTypeSearch 1		, 'K'	, NULL
			EXEC SystemEntityTypeSearch 1		, NULL	, NULL
			EXEC SystemEntityTypeSearch NULL	, NULL	, 'W'

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
Create procedure dbo.SystemEntityTypeSearch
(
		@SystemEntityTypeId			INT				= NULL 				
	,	@EntityName					VARCHAR(100)	= NULL
	,	@AuditId					INT				
	,	@AuditDate					DATETIME		= NULL		
	,	@SystemEntityType			VARCHAR(50)		= 'SystemEntityType'	 
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
	END
	--DECLARE	@StoredProcedureLogId INT
	--EXEC dbo.StoredProcedureLogInsert
	--		@StoredProcedureLogId		= @StoredProcedureLogId	OUTPUT
	--	,	@Name		= 'dbo.SystemEntitySearch'	
	--	,	@ExecutedBy		= @AuditId	

 --   DECLARE @StoredProcedureLogDetailId INT
	--EXEC dbo.StoredProcedureLogDetailInsert
	--		@StoredProcedureLogDetailId		= @StoredProcedureLogDetailId OUTPUT
	--	,	@StoredProcedureLogId		= @StoredProcedureLogId
	--	,	@ParameterName  = 'EntityName'
	--	,   @ParameterValue = @EntityName

	-- if the client did not provide any values
	-- assume search on all possiblities ('%')
	SET @EntityName	= ISNULL(@EntityName, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@EntityName))) = 0
		BEGIN
			SET	@EntityName = '%'
		END

	-- Get Main System Entity Type ID
	DECLARE @SystemEntityId AS INT
	Select @SystemEntityId = dbo.GetSystemEntityTypeId(@SystemEntityType)

	SELECT	a.*
	INTO	#TempMain
	FROM	dbo.SystemEntityType a
	WHERE	a.EntityName LIKE @EntityName	
	AND a.SystemEntityTypeId		      = ISNULL(@SystemEntityTypeId, a.SystemEntityTypeId )
	ORDER BY a.EntityName			ASC
		,	 a.SystemEntityTypeId	ASC
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE SystemEntityTypeId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN
				

	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.SystemEntityTypeId
				AND c.SystemEntityId	= @SystemEntityId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.SystemEntityTypeId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.SystemEntityTypeId
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
				ON	a.SystemEntityTypeId	= b.SystemEntityTypeId
	ORDER BY	a.SystemEntityTypeId	ASC
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
				,	a.SystemEntityTypeId
	END
	IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
		END
END
GO
