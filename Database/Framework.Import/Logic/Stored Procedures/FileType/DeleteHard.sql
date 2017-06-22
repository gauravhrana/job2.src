IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FileTypeDeleteHard')
BEGIN
	PRINT 'Dropping Procedure FileTypeDeleteHard'
	DROP  Procedure FileTypeDeleteHard
END
GO

PRINT 'Creating Procedure FileTypeDeleteHard'
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
CREATE Procedure dbo.FileTypeDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50) = 'FileType'
)
AS
BEGIN

	IF @KeyType = 'FileTypeId'
	BEGIN

		EXEC	dbo.BatchFileDeleteHard 
				@KeyId		=	@KeyId, 
				@KeyType	=	'FileTypeId',
				@AuditId	=	@AuditId

		DELETE	 dbo.FileType
		WHERE	 FileTypeId = @KeyId


	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
