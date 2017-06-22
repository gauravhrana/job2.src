IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ThemeDetailUpdate')
BEGIN
	PRINT 'Dropping Procedure ThemeDetailUpdate'
	DROP  Procedure  ThemeDetailUpdate
END
GO

PRINT 'Creating Procedure ThemeDetailUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ThemeDetailUpdate
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

CREATE Procedure dbo.ThemeDetailUpdate
(
		@ThemeDetailId			INT			= NULL 	OUTPUT	
	,	@ApplicationId	        INT         = NULL
	,	@Value					VARCHAR(50)						
	,	@ThemeId				INT						
	,	@ThemeKeyId				INT
	,	@ThemeCategoryId		INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ThemeDetail'
)
AS
BEGIN
	UPDATE	dbo.ThemeDetail 
	SET		 ApplicationId		=   @ApplicationId
		,	 ThemeId			=	@ThemeId
		,	 ThemeKeyId			=	@ThemeKeyId
		,	 ThemeCategoryId	=	@ThemeCategoryId
		,	 Value				=	@Value							
	WHERE	 ThemeDetailId		=	@ThemeDetailId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ThemeDetail'
		,	@EntityKey				= @ThemeDetailId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END		
 GO