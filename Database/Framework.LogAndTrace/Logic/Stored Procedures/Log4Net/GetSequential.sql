IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'Log4NetSequentialSearch')
BEGIN
	PRINT 'Dropping Procedure Log4NetSequentialSearch'
	DROP  Procedure  Log4NetSequentialSearch
END
GO

PRINT 'Creating Procedure Log4NetSequentialSearch'
GO

/******************************************************************************
**		File: 
**		Name: Log4NetSequentialSearch
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
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.Log4NetSequentialSearch
(	
		@ApplicationId				INT				= NULL
	,	@ExcludeApplicationId		INT
	,	@LogUser					VARCHAR(255)	= NULL
	,	@NoOfRecords				INT	
	,	@ConnectionKey				VARCHAR(100)		= NULL							
	,	@AuditId					INT						
	,	@AuditDate					DATETIME		= NULL		
	,	@SystemEntityType			VARCHAR(50)		= 'Log4Net'
)
AS
BEGIN

	SET NOCOUNT ON

	DECLARE 	@tId				INT
	DECLARE 	@tThread			VARCHAR(255)	
	DECLARE 	@tLevel				VARCHAR(50)		
	DECLARE 	@tLogger			VARCHAR(255)	
	DECLARE 	@tMessage			VARCHAR(4000)	
	DECLARE 	@tException			VARCHAR(2000)
	DECLARE 	@tApplicationId		INT
	DECLARE		@tConnectionKey		VARCHAR(100)
	DECLARE 	@tDate				DATETIME
	DECLARE 	@tLogUser			VARCHAR(255)

	DECLARE db_cursor CURSOR FOR		
		SELECT	TOP (@NoOfRecords)
				a.Id
			,	a.Thread
			,	a.Level
			,	a.Logger
			,	a.Message
			,	a.Exception
			,	a.ApplicationId
			,	a.ConnectionKey
			,	a.Date
			,	a.LogUser
		FROM	dbo.Log4Net a
		WHERE	a.ApplicationId =	ISNULL(@ApplicationId		, a.ApplicationId)
		AND		a.LogUser		=	ISNULL(@LogUser				, a.LogUser)
		AND		a.ConnectionKey=	ISNULL(@ConnectionKey		, a.ConnectionKey)
		AND		a.ApplicationId <>	ISNULL(@ExcludeApplicationId, -1)
		AND		a.ApplicationId IS NOT NULL
		ORDER BY a.Id DESC
	
	CREATE TABLE #TempMyTable 
	(	
			Id				INT				IDENTITY(1,1)
		,	Thread			VARCHAR(255)			
		,	Level			VARCHAR(50)				
		,	Logger			VARCHAR(255)			
		,	Message			VARCHAR(4000)			
		,	Exception		VARCHAR(2000)
		,	ApplicationId	INT
		,	ConnectionKey	VARCHAR(100)
		,	LogUser			VARCHAR(255)
		,	StartDate		DATETIME
		,	EndDate			DATETIME
		,	Count			INT	
	)
	
	DECLARE 	@myMessage			VARCHAR(4000)	
	DECLARE 	@myException		VARCHAR(2000)
	DECLARE 	@myLogUser			VARCHAR(255)
	DECLARE		@myConncetionKey	VARCHAR(100)
	DECLARE		@myId				INT	

	OPEN db_cursor
	FETCH NEXT
	FROM db_cursor INTO @tId, @tThread, @tLevel, @tLogger, @tMessage, @tException, @tApplicationId, @tConnectionKey, @tDate, @tLogUser
	WHILE @@FETCH_STATUS = 0
	BEGIN

			IF @myMessage IS NULL OR @myMessage <> @tMessage OR @myException <> @tException OR @myConncetionKey <> @tConnectionKey OR @myLogUser <> @tLogUser
				BEGIN

					INSERT INTO #TempMyTable 
					(
							Thread			
						,	Level			
						,	Logger			
						,	Message			
						,	Exception		
						,	ApplicationId	
						,	ConnectionKey
						,	LogUser			
						,	StartDate		
						,	EndDate			
						,	Count			
					)
					VALUES
					(
							@tThread			
						,	@tLevel			
						,	@tLogger			
						,	@tMessage			
						,	@tException		
						,	@tApplicationId	
						,	@tConnectionKey
						,	@tLogUser			
						,	@tDate		
						,	@tDate			
						,	1			
					)

					SET		@myMessage			= @tMessage
					SET		@myException		= @tException
					SET		@myLogUser			= @tLogUser
					SET		@myConncetionKey	= @tConnectionKey
					SET		@myId				= SCOPE_IDENTITY()

				END
			ELSE
				BEGIN

					UPDATE	#TempMyTable 
					SET		Count		= COUNT + 1
					,		StartDate	= @tDate
					WHERE	Id = @myId

				END
	
		FETCH NEXT	
		FROM db_cursor INTO @tId, @tThread, @tLevel, @tLogger, @tMessage, @tException, @tApplicationId, @tConnectionKey, @tDate, @tLogUser

	END

	CLOSE db_cursor 

	DEALLOCATE db_cursor
		
	SELECT  Id			
		,	LogUser		
		,	ApplicationId			
		,	Thread			
		,	Level			
		,	Logger			
		,	Message			
		,	Exception		
		,	StartDate		
		,	EndDate			
		,	Count	
		,	ConnectionKey	
	FROM	#TempMyTable 	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
	


	