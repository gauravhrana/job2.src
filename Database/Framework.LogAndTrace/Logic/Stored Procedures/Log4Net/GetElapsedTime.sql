ALTER Procedure dbo.Log4NetElapsedTimeSearch
(	
		@ApplicationId				INT				= NULL
	,	@ConnectionKey				VARCHAR(50)	= NULL
	,	@LogUser					VARCHAR(50)	= NULL
	,	@Computer					VARCHAR(50)	= NULL	
	,	@AuditId					INT						
	,	@AuditDate					DATETIME		= NULL		
	,	@SystemEntityType			VARCHAR(50)		= 'Log4Net'
)
AS
BEGIN

	SET NOCOUNT ON
	
	SET @ConnectionKey	= ISNULL(@ConnectionKey, '%')

	IF LEN(LTRIM(RTRIM(@ConnectionKey))) = 0 
	BEGIN		
		SET	@ConnectionKey = '%'
	END 
	
	SET @Computer	= ISNULL(@Computer, '%')
	
	IF LEN(LTRIM(RTRIM(@Computer))) = 0 
	BEGIN
		SET	@Computer = '%'
	END 
	
	SELECT TOP 1001 a.*,LTRIM(RTRIM(b.ApplicationUserName)) AS 'ApplicationUser',b.ApplicationId AS 'AppId'
	INTO #Source
	FROM Log4Net a INNER JOIN AuthenticationAndAuthorization.dbo.ApplicationUser b 
	ON a.LogUser = b.ApplicationUserid
	WHERE a.ConnectionKey	LIKE @ConnectionKey + '%' 
	AND	  a.Computer		LIKE @Computer + '%'
	AND a.LogUser LIKE ISNULL(@LogUser, a.LogUser)	
	ORDER BY 1 DESC
   	
	SELECT *
	INTO #A
	FROM #Source
	WHERE Message NOT LIKE 'Elapsed Milliseconds%'
	ORDER BY 1 DESC
	
	SELECT TOP 1001 *
	INTO #B
	FROM #Source
	WHERE Message LIKE 'Elapsed Milliseconds%'
	ORDER BY 1 DESC
	
	SELECT a.LogUser, a.[Date],a.ApplicationUser,
	CAST (Reverse(SUBSTRING(substring(reverse(b.Message),charindex(',',reverse(b.Message))+1, 
    len(reverse(b.Message))),1,(CHARINDEX(' ',substring(reverse(b.Message),charindex(',',reverse(b.Message))+1, 
    len(reverse(b.Message))) + ' ')-1))) AS INT) AS 'ElapsedTime',
	CAST(STUFF(b.Message, 1, Len(b.Message) +1- CHARINDEX(' ',Reverse(b.Message)), '') AS INT) AS 'RecordCount',
	--b.Message, 
	a.Message, a.Computer,a.ConnectionKey
	FROM #A a
	LEFT JOIN #B b
	ON a.LogUser = b.LogUser
	AND a.Thread = b.Thread
	AND a.Logger = b.Logger
	AND a.ApplicationId = b.ApplicationId
	AND a.ConnectionKey = b.ConnectionKey
	AND a.StackTrace = b.StackTrace
	AND a.Computer = b.Computer
	AND b.Id = a.Id + 1
	WHERE b.Message IS NOT NULL
	ORDER BY a.[Date] DESC

	--SELECT * FROM #A
	--SELECT * FROM #B
	
	DROP TABLE #A
	DROP TABLE #B
	DROP TABLE #Source
	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO

