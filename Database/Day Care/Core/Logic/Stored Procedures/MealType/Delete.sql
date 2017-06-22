IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MealTypeDelete')
BEGIN
	PRINT 'Dropping Procedure MealTypeDelete'
	DROP  Procedure  MealTypeDelete
END
GO

PRINT 'Creating Procedure MealTypeDelete'
GO

/******************************************************************************
**		File: 
**		Name: MealTypeeDelete
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

CREATE Procedure dbo.MealTypeDelete
(
	    @MealTypeId			INT	
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME	= NULL	
	,	@SystemEntityType	VARCHAR(50)	= 'MealType'
)
AS
BEGIN
	DELETE	dbo.MealType
	WHERE	MealTypeId = @MealTypeId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @MealTypeId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END
GO

