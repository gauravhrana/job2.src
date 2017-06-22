IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationOperationClone')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationClone'
	DROP  Procedure ApplicationOperationClone
END
GO

PRINT 'Creating Procedure ApplicationOperationClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ApplicationOperationClone
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

CREATE Procedure dbo.ApplicationOperationClone
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
	
	SELECT	@Name				= Name
		,	@Description		= Description
		,	@SortOrder			= SortOrder	
		,   @ApplicationId		= ApplicationId
		,	@SystemEntityTypeId	= SystemEntityTypeId
		,	@OperationValue		= OperationValue			
	FROM	dbo.ApplicationOperation
	WHERE   ApplicationOperationId	= @ApplicationOperationId
	ORDER BY ApplicationOperationId

	EXEC dbo.ApplicationOperationInsert 
			@ApplicationOperationId	=	NULL
		,	@Name				=	@Name
		,	@Description		=	@Description
		,	@SortOrder			=	@SortOrder
		,	@ApplicationId	    =	@ApplicationId
		,	@SystemEntityTypeId =	@SystemEntityTypeId
		,	@OperationValue		=	@OperationValue
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationOperationId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
