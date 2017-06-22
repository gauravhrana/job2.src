IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SubscriberApplicationRoleInsert')
BEGIN
	PRINT 'Dropping Procedure SubscriberApplicationRoleInsert'
	DROP  Procedure SubscriberApplicationRoleInsert
END
GO

PRINT 'Creating Procedure SubscriberApplicationRoleInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:SubscriberApplicationRoleInsert
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

CREATE Procedure dbo.SubscriberApplicationRoleInsert
(
		@SubscriberApplicationRoleId	INT				= NULL 	OUTPUT	
	,	@Name							VARCHAR(50)						
	,	@Description					VARCHAR(50)						
	,	@SortOrder						INT								
	,	@AuditId						INT									
	,	@AuditDate						DATETIME		= NULL				
	,	@SystemEntityType				VARCHAR(50)		= 'SubscriberApplicationRole'
)
AS
BEGIN

    EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @SubscriberApplicationRoleId OUTPUT, @AuditId
	
	INSERT INTO dbo.SubscriberApplicationRole 
	( 
			SubscriberApplicationRoleId	
		,	Name						
		,	Description					
		,	SortOrder						
	)
	VALUES 
	(  
			@SubscriberApplicationRoleId	
		,	@Name						
		,	@Description				
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @SubscriberApplicationRoleId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 