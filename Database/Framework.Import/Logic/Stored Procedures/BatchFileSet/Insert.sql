IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileSetInsert')
BEGIN
	PRINT 'Dropping Procedure BatchFileSetInsert'
	DROP  Procedure BatchFileSetInsert
END
GO

PRINT 'Creating Procedure BatchFileSetInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:BatchFileSetInsert
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

CREATE Procedure dbo.BatchFileSetInsert
(
		@BatchFileSetId			INT			= NULL 	OUTPUT	
	,	@ApplicationId			INT	
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(50)						
	,	@CreatedDate			DATETIME						
	,	@CreatedByPersonId		INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'BatchFileSet'
)
AS
BEGIN
	
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @BatchFileSetId OUTPUT, @AuditId
		
	INSERT INTO dbo.BatchFileSet 
	( 
			BatchFileSetId	
		,	ApplicationId					
		,	Name				
		,	Description			
		,	CreatedDate			
		,	CreatedByPersonId
	)
	VALUES 
	(  
			@BatchFileSetId	
		,	@ApplicationId	
		,	@Name				
		,	@Description		
		,	@CreatedDate		
		,	@CreatedByPersonId		
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @BatchFileSetId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END	
GO

 