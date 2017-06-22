IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserTitleClone')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserTitleClone'
	DROP  Procedure ApplicationUserTitleClone
END
GO

PRINT 'Creating Procedure ApplicationUserTitleClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ApplicationUserTitleClone
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
**		
**********************************************************************************************/

CREATE Procedure dbo.ApplicationUserTitleClone
(
		@ApplicationUserTitleId		INT			= NULL 	OUTPUT		
	,	@Name				VARCHAR(50)						
	,	@Description		VARCHAR(50)						
	,	@SortOrder			INT								
	,	@AuditId			INT									
	,	@AuditDate			DATETIME	= NULL				
	,	@SystemEntityType	VARCHAR(50)	= 'ApplicationUserTitle'					
)
AS
BEGIN		
	
	SELECT	@Name				= Name
		,	@Description		= Description
		,	@SortOrder			= SortOrder				
	FROM	dbo.ApplicationUserTitle
	WHERE   ApplicationUserTitleId		= @ApplicationUserTitleId

	EXEC dbo.ApplicationUserTitleInsert 
			@ApplicationUserTitleId	=	NULL
		,	@Name			=	@Name
		,	@Description	=	@Description
		,	@SortOrder		=	@SortOrder
		,	@AuditId		=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType				= @SystemEntityType
		,	@EntityKey						= @ApplicationUserTitleId
		,	@AuditAction					= 'Clone'
		,	@CreatedDate					= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
