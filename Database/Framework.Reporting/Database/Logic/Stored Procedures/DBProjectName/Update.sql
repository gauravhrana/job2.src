IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DBProjectNameUpdate')
BEGIN
	PRINT 'Dropping Procedure DBProjectNameUpdate'
	DROP  Procedure  DBProjectNameUpdate
END
GO

PRINT 'Creating Procedure DBProjectNameUpdate'
GO

/******************************************************************************
**		File: 
**		Name: DBProjectNameUpdate
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

CREATE Procedure dbo.DBProjectNameUpdate
(
		@DBProjectNameId			INT				= NULL	 			
	,	@Name						VARCHAR(50)				
	,	@Description				VARCHAR(500)			
	,	@SortOrder					INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'DBProjectName'
)
AS
BEGIN
	UPDATE	dbo.DBProjectName 
	SET		Name					=	@Name				
		,	[Description]			=	@Description				
		,	SortOrder				=	@SortOrder							
	WHERE	DBProjectNameId		=	@DBProjectNameId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'DBProjectName'
		,	@EntityKey				= @DBProjectNameId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END		
 GO