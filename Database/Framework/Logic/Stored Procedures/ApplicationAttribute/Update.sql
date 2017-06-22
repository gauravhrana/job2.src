IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationAttributeUpdate')
BEGIN
	PRINT 'Dropping Procedure ApplicationAttributeUpdate'
	DROP  Procedure  ApplicationAttributeUpdate
END
GO

PRINT 'Creating Procedure ApplicationAttributeUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationAttributeUpdate
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

CREATE Procedure dbo.ApplicationAttributeUpdate
(
		@ApplicationId				INT				
	,	@RenderApplicationFilter	INT
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'ApplicationAttribute'
)
AS
BEGIN
	UPDATE	dbo.ApplicationAttribute 
	SET		ApplicationId					=	@ApplicationId				
		,	RenderApplicationFilter			=	@RenderApplicationFilter
	WHERE	ApplicationId					=	@ApplicationId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ApplicationAttribute'
		,	@EntityKey				= @ApplicationId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END		
 GO