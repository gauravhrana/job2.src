IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MealTypeClone')
BEGIN
	PRINT 'Dropping Procedure MealTypeClone'
	DROP  Procedure MealTypeClone
END
GO

PRINT 'Creating Procedure MealTypeClone'
GO

/*********************************************************************************************
**		File: 
**		Name: MealTypeClone
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

CREATE Procedure dbo.MealTypeClone
(
		@MealTypeId				INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT			
	,	@Name					VARCHAR(50)							
	,	@Description            VARCHAR (500)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'MealType'				
)
AS
BEGIN

	IF @MealTypeId IS  NULL OR @MealTypeId = -9999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @MealTypeId OUTPUT
	END	
		
	
	SELECT	@ApplicationId		= ApplicationId
		,	@Name				= Name
		,	@Description		= Description
		,	@SortOrder			= SortOrder				
	FROM	dbo.MealType
	WHERE	MealTypeId			= @MealTypeId  
	AND		ApplicationId		= @ApplicationId

	EXEC dbo.MealTypeInsert 
			@MealTypeId			=	NULL
		,	@ApplicationId		=   @ApplicationId
		,	@Name				=	@Name
		,	@Description		=	@Description
		,	@SortOrder			=	@SortOrder
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @MealTypeId
		,	@AuditAction			= 'Clone' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
