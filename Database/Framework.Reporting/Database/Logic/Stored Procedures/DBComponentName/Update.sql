IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DBComponentNameUpdate')
BEGIN
	PRINT 'Dropping Procedure DBComponentNameUpdate'
	DROP  Procedure  DBComponentNameUpdate
END
GO

PRINT 'Creating Procedure DBComponentNameUpdate'
GO

/******************************************************************************
**		File: 
**		Name: DBComponentNameUpdate
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

CREATE Procedure dbo.DBComponentNameUpdate
(
		@DBComponentNameId			INT				= NULL	 			
	,	@Name						VARCHAR(50)				
	,	@Description				VARCHAR(500)			
	,	@SortOrder					INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'DBComponentName'
)
AS
BEGIN
	UPDATE	dbo.DBComponentName 
	SET		Name					=	@Name				
		,	[Description]			=	@Description				
		,	SortOrder				=	@SortOrder							
	WHERE	DBComponentNameId		=	@DBComponentNameId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'DBComponentName'
		,	@EntityKey				= @DBComponentNameId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END		
 GO