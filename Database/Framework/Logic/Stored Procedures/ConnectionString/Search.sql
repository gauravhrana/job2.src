IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ConnectionStringSearch')
BEGIN
	PRINT 'Dropping Procedure ConnectionStringSearch'
	DROP Procedure ConnectionStringSearch
END
GO

PRINT 'Creating Procedure ConnectionStringSearch'
GO

/******************************************************************************
**		File: 
**		Name: ConnectionStringSearch
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
			EXEC ConnectionStringSearch NULL	, NULL	, NULL
			EXEC ConnectionStringSearch NULL	, 'K'	, NULL
			EXEC ConnectionStringSearch 1		, 'K'	, NULL
			EXEC ConnectionStringSearch 1		, NULL	, NULL
			EXEC ConnectionStringSearch NULL	, NULL	, 'W'

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
Create procedure dbo.ConnectionStringSearch
(
		@ConnectionStringId			INT				= NULL 				
	,	@Name						VARCHAR(100)	= NULL
	,	@AuditId					INT				
	,	@AuditDate					DATETIME		= NULL		
	,	@SystemEntityType			VARCHAR(50)		= 'ConnectionString'	 
	,	@ApplicationMode			INT				= NULL		
	,	@AddAuditInfo				INT				= 1
	,	@AddTraceInfo				INT				= 0
	,	@ReturnAuditInfo			INT				= 0	
)
WITH RECOMPILE
AS
BEGIN

	SET  NOCOUNT ON
	
	SET @Name	= ISNULL(@Name, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Name))) = 0
		BEGIN
			SET	@Name = '%'
		END
	-- Get Main System Entity Type ID
	DECLARE @SystemEntityId AS INT
	Select @SystemEntityId = dbo.GetSystemEntityTypeId(@SystemEntityType)

	SELECT	a.*
	INTO	#TempMain
	FROM	dbo.ConnectionString a
	WHERE	a.Name LIKE @Name	
	AND		a.ConnectionStringId		      = ISNULL(@ConnectionStringId, a.ConnectionStringId )
	ORDER BY a.Name					ASC
		,	 a.ConnectionStringId	ASC
			
	IF @ReturnAuditInfo = 1
	BEGIN

			-- get Audit latest record matching on key, ConnectionString
			SELECT		c.EntityKey			
				,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
			INTO		#HistortyInfo
			FROM 		#TempMain a		
			INNER JOIN	CommonServices.dbo.AuditHistory c		
						ON	c.EntityKey			= a.ConnectionStringId
						AND c.SystemEntityId	= @SystemEntityId
						AND c.AuditActionId		IN (1,2)
			GROUP BY	c.EntityKey	
	
			-- Get Audit Date and CreatedByPersonId for given records
			SELECT		a.ConnectionStringId	
					,	c.AuditActionId 
					,	c.CreatedDate
					,	c.CreatedByPersonId	
					, 	c.CreatedDate						AS	'UpdatedDate'
					,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
					,	d.Name								AS	'LastAction'
			INTO		#HistortyInfoDetails
			FROM		#TempMain a
			INNER JOIN	#HistortyInfo										b
						ON	b.EntityKey			= a.ConnectionStringId
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
						ON	a.ConnectionStringId	= b.ConnectionStringId
			ORDER BY	a.ConnectionStringId	ASC
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
			ORDER BY	a.ConnectionStringId

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
