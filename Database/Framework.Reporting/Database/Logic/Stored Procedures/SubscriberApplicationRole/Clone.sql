IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SubscriberApplicationRoleClone')
BEGIN
	PRINT 'Dropping Procedure SubscriberApplicationRoleClone'
	DROP  Procedure SubscriberApplicationRoleClone
END
GO

PRINT 'Creating Procedure SubscriberApplicationRoleClone'
GO

/*********************************************************************************************
**		File: 
**		Name: SubscriberApplicationRoleClone
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
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.SubscriberApplicationRoleClone
(
		@SubscriberApplicationRoleId	INT			= NULL 	OUTPUT	
	,	@Name							VARCHAR(50)						
	,	@Description					VARCHAR(50)						
	,	@SortOrder						INT								
	,	@AuditId						INT									
	,	@AuditDate						DATETIME	= NULL				
	,	@SystemEntityType				VARCHAR(50)	= 'SubscriberApplicationRole'
)
AS
BEGIN

	IF @SubscriberApplicationRoleId IS NULL OR @SubscriberApplicationRoleId = -999999
		BEGIN
			EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @SubscriberApplicationRoleId OUTPUT
		END						
	
	SELECT	@Description		=	Description
		,	@SortOrder			=	SortOrder				
	FROM	dbo.SubscriberApplicationRole
	WHERE   SubscriberApplicationRoleId		=	@SubscriberApplicationRoleId
	ORDER BY SubscriberApplicationRoleId

	EXEC dbo.SubscriberApplicationRoleInsert 
			@SubscriberApplicationRoleId		=	NULL
		,	@Name								=	@Name
		,	@Description						=	@Description
		,	@SortOrder							=	@SortOrder
		,	@AuditId							=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SubscriberApplicationRole'
		,	@EntityKey				= @SubscriberApplicationRoleId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
