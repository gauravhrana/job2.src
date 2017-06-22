IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MealDetailDetails')
BEGIN
	PRINT 'Dropping Procedure MealDetailDetails'
	DROP  Procedure MealDetailDetails
END
GO

PRINT 'Creating Procedure MealDetailDetails'
GO


/******************************************************************************
**		File: 
**		Name: MealDetails_Details
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

CREATE Procedure dbo.MealDetailDetails
(
		@MealDetailId		INT
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME	= NULL	
	,	@SystemEntityType	VARCHAR(50)	= 'MealDetail'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@MealDetailId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

	SELECT	MealDetailId 
		,	ApplicationId
		,	MealId       
		,	FoodTypeId   
		,	AmtFinished	
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'							
	FROM	MealDetail 
	WHERE	MealDetailId = @MealDetailId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityTypeId		= 'MealDetails'
		,	@EntityKey				= @MealDetailId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END		
GO
   