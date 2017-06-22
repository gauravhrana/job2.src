IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemEntityXSystemEntityCategoryUpdate')
BEGIN
	PRINT 'Dropping Procedure SystemEntityXSystemEntityCategoryUpdate'
	DROP  Procedure  SystemEntityXSystemEntityCategoryUpdate
END
GO

PRINT 'Creating Procedure SystemEntityXSystemEntityCategoryUpdate'
GO

/******************************************************************************
**		File: 
**		Name: SystemEntityXSystemEntityCategoryUpdate
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

CREATE Procedure dbo.SystemEntityXSystemEntityCategoryUpdate
(
		@SystemEntityXSystemEntityCategoryId		INT		 			
	,	@SystemEntityId				INT					
	,	@SystemEntityCategoryId				INT			
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'SystemEntityXSystemEntityCategory'
)
AS
BEGIN 

	UPDATE	dbo.SystemEntityXSystemEntityCategory 
	SET		SystemEntityId				=	@SystemEntityId			
		,	SystemEntityCategoryId				=	@SystemEntityCategoryId						
	WHERE	SystemEntityXSystemEntityCategoryId		=	@SystemEntityXSystemEntityCategoryId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SystemEntityXSystemEntityCategory'
		,	@EntityKey				= @SystemEntityXSystemEntityCategoryId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO