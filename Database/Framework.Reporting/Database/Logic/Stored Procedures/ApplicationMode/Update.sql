IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationModeUpdate')
BEGIN
	PRINT 'Dropping Procedure ApplicationModeUpdate'
	DROP  Procedure  ApplicationModeUpdate
END
GO

PRINT 'Creating Procedure ApplicationModeUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationModeUpdate
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

CREATE Procedure dbo.ApplicationModeUpdate
(
		@ApplicationModeId			INT				= NULL	 			
	,	@Name						VARCHAR(50)				
	,	@Description				VARCHAR(500)			
	,	@SortOrder					INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'ApplicationMode'
)
AS
BEGIN
	UPDATE	dbo.ApplicationMode 
	SET		Name					=	@Name				
		,	Description				=	@Description				
		,	SortOrder				=	@SortOrder							
	WHERE	ApplicationModeId	=	@ApplicationModeId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ApplicationMode'
		,	@EntityKey				= @ApplicationModeId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END		
 GO