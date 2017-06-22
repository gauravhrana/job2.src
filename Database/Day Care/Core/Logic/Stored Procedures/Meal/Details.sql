IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MealDetails')
BEGIN
	PRINT 'Dropping Procedure MealDetails'
	DROP  Procedure MealDetails
END
GO

PRINT 'Creating Procedure MealDetails'
GO

/******************************************************************************
**		File: 
**		Name: Meal_Details
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
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.MealDetails
(
		@MealId				INT
	,   @AuditId			INT			
    ,   @AuditDate			DATETIME	= NULL	
	,	@SystemEntityType	VARCHAR(50)	= 'Meal'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@MealId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

	SELECT	MealId
		,	ApplicationId
		,	StudentId
		,	Date
		,	MealTypeId	
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'								
	FROM	dbo.Meal 
	WHERE	MealId = @MealId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @MealId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId		
END		
GO
   