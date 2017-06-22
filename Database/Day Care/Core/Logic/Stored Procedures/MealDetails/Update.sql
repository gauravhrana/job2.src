IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MealDetailUpdate')
BEGIN
	PRINT 'Dropping Procedure MealDetailUpdate'
	DROP  Procedure  MealDetailUpdate
END
GO

PRINT 'Creating Procedure MealDetailUpdate'
GO

/******************************************************************************
**		File: 
**		Name: MealDetailUpdate
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
**		----------						-----------
**
**		Auth:
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.MealDetailUpdate
(       
		@MealDetailId		INT	
	,	@MealId	            INT				
	,	@FoodTypeId		    INT
	,	@AmtFinished        FLOAT
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME		= NULL
	,	@SystemEntityType	VARCHAR(50)		= 'MealDetail'
	 
)
AS
BEGIN

	UPDATE	dbo.MealDetail
	SET		MealDetailId           = @MealDetailId					
		,	MealId				   = @MealId			 
		,	FoodTypeId             = @FoodTypeId        
		,	AmtFinished			   = @AmtFinished
	WHERE	MealDetailId		   = @MealDetailId	

--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @MealDetailId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

 END
GO

