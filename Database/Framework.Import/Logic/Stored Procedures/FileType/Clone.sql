IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FileTypeClone')
BEGIN
	PRINT 'Dropping Procedure FileTypeClone'
	DROP  Procedure FileTypeClone
END
GO

PRINT 'Creating Procedure FileTypeClone'
GO

/*********************************************************************************************
**		File: 
**		Name: FileTypeClone
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
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.FileTypeClone
(
		@FileTypeId				INT			= NULL 	OUTPUT
	,	@ApplicationId			INT			= NULL		
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(50)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50) = 'FileType'				
)
AS
BEGIN		
	
	SELECT	@ApplicationId		= ApplicationId
		,	@Description		= Description
		,	@SortOrder			= SortOrder				
	FROM	dbo.FileType
	WHERE	FileTypeId			= @FileTypeId

	EXEC dbo.FileTypeInsert 
			@FileTypeId			=	NULL
		,	@ApplicationId		=	@ApplicationId
		,	@Name				=	@Name
		,	@Description		=	@Description
		,	@SortOrder			=	@SortOrder
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FileTypeId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
