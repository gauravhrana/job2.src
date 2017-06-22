IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FileTypeInsert')
BEGIN
	PRINT 'Dropping Procedure FileTypeInsert'
	DROP  Procedure FileTypeInsert
END
GO

PRINT 'Creating Procedure FileTypeInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:FileTypeInsert
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
**********************************************************************************************/

CREATE Procedure dbo.FileTypeInsert
(
		@FileTypeId				INT				= NULL 	OUTPUT
	,	@ApplicationId			INT		
	,	@Name					VARCHAR(50)							
	,	@Description			VARCHAR(50)							
	,	@SortOrder				INT									
	,	@AuditId				INT										
	,	@AuditDate				DATETIME		= NULL				
	,	@SystemEntityType		VARCHAR(50)		= 'FileType'
)
AS
BEGIN
	
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @FileTypeId OUTPUT, @AuditId
		
	INSERT INTO dbo.FileType 
	( 
			FileTypeId	
		,	ApplicationId					
		,	Name			
		,	Description		
		,	SortOrder						
	)
	VALUES 
	(  
			@FileTypeId	
		,	@ApplicationId	
		,	@Name			
		,	@Description	
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	 
		,	@EntityKey				= @FileTypeId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 