IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'Log4NetUpdate')
BEGIN
	PRINT 'Dropping Procedure Log4NetUpdate'
	DROP  Procedure  Log4NetUpdate
END
GO

PRINT 'Creating Procedure Log4NetUpdate'
GO

/******************************************************************************
**		File: 
**		Level: Log4NetUpdate
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
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.Log4NetUpdate
(
		@Id						INT		 			
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
	,	@ConnectionKey VARCHAR(100)
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'Log4Net'
)
AS
BEGIN 

	UPDATE	dbo.Log4Net
	SET		LogUser			=	@LogUser
		,	Date			=	@Date
		,	StackTrace		=	@StackTrace
		,	Thread			=	@Thread
		,	Level			=	@Level
		,	Logger			=	@Logger
		,	Message			=	@Message
		,	Computer		=	@Computer
		,	Exception		=	@Exception
		,	ApplicationId	=	@ApplicationId	
		,	ConnectionKey	=	@ConnectionKey
	WHERE	Id	=	@Id

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @Id
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO