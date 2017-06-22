IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRelationInsert')
BEGIN
	PRINT 'Dropping Procedure ApplicationRelationInsert'
	DROP  Procedure ApplicationRelationInsert
END
GO

PRINT 'Creating Procedure ApplicationRelationInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ApplicationRelationInsert
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

CREATE Procedure dbo.ApplicationRelationInsert
(
		@ApplicationRelationId				INT			= NULL 	OUTPUT		
	,	@PublisherApplicationId				INT			
	,	@SubscriberApplicationId			INT	
	,	@SystemEntityTypeId					INT	
	,	@SubscriberApplicationRoleId		INT	
	,	@AuditId							INT									
	,	@AuditDate							DATETIME	= NULL				
	,	@SystemEntityType					VARCHAR(50)	= 'ApplicationRelation'
)
AS
BEGIN
	
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ApplicationRelationId OUTPUT, @AuditId
	
	INSERT INTO dbo.ApplicationRelation 
	( 
			ApplicationRelationId						
		,	PublisherApplicationId	
		,	SubscriberApplicationId	
		,	SystemEntityTypeId		
		,	SubscriberApplicationRoleId
	)
	VALUES 
	(  
			@ApplicationRelationId					
		,	@PublisherApplicationId	
		,	@SubscriberApplicationId	
		,	@SystemEntityTypeId		
		,	@SubscriberApplicationRoleId
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationRelationId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END	
GO

 