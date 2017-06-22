IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PublishXDevelopmentUpdate')
BEGIN
	PRINT 'Dropping Procedure PublishXDevelopmentUpdate'
	DROP  Procedure  PublishXDevelopmentUpdate
END
GO

PRINT 'Creating Procedure PublishXDevelopmentUpdate'
GO

/******************************************************************************
**		File: 
**		Name: PublishXDevelopmentUpdate
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

CREATE Procedure dbo.PublishXDevelopmentUpdate
(
		@PublishXDevelopmentId		INT		 			
	,	@DevelopmentId				INT					
	,	@PublishId			INT		
	,	@ApplicationId		INT			
	,	@AuditId			INT					
	,	@AuditDate			DATETIME	= NULL	
	,	@SystemEntityType	VARCHAR(50)	= 'PublishXDevelopment'
)
AS
BEGIN 

	UPDATE	dbo.PublishXDevelopment 
	SET		PublishId				=	@PublishId
		,	DevelopmentId			=	@DevelopmentId	
		,	ApplicationId					=	@ApplicationId						
	WHERE	PublishXDevelopmentId			=	@PublishXDevelopmentId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'PublishXDevelopment'
		,	@EntityKey				= @PublishXDevelopmentId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO