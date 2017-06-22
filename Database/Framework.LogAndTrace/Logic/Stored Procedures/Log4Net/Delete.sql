IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'Log4NetDelete')
BEGIN
	PRINT 'Dropping Procedure Log4NetDelete'
	DROP  Procedure Log4NetDelete
END
GO

PRINT 'Creating Procedure Log4NetDelete'
GO
/******************************************************************************
**		File: 
**		Level: Log4NetDelete
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
CREATE Procedure dbo.Log4NetDelete
(
		@Id 					INT	
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'Log4Net'
)
AS
BEGIN

	DELETE	 dbo.Log4Net
	WHERE	 Id = @Id

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @Id
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
