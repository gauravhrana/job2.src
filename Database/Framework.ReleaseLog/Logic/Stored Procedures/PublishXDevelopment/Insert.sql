IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PublishXDevelopmentInsert')
BEGIN
	PRINT 'Dropping Procedure PublishXDevelopmentInsert'
	DROP  Procedure PublishXDevelopmentInsert
END
GO

PRINT 'Creating Procedure PublishXDevelopmentInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:PublishXDevelopmentInsert
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
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**********************************************************************************************/

CREATE Procedure dbo.PublishXDevelopmentInsert
(
		@PublishXDevelopmentId			INT			= NULL 	OUTPUT		
	,	@PublishId					INT								
	,	@DevelopmentId				INT						
	,	@ApplicationId			INT		
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'PublishXDevelopment'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @PublishXDevelopmentId OUTPUT, @AuditId
	
	INSERT INTO dbo.PublishXDevelopment 
	( 
			PublishXDevelopmentId						
		,	PublishId				
		,	DevelopmentId
		,	ApplicationId						
	)
	VALUES 
	(  
			@PublishXDevelopmentId					
		,	@PublishId			
		,	@DevelopmentId	
		,	@ApplicationId		
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @PublishXDevelopmentId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 