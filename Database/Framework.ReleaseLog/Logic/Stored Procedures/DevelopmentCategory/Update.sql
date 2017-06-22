IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DevelopmentCategoryUpdate')
BEGIN
	PRINT 'Dropping Procedure DevelopmentCategoryUpdate'
	DROP  Procedure  DevelopmentCategoryUpdate
END
GO

PRINT 'Creating Procedure DevelopmentCategoryUpdate'
GO

/******************************************************************************
**		File: 
**		Name: DevelopmentCategoryUpdate
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
CREATE Procedure dbo.DevelopmentCategoryUpdate
(
		@DevelopmentCategoryId			INT			 			
	,	@Name							VARCHAR(50)				
	,	@Description					VARCHAR(500)			
	,	@SortOrder						INT						 
	,	@AuditId						INT					
	,	@AuditDate						DATETIME	= NULL	
	,	@SystemEntityType				VARCHAR(50)	= 'DevelopmentCategory'
)
AS
BEGIN 

	DECLARE		@DateModified		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT

	SET			@DateModified		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId


	UPDATE	dbo.DevelopmentCategory 
	SET		Name						=	@Name		
		,	Description					=	@Description				
		,	SortOrder					=	@SortOrder	
		,   DateModified				=	@DateModified
		,	ModifiedByAuditId			=   @ModifiedByAuditId	
	WHERE	DevelopmentCategoryId	=	@DevelopmentCategoryId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'DevelopmentCategory'
		,	@EntityKey				= @DevelopmentCategoryId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

 END		
 GO