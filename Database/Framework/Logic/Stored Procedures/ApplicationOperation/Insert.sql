IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationOperationInsert')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationInsert'
	DROP  Procedure ApplicationOperationInsert
END
GO

PRINT 'Creating Procedure ApplicationOperationInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ApplicationOperationInsert
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

CREATE Procedure dbo.ApplicationOperationInsert
(
		@ApplicationOperationId		INT			= NULL 	OUTPUT		
	,	@Name						VARCHAR(50)						
	,	@Description				VARCHAR(50)						
	,	@SortOrder					INT								
	,	@ApplicationId				INT	                          
	,	@OperationValue				VARCHAR(50)						
	,	@AuditId					INT									
	,	@AuditDate					DATETIME	= NULL   			
	,	@SystemEntityTypeId			INT				
	,	@SystemEntityType			VARCHAR(50)	= 'ApplicationOperation'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ApplicationOperationId OUTPUT, @AuditId
	
	INSERT INTO dbo.ApplicationOperation 
	( 
			ApplicationOperationId
		,	Name				
		,	Description			
		,	SortOrder			
		,	ApplicationId		
		,	SystemEntityTypeId	
		,	OperationValue			
	)
	VALUES 
	(  
			@ApplicationOperationId			
		,	@Name							
		,	@Description					
		,	@SortOrder						
		,	@ApplicationId					
		,	@SystemEntityTypeId				
		,	@OperationValue
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationOperationId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 