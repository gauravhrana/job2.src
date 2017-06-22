IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FoodTypeClone')
BEGIN
	PRINT 'Dropping Procedure FoodTypeClone'
	DROP  Procedure FoodTypeClone
END
GO

PRINT 'Creating Procedure FoodTypeClone'
GO

/*********************************************************************************************
**		File: 
**		Name: FoodTypeClone
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

CREATE Procedure dbo.FoodTypeClone
(
		@FoodTypeId				INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT			
	,	@Name					VARCHAR(50)							
	,	@Description            VARCHAR(500)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'FoodType'			
)
AS
BEGIN

	IF @FoodTypeId IS NULL OR @FoodTypeId = -9999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @FoodTypeId OUTPUT
	END	
		
	
	SELECT	@ApplicationId		= ApplicationId
		,	@Name				= Name
		,	@Description		= Description
		,	@SortOrder			= SortOrder				
	FROM	dbo.FoodType
	WHERE	FoodTypeId			= @FoodTypeId 
	AND		ApplicationId		= @ApplicationId

	EXEC dbo.FoodTypeInsert 
			@FoodTypeId			=	NULL
		,	@ApplicationId		=	@ApplicationId
		,	@Name				=	@Name
		,	@Description		=	@Description
		,	@SortOrder			=	@SortOrder
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @FoodTypeId
		,	@AuditAction			= 'Clone' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
