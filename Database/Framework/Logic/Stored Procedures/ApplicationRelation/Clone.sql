IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRelationClone')
BEGIN
	PRINT 'Dropping Procedure ApplicationRelationClone'
	DROP  Procedure ApplicationRelationClone
END
GO

PRINT 'Creating Procedure ApplicationRelationClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ApplicationRelationClone
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
**		
**********************************************************************************************/

CREATE Procedure dbo.ApplicationRelationClone
(
		@ApplicationRelationId				INT		 = NULL 	OUTPUT	
	,	@PublisherApplicationId				INT			
	,	@SubscriberApplicationId			INT	
	,	@SystemEntityTypeId					INT	
	,	@SubscriberApplicationRoleId		INT		
	,	@AuditId							INT									
	,	@AuditDate							DATETIME	 = NULL				
	,	@SystemEntityType					VARCHAR(50) = 'ApplicationRelation'
)
AS
BEGIN

	IF @ApplicationRelationId IS NULL OR @ApplicationRelationId = -999999
		BEGIN
			EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ApplicationRelationId OUTPUT
		END						
	
	SELECT	@PublisherApplicationId			=	PublisherApplicationId	
		,	@SubscriberApplicationId			=	SubscriberApplicationId	
		,	@SystemEntityTypeId				=	SystemEntityTypeId			
		,	@SubscriberApplicationRoleId	=	SubscriberApplicationRoleId
	FROM	dbo.ApplicationRelation
	WHERE   ApplicationRelationId				= @ApplicationRelationId
	ORDER BY ApplicationRelationId

	EXEC dbo.ApplicationRelationInsert 
			@ApplicationRelationId				=	NULL
		,   @PublisherApplicationId				=	@PublisherApplicationId	
		,	@SubscriberApplicationId				=   @SubscriberApplicationId	
		,	@SystemEntityTypeId					=	@SystemEntityTypeId		
		,	@SubscriberApplicationRoleId		=	@SubscriberApplicationRoleId
		,	@AuditId							=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ApplicationRelation'
		,	@EntityKey				= @ApplicationRelationId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
