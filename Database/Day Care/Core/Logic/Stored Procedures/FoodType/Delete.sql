IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FoodTypeDelete')
BEGIN
	PRINT 'Dropping Procedure FoodTypeDelete'
	DROP  Procedure  FoodTypeDelete
END
GO

PRINT 'Creating Procedure FoodTypeDelete'
GO

/******************************************************************************
**		File: 
**		Name: FoodTypeDelete
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
CREATE Procedure dbo.FoodTypeDelete
(
	    @FoodTypeId   		INT
	,   @AuditId			INT			
    ,   @AuditDate			DATETIME	= NULL
	,	@SystemEntityType	VARCHAR(50)	= 'FoodType'
)
AS
 BEGIN
	
	DELETE	dbo.FoodType
	WHERE	FoodTypeId = @FoodTypeId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @FoodTypeId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
GO

