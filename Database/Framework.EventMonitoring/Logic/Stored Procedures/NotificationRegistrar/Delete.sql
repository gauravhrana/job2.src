IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationRegistrarDelete')
BEGIN
	PRINT 'Dropping Procedure NotificationRegistrarDelete'
	DROP  Procedure NotificationRegistrarDelete
END
GO

PRINT 'Creating Procedure NotificationRegistrarDelete'
GO
/******************************************************************************
**		File: 
**		Name: NotificationRegistrarDelete
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
**     ----------						-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Developer:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.NotificationRegistrarDelete
(
		@NotificationRegistrarId 			INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'NotificationRegistrar'
)
AS
BEGIN

	DELETE	 dbo.NotificationRegistrar
	WHERE	 NotificationRegistrarId = @NotificationRegistrarId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'NotificationRegistrar'
		,	@EntityKey				= @NotificationRegistrarId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
