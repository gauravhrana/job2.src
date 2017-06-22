IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRelationUpdate')
BEGIN
	PRINT 'Dropping Procedure ApplicationRelationUpdate'
	DROP  Procedure  ApplicationRelationUpdate
END
GO

PRINT 'Creating Procedure ApplicationRelationUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationRelationUpdate
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
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.ApplicationRelationUpdate
(
		@ApplicationRelationId				INT		 			
	,	@PublisherApplicationId				INT			
	,	@SubscriberApplicationId				INT	
	,	@SystemEntityTypeId					INT	
	,	@SubscriberApplicationRoleId			INT		
	,	@AuditId							INT						
	,	@AuditDate							DATETIME	= NULL	
	,	@SystemEntityType					VARCHAR(50)	= 'ApplicationRelation'
)
AS
BEGIN 

	UPDATE	dbo.ApplicationRelation 
	SET		PublisherApplicationId		=	@PublisherApplicationId	
		,	SubscriberApplicationId		=   @SubscriberApplicationId
		,	SystemEntityTypeId			=	@SystemEntityTypeId
		,	SubscriberApplicationRoleId	=	@SubscriberApplicationRoleId
	WHERE	ApplicationRelationId		=	@ApplicationRelationId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ApplicationRelation'
		,	@EntityKey				= @ApplicationRelationId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END		
GO