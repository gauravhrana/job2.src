IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PublishXDevelopmentDelete')
BEGIN
	PRINT 'Dropping Procedure PublishXDevelopmentDelete'
	DROP  Procedure PublishXDevelopmentDelete
END
GO

PRINT 'Creating Procedure PublishXDevelopmentDelete'
GO
/******************************************************************************
**		File: 
**		Name: PublishXDevelopmentDelete
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
CREATE Procedure dbo.PublishXDevelopmentDelete
(
		@PublishXDevelopmentId 		INT			= NULL		
	,	@PublishId 				INT			= NULL		
	,	@DevelopmentId 				INT			= NULL		
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'PublishXDevelopment'
)
AS
BEGIN

	DELETE	dbo.PublishXDevelopment
	WHERE	PublishXDevelopmentId =	ISNULL(@PublishXDevelopmentId,	PublishXDevelopmentId)	
	AND		PublishId			=	ISNULL(@PublishId,			PublishId)
	AND		DevelopmentId		=	ISNULL(@DevelopmentId,			DevelopmentId)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'PublishXDevelopment'
		,	@EntityKey				= @PublishXDevelopmentId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
