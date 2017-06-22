IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AuditActionDelete')
BEGIN
	PRINT 'Dropping Procedure AuditActionDelete'
	DROP  Procedure AuditActionDelete
END
GO

PRINT 'Creating Procedure AuditActionDelete'
GO


/******************************************************************************
**		File: 
**		Name: AuditActionDelete
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

CREATE Procedure dbo.AuditActionDelete
(
		@AuditActionId 		INT						
	,	@AuditId			INT						
	,	@AuditDate			DATETIME	= NULL		
	,	@SystemEntityType	VARCHAR(50)	= 'AuditAction'
)
AS	
BEGIN

	DELETE	 dbo.AuditAction
	WHERE	 AuditActionId = @AuditActionId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @AuditActionId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
