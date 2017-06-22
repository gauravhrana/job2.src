IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MealUpdate')
BEGIN
	PRINT 'Dropping Procedure MealUpdate'
	DROP  Procedure  MealUpdate
END
GO

PRINT 'Creating Procedure MealUpdate'
GO

/******************************************************************************
**		File: 
**		Name: MealUpdate
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

CREATE Procedure dbo.MealUpdate
(       
		@MealId				INT	
	,	@StudentId			INT	
	,	@Date				Datetime
	,	@MealTypeId			INT			
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME	= NULL	
	,	@SystemEntityType	VARCHAR(50)	= 'Meal'
)
AS
 BEGIN	
	UPDATE	dbo.Meal
	SET		StudentId				= @StudentId	
		,	Date					= @Date			
		,	MealTypeId				= @MealTypeId
	WHERE	MealId		     		= @MealId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @MealId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END
GO

