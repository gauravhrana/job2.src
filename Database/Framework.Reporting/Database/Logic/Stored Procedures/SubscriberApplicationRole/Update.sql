IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SubscriberApplicationRoleUpdate')
BEGIN
	PRINT 'Dropping Procedure SubscriberApplicationRoleUpdate'
	DROP  Procedure  SubscriberApplicationRoleUpdate
END
GO

PRINT 'Creating Procedure SubscriberApplicationRoleUpdate'
GO

/******************************************************************************
**		File: 
**		Name: SubscriberApplicationRoleUpdate
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
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.SubscriberApplicationRoleUpdate
(
		@SubscriberApplicationRoleId		INT				= NULL	 			
	,	@Name								VARCHAR(50)				
	,	@Description						VARCHAR(50)			
	,	@SortOrder							INT					
	,	@AuditId							INT					
	,	@AuditDate							DATETIME		= NULL	
	,	@SystemEntityType					VARCHAR(50)		= 'SubscriberApplicationRole'
)
AS
BEGIN

	UPDATE	dbo.SubscriberApplicationRole 
	SET		Name					=	@Name				
		,	Description				=	@Description				
		,	SortOrder				=	@SortOrder							
	WHERE	SubscriberApplicationRoleId	=	@SubscriberApplicationRoleId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SubscriberApplicationRole'
		,	@EntityKey				= @SubscriberApplicationRoleId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END		
GO