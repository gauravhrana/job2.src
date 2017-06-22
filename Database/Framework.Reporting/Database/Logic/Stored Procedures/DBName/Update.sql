IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DBNameUpdate')
BEGIN
	PRINT 'Dropping Procedure DBNameUpdate'
	DROP  Procedure  DBNameUpdate
END
GO

PRINT 'Creating Procedure DBNameUpdate'
GO

/******************************************************************************
**		File: 
**		Name: DBNameUpdate
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

CREATE Procedure dbo.DBNameUpdate
(
		@DBNameId			INT				= NULL	 			
	,	@Name						VARCHAR(50)				
	,	@Description				VARCHAR(500)			
	,	@SortOrder					INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'DBName'
)
AS
BEGIN
	UPDATE	dbo.DBName 
	SET		Name					=	@Name				
		,	[Description]			=	@Description				
		,	SortOrder				=	@SortOrder							
	WHERE	DBNameId		=	@DBNameId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'DBName'
		,	@EntityKey				= @DBNameId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END		
 GO