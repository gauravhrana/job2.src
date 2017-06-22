IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'Log4NetCleanup')
BEGIN
	PRINT 'Dropping Procedure Log4NetCleanup'
	DROP  Procedure Log4NetCleanup
END
GO

PRINT 'Creating Procedure Log4NetCleanup'
GO
/******************************************************************************
**		File: 
**		Level: Log4NetCleanup
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
CREATE Procedure dbo.Log4NetCleanup
(
		@CleanupDate			DATETIME
	,	@ApplicationId			INT		
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'Log4Net'
)
AS
BEGIN

	DELETE FROM dbo.Log4Net
	WHERE	Date			<	@CleanupDate
	AND		ApplicationId	=	@ApplicationId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
