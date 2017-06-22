IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'Log4NetInsert')
BEGIN
	PRINT 'Dropping Procedure Log4NetInsert'
	DROP  Procedure Log4NetInsert
END
GO

PRINT 'Creating Procedure Log4NetInsert'
GO

/*********************************************************************************************
**		File: 
**		Level:Log4NetInsert
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
**     ----------							-----------
**
**		Auth: 
**		Date: 
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**********************************************************************************************/

CREATE Procedure dbo.Log4NetInsert
(
		@Id						INT				= NULL 	OUTPUT	
	,	@LogUser				VARCHAR(255)
	,	@Date					DATETIME
	,	@Thread					VARCHAR(255)
	,	@Level					VARCHAR(50)	
	,	@Logger					VARCHAR(255)
	,	@Message				VARCHAR(4000)
	,	@Exception				VARCHAR(2000)	
	,	@StackTrace				VARCHAR(6000)	= NULL
	,	@Computer				VARCHAR(100)	= NULL
	,	@ApplicationId			INT
	,	@ConnectionKey			VARCHAR(100)
	,	@AuditId				INT									
	,	@AuditDate				DATETIME		= NULL				
	,	@SystemEntityType		VARCHAR(50)		= 'Log4Net'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @Id OUTPUT, @AuditId
	
	INSERT INTO dbo.Log4Net 
	(
			LogUser
		,	Date
		,	StackTrace
		,	Thread
		,	Level
		,	Logger
		,	Message
		,	Computer
		,	Exception
		,	ApplicationId	
		,	ConnectionKey				
	)
	VALUES 
	(  
			@LogUser
		,	@Date
		,	@StackTrace
		,	@Thread
		,	@Level
		,	@Logger
		,	@Message
		,	@Computer
		,	@Exception
		,	@ApplicationId	
		,	@ConnectionKey		
	)

	SET @Id = SCOPE_IDENTITY()

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @Id
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 