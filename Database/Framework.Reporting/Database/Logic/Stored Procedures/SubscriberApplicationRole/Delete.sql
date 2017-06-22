IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SubscriberApplicationRoleDelete')
BEGIN
	PRINT 'Dropping Procedure SubscriberApplicationRoleDelete'
	DROP  Procedure SubscriberApplicationRoleDelete
END
GO

PRINT 'Creating Procedure SubscriberApplicationRoleDelete'
GO
/******************************************************************************
**		File: 
**		Name: SubscriberApplicationRoleDelete
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
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.SubscriberApplicationRoleDelete
(
		@SubscriberApplicationRoleId 		INT						
	,	@AuditId							INT						
	,	@AuditDate							DATETIME	= NULL		
	,	@SystemEntityType					VARCHAR(50)	= 'SubscriberApplicationRole'
)
AS
BEGIN

	DELETE	 dbo.SubscriberApplicationRole
	WHERE	 SubscriberApplicationRoleId = @SubscriberApplicationRoleId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SubscriberApplicationRole'
		,	@EntityKey				= @SubscriberApplicationRoleId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
