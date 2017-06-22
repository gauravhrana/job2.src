--IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserSearch')
--BEGIN
--	PRINT 'Dropping Procedure ApplicationUserSearch'
--	DROP  Procedure  ApplicationUserSearch
--END
--GO

--PRINT 'Creating Procedure ApplicationUserSearch'
--GO

/******************************************************************************
**		File: 
**		Name: ApplicationUserSearch
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
			EXEC ApplicationUserSearch NULL	, NULL	, NULL
			EXEC ApplicationUserSearch NULL	, 'K'	, NULL
			EXEC ApplicationUserSearch 1	, 'K'	, NULL
			EXEC ApplicationUserSearch 1	, NULL	, NULL
			EXEC ApplicationUserSearch NULL	, NULL	, 'W'

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
ALTER Procedure dbo.ApplicationUserSearch
(
		@ApplicationUserId			INT				=	NULL  
	,	@ApplicationId				INT
	,	@ApplicationUserName		VARCHAR(50)		=	NULL	
	,	@LastName					VARCHAR(50)		=	NULL	
	,	@FirstName					VARCHAR(50)		=	NULL	
	,	@MiddleName					VARCHAR(50)		=	NULL
	,	@ApplicationUserTitleId		INT				=	NULL	
	,	@EmailAddress				VARCHAR(320)	=	NULL
	,	@AuditId					INT							
	,	@AuditDate					DATETIME		=	NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'ApplicationUser'	
	,	@ApplicationMode			INT				= NULL		
	,	@AddAuditInfo				INT				= 1
	,	@AddTraceInfo				INT				= 0
	,	@ReturnAuditInfo			INT				= 0	
)
WITH RECOMPILE
AS
BEGIN

	SET  NOCOUNT ON
	IF @AddTraceInfo = 1 
		BEGIN		
		
			DECLARE @InputParametersLocal	VARCHAR(500)  
			DECLARE @InputValuesLocal		VARCHAR(5000)  
			SET @InputParametersLocal		= 'FirstName' + ', '+ 'LastName'
			SET @InputValuesLocal			= @FirstName + ', ' + @LastName
			EXEC dbo.StoredProcedureLogInsert
				@Name					= 'dbo.ApplicationUserSearch'
			,	@InputParameters		= @InputParametersLocal
			,	@InputValues			= @InputValuesLocal 
	
		END

	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId	AS INT
	EXEC	dbo.SystemEntityTypeGetId 
			@SystemEntityType	=	@SystemEntityType
		,	@SystemEntityTypeId =	@SystemEntityTypeId OUTPUT

	-- if the client did not provide any values
	-- assume search on all possiblities ('%')
	SET	@ApplicationUserName	= ISNULL(@ApplicationUserName, '%')
	SET	@EmailAddress			= ISNULL(@EmailAddress, '%')
	SET	@FirstName				= ISNULL(@FirstName, '%')					
	SET	@LastName				= ISNULL(@LastName, '%')
	SET	@MiddleName				= ISNULL(@MiddleName, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(LTRIM(RTRIM(@ApplicationUserName))) = 0 
	BEGIN
		SET	@ApplicationUserName = '%'
	END
	
	IF LEN(LTRIM(RTRIM(@ApplicationId))) = 0 
	BEGIN
		SET	@ApplicationId = '%'
	END

	IF LEN(LTRIM(RTRIM(@EmailAddress))) = 0 
	BEGIN
		SET	@EmailAddress = '%'
	END

	IF LEN(LTRIM(RTRIM(@FirstName))) = 0 
	BEGIN
		SET	@FirstName = '%'
	END

	IF LEN(LTRIM(RTRIM(@LastName))) = 0 
	BEGIN
		SET	@LastName = '%'
	END

	IF LEN(LTRIM(RTRIM(@MiddleName))) = 0 
	BEGIN
		SET	@MiddleName = '%'
	END
	
	--Here only the first charecter of the string is required to search	
	SELECT	a.ApplicationUserId	
		,	a.ApplicationId	
		,	a.ApplicationUserName	
		,	a.EmailAddress								
		,	a.FirstName										
		,	a.LastName	
		,	a.MiddleName
		,	a.ApplicationUserTitleId
		,	b.Name													AS	'ApplicationUserTitle'								
		,	a.FirstName	+ ' ' + a.LastName +	' ' + a.LastName	AS	'FullName'
		,	c.Name	 											AS	'Application'			
		,	c.Code + ' - ' +	a.ApplicationUserName AS 'ApplicationCode'
	INTO		#TempMain
	FROM		dbo.ApplicationUser			a
	INNER JOIN  dbo.ApplicationUserTitle	b	ON	a.ApplicationUserTitleId	= b.ApplicationUserTitleId
	INNER JOIN	dbo.Application				c	ON	a.ApplicationId				= c.ApplicationId
	WHERE	a.ApplicationUserName	LIKE @ApplicationUserName + '%'
	AND		a.EmailAddress			LIKE @EmailAddress	+ '%'
	AND		a.LastName				LIKE @LastName	+ '%'
	AND		a.FirstName				LIKE @FirstName	+ '%'
	AND		a.MiddleName			LIKE @MiddleName + '%'
	AND		b.ApplicationUserTitleId	= ISNULL(@ApplicationUserTitleId,b.ApplicationUserTitleId )
	AND		c.ApplicationId				= ISNULL(@ApplicationId,c.ApplicationId )
	AND		a.ApplicationUserID			= ISNULL(@ApplicationUserID,a.ApplicationUserID )
	AND 	c.Code IS NOT NULL 
	ORDER BY		a.LastName	ASC				 
				,	a.FirstName ASC				
				,   a.MiddleName ASC			
				,	a.ApplicationUserId  ASC

			
	IF @ReturnAuditInfo = 1
		BEGIN

			-- get Audit latest record matching on key, systementitytype
			SELECT		c.EntityKey			
				,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
			INTO		#HistortyInfo
			FROM 		#TempMain a		
			INNER JOIN	CommonServices.dbo.AuditHistory c		
						ON	c.EntityKey			= a.ApplicationUserId
						AND c.SystemEntityId	= @SystemEntityTypeId
						AND c.AuditActionId		IN (1,2)
			GROUP BY	c.EntityKey	
	
			-- Get Audit Date and CreatedByPersonId for given records
			SELECT		a.ApplicationUserId	
					,	c.AuditActionId 
					,	c.CreatedDate
					,	c.CreatedByPersonId	
					, 	c.CreatedDate						AS	'UpdatedDate'
					,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
					,	d.Name								AS	'LastAction'
			INTO		#HistortyInfoDetails
			FROM		#TempMain a
			INNER JOIN	#HistortyInfo										b
						ON	b.EntityKey			= a.ApplicationUserId
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
						ON	a.ApplicationUserId	= b.ApplicationUserId
			ORDER BY	a.ApplicationUserId
		END
	ELSE
		BEGIN
			
			DECLARE @StaticUpdatedDate AS DATETIME
			SET @StaticUpdatedDate = Convert(datetime, '1/1/1900', 103)
		
			SELECT 	a.*
				, 	UpdatedDate = @StaticUpdatedDate
				,	UpdatedBy	= 'Unknown'
				,	LastAction	= 'Unknown'
			FROM	#TempMain a		
			ORDER BY	a.ApplicationUserId

		END
	IF @AddAuditInfo = 1 
		BEGIN

			--Create Audit Record@CreatedByPersonId
		  	EXEC dbo.AuditHistoryInsert
					@SystemEntityType				= @SystemEntityType	 
				,	@EntityKey						= @ApplicationUserId
				,	@AuditAction					= 'Search' 
				,	@CreatedDate					= @AuditDate
				,	@CreatedByPersonId				= @AuditId

		END
END

GO

