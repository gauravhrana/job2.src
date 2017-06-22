IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleasePublishCategoryUpdate')
BEGIN
	PRINT 'Dropping Procedure ReleasePublishCategoryUpdate'
	DROP  Procedure  ReleasePublishCategoryUpdate
END
GO

PRINT 'Creating Procedure ReleasePublishCategoryUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ReleasePublishCategoryUpdate
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

CREATE Procedure dbo.ReleasePublishCategoryUpdate
(
		@ReleasePublishCategoryId		INT 			
	,	@Name							VARCHAR(50)				
	,	@Description					VARCHAR (500)			
	,	@SortOrder						INT					
	,	@AuditId						INT					
	,	@AuditDate						DATETIME		= NULL	
	,	@SystemEntityType				VARCHAR(50)		= 'ReleasePublishCategory'
)
AS
BEGIN
 
 	UPDATE	dbo.ReleasePublishCategory 
	SET		Name							=	@Name				
		,	Description						=	@Description				
		,	SortOrder						=	@SortOrder						
	WHERE	ReleasePublishCategoryId		=	@ReleasePublishCategoryId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReleasePublishCategory'
		,	@EntityKey				= @ReleasePublishCategoryId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END		
 GO