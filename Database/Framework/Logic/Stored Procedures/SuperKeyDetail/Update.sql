IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SuperKeyDetailUpdate')
BEGIN
	PRINT 'Dropping Procedure SuperKeyDetailUpdate'
	DROP  Procedure  SuperKeyDetailUpdate
END
GO

PRINT 'Creating Procedure SuperKeyDetailUpdate'
GO

/******************************************************************************
**		File: 
**		Name: SuperKeyDetailUpdate
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

CREATE Procedure dbo.SuperKeyDetailUpdate
(
		@SuperKeyDetailId			INT		
	,	@EntityKey					INT	
	,	@SuperKeyId					INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'SuperKeyDetail'
)
AS
BEGIN

	UPDATE	dbo.SuperKeyDetail 
	SET		SuperKeyId				=	@SuperKeyId
		,	EntityKey				=	@EntityKey							
	WHERE	SuperKeyDetailId		=	@SuperKeyDetailId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SuperKeyDetail'
		,	@EntityKey				= @SuperKeyDetailId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

 END		
 GO