IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='UserLoginSearch')
BEGIN
	PRINT 'Dropping Procedure UserLoginSearch'
	DROP Procedure UserLoginSearch
END
GO

PRINT 'Creating Procedure UserLoginSearch'
GO

/******************************************************************************
**		File: 
**		Name: UserLoginSearch
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
			EXEC UserLoginSearch NULL	, NULL	, NULL
			EXEC UserLoginSearch NULL	, 'K'	, NULL
			EXEC UserLoginSearch 1		, 'K'	, NULL
			EXEC UserLoginSearch 1		, NULL	, NULL
			EXEC UserLoginSearch NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				RecordDate:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/
Create procedure UserLoginSearch
(
		@UserLoginId			INT				= NULL 	
	,	@ApplicationId			INT				= NULL		
	,	@UserName				VARCHAR(50)		= NULL 
	,	@RecordDate				DATETIME		= NULL
	,	@RecordDate2			DATETIME		= NULL
	,	@UserLoginStatusId		INT				= NULL
	,	@AuditId				INT								
	,	@AuditDate				DATETIME		= NULL			
	,	@SystemEntityType		VARCHAR(50)		= 'UserLogin'
	,	@ApplicationMode		INT				= NULL		
	,	@AddAuditInfo			INT				 = 1
	,	@AddTraceInfo			INT				 = 0
	,	@ReturnAuditInfo		INT				 = 0
)
AS
BEGIN

	SET  NOCOUNT ON

	IF @AddTraceInfo = 1 
		BEGIN

			DECLARE @InputParametersLocal	VARCHAR(500)  
			DECLARE @InputValuesLocal		VARCHAR(5000)  
			SET @InputParametersLocal		= 'Name' 
			SET @InputValuesLocal			= @UserName  
			EXEC dbo.StoredProcedureLogInsert
					@Name						= 'dbo.UserLoginSearch'
				,	@InputParameters			= @InputParametersLocal
				,	@InputValues				= @InputValuesLocal	
				--,	@ExecutedBy					= 'System'	
		
		END


	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	-- TRACE

	-- if the UserLogin did not provide any values
	-- assume search on all possiblities ('%')
	SET @UserName	= ISNULL(@UserName, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@UserName))) = 0
		BEGIN
			SET	@UserName = '%'
		END
		
	 ELSE 
		BEGIN
			SET @UserName = replace(@UserName,' ','.')
		END
	  

	SELECT	a.UserLoginId			
		,	a.ApplicationId
		,	a.UserName						
		,	a.RecordDate			
		,	a.UserLoginStatusId		
		,	b.Name					AS	'UserLoginStatus'
	INTO	#TempMain
	FROM		dbo.UserLogin		a
	INNER JOIN	dbo.UserLoginStatus	b	ON	a.UserLoginStatusId	=	b.UserLoginStatusId	
	WHERE	a.UserName LIKE @UserName	+ '%'
	AND		a.UserLoginStatusId	=	ISNULL(@UserLoginStatusId, a.UserLoginStatusId)
	--AND		a.ApplicationId		=	ISNULL(@ApplicationId, a.ApplicationId )
	AND		a.UserLoginId		=	ISNULL(@UserLoginId, a.UserLoginId)
	AND		a.RecordDate	>= ISNULL(@RecordDate, a.RecordDate)   
	AND		a.RecordDate	<= ISNULL(@RecordDate2 , a.RecordDate)
	--AND		substring((CAST(a.RecordDate AS CHAR(50))), 1,(len(a.RecordDate)-4)) 	>= @FromSearchDate
	--AND		substring((CAST(a.RecordDate AS CHAR(50))), 1,(len(a.RecordDate)-4))	<= @ToSearchDate
	
	ORDER BY a.UserLoginStatusId	ASC,
			 a.UserName				ASC,
			 a.UserLoginId			ASC,
			 a.RecordDate			ASC		
			 
	IF @ReturnAuditInfo = 1
		BEGIN
		
		
		
			-- get Audit latest record matching on key, systementitytype
			SELECT		c.EntityKey			
				,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
			INTO		#HistortyInfo
			FROM 		#TempMain a		
			INNER JOIN	CommonServices.dbo.AuditHistory c		
						ON	c.EntityKey			= a.UserLoginId
						AND c.SystemEntityId	= @SystemEntityTypeId
						AND c.AuditActionId		IN (1,2)
			GROUP BY	c.EntityKey	
	
			-- Get Audit Date and CreatedByPersonId for given records
			SELECT		a.UserLoginId	
					,	c.AuditActionId 
					,	c.CreatedDate
					,	c.CreatedByPersonId	
					, 	c.CreatedDate						AS	'UpdatedDate'
					,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
					,	d.Name								AS	'LastAction'
			INTO		#HistortyInfoDetails
			FROM		#TempMain a
			INNER JOIN	#HistortyInfo										b
						ON	b.EntityKey			= a.UserLoginId
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
						ON	a.UserLoginId	= b.UserLoginId
			ORDER BY	a.UserLoginStatusId				ASC
					,	a.UserLoginId

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
			ORDER BY	a.UserLoginStatusId				ASC
					,	a.UserLoginId

		END

	IF @AddAuditInfo = 1 
		BEGIN 

			-- Create Audit Record
			EXEC dbo.AuditHistoryInsert
					@SystemEntityType		= @SystemEntityType		
				,	@EntityKey				= @UserLoginId
				,	@AuditAction			= 'Search'
				,	@CreatedDate			= @AuditDate
				,	@CreatedByPersonId		= @AuditId	
		
		END	
END
GO

