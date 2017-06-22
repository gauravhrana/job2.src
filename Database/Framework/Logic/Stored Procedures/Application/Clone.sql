IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationClone')
BEGIN
	PRINT 'Dropping Procedure ApplicationClone'
	DROP  Procedure ApplicationClone
END
GO

PRINT 'Creating Procedure ApplicationClone'
GO

/*********************************************************************************************
**		Task: 
**		Name: ApplicationClone
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

CREATE Procedure dbo.ApplicationClone
(
		@ApplicationId			INT			= NULL 	OUTPUT		
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(50)						
	,	@SortOrder				INT	
	,	@Code					VARCHAR(50)													
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'Application'	 			
)
AS
BEGIN		
	
	SELECT	@Description		=	Description
		,	@SortOrder			=	SortOrder
		,	@Code				=	Code				
	FROM	dbo.Application
	WHERE	ApplicationId		=	@ApplicationId

	EXEC dbo.ApplicationInsert 
			@ApplicationId		=	NULL
		,	@Name				=	@Name
		,	@Description		=	@Description
		,	@SortOrder			=	@SortOrder
		,	@Code				=	@Code
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
