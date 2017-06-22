IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ThemeDetailsUpdate')
BEGIN
	PRINT 'Dropping Procedure ThemeDetailsUpdate'
	DROP  Procedure  ThemeDetailsUpdate
END
GO

PRINT 'Creating Procedure ThemeDetailsUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ThemeDetailsUpdate
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

CREATE Procedure dbo.ThemeDetailsUpdate
(
		@ThemeDetailId			INT			= NULL 	OUTPUT	
	,	@ApplicationId	        INT         = NULL
	,	@Value					VARCHAR(50)						
	,	@ThemeId				INT						
	,	@ThemeKeyId				INT
	,	@ThemeCategoryId		INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ThemeDetails'
)
AS
BEGIN
	UPDATE	dbo.ThemeDetails 
	SET		 ApplicationId		=   @ApplicationId
		,	 ThemeId			=	@ThemeId
		,	 ThemeKeyId			=	@ThemeKeyId
		,	 ThemeCategoryId	=	@ThemeCategoryId
		,	 Value				=	@Value							
	WHERE	 ThemeDetailId		=	@ThemeDetailId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ThemeDetails'
		,	@EntityKey				= @ThemeDetailId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END		
 GO