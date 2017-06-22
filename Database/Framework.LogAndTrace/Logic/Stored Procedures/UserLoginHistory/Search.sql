
IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='UserLoginHistorySearch')
BEGIN
	PRINT 'Dropping Procedure UserLoginHistorySearch'
	DROP Procedure UserLoginHistorySearch
END
GO

PRINT 'Creating Procedure UserLoginHistorySearch'
GO

/******************************************************************************
**		File: 
**		Name: UserLoginHistorySearch
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
			EXEC UserLoginHistorySearch NULL	, NULL	, NULL
			EXEC UserLoginHistorySearch NULL	, 'K'	, NULL
			EXEC UserLoginHistorySearch 1		, 'K'	, NULL
			EXEC UserLoginHistorySearch 1		, NULL	, NULL
			EXEC UserLoginHistorySearch NULL	, NULL	, 'W'

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
Create procedure UserLoginHistorySearch
(
		@UserLoginHistoryId		INT				= NULL 	
	,	@ApplicationId			INT				= NULL		
	,	@UserName				VARCHAR(50)		= NULL 	
	,	@FromSearchDate			DATETIME		= NULL			
	,	@ToSearchDate			DATETIME		= NULL
	 ,	@UserId					INT				= NULL 	
	,	@AuditId				INT								
	,	@AuditDate				DATETIME		= NULL			
	,	@SystemEntityType		VARCHAR(50)		= 'UserLoginHistory'
	,	@ApplicationMode		INT				= NULL		
	,	@AddAuditInfo			INT				 = 1
	,	@AddTraceInfo			INT				 = 0
	,	@ReturnAuditInfo		INT				 = 0
)
WITH RECOMPILE
AS
BEGIN

	SET ANSI_NULLS ON;
	SET  NOCOUNT ON

	IF @AddTraceInfo = 1 
		BEGIN

			DECLARE @InputParametersLocal	VARCHAR(500)  
			DECLARE @InputValuesLocal		VARCHAR(5000)  
			SET @InputParametersLocal		= 'Name' 
			SET @InputValuesLocal			= @UserName  
			EXEC dbo.StoredProcedureLogInsert
					@Name						= 'dbo.UserLoginHistorySearch'
				,	@InputParameters			= @InputParametersLocal
				,	@InputValues				= @InputValuesLocal	
				--,	@ExecutedBy					= 'System'	
				
		END


	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	-- TRACE

	-- if the UserLoginHistory did not provide any values
	-- assume search on all possiblities ('%')
	SET @UserName	= ISNULL(@UserName, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@UserName))) = 0
		BEGIN
			SET	@UserName = '%'
		END	  
	
	
	IF 	@FromSearchDate	 IS NULL AND @ToSearchDate IS null
		BEGIN
   			
			SELECT	a.UserLoginHistoryId 			
				,	a.ApplicationId as 'ApplicationId'
				,	a.UserName
				,	a.UserId
				,	a.URL
				,	a.ServerName	
	   
			   ,	a.DateVisited
   			FROM		dbo.UserLoginHistory		a	
			WHERE	a.UserName LIKE @UserName	+ '%'
			AND		a.UserId			=	ISNULL(@UserId,	a.UserId)
			AND		a.ApplicationId		=	ISNULL(@ApplicationId, a.ApplicationId )
			AND		a.UserLoginHistoryId		=	ISNULL(@UserLoginHistoryId, a.UserLoginHistoryId)   
	
		END
	ELSE
		BEGIN
			
			SELECT	a.UserLoginHistoryId 			
				,	a.ApplicationId as 'ApplicationId'
	   			,	a.UserName
				,	a.UserId
				,	a.URL
				,	a.ServerName
		 
				,	a.DateVisited
   			FROM		dbo.UserLoginHistory		a	
			WHERE	a.UserName LIKE @UserName	+ '%'
			AND		a.UserId			=	ISNULL(@UserId,	a.UserId)
			AND		a.ApplicationId		=	ISNULL(@ApplicationId, a.ApplicationId )
			AND		a.UserLoginHistoryId		=	ISNULL(@UserLoginHistoryId, a.UserLoginHistoryId)
	
			AND		CONVERT(VARCHAR(30), DateVisited, 112) 	>= @FromSearchDate
			AND		CONVERT(VARCHAR(30), DateVisited, 112) <=	 @ToSearchDate
	
			ORDER BY  a.DateVisited   ASC 
   
		End  
   
   IF @AddAuditInfo = 1 
		BEGIN
		
			-- Create Audit Record
			EXEC dbo.AuditHistoryInsert
					@SystemEntityType		= 'UserLoginHistory'
				,	@EntityKey				= @UserLoginHistoryId
				,	@AuditAction			= 'Search'
				,	@CreatedDate			= @AuditDate
				,	@CreatedByPersonId		= @AuditId	
		
		END
	

END
GO
	

