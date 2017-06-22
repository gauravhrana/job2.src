IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FileTypeUpdate')
BEGIN
	PRINT 'Dropping Procedure FileTypeUpdate'
	DROP  Procedure  FileTypeUpdate
END
GO

PRINT 'Creating Procedure FileTypeUpdate'
GO

/******************************************************************************
**		File: 
**		Name: FileTypeUpdate
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
**     ----------							-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.FileTypeUpdate
(
		@FileTypeId				INT			
	,	@Name					VARCHAR(50)					
	,	@Description			VARCHAR(50)				
	,	@SortOrder				INT						
	,	@AuditId				INT							
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'FileType'
)
AS
BEGIN 

	UPDATE	dbo.FileType 
	SET		Name			=	@Name	
		,	Description		=	@Description				
		,	SortOrder		=	@SortOrder							
	WHERE	FileTypeId		=	@FileTypeId 

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @FileTypeId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO