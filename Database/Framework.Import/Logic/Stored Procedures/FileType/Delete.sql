IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FileTypeDelete')
BEGIN
	PRINT 'Dropping Procedure FileTypeDelete'
	DROP  Procedure FileTypeDelete
END
GO

PRINT 'Creating Procedure FileTypeDelete'
GO
/******************************************************************************
**		File: 
**		Name: FileTypeDelete
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
CREATE Procedure dbo.FileTypeDelete
(
		@FileTypeId 		INT				
	,	@AuditId			INT						
	,	@AuditDate			DATETIME	= NULL		
	,	@SystemEntityType	VARCHAR(50)	= 'FileType'
)
AS
BEGIN

	DELETE		dbo.FileType
	WHERE		FileTypeId			= @FileTypeId 	

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FileTypeId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
