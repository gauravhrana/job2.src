IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MealDetailDelete')
BEGIN
	PRINT 'Dropping Procedure MealDetailDelete'
	DROP  Procedure  MealDetailDelete
END
GO

PRINT 'Creating Procedure MealDetailDelete'
GO

/******************************************************************************
**		File: 
**		Name: MealDetailsDelete
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

CREATE Procedure dbo.MealDetailDelete
(
	    @MealDetailId		INT
	,	@ApplicationId		INT      = NULL
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME = NULL	
	,   @SystemEntityType   VARCHAR(50) = 'MealDetail'
)
AS
 BEGIN

	DELETE	MealDetail
	WHERE	MealDetailId = @MealDetailId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @MealDetailId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
			
 END
GO

