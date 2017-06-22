IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MealDelete')
BEGIN
	PRINT 'Dropping Procedure MealDelete'
	DROP  Procedure  MealDelete
END
GO

PRINT 'Creating Procedure MealDelete'
GO

/******************************************************************************
**		File: 
**		Name: MealDelete
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

CREATE Procedure dbo.MealDelete
(
	    @MealId	            INT
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME	= NULL
	,	@SystemEntityType	VARCHAR(50)	= 'Meal'
)
AS
 BEGIN
	DELETE	dbo.Meal
	WHERE	MealId = @MealId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @MealId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END
GO

